using System.Text;

namespace Encryption___decryption_APP.Services
{
    public class CipherService
    {
        internal string CaesarCipher(string text, int shift)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)(((c + shift - offset) % 26 + offset)));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        internal string PlayfairCipher(string text, string key, bool encrypt)
        {

            string formattedText = text.Replace("J", "I").ToUpper();
            string keySquare = GenerateKeySquare(key.ToUpper());

            return encrypt ? EncryptPlayfair(formattedText, keySquare) : DecryptPlayfair(formattedText, keySquare);
        }

        internal string GenerateKeySquare(string key)
        {
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";  // Without 'J'
            StringBuilder keySquare = new StringBuilder();

            foreach (char c in key + alphabet)
            {
                if (!keySquare.ToString().Contains(c))
                    keySquare.Append(c);
            }

            return keySquare.ToString();
        }

        internal string EncryptPlayfair(string text, string keySquare)
        {
            return ProcessPlayfair(text, keySquare, true);
        }

        internal string DecryptPlayfair(string text, string keySquare)
        {
            return ProcessPlayfair(text, keySquare, false);
        }

        internal string ProcessPlayfair(string text, string keySquare, bool encrypt)
        {
            text = text.ToUpper().Replace("J", "I");  // Standard Playfair formatting
            text = FormatPlayfairText(text);  // Ensures even-length text
            char[,] matrix = CreateKeyMatrix(keySquare);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i], b = text[i + 1];
                (int rowA, int colA) = FindPosition(matrix, a);
                (int rowB, int colB) = FindPosition(matrix, b);

                if (rowA == rowB)  // Same row: shift right (encrypt) or left (decrypt)
                {
                    colA = (colA + (encrypt ? 1 : -1) + 5) % 5;
                    colB = (colB + (encrypt ? 1 : -1) + 5) % 5;
                }
                else if (colA == colB)  // Same column: shift down (encrypt) or up (decrypt)
                {
                    rowA = (rowA + (encrypt ? 1 : -1) + 5) % 5;
                    rowB = (rowB + (encrypt ? 1 : -1) + 5) % 5;
                }
                else  // Rectangle swap
                {
                    (colA, colB) = (colB, colA);
                }
                result.Append(matrix[rowA, colA]).Append(matrix[rowB, colB]);
            }

            return result.ToString();
        }

        internal string FormatPlayfairText(string text)
        {
            StringBuilder formattedText = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                formattedText.Append(text[i]);
                if (i < text.Length - 1 && text[i] == text[i + 1])
                {
                    formattedText.Append('X');  // Replace repeated letters with 'X'
                }
            }
            if (formattedText.Length % 2 != 0)
            {
                formattedText.Append('X');  // Make even length
            }
            return formattedText.ToString();
        }

        internal char[,] CreateKeyMatrix(string key)
        {
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";  // No 'J' in Playfair
            StringBuilder keyBuilder = new StringBuilder();
            foreach (char c in key + alphabet)
            {
                if (!keyBuilder.ToString().Contains(c))
                    keyBuilder.Append(c);
            }

            char[,] matrix = new char[5, 5];
            for (int i = 0, index = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = keyBuilder[index++];
                }
            }
            return matrix;
        }

        internal (int, int) FindPosition(char[,] matrix, char letter)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == letter)
                        return (i, j);
                }
            }
            return (-1, -1);  // Should never happen
        }

        internal string MonoalphabeticCipher(string text, bool encrypt)
        {
            const string originalAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string substitutionAlphabet = "QWERTYUIOPLKJHGFDSAZXCVBNM"; // Example shuffled alphabet

            text = text.ToUpper();
            StringBuilder result = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    int index = encrypt
                        ? originalAlphabet.IndexOf(c)  // Find position in original alphabet
                        : substitutionAlphabet.IndexOf(c);  // Find position in shuffled alphabet

                    char mappedChar = encrypt
                        ? substitutionAlphabet[index]  // Replace with shuffled letter
                        : originalAlphabet[index];  // Replace with original letter

                    result.Append(mappedChar);
                }
                else
                {
                    result.Append(c);  // Keep non-alphabet characters unchanged
                }
            }
            return result.ToString();
        }
    }
}
