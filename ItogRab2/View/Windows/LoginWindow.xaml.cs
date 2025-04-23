using ItogRab2.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ItogRab2.View.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var viewModel = DataContext as LoginVM;
            if (viewModel != null)
            {
                viewModel.CloseRequested += ViewModel_CloseRequested;
            }
        }
        private void ViewModel_CloseRequested(object sender, EventArgs e)
        {
            (this.Parent as Window).Close();
        }

        private void BtnAuthentication_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
