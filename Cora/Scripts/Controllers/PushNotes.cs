using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Cora.UI
{
    public static class PushNotes
    {
        public static void Show(string message)
        {
            _show(message, 0);
        }
        public static void ShowError(string message)
        {
            _show(message, 1);
        }
        private static void _show(string note, int type)
        {
            MainMenu menu = MainMenu.Get();
            
            if(menu == null)
            {
                return;
            }

            var border = menu.NoteBorder;
            var text = menu.NoteText;

            switch (type)
            {
                case 1:
                    border.Background = (Brush)new BrushConverter().ConvertFrom("#ff7675");
                    break;
                default:
                    border.Background = (Brush)new BrushConverter().ConvertFrom("#55ef90");
                    break;
            }

            text.Text = note;

            border.Visibility = Visibility.Visible;
            border.Opacity = 1;

            border.BeginAnimation(UIElement.OpacityProperty, null);

            if (border.GetAnimationBaseValue(UIElement.OpacityProperty) != null)
            {
                // Há uma animação de Opacidade em andamento
            }

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,  // Começa com opacidade 0
                To = 1,    // Vai até opacidade 1
                Duration = TimeSpan.FromSeconds(0.5)
            };
            border.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,  // Começa com opacidade 1
                To = 0,    // Vai até opacidade 0
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(3) // Depois de 2 segundos
            };

            fadeOutAnimation.Completed += (s, e) =>
            {
                border.Visibility = Visibility.Collapsed; // Esconde após a animação
            };

            border.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }
    }
}
