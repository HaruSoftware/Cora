using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cora.Entities;
using Cora.Data;

namespace Cora.UI.Windows
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        public User CurrentSelectedUser;

        public delegate void UserSelectedHandler(User user);
        public event UserSelectedHandler UserSelected;

        public UserList()
        {
            InitializeComponent();
            Password.Focus();
        }
        public void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var usersInData = DataAccess.Get("Users").GetDataList("Users");

            Users.ItemsSource = usersInData.OfType<User>().OrderBy(x => x.Id);

            if (Users.Items.Count > 0)
            {
                Users.SelectedItem = Users.Items[0];
            }
        }

        public async void CheckPassword(object sender, RoutedEventArgs e)
        {
            if (Users.SelectedItem == null)
            {
                WrongPasswordText.Text = "Selecione um usuário...";
                return;
            }

            CurrentSelectedUser = Users.SelectedItem as User;

            WrongPasswordText.Visibility = Visibility.Hidden;

            var pass = Password.Password;

            if (string.IsNullOrEmpty(pass)) return;

            if (pass != CurrentSelectedUser.Password)
            {
                var secretKey = InstanceManager.CurrentEnterprise.SecretKey;

                if (pass == secretKey)
                {
                    var selected = User.GetAdministrator();

                    MainMenu.Get().SetLoginState(selected);
                    InstanceManager.ConnectedUser = selected;

                    await Task.Delay(15);
                    
                    UserSelected?.Invoke(selected);

                    this.Close();
                }

                WrongPasswordText.Text = "Senha incorreta! Tente novamente.";
                WrongPasswordText.Visibility = Visibility.Visible;
                return;
            }

            //Insert enter user logistic

            MainMenu.Get().SetLoginState(CurrentSelectedUser);
            InstanceManager.ConnectedUser = CurrentSelectedUser;

            await Task.Delay(5);

            UserSelected?.Invoke(CurrentSelectedUser);

            this.Close();
        }

        #region Window

        private void GridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Evite que a tecla Enter cause uma nova linha na edição
                e.Handled = true;

                // Obtenha a célula atual
                DataGridCellInfo cellInfo = Users.CurrentCell;

                Password.Focus();
                Password.SelectAll();

            }

        }
        private void GridMouseLeftDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifique se o botão esquerdo do mouse foi clicado
            if (e.ChangedButton == MouseButton.Left && e.ClickCount >= 2)
            {
                e.Handled = true;

                // Obtenha a célula atual
                DataGridCellInfo cellInfo = Users.CurrentCell;

                Password.Focus();
                Password.SelectAll();
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //e
                CheckPassword(null, null);
            }
        }
    }
}
