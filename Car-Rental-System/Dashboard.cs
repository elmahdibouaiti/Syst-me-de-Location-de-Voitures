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

namespace Car_Rental_System
{
    public partial class Dashboard : Form
    {
        private SqlConnection cn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cn;Integrated Security=True;");

        private Client Client;
        private Car Car;
        private Invoice Invoice;
        public Dashboard()
        {
            InitializeComponent();
            DisplayCounts();
            Client = new Client();
            Car = new Car();
            Invoice = new Invoice();
        }
        private void DisplayCounts()
        {
            try
            {
                cn.Open();
                // Get count of clients
                SqlCommand cmdClients = new SqlCommand("SELECT COUNT(*) FROM Client", cn);
                int clientCount = (int)cmdClients.ExecuteScalar();
                lbclt.Text = clientCount.ToString();

                // Get count of cars
                SqlCommand cmdCars = new SqlCommand("SELECT COUNT(*) FROM Cars", cn);
                int carCount = (int)cmdCars.ExecuteScalar();
                lbinv.Text = carCount.ToString();

                // Get count of invoices
                SqlCommand cmdInvoices = new SqlCommand("SELECT COUNT(*) FROM Invoices", cn);
                int invoiceCount = (int)cmdInvoices.ExecuteScalar();
                lbcr.Text = invoiceCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Invoice.Show();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {

        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            this.Hide();
            Client.Show();

        }

        private void btn_car_Click(object sender, EventArgs e)
        {
            this.Hide();
            Car.Show();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
