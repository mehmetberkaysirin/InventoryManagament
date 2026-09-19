namespace InventoryManagement.Web.Models.Account;

public class AssignRoleViewModel
{
    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public List<RoleAssignItem> Roles { get; set; } = new();
}

public class RoleAssignItem
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsExist { get; set; }
}