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
    public partial class sinif_islemleri : Form
    {
        public sinif_islemleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data source = .\SQLEXPRESS;Initial catalog = Msheryum1;Integrated security=true;");

        private void sinif_islemleri_Load(object sender, EventArgs e)
        {
            yenile();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Sınıf ara")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = Color.Gray;
                textBox2.Text = "Sınıf ara";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLE BUTONU

            if (textBox1.Text == "")
            {
                MessageBox.Show("Sınıf Girmediniz, lütfen sınıf giriniz.","Uyarı Penceresi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            baglanti.Open();
            try
            {
                SqlCommand komut = new SqlCommand("INSERT INTO sinif_tablosu(sinif) values(@sinif)", baglanti);
                komut.Parameters.AddWithValue("@sinif", textBox1.Text);
             
                komut.ExecuteNonQuery();
                yenile();
            }
            catch(Exception hata)
            {
                MessageBox.Show("Hata = "+hata.Message);
            }
            finally
            {
                baglanti.Close();
            }


        }
        void yenile()
        {
            listView1.Items.Clear();


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
                listView1.Items.Add(Ivi);
            }
            i = 0;
            foreach (ListViewItem item in listView1.Items)
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

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            try
            {
                SqlCommand komut = new SqlCommand("UPDATE sinif_tablosu SET sinif=@sinif where sinif_id=@id",baglanti);
                komut.Parameters.AddWithValue("@id", listView1.SelectedItems[0].SubItems[2].Text);
                komut.Parameters.AddWithValue("@sinif",textBox1.Text);
                komut.ExecuteNonQuery();
                yenile();

            }
            catch (Exception)
            {

                
            }
            baglanti.Close();

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            bool i = true;
            DialogResult durum = new DialogResult();
            if (listView1.CheckedItems.Count >0)
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

            if (listView1.CheckedItems.Count <= 0 && i == false)
            {
                MessageBox.Show("Değer seçiniz.");
            }
            else if (durum == DialogResult.Yes)
            {
                    foreach (ListViewItem item in listView1.CheckedItems)
                    {

                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        SqlCommand komut = new SqlCommand("delete from sinif_tablosu where sinif_id=@id", baglanti);
                        komut.Parameters.AddWithValue("@id", item.SubItems[2].Text);
                        komut.ExecuteNonQuery();
                        item.SubItems.Clear();
                        baglanti.Close();
                    }
                    yenile();
                    MessageBox.Show("Kayıt silinmiştir.");
            }
        }
    }
}
