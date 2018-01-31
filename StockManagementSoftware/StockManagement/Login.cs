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

namespace StockManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            textBox_Username.Focus();

        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_Password.Clear();
            textBox_Username.Clear();
            textBox_Username.Focus();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            // TO-DO: Check Login username and password and redirect to main page

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [StockDb].[dbo].[Login] where Username='" + textBox_Username.Text + "' and Password='" + textBox_Password.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_Clear_Click(sender, e);
            }

            
        }
    }
}
