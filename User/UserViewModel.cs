using MyProject.baseofMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace MyProject.User
{
    public class UserViewModel:INotifyPropertyChanged
    {
        public Make_appointment make;
        public Dictionary<string, int> Doctors = new Dictionary<string, int>();
        public ObservableCollection<User> VISITS { get; set; } = new ObservableCollection<User>();

        private Visit _visit;
        public Visit Visit
        {
            get
            {
                return _visit;
            }
            set
            {
                _visit = value;
                OnPropertyChanged("Visit");
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        private User _User;
        public User USER
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("USER");
            }
        }

       public UserViewModel(int id)
        {
            User = new User(id);
            LoadVisits(id);
        }

        public void ChangeDoctor()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"select * FROM DOCTOR WHERE SPECIALISATION='{make.doctor_specialization.SelectedItem}'";
                SqlCommand command = new SqlCommand(select, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        make.doctor_Name.Items.Add(reader.GetString(2));
                        Doctors.Add( reader.GetString(2), reader.GetInt32(0));
               

                    }
                }
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

                string select1 = $"select * FROM DOCTOR INNER JOIN (SELECT * FROM VISIT INNER JOIN PACIENT  ON VISIT.IDPACIENT=PACIENT.PACIENTID WHERE PACIENT.PACIENTID={id}) VISITS ON VISITS.IDDOCTOR=DOCTOR.DOCTORID";
                SqlCommand command = new SqlCommand(select1, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.DocName = reader.GetString(2);
                        user.DateVisit = reader.GetDateTime(9);
                        user.TimeVisit = reader.GetTimeSpan(10);
                        if (!reader.IsDBNull(11))
                        {
                            user.Diagnose = reader.GetString(11);
                        }
                      
                        if (!reader.IsDBNull(12))
                        {
                            user.Treatment = reader.GetString(12);
                        }
                        VISITS.Add(user);
                    }
                }
                connection.Close();
            }

        }
        public void ChangeSpecialisation()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"select * FROM DOCTOR ";
                SqlCommand command = new SqlCommand(select, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Visit visit = new Visit();
                        visit.DocSpesialization = reader.GetString(3);
                        make.doctor_specialization.Items.Add(visit.DocSpesialization);
                   
                    }
                }
            }
        }
        public void ChangeDate()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {
                string select = $"select * FROM TIMETABLE ";
                SqlCommand command = new SqlCommand(select, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Visit visit = new Visit();
                        visit.DocSpesialization = reader.GetString(3);
                        make.doctor_specialization.Items.Add(visit.DocSpesialization);
                       
                    }
                }
            }
        }
        private RelayCommand _sortDoctors;
        public RelayCommand SortDoctors
        {
            get
            {
                return _sortDoctors ??
                     (_sortDoctors = new RelayCommand(obj =>
                       {
                          
                         ChangeDoctor();

                     }));
            }
        }
        private RelayCommand _sortDate;
        private RelayCommand SortDate
        {
            get
            {
                return _sortDate ??
                    (_sortDate = new RelayCommand(obj =>
                    {
                        ChangeDate();
                    }));
            }
        }
        private RelayCommand _makeApp;
        public RelayCommand MakeApp
        {
            get
            {
                return _makeApp ??
                    (_makeApp = new RelayCommand(obj =>
                   {
                       Visit = new Visit();
                       Make_appointment make = new Make_appointment(this);
                       this.make = make;
                     
                       ChangeSpecialisation();
                       make.ShowDialog();

                   }));
            }
        }
        private RelayCommand _addVisit;
        public RelayCommand AddVisit
        {
            get
            {
                return _addVisit ??
                  (_addVisit = new RelayCommand(obj =>
                 {
                     if (make.calendar.SelectedDate != null && make.Time.Value != null)
                     {
                         Visit.DateVisit = (DateTime)make.calendar.SelectedDate;
                         if (Visit.DateVisit.DayOfWeek != DayOfWeek.Saturday && Visit.DateVisit.DayOfWeek != DayOfWeek.Sunday)
                         {
                             DateTime time = (DateTime)make.Time.Value;
                             Visit.TimeVisit = time.TimeOfDay;
                             int DoctorId;
                             Doctors.TryGetValue(make.doctor_Name.Text, out DoctorId);
                             Visit.AddNewVisit(User.id, DoctorId);
                             make.Close();
                         }
                         else
                             MessageBox.Show("Дайте врачам отдохнуть");
                     }
                     else
                         MessageBox.Show("Данные не введены");
                     
                 }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
