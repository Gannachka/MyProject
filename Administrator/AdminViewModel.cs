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

namespace MyProject.Administrator
{
    class AdminViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Admin> PACIENT { get; set; } = new ObservableCollection<Admin>();
    
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
                            pacients.Email = reader.GetString(5);
                            PACIENT.Add(pacients);
                        }
                        
                    }
                }
                connection.Close();
            }
        
       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
