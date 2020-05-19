﻿using MyProject.Administrator;
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

        public AdminWindow(MainWindow mainWindow)
        {
                      

                AdminViewModel admin = new AdminViewModel();

                DataContext = admin;
                Execute += admin.LoadUsers;
                Execute.Invoke();

                InitializeComponent();
        }
    }
}
