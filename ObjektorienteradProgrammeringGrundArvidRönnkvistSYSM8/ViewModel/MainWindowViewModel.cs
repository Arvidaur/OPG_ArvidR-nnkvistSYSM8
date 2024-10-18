using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand RegisterNewCommand { get; }

        public MainWindowViewModel()
        {
            // Initialize commands and bind them to methods
            LoginCommand = new RelayCommand(Login);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            RegisterNewCommand = new RelayCommand(Register);
        }

        private void Login(object parameter)
        {
            // Replace with your user validation logic
            if (Username == "BigArvid69" && Password == "muscles")
            {
                // Login successful logic
                System.Windows.MessageBox.Show("Login successful!");

            }
            else
            {
                // Login failure logic
                System.Windows.MessageBox.Show("Invalid username or password.");
            }
        }

        private void ForgotPassword(object parameter)
        {
            System.Windows.MessageBox.Show("Forgot password functionality is not implemented yet.");
        }

        private void Register(object parameter)
        {
            
            var userDetailWindow = new UserDetailsWindow(); //Creating an instance of UserDetailWindow
            userDetailWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
