using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Application.Invoices.DTOs;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ErpSystem.Infrastructure.Services;

public class PdfService : IPdfService
{
    public async Task<byte[]> GenerateInvoicePdfAsync(InvoiceDetailDto invoice)
    {
        return await Task.Run(() =>
        {
            using var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 50, 50, 50, 50);
            var writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();

            var fontPath = Path.Combine(
                AppContext.BaseDirectory,
                "Resources",
                "Fonts",
                "FreeSans.ttf"
            );
            if (!File.Exists(fontPath))
                throw new FileNotFoundException("Font file not found: " + fontPath);

            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var titleFont = new Font(baseFont, 18, Font.BOLD);
            var headerFont = new Font(baseFont, 12, Font.BOLD);
            var normalFont = new Font(baseFont, 10, Font.NORMAL);
            var cellFont = new Font(baseFont, 9, Font.NORMAL);
            var boldFont = new Font(baseFont, 10, Font.BOLD);

            var title = new Paragraph($"ФАКТУРА {invoice.InvoiceNumber}", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
            };
            document.Add(title);
            document.Add(new Paragraph(" "));

            var infoTable = new PdfPTable(2) { WidthPercentage = 100 };
            infoTable.SetWidths(new float[] { 1f, 1f });

            var leftCell = new PdfPCell { Border = Rectangle.NO_BORDER };
            leftCell.AddElement(new Paragraph("ERP", headerFont));
            leftCell.AddElement(new Paragraph("ЕИК: 123456789", normalFont));
            leftCell.AddElement(new Paragraph("Адрес: гр. София", normalFont));
            leftCell.AddElement(new Paragraph("Тел: +359 988 750 054", normalFont));
            infoTable.AddCell(leftCell);

            var rightCell = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            };
            rightCell.AddElement(
                new Paragraph($"Дата: {invoice.InvoiceDate:dd.MM.yyyy}", normalFont)
            );
            infoTable.AddCell(rightCell);

            document.Add(infoTable);
            document.Add(new Paragraph(" "));

            document.Add(new Paragraph("КЛИЕНТ:", headerFont));
            document.Add(new Paragraph($"{invoice.CustomerName}", normalFont));
            document.Add(new Paragraph($"Адрес: {invoice.CustomerAddress}", normalFont));
            if (!string.IsNullOrEmpty(invoice.CustomerPhone))
                document.Add(new Paragraph($"Телефон: {invoice.CustomerPhone}", normalFont));
            if (!string.IsNullOrEmpty(invoice.CustomerEmail))
                document.Add(new Paragraph($"Имейл: {invoice.CustomerEmail}", normalFont));

            document.Add(new Paragraph(" "));

            var itemsTable = new PdfPTable(5) { WidthPercentage = 100 };
            itemsTable.SetWidths(new float[] { 3f, 1f, 1.5f, 1.5f, 2f });

            void AddHeader(string text, int align = Element.ALIGN_RIGHT)
            {
                itemsTable.AddCell(
                    new PdfPCell(new Phrase(text, headerFont))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = align,
                    }
                );
            }

            AddHeader("Продукт", Element.ALIGN_LEFT);
            AddHeader("Кол.", Element.ALIGN_CENTER);
            AddHeader("Цена");
            AddHeader("ДДС");
            AddHeader("Общо");

            foreach (var item in invoice.Items)
            {
                itemsTable.AddCell(new PdfPCell(new Phrase(item.ProductName, cellFont)));
                itemsTable.AddCell(
                    new PdfPCell(new Phrase(item.Quantity.ToString(), cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    }
                );
                itemsTable.AddCell(
                    new PdfPCell(new Phrase($"{item.UnitPrice:F2} лв", cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    }
                );
                itemsTable.AddCell(
                    new PdfPCell(new Phrase($"{item.VatAmount:F2} лв", cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    }
                );
                itemsTable.AddCell(
                    new PdfPCell(new Phrase($"{item.LineTotal:F2} лв", cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    }
                );
            }

            document.Add(itemsTable);
            document.Add(new Paragraph(" "));

            var totalsTable = new PdfPTable(2)
            {
                WidthPercentage = 60,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            };
            totalsTable.SetWidths(new float[] { 2f, 1f });

            void AddTotalRow(string label, string value, Font font)
            {
                totalsTable.AddCell(
                    new PdfPCell(new Phrase(label, font))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    }
                );
                totalsTable.AddCell(
                    new PdfPCell(new Phrase(value, font))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                    }
                );
            }

            AddTotalRow("Сума без ДДС:", $"{invoice.SubTotal:F2} лв", normalFont);
            AddTotalRow("ДДС:", $"{invoice.VatAmount:F2} лв", normalFont);
            AddTotalRow("ОБЩО:", $"{invoice.TotalAmount:F2} лв", boldFont);

            document.Add(totalsTable);

            if (!string.IsNullOrEmpty(invoice.Notes))
            {
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("Забележки:", headerFont));
                document.Add(new Paragraph(invoice.Notes, normalFont));
            }

            document.Close();
            return memoryStream.ToArray();
        });
    }
}
