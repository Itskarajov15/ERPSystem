namespace ErpSystem.Frontend.Core.Constants;

public static class RoleErrorKeys
{
    private static readonly Dictionary<string, string> _translations =
        new()
        {
            ["RoleNotFound"] = "Ролята не е намерена",
            ["RoleAlreadyExists"] = "Роля с това име вече съществува",
            ["RoleCreationFailed"] = "Създаването на ролята неуспешно",
            ["RoleUpdateFailed"] = "Обновяването на ролята неуспешно",
            ["RoleDeleteFailed"] = "Изтриването на ролята неуспешно",
            ["RoleNameRequired"] = "Името на ролята е задължително",
            ["RoleNameTooShort"] = "Името на ролята трябва да бъде поне 3 символа",
            ["RoleNameTooLong"] = "Името на ролята не може да бъде по-дълго от 50 символа",
            ["RoleNameInvalidCharacters"] =
                "Името на ролята може да съдържа само букви, цифри, долни черти и тирета",
            ["RoleDescriptionTooLong"] = "Описанието не може да бъде по-дълго от 500 символа",
            ["RoleDataRequired"] = "Данните за ролята са задължителни",
            ["RoleIdRequired"] = "ID на ролята е задължително",
            ["UserNotFound"] = "Потребителят не е намерен",
            ["UserCreationFailed"] = "Създаването на потребителя неуспешно",
            ["UserDeleteFailed"] = "Изтриването на потребителя неуспешно",
            ["FailedToRemoveRoleFromUser"] = "Неуспешно премахване на роля от потребител",
            ["FailedToAddRoleToUser"] = "Неуспешно добавяне на роля към потребител",
            ["RoleWithNameNotExists"] = "Роля с това име не съществува",
            ["RoleInUseCannotDelete"] =
                "Не може да се изтрие роля, която се използва от потребители",
        };

    public static string Translate(string errorKey)
    {
        return _translations.TryGetValue(errorKey, out var translation) ? translation : errorKey;
    }
}
