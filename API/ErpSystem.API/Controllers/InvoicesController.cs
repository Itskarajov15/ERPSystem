using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Application.Invoices.Commands.CreateInvoiceFromOrder;
using ErpSystem.Application.Invoices.Commands.UpdateInvoiceStatus;
using ErpSystem.Application.Invoices.Queries.GetInvoiceById;
using ErpSystem.Application.Invoices.Queries.GetInvoices;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.API.Controllers;

[Route("api/invoices")]
public class InvoicesController : BaseController
{
    private readonly IPdfService _pdfService;

    public InvoicesController(IMediator mediator, IPdfService pdfService)
        : base(mediator)
    {
        _pdfService = pdfService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] InvoiceFilters? invoiceFilters
    )
    {
        var query = new GetInvoicesQuery(paginationParams, invoiceFilters);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetInvoiceByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("create-from-order")]
    public async Task<IActionResult> CreateFromOrder(CreateInvoiceFromOrderCommand command)
    {
        var invoiceId = await _mediator.Send(command);

        return Ok(invoiceId);
    }

    [HttpPut("{id}/update-status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateInvoiceStatusCommand command)
    {
        if (id != command.InvoiceId)
            return BadRequest("Invoice ID mismatch");

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetInvoicePdf(Guid id)
    {
        var query = new GetInvoiceByIdQuery(id);
        var invoice = await _mediator.Send(query);

        if (invoice == null)
            return NotFound();

        var pdfBytes = await _pdfService.GenerateInvoicePdfAsync(invoice);

        return File(pdfBytes, "application/pdf", $"Invoice_{invoice.InvoiceNumber}.pdf");
    }
}
