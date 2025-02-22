using System.ComponentModel.DataAnnotations;

namespace Encryption___decryption_APP.DTOs
{
    public class PlayfairRequest
    {
        [Required]
        public string PlainText { get; set; }
        [Required]
        public string Key { get; set; }
    }
}
