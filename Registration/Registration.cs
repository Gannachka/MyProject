using Microsoft.Build.Framework;
using MyProject.baseofMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media.Imaging;
using Xunit;
using Xunit.Abstractions;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MyProject.Registration
{
    public enum Gender
    {
        Man,
        Woman
    }
    class Registration:INotifyPropertyChanged
    {
        public delegate void End();
        public event End Execute;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                 _name=value;
                OnPropertyChanged("Name");
            }
        }
        private string _sername;
        public string Surname
        {
            get
            {
                return _sername;
            }
            set
            {
                _sername=value;
                OnPropertyChanged("Surname");
            }
        }
        public BitmapImage Image { get; set; }

        private string _email;
        public string Email
        {

            get
            {
                return _email;
                
            }
            set
            {
                if (isValid(value))
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
                else
                    MessageBox.Show("Неверно введённый email");
            }
        }
        private static bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);

            return isMatch.Success;
        }
        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login=value;
                OnPropertyChanged("Login");
            }
        }
        [Required(ErrorMessage = "Пароль должен состоять из букв латинского алфавита и цифр")]

        private string _password;
       
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if(Regex.IsMatch(value, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$") && value.Length>7)
                {
                    _password = GetHash(value);
                    OnPropertyChanged("Password");
                } 
            }
        }
        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Convert.FromBase64String(input));

            return Convert.ToBase64String(hash);
        }
        [Required(ErrorMessage = "Введите день рождения")]

        private DateTime _bday;
         public DateTime Bday
        {
            get
            {
                return _bday;
            }
            set
            {
                 _bday=value;
                OnPropertyChanged("Bday");
            }
        }
        [Required(ErrorMessage = "Укажите пол")]

        private Gender _gender;
         public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                 _gender=value;
                OnPropertyChanged("Gender");
            }
        }
        [Required(ErrorMessage = "пароли не совпадают")]
        
        private string _passwordConfirm;
        [Compare("Password")]
        public string PasswordConfirm
        {
            get
            {
                return _passwordConfirm;
            }
            set
            {
                if (value.Length > 7)
                {
                    _passwordConfirm = GetHash(value);
                    OnPropertyChanged("PasswordConfirm");
                }
            }
        }
        private RelayCommand _register;
        public RelayCommand Register
        {
            get
            {
                return _register ??
                    (_register = new RelayCommand(obj =>
                    {
                        if (Name != null && Surname != null && Password != null && PasswordConfirm != null && Email != null && Bday != null && PasswordConfirm.Equals(Password))
                        {
                            SqlConnection connection = new SqlConnection(Properties.Settings.Default.Connection);
                            try
                            {
                                connection.Open();
                            }
                             
                            finally
                            {
                                using (SqlTransaction transaction = connection.BeginTransaction())
                                {
                                    SqlCommand command = connection.CreateCommand();
                                    command.Transaction = transaction;
                                    try
                                    {
                                        string insert0 = $"insert into PACIENT VALUES( 1, '{Bday}', '{Name}' , '{Surname}' , '{Gender.ToString()}' , '{Email}', '{Image}')";
                                        string insert1 = $"SELECT PACIENT.PACIENTID FROM PACIENT WHERE PACIENT.BIRTHDAY = '{Bday}' AND PACIENT.NAME='{Name}' " +
                                        $"AND PACIENT.GENDER='{Gender}' AND PACIENT.EMAIL='{Email}' AND PACIENT.SURNAME='{Surname}'"; ;
                                        string insert = $"insert into AUTINTIFICATION (PASSWORD, LOGIN, IDPACIENT) values ( '{Password}','{Email}', @ID)";
                                        command.CommandText = insert0;
                                        command.ExecuteNonQuery();
                                        command.CommandText = insert1;
                                        using (SqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                command.CommandText = insert;
                                                SqlParameter parameter = new SqlParameter("@ID", reader.GetInt32(0));
                                                command.Parameters.Add(parameter);
                                                reader.Close();
                                            }
                                        }
                                        command.ExecuteNonQuery();
                                        Execute.Invoke();
                                        transaction.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                    }
                                    connection.Close();

                                }
                            }

                        }
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
