using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace GYM
{
    public partial class Form2 : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        DateTime dt;
        Class1 connection = new Class1();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string format;
        FileStream fs;
        byte[] barimg;
        string strimg;
        CustomButton button1;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (Class1.Table == "Member" && checkBox1.Checked)
                {
                    dt = DateTime.Now;
                    format = "dd MM yyyy";
                    format = dt.ToString(format);
                    cmd = new SqlCommand("insert into [Member Fee] values ('" + Class1.ID + "',@pay)", connection.Conn());
                    cmd.Parameters.AddWithValue("@pay", format);
                    cmd.ExecuteNonQuery();
                    this.Hide();
                }
                if (Class1.Table == "Employee" && checkBox1.Checked)
                {
                    dt = DateTime.Now;
                    format = "dd MM yyyy";
                    format = dt.ToString(format);
                    cmd = new SqlCommand("insert into [Employee Fee] values ('" + Class1.ID + "',@pay)", connection.Conn());
                    cmd.Parameters.AddWithValue("@pay", format);
                    cmd.ExecuteNonQuery();
                    this.Hide();
                }
                else this.Hide();
            }
            catch (Exception)
            { MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { connection.Close(); }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                button1 = new CustomButton();
                button1.Text = "Save";
                button1.Size = new Size(75, 23);
                button1.FlatStyle = FlatStyle.Flat;
                button1.DialogResult = DialogResult.OK;
                button1.Location = new Point(222, 423);
                button1.Click += button1_Click;
                this.Controls.Add(button1);
                connection.Open();
                if (Class1.Table == "Machine")
                {
                    linkLabel1.Visible = false;
                    dataGridView1.Visible = false;
                    cmd = new SqlCommand("select Picture from [Machine Record] where ID = @id", connection.Conn());
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);

                    cmd.Parameters["@id"].Value = Class1.ID;
                    barimg = (byte[])cmd.ExecuteScalar();
                    strimg = Convert.ToString(DateTime.Now.ToFileTime());
                    fs = new FileStream(strimg, FileMode.CreateNew, FileAccess.Write);
                    fs.Write(barimg, 0, barimg.Length);
                    fs.Flush();
                    fs.Close();
                    pictureBox1.Image = Image.FromFile(strimg);

                    Class1.Query = "select * from [Machine Record] where ID = \'" + Class1.ID + "\' ";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    label1.Text = reader["ID"].ToString();
                    label1.Visible = true;
                    label2.Text = reader["Name"].ToString();
                    label2.Visible = true;
                    label3.Text = reader["Code"].ToString();
                    label3.Visible = true;
                    label4.Text = reader["Cost"].ToString();
                    label4.Visible = true;
                    label5.Text = reader["Seller"].ToString();
                    label5.Visible = true;
                    label6.Text = reader["Reciept No"].ToString();
                    label6.Visible = true; ;
                    label7.Text = reader["Exercise"].ToString();
                    label7.Visible = true;
                    label8.Text = reader["Date"].ToString();
                    reader.Close();
                    checkBox1.Visible = false;
                    label8.Visible = true;
                    label9.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = true;
                    label15.Visible = true;
                    label15.Text = "ID";
                    label17.Text = "Code";
                    label18.Text = "Cost";
                    label19.Text = "Seller";
                    label20.Text = "Reciept No";
                    label21.Text = "Exercise";
                    label22.Text = "Date";

                    label17.Visible = true;
                    label18.Visible = true;
                    label19.Visible = true;
                    label20.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;
                    label23.Visible = false;
                    label24.Visible = false;
                    label25.Visible = false;
                    label26.Visible = false;
                    label27.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                }
                if (Class1.Table == "Member")
                {
                    linkLabel1.Visible = true;
                    cmd = new SqlCommand("select Picture from [Member Record] where ID = @id", connection.Conn());
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);

                    cmd.Parameters["@id"].Value = Class1.ID;
                    barimg = (byte[])cmd.ExecuteScalar();
                    strimg = Convert.ToString(DateTime.Now.ToFileTime());
                    fs = new FileStream(strimg, FileMode.CreateNew, FileAccess.Write);
                    fs.Write(barimg, 0, barimg.Length);
                    fs.Flush();
                    fs.Close();
                    pictureBox1.Image = Image.FromFile(strimg);

                    Class1.Query = "select * from [Member Record] where [ID] = \'" + Class1.ID + "\'";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    label1.Text = reader["ID"].ToString();
                    checkBox1.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    label17.Visible = true;
                    label18.Visible = true;
                    label19.Visible = true;
                    label20.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;
                    label23.Visible = true;
                    label24.Visible = true;
                    label25.Visible = true;
                    label26.Visible = true;
                    label27.Visible = true;
                    label28.Visible = true;
                    label29.Visible = false;
                    label30.Visible = false;
                    label15.Text = "ID";
                    label17.Text = "Father Name";
                    label18.Text = "CNIC";
                    label19.Text = "Contact No";
                    label20.Text = "Adress";
                    label21.Text = "Marital Status";
                    label22.Text = "Gender";
                    label23.Text = "Age";
                    label24.Text = "Height";
                    label25.Text = "Weight";
                    label26.Text = "BMI";
                    label27.Text = "Admission Date";
                    label2.Text = reader["Name"].ToString();
                    label3.Text = reader["Father Name"].ToString();
                    label4.Text = reader["CNIC"].ToString();
                    label5.Text = reader["Contact No"].ToString();
                    label6.Text = reader["Adress"].ToString();
                    label7.Text = reader["Marital Status"].ToString();
                    label8.Text = reader["Gender"].ToString();
                    label9.Text = reader["Age"].ToString();
                    label10.Text = reader["Height"].ToString();
                    label11.Text = reader["Weight"].ToString();
                    label12.Text = reader["BMI"].ToString();
                    label13.Text = reader["Date"].ToString();
                    reader.Close();
                    Class1.Query = "select top 1 [Date Of Pay] from [Member Fee] where ID = '" + Class1.ID + "' order by [Date Of Pay] asc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    label28.Text = "Last Pay On";
                    label14.Text = reader["Date Of Pay"].ToString();
                    reader.Close();
                }
                if (Class1.Table == "Employee")
                {
                    linkLabel1.Visible = true;
                    cmd = new SqlCommand("select Picture from [Employee Record] where ID = @id", connection.Conn());
                    cmd.Parameters.Add("@id", SqlDbType.Int, 4);

                    cmd.Parameters["@id"].Value = Class1.ID;
                    barimg = (byte[])cmd.ExecuteScalar();
                    strimg = Convert.ToString(DateTime.Now.ToFileTime());
                    fs = new FileStream(strimg, FileMode.CreateNew, FileAccess.Write);
                    fs.Write(barimg, 0, barimg.Length);
                    fs.Flush();
                    fs.Close();
                    pictureBox1.Image = Image.FromFile(strimg);

                    Class1.Query = "select * from [Employee Record] where [ID] = \'" + Class1.ID + "\'";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    label1.Text = reader["ID"].ToString();
                    label2.Text = reader["Name"].ToString();
                    label3.Text = reader["Father Name"].ToString();
                    label4.Text = reader["CNIC"].ToString();
                    label5.Text = reader["Contact No"].ToString();
                    label6.Text = reader["Adress"].ToString();
                    label7.Text = reader["Marital Status"].ToString();
                    label8.Text = reader["Gender"].ToString();
                    label9.Text = reader["Age"].ToString();
                    label10.Text = reader["Designation"].ToString();
                    label11.Text = reader["Salary"].ToString();
                    label12.Text = reader["Start Time"].ToString();
                    label13.Text = reader["End Time"].ToString();
                    label14.Text = reader["Date"].ToString();
                    reader.Close();
                    Class1.Query = "select top 1 [Date Of Pay] from [Employee Fee] where ID = '" + Class1.ID + "' order by [Date Of Pay] asc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    label29.Text = "Last Pay On";
                    label30.Text = reader["Date Of Pay"].ToString();
                    label30.Visible = true;
                    reader.Close();
                    checkBox1.Visible = true;
                    label15.Visible = true;
                    label15.Text = "ID";
                    label17.Text = "Father Name";
                    label18.Text = "CNIC";
                    label19.Text = "Contact No";
                    label20.Text = "Adress";
                    label21.Text = "Marital Status";
                    label22.Text = "Gender";
                    label23.Text = "Age";
                    label24.Text = "Designation";
                    label25.Text = "Salary";
                    label26.Text = "Start Time";
                    label27.Text = "End Time";
                    label28.Text = "Admission Date";
                    label29.Visible = true;
                    label17.Visible = true;
                    label18.Visible = true;
                    label19.Visible = true;
                    label20.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;
                    label23.Visible = true;
                    label24.Visible = true;
                    label25.Visible = true;
                    label26.Visible = true;
                    label27.Visible = true;
                    label28.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        SqlDataAdapter a;
        DataTable data = new DataTable();

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                dataGridView1.Visible = true;
                connection.Open();
                if (Class1.Table == "Member")
                {
                    a = new SqlDataAdapter("select * from [Member Fee] where ID = '" + Class1.ID + "' order by [Date Of Pay] asc", connection.Conn());
                    data = new DataTable();
                    a.Fill(data);
                    dataGridView1.DataSource = data;
                }
                if (Class1.Table == "Employee")
                {
                    a = new SqlDataAdapter("select * from [Employee Fee] where ID = '" + Class1.ID + "' order by [Date Of Pay] asc", connection.Conn());
                    data = new DataTable();
                    a.Fill(data);
                    dataGridView1.DataSource = data;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }
    }
}