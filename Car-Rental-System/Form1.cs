using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_Rental_System
{
    public partial class Form1 : Form
    {
        private Dashboard Dashboard;
        SqlConnection cn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cn;Integrated Security=True;");
        public Form1()
        {
            InitializeComponent();
            InitializeMyForm();
            Dashboard = new Dashboard();
        }

        public void InitializeMyForm()
        {
            BackColor = Color.LightBlue;
            TransparencyKey = BackColor;
        }

        private void pn_register_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Guna2CircleButton2_Click(object sender, EventArgs e)
        {
            pn_register.Visible = true;
            pn_login.Visible = false;
            guna2Transition1.ShowSync(pn_register);
        }

        

        private void Guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            pn_login.Visible = true;
            pn_register.Visible = false;
            guna2Transition1.ShowSync(pn_login);
        }

        private void Guna2Button2_Click(object sender, EventArgs e)
        {
            string username = Guna2TextBox4.Text;
            string password = Guna2TextBox5.Text;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                try
                {
                    cn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int userCount = (int)command.ExecuteScalar();

                    if (userCount > 0)
                    {
                        MessageBox.Show("Login successful!");
                        // Navigate to your dashboard or next form upon successful login
                        this.Hide();
                        Dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    cn.Close(); // Ensure to close the connection
                }
            }
            else
            {
                MessageBox.Show("Please enter username and password.");
            }

        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            string email = Guna2TextBox3.Text;
            string username = Guna2TextBox1.Text;
            string password = Guna2TextBox2.Text;
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                try
                {
                    cn.Open();
                        string query = "INSERT INTO Users (Email, Username, Password) VALUES (@Email, @Username, @Password)";
                        SqlCommand command = new SqlCommand(query, cn);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Signup successful!");

                            pn_login.Visible = true;
                            pn_register.Visible = false;
                            guna2Transition1.ShowSync(pn_login);

                            Guna2TextBox3.Text = "";
                            Guna2TextBox1.Text = "";
                            Guna2TextBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Signup failed. Please try again.");
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the fields.");
            }

        }

        private void pn_login_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
