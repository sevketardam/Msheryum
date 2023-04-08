using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Drawing.Text;

namespace Msheryum
{
    public partial class kisi_ara : Form
    {
        public kisi_ara()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data source = .\SQLEXPRESS01;Initial catalog = Msheryum1;Integrated security=true;");
        public static byte verigonder;
        public static string ogretmenad;
        public static string ogretmensoyad;
        public static string ogretmentc;
        public static string ogrenciad;
        public static string ogrencisoyad;
        public static string ogrencino;
        public static string velieposta;
        public static string kartid;
        public static int id = -1;
        static byte git;
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            kartid = textBox8.Text;
            if(textBox8.Text=="")
            {
                label10.Text = "";
            }

            if (textBox8.Text == "")
            {

                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox7.Clear();
                kisislemBtn.Enabled = false;
            }
            else
            {
                if (textBox9.Text != "")
                {

                }
                else
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut2 = new SqlCommand("select * from ogretmen_bilgi", baglanti);
                    SqlDataReader rdr = komut2.ExecuteReader();
                    while (rdr.Read())
                    {

                        if (rdr["kart_id"].ToString() == textBox8.Text)
                        {
                            textBox3.Text = rdr["ogretmen_adi"].ToString();
                            textBox4.Text = rdr["ogretmen_soyadi"].ToString();
                            textBox7.Text = rdr["ogretmen_tc"].ToString();
                            ogretmenad = rdr["ogretmen_adi"].ToString();
                            ogretmensoyad = rdr["ogretmen_soyadi"].ToString();
                            ogretmentc = rdr["ogretmen_tc"].ToString();
                            id = Convert.ToInt32(rdr["ogretmen_id"]);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            verigonder = 1;
                            kisislemBtn.Enabled = true;
                            git = 1;
                        }
                    }

                    baglanti.Close();

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    SqlCommand komut3 = new SqlCommand("select * from ogrenci_bilgi", baglanti);
                    SqlDataReader rdr2 = komut3.ExecuteReader();
                    while (rdr2.Read())
                    {

                        if (rdr2["kart_id"].ToString() == textBox8.Text)
                        {

                            id = Convert.ToInt32(rdr2["ogrenci_id"]);
                            textBox1.Text = rdr2["ogrenci_adi"].ToString();
                            textBox2.Text = rdr2["ogrenci_soyadi"].ToString();
                            textBox5.Text = rdr2["ogrenci_numara"].ToString();
                            textBox6.Text = rdr2["veli_eposta"].ToString();
                            ogrenciad = rdr2["ogrenci_adi"].ToString();
                            ogrencisoyad = rdr2["ogrenci_soyadi"].ToString();
                            ogrencino = rdr2["ogrenci_numara"].ToString();
                            velieposta = rdr2["veli_eposta"].ToString();

                            textBox3.Clear();
                            textBox4.Clear();
                            textBox7.Clear();
                            verigonder = 2;
                            kisislemBtn.Enabled = true;
                            git = 2;

                        }

                    }
                    baglanti.Close();
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    SqlCommand komut5 = new SqlCommand("select giris_saat from giris_cikis_saat  where Kart_id = @kID and giris_saat != '' ", baglanti);
                    komut5.Parameters.AddWithValue("@kID", textBox8.Text);
                    SqlDataReader rdr5 = komut5.ExecuteReader();

                    if (git == 1)
                    {

                        if (rdr5.Read())
                        {
                            label10.Text = "Öğretmen okuldadır.";

                        }
                        else if(!rdr5.Read())
                        {
                            label10.Text = "Öğretmen okulda değildir.";
                        }
                        else
                        {
                            label10.Text = "";
                        }

                    }

                    baglanti.Close();


                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut4 = new SqlCommand("select giris_saat from giris_cikis_saat  where Kart_id = @kID and giris_saat != ''   ", baglanti);
                    komut4.Parameters.AddWithValue("@kID", textBox8.Text);
                    SqlDataReader rdr4 = komut4.ExecuteReader();
                    if (git == 2)
                    {

                        if (rdr4.Read())
                        {
                            label10.Text = "Öğrenci okuldadır.";

                        }
                        else if(!rdr4.Read())
                        {
                            label10.Text = "Öğrenci okulda değildir.";
                        }
                        else
                        {
                            label10.Text = "";
                        }

                    }


                    rdr2.Close();
                    baglanti.Close();
                }
              



            }
        }
        string[] portlar = SerialPort.GetPortNames();

        private void button1_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
        }
        private void displayText(object o, EventArgs e)
        {
            textBox8.Text = rxString.ToString();
   
        }
        private string rxString;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);


        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        private void kisi_ara_Load(object sender, EventArgs e)
        {
            byte[] fontData = Properties.Resources.Akrobat_Thin;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Akrobat_Thin.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Akrobat_Thin.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0],16.0F);
            groupBox1.Font = myFont;
            groupBox2.Font = myFont;
            label1.Font = myFont;
            label2.Font = myFont;
            label3.Font = myFont;
            label4.Font = myFont;
            label5.Font = myFont;
            label6.Font = myFont;
            label7.Font = myFont;
            label8.Font = myFont;
            label9.Font = myFont;
            label10.Font = myFont;
            label11.Font = myFont;
            comboBox1.Font = myFont;
            textBox1.Font = myFont;
            textBox2.Font = myFont;
            textBox3.Font = myFont;
            textBox4.Font = myFont;
            textBox5.Font = myFont;
            textBox6.Font = myFont;
            textBox7.Font = myFont;
            textBox8.Font = myFont;
            textBox9.Font = myFont;
            myFont = new Font(fonts.Families[0],10.0F);
            button8.Font = myFont;
            button2.Font = myFont;

            try
            {
                foreach (string port in portlar)
                {
                    comboBox1.Items.Add(port);


                }
                comboBox1.SelectedIndex = 0;


                serialPort1.PortName = comboBox1.Text;

                serialPort1.Open();
            }
            catch 
            {

                label12.Text = "";
            }
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sorgu sorgu = new sorgu();
            sorgu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;

            if (i % 2 == 0)
            {
                label10.ForeColor = Color.Gray;
            }
            else
            {
                label10.ForeColor = Color.LightYellow;
               
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void kisi_ara_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult karar = MessageBox.Show("Geri dönmek istediğinize  istediğinize emin misiniz?", "Onay Penceresi", MessageBoxButtons.YesNo);
            e.Cancel = false;
            if (karar == DialogResult.No)
            {
                e.Cancel = true;
            }
          
        }

        private void kisi_ara_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
         
            if (textBox8.Text == "")
            {
                label10.Text = "";
            }

            if (textBox9.Text == "")
            {

                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox7.Clear();
                textBox8.Clear();
                kisislemBtn.Enabled = false;
            }
            else
            {

      
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                SqlCommand komut3 = new SqlCommand("select * from ogrenci_bilgi", baglanti);
                SqlDataReader rdr2 = komut3.ExecuteReader();
                while (rdr2.Read())
                {

                    if (rdr2["ogrenci_numara"].ToString() == textBox9.Text)
                    {

                        id = Convert.ToInt32(rdr2["ogrenci_id"]);
                        textBox1.Text = rdr2["ogrenci_adi"].ToString();
                        textBox2.Text = rdr2["ogrenci_soyadi"].ToString();
                        textBox5.Text = rdr2["ogrenci_numara"].ToString();
                        textBox6.Text = rdr2["veli_eposta"].ToString();
                        ogrenciad = rdr2["ogrenci_adi"].ToString();
                        ogrencisoyad = rdr2["ogrenci_soyadi"].ToString();
                        ogrencino = rdr2["ogrenci_numara"].ToString();
                        velieposta = rdr2["veli_eposta"].ToString();

                        textBox8.Text = rdr2["kart_id"].ToString();

                        textBox3.Clear();
                        textBox4.Clear();
                        textBox7.Clear();
                        verigonder = 2;
                        kisislemBtn.Enabled = true;
                        git = 2;

                    }
                    else
                    {

        
              

                    }

                }
                baglanti.Close();

                if (textBox8.Text != "")
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut4 = new SqlCommand("select giris_saat from giris_cikis_saat  where Kart_id = @kID and giris_saat != ''   ", baglanti);
                    komut4.Parameters.AddWithValue("@kID", textBox8.Text);
                    SqlDataReader rdr4 = komut4.ExecuteReader();
                    if (git == 2)
                    {

                        if (rdr4.Read())
                        {
                            label10.Text = "Öğrenci okuldadır.";

                        }
                        else if(!rdr4.Read())
                        {
                            label10.Text = "Öğrenci okulda değildir.";
                        }
                        else
                        {
                            label10.Text = "";
                        }

                    }


                    rdr2.Close();
                    baglanti.Close();
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            rxString = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(displayText));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sorgu sorgu = new sorgu();
            sorgu.Show();
        }
    }
}