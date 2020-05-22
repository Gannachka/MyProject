
using MyProject.baseofMVVM;
using MyProject.Registration;
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
using System.Windows.Media.Imaging;

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

                       
                            string select = $" select IDDOCTOR, IDPACIENT, IDADMIN FROM AUTINTIFICATION WHERE LOGIN='{user.Login}' AND PASSWORD='{user.Password}'";
                            SqlCommand sqlCommand = new SqlCommand(select, connection);
                        connection.Open();
                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (!reader.IsDBNull(0))
                                    {
                                        DoctorWindow doctorWindow = new DoctorWindow(_mainWindow, reader.GetInt32(0));
                                        _mainWindow.Hide();
                                        doctorWindow.Show();
                                    }
                                    else if (!reader.IsDBNull(1))
                                    {
                                        UserWindow userWindow = new UserWindow(_mainWindow, reader.GetInt32(1));
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
                        
                    }));
            }
        }
        private RelayCommand _registration;
        private AdminWindow adminWindow;

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

        public UserAuthenticationViewModel(AdminWindow adminWindow)
        {
            this.adminWindow = adminWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
