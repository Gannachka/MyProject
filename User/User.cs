using MyProject.Autentification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyProject.User
{
    public class User : INotifyPropertyChanged
    {
        public int id { get; private set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
               
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
                _sername = value;
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
                 _email = value;
                OnPropertyChanged("Email");
            }
        }
        public User(int id)
        {
            this.id = id;
            CreateNewUser(id);
        }
        private void CreateNewUser(int id)
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"select * from PACIENT WHERE PACIENTID={id}";
                SqlCommand command = new SqlCommand(select, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Name = reader.GetString(3);
                        Surname = reader.GetString(4);
                        Email = reader.GetString(5);
                        if (reader.GetString(7) == "")
                        {
                            Image = new BitmapImage(new Uri(@"file:///D:/4 семестр/ООП/Курсовой проект/MyProject/Images/user.png"));
                        }
                        else
                            Image = new BitmapImage(new Uri(reader.GetString(7)));
                 
                        
                    }

                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
