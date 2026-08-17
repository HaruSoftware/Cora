using System;
using System.Windows;
using Cora.Services;

namespace Cora.Windows.Menu
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserRepository _userRepository;

        public Login()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
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

            var user = _userRepository.AuthenticateUser(username, password);

            if (user != null)
            {
                MessageBox.Show($"Bem-vindo, {user.Username}!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                // Here we navigate to the main application window or next screen
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LinkCadastro_Click(object sender, RoutedEventArgs e)
        {
            Cadastro cadastroWindow = new Cadastro();
            cadastroWindow.ShowDialog();
        }
    }
}
