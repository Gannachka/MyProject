using MyProject.Administrator;
using MyProject.Autentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public delegate void WindowLoaded();
        public event WindowLoaded Execute;
        public delegate void WindowLoadedD();
        public event WindowLoadedD ExecuteD;
        private MainWindow _mainWindow;

        public AdminWindow(MainWindow mainWindow)
        {
           AdminViewModel admin = new AdminViewModel(this);

                DataContext = admin;
                Execute += admin.LoadUsers;
                ExecuteD += admin.LoadDoctors;
                Execute.Invoke();
            ExecuteD.Invoke();
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainWindow.Show();
        }
    }
}
