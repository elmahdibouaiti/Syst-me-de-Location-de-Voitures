using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_Rental_System
{
    public partial class Car : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cn;Integrated Security=True;");
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        SqlCommandBuilder cb;
        private int currentRow = 0;
        private Car car;
        private Invoice invoice;
        private Dashboard dashboard;
        public Car()
        {
            InitializeComponent();
            InitializeComponent();
            FillCarsDataSet();
        }

        private void Car_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cnDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.cnDataSet.Cars);
            // TODO: This line of code loads data into the 'cnDataSet1.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.cnDataSet1.Client);

        }
        private void FillCarsDataSet()
        {
            try
            {
                cn.Open();
                string query = "SELECT * FROM Cars";
                da = new SqlDataAdapter(query, cn);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds, "Cars");
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
        private void UpdateData()
        {
            try
            {
                da.Update(ds, "Cars");
                ds.Tables["Cars"].Clear();
                da.Fill(ds, "Cars");
                guna2DataGridView1.DataSource = ds.Tables["Cars"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void UpdateTextBoxesFromRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < ds.Tables["Cars"].Rows.Count)
            {
                DataRow row = ds.Tables["Cars"].Rows[rowIndex];
                guna2TextBox2.Text = row["Brand"].ToString();
                guna2TextBox1.Text = row["Model"].ToString();
                guna2TextBox3.Text = row["Year"].ToString();
                guna2TextBox4.Text = row["RentalPrice"].ToString();
            }
        }
        private void addcar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = ds.Tables["Cars"].NewRow();
                newRow["Brand"] = guna2TextBox2.Text;
                newRow["Model"] = guna2TextBox1.Text;
                newRow["Year"] = guna2TextBox3.Text;
                newRow["RentalPrice"] = guna2TextBox4.Text;

                ds.Tables["Cars"].Rows.Add(newRow);
                da.Update(ds, "Cars");
                MessageBox.Show("Car added successfully!");

                ds.Tables["Cars"].Clear();
                UpdateData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void updatecar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = guna2DataGridView1.CurrentCell.RowIndex;

                ds.Tables["Cars"].Rows[selectedRowIndex]["Brand"] = guna2TextBox2.Text;
                ds.Tables["Cars"].Rows[selectedRowIndex]["Model"] = guna2TextBox1.Text;
                ds.Tables["Cars"].Rows[selectedRowIndex]["Year"] = guna2TextBox3.Text;
                ds.Tables["Cars"].Rows[selectedRowIndex]["RentalPrice"] = guna2TextBox4.Text;

                da.Update(ds, "Cars");
                MessageBox.Show("Car updated successfully!");

                ds.Tables["Cars"].Clear();
                da.Fill(ds, "Cars");
                guna2DataGridView1.DataSource = ds.Tables["Cars"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void deletecar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = guna2DataGridView1.CurrentCell.RowIndex;
                ds.Tables["Cars"].Rows[selectedRowIndex].Delete();

                da.Update(ds, "Cars");
                MessageBox.Show("Car deleted successfully!");

                ds.Tables["Cars"].Clear();
                da.Fill(ds, "Cars");
                guna2DataGridView1.DataSource = ds.Tables["Cars"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void first_Click(object sender, EventArgs e)
        {
            currentRow = 0;
            UpdateTextBoxesFromRow(currentRow);
        }

        private void previous_Click(object sender, EventArgs e)
        {
            if (currentRow > 0)
            {
                currentRow--;
                UpdateTextBoxesFromRow(currentRow);
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (currentRow < ds.Tables["Cars"].Rows.Count - 1)
            {
                currentRow++;
                UpdateTextBoxesFromRow(currentRow);
            }
        }

        private void last_Click(object sender, EventArgs e)
        {
            currentRow = ds.Tables["Cars"].Rows.Count - 1;
            UpdateTextBoxesFromRow(currentRow);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
