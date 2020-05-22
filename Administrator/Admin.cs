using MyProject.Registration;
using MyProject.User;
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

namespace MyProject.Administrator
{
    public class Admin:INotifyPropertyChanged
    {
        private int _idpacient;
        public int IDpacient
        {
            get
            {
                return _idpacient;
            }
            set
            {
                _idpacient = value;
                OnPropertyChanged("IDpacient");
            }
        }
        private int _iddoctor;
        public int IDdoctor
        {
            get
            {
                return _iddoctor;
            }
            set
            {
                _iddoctor = value;
                OnPropertyChanged("IDpacient");
            }
        }
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
        private string _doctor;
        public string Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;

                OnPropertyChanged("Doctor");
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
        private Gender _gender;
        public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
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
        private string _exrerience;
        public string Experience
        {
            get
            {
                return _exrerience;
            }
            set
            {
                _exrerience = value;
                OnPropertyChanged("Experience");
            }
        }
        private int _room;
        private readonly object Execute;

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
        
        public Admin(int id)
        {
            _idpacient = id;
            _iddoctor = id;
        }
        public Admin()
        {

        }
       
        public void DeletePacient()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string delete = $"delete from AUTINTIFICATION where IDPACIENT={IDpacient}";
                SqlCommand command1 = new SqlCommand(delete, connection);
                command1.ExecuteNonQuery();
                string deletePacient = $"delete from PACIENT where PACIENTID={IDpacient}";
                SqlCommand command = new SqlCommand(deletePacient, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteDoctor()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string delete1 = $"delete from AUTINTIFICATION where IDDOCTOR={IDdoctor}";
                SqlCommand command1 = new SqlCommand(delete1, connection);
                command1.ExecuteNonQuery();
                string delete = $"delete from DOCTOR where DOCTORID ={IDdoctor}";
                SqlCommand command = new SqlCommand(delete, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
