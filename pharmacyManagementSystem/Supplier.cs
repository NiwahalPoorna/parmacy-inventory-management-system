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
    public partial class Supplier : Form
    {

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Niwahal Poorna\\Documents\\projectPharmacyMS\\pharmacyManagementSystem\\pharmacyManagementSystem\\pharmacyDB.mdf\";Integrated Security=True";

        public Supplier()
        {
            InitializeComponent();

            DisplayDataInGridView();
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Supplier_Load(object sender, EventArgs e)
        {

        }

        private void txtContactPerson_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string supplierID = txtSupplierID.Text;
            string supplierName = txtSupplierName.Text;
            string contactPerson = txtContactPerson.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

           
            if (string.IsNullOrWhiteSpace(supplierName) || string.IsNullOrWhiteSpace(contactPerson) || string.IsNullOrWhiteSpace(supplierID) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string sql = "INSERT INTO SupplierTbl (SupplierID,SupplierName, ContactPerson, Email, Phone, Address) " +
                                 "VALUES (@SupplierID,@SupplierName, @ContactPerson, @Email, @Phone, @Address)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.Parameters.AddWithValue("@SupplierName", supplierName);
                        command.Parameters.AddWithValue("@ContactPerson", contactPerson);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", address);

                      
                        command.ExecuteNonQuery();
                    }
                }

               
               
                DialogResult result = MessageBox.Show("Supplier details saved successfully. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ResetFields();
                    DisplayDataInGridView();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        
    }

        private void ResetFields()
        {
            txtSupplierID.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void DisplayDataInGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT SupplierID,SupplierName, ContactPerson, Email, Phone, Address FROM SupplierTbl";
                    SqlCommand command = new SqlCommand(sql, connection);

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

                    dataGridView1.Columns["SupplierID"].HeaderText = "ID";
                    dataGridView1.Columns["SupplierName"].HeaderText = "Supplier Name";
                    dataGridView1.Columns["ContactPerson"].HeaderText = "Contact Person";
                    dataGridView1.Columns["Email"].HeaderText = "Email";
                    dataGridView1.Columns["Phone"].HeaderText = "Phone";
                    dataGridView1.Columns["Address"].HeaderText = "Address";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFields();

            DisplayDataInGridView();
        }

      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int supplierID = Convert.ToInt32(txtSupplierID.Text);
            string supplierName = txtSupplierName.Text;
            string contactPerson = txtContactPerson.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE SupplierTbl SET SupplierName = @SupplierName, ContactPerson = @ContactPerson, Email = @Email, Phone = @Phone, Address = @Address WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    command.Parameters.AddWithValue("@SupplierName", supplierName);
                    command.Parameters.AddWithValue("@ContactPerson", contactPerson);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        DialogResult result = MessageBox.Show("Update successful. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            DisplayDataInGridView();

                            ResetFields();
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string supplierID = txtSupplierID.Text;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM   SupplierTbl WHERE SupplierID = @SupplierID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.ExecuteNonQuery();
                    }
                }
                
               
                ResetFields();


                DialogResult result1 =  MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result1 == DialogResult.Yes)
                {
                    DisplayDataInGridView();

                   
                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                Application.Exit();

            }
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Obj = new Login();
            Obj.Show();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtSupplierID.Text = row.Cells["SupplierID"].Value.ToString();
                txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                txtContactPerson.Text = row.Cells["ContactPerson"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }
    }
}
