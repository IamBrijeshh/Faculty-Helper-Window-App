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

namespace Faculty_Helper
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
        public String fgetTextBox1String()
        {
            return textBox1.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "select count(*) from teachers where username=@user and password=@pass";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            int i = Convert.ToInt32(da.GetValue(0));
            if(textBox1.Text != "")
            {
                Globle.setText(textBox1.Text);
            }
            if (i > 0)
            {
                Form2 fm = new Form2();
                fm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Nope!!");
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
