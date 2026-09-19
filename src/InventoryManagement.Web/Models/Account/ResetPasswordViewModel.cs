using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Web.Models.Account // Proje ad alanına göre düzenle
{
    public class AdminResetPasswordViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni şifre alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrarı")]
        [Compare("NewPassword", ErrorMessage = "Girdiğiniz şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}