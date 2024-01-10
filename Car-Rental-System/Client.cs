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
    public partial class Client : Form
    {
        private SqlConnection cn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cn;Integrated Security=True;");
        private SqlDataAdapter da;
        private DataSet ds = new DataSet();
        private SqlCommandBuilder cb;
        private int currentRow = 0;
        private Car car;
        private Invoice invoice;
        private Dashboard dashboard;
        public Client()
        {
            InitializeComponent();
            FillClientsDataSet();
            car = new Car();
            invoice = new Invoice();
            dashboard = new Dashboard();
        }
        private void FillClientsDataSet()
        {
            try
            {
                cn.Open();
                string query = "SELECT * FROM Client";
                da = new SqlDataAdapter(query, cn);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds, "Client");
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
        private void UpdateTextBoxesFromRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < ds.Tables["Client"].Rows.Count)
            {
                DataRow row = ds.Tables["Client"].Rows[rowIndex];
                guna2TextBox2.Text = row["Firstname"].ToString();
                guna2TextBox1.Text = row["Lastname"].ToString();
                guna2TextBox3.Text = row["CIN"].ToString();
                guna2TextBox4.Text = row["Phone"].ToString();
            }
        }
        private void addcar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = ds.Tables["Client"].NewRow();
                newRow["Firstname"] = guna2TextBox2.Text;
                newRow["Lastname"] = guna2TextBox1.Text;
                newRow["CIN"] = guna2TextBox3.Text;
                newRow["Phone"] = guna2TextBox4.Text;

                ds.Tables["Client"].Rows.Add(newRow);
                da.Update(ds, "Client");
                MessageBox.Show("Client added successfully!");

                ds.Tables["Client"].Clear();
                da.Fill(ds, "Client");
                guna2DataGridView1.DataSource = ds.Tables["Client"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cnDataSet1.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.cnDataSet1.Client);

        }

        private void updatecar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRowIndex = guna2DataGridView1.CurrentCell.RowIndex;

                ds.Tables["Client"].Rows[selectedRowIndex]["Firstname"] = guna2TextBox2.Text;
                ds.Tables["Client"].Rows[selectedRowIndex]["Lastname"] = guna2TextBox1.Text;
                ds.Tables["Client"].Rows[selectedRowIndex]["CIN"] = guna2TextBox3.Text;
                ds.Tables["Client"].Rows[selectedRowIndex]["Phone"] = guna2TextBox4.Text;

                da.Update(ds, "Client");
                MessageBox.Show("Client updated successfully!");

                ds.Tables["Client"].Clear();
                da.Fill(ds, "Client");
                guna2DataGridView1.DataSource = ds.Tables["Client"];
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
                ds.Tables["Client"].Rows[selectedRowIndex].Delete();

                da.Update(ds, "Client");
                MessageBox.Show("Client deleted successfully!");

                ds.Tables["Client"].Clear();
                da.Fill(ds, "Client");
                guna2DataGridView1.DataSource = ds.Tables["Client"];
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
            if (currentRow < ds.Tables["Client"].Rows.Count - 1)
            {
                currentRow++;
                UpdateTextBoxesFromRow(currentRow);
            }
        }

        private void last_Click(object sender, EventArgs e)
        {
            currentRow = ds.Tables["Client"].Rows.Count - 1;
            UpdateTextBoxesFromRow(currentRow);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
