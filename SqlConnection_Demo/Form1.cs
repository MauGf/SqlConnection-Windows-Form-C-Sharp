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

namespace SqlConnection_Demo
{
    public partial class frMain : Form    
    {
        SqlConnection con = new SqlConnection(@"Data Source=MAU-PC\SQLEXPRESS;Initial Catalog=Ventas;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //codigo variable used in Updating and Deleting Record  
        int codigo = 0;
        public frMain()
        {
            InitializeComponent();
            DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                cmd = new SqlCommand("insert into Agenda(cliente,telefono,descripcion) values(@cliente,@telefono,@descripcion)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@cliente", textBox1.Text);
                cmd.Parameters.AddWithValue("@telefono", textBox2.Text);
                cmd.Parameters.AddWithValue("@descripcion", textBox3.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }

        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Agenda", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            codigo = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                cmd = new SqlCommand("update Agenda set cliente=@cliente,telefono=@telefono,descripcion=@descripcion where codigo=@codigo", con);
                con.Open();
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@cliente", textBox1.Text);
                cmd.Parameters.AddWithValue("@telefono", textBox2.Text);
                cmd.Parameters.AddWithValue("@descripcion", textBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (codigo != 0)
            {
                cmd = new SqlCommand("delete Agenda where codigo=@codigo", con);
                con.Open();
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programa creado Por MauG!", "Gracias!!");
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
