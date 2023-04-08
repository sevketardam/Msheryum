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
using System.Drawing.Text;
using System.Net.Mail;
using System.Net;

namespace Msheryum
{
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source = .\SQLEXPRESS01; Initial Catalog = Msheryum1; Integrated Security = true;");
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Msheryum.Properties.Resources.onaylahover;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Msheryum.Properties.Resources.onayla;

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Msheryum.Properties.Resources.kodistehover;

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Msheryum.Properties.Resources.kodiste;

        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);


        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        private void sifremiunuttum_Load(object sender, EventArgs e)
        {
            pictureBox2.Image = Msheryum.Properties.Resources.kodisteenable;

            byte[] fontData = Properties.Resources.Akrobat_Thin;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Akrobat_Thin.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Akrobat_Thin.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            label1.Visible = false;
            myFont = new Font(fonts.Families[0], 10.0F);
            label1.Font = myFont;
            myFont = new Font(fonts.Families[0], 16.0F);
            textBox1.Font = myFont;
            myFont = new Font(fonts.Families[0], 14.0F);
            textBox2.Font = myFont;
        }
        int cekid;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("Select * from arayuz_sifre  where admin_ad = '" + textBox1.Text + "'", conn);
            SqlDataReader commandr = command.ExecuteReader();
            if (commandr.Read())
            {
                cekid = Convert.ToInt32(commandr["admin_id"]);
                textBox3.Visible = false;
                textBox1.Enabled = false;
                
pictureBox1.Enabled = false;

            }
            else 
            {
                textBox1.Clear();
                label1.Visible = true;

            }
            conn.Close();
        }
        public static string code;
        int randomgenerate;
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        Random rand = new Random();

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand sendcode = new SqlCommand("Select * from arayuz_sifre where admin_id = '" + cekid + "'", conn);
            SqlDataReader sendcodedr = sendcode.ExecuteReader();
            if (sendcodedr.Read())
            {
                if (textBox2.Text == sendcodedr["ePosta"].ToString())
                {
                    string cekeposta = sendcodedr["ePosta"].ToString();
                    do
                    {
                        randomgenerate = rand.Next(0, 10);
                        if (randomgenerate % 2 == 0)
                        {
                            int randomletter = rand.Next(0, 26);
                            code += letters[randomletter];
                        }
                        else
                        {
                            int random = rand.Next(0, 10);
                            code += random;
                        }
                    }
                    while (code.Length < 6);
                    MessageBox.Show(code); //Oluşturulan Kodu Ekranda Gösterir.

                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp.gmail.com";
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential("msheryum.projesi@gmail.com", "erdo#meto#sevket#duzceartvin123");

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("msheryum.projesi@gmail.com", "Msheryum");
                    mail.To.Add(cekeposta);
                    mail.Subject = "Şifre Değiştirme Kodu.";
                    mail.IsBodyHtml = true;
                    mail.Body = "Şifrenizi Değiştirmek İçin Kodunuz : " + code;
                    try
                    {

                        sc.Send(mail);
                        MessageBox.Show("Eposta Adresinize Şifre Yenileme Kodu Gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
                        
                        sifrenidegistir sd = new sifrenidegistir();
                        sd.kod = code;
                        sd.id = cekid;
                        sd.ShowDialog();
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Kod Gönderilemedi Lütfen Tekrar Deneyiniz. \n\n" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Eposta Adresi Girdiğiniz Kullanıcı Adıyla Uyuşmuyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                }
            }
            else 
            {
                MessageBox.Show("Hata", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }




    }
}
