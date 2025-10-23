using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Infrastructure
{
    public class Common
    {
        public static string GenerateShortCode()
        {
            string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string code = "";
            var length = new Random().Next(6, 9);
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, alphabet.Length);
                code += alphabet[index];
            }

            return code;
        }
    }
}
