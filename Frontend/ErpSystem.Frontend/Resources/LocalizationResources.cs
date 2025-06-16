namespace ErpSystem.Frontend.Resources;

public static class LocalizationResources
{
    public static readonly Dictionary<string, string> BulgarianTranslations =
        new()
        {
            // User Management Page
            { "User Management", "Управление на потребители" },
            { "Users", "Потребители" },
            { "Create New User", "Създай нов потребител" },
            { "Create New Role", "Създай нова роля" },
            { "Name", "Име" },
            { "Email", "Имейл" },
            { "Status", "Статус" },
            { "Roles", "Роли" },
            { "Created", "Създаден" },
            { "Last Login", "Последно влизане" },
            { "Actions", "Действия" },
            // Role Management
            { "Role Name", "Име на роля" },
            { "Description", "Описание" },
            { "Endpoint Permissions", "Права за достъп" },
            { "Cancel", "Отказ" },
            { "Create", "Създай" },
            { "Save Changes", "Запази промените" },
            { "Close", "Затвори" },
            // Common
            { "Active", "Активен" },
            { "Inactive", "Неактивен" },
            // Controller Names
            { "Authentication", "Автентикация" },
            { "Products", "Продукти" },
            { "Orders", "Поръчки" },
            { "Inventory", "Склад" },
            { "Customers", "Клиенти" },
            { "Deliveries", "Доставки" },
            { "Invoices", "Фактури" },
            { "PaymentMethods", "Методи на плащане" },
            { "Payments", "Плащания" },
            { "UnitsOfMeasure", "Мерни единици" },
            // Action Names
            { "Index", "Начало" },
            { "Edit", "Редактиране" },
            { "Delete", "Изтриване" },
            { "Details", "Детайли" },
            { "List", "Списък" },
            { "Update", "Обновяване" },
            { "Register", "Регистрация" },
            { "Login", "Вход" },
            { "Logout", "Изход" },
            // Error Messages
            { "Resource not found", "Ресурсът не е намерен" },
            { "Unauthorized access", "Неоторизиран достъп" },
            { "Forbidden access", "Забранен достъп" },
            { "Invalid request", "Невалидна заявка" },
            {
                "An error occurred while processing your request",
                "Възникна грешка при обработката на заявката"
            },
            { "Access denied", "Достъпът е отказан" },
            { "Session expired", "Сесията е изтекла" },
            { "Invalid credentials", "Невалидни данни за вход" },
            { "Operation failed", "Операцията не беше успешна" },
            // Supplier translations
            { "Suppliers", "Доставчици" },
            { "Manage Suppliers", "Управление на доставчици" },
            { "Add New Supplier", "Добави нов доставчик" },
            { "Search...", "Търсене..." },
            { "All Status", "Всички статуси" },
            { "Search", "Търси" },
            { "Phone", "Телефон" },
            { "Address", "Адрес" },
            { "Previous", "Предишна" },
            { "Next", "Следваща" },
            { "Confirm Delete", "Потвърди изтриване" },
            {
                "Are you sure you want to delete this supplier?",
                "Сигурни ли сте, че искате да изтриете този доставчик?"
            },
            { "per page", "на страница" },
            // Supplier form translations
            { "Create Supplier", "Създаване на доставчик" },
            { "Edit Supplier", "Редактиране на доставчик" },
            { "Supplier Details", "Детайли за доставчика" },
            { "Back to List", "Обратно към списъка" },
            // Success messages
            { "Supplier created successfully", "Доставчикът е създаден успешно" },
            { "Supplier updated successfully", "Доставчикът е обновен успешно" },
            { "Supplier deleted successfully", "Доставчикът е изтрит успешно" },
            // Error messages
            { "Failed to load suppliers", "Грешка при зареждане на доставчиците" },
            { "Supplier not found", "Доставчикът не е намерен" },
            { "Failed to create supplier", "Грешка при създаване на доставчика" },
            { "Failed to update supplier", "Грешка при обновяване на доставчика" },
            { "Failed to delete supplier", "Грешка при изтриване на доставчика" },
            { "Invalid supplier ID", "Невалиден ID на доставчик" },
            // Validation messages
            { "Name is required", "Името е задължително" },
            {
                "Name cannot be longer than 100 characters",
                "Името не може да бъде по-дълго от 100 символа"
            },
            { "Email is required", "Имейлът е задължителен" },
            { "Invalid email address", "Невалиден имейл адрес" },
            { "Phone number is required", "Телефонният номер е задължителен" },
            { "Invalid phone number", "Невалиден телефонен номер" },
            { "Address is required", "Адресът е задължителен" },
            { "No description available", "Няма налично описание" },
            // Permission translations
            // CustomersController
            { "Customers.GetAll", "Клиенти - Всички" },
            { "Customers.GetById", "Клиенти - По ID" },
            { "Customers.Add", "Клиенти - Добави" },
            { "Customers.Update", "Клиенти - Обнови" },
            { "Customers.Delete", "Клиенти - Изтрий" },
            // ProductsController
            { "Products.GetAll", "Продукти - Всички" },
            { "Products.GetById", "Продукти - По ID" },
            { "Products.Add", "Продукти - Добави" },
            { "Products.Update", "Продукти - Обнови" },
            { "Products.Delete", "Продукти - Изтрий" },
            // OrdersController
            { "Orders.GetAll", "Поръчки - Всички" },
            { "Orders.GetById", "Поръчки - По ID" },
            { "Orders.Add", "Поръчки - Добави" },
            { "Orders.Complete", "Поръчки - Завърши" },
            { "Orders.Cancel", "Поръчки - Откажи" },
            { "Orders.Delete", "Поръчки - Изтрий" },
            // DeliveriesController
            { "Deliveries.GetAll", "Доставки - Всички" },
            { "Deliveries.GetById", "Доставки - По ID" },
            { "Deliveries.Add", "Доставки - Добави" },
            { "Deliveries.StartProgress", "Доставки - Стартирай прогрес" },
            { "Deliveries.Complete", "Доставки - Завърши" },
            { "Deliveries.Delete", "Доставки - Изтрий" },
            // InvoicesController
            { "Invoices.GetAll", "Фактури - Всички" },
            { "Invoices.GetById", "Фактури - По ID" },
            { "Invoices.CreateFromOrder", "Фактури - Създай от поръчка" },
            { "Invoices.UpdateStatus", "Фактури - Обнови статус" },
            { "Invoices.GetInvoicePdf", "Фактури - PDF" },
            // PaymentMethodsController
            { "PaymentMethods.GetAll", "Методи на плащане - Всички" },
            { "PaymentMethods.GetDropdownList", "Методи на плащане - Dropdown списък" },
            { "PaymentMethods.GetById", "Методи на плащане - По ID" },
            { "PaymentMethods.Add", "Методи на плащане - Добави" },
            { "PaymentMethods.Update", "Методи на плащане - Обнови" },
            { "PaymentMethods.Delete", "Методи на плащане - Изтрий" },
            // PaymentsController
            { "Payments.GetAll", "Плащания - Всички" },
            { "Payments.GetById", "Плащания - По ID" },
            { "Payments.RecordPayment", "Плащания - Запиши плащане" },
            // SuppliersController
            { "Suppliers.GetAll", "Доставчици - Всички" },
            { "Suppliers.GetById", "Доставчици - По ID" },
            { "Suppliers.Add", "Доставчици - Добави" },
            { "Suppliers.Update", "Доставчици - Обнови" },
            { "Suppliers.Delete", "Доставчици - Изтрий" },
            // UnitsOfMeasureController
            { "UnitsOfMeasure.GetAll", "Мерни единици - Всички" },
            { "UnitsOfMeasure.GetById", "Мерни единици - По ID" },
            { "UnitsOfMeasure.Add", "Мерни единици - Добави" },
            { "UnitsOfMeasure.Update", "Мерни единици - Обнови" },
            { "UnitsOfMeasure.Delete", "Мерни единици - Изтрий" },
            // AuthenticationController
            { "Authentication.Login", "Автентикация - Вход" },
            { "Authentication.Register", "Автентикация - Регистрация" },
            { "Authentication.GetUsers", "Автентикация - Потребители" },
            { "Authentication.GetRoles", "Автентикация - Роли" },
            { "Authentication.GetRoleById", "Автентикация - Роля по ID" },
            { "Authentication.GetEndpoints", "Автентикация - Ендпойнти" },
            { "Authentication.CreateRole", "Автентикация - Създай роля" },
            { "Authentication.EditRole", "Автентикация - Редактирай роля" },
            { "Authentication.DeleteRole", "Автентикация - Изтрий роля" },
            { "Authentication.AssignRole", "Автентикация - Присвои роля" },
            { "Authentication.RemoveRole", "Автентикация - Премахни роля" },
        };
}
