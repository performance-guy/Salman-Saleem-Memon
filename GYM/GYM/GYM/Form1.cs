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
using ZedGraph;
using ComponentFactory.Krypton.Toolkit;

namespace GYM
{
    public partial class Form1 : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        Class1 connection = new Class1();
        SqlCommand cmd;
        SqlDataReader reader;
        int temp;
        sbyte i;
        string[] name = new string[5];
        string[] fathername = new string[5];
        byte[] id = new byte[5];
        OpenFileDialog Opendialog = new OpenFileDialog();
        string Sfeet, Sinch;
        float Iinch, Ifeet, meter, bmi;
        DateTime dt;
        string format = "dd MM yyyy";
        string allowedCharacterSet = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM.- \u007f\u0008";
        Image img;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Sfeet = maskedTextBox3.Text[0].ToString() + maskedTextBox3.Text[1].ToString();
                Sinch = maskedTextBox3.Text[3].ToString() + maskedTextBox3.Text[4].ToString();
                Ifeet = float.Parse(Sfeet);
                Iinch = float.Parse(Sinch) + (Ifeet * 12);
                meter = (float)Iinch / 12;
                meter = meter / (float)3.2;
                meter = meter * meter;
                bmi = float.Parse(textBox4.Text) / meter;
                bmi = (float)Math.Round(bmi, 1);
                textBox5.Text = bmi.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Opendialog.Filter = "(.jpg .png .gif .ico .bmp)|*.jpg;*.bmp;*.png;*.ico;*.gif|All Files|*.*";
            if (Opendialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(Opendialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add member?", "Add Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dt = DateTime.Now;
                    format = dt.ToString(format);
                    temp = connection.Select_Last_Member();
                    temp++;
                    Class1.Query = "insert into [Member Record] (Age,ID,Name,[Father Name],CNIC,Adress,[Contact No],[Marital Status],Gender,Weight,Height,BMI,[Date]) values(@age,@id,@name,@fathername,@cnic,@adress,@contact,@marital,@gender,@weight,@height,@bmi,@date)";
                    connection.Open();
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@id", temp);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@fathername", textBox2.Text);
                    cmd.Parameters.AddWithValue("@cnic", maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@adress", textBox3.Text);
                    cmd.Parameters.AddWithValue("@contact", maskedTextBox2.Text);
                    cmd.Parameters.AddWithValue("@marital", (radioButton1.Checked ? radioButton1.Text : radioButton2.Text));
                    cmd.Parameters.AddWithValue("@gender", (radioButton3.Checked ? radioButton3.Text : radioButton4.Text));
                    cmd.Parameters.AddWithValue("@weight", float.Parse(textBox4.Text));
                    cmd.Parameters.AddWithValue("@height", maskedTextBox3.Text);
                    cmd.Parameters.AddWithValue("@bmi", float.Parse(textBox5.Text));
                    cmd.Parameters.AddWithValue("@date", format);
                    cmd.ExecuteNonQuery();

                    Class1.Query = "insert into [Member Fee] values (@eyed,@dt)";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.AddWithValue("@dt", format);
                    cmd.Parameters.AddWithValue("@eyed", temp);
                    cmd.ExecuteNonQuery();

                    Class1.Query = "update Admin set [Last Member]=@lastmember";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.AddWithValue("@lastmember", temp);
                    cmd.ExecuteNonQuery();

                    FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                    byte[] picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));

                    SqlParameter para = new SqlParameter();
                    para.SqlDbType = SqlDbType.Image;
                    para.ParameterName = "pic";
                    para.Value = picbyte;
                    Class1.Query = "update [Member Record] set Picture=@pic where ID='" + temp + "'";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Member added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Class1.Table = "Member";
                    Class1.ID = (byte)temp;
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                    if (float.Parse(textBox5.Text) < 25)
                    {
                        img = Image.FromFile("LESS_THEN_25.jpg");
                        printDocument1.Print();
                    }
                    if (float.Parse(textBox5.Text) == 25)
                    {
                        img = Image.FromFile("EQUAL_25.jpg");
                        printDocument1.Print();
                    }
                    if (float.Parse(textBox5.Text) > 25)
                    {
                        img = Image.FromFile("GREATER_THEN_25.jpg");
                        printDocument1.Print();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please check again" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Opendialog.Filter = "(.jpg .png .gif .ico .bmp)|*.jpg;*.bmp;*.png;*.ico;*.gif|All Files|*.*";
            if (Opendialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(Opendialog.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add employee?", "Add Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dt = DateTime.Now;
                    format = dt.ToString(format);
                    temp = connection.Select_Last_Employee();
                    temp++;
                    Class1.Query = "insert into [Employee Record] (Age,ID,Name,[Father Name],CNIC,Adress,[Contact No],[Marital Status],Gender,Designation,Salary,[Start Time],[End Time],[Date]) values(@age,@id,@name,@fathername,@cnic,@adress,@contact,@marital,@gender,@designation,@salary,@start,@end,@date)";
                    connection.Open();
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    cmd.Parameters.AddWithValue("@age", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@id", temp);
                    cmd.Parameters.AddWithValue("@name", textBox8.Text);
                    cmd.Parameters.AddWithValue("@fathername", textBox7.Text);
                    cmd.Parameters.AddWithValue("@cnic", maskedTextBox5.Text);
                    cmd.Parameters.AddWithValue("@adress", textBox6.Text);
                    cmd.Parameters.AddWithValue("@contact", maskedTextBox4.Text);
                    cmd.Parameters.AddWithValue("@marital", (radioButton8.Checked ? radioButton8.Text : radioButton7.Text));
                    cmd.Parameters.AddWithValue("@gender", (radioButton6.Checked ? radioButton6.Text : radioButton5.Text));
                    cmd.Parameters.AddWithValue("@designation", (textBox9.Text));
                    cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(textBox10.Text));
                    cmd.Parameters.AddWithValue("@start", dateTimePicker1.Value.ToShortTimeString());
                    cmd.Parameters.AddWithValue("@end", dateTimePicker2.Value.ToShortTimeString());
                    cmd.Parameters.AddWithValue("@date", format);
                    cmd.ExecuteNonQuery();

                    Class1.Query = "insert into [Employee Fee] values (@eyed,@dt)";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.AddWithValue("@dt", format);
                    cmd.Parameters.AddWithValue("@eyed", temp);
                    cmd.ExecuteNonQuery();

                    Class1.Query = "update Admin set [Last Employee]=@lastemployee";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.AddWithValue("@lastemployee", temp);
                    cmd.ExecuteNonQuery();

                    FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                    byte[] picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));
                    SqlParameter para = new SqlParameter();
                    para.SqlDbType = SqlDbType.Image;
                    para.ParameterName = "pic";
                    para.Value = picbyte;
                    Class1.Query = "update [Employee Record] set Picture=@pic where ID='" + temp + "'";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Employee added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Class1.Table = "Employee";
                    Class1.ID = (byte)temp;
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Opendialog.Filter = "(.jpg .png .gif .ico .bmp)|*.jpg;*.bmp;*.png;*.ico;*.gif|All Files|*.*";
            if (Opendialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(Opendialog.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add machine?", "Add Machine", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dt = DateTime.Now;
                    format = dt.ToString(format);
                    temp = connection.Select_Last_Machine();
                    temp++;
                    Class1.Query = "insert into [Machine Record] (ID,Name,Exercise,Code,Cost,Seller,[Reciept No],Date) values(@id,@name,@exercise,@code,@cost,@seller,@reciept,@date)";
                    connection.Open();
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    cmd.Parameters.AddWithValue("@code", textBox13.Text);
                    cmd.Parameters.AddWithValue("@id", temp);
                    cmd.Parameters.AddWithValue("@name", textBox12.Text);
                    cmd.Parameters.AddWithValue("@exercise", textBox11.Text);
                    cmd.Parameters.AddWithValue("@cost", textBox15.Text);
                    cmd.Parameters.AddWithValue("@seller", textBox14.Text);
                    cmd.Parameters.AddWithValue("@reciept", textBox13.Text);
                    cmd.Parameters.AddWithValue("@date", format);
                    cmd.ExecuteNonQuery();

                    Class1.Query = "update Admin set [Last Machine]=@lastmachine";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.AddWithValue("@lastmachine", temp);
                    cmd.ExecuteNonQuery();

                    FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                    byte[] picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));
                    SqlParameter para = new SqlParameter();
                    para.SqlDbType = SqlDbType.Image;
                    para.ParameterName = "pic";
                    para.Value = picbyte;
                    Class1.Query = "update [Machine Record] set Picture=@pic where ID='" + temp + "'";
                    cmd.CommandText = Class1.Query;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Machine added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Class1.Table = "Machine";
                    Class1.ID = (byte)temp;
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            linkLabel7.Visible = false;
            i = 0;
            temp = 0;
            try
            {
                timer1.Stop();
                connection.Open();
                if (comboBox1.SelectedItem.ToString() == "ID" && comboBox2.SelectedItem.ToString() == "Employee")
                {
                    linkLabel1.Visible = false;
                    linkLabel2.Visible = false;
                    linkLabel3.Visible = false;
                    linkLabel4.Visible = false;
                    linkLabel5.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label32.Visible = false;
                    label33.Visible = false;
                    label34.Visible = false;
                    Class1.Query = "select * from [Employee Record] where ID = '" + textBox16.Text + "'";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        i++;
                    if (i != 0)
                    {

                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        Class1.ID = Convert.ToByte(textBox16.Text);
                        Class1.Table = "Employee";
                        Form2 frm = new Form2();
                        frm.ShowDialog();
                    }
                    else { label41.Visible = true; label41.Location = new Point(275, 100); label41.Text = "No employee found"; timer1.Start(); timer1.Interval = 500; }
                }
                if (comboBox1.SelectedItem.ToString() == "ID" && comboBox2.SelectedItem.ToString() == "Member")
                {
                    linkLabel1.Visible = false;
                    linkLabel2.Visible = false;
                    linkLabel3.Visible = false;
                    linkLabel4.Visible = false;
                    linkLabel5.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label32.Visible = false;
                    label33.Visible = false;
                    label34.Visible = false;
                    Class1.Query = "select * from [Member Record] where ID = '" + textBox16.Text + "'";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        i++;
                    if (i != 0)
                    {
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        timer1.Stop();
                        Class1.ID = Convert.ToByte(textBox16.Text);
                        Class1.Table = "Member";
                        Form2 frm = new Form2();
                        frm.ShowDialog();
                    }
                    else { label41.Visible = true; label41.Location = new Point(275, 100); label41.Text = "No member found"; timer1.Start(); timer1.Interval = 500; }
                }
                if (comboBox1.SelectedItem.ToString() == "ID" && comboBox2.SelectedItem.ToString() == "Machine")
                {
                    linkLabel1.Visible = false;
                    linkLabel2.Visible = false;
                    linkLabel3.Visible = false;
                    linkLabel4.Visible = false;
                    linkLabel5.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label32.Visible = false;
                    label33.Visible = false;
                    label34.Visible = false;
                    Class1.Query = "select * from [Machine Record] where ID = '" + textBox16.Text + "'";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        i++;
                    if (i != 0)
                    {
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        timer1.Stop();
                        Class1.ID = Convert.ToByte(textBox16.Text);
                        Class1.Table = "Machine";
                        Form2 frm = new Form2();
                        frm.ShowDialog();
                    }
                    else { label41.Visible = true; label41.Location = new Point(275, 100); label41.Text = "No machine found"; timer1.Start(); timer1.Interval = 500; }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Employee")
                {
                    Class1.Query = "select * from [Employee Record] where [Name] like \'%" + textBox16.Text + "%\' ";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    reader.Close();
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else linkLabel6.Visible = false;
                    if (i == 0)
                    {
                        linkLabel1.Visible = false;
                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = false;
                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                        label41.Visible = true; label41.Location = new Point(275, 100);
                        label41.Text = "No employee found"; timer1.Start(); timer1.Interval = 500;
                    }
                    if (i == 1)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label30.Visible = true;

                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 2)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label30.Visible = true;
                        label31.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 3)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 4)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label31.Text = fathername[2];
                        label33.Text = fathername[3];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        linkLabel5.Visible = false;

                        label34.Visible = false;
                    }
                    else if (i == 5)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.Query = "select * from [Machine Record] where [Name] like \'%" + textBox16.Text + "%\' ";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Code"].ToString();
                            fathername[i] = reader["Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    reader.Close();
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else linkLabel6.Visible = false;
                    if (i == 0)
                    {
                        linkLabel1.Visible = false;
                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = false;
                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false; label41.Location = new Point(275, 100);
                        label41.Visible = true; label41.Text = "No machine found"; timer1.Start(); timer1.Interval = 500;
                    }
                    if (i == 1)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;
                        label30.Text = fathername[0];
                        label30.Visible = true;
                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 2)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 3)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 4)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        linkLabel5.Visible = false;
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;

                        label34.Visible = false;
                    }
                    else if (i == 5)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.Query = "select * from [Member Record] where [Name] like \'%" + textBox16.Text + "%\' order by ID asc ";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    reader.Close();
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else linkLabel6.Visible = false;
                    if (i == 0)
                    {
                        linkLabel1.Visible = false;
                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = false;
                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false; label41.Location = new Point(275, 100);
                        label41.Visible = true; label41.Text = "No member found"; timer1.Start(); timer1.Interval = 500;
                    }
                    if (i == 1)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;
                        label30.Text = "";
                        label30.Visible = true;

                        label30.Text = fathername[0];
                        label30.Visible = true;
                        linkLabel2.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;

                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 2)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 3)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 4)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        linkLabel5.Visible = false;
                        label34.Visible = false;
                    }
                    else if (i == 5)
                    {
                        timer1.Stop();
                        label41.Visible = false;
                        label41.Location = new Point(275, 100);
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                label41.Visible = false;
                label41.Location = new Point(275, 100);
                MessageBox.Show("Please check again ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { connection.Close(); }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel7.Text = "Previous";
            linkLabel7.Visible = true;
            temp = 0;
            i = 0;
            try
            {
                connection.Open();
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.Query = "select * from [Member Record] where ID > \'" + id[4] + "\' and name like \'%" + textBox16.Text + "%\' order by ID asc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    reader.Close();
                    if (i == 1)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;
                        label30.Text = "";
                        label30.Visible = true;

                        label30.Text = fathername[0];
                        label30.Visible = true;

                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;


                        linkLabel2.Visible = false;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 2)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label30.Visible = true;
                        label31.Visible = true;

                        label32.Visible = false;
                        label33.Visible = false;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 3)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;

                        label33.Visible = false;
                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 4)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 5)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else
                    {
                        linkLabel6.Visible = false;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Employee")
                {
                    Class1.Query = "select * from [Employee Record] where ID > \'" + id[4] + "\' and name like \'%" + textBox16.Text + "%\' order by ID asc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    if (i == 1)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;
                        label30.Text = "";
                        label30.Visible = true;

                        label30.Text = fathername[0];
                        label30.Visible = true;

                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;


                        linkLabel2.Visible = false;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 2)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label30.Visible = true;
                        label31.Visible = true;

                        label32.Visible = false;
                        label33.Visible = false;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 3)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;

                        label33.Visible = false;
                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 4)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 5)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else
                    {
                        linkLabel6.Visible = false;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.Query = "select * from [Machine Record] where ID > \'" + id[4] + "\' and name like \'%" + textBox16.Text + "%\' order by ID asc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i < 5)
                        {
                            name[i] = reader["Code"].ToString();
                            fathername[i] = reader["Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i++;
                        }
                        temp++;
                    }
                    reader.Close();
                    if (i == 1)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel1.Visible = true;
                        label30.Text = "";
                        label30.Visible = true;

                        label30.Text = fathername[0];
                        label30.Visible = true;

                        label31.Visible = false;
                        label32.Visible = false;
                        label33.Visible = false;
                        label34.Visible = false;


                        linkLabel2.Visible = false;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 2)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label30.Visible = true;
                        label31.Visible = true;

                        label32.Visible = false;
                        label33.Visible = false;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = false;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 3)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel3.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;

                        label33.Visible = false;
                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = false;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 4)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;

                        label34.Visible = false;

                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;
                        linkLabel5.Visible = false;
                    }
                    else if (i == 5)
                    {
                        linkLabel1.Text = name[0];
                        linkLabel2.Text = name[1];
                        linkLabel2.Visible = true;
                        linkLabel1.Visible = true;

                        linkLabel3.Text = name[2];
                        linkLabel4.Text = name[3];
                        linkLabel3.Visible = true;
                        linkLabel4.Visible = true;

                        linkLabel5.Text = name[4];
                        linkLabel5.Visible = true;
                        label30.Text = fathername[0];
                        label31.Text = fathername[1];
                        label32.Text = fathername[2];
                        label33.Text = fathername[3];
                        label34.Text = fathername[4];
                        label30.Visible = true;
                        label31.Visible = true;
                        label32.Visible = true;
                        label33.Visible = true;
                        label34.Visible = true;
                    }
                    if (temp > 5)
                    {
                        linkLabel6.Text = "Next";
                        linkLabel6.Visible = true;
                    }
                    else
                    {
                        linkLabel6.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                label41.Visible = false;
                label41.Location = new Point(275, 100);
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedCharacterSet.Contains(e.KeyChar.ToString())) { }
            else e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedCharacterSet.Contains(e.KeyChar.ToString())) { }
            else e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedCharacterSet.Contains(e.KeyChar.ToString())) { }
            else e.Handled = true;
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedCharacterSet.Contains(e.KeyChar.ToString())) { }
            else e.Handled = true;
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (allowedCharacterSet.Contains(e.KeyChar.ToString())) { }
            else e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.Equals("\u0008", e.KeyChar) || char.Equals("\u007f", e.KeyChar)) { }
            else e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.Equals("\u0008", e.KeyChar) || char.Equals("\u007f", e.KeyChar)) { }
            else e.Handled = true;
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.Equals("\u0008", e.KeyChar) || char.Equals("\u007f", e.KeyChar)) { }
            else e.Handled = true;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel6.Text = "Next";
            linkLabel6.Visible = true;
            temp = 1;
            i = 4;

            try
            {
                connection.Open();
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.Query = "select top 5 * from [Member Record] where ID < \'" + id[0] + "\' and name like \'%" + textBox16.Text + "%\' order by ID desc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i >= 0)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i--;
                        }
                        temp++;
                    }
                    reader.Close();
                    linkLabel1.Text = name[0];
                    linkLabel2.Text = name[1];
                    linkLabel2.Visible = true;
                    linkLabel1.Visible = true;

                    linkLabel3.Text = name[2];
                    linkLabel4.Text = name[3];
                    linkLabel3.Visible = true;
                    linkLabel4.Visible = true;

                    linkLabel5.Text = name[4];
                    linkLabel5.Visible = true;
                    label30.Text = fathername[0];
                    label31.Text = fathername[1];
                    label32.Text = fathername[2];
                    label33.Text = fathername[3];
                    label34.Text = fathername[4];
                    label30.Visible = true;
                    label31.Visible = true;
                    label32.Visible = true;
                    label33.Visible = true;
                    label34.Visible = true;

                    if (temp > 5)
                    {
                        linkLabel7.Text = "Previous";
                        linkLabel7.Visible = true;
                    }
                    else
                    {
                        linkLabel7.Visible = false;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Employee")
                {
                    Class1.Query = "select top 5 * from [Employee Record] where ID < \'" + id[0] + "\' and name like \'%" + textBox16.Text + "%\' order by ID desc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i >= 0)
                        {
                            name[i] = reader["Name"].ToString();
                            fathername[i] = reader["Father Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i--;
                        }
                        temp++;
                    }
                    reader.Close();
                    linkLabel1.Text = name[0];
                    linkLabel2.Text = name[1];
                    linkLabel2.Visible = true;
                    linkLabel1.Visible = true;

                    linkLabel3.Text = name[2];
                    linkLabel4.Text = name[3];
                    linkLabel3.Visible = true;
                    linkLabel4.Visible = true;

                    linkLabel5.Text = name[4];
                    linkLabel5.Visible = true;
                    label30.Text = fathername[0];
                    label31.Text = fathername[1];
                    label32.Text = fathername[2];
                    label33.Text = fathername[3];
                    label34.Text = fathername[4];
                    label30.Visible = true;
                    label31.Visible = true;
                    label32.Visible = true;
                    label33.Visible = true;
                    label34.Visible = true;

                    if (temp > 5)
                    {
                        linkLabel7.Text = "Previous";
                        linkLabel7.Visible = true;
                    }
                    else
                    {
                        linkLabel7.Visible = false;
                    }
                }
                if (comboBox1.SelectedItem.ToString() == "Name" && comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.Query = "select top 5 * from [Machine Record] where ID < \'" + id[0] + "\' and name like \'%" + textBox16.Text + "%\' order by ID desc";
                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (i >= 0)
                        {
                            name[i] = reader["Code"].ToString();
                            fathername[i] = reader["Name"].ToString();
                            id[i] = Convert.ToByte(reader["ID"]);
                            i--;
                        }
                        temp++;
                    }
                    reader.Close();
                    linkLabel1.Text = name[0];
                    linkLabel2.Text = name[1];
                    linkLabel2.Visible = true;
                    linkLabel1.Visible = true;

                    linkLabel3.Text = name[2];
                    linkLabel4.Text = name[3];
                    linkLabel3.Visible = true;
                    linkLabel4.Visible = true;

                    linkLabel5.Text = name[4];
                    linkLabel5.Visible = true;
                    label30.Text = fathername[0];
                    label31.Text = fathername[1];
                    label32.Text = fathername[2];
                    label33.Text = fathername[3];
                    label34.Text = fathername[4];
                    label30.Visible = true;
                    label31.Visible = true;
                    label32.Visible = true;
                    label33.Visible = true;
                    label34.Visible = true;

                    if (temp > 5)
                    {
                        linkLabel7.Text = "Previous";
                        linkLabel7.Visible = true;
                    }
                    else
                    {
                        linkLabel7.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                label41.Visible = false;
                label41.Location = new Point(275, 100);
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                if (comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.ID = Convert.ToByte(id[0]);
                    Class1.Table = "Member";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Employee")
                {

                    Class1.ID = Convert.ToByte(id[0]);
                    Class1.Table = "Employee";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.ID = Convert.ToByte(id[0]);
                    Class1.Table = "Machine";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                if (comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.ID = Convert.ToByte(id[1]);
                    Class1.Table = "Member";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Employee")
                {

                    Class1.ID = Convert.ToByte(id[1]);
                    Class1.Table = "Employee";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.ID = Convert.ToByte(id[1]);
                    Class1.Table = "Machine";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again" + ex.TargetSite + ex.StackTrace, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                if (comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.ID = Convert.ToByte(id[2]);
                    Class1.Table = "Member";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Employee")
                {

                    Class1.ID = Convert.ToByte(id[2]);
                    Class1.Table = "Employee";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.ID = Convert.ToByte(id[2]);
                    Class1.Table = "Machine";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                connection.Open();
                if (comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.ID = Convert.ToByte(id[3]);
                    Class1.Table = "Member";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Employee")
                {

                    Class1.ID = Convert.ToByte(id[3]);
                    Class1.Table = "Employee";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.ID = Convert.ToByte(id[3]);
                    Class1.Table = "Machine";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                if (comboBox2.SelectedItem.ToString() == "Member")
                {
                    Class1.ID = Convert.ToByte(id[4]);
                    Class1.Table = "Member";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Employee")
                {

                    Class1.ID = Convert.ToByte(id[4]);
                    Class1.Table = "Employee";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
                if (comboBox2.SelectedItem.ToString() == "Machine")
                {
                    Class1.ID = Convert.ToByte(id[4]);
                    Class1.Table = "Machine";
                    Form2 frm = new Form2();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        { }

        private void button9_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button9.PerformClick();
                button15.PerformClick();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Point location = new Point(-300, 10);
            e.Graphics.DrawImage(img, location);
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void Setsize()
        {
            zedGraphControl1.Location = new Point(125, 50);
            zedGraphControl1.Size = new Size(450, 315);
            CreatGraph(zedGraphControl1);
        }

        GraphPane Pane;
        LineItem Curve;
        PointPairList List;

        private void CreatGraph(ZedGraphControl z)
        {
            Pane = new GraphPane();
            Pane = z.GraphPane;
            Pane.Title.Text = "LAST MONTH ACTIVITY";
            Pane.XAxis.Title.Text = "MEMBERS";
            Pane.YAxis.Title.Text = "MONTHS";
            List = new PointPairList();
            List.Clear();
            List.Add(0, 0);
            List.Add(i, 1);
            Curve = new LineItem("");
            Pane.CurveList.Clear();
            Curve = Pane.AddCurve("Member", List, Color.Black, SymbolType.None);
            Curve.Line.Width = 03;
            Pane.Chart.Border.Color = Color.Black;
            Pane.Chart.Border.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            Pane.Fill.Type = ZedGraph.FillType.None;
            Pane.Fill.AlignH = ZedGraph.AlignH.Right;
            Pane.Fill.AlignV = ZedGraph.AlignV.Top;
            Pane.LineType = ZedGraph.LineType.Normal;
            Pane.Margin.All = 0;
            Pane.TitleGap = 0;
            Pane.XAxis.Color = Color.MediumBlue;
            Pane.X2Axis.Cross = 0;
            Pane.BaseDimension = 10;
            z.AxisChange();
        }

        private void button12_Click(object sender, EventArgs e)
        {
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        string smonth, sdate, sstring;
        int imonth;
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                i = 0;
                dt = DateTime.Now;
                imonth = dt.AddMonths(-1).Month;
                Class1.Query = "select * from [Member Record]";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sstring = reader["Date"].ToString();
                    sdate = sstring[0].ToString();
                    sdate += sstring[1];
                    smonth = sstring[3].ToString();
                    smonth += sstring[4];
                    if (imonth < Convert.ToInt32(smonth) || (imonth == Convert.ToInt32(smonth) && Convert.ToInt32(sdate) >= dt.Day))
                    {
                        i++;
                    }
                }
                reader.Close();
                Setsize();
            }
            catch (Exception ex)
            { MessageBox.Show("Please check again" + ex.Message + ex.TargetSite + ex.StackTrace, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { connection.Close(); }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.Query = "update [Admin] set Login = 0";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                cmd.ExecuteNonQuery();
                Application.Exit();
            }
            catch (Exception)
            { }
            finally { connection.Close(); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are sure you want to quit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        string str, str2;
        int[] v = new int[10];
        int j;
        int var;
        int var2;

        private void button9_Click_2(object sender, EventArgs e)
        {
            try
            {
                linkLabel14.Visible = false;
                linkLabel8.Visible = false;
                j = 0;
                i = 0;
                temp = 0;
                dt = DateTime.Now;
                format = "dd MM yyyy";
                format = dt.ToString(format);
                str = format[0].ToString() + format[1].ToString();
                var = Convert.ToInt32(str);
                connection.Open();
                Class1.Query = "select * from [Member Fee] order by ID ASC";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    str = format[6].ToString() + format[7].ToString() + format[8].ToString() + format[9].ToString();
                    var = Convert.ToInt32(str);
                    str = reader["Date Of Pay"].ToString();
                    str2 = str[6].ToString() + str[7].ToString() + str[8].ToString() + str[9].ToString();
                    var2 = Convert.ToInt32(str2);
                    if (var >= var2)
                    {
                        str = format[3].ToString() + format[4].ToString();
                        var = Convert.ToInt32(str);
                        str = reader["Date Of Pay"].ToString();
                        str2 = str[3].ToString() + str[4].ToString();
                        var2 = Convert.ToInt32(str2);
                        if (var > var2)
                        {
                            str = format[0].ToString() + format[1].ToString();
                            var = Convert.ToInt32(str);
                            str = reader["Date Of Pay"].ToString();
                            str2 = str[0].ToString() + str[1].ToString();
                            var2 = Convert.ToInt32(str2);
                            if (var >= var2)
                            {
                                if (i < 5)
                                {
                                    str = reader["ID"].ToString();
                                    id[i] = Convert.ToByte(str);
                                    i++;
                                }
                                temp++;
                            }
                        }
                    }
                }
                reader.Close();
                for (j = 0; j < 5; j++)
                {
                    cmd = new SqlCommand(@"select Name,[Father Name] from [Member Record] where ID='" + id[j] + "' order by ID ASC", connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        name[j] = reader["Name"].ToString();
                        fathername[j] = reader["Father Name"].ToString();
                    }
                    reader.Close();
                }
                if (i == 0)
                {
                    label42.Visible = true;
                    label42.Location = new Point(275, 100); label42.Text = "No overdue member found"; timer1.Start(); timer1.Interval = 500;
                }
                if (i == 1)
                {
                    label42.Visible = false;
                    label42.Location = new Point(275, 100);
                    timer1.Stop();
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel2.Visible = false;
                    linkLabel11.Visible = false;
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = false;
                    label37.Visible = false;
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 2)
                {
                    label42.Visible = false;
                    label42.Location = new Point(275, 100);
                    timer1.Stop();
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = false;
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = false;
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 3)
                {
                    label42.Visible = false;
                    label42.Location = new Point(275, 100);
                    timer1.Stop();
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 4)
                {
                    label42.Visible = false;
                    label42.Location = new Point(275, 100);
                    timer1.Stop();
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = true;
                    linkLabel10.Text = name[3];
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = true;
                    label36.Text = fathername[3];
                    label35.Visible = false;
                }
                else if (i == 5)
                {
                    label42.Visible = false;
                    label42.Location = new Point(275, 100);
                    timer1.Stop();
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = true;
                    linkLabel10.Text = name[3];
                    linkLabel9.Visible = true;
                    linkLabel9.Text = name[4];
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = true;
                    label36.Text = fathername[3];
                    label35.Visible = true;
                    label35.Text = fathername[4];
                }
                if (temp > 5)
                {
                    linkLabel14.Visible = true;
                }
            }
            catch (Exception ex)
            {
                label42.Visible = false;
                label42.Location = new Point(275, 100);
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel8.Visible = true;
                j = 0;
                i = 0;
                temp = 0;
                dt = DateTime.Now;
                format = "dd MM yyyy";
                format = dt.ToString(format);
                str = format[0].ToString() + format[1].ToString();
                var = Convert.ToInt32(str);
                connection.Open();
                Class1.Query = "select * from [Member Fee] where ID > '" + id[4] + "' order by ID ASC";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    str = format[6].ToString() + format[7].ToString() + format[8].ToString() + format[9].ToString();
                    var = Convert.ToInt32(str);
                    str = reader["Date Of Pay"].ToString();
                    str2 = str[6].ToString() + str[7].ToString() + str[8].ToString() + str[9].ToString();
                    var2 = Convert.ToInt32(str2);
                    if (var >= var2)
                    {
                        str = format[3].ToString() + format[4].ToString();
                        var = Convert.ToInt32(str);
                        str = reader["Date Of Pay"].ToString();
                        str2 = str[3].ToString() + str[4].ToString();
                        var2 = Convert.ToInt32(str2);
                        if (var > var2)
                        {
                            str = format[0].ToString() + format[1].ToString();
                            var = Convert.ToInt32(str);
                            str = reader["Date Of Pay"].ToString();
                            str2 = str[0].ToString() + str[1].ToString();
                            var2 = Convert.ToInt32(str2);
                            if (var >= var2)
                            {
                                if (i < 5)
                                {
                                    str = reader["ID"].ToString();
                                    id[i] = Convert.ToByte(str);
                                    i++;
                                }
                                temp++;
                            }
                        }
                    }
                }
                reader.Close();
                for (j = 0; j < 5; j++)
                {
                    cmd = new SqlCommand(@"select Name,[Father Name] from [Member Record] where ID='" + id[j] + "'", connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        name[j] = reader["Name"].ToString();
                        fathername[j] = reader["Father Name"].ToString();
                    }
                    reader.Close();
                }
                if (i == 1)
                {
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = false;
                    linkLabel11.Visible = false;
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = false;
                    label37.Visible = false;
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 2)
                {
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = false;
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = false;
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 3)
                {
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = false;
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = false;
                    label35.Visible = false;
                }
                else if (i == 4)
                {
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = true;
                    linkLabel10.Text = name[3];
                    linkLabel9.Visible = false;
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = true;
                    label36.Text = fathername[3];
                    label35.Visible = false;
                }
                else if (i == 5)
                {
                    linkLabel13.Visible = true;
                    linkLabel13.Text = name[0];
                    label39.Text = fathername[0];
                    label39.Visible = true;
                    linkLabel12.Visible = true;
                    linkLabel12.Text = name[1];
                    linkLabel11.Visible = true;
                    linkLabel11.Text = name[2];
                    linkLabel10.Visible = true;
                    linkLabel10.Text = name[3];
                    linkLabel9.Visible = true;
                    linkLabel9.Text = name[4];
                    label38.Visible = true;
                    label38.Text = fathername[1];
                    label37.Visible = true;
                    label37.Text = fathername[2];
                    label36.Visible = true;
                    label36.Text = fathername[3];
                    label35.Visible = true;
                    label35.Text = fathername[4];
                }
                if (temp > 5)
                {
                    linkLabel14.Visible = true;
                }
                else linkLabel14.Visible = false;
            }
            catch (System.Exception ex)
            {
                label42.Visible = false;
                label42.Location = new Point(275, 100);
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel14.Visible = true;
                j = 0;
                i = 4;
                temp = 0;
                dt = DateTime.Now;
                format = "dd MM yyyy";
                format = dt.ToString(format);
                str = format[0].ToString() + format[1].ToString();
                var = Convert.ToInt32(str);
                connection.Open();
                Class1.Query = "select * from [Member Fee] where ID < '" + id[0] + "' order by ID desc";
                cmd = new SqlCommand(Class1.Query, connection.Conn());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    str = format[6].ToString() + format[7].ToString() + format[8].ToString() + format[9].ToString();
                    var = Convert.ToInt32(str);
                    str = reader["Date Of Pay"].ToString();
                    str2 = str[6].ToString() + str[7].ToString() + str[8].ToString() + str[9].ToString();
                    var2 = Convert.ToInt32(str2);
                    if (var >= var2)
                    {
                        str = format[3].ToString() + format[4].ToString();
                        var = Convert.ToInt32(str);
                        str = reader["Date Of Pay"].ToString();
                        str2 = str[3].ToString() + str[4].ToString();
                        var2 = Convert.ToInt32(str2);
                        if (var > var2)
                        {
                            str = format[0].ToString() + format[1].ToString();
                            var = Convert.ToInt32(str);
                            str = reader["Date Of Pay"].ToString();
                            str2 = str[0].ToString() + str[1].ToString();
                            var2 = Convert.ToInt32(str2);
                            if (var >= var2)
                            {
                                if (i >= 0)
                                {
                                    str = reader["ID"].ToString();
                                    id[i] = Convert.ToByte(str);
                                    i--;
                                }
                                temp++;
                            }
                        }
                    }
                }
                reader.Close();
                for (j = 0; j < 5; j++)
                {
                    cmd = new SqlCommand("select Name,[Father Name] from [Member Record] where ID = '" + id[j] + "'", connection.Conn());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        name[j] = reader["Name"].ToString();
                        fathername[j] = reader["Father Name"].ToString();
                    }
                    reader.Close();
                }
                linkLabel13.Visible = true;
                linkLabel13.Text = name[0];
                label39.Text = fathername[0];
                label39.Visible = true;
                linkLabel12.Visible = true;
                linkLabel12.Text = name[1];
                linkLabel11.Visible = true;
                linkLabel11.Text = name[2];
                linkLabel10.Visible = true;
                linkLabel10.Text = name[3];
                linkLabel9.Visible = true;
                linkLabel9.Text = name[4];
                label38.Visible = true;
                label38.Text = fathername[1];
                label37.Visible = true;
                label37.Text = fathername[2];
                label36.Visible = true;
                label36.Text = fathername[3];
                label35.Visible = true;
                label35.Text = fathername[4];
                if (temp >= 5)
                {
                    linkLabel8.Visible = true;
                }
                else linkLabel8.Visible = false;
            }
            catch (Exception ex)
            {
                label42.Visible = false;
                label42.Location = new Point(275, 100); MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.ID = Convert.ToByte(id[0]);
                Class1.Table = "Member";
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.ID = Convert.ToByte(id[1]);
                Class1.Table = "Member";
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.ID = Convert.ToByte(id[2]);
                Class1.Table = "Member";
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.ID = Convert.ToByte(id[3]);
                Class1.Table = "Member";
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                connection.Open();
                Class1.ID = Convert.ToByte(id[4]);
                Class1.Table = "Member";
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Please check again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.Close(); }
        }

        private void button9_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                button9.PerformClick();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            button15.PerformClick();
            timer1.Stop();
            label41.Visible = false;
            label42.Visible = false;
            label41.Location = new Point(275, 100);
            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            linkLabel11.Visible = false;
            linkLabel12.Visible = false;
            linkLabel13.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            linkLabel1.Visible = false;
            linkLabel2.Visible = false;
            linkLabel3.Visible = false;
            linkLabel4.Visible = false;
            linkLabel5.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            linkLabel7.Visible = false;
            linkLabel6.Visible = false;
            linkLabel14.Visible = false;
            linkLabel8.Visible = false;
        }

        private void button15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                button15.PerformClick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label41.Location.X == 275 && label41.Location.Y == 100)
            {
                label41.Location = new Point(65, 180);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label41.Location.X == 65 && label41.Location.Y == 180)
            {
                label41.Location = new Point(275, 255);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label41.Location.X == 275 && label41.Location.Y == 255)
            {
                label41.Location = new Point(490, 180);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label41.Location.X == 490 && label41.Location.Y == 180)
            {
                label41.Location = new Point(275, 100);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }

            if (label42.Location.X == 275 && label42.Location.Y == 100)
            {
                label42.Location = new Point(6, 180);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label42.Location.X == 6 && label42.Location.Y == 180)
            {
                label42.Location = new Point(275, 255);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label42.Location.X == 275 && label42.Location.Y == 255)
            {
                label42.Location = new Point(490, 180);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
            else if (label42.Location.X == 490 && label42.Location.Y == 180)
            {
                label42.Location = new Point(275, 100);
                timer1.Stop();
                timer1.Start();
                timer1.Interval = 500;
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }
    }
}