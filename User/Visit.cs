﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyProject.User
{
    public class Visit:INotifyPropertyChanged
    {       
        private string _docName;
        public string DocName
        {
            get
            {
                return _docName;
            }
            set
            {
                _docName = value;
                OnPropertyChanged("DocName");
            }
        }
        public UserViewModel UserViewModel { get; private set; }
        private string _docSpecialization;
        public string DocSpesialization
        {
            get
            {
                return _docSpecialization;
            }
            set
            {
                _docSpecialization = value;
                OnPropertyChanged("DocSpesialization");
            }
        }
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
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _dateVisit;
        public DateTime DateVisit
        {
            get
            {
                return _dateVisit;
            }
            set
            {
                _dateVisit = value;
                OnPropertyChanged("DateVisit");
            }
        }
        private TimeSpan _timeVisit;
        public TimeSpan TimeVisit
        {
            get
            {
                return _timeVisit;
            }
            set
            {
                _timeVisit = value;
                OnPropertyChanged("TimeVisit");
            }
        }
        private string _treatmrnt;
        public string Treatment
        {
            get
            {
                return _treatmrnt;
            }
            set
            {
                _treatmrnt = value;
              OnPropertyChanged("Treatment");

            }
        }
        private string _diagnose;
        public string Diagnose
        {
            get
            {
                return _diagnose;
            }
            set
            {
                _diagnose = value;
                OnPropertyChanged("Diagnose");

            }
        }
        private int id;            
        public Visit()
        {
        }   
       
        public User AddNewVisit(int id, int doctorid)
        {
            User user=null;
            SqlConnection connection = new SqlConnection(MyProject.Properties.Settings.Default.Connection);
            try
            {
                connection.Open();
            }
            finally
            {                
                string select = $"select * FROM VISIT WHERE TIME='{TimeVisit}' AND DATE='{DateVisit}' AND idDoctor='{doctorid}'";
                SqlCommand command = new SqlCommand(select, connection);
                using(SqlDataReader reader= command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        string insert = $"insert into VISIT (IDPACIENT, IDDOCTOR, DATE, TIME) VALUES ({id},{doctorid},'{DateVisit}','{TimeVisit}') ";
                        command.CommandText = insert;
                        reader.Close();
                        command.ExecuteNonQuery();
                        string findInfo = $"select ROOM, NAME from DOCTOR where DOCTORID={doctorid}";
                        command.CommandText = findInfo;
                        using (SqlDataReader dataReader = command.ExecuteReader()) 
                        {
                            dataReader.Read();
                            user = new User();
                            user.DateVisit = this.DateVisit;
                            user.TimeVisit = this.TimeVisit;
                            user.DocName = dataReader.GetString(1);
                            user.Room = dataReader.GetInt32(0);
                        }
                    }
                    else
                        MessageBox.Show("Данное время занято!");
                }
                connection.Close();                
            }
            return user;
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
