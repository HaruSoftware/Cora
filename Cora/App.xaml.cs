using Cora.UI;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Cora
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void GlobalKeyDownHandler(object sender, KeyEventArgs e)
        {
            // Verifica se a tecla pressionada é o botão Esc
            if (e.Key == Key.Escape)
            {
                var menu = MainMenu.Get();

                if (menu != null)
                {
                    menu.SetWindow(menu.LastSelectedPage);
                }
            }
        }
    }
    public class ColorDarkerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                // Calcula a cor mais escura
                var color = brush.Color;
                byte factor = 30; // Ajuste esse valor para o quão escura você quer a cor
                return new SolidColorBrush(Color.FromRgb(
                    (byte)Math.Max(0, color.R - factor),
                    (byte)Math.Max(0, color.G - factor),
                    (byte)Math.Max(0, color.B - factor)));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
