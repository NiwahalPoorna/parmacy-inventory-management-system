using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacyManagementSystem
{
    public partial class MedStock : Form
    {


        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Niwahal Poorna\\Documents\\projectPharmacyMS\\pharmacyManagementSystem\\pharmacyManagementSystem\\pharmacyDB.mdf\";Integrated Security=True";


        

        public MedStock()
        {
            InitializeComponent();


            
            LoadMedicineData();
           

            dataGridView1.AutoGenerateColumns = false;

           
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
            //string supplier = txtSupplierM.Text;

            string supplier = comboBox1.SelectedValue.ToString(); 



            if (string.IsNullOrWhiteSpace(medicineCode) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(batchNumber) ||
            string.IsNullOrWhiteSpace(txtQuantityM.Text) || string.IsNullOrWhiteSpace(txtCostPriceM.Text) ||
            string.IsNullOrWhiteSpace(txtSellingPriceM.Text) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

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

                    string sql = "INSERT INTO MedicineTbl (MedicineCode,NameM, BatchNumberM, QuantityM, CostPriceM, SellingPriceM, ManufactureDateM, ExpiryDateM, LocationM, SupplierM) " +
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
                LoadMedicineData();

              
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
            //txtSupplierM.Text = string.Empty;

            //comboBox1.Items.Clear();
        }



        private void LoadMedicineData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT [MedicineID] AS [Medicine ID],[MedicineCode] AS [Medicine Code], [NameM] AS [Name], [BatchNumberM] AS [Batch Number], " +
                                 "[QuantityM] AS [Quantity], [CostPriceM] AS [Cost Price], [SellingPriceM] AS [Selling Price], " +
                                 "[ManufactureDateM] AS [Manufacture Date], [ExpiryDateM] AS [Expiry Date], " +
                                 "[LocationM] AS [Location], [SupplierM] AS [Supplier] FROM MedicineTbl";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {


                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        if (!dataGridView1.Columns.Contains("SelectButton"))
                        {

                            DataGridViewButtonColumn selectButtonColumn = new DataGridViewButtonColumn();
                            selectButtonColumn.Name = "SelectButton";
                            selectButtonColumn.HeaderText = "Select";
                            selectButtonColumn.Text = "Select";
                            selectButtonColumn.UseColumnTextForButtonValue = true;
                            dataGridView1.Columns.Add(selectButtonColumn);
                        }

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
        }

        private void txtSellingPriceM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNameM_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["SelectButton"].Index && e.RowIndex >= 0)
            {
               
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string medicineID = selectedRow.Cells["Medicine ID"].Value.ToString();
                string medicineCode = selectedRow.Cells["Medicine Code"].Value.ToString();
                string name = selectedRow.Cells["Name"].Value.ToString();
                string batchNumber = selectedRow.Cells["Batch Number"].Value.ToString();
                string quantity = selectedRow.Cells["Quantity"].Value.ToString();
                string costPrice = selectedRow.Cells["Cost Price"].Value.ToString();
                string sellingPrice = selectedRow.Cells["Selling Price"].Value.ToString();
                DateTime manufactureDate = Convert.ToDateTime(selectedRow.Cells["Manufacture Date"].Value);
                DateTime expiryDate = Convert.ToDateTime(selectedRow.Cells["Expiry Date"].Value);
                string location = selectedRow.Cells["Location"].Value.ToString();
                //string supplier = selectedRow.Cells["Supplier"].Value.ToString();

                
                label1.Text = medicineID;
                txtMedicineCode.Text = medicineCode;
                txtNameM.Text = name;
                txtBatchNumberM.Text = batchNumber;
                txtQuantityM.Text = quantity;
                txtCostPriceM.Text = costPrice;
                txtSellingPriceM.Text = sellingPrice;
                dtManufactureDateM.Value = manufactureDate;
                dtExpiryDateM.Value = expiryDate;
                txtLocationM.Text = location;
                //txtSupplierM.Text = supplier;

                //comboBox1.SelectedValue = supplier; // Assuming the ComboBox is named comboBox1



            }



        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int medicineID = int.Parse(label1.Text);
            string medicineCode = txtMedicineCode.Text;
            string name = txtNameM.Text;
            string batchNumber = txtBatchNumberM.Text;
            int quantity = int.Parse(txtQuantityM.Text);
            decimal costPrice = decimal.Parse(txtCostPriceM.Text);
            decimal sellingPrice = decimal.Parse(txtSellingPriceM.Text);
            //int quantity;
            //decimal costPrice;
            //decimal sellingPrice;
            DateTime manufactureDate = dtManufactureDateM.Value;
            DateTime expiryDate = dtExpiryDateM.Value;
            string location = txtLocationM.Text;
            //string supplier = txtSupplierM.Text;
            string supplier = comboBox1.SelectedValue.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE MedicineTbl SET NameM = @Name, BatchNumberM = @BatchNumber, QuantityM = @Quantity, " +
                             "CostPriceM = @CostPrice, SellingPriceM = @SellingPrice, ManufactureDateM = @ManufactureDate, " +
                             "ExpiryDateM = @ExpiryDate, LocationM = @Location, SupplierM = @Supplier,@MedicineCode = @medicineCode " +
                             "WHERE MedicineID = @MedicineID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MedicineID", medicineID);
                    command.Parameters.AddWithValue("@MedicineCode", medicineCode);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@BatchNumber", batchNumber);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@CostPrice", costPrice);
                    command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                    command.Parameters.AddWithValue("@ManufactureDate", manufactureDate);
                    command.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@Supplier", supplier);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!");
                        
                        LoadMedicineData();

                        ClearInputFields();


                    }
                    else
                    {
                        MessageBox.Show("Failed to update the record.");
                    }
                }
            }

            }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

          
               
                string medicineID = label1.Text;

                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                   
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string sql = "DELETE FROM MedicineTbl WHERE MedicineID = @MedicineID";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@MedicineID", medicineID);
                            command.ExecuteNonQuery();
                        }
                    }
                LoadMedicineData();


               
                MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                ClearInputFields();
            }
            
            
        }

        private void PopulateDropdownList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT SupplierName FROM SupplierTbl"; // Modify with your actual table and column names

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            // Create a list to store the data
                            List<string> dataList = new List<string>();

                            while (reader.Read())
                            {
                                // Get the value from the column "NameM"
                                string value = reader.GetString(reader.GetOrdinal("SupplierName")); // Modify with your actual column name

                                // Add the value to the list
                                dataList.Add(value);
                            }

                            // Bind the data to the dropdown list
                            comboBox1.DataSource = dataList;

                            // Enable auto-suggest
                            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }









        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void MedStock_Load(object sender, EventArgs e)
        {
            PopulateDropdownList();
        }

        private void btnMedStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            MedStock Obj = new MedStock();
            Obj.Show();
        }

       

        private void btnSup_Click(object sender, EventArgs e)
        {
            this.Hide();
            Supplier Obj = new Supplier();
            Obj.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Login Obj = new Login();
            //Obj.Show();



            DialogResult result = MessageBox.Show("Do you want to log out the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                Login obj = new Login();
                obj.Show();
            }
        }
    }
}

