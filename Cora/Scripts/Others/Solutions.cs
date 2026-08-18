using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Cora
{
    public static class Generics
    {
#nullable enable
        public static long GenerateID(int? amount = null, string? prefix = null)
        {
            string values = "0123456789";
            string finalValue = prefix ?? "";

            Random random = new Random();
            int totalAmount = amount ?? 15;

            int remaining = totalAmount - finalValue.Length;

            if (remaining < 0)
                throw new ArgumentException("Prefix length cannot be greater than the total amount.");

            for (int i = 0; i < remaining; i++)
            {
                finalValue += values[random.Next(1, 10)];
            }
            return long.Parse(finalValue);
        }
#nullable disable
        public static long CalculateMd5(object input)
        {
            // Serializa o objeto para uma string JSON
            string jsonString = JsonConvert.SerializeObject(input);
            var inputBytes = Encoding.UTF8.GetBytes(jsonString);

            using (MD5 md5 = MD5.Create())
            {
                // Calcula o hash MD5
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                long result = 0;
                for (int i = 0; i < 8; i++)
                {
                    result = (result << 8) | (byte)(hashBytes[i] & 0xFF);
                }

                return result;
            }
        }
        public static bool CompareMD5(string firstValue, string secondValue)
        {
            return firstValue == secondValue;
        }
        public static bool CheckPrefix(long number, string prefix)
        {
            string numberStr = number.ToString();
            return numberStr.StartsWith(prefix);
        }
        public static async Task<BitmapImage> DownloadImageAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = await client.GetByteArrayAsync(url);

                    using (var stream = new MemoryStream(data))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        image.Freeze(); // importante para poder usar em UI thread
                        return image;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        public static string SqlNumber(this decimal value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
    public class Parsing
    {
        public static short Short(string text)
        {
            if (!short.TryParse(text, out short result))
            {
                return 0;
            }
            return result;
        }
        public static long Long(string text)
        {
            if (!long.TryParse(text, out long result))
            {
                return 0;
            }
            return result;
        }
        public static decimal Decimal(string text)
        {
            if (!decimal.TryParse(text, out decimal result))
            {
                return 0;
            }
            return result;
        }
        public static double Double(string text)
        {

            if (!double.TryParse(text, out double result))
            {
                return 0;
            }

            return result;
        }
        public static int Int(string text)
        {

            if (!int.TryParse(text, out int result))
            {
                return 0;
            }

            return result;
        }
        public static float Float(string text)
        {
            if (!float.TryParse(text, out float result))
            {
                return 0;
            }

            return result;
        }
        public static bool IsNumber(string Text)
        {
            if (double.TryParse(Text, out double result))
            {
                // Retorna verdadeiro se o número for positivo
                return result >= 0;
            }
            return false; // Retorna falso se a conversão falhar
        }
        public static int BoolToInt(bool value)
        {
            return value == true ? 1 : 0;
        }
    }
}
