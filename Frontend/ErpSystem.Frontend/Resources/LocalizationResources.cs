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
            { "Invalid request", "Невалидна заявка" },
            {
                "An error occurred while processing your request",
                "Възникна грешка при обработката на заявката"
            },
            { "Access denied", "Достъпът е отказан" },
            { "Session expired", "Сесията е изтекла" },
            { "Invalid credentials", "Невалидни данни за вход" },
            { "Operation failed", "Операцията не беше успешна" },
        };
}
