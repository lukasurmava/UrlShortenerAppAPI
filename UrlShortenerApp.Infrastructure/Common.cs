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

        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
