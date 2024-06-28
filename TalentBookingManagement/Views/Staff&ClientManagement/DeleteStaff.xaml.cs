﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using TalentBookingManagement.Models;

namespace TalentBookingManagement.Views.Staff_ClientManagement
{
    /// <summary>
    /// Interaction logic for DeleteStaff.xaml
    /// </summary>
    public partial class DeleteStaff : Window
    {
        public DeleteStaff()
        {
            InitializeComponent();
        }
        private void LoadStaffDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(StaffIDInputTextBox.Text, out int staffId))
            {
                LoadStaffDetails(staffId);
            }
            else
            {
                MessageBox.Show("Please enter a valid Staff ID.");
            }
        }
        private void LoadStaffDetails(int staffId)
        {

            Staff staff = GetStaffFromDatabase(staffId);

            if (staff != null)
            {
                StaffIDTextBox.Text = staff.StaffID.ToString();
                FirstNameTextBox.Text = staff.FirstName;
                LastNameTextBox.Text = staff.LastName;
                PhoneNumberTextBox.Text = staff.PhoneNumber;
                AgeTextBox.Text = staff.Age.ToString();
                GenderTextBox.Text = staff.Gender;
                EmailTextBox.Text = staff.Email;
                CityTextBox.Text = staff.City;
                SuburbTextBox.Text = staff.Suburb;
                StreetAddressTextBox.Text = staff.StreetAddress;
                PostcodeTextBox.Text = staff.Postcode.ToString();
                ActiveStatusTextBox.Text = staff.ActiveStatus;
                UsernameTextBox.Text = staff.Username;
                PasswordTextBox.Text = staff.Password;
                RoleIDTextBox.Text = staff.RoleID.ToString();

                StaffDetailsStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Staff not found.");
            }
        }
        private Staff GetStaffFromDatabase(int staffId)
        {
            string connectionString = "Server=citizen.manukautech.info,6306;Database=S601_LetItGo_Project;User Id=S601_LetItGo;Password=fBit$26170;"; // Update with your actual connection string
            Staff staff = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("ViewStaffDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StaffID", staffId);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                staff = new Staff
                                {
                                    StaffID = Convert.ToInt32(reader["StaffID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Age = Convert.ToInt32(reader["Age"]),
                                    Gender = reader["Gender"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    City = reader["City"].ToString(),
                                    Suburb = reader["Suburb"].ToString(),
                                    StreetAddress = reader["StreetAddress"].ToString(),
                                    Postcode = reader["Postcode"].ToString(),
                                    ActiveStatus = reader["ActiveStatus"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    RoleID = Convert.ToInt32(reader["RoleID"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


            return staff;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(StaffIDTextBox.Text, out int staffId))
            {
                bool isDeleted = DeleteStaffLogically(staffId);

                if (isDeleted)
                {
                    MessageBox.Show("Staff deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete staff.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Staff ID.");
            }
        }

        private bool DeleteStaffLogically(int staffId)
        {
            string connectionString = "Server=citizen.manukautech.info,6306;Database=S601_LetItGo_Project;User Id=S601_LetItGo;Password=fBit$26170;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteStaff", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StaffID", staffId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}