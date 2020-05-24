using MyProject.User;
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
    /// Логика взаимодействия для Make_appointment.xaml
    /// </summary>
    public partial class Make_appointment : Window
    {
       
        public Make_appointment(UserViewModel vM)
        {
            
            DataContext = vM;
            InitializeComponent();
            calendar.DisplayDateStart = DateTime.Now;
            Time.TimeInterval = TimeSpan.FromMinutes(15.0);
        }
       

    }
}
