using System.Windows;
using Cora.Models;
using Cora.Services;

namespace Cora.Windows.Menu
{
    public partial class Cadastro : Window
    {
        private UserRepository _userRepository;

        public Cadastro()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
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

            if (_userRepository.UserExists(username))
            {
                MessageBox.Show("Este nome de usuário já está em uso.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = password
            };

            _userRepository.CreateUser(newUser);

            MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
