using System.Windows;
using Cora.Data;
using Cora.Entities;

namespace Cora.Windows.Menu
{
    public partial class Cadastro : Window
    {

        public Cadastro()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("As senhas não coincidem.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var userExists = DataAccess.Get("Users").GetDataList("Users").OfType<User>().Any(u => u.Username.ToLower() == username.ToLower());

            if (userExists == true)
            {
                MessageBox.Show("Este nome de usuário já está em uso.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new User
            {
                InstanceId = Generics.GenerateID(15),
                Username = username,
                Password = password,
                FullName = username,
                Permissions = [],
                UserRole = 1
            };

            DataAccess.Get("Users").Write("Users", newUser);
            MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
