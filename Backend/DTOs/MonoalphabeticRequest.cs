using System.ComponentModel.DataAnnotations;

namespace Encryption___decryption_APP.DTOs
{
    public class MonoalphabeticRequest
    {
        [Required]
        public string PlainText { get; set; }
    }
}
