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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void button_Display_Click(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=StockDb;Integrated Security=True");
            //SqlCommand cmd = new SqlCommand(@"SELECT * FROM [StockDb].[dbo].[Products]", conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT * FROM [StockDb].[dbo].[Products]", conn);
            //adapter.Fill()
        }
    }
}
