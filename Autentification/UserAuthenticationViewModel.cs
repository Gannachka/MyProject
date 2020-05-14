using MyProject.baseofMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace MyProject.Autentification
{
    class UserAuthenticationViewModel : INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private UserAuthetification _selectedUserAuthentication;
        public UserAuthetification SelectedUserAuthentication
        {
            get
            {
                return _selectedUserAuthentication;
            }
            set
            {
                _selectedUserAuthentication = value;
                OnPropertyChanged("SelectedUserAuthentication");
            }
        }
        private RelayCommand _enter;
        public RelayCommand Enter
        {
            get
            {
                return _enter ??
                    (_enter = new RelayCommand(obj =>
                    {
                        UserAuthetification user = obj as UserAuthetification;
                        user.Password = _mainWindow.AuthenticationPassBox.Password;
                        SqlConnection connection = new SqlConnection(Properties.Settings.Default.Connection);
                        try
                        {
                            connection.Open();

                        }
                        finally
                        {
                            string select = $" select IDDOCTOR, IDPACIENT, IDADMIN FROM AUTINTIFICATION WHERE LOGIN='{user.Login}' AND PASSWORD='{user.Password}'";
                            SqlCommand sqlCommand = new SqlCommand(select, connection);
                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (!reader.IsDBNull(0))
                                    {
                                        DoctorWindow doctorWindow = new DoctorWindow(_mainWindow);
                                        _mainWindow.Hide();
                                        doctorWindow.Show();
                                    }
                                    else if (!reader.IsDBNull(1))
                                    {
                                        UserWindow userWindow = new UserWindow(_mainWindow);
                                        _mainWindow.Hide();
                                        userWindow.Show();

                                    }
                                    else if (!reader.IsDBNull(2))
                                    {
                                        AdminWindow adminWindow = new AdminWindow(_mainWindow);
                                        _mainWindow.Hide();
                                        adminWindow.Show();
                                    }
                                    else
                                        MessageBox.Show("Введён неправильный пароль");
                                }
                            }
                        }
                    }));
            }
        }
        private RelayCommand _registration;
        public RelayCommand Registration
        {
            get
            {
                return _registration ??
                    (_registration = new RelayCommand(obj =>
                    {
                        RegistrationWindow registrationWindow = new RegistrationWindow(_mainWindow);
                        registrationWindow.Show();
                    }));
            }
        }
        public UserAuthenticationViewModel(MainWindow mainWindow)
        {
            SelectedUserAuthentication = new UserAuthetification();
            _mainWindow = mainWindow;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
