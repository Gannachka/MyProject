using MyProject.Registration;
using MyProject.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Administrator
{
    class Admin:INotifyPropertyChanged
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

        public Admin(int id)
        {
            _idpacient = id;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
