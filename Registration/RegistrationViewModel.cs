using Microsoft.Win32;
using MyProject.baseofMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyProject.Registration
{
    class RegistrationViewModel: INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private RegistrationWindow _registrationWindow;
        private Registration _registration;
        public Registration Registration
        {
            get
            {
                return _registration;
            }
            set
            {
                _registration= value;
                OnPropertyChanged("Registration");
            }
        }
        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {
                return _cancel ??
                    (_cancel = new RelayCommand(obj =>
                    {
                        Close();
                    }));
            }
        }
        private RelayCommand _passChanged;
        public RelayCommand PassChanged
        {
            get
            {
                return _passChanged ??
                    (_passChanged = new RelayCommand(obj =>
                    {
                        Registration.Password = _registrationWindow.AuthenticationPassBox.Password;
                    }));
            }
        }
        private RelayCommand _passConfChanged;
        public RelayCommand PassConfChanged
        {
            get
            {
                return _passConfChanged ??
                    (_passConfChanged = new RelayCommand(obj =>
                    {
                        Registration.PasswordConfirm = _registrationWindow.AuthenticationPassBox1.Password;
                    }));
            }
        }
        private RelayCommand _genderchecked;
        public RelayCommand Genderchecked
        {
            get
            {
                return _genderchecked ??
                    (_genderchecked =new RelayCommand(obj =>
                    {
                    string str= obj as string;
                        if (str.Equals("м"))
                        {

                            Registration.Gender = Gender.Man;
                        }
                        else
                        {
                            Registration.Gender = Gender.Woman;
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
                         Registration.Bday = _registrationWindow.BDAY.SelectedDate.Value;
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
                            Registration.Image = new BitmapImage(new Uri(ofdPicture.FileName));
                    }));
            }
        }
        private void Close()
        {
            _registrationWindow.Close();
            _mainWindow.Show();
        }
        public RegistrationViewModel(RegistrationWindow registrationwindow, MainWindow mainWindow)
        {
            _registrationWindow = registrationwindow;
            Registration = new Registration();
            Registration.Execute += Close;
            _mainWindow = mainWindow;

        }
        
            
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
