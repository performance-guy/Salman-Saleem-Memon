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
        byte[] bitmap;

        public Form2(SqlDataReader read,string Type)
        {
            InitializeComponent();

            if (Type == "Machine")
            {
                lBL_hEad.Text = "DETAIL OF MACHINE";

                
                label24.Visible = false; label25.Visible = false; label26.Visible = false; label27.Visible = false;
                label28.Visible = false; label29.Visible = false;
                label38.Visible = false; label39.Visible = false; label40.Visible = false; label41.Visible = false;
                label42.Visible = false; label43.Visible = false;

                label30A.Text = "ID"; label31.Text = "Name"; label32.Text = "Exercise"; label33.Text = "Serial";
                label34.Text = "Cost";label35.Text = "Seller"; label36.Text = "Date"; label37.Text = "Receipt_No";

                label15.Text = read[0].ToString(); label17.Text = read[1].ToString();
                label18.Text = read[2].ToString(); label19.Text = read[3].ToString();
                label20.Text = read[4].ToString(); label21.Text = read[5].ToString();
                label22.Text = read[6].ToString(); label23.Text = read[8].ToString();

                if (read[7].ToString() != "")
                {
                    bitmap = read[7] as byte[] ?? null;

                    using (MemoryStream ms = new MemoryStream(bitmap))
                    {
                        Bitmap btm = new System.Drawing.Bitmap(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = (Image)btm;
                    }
                }
            }
            if (Type == "Member")
            {
                lBL_hEad.Text = "DETAIL OF MEMBER";

                
                label24.Visible = true; label25.Visible = true; label26.Visible = true; label27.Visible = true;
                label28.Visible = true; label29.Visible = false;
                label38.Visible = true; label39.Visible = true; label40.Visible = true; label41.Visible = true;
                label42.Visible = true; label43.Visible = false;

                label30A.Text = "ID"; label31.Text = "Name"; label32.Text = "Father_Name"; label33.Text = "address";
                label34.Text = "CNIC"; label35.Text = "Contact_No"; label36.Text = "Martial_Status"; label37.Text = "Gender";
                label38.Text = "Age"; label39.Text = "Weight"; label40.Text = "Height"; label41.Text = "BMI"; label42.Text = "date";
               


                label15.Text = read[0].ToString(); label17.Text = read[1].ToString();
                label18.Text = read[2].ToString(); label19.Text = read[3].ToString();
                label20.Text = read[4].ToString(); label21.Text = read[5].ToString();
                label22.Text = read[6].ToString(); label23.Text = read[7].ToString();
                label24.Text = read[8].ToString(); label25.Text = read[9].ToString();
                label26.Text = read[10].ToString(); label27.Text = read[11].ToString();
                label28.Text = read[12].ToString();

                if (read[13].ToString() != "")
                {
                    bitmap = read[13] as byte[] ?? null;

                    using (MemoryStream ms = new MemoryStream(bitmap))
                    {
                        Bitmap btm = new System.Drawing.Bitmap(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = (Image)btm;
                    }
                }
            }
            if (Type == "Employee")
            {
                lBL_hEad.Text = "DETAIL OF EMPLOYEE";

                
                label24.Visible = true; label25.Visible = true; label26.Visible = true; label27.Visible = true;
                label28.Visible = true; label29.Visible = true;
                label38.Visible = true; label39.Visible = true; label40.Visible = true; label41.Visible = true;
                label42.Visible = true; label43.Visible = true;

                label30A.Text = "ID"; label31.Text = "Name"; label32.Text = "FatherName"; label33.Text = "Address";
                label34.Text = "CNIC"; label35.Text = "ContactNo"; label36.Text = "Martial_Status"; label37.Text = "Gender";
                label38.Text = "Age"; label39.Text = "Designation"; label40.Text = "Salary"; label41.Text = "StartTime"; label42.Text = "OffTime";
                label43.Text = "Date";

                label15.Text = read[12].ToString(); label17.Text = read[0].ToString();
                label18.Text = read[1].ToString(); label19.Text = read[2].ToString();
                label20.Text = read[3].ToString(); label21.Text = read[4].ToString();
                label22.Text = read[5].ToString(); label23.Text = read[6].ToString();
                label24.Text = read[7].ToString(); label25.Text = read[8].ToString();
                label26.Text = read[9].ToString(); label27.Text = read[10].ToString();
                label28.Text = read[11].ToString(); label29.Text = read[14].ToString();

                if (read[13].ToString() != "")
                {
                    bitmap = read[13] as byte[] ?? null;

                    using (MemoryStream ms = new MemoryStream(bitmap))
                    {
                        Bitmap btm = new System.Drawing.Bitmap(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = (Image)btm;
                    }
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}