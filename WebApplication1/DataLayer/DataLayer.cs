using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DataLayer
    {


        public void CreateNewUser(UserModel UserModel)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("INSERT INTO Users VALUES(@userName, @passWord, @IsActive, @email, @streetAddress, @ZipCode,@phoneNumber, @ccNumber, @paymentMethod)", cnn))
                {
                    command.Parameters.AddWithValue("@userName", UserModel.UserName);
                    command.Parameters.AddWithValue("@passWord", UserModel.PassWord);
                    command.Parameters.AddWithValue("@IsActive", UserModel.IsActive);
                    command.Parameters.AddWithValue("@email", UserModel.Email);
                    command.Parameters.AddWithValue("@streetAddress", UserModel.StreetAddress);
                    command.Parameters.AddWithValue("@ZipCode", UserModel.ZipCode);
                    command.Parameters.AddWithValue("@phoneNumber", UserModel.PhoneNumber);
                    command.Parameters.AddWithValue("@ccNumber", UserModel.CCNumber);
                    command.Parameters.AddWithValue("@paymentMethod", UserModel.PaymentMethod);
                    command.ExecuteNonQuery();

                    //result = command.ExecuteNonQuery();
                }
                //MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
        }

        public void SaveToFavorites(int movieID)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("UPDATE FavoritesList SET movieID = @movieID WHERE MovieID = @movieID IF @@ROWCOUNT = 0 INSERT INTO FavoritesList(movieID, userID) VALUES (@movieID, @userID)", cnn))
                {
                    command.Parameters.AddWithValue("@movieID", movieID);
                    command.Parameters.AddWithValue("@userID", "Yuri01");
                    command.ExecuteNonQuery();

                    //result = command.ExecuteNonQuery();
                }
                //MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
        }

        public void DeleteFromFavorites(int movieID)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("DELETE FavoritesList WHERE movieID = @movieID AND userID = @userID", cnn))
                {
                    command.Parameters.AddWithValue("@movieID", movieID);
                    command.Parameters.AddWithValue("@userID", "Yuri01");
                    command.ExecuteNonQuery();

                    //result = command.ExecuteNonQuery();
                }
                //MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
        }

        public void DeactivateMember(UserModel UserModel)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("UPDATE Users SET IsActive = 'N' WHERE userName = @userName", cnn))
                {
                    command.Parameters.AddWithValue("@userName", UserModel.UserName);

                    command.ExecuteNonQuery();
                }
                //MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
        }

        public void SaveSettings(SettingsListModel settingsListModel)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();

                for (int i = 0; i < settingsListModel.listOfSettings.Count(); i++)
                {
                    using (var command = new SqlCommand("UPDATE Settings SET settingActive = @settingActive WHERE settingID = @settingID", cnn))
                    {
                        command.Parameters.AddWithValue("@settingID", settingsListModel.listOfSettings.ElementAt(i).settingID);
                        if (settingsListModel.listOfSettings.ElementAt(i).settingsSelected)
                        {
                            command.Parameters.AddWithValue("@settingActive", "Y");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@settingActive", "N");
                        }
                        
                        command.ExecuteNonQuery();
                    }

                    
                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
        }

        public bool CheckForExistingUser(string UserName, string passWord)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            int result = 0;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM Users WHERE userName = @userName AND passWord = @passWord", cnn))
                {
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.AddWithValue("@passWord", passWord);
                    var result2 = command.ExecuteScalar();
                    try
                    {
                        result = (int)result2;
                    }
                    catch
                    {
                        result = -1;
                    }
                    
                }

                //MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            if (result == -1)
                return false;
            else
                return true;
        }

        public DataTable GetPaymentMethods()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
        cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM PaymentMethod", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();
                    
                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getAllMovies()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM MovieList", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getAllComics()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM ComicList", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getAllFavorites()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("  SELECT *   FROM FavoritesList AS f   INNER JOIN MovieList AS M ON f.movieID = M.movieID", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }



        public DataTable getAllSettings()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM Settings", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getAutoPlaySetting()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT settingActive FROM Settings WHERE settingID = 1", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getGenreNames()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM Genre", cnn))
                {
                    //command.Parameters.AddWithValue("@userName", UserName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getFriends(int userID)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT F.ID , F.userID, F.friendID, U.userName FROM[PrototypeDB].[dbo].[FriendList] AS F INNER JOIN Users AS U ON U.userId = F.friendID WHERE F.userID = @userID", cnn))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }

        public DataTable getAllUserData(string userName)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-9A0CTF1\\SQLEXPRESS;Initial Catalog=PrototypeDB;Integrated Security=SSPI;";
            DataTable dataTable = new DataTable();
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                using (var command = new SqlCommand("SELECT * FROM Users WHERE userName = @userName", cnn))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    //nn.Close();
                    da.Dispose();

                }

                //MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Can not open connection ! ");
            }
            return dataTable;
        }
    }
}
