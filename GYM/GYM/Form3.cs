using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;

namespace GYM
{
    public partial class Form3 : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        Class1 connection = new Class1();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                Class1.Query = "Select Admin_name,Password from Login_Page";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                reader = cmd.ExecuteReader();
                reader.Read();


                if (comboBox1.SelectedItem.ToString() == reader["Admin_name"].ToString() && textBox2.Text == reader["Password"].ToString())
                {
                    reader.Close();
                    //Class1.Query = "update [Admin] set [Login] = 1";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    cmd.ExecuteNonQuery();
                    this.Hide();
                    Form1 frm = new Form1();
                    frm.Show();

                }
                else MessageBox.Show("Incorrect user name or password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            { MessageBox.Show("Please check again", "Message"); }
            finally { connection.Close(); }
        }


        CustomButton button1;

        private void Form3_Load(object sender, EventArgs e)
        {
            button1 = new CustomButton();
            button1.Text = "Login";
            button1.FlatStyle = FlatStyle.Flat;
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(113, 99);
            button1.Size = new Size(75, 23);
            button1.Click += button1_Click;
            this.Controls.Add(button1);

            try
            {
                connection.Open();
                Class1.Query = "Select Admin_name from Login_Page";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                { comboBox1.Items.Add(reader[0].ToString()); }
                connection.Close();
            }
            catch { ;}
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }
    }
}