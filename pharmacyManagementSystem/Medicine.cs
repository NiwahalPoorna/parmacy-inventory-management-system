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

namespace pharmacyManagementSystem
{
    public partial class Medicine : Form
    {


        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Niwahal Poorna\\Documents\\projectPharmacyMS\\pharmacyManagementSystem\\pharmacyManagementSystem\\pharmacyDB.mdf\";Integrated Security=True";

        public Medicine()
        {
            InitializeComponent();

            // Set the column names for the DataGridView
            //dataGridView1.Columns[0].HeaderText = "Medicine Code";
            //dataGridView1.Columns[1].HeaderText = "Name";
            //dataGridView1.Columns[2].HeaderText = "Batch Number";
            //dataGridView1.Columns[3].HeaderText = "Quantity";
            //dataGridView1.Columns[4].HeaderText = "Cost Price";
            //dataGridView1.Columns[5].HeaderText = "Selling Price";
            //dataGridView1.Columns[6].HeaderText = "Manufacture Date";
            //dataGridView1.Columns[7].HeaderText = "Expiry Date";
            //dataGridView1.Columns[8].HeaderText = "Location";
            //dataGridView1.Columns[9].HeaderText = "Notes";

            // Call the method to load and display the data in the DataGridView
            LoadMedicineData();
        }

        private void Medicine_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNotesM_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            string medicineCode = txtMedicineCode.Text;
            string name = txtNameM.Text;
            string batchNumber = txtBatchNumberM.Text;
            //int quantity = int.Parse(txtQuantityM.Text);
            //decimal costPrice = decimal.Parse(txtCostPriceM.Text);
            //decimal sellingPrice = decimal.Parse(txtSellingPriceM.Text);
            int quantity;
            decimal costPrice;
            decimal sellingPrice;
            DateTime manufactureDate = dtManufactureDateM.Value;
            DateTime expiryDate = dtExpiryDateM.Value;
            string location = txtLocationM.Text;
            string supplier = txtNotesM.Text;


            if (string.IsNullOrWhiteSpace(medicineCode) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(batchNumber) ||
          string.IsNullOrWhiteSpace(txtQuantityM.Text) || string.IsNullOrWhiteSpace(txtCostPriceM.Text) ||
          string.IsNullOrWhiteSpace(txtSellingPriceM.Text) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            // Parse the numeric fields
            if (!int.TryParse(txtQuantityM.Text, out quantity) || !decimal.TryParse(txtCostPriceM.Text, out costPrice) || !decimal.TryParse(txtSellingPriceM.Text, out sellingPrice))
            {
                MessageBox.Show("Invalid numeric value entered. Please enter valid numeric values.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO MedicineTbl (MedicineCode,NameM, BatchNumberM, QuantityM, CostPriceM, SellingPriceM, ManufactureDateM, ExpiryDateM, LocationM, Supplier) " +
                                 "VALUES (@MedicineCode,@NameM, @BatchNumberM, @QuantityM, @CostPriceM, @SellingPriceM, @ManufactureDateM, @ExpiryDateM, @LocationM, @SupplierM)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineCode", medicineCode);
                        command.Parameters.AddWithValue("@NameM", name);
                        command.Parameters.AddWithValue("@BatchNumberM", batchNumber);
                        command.Parameters.AddWithValue("@QuantityM", quantity);
                        command.Parameters.AddWithValue("@CostPriceM", costPrice);
                        command.Parameters.AddWithValue("@SellingPriceM", sellingPrice);
                        command.Parameters.AddWithValue("@ManufactureDateM", manufactureDate);
                        command.Parameters.AddWithValue("@ExpiryDateM", expiryDate);
                        command.Parameters.AddWithValue("@LocationM", location);
                        command.Parameters.AddWithValue("@SupplierM", supplier);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data stored successfully!");

                // Clear the input fields
                ClearInputFields();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while storing the data: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }


        }

        private void ClearInputFields()
        {
            txtMedicineCode.Text = string.Empty;
            txtNameM.Text = string.Empty;
            txtBatchNumberM.Text = string.Empty;
            txtQuantityM.Text = string.Empty;
            txtCostPriceM.Text = string.Empty;
            txtSellingPriceM.Text = string.Empty;
            dtManufactureDateM.Value = DateTime.Now;
            dtExpiryDateM.Value = DateTime.Now;
            txtLocationM.Text = string.Empty;
            //txtSupM.Text = string.Empty;
        }

       

        private void LoadMedicineData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT [MedicineCode] AS [Medicine Code], [NameM] AS [Name], [BatchNumberM] AS [Batch Number], " +
                                 "[QuantityM] AS [Quantity], [CostPriceM] AS [Cost Price], [SellingPriceM] AS [Selling Price], " +
                                 "[ManufactureDateM] AS [Manufacture Date], [ExpiryDateM] AS [Expiry Date], " +
                                 "[LocationM] AS [Location], [SupplierM] AS [Supplier] FROM MedicineTbl";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while loading the data: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtMedicineCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNameM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBatchNumberM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantityM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCostPriceM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSellingPriceM_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtManufactureDateM_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtExpiryDateM_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtLocationM_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
        }

       

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
