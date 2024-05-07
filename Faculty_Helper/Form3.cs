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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "insert into  teachers values (@uname,@pass,@name)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@uname", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            cmd.Parameters.AddWithValue("@name", textBox3.Text);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Account created!!!!");
                Form1 fm = new Form1();
                fm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Nope!!");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
    }
}
