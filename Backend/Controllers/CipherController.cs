using Microsoft.AspNetCore.Mvc;
using System.Text;
using Encryption___decryption_APP.DTOs;
using Encryption___decryption_APP.Services;

namespace CipherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CipherController : ControllerBase
    {
        private readonly CipherService _cipherService;

        public CipherController(CipherService cipherService)
        {
            _cipherService = cipherService;
        }

        [HttpPost("caesar/{operation}")]
        public IActionResult CaesarEncrypt([FromBody] CaesarRequest request, string operation)
        {
            if (string.IsNullOrEmpty(request.PlainText))
                return BadRequest(new { Response = "Plain text is empty." });
            
            if (!int.TryParse(request.Shift, out int shift)) 
                return BadRequest(new { Response = "Shift should be a number." });

            if (operation != "encrypt" && operation != "decrypt")
                return BadRequest(new { Response = $"Invalid Operation {operation}" });
            
            if (operation == "decrypt") shift *= -1;

            string encryptedText = _cipherService.CaesarCipher(request.PlainText, shift);

            return Ok(new { Response = encryptedText });
        }

        [HttpPost("playfair/{operation}")]
        public IActionResult PlayfairEncrypt([FromBody] PlayfairRequest request, string operation)
        {
            if (string.IsNullOrEmpty(request.PlainText)) 
                return BadRequest(new { Response = "Plain text is empty." });
            
            if (string.IsNullOrEmpty(request.Key)) 
                return BadRequest(new { Response = "Key is empty." });

            if (operation != "encrypt" && operation != "decrypt")
                return BadRequest(new { Response = $"Invalid Operation {operation}" });

            string encryptedText = _cipherService.PlayfairCipher(request.PlainText, request.Key, operation == "encrypt");
            
            return Ok(new { Response = encryptedText });
        }

        [HttpPost("monoalphabetic/{operation}")]
        public IActionResult MonoalphabeticEncrypt([FromBody] MonoalphabeticRequest request, string operation)
        {
            if (string.IsNullOrEmpty(request.PlainText))
                return BadRequest(new { Response = "Plain text is empty." });

            if (operation != "encrypt" && operation != "decrypt")
                return BadRequest(new { Response = $"Invalid Operation {operation}" });


            string encryptedText = _cipherService.MonoalphabeticCipher(request.PlainText, operation == "encrypt");
            return Ok(new { Response = encryptedText });
        }
    }
}