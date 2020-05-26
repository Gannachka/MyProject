using MyProject.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private TimeSpan _timeStart;
        public TimeSpan TimeStart
        {
            get
            {
                return _timeStart;
            }
            set
            {
                _timeStart = value;
                OnPropertyChanged("DayStart");
            }
        }
        private TimeSpan _timeEnd;

        public TimeSpan TimeEnd
        {
            get
            {
                return _timeEnd;
            }
            set
            {
                _timeEnd = value;
                OnPropertyChanged("TimeEnd");
            }
        }
        private DateTime _dateVisit;
        public DateTime DataVisit
        {
            get
            {
                return _dateVisit;
            }
            set
            {
                _dateVisit = value;
                OnPropertyChanged("DataVisit");
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
                OnPropertyChanged("DataVisit");
            }
        }
        private string _pacientname;
        public string PacientName
        {
            get
            {
                return _pacientname;
            }
            set
            {
                _pacientname = value;

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
        public int IdVisit { get;  set; }
        
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
                SqlConnection connection= new SqlConnection(MyProject.Properties.Settings.Default.Connection);
                try
                {
                    connection.Open();
                }
                finally
                {                    
                    string update = $"update VISIT SET TREATMENT='{_treatmrnt}' WHERE VISITID={IdVisit} ";
                    SqlCommand command = new SqlCommand(update, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
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
                SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
                try
                {
                    connection.Open();
                }
                finally
                {
                    string update = $"update VISIT SET DIAGNOSIS='{_diagnose}' WHERE VISITID={IdVisit} ";
                    SqlCommand command = new SqlCommand(update, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                OnPropertyChanged("Diagnose");

            }
        }
        public DoctorWindow _doctorWindow;
        private Doctor _doctor;
        public Doctor Visits
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged("Visits");
            }
        }
        
        public Doctor() { }
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
