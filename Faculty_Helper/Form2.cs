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
    public partial class Form2 : Form
    {
        public string teachers;
        public Form2()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "select name from teachers where username = @iuser";
            InitializeComponent();
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            comboBox5.SelectedItem = wk.ToString().ToLower();
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@iuser", Globle.getText());
            SqlDataReader da = cmd.ExecuteReader();
            da.Read();
            label3.Text = label3.Text + da[0];
            teachers = da[0].ToString();
            con.Close();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(comboBox1.SelectedItem) > Convert.ToDateTime(comboBox2.SelectedItem))
                MessageBox.Show("Enter a valid 'to' and 'from' paramater");
            else
            {
                string p;
                
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                for (int k = 1; k <= 16; k++)
                {
                    int pol = k;
                    if (pol > 8)
                    {
                        pol = pol - 8;
                        comboBox4.Items.Add("Lab " + pol);
                    }
                    else
                    {
                        comboBox4.Items.Add("Class " + pol);
                    }
                }
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                //string q = "select distinct(class_id) from @day where too>=@ifrom and fromm<=@ito";
                string q = "select distinct(class_id) from " + comboBox5.SelectedItem.ToString() + " where too>@ifrom and fromm<@ito";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@ifrom", comboBox1.SelectedItem);
                cmd.Parameters.AddWithValue("@ito", comboBox2.SelectedItem);
                cmd.Parameters.AddWithValue("@day", comboBox5.SelectedItem.ToString());
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {

                    int pol = Convert.ToInt32(da[0]);
                    if (pol > 8)
                    {
                        pol = pol - 8;
                        p = "Lab " + pol.ToString();
                        comboBox3.Items.Add("Lab " + pol);
                    }
                    else
                    {
                        p = "Class " + pol.ToString();
                        comboBox3.Items.Add("Class " + pol);
                    }
                    if (comboBox3.Items.Contains(p))
                    {
                        comboBox4.Items.Remove(p);
                    }
                }
                con.Close();

                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "insert into " + comboBox5.SelectedItem.ToString() + " values (@class,@too,@fromm,@teacher)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@fromm", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@too", comboBox2.SelectedItem);

            string cl = comboBox4.SelectedItem.ToString();
            String[] strlist = cl.Split(' ');

            int cn = Convert.ToInt32(strlist[1]);
            String tempclass = strlist[0];
            //MessageBox.Show(tempclass + "_" + cn);
            if (tempclass.Equals("Lab"))
            {
                cn = cn + 8;
            }
            //MessageBox.Show("New : "+cn);

            cmd.Parameters.AddWithValue("@class", cn);
            cmd.Parameters.AddWithValue("@teacher", teachers);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                MessageBox.Show("Class Booked");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "select class_id,too,fromm from " + comboBox5.SelectedItem.ToString() + " where booked_teacher = @iname";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@iname", teachers);
            SqlDataReader da = cmd.ExecuteReader();
            string p;
            while (da.Read())
            {

                int pol = Convert.ToInt32(da[0]);
                if (pol > 8)
                {
                    pol = pol - 8;
                    p = "Lab " + pol.ToString();
                    comboBox6.Items.Add("Lab " + pol + " : " +da[2].ToString()+" - "+da[1].ToString());
                }
                else
                {
                    p = "Class " + pol.ToString();
                    comboBox6.Items.Add("Class " + pol + " : " + da[2].ToString() + " - " + da[1].ToString());
                }
            }
            con.Close();

        }
        private void btn_del_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\20BSIT019\SEM 5\C# and .Net\Case study\3\Faculty_Helper\Faculty_Helper.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            string q = "delete from " + comboBox5.SelectedItem.ToString() +  " where class_id=@class and too=@ito and fromm=@ifrom ";
            String[] strlist = comboBox6.SelectedItem.ToString().Split(' ');
            int temp = Convert.ToInt32(strlist[1]);
            if(strlist[0].Equals("Lab"))
            {
                 temp = temp + 8;
            }
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@class", temp);
            cmd.Parameters.AddWithValue("@ifrom", strlist[3]);
            cmd.Parameters.AddWithValue("@ito", strlist[5]);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Deleted!!!!");
            }
            else
            {
                MessageBox.Show("Not Deleted!!!");
            }
            comboBox6.Items.Clear();
            comboBox6.SelectedText="";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fm = new Form1();
            fm.Show();
        }
    }
 }
