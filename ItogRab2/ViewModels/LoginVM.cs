using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ItogRab2.Commands;
using System.ComponentModel;
using ItogRab2.Model;
using ItogRab2.View.Windows;

namespace ItogRab2.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        public ICommand CloseWindowCommand { get; }
        public event EventHandler CloseRequested;

        private User _tbData = new User(); 
        public User User
        {
            get { return _tbData; }
            set { SetProperty(ref _tbData, value); }
        }

        public ICommand AuthenticationCommand { get; }

        public LoginVM()
        {
            AuthenticationCommand = new RelayCommand(ExecuteAuthentication, CanExecuteAuthentication);
        }

        private bool CanExecuteAuthentication(object parameter)
        {

            return User != null && User.IsValid;
        }

        private void ExecuteAuthentication(object parameter)
        {
            if (User == null || string.IsNullOrEmpty(User.InputText))
            {
                MessageBox.Show("Ошибка ввода данных", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User.Validate();
                if (!User.IsValid)
                {
                    MessageBox.Show("Некорректные данные", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (User.InputText == "01")
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("В доступе отказано", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                User.InputText = string.Empty;
                User.IsValid = false;
                OnPropertyChanged(nameof(User));  
            }
        }
        private void CloseWindow()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}



