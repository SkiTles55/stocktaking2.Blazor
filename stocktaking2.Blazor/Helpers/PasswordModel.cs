using System.ComponentModel.DataAnnotations;

namespace stocktaking2.Blazor.Helpers
{
    public class PasswordModel
    {
        [Required(ErrorMessage ="Введите текущий пароль.")]
        [MinLength(6, ErrorMessage ="Минимальная длина пароля - 6 символов.")]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "Введите новый пароль.")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля - 6 символов.")]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "Повторите новый пароль.")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля - 6 символов.")]
        [Compare(nameof(NewPass), ErrorMessage ="Введенные пароли не совпадают.")]
        public string ConfirmNewPass { get; set; }
    }
}
