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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace login_application
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void userTextBox_Enter(object sender, EventArgs e)
        {
            userTextBox.IconLeft = Properties.Resources.gender_neutral_user_32px__845EC2;
        }

        private void userTextBox_Leave(object sender, EventArgs e)
        {
            userTextBox.IconLeft = Properties.Resources.gender_neutral_user_32px;
        }

        private void pwTextBox_Click(object sender, EventArgs e)
        {
            pwTextBox.IconLeft = Properties.Resources.lock_26px_845EC2;
        }

        private void pwTextBox_Leave(object sender, EventArgs e)
        {
            pwTextBox.IconLeft = Properties.Resources.lock_26px;
        }

        public SqlConnection sqlConnection() {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Savinda\Documents\bookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
            return con;
        }

        public DataTable dataTable()
        {
            sqlConnection().Open();
            string query = " SELECT count (*) FROM userTbl WHERE  Uname = '" + userTextBox.Text + "' and Upass = '" + pwTextBox.Text + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlConnection());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string userName = userTextBox.Text;

            if (dataTable().Rows[0][0].ToString() == "1")
            {
                users obj = new users();
                this.Hide();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Incorrect user name or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlConnection().Close();
        }
    }
}
