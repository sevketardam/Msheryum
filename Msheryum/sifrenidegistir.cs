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

namespace Msheryum
{
    public partial class sifrenidegistir : Form
    {
        public sifrenidegistir()
        {
            InitializeComponent();
        }
        public string kod { get; set; }
        public int id { get; set; }
        SqlConnection conn = new SqlConnection(@"Data Source =.\SQLEXPRESS01; Initial Catalog=Msheryum1; Integrated Security=true");
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text == kod && textBox2.Text == textBox3.Text)
            {
                MessageBox.Show(id.ToString());
                conn.Open();
                SqlCommand komut = new SqlCommand("update arayuz_sifre set admin_sifre=@p1 where admin_id = '" + id + "'", conn);
                komut.Parameters.AddWithValue("@p1",textBox3.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Şifreniz başarıyla değiştirildi Anasayfaya Yönlendiriliyorsunuz","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                girisEkrani ge = new girisEkrani();
                ge.Show();
                this.Hide();


                conn.Close();
            }
            else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Hiçbir alanı boş bırakmayınız.");
            }
        }
    }
}

