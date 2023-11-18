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

namespace login_application
{
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            viewTbl();
        }

        int key = 0;

        public SqlConnection sqlconnection() {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Savinda\Documents\bookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
            return con;
        }

        //refresh table data
        public void viewTbl()
        {
            sqlconnection().Open();

            string query = "SELECT * FROM userTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlconnection());
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            DataGridView.DataSource = ds.Tables[0];
            sqlconnection().Close();
        }

        //All text box clear function
        public void reset()
        {
            AddressTextBox.Clear();
            NameTextBox.Clear();
            PasswordTextBox.Clear();
            TpNumberTextBox1.Clear();
        }

        //when click on the table cell the selected row was display in text boxes
        public void ViewTableDataInTextBox() {
            NameTextBox.Text = DataGridView.SelectedRows[0].Cells[1].Value.ToString();
            TpNumberTextBox1.Text = DataGridView.SelectedRows[0].Cells[2].Value.ToString();
            AddressTextBox.Text = DataGridView.SelectedRows[0].Cells[3].Value.ToString();
            PasswordTextBox.Text = DataGridView.SelectedRows[0].Cells[4].Value.ToString();

            if (NameTextBox.Text.Length == 0)
            {
                key = 0;
            }
            else
            {
                key = int.Parse(DataGridView.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        //add data to row
        public void add() {
            if (AddressTextBox.Text.Length == 0 || NameTextBox.Text.Length == 0 || PasswordTextBox.Text.Length == 0 || TpNumberTextBox1.Text.Length == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlconnection().Open();

                    string query = " INSERT INTO  userTbl VALUES ('" + NameTextBox.Text + "', '" + TpNumberTextBox1.Text + "', '" + AddressTextBox.Text + "', '" + PasswordTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, sqlconnection());
                    cmd.ExecuteNonQuery();

                    sqlconnection().Close();

                    viewTbl();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //delete row
        public void delete()
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlconnection().Open();

                    string query = " DELETE FROM userTbl WHERE  Uid = '" + key + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlconnection());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sucessfuly Deleted !");

                    reset();

                    sqlconnection().Close();

                    viewTbl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void edit() {
            if (NameTextBox.Text.Length == 0 || TpNumberTextBox1.Text.Length == 0 || AddressTextBox.Text.Length == 0 || PasswordTextBox.Text.Length == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlconnection().Open();

                    string query = " UPDATE userTbl SET  Uname = '" + NameTextBox.Text + "', Uadd = '" + AddressTextBox.Text + "', Upass = '" + PasswordTextBox.Text + "' WHERE Uid = '" + key + "' ";
                    SqlCommand cmd = new SqlCommand(query, sqlconnection());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information Updated");

                    reset();

                    sqlconnection().Close();

                    viewTbl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewTableDataInTextBox();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            add();
        }
    } 
}
