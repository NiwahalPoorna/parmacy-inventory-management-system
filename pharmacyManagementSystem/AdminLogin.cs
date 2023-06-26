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
    public partial class AdminLogin : Form
    {


        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Niwahal Poorna\\Documents\\projectPharmacyMS\\pharmacyManagementSystem\\pharmacyManagementSystem\\pharmacyDB.mdf\";Integrated Security=True";

        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
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

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (IsValidCredentials(username, password))
            {
                MessageBox.Show("Login successful! Welcome, administrator.");

                this.Hide();
                MedStock Obj = new MedStock();
                Obj.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }


        private bool IsValidCredentials(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM AdminTbl WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count;
                        if (int.TryParse(result.ToString(), out count))
                        {
                            return count > 0;
                        }
                    }

                    return false;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
           
        }
    }
}
