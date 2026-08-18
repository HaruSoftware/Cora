using Cora.Data;
using Cora.Entities;
using Cora.UI;
using System;
using System.Windows;

namespace Cora.Windows.Menu
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            Serializer.CreateDirectories();
            DataAccess.InitializeAll();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Autenticar usuário

            var user = DataAccess.Get("Users").GetData("Users", new DBFilter(" AND Username = @username", username)) as User;

            if(user == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(user.Password == password)
            {
                if (InstanceManager.Menu == null)
                {
                    InstanceManager.Menu = new MainMenu();
                    InstanceManager.Menu.Show();
                }

                Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void LinkCadastro_Click(object sender, RoutedEventArgs e)
        {
            Cadastro cadastroWindow = new Cadastro();
            cadastroWindow.ShowDialog();
        }
    }
}
