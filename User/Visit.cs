﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        public void AddNewVisit(int id, int doctorid)
        {
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
                       

                    }
                }
                connection.Close();
               
            }
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
