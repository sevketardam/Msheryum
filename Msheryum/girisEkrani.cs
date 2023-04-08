using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing.Text;

namespace Msheryum
{
    public partial class girisEkrani : Form
    {
        public girisEkrani()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);


        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        SqlConnection baglanti = new SqlConnection(@"Data source = .\SQLEXPRESS01;Initial catalog = Msheryum1;Integrated security=true;"); //Veritabanı bağlantı kodu
        public static bool durum = true; //true ya da false değeri atanan bir değişken oluşturuyor ve başlangıçta true değeri veriliyor

        private void btnGiris_Click_1(object sender, EventArgs e)
        {
           
        }
      
        private void girisEkrani_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';

            byte[] fontData = Properties.Resources.Akrobat_Thin;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Akrobat_Thin.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Akrobat_Thin.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 16.0F);
            label9.Font = myFont;
            label8.Font = myFont;
            label3.Font = myFont;
            label2.Font = myFont;
            label1.Font = myFont;
            myFont = new Font(fonts.Families[0],14.0F);
            textBox1.Font = myFont;
            textBox2.Font = myFont;
            myFont = new Font(fonts.Families[0], 10.0F);
            label12.Font = myFont;
            myFont = new Font(fonts.Families[0], 13.0F);



            int i = 0;
             //Uygulamanın genişliğini belirtiyor
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from ogrenci_bilgi",baglanti); //ogrenci_bilgi adlı tablodan bütün verileri çekiyor
            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read()) 
            {
                i++; //Her öğrenci verisi okunduğunda i adlı değişkeni 1 arttırıyor
            }
            label8.Text = i.ToString(); //ve arttırılan değişkeni yazdırıyor
            baglanti.Close();
            i = 0; //öğrencileri okuduktan sonra sıfırlıyor
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from ogretmen_bilgi", baglanti); //ogretmen_bilgi adlı tablodan tüm verileri çekiyor
            SqlDataReader okuyucu2 = komut2.ExecuteReader();
            while (okuyucu2.Read())
            {
                i++;  //Her öğretmen verisi okunduğunda i adlı değişkeni 1 arttırıyor
            }
            label9.Text = i.ToString(); //kaç tane öğretmen bulunduğunu ekrana yazdırıyor

            baglanti.Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)//Fare imleci resimkutusunun üzerine geldiği zaman 
        {
            textBox2.PasswordChar = '\0';
            pictureBox2.Image = Msheryum.Properties.Resources.view; //şifre yazılan kutunun yanındaki resmin değişmesini sağlıyor
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)//Fare imleci resimkutusunun üzerinden ayrıldığında
        {
            textBox2.PasswordChar = '*';
            pictureBox2.Image = Msheryum.Properties.Resources.hide;
        }

        private void button3_Click(object sender, EventArgs e) //Yukarıdaki Butona tıklamayla aynı işlemleri yapıyor fakat başka bir menü açıyor
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Msheryum.Properties.Resources.istatistikhover;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Toplam Öğrenci Sayısı = " + label8.Text + "\n" + "Toplam Öğretmen Sayısı = " + label9.Text, "İstatistikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Msheryum.Properties.Resources.istatistik;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.Font = new Font(label12.Font.Name, label12.Font.SizeInPoints, FontStyle.Underline);
            label12.ForeColor = Color.RoyalBlue;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.Font = new Font(label12.Font.Name, label12.Font.SizeInPoints, FontStyle.Regular);
            label12.ForeColor = Color.CornflowerBlue;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox1.BackColor = Color.White;
                label2.ForeColor = Color.White;
                textBox2.BackColor = Color.Gainsboro;
                label3.ForeColor = Color.Gainsboro;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            label2.ForeColor = Color.White;
            textBox2.BackColor = Color.Gainsboro;
            label3.ForeColor = Color.Gainsboro;
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox2.BackColor = Color.Gainsboro;
                label2.ForeColor = Color.Gainsboro;
                textBox2.BackColor = Color.White;
                label3.ForeColor = Color.White;
                pictureBox2.BackColor = Color.White;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Gainsboro;
            label2.ForeColor = Color.Gainsboro;
            textBox2.BackColor = Color.White;
            label3.ForeColor = Color.White;
            pictureBox2.BackColor = Color.White;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from arayuz_sifre", baglanti); //Veritabanındaki arayuz_sifre adlı tablodan tüm verileri çekiyor
            SqlDataReader okuyucu = komut.ExecuteReader();

            while (okuyucu.Read()) // Yukarıdaki SqlDataReader sınıfından oluşturulan reader okunmaya devam ettikçe anlamına geliyor
            {
                if (textBox1.Text == okuyucu["admin_ad"].ToString() && textBox2.Text == okuyucu["admin_sifre"].ToString()) //Giriş yapılan kullanıcı adı ve şifre Veritabanındakilerle ile aynıysa yani bilgiler doğru yazılmışsa
                {
                    menu menu = new menu();
                    menu.Show();
                    this.Hide();
                }

            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Msheryum.Properties.Resources.girisyaphover;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Msheryum.Properties.Resources.girisyap;

        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Msheryum.Properties.Resources.kisiarahover;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Msheryum.Properties.Resources.kisiara;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from arayuz_sifre", baglanti);
            SqlDataReader reader = komut.ExecuteReader();


            while (reader.Read())
            {
                if (textBox1.Text == reader["admin_ad"].ToString() && textBox2.Text == reader["admin_sifre"].ToString())
                {
                    durum = false;
                    sorgu srg = new sorgu();
                    srg.ShowDialog();
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            sifremiunuttum su = new sifremiunuttum();
            su.ShowDialog();
        }

    
    }
}
