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
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Drawing.Text;

namespace Msheryum
{
    public partial class menu : Form
    {
        string[] portlar = SerialPort.GetPortNames(); //Port isimlerini almak için dizi oluşturuyor

        public menu()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);


        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        public static int buyukluk = 0; //Bilgilerin gösterildiği ListView'in boyutlarını tutuyor başlangıç değeri 0 
        SqlConnection baglanti = new SqlConnection(@"Data source = .\SQLEXPRESS01;Initial catalog = Msheryum1;Integrated security=true;");

        private void menu_Load(object sender, EventArgs e)
        {

            byte[] fontData = Properties.Resources.Akrobat_Thin;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Akrobat_Thin.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Akrobat_Thin.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);



            listView2.BackColor = Color.FromArgb(33, 33, 33);//öğretmen
            listView11.BackColor = Color.FromArgb(33, 33, 33);//öğrenci
            listView12.BackColor = Color.FromArgb(33, 33, 33);//sinif
            listView16.BackColor = Color.FromArgb(33,33,33);//giriş çıkış

            groupBox1.Visible = false;//öğretmen

            myFont = new Font(fonts.Families[0], 14.0F);
            groupBox1.Font = myFont;
            label12.Font = myFont;
            label9.Font = myFont;
            label6.Font = myFont;
            label8.Font = myFont;
            label46.Font = myFont;
            label21.Font = myFont;
            label19.Font = myFont;
            button13.Font = myFont;
            button6.Font = myFont;
            button5.Font = myFont;
            textBox9.Font = myFont;
            listView2.Font = myFont;
            prtCmb.Font = myFont;
            krtidTxt.Font = myFont;
            tcTxt.Font = myFont;
            ogretmenAdiTxt.Font = myFont;
            ogretmenSoyadTx.Font = myFont;


            myFont = new Font(fonts.Families[0], 10.0F);
            button7.Font = myFont;

            groupBox2.Visible = false;//email
            myFont = new Font(fonts.Families[0],14.0F);
            groupBox2.Font = myFont;
            label45.Font = myFont;
            label44.Font = myFont;
            label43.Font = myFont;
            maskedTextBox3.Font = myFont;
            comboBox3.Font = myFont;
            textBox3.Font = myFont;
            richTextBox3.Font = myFont;
            button14.Font = myFont;

            groupBox3.Visible = false;//öğrenci
            myFont = new Font(fonts.Families[0], 14.0F);
            groupBox3.Font = myFont;
            label73.Font = myFont;
            label72.Font = myFont;
            label71.Font = myFont;
            label70.Font = myFont;
            label69.Font = myFont;
            label68.Font = myFont;
            label47.Font = myFont;
            label48.Font = myFont;
            label49.Font = myFont;
            checkBox8.Font = myFont;
            button36.Font = myFont;
            button34.Font = myFont;
            button35.Font = myFont;
            textBox35.Font = myFont;
            listView11.Font = myFont;
            comboBox11.Font = myFont;
            krtidstTx.Font = myFont;
            ogrenciAdTx.Font = myFont;
            ogrenciSoyadTx.Font = myFont;
            sinifCm.Font = myFont;
            ogrenciNum.Font = myFont;
            emailTx.Font = myFont;
            myFont = new Font(fonts.Families[0],10.0F);
            button15.Font = myFont;


            groupBox4.Visible = false;//sinif
            myFont = new Font(fonts.Families[0],14.0F);
            groupBox4.Font = myFont;
            label74.Font = myFont;
            textBox31.Font = myFont;
            button39.Font = myFont;
            button38.Font = myFont;
            textBox4.Font = myFont;
            listView12.Font = myFont;
            button37.Font = myFont;

            groupBox5.Visible = false;//giriş çıkış
            groupBox5.Font = myFont;
            label93.Font = myFont;
            textBox32.Font = myFont;
            listView16.Font = myFont;

            //  System.Text.RegularExpressions.Regex.IsMatch(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", maskedTextBox1.Text);
            yenile3();
            yenile2();
            yenile();
            yenile4();
            sinifCm.SelectedIndex = -1; //Sınıfların gösterildi kutunun boş gelmesini sağlar



            if (kisi_ara.verigonder == 1) //kisi_ara daki veri gönder değişkenin değeri 1 ise 
            {
                krtidTxt.Text = kisi_ara.kartid; //kisi ara'dan gelen değeri metin kutusuna yazıdıyor
                tcTxt.Text = kisi_ara.ogretmentc;
                ogretmenAdiTxt.Text = kisi_ara.ogretmenad;
                ogretmenSoyadTx.Text = kisi_ara.ogretmensoyad;
            }
            else if (kisi_ara.verigonder == 2)
            {
                krtidstTx.Text = kisi_ara.kartid;
                ogrenciAdTx.Text = kisi_ara.ogrenciad;
                ogrenciSoyadTx.Text = kisi_ara.ogrencisoyad;
                ogrenciNum.Text = kisi_ara.ogrencino;
                emailTx.Text = kisi_ara.velieposta;
            }


            try
            {
                sinifCm.Items.Add("Seçiniz"); //Sınıfların gösterildiği kutuya Seçiniz adlı veri ekliyor
                if (sinifCm.Text == "Seçiniz") //Sınıfların gösterildiği ComboBox'ta Seçiniz yazıyorsa. Bir işlem yapmıyor ama Else kodunun işlenmesi için eklenıyor
                {

                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("select * from sinif_tablosu ", baglanti); //Sınıfların bulunduğu tablodan verileri çekiyor

                    SqlDataAdapter da = new SqlDataAdapter(komut);//Çekilen verilerin DataReader'a doldurulmasını sağlar
                    SqlDataReader dr = komut.ExecuteReader();
                    Dictionary<string, string> dis = new Dictionary<string, string>(); // Sınıf bilgilerinin tutulacağı birnevi bir sözlük oluşturur
                    while (dr.Read())
                    {
                        dis.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());//dr okundukça sınıf tablosundaki verilen comboBox'a aktarılmasını sağlar
                    }
                    sinifCm.DataSource = new BindingSource(dis, null); //comboBox'un veri kaynağını belirtiyor
                    sinifCm.DisplayMember = "Value";
                    sinifCm.ValueMember = "Key";
                    baglanti.Close();
                }



                foreach (string port in portlar) // Ardiuno ile alakalı portlar dizisin içinde tüm elemanlarında dönmesini sağlıupr
                {
                    comboBox11.Items.Add(port); //Port'u comboBox'a ekliyor
                    prtCmb.Items.Add(port);

                }
                comboBox11.SelectedIndex = 0;
                prtCmb.SelectedIndex = 0;

                serialPort1.PortName = comboBox1.Text;
                serialPort1.PortName = prtCmb.Text;

                serialPort1.Open();
                buyukluk = listView12.Width;

            }
            catch (Exception)//Hatayı yakalıyor
            {
                label15.Text = "Cihaz Bağlı Değil!"; //Eğer Arduino seti bağlı değilse Cihaz bağlı değil diye yazdırıyor
                label15.ForeColor = Color.Red;//Arka rengini Kırmızı yapıyor

                label16.Text = "Cihaz Bağlı Değil!";
                label16.ForeColor = Color.Red;

            }
            buyukluk =
            listView2.Width = listView2.Columns[0].Width + listView2.Columns[1].Width + listView2.Columns[2].Width + listView2.Columns[3].Width; //Bilgilerin gözüktüğü ListView'lerin kolonlarına göre genişliğini ayarlıyor
            listView1.Width = listView1.Columns[0].Width + listView1.Columns[1].Width + listView1.Columns[2].Width + listView1.Columns[3].Width + listView1.Columns[4].Width + listView1.Columns[6].Width;

        }

        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult karar = MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Onay Penceresi", MessageBoxButtons.YesNo); //Eğer uygulama kapatılmak isteniyor ise uyarı mesajı veriliyor
            e.Cancel = false;
            if (karar == DialogResult.No) //Eğer kapatılmak istenmiyorsa
            {
                e.Cancel = true;//işlem iptal ediliyor
            }

        }
       
        private void timer1_Tick(object sender, EventArgs e) //Kronometre her tiklendiğinde
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString(); //Şuanki tarih bilgisini ekrana yazdırıyor
        }
      
        void yenile() //ListView içerisindeki verileri yeniler
        {
            listView11.Items.Clear();


            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            int i = 0;
            SqlCommand komut = new SqlCommand("select ogrenci_id,ogrenci_numara,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,kart_id from ogrenci_bilgi JOIN sinif_tablosu on ogrenci_bilgi.sinif = sinif_tablosu.sinif_id", baglanti);
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {

                ListViewItem Ivi = new ListViewItem(rdr["kart_id"].ToString());
                Ivi.SubItems.Add(rdr["ogrenci_numara"].ToString());
                Ivi.SubItems.Add(rdr["ogrenci_adi"].ToString());
                Ivi.SubItems.Add(rdr["ogrenci_soyadi"].ToString());
                Ivi.SubItems.Add(rdr["veli_eposta"].ToString());
                Ivi.SubItems.Add(rdr["ogrenci_id"].ToString());
                Ivi.SubItems.Add(rdr["sinif"].ToString());
                listView11.Items.Add(Ivi);

            }
            foreach (ListViewItem item in listView11.Items)
            {

                if (i++ % 2 == 0)//Her iki veride bir arkaplanın rengini değiştirir
                {
                    item.BackColor = Color.DarkGray;
                    item.ForeColor = Color.White;
                }
            }
            if (listView11.Items.Count <= 0)
            {
                listView11.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listView11.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView11.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView11.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView11.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView11.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView11.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        private void menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        void yenile2() // Öğretmen bilgilerin yenilenmesini sağlar
        {
            listView2.Items.Clear();


            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select * from ogretmen_bilgi", baglanti);
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {

                ListViewItem Ivi = new ListViewItem(rdr["kart_id"].ToString());
                Ivi.SubItems.Add(rdr["ogretmen_tc"].ToString());
                Ivi.SubItems.Add(rdr["ogretmen_adi"].ToString());
                Ivi.SubItems.Add(rdr["ogretmen_soyadi"].ToString());
                Ivi.SubItems.Add(rdr["ogretmen_id"].ToString());
                listView2.Items.Add(Ivi);

            }
            int i = 0;
            foreach (ListViewItem item in listView2.Items)
            {

                if (i++ % 2 == 0)
                {
                    item.ForeColor = Color.White;
                }
            }
            if (listView2.Items.Count <= 0)
            {
                listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listView2.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView2.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView2.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView2.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        private string rxString;

     
        public string tc;
      

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(displayText));
        }
        private void displayText(object o, EventArgs e)
        {
            krtidTxt.Text = rxString.ToString();
            krtidstTxt.Text = rxString.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            this.Width = 480;
            this.Height = 480;

        }

        private void tabPage3_Leave(object sender, EventArgs e)
        {
            this.Width = 959;
            this.Height = 533;
        }
        void yenile3()
        {
            listView12.Items.Clear();


            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            int i = 0;
            SqlCommand komut = new SqlCommand("select * from sinif_tablosu", baglanti);
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {
                i++;
                ListViewItem Ivi = new ListViewItem(i.ToString());
                Ivi.SubItems.Add(rdr["sinif"].ToString());
                Ivi.SubItems.Add(rdr["sinif_id"].ToString());
                listView12.Items.Add(Ivi);
            }
            i = 0;
            foreach (ListViewItem item in listView3.Items)
            {

                if (i++ % 2 == 0)
                {
                    item.BackColor = Color.Gray;
                    item.ForeColor = Color.White;
                }
                else
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Gray;
                }
            }
            if (listView12.Items.Count <= 0)
            {
                listView12.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listView12.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView12.Columns[1].Width = 133;
            }

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

 
        public int numara;
        public int sinif_id;

   
        private void yenile4()
        {

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            listView16.Items.Clear();
            SqlCommand komut = new SqlCommand("select giris_cikis_saat.Kart_id,ogrenci_adi,ogrenci_soyadi,giris_saat,cikis_saat,tarihi from giris_cikis_saat JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id", baglanti);
            SqlDataReader rdr = komut.ExecuteReader();

            while (rdr.Read())
            {
                ListViewItem ekle = new ListViewItem(rdr["Kart_id"].ToString());
                ekle.SubItems.Add(rdr["ogrenci_adi"].ToString());
                ekle.SubItems.Add(rdr["ogrenci_soyadi"].ToString());
                ekle.SubItems.Add(rdr["giris_saat"].ToString());
                ekle.SubItems.Add(rdr["cikis_saat"].ToString());
                ekle.SubItems.Add(rdr["tarihi"].ToString());
                listView16.Items.Add(ekle);
            }

            baglanti.Close();
            if (listView16.Items.Count <= 0)
            {
                listView16.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listView16.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView16.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView16.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView16.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView16.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView16.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            this.Width = 540;
            yenile4();

        }

        private void tabPage4_Leave(object sender, EventArgs e)
        {
            this.Width = 959;
        }
        
        private void tabPage5_Enter(object sender, EventArgs e)
        {
            this.Width = 480;
        }
       
        private void tabPage5_Leave(object sender, EventArgs e)
        {
            this.Width = 959;
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tcTxt.Clear();
            ogretmenSoyadTxt.Clear();
            ogretmenAdiTxt.Clear();
            krtidstTxt.Clear();
            ogrenciSoyadTxt.Clear();
            ogrenciNumaraTxt.Clear();
            emailTxt.Clear();
            krtidstTxt.Clear();
            ogrenciAdTxt.Clear();
            krtidTxt.Clear();
            sinifCmb.SelectedIndex = -1;

        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Refresh();
        }

        int sayac = 1;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            if (sayac == 1)
            {
                sayac = 0;
                pictureBox6.Image = Msheryum.Properties.Resources.menulast;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                pictureBox11.Visible = true;

            }
            else if (sayac == 0)
            {
                sayac++;
                pictureBox6.Image = Msheryum.Properties.Resources.menufirst;
                this.BackgroundImage = Msheryum.Properties.Resources.dashboardfirst;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
            }
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Msheryum.Properties.Resources.emailhover;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Msheryum.Properties.Resources.email;

        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Image = Msheryum.Properties.Resources.ogrenci;
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.Image = Msheryum.Properties.Resources.ogrencihover;
        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.Image = Msheryum.Properties.Resources.ogretmenhover1;

        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.Image = Msheryum.Properties.Resources.ogretmen;

        }

        private void pictureBox10_MouseEnter(object sender, EventArgs e)
        {
            pictureBox10.Image = Msheryum.Properties.Resources.sinifislemlerihover;

        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.Image = Msheryum.Properties.Resources.sinifislemleri;

        }

        private void pictureBox11_MouseEnter(object sender, EventArgs e)
        {
            pictureBox11.Image = Msheryum.Properties.Resources.giriscikishover;

        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.Image = Msheryum.Properties.Resources.giriscikis;

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            groupBox5.Visible = false;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (maskedTextBox3.Text == "" && textBox3.Text == "" && richTextBox3.Text == "")
            {
                MessageBox.Show("Lütfen gereken değerleri giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string email = maskedTextBox3.Text;
                Regex regex = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    MailMessage mesaj = new MailMessage();
                    SmtpClient istem = new SmtpClient();
                    istem.Credentials = new System.Net.NetworkCredential("msheryum.projesi@gmail.com", "erdo#meto#sevket#duzceartvin123");
                    istem.Port = 587;
                    istem.Host = "smtp.gmail.com";
                    istem.EnableSsl = true;
                    mesaj.To.Add(maskedTextBox3.Text);
                    mesaj.From = new MailAddress("msheryum.projesi@gmail.com");
                    mesaj.Subject = textBox3.Text;
                    mesaj.Body = richTextBox3.Text;
                    istem.Send(mesaj);
                    MessageBox.Show("Mesajınız iletilmiştir.");
                }
                else
                {
                    MessageBox.Show("E-mail formatınız düzgün değildir.");
                }
            }
        }

        private void krtidTxt_TextChanged(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand komut = new SqlCommand("select * from ogretmen_bilgi", baglanti);
            SqlDataReader rdr = komut.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["kart_id"].ToString() == krtidTxt.Text)
                {
                    krtidTxt.Text = rdr["kart_id"].ToString();
                    tcTxt.Text = rdr["ogretmen_tc"].ToString();
                    ogretmenAdiTxt.Text = rdr["ogretmen_adi"].ToString();
                    ogretmenSoyadTx.Text = rdr["ogretmen_soyadi"].ToString();
                }
            }
            baglanti.Close();
        }

        private void tcTxt_TextChanged_1(object sender, EventArgs e)
        {
            if (tcTxt.Text == " ")
            {
                tcTxt.Clear();
            }
            if (tcTxt.Text.Length < 11 && tcTxt.Text != "")//eğer TC kimlik nosu girilen textBox 11'den küçükse uyarır
            {
                errorProvider1.SetError(tcTxt, "TC Kimlik No 11 karakter olmalıdır!");
            }

            else
            {
                errorProvider1.Clear();
            }
        }

        private void ogretmenAdiTxt_TextChanged(object sender, EventArgs e)
        {
            if (ogretmenAdiTxt.Text == " ")
            {
                ogretmenAdiTxt.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (ogretmenSoyadTx.Text == " ")
            {
                ogretmenSoyadTx.Clear();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            serialPort1.Write("1");

        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool durum = true;

            if (krtidTxt.Text == "" && tcTxt.Text == "" && ogretmenAdiTxt.Text == "" && ogretmenSoyadTx.Text == "")//Eğer veri girilen metin kutuları boş ise
            {
                MessageBox.Show("Bilgileri Giriniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning); //Uyarı mesajı yazıdırıyor
            }
            else
            {
                try
                {

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    SqlCommand komut2 = new SqlCommand("select * from ogretmen_bilgi", baglanti);
                    SqlDataReader rdr = komut2.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr["kart_id"].ToString() == krtidTxt.Text || rdr["ogretmen_tc"].ToString() == tcTxt.Text)//Eklenmeye çalışan tc kimlik numarası ve kart numarası zaten varsa
                        {
                            durum = false;//değişken false durumunu alır
                        }

                    }
                    baglanti.Close();

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    if (durum == false)//durum değişkeni false ise
                    {
                        MessageBox.Show("Kart ID ya da TC kimlik ile kaydedilmiş böyle bir kayıt zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SqlCommand komut = new SqlCommand("insert  into ogretmen_bilgi (ogretmen_tc,ogretmen_adi,ogretmen_soyadi,kart_id) values(@tc,@ad,@soyad,@kart)", baglanti);//Veritabanına ad,soyadi,tc ve kart numarası gibi verileri ekliyor
                        komut.Parameters.AddWithValue("@ad", ogretmenAdiTxt.Text);
                        komut.Parameters.AddWithValue("@soyad", ogretmenSoyadTx.Text);
                        komut.Parameters.AddWithValue("@tc", tcTxt.Text);
                        komut.Parameters.AddWithValue("@kart", krtidTxt.Text);
                        komut.ExecuteNonQuery(); //komutun çalışmasını sağlıyor
                        MessageBox.Show("Kart kaydedilmiştir.");
                        tcTxt.Clear(); // veri eklendikten sonra kutuları temizliyor
                        ogretmenSoyadTx.Clear();
                        ogretmenAdiTxt.Clear();
                        krtidTxt.Clear();
                        sinifCmb.SelectedIndex = -1;//combobox'un boş gelmesini sağlıyor
                    }

                    baglanti.Close();

                    yenile2();//Listview'in verilerini yenileyip yeni veri eklemiş halini gösteriyor
                }


                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);//Hata oluşması durumunda ekrana yazdırıyor
                }

            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (listView2.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("Güncelleyeceğiniz öğretmeni seçiniz.");
                }
                else
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    bool durum = true;
                    SqlCommand komut2 = new SqlCommand("select * from ogretmen_bilgi where ogretmen_tc = '" + tcTxt.Text + "'", baglanti);
                    SqlDataReader rdr = komut2.ExecuteReader();
                    if (rdr.Read())
                    {
                        durum = false;
                    }


                    baglanti.Close();
                    if (durum == false && tc != tcTxt.Text)
                    {
                        MessageBox.Show("Kart ID ya da TC kimlik ile kaydedilmiş böyle bir kayıt zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        SqlCommand komut = new SqlCommand("update ogretmen_bilgi set kart_id=@kart,ogretmen_adi=@ad,ogretmen_soyadi=@soyad,ogretmen_tc=@tc where ogretmen_id=@id", baglanti); //öğretmen bilgilerinin güncellemesini sağlar
                        if (kisi_ara.id != -1 && listView2.SelectedItems.Count == 0)
                        {
                            komut.Parameters.AddWithValue("@id", kisi_ara.id);
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@id", listView2.SelectedItems[0].SubItems[4].Text);
                        }

                        komut.Parameters.AddWithValue("@ad", ogretmenAdiTxt.Text);
                        komut.Parameters.AddWithValue("@soyad", ogretmenSoyadTx.Text);
                        komut.Parameters.AddWithValue("@tc", tcTxt.Text);
                        komut.Parameters.AddWithValue("@kart", krtidTxt.Text);
                        komut.ExecuteNonQuery();
                        yenile2();
                        MessageBox.Show("Öğretmen Güncellenmiştir.");
                        baglanti.Close();
                    }
                }



            }
            catch (Exception hata)
            {

                MessageBox.Show("Hata;" + hata);
            } 
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz ?", "Onay Penceresi", MessageBoxButtons.YesNo);
                if (durum == DialogResult.Yes)
                {
                    if (listView2.CheckedItems.Count <= 0) // Eğer ListView'de seçilen veri sayısı 0 ise uyarı veriyor
                    {
                        MessageBox.Show("Değer seçiniz.");
                    }
                    else
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        foreach (ListViewItem item2 in listView2.CheckedItems)
                        {

                            SqlCommand komut3 = new SqlCommand("delete from giris_cikis_saat where kart_id = '" + item2.SubItems[0].Text + "'", baglanti);
                            komut3.ExecuteNonQuery();
                        }
                        baglanti.Close();
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        foreach (ListViewItem item in listView2.CheckedItems)
                        {

                          

                            SqlCommand komut = new SqlCommand("delete from ogretmen_bilgi where ogretmen_id = @id", baglanti);
                            komut.Parameters.AddWithValue("@id", item.SubItems[4].Text);
                            komut.ExecuteNonQuery();

                            item.SubItems.Clear();
                       
                        }
                        baglanti.Close();
                        yenile2();
                        MessageBox.Show("Kayıt silinmiştir.");
                    }


                }

            }

            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);

            }
        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox9.Text == "" || textBox9.Text == "TC Kimlik ile sorgula")
            {
                yenile2();
            }
            else
            {
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM ogretmen_bilgi WHERE ogretmen_tc LIKE '%" + textBox9.Text + "%'", baglanti); //Kişiyi TC kimlik nosuna göre bilgilerini getirir
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    try
                    {
                        ListViewItem ekle = new ListViewItem(okuyucu["kart_id"].ToString());
                        ekle.SubItems.Add(okuyucu["ogretmen_tc"].ToString());
                        ekle.SubItems.Add(okuyucu["ogretmen_adi"].ToString());
                        ekle.SubItems.Add(okuyucu["ogretmen_soyadi"].ToString());
                        ekle.SubItems.Add(okuyucu["ogretmen_id"].ToString());
                        listView2.Items.Add(ekle);

                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                    }

                }
                int i = 0;
                foreach (ListViewItem item in listView2.Items)
                {

                    if (i++ % 2 == 0)
                    {
                        item.BackColor = Color.DarkGray;
                        item.ForeColor = Color.White;
                    }
                }
                okuyucu.Close();
                baglanti.Close();
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)//Onay kutusu (checkBox) eğer seçiliyse
            {
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    listView2.Items[i].Checked = true; //Diğer bütün checkBox'ları seçili yap
                }
            }
            else
            {
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    listView2.Items[i].Checked = false; // Tümünü seç seçili değilse tümü seçilmesin
                }
            }
        }

        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)//Listview'de seçilen verinin sayısı 0'dan büyükse
            {
                button6.Enabled = true; // Güncelleme butonunun çalışabilmesini sağlar
                ListViewItem item = listView2.SelectedItems[0];
                tcTxt.Text = item.SubItems[1].Text; // seçilen bilgilerin textBoxlara yazılmasını sağlar
                krtidTxt.Text = item.SubItems[0].Text;
                ogretmenAdiTxt.Text = item.SubItems[2].Text;
                ogretmenSoyadTx.Text = item.SubItems[3].Text;
                tc = item.SubItems[1].Text;
            }
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                checkBox8.ForeColor = Color.Green;
            }
            else
            {
                checkBox8.ForeColor = Color.Red;

            }
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            if (ogrenciAdTx.Text == " ")
            {
                ogrenciAdTx.Clear();
            }
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            if (ogrenciSoyadTxt.Text == " ")
            {
                ogrenciSoyadTxt.Clear();
            }
        }

        private void listView11_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView11.SelectedItems.Count > 0)
            {
                button35.Enabled = true;
                ListViewItem item = listView11.SelectedItems[0];
                krtidstTx.Text = item.SubItems[0].Text;
                ogrenciNum.Text = item.SubItems[1].Text;
                ogrenciAdTx.Text = item.SubItems[2].Text;
                ogrenciSoyadTx.Text = item.SubItems[3].Text;
                emailTx.Text = item.SubItems[4].Text;
                sinifCm.Text = item.SubItems[6].Text;

                numara = Convert.ToInt32(item.SubItems[1].Text);
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                bool durum = true;
                SqlCommand komut2 = new SqlCommand("Select * From ogrenci_bilgi", baglanti);
                SqlDataReader rdr = komut2.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["kart_id"].ToString() == krtidstTx.Text || rdr["ogrenci_numara"].ToString() == ogrenciNum.Text) //Eklenmeye çalışan tc kimlik numarası ve kart numarası zaten varsa
                    {
                        durum = false;
                    }
                }
                baglanti.Close();
                if (krtidstTx.Text == "" && ogrenciAdTx.Text == "" && ogrenciSoyadTx.Text == "" && ogrenciNum.Text == "" && emailTx.Text == "" && sinifCm.SelectedIndex == -1) //Eğer verilerin ekleneceği textBoxlar boş ise
                {
                    MessageBox.Show("Bilgileri Giriniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (durum == false)
                {
                    MessageBox.Show("Kart ID ya da Öğrenci numarası ile kaydedilmiş böyle bir kayıt zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string posta = emailTx.Text;
                    Regex regex = new Regex(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$");//Belli şartlar içerisinde E-Mail formatında giriş yapılmasını sağlıyor
                    Match match = regex.Match(posta); //Eşleşmesi kontrol edilecek şart belirtiliyor
                    if (match.Success)//Eğer girilen mail gerçek mail formatındaysa 
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        SqlCommand komut = new SqlCommand("insert  into ogrenci_bilgi (ogrenci_numara,ogrenci_adi,ogrenci_soyadi,veli_eposta,kart_id,sinif,eposta_istek) values(@no,@ad,@soyad,@eposta,@kart,@sinif,@istek)", baglanti);
                        komut.Parameters.AddWithValue("@ad", ogrenciAdTx.Text);
                        komut.Parameters.AddWithValue("@soyad", ogrenciSoyadTx.Text);
                        komut.Parameters.AddWithValue("@no", ogrenciNum.Text);
                        komut.Parameters.AddWithValue("@eposta", emailTx.Text);
                        komut.Parameters.AddWithValue("@kart", krtidstTx.Text);
                        komut.Parameters.AddWithValue("@sinif", sinifCm.SelectedValue);
                        if (checkBox8.Checked == true)
                        {
                            komut.Parameters.AddWithValue("@istek", 1);
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@istek", 0);
                        }
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kart kaydedilmiştir.");
                        baglanti.Close();
                        yenile();
                        ogrenciAdTx.Text = "";
                        ogrenciSoyadTx.Text = "";
                        ogrenciNum.Text = "";
                        emailTx.Text = "";
                        krtidstTx.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("E-Mail formatı yanlış girildi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("Hata;" + hata);
            }
        }

        private void listView12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView12.SelectedItems.Count > 0)
            {
                button38.Enabled = true;
                ListViewItem item = listView12.SelectedItems[0];

                textBox31.Text = item.SubItems[1].Text;
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox4.Text == "Sınıf ara")
            {
                yenile3();
            }
            else
            {
                listView12.Items.Clear();
                baglanti.Open();
                int i = 0;
                SqlCommand komut = new SqlCommand("select * from sinif_tablosu where sinif like '%" + textBox4.Text + "%'", baglanti); //TextBox'a girilen değere göre
                SqlDataReader rdr = komut.ExecuteReader();
                while (rdr.Read())
                {
                    i++;
                    ListViewItem Ivi = new ListViewItem(i.ToString());
                    Ivi.SubItems.Add(rdr["sinif"].ToString());
                    Ivi.SubItems.Add(rdr["sinif_id"].ToString());
                    listView12.Items.Add(Ivi);
                }
                i = 0;
                foreach (ListViewItem item in listView3.Items)
                {

                    if (i++ % 2 == 0)
                    {
                        item.BackColor = Color.Gray;
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                        item.ForeColor = Color.Gray;
                    }
                }
                baglanti.Close();
            }
        }
 
        private void button39_Click_1(object sender, EventArgs e)
        {
            if (textBox31.Text == "")
            {
                MessageBox.Show("Sınıf Girmediniz, lütfen sınıf giriniz.", "Uyarı Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    bool durum = true;
                    SqlCommand komut2 = new SqlCommand("select * from sinif_tablosu", baglanti);
                    SqlDataReader rdr = komut2.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr["sinif"].ToString() == textBox31.Text)
                        {
                            durum = false;
                        }
                    }
                    baglanti.Close();
                    if (durum == false)
                    {
                        MessageBox.Show("Böyle bir sınıf zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        SqlCommand komut = new SqlCommand("INSERT INTO sinif_tablosu(sinif) values(@sinif)", baglanti);
                        komut.Parameters.AddWithValue("@sinif", textBox31.Text);

                        komut.ExecuteNonQuery();
                        MessageBox.Show("Sınıf eklenmiştir.");
                        textBox31.Text = "";

                        baglanti.Close();
                        yenile3();
                        yenile2();
                        yenile();
                        yenile4();
                        yenile5();
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata = " + hata.Message);
                }


            }
        }

        void yenile5()
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sinif_tablosu ", baglanti); //Sınıfların bulunduğu tablodan verileri çekiyor

            SqlDataAdapter da = new SqlDataAdapter(komut);//Çekilen verilerin DataReader'a doldurulmasını sağlar
            SqlDataReader dr = komut.ExecuteReader();
            Dictionary<string, string> dis = new Dictionary<string, string>(); // Sınıf bilgilerinin tutulacağı birnevi bir sözlük oluşturur
            while (dr.Read())
            {
                dis.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());//dr okundukça sınıf tablosundaki verilen comboBox'a aktarılmasını sağlar
            }
            sinifCm.DataSource = new BindingSource(dis, null); //comboBox'un veri kaynağını belirtiyor
            sinifCm.DisplayMember = "Value";
            sinifCm.ValueMember = "Key";
            baglanti.Close();

        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                bool durum = true;
                SqlCommand komut2 = new SqlCommand("select * from sinif_tablosu", baglanti);
                SqlDataReader rdr = komut2.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr["sinif"].ToString() == textBox31.Text)
                    {
                        durum = false;
                    }
                }
                baglanti.Close();

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                if (textBox12.Text == "")
                {
                    MessageBox.Show("Lütfen sınıf seçiniz");
                }
                else if (durum == false)
                {
                    MessageBox.Show("Böyle bir sınıf zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand komut = new SqlCommand("UPDATE sinif_tablosu SET sinif=@sinif where sinif_id=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", listView12.SelectedItems[0].SubItems[2].Text);
                    komut.Parameters.AddWithValue("@sinif", textBox31.Text);

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Sınıf güncellenmiştir.");


                    button11.Enabled = false;
                }
                baglanti.Close();
                yenile3();
                yenile2();
                yenile();
                yenile4();
                yenile5();

            }
            catch (Exception)
            {


            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            bool i = true;
            DialogResult durum = new DialogResult();
            if (listView12.CheckedItems.Count > 0)
            {
                durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz ?", "Onay Penceresi", MessageBoxButtons.YesNo);

            }
            if (durum == DialogResult.Yes)
            {

            }
            else
            {
                i = false;
            }

            if (listView12.CheckedItems.Count <= 0 && i == false)
            {
                MessageBox.Show("Değer seçiniz.");
            }
            else if (durum == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                int i3 = 0;
                foreach (ListViewItem item2 in listView12.CheckedItems)
                {

                    SqlCommand komut2 = new SqlCommand("select sinif from ogrenci_bilgi where sinif = '" + listView12.CheckedItems[i3].SubItems[2].Text + "'", baglanti);

                    i3++;
                    if (komut2.ExecuteScalar() == null)
                    {


                    }
                    else
                    {
                        int oku = (int)komut2.ExecuteScalar();
                        SqlCommand komut3 = new SqlCommand("delete from ogrenci_bilgi where sinif = '" + oku + "'", baglanti);
                        komut3.ExecuteNonQuery();
                    }

                }


                baglanti.Close();
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                foreach (ListViewItem item in listView12.CheckedItems)
                {


                    SqlCommand komut = new SqlCommand("delete from sinif_tablosu where sinif_id=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", item.SubItems[2].Text);
                    komut.ExecuteNonQuery();
                    item.SubItems.Clear();

                }
                baglanti.Close();
                listView1.Items.Clear();
                listView2.Items.Clear();
                yenile3();
                yenile2();
                yenile();
                yenile4();
                yenile5();
                MessageBox.Show("Sınıf silinmiştir.");

            }
        }

        private void textBox32_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox32.Text == "" || textBox32.Text == "İsime göre sorgula")
            {
                yenile4();
            }
            else
            {
                listView4.Items.Clear();
                baglanti.Open();
                int i = 0;
                SqlCommand komut = new SqlCommand("select giris_cikis_saat.Kart_id,ogrenci_adi,ogrenci_soyadi,giris_saat,cikis_saat,tarihi from giris_cikis_saat JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id where ogrenci_adi like '%" + textBox1.Text + "%'", baglanti);
                SqlDataReader rdr = komut.ExecuteReader();
                while (rdr.Read())
                {
                    i++;
                    ListViewItem ekle = new ListViewItem(rdr["Kart_id"].ToString());
                    ekle.SubItems.Add(rdr["ogrenci_adi"].ToString());
                    ekle.SubItems.Add(rdr["ogrenci_soyadi"].ToString());
                    ekle.SubItems.Add(rdr["giris_saat"].ToString());
                    ekle.SubItems.Add(rdr["cikis_saat"].ToString());
                    ekle.SubItems.Add(rdr["tarihi"].ToString());
                    listView16.Items.Add(ekle);
                }
                i = 0;
                foreach (ListViewItem item in listView16.Items)
                {

                    if (i++ % 2 == 0)
                    {
                        item.BackColor = Color.Gray;
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                        item.ForeColor = Color.Gray;
                    }
                }
                baglanti.Close();
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                bool durum = true;
                SqlCommand komut2 = new SqlCommand("SeLeCT * FrOm ogrenci_bilgi WHERE ogrenci_numara = " + ogrenciNum.Text + "", baglanti);
                SqlDataReader rdr = komut2.ExecuteReader();
                if (rdr.Read())
                {
                    durum = false;
                }

                baglanti.Close();
                if (durum == false && numara != Convert.ToInt32(ogrenciNum.Text))
                {
                    MessageBox.Show("Kart ID ya da Öğrenci numarası ile kaydedilmiş böyle bir kayıt zaten var.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut = new SqlCommand("update ogrenci_bilgi set ogrenci_adi=@ad,ogrenci_soyadi=@soyad,ogrenci_numara=@no,veli_eposta=@eposta,sinif=@sinif, eposta_istek=@istek where ogrenci_id=@id", baglanti);

                    if (kisi_ara.id != -1 && listView11.SelectedItems.Count == 0)
                    {
                        komut.Parameters.AddWithValue("@id", kisi_ara.id);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@id", listView11.SelectedItems[0].SubItems[5].Text);
                    }

                    komut.Parameters.AddWithValue("@ad", ogrenciAdTx.Text);
                    komut.Parameters.AddWithValue("@soyad", ogrenciSoyadTx.Text);
                    komut.Parameters.AddWithValue("@no", ogrenciNum.Text);
                    komut.Parameters.AddWithValue("@eposta", emailTx.Text);
                    komut.Parameters.AddWithValue("@kart", krtidstTx.Text);
                    komut.Parameters.AddWithValue("@sinif", sinifCm.SelectedValue);
                    if (checkBox8.Checked == true)
                    {
                        komut.Parameters.AddWithValue("@istek", 1);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@istek", 0);
                    }

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Öğrenciler Güncellenmiştir.");

                    baglanti.Close();
                    yenile();
                }

            }
            catch (Exception asd)
            {
                MessageBox.Show("Hata;" + asd);
            }
        }

        private void listView11_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listView11.CheckedIndices.Count == 0)
            {
                label72.Text = "0";
            }
            else
            {
                label72.Text = listView11.CheckedIndices.Count.ToString();
            }
        }

        private void listView11_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (listView11.Columns[5].Width > 0)
            {
                listView11.Columns[5].Width = 0;
            }
            listView11.Width = listView11.Columns[0].Width + listView11.Columns[1].Width + listView11.Columns[2].Width + listView11.Columns[3].Width + listView11.Columns[4].Width + listView11.Columns[6].Width;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            bool i = true;
            DialogResult durum = new DialogResult();
            if (listView11.CheckedItems.Count > 0)
            {
                durum = MessageBox.Show("Kaydı silmek istediğinizden emin misiniz ?", "Onay Penceresi", MessageBoxButtons.YesNo);

            }
            if (durum == DialogResult.Yes)
            {

            }
            else
            {
                i = false;
            }

            if (listView11.CheckedItems.Count <= 0 && i == false)
            {
                MessageBox.Show("Değer seçiniz.");
            }
            else if (durum == DialogResult.Yes)
            {

                foreach (ListViewItem item2 in listView11.CheckedItems)
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }


                    SqlCommand komut3 = new SqlCommand("delete from giris_cikis_saat where kart_id = '" + item2.SubItems[0].Text + "'", baglanti);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();

                }



                foreach (ListViewItem item in listView11.CheckedItems)
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }


                    SqlCommand komut = new SqlCommand("delete from ogrenci_bilgi where ogrenci_id=@id", baglanti);
                    komut.Parameters.AddWithValue("@id", item.SubItems[5].Text);
                    komut.ExecuteNonQuery();
                    item.SubItems.Clear();
                    baglanti.Close();
                }

                yenile();
                MessageBox.Show("Kayıt silinmiştir.");
                checkBox1.Checked = false;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                for (int i = 0; i < listView11.Items.Count; i++)
                {
                    listView11.Items[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < listView11.Items.Count; i++)
                {
                    listView11.Items[i].Checked = false;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }


        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked == true)//Onay kutusu (checkBox) eğer seçiliyse
            {
                for (int i = 0; i < listView12.Items.Count; i++)
                {
                    listView12.Items[i].Checked = true; //Diğer bütün checkBox'ları seçili yap
                }
            }
            else
            {
                for (int i = 0; i < listView12.Items.Count; i++)
                {
                    listView12.Items[i].Checked = false; // Tümünü seç seçili değilse tümü seçilmesin
                }
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Sınıf Ara")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Sınıf Ara";
                yenile3();
            }
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            textBox9.Clear();
        }

        private void textBox9_Leave_1(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "TC Kimlik İle Sorgula";
                yenile2();
            }
        }

        private void textBox9_Enter_1(object sender, EventArgs e)
        {
            if (textBox9.Text == "TC Kimlik İle Sorgula")
            {
                textBox9.Text = "";
                
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            if (textBox35.Text == "" || textBox35.Text == "Numarası İle Sorgula")
            {
                yenile();
            }
            else
            {
                listView11.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM ogrenci_bilgi WHERE ogrenci_numara LIKE '%" + textBox35.Text + "%'", baglanti);
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    try
                    {
                        ListViewItem ekle = new ListViewItem(okuyucu["kart_id"].ToString());
                        ekle.SubItems.Add(okuyucu["ogrenci_numara"].ToString());
                        ekle.SubItems.Add(okuyucu["ogrenci_adi"].ToString());
                        ekle.SubItems.Add(okuyucu["ogrenci_soyadi"].ToString());
                        ekle.SubItems.Add(okuyucu["sinif"].ToString());
                        ekle.SubItems.Add(okuyucu["veli_eposta"].ToString());
                        ekle.SubItems.Add(okuyucu["eposta_istek"].ToString());

                        listView11.Items.Add(ekle);

                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                    }

                }
                int i = 0;
                foreach (ListViewItem item in listView11.Items)
                {

                    if (i++ % 2 == 0)
                    {
                        item.BackColor = Color.DarkGray;
                        item.ForeColor = Color.White;
                    }
                }
                okuyucu.Close();
                baglanti.Close();
            }
        }

        private void textBox35_Leave(object sender, EventArgs e)
        {
            if (textBox35.Text == "")
            {
                textBox35.Text = "Numarası İle Sorgula";
                yenile();
            }
        }

        private void textBox35_Enter(object sender, EventArgs e)
        {
            if (textBox35.Text == "Numarası İle Sorgula")
            {
                textBox35.Text = "";
            }
        }

        private void textBox35_MouseClick(object sender, MouseEventArgs e)
        {
            textBox35.Clear();
        }
    }
}
