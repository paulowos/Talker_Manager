using System.Security.Cryptography;
using System.Text;

namespace TalkerManager.Model;

public class Token
{
    public Token(string email, string password)
    {
        var sha = SHA256.Create();
        var textBytes = Encoding.UTF8.GetBytes(email + password);
        var hash = sha.ComputeHash(textBytes);
        var token = BitConverter.ToString(hash).Replace("-", string.Empty);

        this.token = token;
    }

    public string token { get; set; }
}