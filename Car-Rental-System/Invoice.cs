using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Car_Rental_System
{
    public partial class Invoice : Form
    {
        private SqlConnection cn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cn;Integrated Security=True;");
        private SqlDataAdapter da;
        private DataSet ds = new DataSet();
        private SqlCommandBuilder cb;

        public Invoice()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        private void FillComboBoxes()
        {
            try
            {
                cn.Open();

                // Fetch client names from the Clients table and bind them to the Guna2ComboBox for clients
                string clientQuery = "SELECT Id, CONCAT(Firstname, ' ', Lastname) AS FullName FROM Client";
                SqlDataAdapter clientAdapter = new SqlDataAdapter(clientQuery, cn);
                DataTable clientTable = new DataTable();
                clientAdapter.Fill(clientTable);

                cmb_clt.DataSource = clientTable;
                cmb_clt.DisplayMember = "FullName";
                cmb_clt.ValueMember = "Id";

                // Fetch car models from the Cars table and bind them to the Guna2ComboBox for cars
                string carQuery = "SELECT Id, Model FROM Cars";
                SqlDataAdapter carAdapter = new SqlDataAdapter(carQuery, cn);
                DataTable carTable = new DataTable();
                carAdapter.Fill(carTable);

                cmb_car.DataSource = carTable;
                cmb_car.DisplayMember = "Model";
                cmb_car.ValueMember = "Id";
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
        private void Invoice_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cnDataSet2.Invoices' table. You can move, or remove it, as needed.
            this.invoicesTableAdapter.Fill(this.cnDataSet2.Invoices);

        }
        private decimal CalculateTotalPrice()
        {
            try
            {
                // Fetch rental rate from the database based on the selected car
                decimal rentalRate = 100; // Replace this with fetching rental rate from the database

                // Retrieve the selected start date and end date from the DateTimePicker controls
                DateTime startDate = startdate.Value;
                DateTime endDate = enddate.Value;

                // Calculate duration
                TimeSpan duration = endDate - startDate;
                int rentalDays = duration.Days;

                // Calculate total price based on rental rate and duration
                decimal totalPrice = rentalRate * rentalDays;

                return totalPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating total price: " + ex.Message);
                return 0;
            }
        }
        private void FillDataGrid()
        {
            try
            {
                cn.Open();
                string query = "SELECT * FROM Invoices";
                da = new SqlDataAdapter(query, cn);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds, "Invoices");

                guna2DataGridView1.DataSource = ds.Tables["Invoices"];
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

        private void cmb_clt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void startdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void addcar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = ds.Tables["Invoices"].NewRow();
                newRow["ClientID"] = cmb_clt.SelectedValue;
                newRow["CarID"] = cmb_car.SelectedValue;
                newRow["StartDate"] = startdate.Value;
                newRow["EndDate"] = enddate.Value;

                // Calculate the total price and update the DataRow
                decimal totalPrice = CalculateTotalPrice();
                newRow["TotalPrice"] = totalPrice;

                ds.Tables["Invoices"].Rows.Add(newRow);
                da.Update(ds, "Invoices");
                MessageBox.Show("Invoice added successfully!");

                ds.Tables["Invoices"].Clear();
                FillDataGrid(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding invoice: " + ex.Message);
            }
        }

        private void updatecar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = guna2DataGridView1.CurrentCell.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataRow selectedRow = ds.Tables["Invoices"].Rows[selectedRowIndex];

                    // Update selected invoice data
                    selectedRow["ClientID"] = cmb_clt.SelectedValue;
                    selectedRow["CarID"] = cmb_car.SelectedValue;
                    selectedRow["StartDate"] = startdate.Value;
                    selectedRow["EndDate"] = enddate.Value;

                    decimal totalPrice = CalculateTotalPrice();
                    selectedRow["TotalPrice"] = totalPrice;

                    da.Update(ds, "Invoices");
                    MessageBox.Show("Invoice updated successfully!");

                    ds.Tables["Invoices"].Clear();
                    FillDataGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("Please select a row to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating invoice: " + ex.Message);
            }
        }

        private void deletecar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = guna2DataGridView1.CurrentCell.RowIndex;
                if (selectedRowIndex >= 0)
                {
                    DataRow selectedRow = ds.Tables["Invoices"].Rows[selectedRowIndex];

                    selectedRow.Delete();

                    da.Update(ds, "Invoices");
                    MessageBox.Show("Invoice deleted successfully!");

                    ds.Tables["Invoices"].Clear();
                    FillDataGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting invoice: " + ex.Message);
            }
        }
    }
    
}
