using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Doctor
{
    class Doctor : INotifyPropertyChanged
    {
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
       
        private string _specialization;
        public string Specialization
        {
            get
            {
                return _specialization;
            }
            set
            {
                _specialization = value;
                OnPropertyChanged("Specialization");
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
        public Doctor(int id)
        {
            CreateNewDoctor(id);
        }
        private void CreateNewDoctor(int id)
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"select * from DOCTOR WHERE DOCTORID={id}";
                SqlCommand command = new SqlCommand(select, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Name= reader.GetString(2);
                     
                        Specialization = reader.GetString(3);
                        Room = reader.GetInt32(5);


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
