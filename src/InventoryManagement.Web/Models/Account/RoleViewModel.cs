using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Web.Models.Account;

public class RoleViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Rol adı zorunludur.")]
    [Display(Name = "Rol Adı")]
    public string Name { get; set; } = string.Empty;
}