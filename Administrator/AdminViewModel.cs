using Microsoft.Win32;
using MyProject.Autentification;
using MyProject.baseofMVVM;
using MyProject.User;
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
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyProject.Administrator
{
    public class AdminViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Admin> PACIENTS { get; set; } = new ObservableCollection<Admin>();
        public ObservableCollection<Admin> PACIENT { get; set; } = new ObservableCollection<Admin>();
        public ObservableCollection<Admin> DOCTOR { get; set; } = new ObservableCollection<Admin>();
        public ObservableCollection<Admin> DOCTORS { get; set; } = new ObservableCollection<Admin>();
        public AdminWindow _adminWindow;
        private Add _addP;
        public Add AddP
        {
            get
            {
                return _addP;
            }
            set
            {
                _addP = value;
                OnPropertyChanged("AddP");
            }

        }
        private Admin _selectedUser;
        private AddPacient pacient;
        private AddDoctor doctor;

        public Admin SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        private RelayCommand _deletepacient;
        public RelayCommand DeletePacient
        {
            get
            {
                return _deletepacient ??
                    (_deletepacient = new RelayCommand(obj =>
                    {
                       Admin pacient = obj as Admin;
                        if (pacient != null)
                        {
                            if (_adminWindow.PacientDataGrid.SelectedItems.Count != 1)
                            {
                                MessageBox.Show("Выберете один элемент");
                            }
                            else
                            {
                                pacient.DeletePacient();
                                PACIENT.Remove(pacient);
                            }
                               
                        }
                    }));
            }
        }
        private RelayCommand _deletedoctort;
        public RelayCommand DeleteDoctor
        {
            get
            {
                return _deletedoctort ??
                    (_deletedoctort = new RelayCommand(obj =>
                    {
                        Admin doctor = obj as Admin;
                        if (doctor != null)
                        {
                            if (_adminWindow.DoctorDataGrid.SelectedItems.Count !=1) 
                            {
                                MessageBox.Show("Выберете один элемент");
                            }
                            else
                            {
                                doctor.DeleteDoctor();
                                DOCTOR.Remove(doctor);
                            }
                            
                        }
                    }));
            }
        }
        private RelayCommand _add;
        public RelayCommand Add
        {
            get
            {
                return _add ??
                    (_add = new RelayCommand(obj =>
                    {
                        AddPacient pacient = new AddPacient(this);
                        this.pacient = pacient;
                        SelectedUser = new Admin();
                        AddP = new Add();
                        pacient.ShowDialog();
                      
                   }));
            }
        }
        private RelayCommand _addNew;
        public RelayCommand AddNew
        {
            get
            {
                return _addNew ??
                    (_addNew = new RelayCommand(obj =>
                    {
                        Add pacient = obj as Add;
                        if (pacient != null)
                        {
                            pacient.AddNewPacient();
                            Admin admin = new Admin();
                            admin.IDpacient = pacient.ID;
                            admin.Name = pacient.Name;
                            admin.Surname = pacient.Surname;
                            admin.Email = pacient.Email;
                            admin.Gender = pacient.Gender;
                            PACIENT.Add(admin);
                        }
                    }));
            }
        }

        private RelayCommand _addDoctor;
        public RelayCommand AddDoctor
        {
            get
            {
                return _addDoctor ??
                    (_addDoctor = new RelayCommand(obj =>
                    {
                        AddDoctor doctor = new AddDoctor(this);
                        this.doctor = doctor;
                        SelectedUser = new Admin();
                        doctor.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addNewDoc;
        public RelayCommand AddNewDoc
        {
            get
            {
                return _addNewDoc ??
                    (_addNewDoc = new RelayCommand(obj =>
                    {
                        Admin admin = obj as Admin;
                        if (admin != null)
                        {
                            
                            admin.AddNewDoctor();
                            DOCTOR.Add(admin);
                        }

                    }));
            }
        }
        private RelayCommand _passChanged;
        public RelayCommand PassChanged1
        {
            get
            {
                return _passChanged ??
                    (_passChanged = new RelayCommand(obj =>
                    {
                        AddP.Password = pacient.AuthenticationPassBox.Password;
                    }));
            }
        }
        private RelayCommand _passchanged;
        public RelayCommand PassChanged
        {
            get
            {
                return _passchanged ??
                    (_passchanged = new RelayCommand(obj =>
                    {
                        SelectedUser.Password = doctor.AuthenticationPassBox.Password;
                    }));
            }
        }
        private RelayCommand _genderchecked;
        public RelayCommand Genderchecked
        {
            get
            {
                return _genderchecked ??
                    (_genderchecked = new RelayCommand(obj =>
                    {
                        string str = obj as string;
                        if (str.Equals("м"))
                        {

                           AddP.Gender = Gender.Man;
                        }
                        else
                        {
                            AddP.Gender = Gender.Woman;
                        }
                    }));
            }
        }
        private RelayCommand _selectDate;
        public RelayCommand DateSelect
        {
            get
            {
                return _selectDate ??
                    (_selectDate = new RelayCommand(obj =>
                    {
                        AddP.Bday = pacient.BDAY.SelectedDate.Value;
                    }));
            }
        }
        private RelayCommand _addPicture;
        public RelayCommand AddPicture
        {
            get
            {
                return _addPicture ??
                    (_addPicture = new RelayCommand(obj =>
                    {
                        OpenFileDialog ofdPicture = new OpenFileDialog();
                        ofdPicture.Filter =
                            "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                        ofdPicture.FilterIndex = 1;

                        if (ofdPicture.ShowDialog() == true)
                            AddP.Image = new BitmapImage(new Uri(ofdPicture.FileName));
                    }));
            }
        }


        public void LoadUsers()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                using (FileStream fstream = File.OpenRead("SQLQuery2.sql"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = Encoding.Default.GetString(array);
                    command.CommandText = textFromFile;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                string select = $"select * from PACIENT ";
                SqlCommand command = new SqlCommand(select, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Admin pacients = new Admin(reader.GetInt32(0));
                        pacients.IDpacient = reader.GetInt32(0);
                        pacients.Name = reader.GetString(3);
                        pacients.Surname = reader.GetString(4);
                        if (reader.GetString(5).Equals("Man"))
                            pacients.Gender = Gender.Man;
                        else
                            pacients.Gender = Gender.Woman;
                        pacients.Email = reader.GetString(6);
                        PACIENT.Add(pacients);
                    }

                }
                connection.Close();
            }           
        }
        public void LoadDoctors()
        {
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                using (FileStream fstream = File.OpenRead("SQLQuery2.sql"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = Encoding.Default.GetString(array);
                    command.CommandText = textFromFile;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {

                string selectdoc = $"select * from DOCTOR ";
                SqlCommand commanddoc = new SqlCommand(selectdoc, connection);
                using (SqlDataReader reader = commanddoc.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Admin doctors = new Admin(reader.GetInt32(0));

                        doctors.IDdoctor = reader.GetInt32(0);
                        doctors.Doctor = reader.GetString(2);
                        doctors.Specialization = reader.GetString(3);
                        doctors.Experience =Convert.ToString(reader.GetValue(4));
                        doctors.Room = reader.GetInt32(5);

                        DOCTOR.Add(doctors);
                    }

                }
                connection.Close();
            }
        }
        private RelayCommand _searchPacient;
        public RelayCommand SearchPacient
        {
            get
            {
                return _searchPacient ??
                    (_searchPacient = new RelayCommand(obj =>
                       {
                           ShowPacient pacient = new ShowPacient(this);
                           var search = from t in PACIENT where t.Surname == _adminWindow.SearchPacient.Text || t.Name == _adminWindow.SearchPacient.Text || Convert.ToString(t.IDpacient) == _adminWindow.SearchPacient.Text select t;
                           foreach (var item in search)
                           {
                               PACIENTS.Add(item);
                           }
                           pacient.ShowDialog();

                       }));
            }
        }
        private RelayCommand _searchDoctor;
        public RelayCommand SearchDoctor
        {
            get
            {
                return _searchDoctor ??
                    (_searchDoctor = new RelayCommand(obj =>
                    {
                        ShowDoctor pacient = new ShowDoctor(this);
                        var search = from t in DOCTOR where t.Specialization == _adminWindow.SearchDoctor.Text || t.Doctor == _adminWindow.SearchDoctor.Text || Convert.ToString(t.IDdoctor) == _adminWindow.SearchDoctor.Text select t;
                        foreach (var item in search)
                        {
                            DOCTORS.Add(item);
                        }
                        pacient.ShowDialog();

                    }));
            }
        }
        public AdminViewModel(AdminWindow adminWindow)
        {
            _adminWindow = adminWindow;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
