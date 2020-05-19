using MyProject.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Doctor
{
    class DoctorViewModel : INotifyPropertyChanged
    {
        private Doctor _doctor;
        public Doctor Doctor
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
      
        public DoctorViewModel (int id)
        {
            Doctor = new Doctor(id);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
