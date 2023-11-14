using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            bindData();
        }

        SqlConnection con = new SqlConnection("Data Source=MYLAPTOP\\SQLEXPRESS;Initial Catalog=ProgrammingTutorialDB;Integrated Security=True");


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlCommand sqlCommand = new SqlCommand("INSERT into ProductInfo_Tab VALUES ('" + int.Parse(txtProduct.Text) + "','" + txtItem.Text + "','" + txtDesign.Text + "','" + cbColor.Text + "',GETDATE(),null)", con);

            sqlCommand.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Successfully insert data");

            clearTxtBox();
            bindData();
        }

        public void clearTxtBox()
        {
            txtProduct.Text = "";
            txtItem.Text = "";
            txtDesign.Text = "";
            cbColor.SelectedItem = null;
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void bindData()
        {
            SqlCommand sqlcommand = new SqlCommand("SELECT * FROM ProductInfo_Tab",con);
            SqlDataAdapter sd = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand command = new SqlCommand("UPDATE ProductInfo_Tab SET ItemName='" + txtItem.Text + "',Design='" + txtDesign.Text + "',Color='" + txtDesign.Text + "',UpdateDate = GETDATE() WHERE ProductId = '" + int.Parse(txtProduct.Text) + "'", con);

            command.ExecuteNonQuery();

            con.Close();

            bindData();
            MessageBox.Show("Product Updated Successfully");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtProduct.Text != "")
            {
                con.Open();

                SqlCommand command = new SqlCommand("DELETE FROM ProductInfo_Tab WHERE ProductID = '" + int.Parse(txtProduct.Text) + "'",con);
                command.ExecuteNonQuery();

                con.Close();

                bindData();

                MessageBox.Show("Successfully deleted data");
                return;
            }

            MessageBox.Show("Product ID wajib di isi");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM ProductInfo_Tab WHERE ProductID = '" + txtProduct.Text + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
