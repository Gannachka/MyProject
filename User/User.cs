using MyProject.Autentification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        private string _docName;

        public string DocName
        {
            get
            {
                return _docName;
            }
            set
            {
                _docName = value;
                OnPropertyChanged("DocName");
            }
        }
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
        private int _room;
        public int Room
        {
            get
            {
                return _room;
            }
            set
            {
                _room = value;

                OnPropertyChanged("Room");
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
        private DateTime _dateVisit;
        public DateTime DateVisit
        {
            get
            {
                return _dateVisit;
            }
            set
            {
                _dateVisit = value;
                OnPropertyChanged("DateVisit");
            }
        }
        private TimeSpan _timeVisit;
        public TimeSpan TimeVisit
        {
            get
            {
                return _timeVisit;
            }
            set
            {
                _timeVisit = value;
                OnPropertyChanged("TimeVisit");
            }
        }
        private string _treatmrnt;
        public string Treatment
        {
            get
            {
                return _treatmrnt;
            }
            set
            {
                _treatmrnt = value;
                OnPropertyChanged("Treatment");

            }
        }
        private string _diagnose;
        public string Diagnose
        {
            get
            {
                return _diagnose;
            }
            set
            {
                _diagnose = value;
                OnPropertyChanged("Diagnose");

            }
        }
       
        public User(int id)
        {
            this.id = id;
            CreateNewUser(id);
           
        }
        public User() { }
     
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
                        Email = reader.GetString(6);
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
        public override string ToString()
        {
            return "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
