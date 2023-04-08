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
    public partial class sorgu : Form
    {
        public sorgu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data source = .\SQLEXPRESS01;Initial catalog = Msheryum1;Integrated security=true;");
        private void sorgu_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sinif_tablosu ", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            Dictionary<string, string> dis = new Dictionary<string, string>();
            while (dr.Read())
            {
                dis.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString());
            }
            comboBox1.DataSource = new BindingSource(dis, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            baglanti.Close();



            try
            {
                int i = 0;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut2 = new SqlCommand("select cikis_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,giris_saat,tarihi,giris_cikis_saat.kart_id,eposta_istek from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif", baglanti);
                SqlDataReader rdr2 = komut2.ExecuteReader();
               
                while (rdr2.Read())
                {
                    if (rdr2["giris_saat"].ToString() == "")
                    {

                    }
                    else
                    {
                        if (rdr2["cikis_saat"].ToString() != "")
                        {

                        }
                        else
                        {
                            
                            ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_numara"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_adi"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_soyadi"].ToString());
                            Ivi2.SubItems.Add(rdr2["sinif"].ToString());
                            Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                            Ivi2.SubItems.Add(rdr2["tarihi"].ToString());

                            listView1.Items.Add(Ivi2);
                            if (rdr2["eposta_istek"].ToString() == "True")
                            {
                                listView1.Items[i].Checked = true;
                            }
                            i++;
                        }

                      
                    }
                 

                }
                i = 0;
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("select giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id,eposta_istek from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif ", baglanti);
                SqlDataReader rdr3 = komut3.ExecuteReader();

                while (rdr3.Read())
                {
                    if (rdr3["cikis_saat"].ToString() == "")
                    {

                    }
                    else
                    {

                        ListViewItem Ivi3 = new ListViewItem(rdr3["Kart_id"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_numara"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_adi"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_soyadi"].ToString());
                        Ivi3.SubItems.Add(rdr3["sinif"].ToString());
                        Ivi3.SubItems.Add(rdr3["cikis_saat"].ToString());
                        Ivi3.SubItems.Add(rdr3["tarihi"].ToString());

                        listView2.Items.Add(Ivi3);
                        if (rdr3["eposta_istek"].ToString() == "True")
                        {
                            listView2.Items[i].Checked = true;
                        }
                        i++;
                    }
                 
                }

                baglanti.Close();
                if (listView1.Items.Count <= 0)
                {
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                else
                {
                    listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                if (listView2.Items.Count <= 0)
                {
                    listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                else
                {
                    listView2.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView2.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView2.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView2.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView2.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView2.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView2.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.ColumnContent);
                }





                listView1.Width = listView1.Columns[0].Width + listView1.Columns[1].Width + listView1.Columns[3].Width + listView1.Columns[4].Width + listView1.Columns[5].Width + listView1.Columns[6].Width;
                listView2.Width = listView2.Columns[0].Width + listView2.Columns[1].Width + listView2.Columns[3].Width + listView2.Columns[4].Width + listView2.Columns[5].Width + listView2.Columns[6].Width;



            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }
        static bool change;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (change == false)
                {
                    label3.Visible = false;
                    comboBox1.Visible = false;
                    listView3.Items.Clear();
                    listView4.Items.Clear();
                    listView1.Visible = false;
                    listView2.Visible = false;
                    listView3.Visible = true;
                    listView4.Visible = true;
                    label1.Text = "Okulda Olan Öğretmenler";
                    label2.Text = "Okulda Olmayan Öğretmenler";
                    label5.Text = "TC Kimlik:";
                    
                    button1.Text = "Öğrenci Bilgisi";
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut = new SqlCommand("select ogretmen_tc,giris_saat,giris_cikis_saat.Kart_id,ogretmen_adi,ogretmen_soyadi,ogretmen_tc,cikis_saat,tarihi from giris_cikis_saat JOIN ogretmen_bilgi on ogretmen_bilgi.kart_id = giris_cikis_saat.Kart_id  ", baglanti);
                    SqlDataReader rdr = komut.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr["cikis_saat"].ToString() == "")
                        {

                        }
                        else
                        {


                            ListViewItem Ivi = new ListViewItem(rdr["Kart_id"].ToString());
                            Ivi.SubItems.Add(rdr["ogretmen_tc"].ToString());
                            Ivi.SubItems.Add(rdr["ogretmen_adi"].ToString());
                            Ivi.SubItems.Add(rdr["ogretmen_soyadi"].ToString());
                            Ivi.SubItems.Add(rdr["cikis_saat"].ToString());
                            Ivi.SubItems.Add(rdr["tarihi"].ToString());

                            listView3.Items.Add(Ivi);
                        }

                    }
                    baglanti.Close();

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut2 = new SqlCommand("select ogretmen_tc,cikis_saat,giris_cikis_saat.Kart_id,ogretmen_adi,ogretmen_soyadi,ogretmen_tc,giris_saat,cikis_saat,tarihi from giris_cikis_saat JOIN ogretmen_bilgi on ogretmen_bilgi.kart_id = giris_cikis_saat.Kart_id  ", baglanti);
                    SqlDataReader rdr2 = komut2.ExecuteReader();

                    while (rdr2.Read())
                    {
                        if (rdr2["giris_saat"].ToString() == "")
                        {

                        }
                        else
                        {
                            if (rdr2["cikis_saat"].ToString() != "")
                            {

                            }
                            else
                            {
                                ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogretmen_tc"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogretmen_adi"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogretmen_soyadi"].ToString());
                                Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                                Ivi2.SubItems.Add(rdr2["tarihi"].ToString());

                                listView4.Items.Add(Ivi2);
                            }

                        }

                    }
                    baglanti.Close();
                    change = true;

                    if (listView4.Items.Count <= 0)
                    {
                        listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }
                    else
                    {
                        listView4.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                        listView4.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView4.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView4.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView4.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize);
                        listView4.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent);
                    }

                    if (listView3.Items.Count <= 0)
                    {
                        listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }
                    else
                    {
                        listView3.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                        listView3.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView3.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView3.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView3.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize);
                        listView3.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent);
                    }

                    listView3.Width = listView3.Columns[0].Width + listView3.Columns[1].Width + listView3.Columns[3].Width + listView3.Columns[4].Width + listView3.Columns[5].Width;
                    listView4.Width = listView4.Columns[0].Width + listView4.Columns[1].Width + listView4.Columns[3].Width + listView4.Columns[4].Width + listView4.Columns[5].Width;

                }
                else if (change == true)
                {
                    label3.Visible = true;
                    comboBox1.Visible = true;
                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    listView3.Visible = false;
                    listView4.Visible = false;
                    listView1.Visible = true;
                    listView2.Visible = true;
                    label1.Text = "Okulda Olan Öğrenciler";
                    label2.Text = "Okulda Olmayan Öğrenciler";
                    button1.Text = "Öğretmen Bilgisi";
                    label5.Text = "Numarası:";
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand komut2 = new SqlCommand("select cikis_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,giris_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif", baglanti);
                    SqlDataReader rdr2 = komut2.ExecuteReader();

                    while (rdr2.Read())
                    {
                        if (rdr2["giris_saat"].ToString() == "")
                        {

                        }
                        else
                        {
                            if (rdr2["cikis_saat"].ToString() != "")
                            {

                            }
                            else
                            {
                                ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogrenci_numara"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogrenci_adi"].ToString());
                                Ivi2.SubItems.Add(rdr2["ogrenci_soyadi"].ToString());
                                Ivi2.SubItems.Add(rdr2["sinif"].ToString());
                                Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                                Ivi2.SubItems.Add(rdr2["tarihi"].ToString());
                                listView1.Items.Add(Ivi2);
                            }


                        }

                    }

                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("select giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif ", baglanti);
                    SqlDataReader rdr3 = komut3.ExecuteReader();

                    while (rdr3.Read())
                    {
                        if (rdr3["cikis_saat"].ToString() == "")
                        {

                        }
                        else
                        {

                            ListViewItem Ivi3 = new ListViewItem(rdr3["Kart_id"].ToString());
                            Ivi3.SubItems.Add(rdr3["ogrenci_numara"].ToString());
                            Ivi3.SubItems.Add(rdr3["ogrenci_adi"].ToString());
                            Ivi3.SubItems.Add(rdr3["ogrenci_soyadi"].ToString());
                            Ivi3.SubItems.Add(rdr3["sinif"].ToString());
                            Ivi3.SubItems.Add(rdr3["cikis_saat"].ToString());
                            Ivi3.SubItems.Add(rdr3["tarihi"].ToString());

                            listView2.Items.Add(Ivi3);

                        }

                    }

                    baglanti.Close();
                    change = false;
                    listView1.Width = listView1.Columns[0].Width + listView1.Columns[1].Width + listView1.Columns[3].Width + listView1.Columns[4].Width + listView1.Columns[5].Width + listView1.Columns[6].Width;
                    listView2.Width = listView2.Columns[0].Width + listView2.Columns[1].Width + listView2.Columns[3].Width + listView2.Columns[4].Width + listView2.Columns[5].Width + listView2.Columns[6].Width;
                }
            }
            catch (Exception AS)
            {

                MessageBox.Show(AS.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex < 0)
            {

            }
            else
            {
                listView1.Items.Clear();
                listView2.Items.Clear();
                listView3.Items.Clear();
                listView4.Items.Clear();

                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("select giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif WHERE sinif_id LIKE '" + comboBox1.SelectedValue + "'  ", baglanti);
                SqlDataReader rdr2 = komut2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (rdr2["giris_saat"].ToString() == "")
                    {

                    }
                    else
                    {
                        if (rdr2["cikis_saat"].ToString() != "")
                        {

                        }
                        else
                        {
                            ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_numara"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_adi"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_soyadi"].ToString());
                            Ivi2.SubItems.Add(rdr2["sinif"].ToString());
                            Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                            Ivi2.SubItems.Add(rdr2["tarihi"].ToString());
                            listView1.Items.Add(Ivi2);
                        }


                    }

                }
                baglanti.Close();

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut3 = new SqlCommand("select giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif WHERE sinif_id LIKE '" + comboBox1.SelectedValue + "'  ", baglanti);
                SqlDataReader rdr3 = komut3.ExecuteReader();

                while (rdr3.Read())
                {
                    if (rdr3["cikis_saat"].ToString() == "")
                    {

                    }
                    else
                    {

                        ListViewItem Ivi3 = new ListViewItem(rdr3["Kart_id"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_numara"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_adi"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_soyadi"].ToString());
                        Ivi3.SubItems.Add(rdr3["sinif"].ToString());
                        Ivi3.SubItems.Add(rdr3["cikis_saat"].ToString());
                        Ivi3.SubItems.Add(rdr3["tarihi"].ToString());

                        listView2.Items.Add(Ivi3);


                    }

                }
                baglanti.Close();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();
            if (label5.Text == "Numarası:")
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("select eposta_istek,giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif WHERE ogrenci_numara LIKE '%" + textBox1.Text + "%'  ", baglanti);
                SqlDataReader rdr2 = komut2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (rdr2["giris_saat"].ToString() == "")
                    {

                    }
                    else
                    {
                        if (rdr2["cikis_saat"].ToString() != "")
                        {

                        }
                        else
                        {
                            ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_numara"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_adi"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogrenci_soyadi"].ToString());
                            Ivi2.SubItems.Add(rdr2["sinif"].ToString());
                            Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                            Ivi2.SubItems.Add(rdr2["tarihi"].ToString());
                            listView1.Items.Add(Ivi2);
                            if (rdr2["eposta_istek"].ToString() == "True")
                            {
                                listView1.Items[i].Checked = true;
                            }
                        }


                    }
                    i++;

                }
                baglanti.Close();
                i = 0;
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut3 = new SqlCommand("select eposta_istek,giris_saat,ogrenci_adi,ogrenci_soyadi,sinif_tablosu.sinif,veli_eposta,ogrenci_numara,cikis_saat,tarihi,giris_cikis_saat.kart_id from giris_cikis_saat  JOIN ogrenci_bilgi on ogrenci_bilgi.kart_id = giris_cikis_saat.Kart_id JOIN sinif_tablosu on sinif_tablosu.sinif_id = ogrenci_bilgi.sinif WHERE ogrenci_numara LIKE '%" + textBox1.Text + "%'  ", baglanti);
                SqlDataReader rdr3 = komut3.ExecuteReader();

                while (rdr3.Read())
                {
                    if (rdr3["cikis_saat"].ToString() == "")
                    {

                    }
                    else
                    {

                        ListViewItem Ivi3 = new ListViewItem(rdr3["Kart_id"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_numara"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_adi"].ToString());
                        Ivi3.SubItems.Add(rdr3["ogrenci_soyadi"].ToString());
                        Ivi3.SubItems.Add(rdr3["sinif"].ToString());
                        Ivi3.SubItems.Add(rdr3["cikis_saat"].ToString());
                        Ivi3.SubItems.Add(rdr3["tarihi"].ToString());

                        listView2.Items.Add(Ivi3);

                        if (rdr3["eposta_istek"].ToString() == "True")
                        {
                            listView2.Items[i].Checked = true;
                        }


                    }
                    i++;
                }
                baglanti.Close();
            }
            else if(label5.Text == "TC Kimlik:")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut = new SqlCommand("select ogretmen_tc,giris_saat,giris_cikis_saat.Kart_id,ogretmen_adi,ogretmen_soyadi,ogretmen_tc,cikis_saat,tarihi from giris_cikis_saat JOIN ogretmen_bilgi on ogretmen_bilgi.kart_id = giris_cikis_saat.Kart_id where ogretmen_tc LIKE '%"+textBox1.Text+"%' ", baglanti);
                SqlDataReader rdr = komut.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr["cikis_saat"].ToString() == "")
                    {

                    }
                    else
                    {


                        ListViewItem Ivi = new ListViewItem(rdr["Kart_id"].ToString());
                        Ivi.SubItems.Add(rdr["ogretmen_tc"].ToString());
                        Ivi.SubItems.Add(rdr["ogretmen_adi"].ToString());
                        Ivi.SubItems.Add(rdr["ogretmen_soyadi"].ToString());
                        Ivi.SubItems.Add(rdr["cikis_saat"].ToString());
                        Ivi.SubItems.Add(rdr["tarihi"].ToString());

                        listView3.Items.Add(Ivi);
                    }

                }
                baglanti.Close();

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand komut2 = new SqlCommand("select ogretmen_tc,cikis_saat,giris_cikis_saat.Kart_id,ogretmen_adi,ogretmen_soyadi,ogretmen_tc,giris_saat,cikis_saat,tarihi from giris_cikis_saat JOIN ogretmen_bilgi on ogretmen_bilgi.kart_id = giris_cikis_saat.Kart_id where ogretmen_tc LIKE '%"+textBox1.Text+"%' ", baglanti);
                SqlDataReader rdr2 = komut2.ExecuteReader();

                while (rdr2.Read())
                {
                    if (rdr2["giris_saat"].ToString() == "")
                    {

                    }
                    else
                    {
                        if (rdr2["cikis_saat"].ToString() != "")
                        {

                        }
                        else
                        {
                            ListViewItem Ivi2 = new ListViewItem(rdr2["Kart_id"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogretmen_tc"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogretmen_adi"].ToString());
                            Ivi2.SubItems.Add(rdr2["ogretmen_soyadi"].ToString());
                            Ivi2.SubItems.Add(rdr2["giris_saat"].ToString());
                            Ivi2.SubItems.Add(rdr2["tarihi"].ToString());

                            listView4.Items.Add(Ivi2);
                        }

                    }

                }
                baglanti.Close();
            }

        }


    }
}
