using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Encryption___decryption_APP.DTOs
{
    public class CaesarRequest
    {
        [Required]
        public string PlainText { get; set; }
        
        [Required]
        public string Shift { get; set; }
    }
}
