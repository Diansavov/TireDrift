using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TireDrift.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Паролата е задължителна")]
        public string Password { get; set; }

    }
}