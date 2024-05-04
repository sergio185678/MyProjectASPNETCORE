using System.Security.Cryptography;
using System.Text;

namespace MyProjectAPINETCore.Utils
{
    public class Encriptar
    {
        public static string EncriptarPasword(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sbuiler = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sbuiler.Append(data[i].ToString("x2"));
            }
            return sbuiler.ToString();
        }
    }
}
