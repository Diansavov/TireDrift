using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TireDrift.Models.ViewModels
{
    public class EmployeeRegister
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Потребителското име е задължително")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Имейла е задължителен")]
        [EmailAddress(ErrorMessage ="Имейлът трябва да е валиден")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Паролата е задължителна")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Личното име е задължително")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Фамилното име е задължително")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Телефонният номер е задължителен")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Ролята е задължителна")]
        public string Role { get; set; }
    }
}