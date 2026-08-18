using Cora.Enums;
using Cora.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cora.UI.Windows;
using Cora.Data;

namespace Cora.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public bool IsMenuCollapsed { get; set; }

        public static MainMenu Instance { get; private set; }

        public UserList Userlist { get; set; }

        public int LastSelectedPage { get; set; }
        public int CurrentSelectedPage { get; set; }

        public MainMenu()
        {
            InitializeComponent();
            Instance = this;
        }
        public static MainMenu Get()
        {
            return Instance;
        }

        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var culture = new CultureInfo("pt-BR");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CurrentSelectedPage = -1;

            SetWindow((int)MenuWindow.HOME);
        }
        public void SetWindow(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;

            int index = int.Parse(button.Uid);

            SetWindow(index);
        }
        public void UpdateLoginState(object sender, RoutedEventArgs e)
        {
            PushNotes.ShowError("Identificado");

            if (InstanceManager.ConnectedUser == null)
            {
                SetWindow(15);
                return;
            }

            SetLoginState(null);
            SetWindow((int)MenuWindow.HOME);

        }
        public void SetLoginState(User user)
        {
            var loggedColor = Brushes.LimeGreen;
            var disconnectColor = Brushes.Red;

            InstanceManager.ConnectedUser = user;

            if (user == null)
            {
                UserLogged.Stroke = disconnectColor;
                CurrentUserName.Text = "Desconectado";
                return;
            }

            UserLogged.Stroke = loggedColor;
            CurrentUserName.Text = user.FullName;
        }
        public void SetWindow(int window, object instance = null)
        {
            if (window == (int)MenuWindow.NONE) return;

            switch (window)
            {
                case 0:
                    break;
                case (int)MenuWindow.WINDOW_USERLOGIN:
                    Userlist = new UserList();
                    Userlist.ShowDialog();
                    break;
            }
            CurrentSelectedPage = window;
        }

        public void SelectWindowMarker(int window)
        {
            if (window == 0)
            {
                CurrentPageMarker.Visibility = Visibility.Hidden;
            }
            else
            {
                CurrentPageMarker.Visibility = Visibility.Visible;
            }

            if (window < 1) return;
            if (window > 12) return;

            Grid.SetRow(CurrentPageMarker, window);
        }

        public void SetVerticalMenu(object sender, RoutedEventArgs e)
        {
            IsMenuCollapsed = !IsMenuCollapsed;

            if (IsMenuCollapsed)
            {
                Storyboard expandStoryboard = (Storyboard)FindResource("MenuExpandAnimation");
                expandStoryboard.Begin(MenuGrid);
                FirstButton.Width = 190;
            }
            else
            {
                Storyboard collapseStoryboard = (Storyboard)FindResource("MenuCollapseAnimation");
                collapseStoryboard.Begin(MenuGrid);
                FirstButton.Width = 35;
            }
        }

        public object GetCurrentWindowObject()
        {
            return defaultFrame.Content;
        }
    }
}
