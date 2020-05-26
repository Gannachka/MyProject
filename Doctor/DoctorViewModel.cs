using MyProject.baseofMVVM;
using MyProject.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Doctor> VISIT { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Timetable> TIMETABLE { get; set; } = new ObservableCollection<Timetable>();
        private Doctor _Doctor;
        public Doctor DOCTOR
        {
            get
            {
                return _Doctor;
            }
            set
            {
                _Doctor = value;
                OnPropertyChanged("DOCTOR");
            }
        }
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
        private Timetable _timetable;
        public Timetable Timetable
        {
            get
            {
                return _timetable;
            }
            set
            {
                _timetable = value;
                OnPropertyChanged("Timetable");
            }
        }

        private void LoadVisits(int id)
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {

                string select1 = $"select * FROM PACIENT INNER JOIN (SELECT * FROM VISIT INNER JOIN DOCTOR ON VISIT.IDDOCTOR=DOCTOR.DOCTORID WHERE DOCTOR.DOCTORID={id} AND VISIT.DATE >=  CONVERT (date, SYSDATETIME()) ) VISITS ON VISITS.IDPACIENT=PACIENT.PACIENTID";
                SqlCommand command = new SqlCommand(select1, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.IdVisit = reader.GetInt32(8);
                        doctor.PacientName = reader.GetString(3);
                        doctor.Surname = reader.GetString(4);
                        doctor.DataVisit = reader.GetDateTime(11);
                        doctor.TimeVisit = reader.GetTimeSpan(12);
                        if (!reader.IsDBNull(13))
                        {
                            doctor.Diagnose = reader.GetString(13);

                        }
                        if (!reader.IsDBNull(14))
                        {
                            doctor.Treatment = reader.GetString(14);
                        }

                        VISIT.Add(doctor);


                    }
                }
                connection.Close();
                
            }

        }
        public void LoadTimetable()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"SELECT * FROM TIMETABLE INNER JOIN (SELECT * FROM DOCTOR INNER JOIN TABLECONNECTION ON DOCTOR.DOCTORID=TABLECONNECTION.idDOCTOR) DOCTORS ON DOCTORS.idTAMETABLE=TIMETABLE.ID";
                SqlCommand command = new SqlCommand(select, connection);
                
                using(SqlDataReader reader=command.ExecuteReader())
                {
                    int counter = 0;
                    Timetable timetable = new Timetable();
                    while (reader.Read())
                    {
                        if (counter==5)
                        {
                            TIMETABLE.Add(timetable);
                            timetable = new Timetable();                            
                            counter = 0;
                        }
                        timetable.Room = reader.GetInt32(9);
                        timetable.Doctor = reader.GetString(6);
                        switch(reader.GetString(3))
                        {
                            case "Mon":
                                timetable.Mon = reader.GetTimeSpan(1).ToString().Remove(5,3) + "-" + reader.GetTimeSpan(2).ToString().Remove(5, 3);
                                break;
                            case "Tues":
                                timetable.Tues = reader.GetTimeSpan(1).ToString().Remove(5, 3) + "-" + reader.GetTimeSpan(2).ToString().Remove(5, 3);
                                break;
                            case "Wed":
                                timetable.Wed = reader.GetTimeSpan(1).ToString().Remove(5, 3) + "-" + reader.GetTimeSpan(2).ToString().Remove(5, 3);
                                break;
                            case "Thurs":
                                timetable.Thurs = reader.GetTimeSpan(1).ToString().Remove(5, 3) + "-" + reader.GetTimeSpan(2).ToString().Remove(5, 3);
                                break;
                            case "Fri":
                                timetable.Fri = reader.GetTimeSpan(1).ToString().Remove(5, 3) + "-" + reader.GetTimeSpan(2).ToString().Remove(5, 3);
                                break;
                        }
                        counter++;
                      
                    }
                    TIMETABLE.Add(timetable);
                }
                connection.Close();
                Timetable = TIMETABLE[0];
            }

        }
        private RelayCommand _right;
        public RelayCommand Right
        {
            get
            {
                return _right ??
                    (_right = new RelayCommand(obj =>
                   {
                       int i = TIMETABLE.IndexOf(Timetable);
                       i++;
                       if (i == TIMETABLE.Count)
                           Timetable = TIMETABLE[0];
                       else
                           Timetable = TIMETABLE[i];

                   }));
            }
        }
        private RelayCommand _left;
        public RelayCommand Left
        {
            get
            {
                return _left ??
                    (_left = new RelayCommand(obj =>
                    {
                        int i = TIMETABLE.IndexOf(Timetable);
                        i--;
                        if (i == -1)
                            Timetable = TIMETABLE[TIMETABLE.Count-1];
                        else
                            Timetable = TIMETABLE[i];

                    }));
            }
        }
        public DoctorViewModel (int id)
        {
            Doctor = new Doctor(id);
            LoadVisits(id);
            LoadTimetable();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
