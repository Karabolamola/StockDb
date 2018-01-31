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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox_Status.SelectedIndex = 0;
            LoadData();
        }

        private bool IfProductsExists(SqlConnection conn, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select 1 from [Products] WHERE [ProductCode] = '" + productCode + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void button_Add_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Integrated Security=True");
            //SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [StockDb].[dbo].[Products] where ProductCode='" + textBox_Product_Code + "' and ProductName='" + textBox_Product_Name + "'", conn);
            //DataTable dt = new DataTable();

            conn.Open();
            bool status = false;
            if (comboBox_Status.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var sqlQuery = "";
            if (IfProductsExists(conn, textBox_Product_Code.Text))
            {
                sqlQuery = @"UPDATE [StockDb].[dbo].[Products] SET [ProductName] = '" + textBox_Product_Name.Text + "', [ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox_Product_Code.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO[StockDb].[dbo].[Products] ([ProductCode],[ProductName],[ProductStatus]) VALUES('" + textBox_Product_Code.Text + "','" + textBox_Product_Name.Text + "','" + status + "')";
            }

            


            // Insert Logic
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            textBox_Product_Code.Clear();
            textBox_Product_Name.Clear();

            comboBox_Status.SelectedIndex = 0;

            //Reading Data
            LoadData();
        }

        

        public void LoadData()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Integrated Security=True");

            // Reading Data
            SqlDataAdapter sda = new SqlDataAdapter("select * from [StockDb].[dbo].[Products]", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView_Products.Rows.Clear();
            //dataGridView_Products.DefaultCellStyle.SelectionForeColor = Color.Black;
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView_Products.Rows.Add();
                dataGridView_Products.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView_Products.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView_Products.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView_Products.Rows[n].Cells[2].Value = "Inactive";
                }
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Integrated Security=True");
            //SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [StockDb].[dbo].[Products] where ProductCode='" + textBox_Product_Code + "' and ProductName='" + textBox_Product_Name + "'", conn);
            //DataTable dt = new DataTable();

            var sqlQuery = "";
            if (IfProductsExists(conn, textBox_Product_Code.Text))
            {
                conn.Open();
                sqlQuery = @"DELETE FROM [StockDb].[dbo].[Products] WHERE ProductCode='" + textBox_Product_Code.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                textBox_Product_Code.Clear();
                textBox_Product_Name.Clear();
                comboBox_Status.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Record not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Insert Logic
            //SqlCommand cmd = new SqlCommand(@"DELETE FROM [StockDb].[dbo].[Products] WHERE ProductCode='" + textBox_Product_Code.Text + "'", conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //textBox_Product_Code.Clear();
            //textBox_Product_Name.Clear();
            //comboBox_Status.SelectedIndex = 0;

            // Reading Data
            LoadData();
        }

        private void dataGridView_Products_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Product_Code.Text = dataGridView_Products.SelectedRows[0].Cells[0].Value.ToString();
            textBox_Product_Name.Text = dataGridView_Products.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView_Products.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox_Status.SelectedIndex = 0;
            }
            else
            {
                comboBox_Status.SelectedIndex = 1;
            };
        }

       

        //ds.Clear();

        //da.SelectCommand = new SqlCommand("SELECT * FROM tblContacts", sc);
        //da.Fill(ds);

        //dataGrid.DataSource = ds.Tables[0];

        //DataSet ds = new DataSet();
        //SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Intergrated Security=True");
        //SqlCommand cmd = new SqlCommand(@"SELECT * FROM [StockDb].[dbo].[Products]", conn);
        //dataGridView_Products.DataSource = ds.Tables[0];
    }
}
