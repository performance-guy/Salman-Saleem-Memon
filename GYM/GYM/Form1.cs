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
            Remove_tab();
        }

        public void Remove_tab()
        {
            tabControl1.TabPages.Remove(tabPage2); tabControl1.TabPages.Remove(tabPage3); tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage5); tabControl1.TabPages.Remove(tabPage_Record);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        Class1 connection = new Class1();
        SqlCommand cmd;
        SqlDataReader reader;

        int temp;
        
        sbyte i;
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
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""
         && maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && maskedTextBox3.Text != "" && (radioButton1.Checked != false || radioButton2.Checked != false)
                && (radioButton3.Checked != false || radioButton4.Checked != false) && textBox4.Text != "" && textBox5.Text != ""
                && pictureBox1.Image != null)
            {
                if (MessageBox.Show("Are you sure you want to add member?", "Add Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dt = DateTime.Now;
                        format = dt.ToString(format);
                        temp = connection.Select_Last_Member("select MAX(ID) from Member_Record");
                        temp++;
                        Class1.Query = "insert into Member_Record (Age,ID,Name,Father_Name,CNIC,address,Contact_No,Martial_Status,Gender,Weight,Height,BMI,date) values(@age,@id,@name,@fathername,@cnic,@adress,@contact,@marital,@gender,@weight,@height,@bmi,@date)";
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


                        FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                        byte[] picbyte = new byte[fs.Length];
                        fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));

                        SqlParameter para = new SqlParameter();
                        para.SqlDbType = SqlDbType.Image;
                        para.ParameterName = "pic";
                        para.Value = picbyte;
                        Class1.Query = "update Member_Record set Picture=@pic where ID='" + temp + "'";
                        cmd.CommandText = Class1.Query;
                        cmd.Parameters.Add(para);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        MessageBox.Show("Member added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class1.Table = "Member";
                        Class1.ID = (byte)temp;

                        //for Print Purpose

                        //if (float.Parse(textBox5.Text) < 25)//by using bmi
                        //{
                            
                        //    img = Image.FromFile("LESS_THEN_25.jpg");
                        //    printDocument1.Print();
                        //}
                        //if (float.Parse(textBox5.Text) == 25)
                        //{
                        //    img = Image.FromFile("EQUAL_25.jpg");
                        //    printDocument1.Print();
                        //}
                        //if (float.Parse(textBox5.Text) > 25)
                        //{
                        //    img = Image.FromFile("GREATER_THEN_25.jpg");
                        //    printDocument1.Print();
                        //}

                        radioButton1.Checked = false; textBox1.Text = "";textBox2.Text = ""; textBox3.Text = "";
                        maskedTextBox1.Text = ""; maskedTextBox2.Text = ""; maskedTextBox3.Text = "";radioButton1.Checked = false; 
                        radioButton2.Checked = false; radioButton3.Checked = false; radioButton4.Checked = false; textBox4.Text = "";
                        textBox5.Text = ""; pictureBox1.Image = null; numericUpDown1.ResetText();
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
            else
            {
                MessageBox.Show("Fill Specified Fields", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        //Confirm Button
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "" && textBox7.Text != "" && textBox6.Text != ""&& maskedTextBox5.Text != "" &&
                maskedTextBox4.Text != "" && (radioButton8.Checked != false || radioButton7.Checked != false) &&
                (radioButton6.Checked != false || radioButton5.Checked != false) && numericUpDown2.Text != "" && 
                textBox9.Text != "" && textBox10.Text != "" && pictureBox2.Image!= null)
            {
                if (MessageBox.Show("Are you sure you want to add employee?", "Add Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dt = DateTime.Now;
                        format = dt.ToString(format);
                        temp = connection.Select_Last_Member("select MAX(ID) from Employee_Record");
                        temp++;
                        Class1.Query = "insert into Employee_Record (Age,ID,Name,FatherName,CNIC,Address,ContactNo,Martial_Status,Gender,Designation,Salary,StartTime,OffTime,Date) values(@age,@id,@name,@fathername,@cnic,@adress,@contact,@marital,@gender,@designation,@salary,@start,@end,@date)";
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

                        

                        FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                        byte[] picbyte = new byte[fs.Length];
                        fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));
                        SqlParameter para = new SqlParameter();
                        para.SqlDbType = SqlDbType.Image;
                        para.ParameterName = "pic";
                        para.Value = picbyte;
                        Class1.Query = "update Employee_Record set Picture=@pic where ID='" + temp + "'";
                        cmd.CommandText = Class1.Query;
                        cmd.Parameters.Add(para);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        MessageBox.Show("Employee added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class1.Table = "Employee";
                        Class1.ID = (byte)temp;
                        

                textBox8.Text = ""; textBox7.Text =""; textBox6.Text ="";maskedTextBox5.Text = ""; 
                maskedTextBox4.Text = "";radioButton8.Checked = false;radioButton7.Checked = false;
                radioButton6.Checked = false; radioButton5.Checked = false;  numericUpDown2.Text = "";
                textBox9.Text = ""; textBox10.Text = ""; pictureBox2.Image = null;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please check again\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill Specified Fields", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        //ADD MACHINE
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "" && textBox15.Text != ""
                && textBox14.Text != "" && textBox17.Text != "" && pictureBox3.Image != null)
            {
                if (MessageBox.Show("Are you sure you want to add machine?", "Add Machine", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dt = DateTime.Now;
                        format = dt.ToString(format);
                        temp = connection.Select_Last_Member("select MAX(ID) from Machine_Record");
                        temp++;
                        Class1.Query = "insert into Machine_Record (ID,Name,Exercise,Serial,Cost,Seller,Receipt_No,Date) values(@id,@name,@exercise,@code,@cost,@seller,@reciept,@date)";
                        connection.Open();
                        cmd = new SqlCommand(Class1.Query, connection.Conn());
                        cmd.Parameters.AddWithValue("@code", textBox13.Text);
                        cmd.Parameters.AddWithValue("@id", temp);
                        cmd.Parameters.AddWithValue("@name", textBox12.Text);
                        cmd.Parameters.AddWithValue("@exercise", textBox11.Text);
                        cmd.Parameters.AddWithValue("@cost", float.Parse(textBox15.Text));
                        cmd.Parameters.AddWithValue("@seller", textBox14.Text);
                        cmd.Parameters.AddWithValue("@reciept", textBox13.Text);
                        cmd.Parameters.AddWithValue("@date", format);
                        cmd.ExecuteNonQuery();

                        

                        FileStream fs = new FileStream(@Opendialog.FileName, FileMode.Open, FileAccess.Read);
                        byte[] picbyte = new byte[fs.Length];
                        fs.Read(picbyte, 0, Convert.ToInt32(fs.Length));
                        SqlParameter para = new SqlParameter();
                        para.SqlDbType = SqlDbType.Image;
                        para.ParameterName = "pic";
                        para.Value = picbyte;
                        Class1.Query = "update Machine_Record set Picture=@pic where ID='" + temp + "'";
                        cmd.CommandText = Class1.Query;
                        cmd.Parameters.Add(para);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        MessageBox.Show("Machine added successfully", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class1.Table = "Machine";
                        Class1.ID = (byte)temp;

                       textBox13.Text = ""; textBox12.Text = ""; textBox11.Text = ""; textBox15.Text = "";
                       textBox14.Text = ""; textBox17.Text = ""; pictureBox3.Image = null;



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please check again\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill Specified Fields", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

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
        string allowed = "1234567890.\u0008\u007f";
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!allowed.Contains(e.KeyChar))
                e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!allowed.Contains(e.KeyChar))
                e.Handled = true;
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!allowed.Contains(e.KeyChar))
                e.Handled = true;
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        { }

        private void button9_KeyDown(object sender, KeyEventArgs e)
        {
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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are sure you want to Logout?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

       
        

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                if ((cbx_id_name.SelectedItem.ToString() == "ID" || cbx_id_name.SelectedItem.ToString() == "Name") && cbx_type.SelectedItem.ToString() == "Machine")
                {
                    if (cbx_id_name.SelectedItem.ToString() == "ID")
                    {
                        Class1.Query = "select *from Machine_Record where ID=" + int.Parse(txt_search.Text);
                    }
                    else if (cbx_id_name.SelectedItem.ToString() == "Name")
                    {
                        Class1.Query = "select *from Machine_Record where Name='" + (txt_search.Text)+"'";
                    }

                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    Form2 frm = new Form2(reader, cbx_type.SelectedItem.ToString());
                    frm.Show();
                }
              else  if ((cbx_id_name.SelectedItem.ToString() == "ID" || cbx_id_name.SelectedItem.ToString() == "Name") && cbx_type.SelectedItem.ToString() == "Member")
                {
                    if (cbx_id_name.SelectedItem.ToString() == "ID")
                    {
                        Class1.Query = "select *from Member_Record where ID=" + int.Parse(txt_search.Text);
                    }
                    else if (cbx_id_name.SelectedItem.ToString() == "Name")
                    {
                        Class1.Query = "select *from Member_Record where Name='" + txt_search.Text + "'";
                    }

                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    Form2 frm = new Form2(reader, cbx_type.SelectedItem.ToString());
                    frm.Show();
                }
                else if ((cbx_id_name.SelectedItem.ToString() == "ID" || cbx_id_name.SelectedItem.ToString() == "Name") && cbx_type.SelectedItem.ToString() == "Employee")
                {
                    if (cbx_id_name.SelectedItem.ToString() == "ID")
                    {
                        Class1.Query = "select *from Employee_Record where ID=" + int.Parse(txt_search.Text);
                    }
                    else if (cbx_id_name.SelectedItem.ToString() == "Name")
                    {
                        Class1.Query = "select *from Employee_Record where Name='" + (txt_search.Text)+"'";
                    }

                    cmd = new SqlCommand(Class1.Query, connection.Conn());
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    Form2 frm = new Form2(reader, cbx_type.SelectedItem.ToString());
                    frm.Show();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
        }

        //For exit the application
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tlstrip_menu_MemberNew_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage2);
            tlstrip_menu_MemberNew.Enabled = false;
            tabControl1.SelectTab(tabPage2);
        }

        private void btn_CloseMember_Click(object sender, EventArgs e)
        {
            tlstrip_menu_MemberNew.Enabled = true;
            tabControl1.TabPages.Remove(tabPage2);
        }

        private void btn_Close_Employee_Click(object sender, EventArgs e)
        {
            tlstrip_menu_EmployeeNew.Enabled = true;
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void tlstrip_menu_EmployeeNew_Click(object sender, EventArgs e)
        {
            tlstrip_menu_EmployeeNew.Enabled = false;
            tabControl1.TabPages.Add(tabPage3);
            tabControl1.SelectTab(tabPage3);
        }

        private void btn_Close_Machine_Click(object sender, EventArgs e)
        {
            tlstrip_menu_MachineNew.Enabled = true;
            tabControl1.TabPages.Remove(tabPage4);
        }

        private void tlstrip_menu_MachineNew_Click(object sender, EventArgs e)
        {
            tlstrip_menu_MachineNew.Enabled = false;
            tabControl1.TabPages.Add(tabPage4);
            tabControl1.SelectTab(tabPage4);
        }

        private void btn_Close_Search_Click(object sender, EventArgs e)
        {
            searchToolStripMenuItem.Enabled = true;
            tabControl1.TabPages.Remove(tabPage5);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchToolStripMenuItem.Enabled = false;
            tabControl1.TabPages.Add(tabPage5);
            tabControl1.SelectTab(tabPage5);
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GYM management sysytem can store the data of:\nEmployee\nMember Records\nMachine Data\nIf you want to check the data you can search also.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        DataTable data = new DataTable(); //create datatable for adding record

        private void Member_recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage_Record);
            tabControl1.SelectTab(tabPage_Record);

            Member_recordToolStripMenuItem.Enabled = false;
            Employee_recordToolStripMenuItem.Enabled = false;
            Machine_recordToolStripMenuItem.Enabled = false;

            lBL_hEad.Text = "DETAIL OF MEMBER";

            try
            {
                SqlDataAdapter record = null;
                Class1.Query = "select ID,Name,Father_Name,Age,CNIC,address,Contact_No,Martial_Status,Gender,Weight,Height,BMI,date from Member_Record";
                connection.Open();

                data.Clear();
                using (record = new SqlDataAdapter(Class1.Query, connection.Conn()))
                {
                    dtgrid_Record.DataSource = null;
                    dtgrid_Record.Columns.Clear();
                    record.Fill(data);
                   
                    
                    dtgrid_Record.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Employee_recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage_Record);
            tabControl1.SelectTab(tabPage_Record);

            Member_recordToolStripMenuItem.Enabled = false;
            Employee_recordToolStripMenuItem.Enabled = false;
            Machine_recordToolStripMenuItem.Enabled = false;

            lBL_hEad.Text = "DETAIL OF EMPLOYEE";

            try
            {
                SqlDataAdapter record = null;
                Class1.Query = "select ID,Name,FatherName,Age,CNIC,Address,ContactNo,Martial_Status,Gender,Designation,Salary,StartTime,OffTime,Date from Employee_Record";
                connection.Open();
                data.Clear();
                using (record = new SqlDataAdapter(Class1.Query, connection.Conn()))
                {
                    dtgrid_Record.Columns.Clear();
                    dtgrid_Record.DataSource = null;
                    record.Fill(data);               
                    
                    dtgrid_Record.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Machine_recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage_Record);
            tabControl1.SelectTab(tabPage_Record);

            Member_recordToolStripMenuItem.Enabled = false;
            Employee_recordToolStripMenuItem.Enabled = false;
            Machine_recordToolStripMenuItem.Enabled = false;

            lBL_hEad.Text = "DETAIL OF MACHINE";

            try
            {
                SqlDataAdapter record = null;
                Class1.Query = "select ID,Name,Exercise,Serial,Cost,Seller,Receipt_No,Date from Machine_Record";
                connection.Open();
                data.Clear();
                using (record = new SqlDataAdapter(Class1.Query, connection.Conn()))
                {
                    

                    record.Fill(data);
                   
                  
                    dtgrid_Record.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check again\n" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btn_Recordclose_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage_Record);

                Member_recordToolStripMenuItem.Enabled = true;
                Employee_recordToolStripMenuItem.Enabled = true;
                Machine_recordToolStripMenuItem.Enabled = true;

                
                data.Clear();
                data.Columns.Clear();

                dtgrid_Record.DataSource = null;
                Class1.Query = "";
        }
    }
}