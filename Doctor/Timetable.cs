using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Doctor
{
    public class Timetable:INotifyPropertyChanged
    {
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
        private string _mon;
        public string Mon
        {
            get
            {
                return _mon;
            }
            set
            {
                _mon = value;
                OnPropertyChanged("Mon");
            }
        }
        private string _tues;
        public string Tues
        {
            get
            {
                return _tues;
            }
            set
            {
                _tues = value;
                OnPropertyChanged("Tues");
            }
        }
        private string _wed;
        public string Wed
        {
            get
            {
                return _wed;
            }
            set
            {
                _wed = value;
                OnPropertyChanged("Wed");
            }
        }
        private string _thurs;
        public string Thurs
        {
            get
            {
                return _thurs;
            }
            set
            {
                _thurs = value;
                OnPropertyChanged("Thurs");
            }
        }
        private string _fri;
        public string Fri
        {
            get
            {
                return _fri;
            }
            set
            {
                _fri = value;
                OnPropertyChanged("Fri");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
