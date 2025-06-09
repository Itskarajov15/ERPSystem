namespace ErpSystem.Application.Common.Constants;

public static class RoleErrorKeys
{
    public const string RoleNotFound = "RoleNotFound";
    public const string RoleAlreadyExists = "RoleAlreadyExists";
    public const string RoleCreationFailed = "RoleCreationFailed";
    public const string RoleUpdateFailed = "RoleUpdateFailed";
    public const string RoleDeleteFailed = "RoleDeleteFailed";
    public const string RoleNameRequired = "RoleNameRequired";
    public const string RoleNameTooShort = "RoleNameTooShort";
    public const string RoleNameTooLong = "RoleNameTooLong";
    public const string RoleNameInvalidCharacters = "RoleNameInvalidCharacters";
    public const string RoleDescriptionTooLong = "RoleDescriptionTooLong";
    public const string RoleDataRequired = "RoleDataRequired";
    public const string RoleIdRequired = "RoleIdRequired";
    public const string PermissionIdsRequired = "PermissionIdsRequired";
    public const string UserNotFound = "UserNotFound";
    public const string UserCreationFailed = "UserCreationFailed";
    public const string UserDeleteFailed = "UserDeleteFailed";
    public const string FailedToRemoveRoleFromUser = "FailedToRemoveRoleFromUser";
    public const string FailedToAddRoleToUser = "FailedToAddRoleToUser";
    public const string RoleWithNameNotExists = "RoleWithNameNotExists";
    public const string RoleInUseCannotDelete = "RoleInUseCannotDelete";
}
