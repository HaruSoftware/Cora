using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cora
{
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
