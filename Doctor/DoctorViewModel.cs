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
        public ObservableCollection<Doctor> VISIT { get; set; } = new ObservableCollection<Doctor>();
        private void LoadVisits(int id)
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {

                string select1 = $"select * FROM PACIENT INNER JOIN (SELECT * FROM VISIT INNER JOIN DOCTOR ON VISIT.IDDOCTOR=DOCTOR.DOCTORID WHERE DOCTOR.DOCTORID={id}) VISITS ON VISITS.IDPACIENT=PACIENT.PACIENTID";
                SqlCommand command = new SqlCommand(select1, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Doctor doctor = new Doctor();
                        doctor.IdVisit = reader.GetInt32(1);
                        doctor.PacientName = reader.GetString(3);
                        doctor.Surname = reader.GetString(4);
                        doctor.DataVisit = reader.GetDateTime(11);
                        doctor.TimeVisit = reader.GetTimeSpan(12);
                        if (reader.IsDBNull(0))
                        {
                            doctor.Diagnose = reader.GetString(13);

                        }
                        if (reader.IsDBNull(0))
                        {
                            doctor.Treatment = reader.GetString(14);
                        }

                        VISIT.Add(doctor);


                    }
                }
                connection.Close();
            }

        }
        public DoctorViewModel (int id)
        {
            Doctor = new Doctor(id);
            LoadVisits(id);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
