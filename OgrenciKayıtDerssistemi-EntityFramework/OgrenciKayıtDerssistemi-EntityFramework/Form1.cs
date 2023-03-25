using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciKayıtDerssistemi_EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbOgrenciDerslerEntities DbogrenciDers=new DbOgrenciDerslerEntities();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DegerleriListele();
        }

            SqlConnection sqlCon = new SqlConnection(@"Data Source=EMREZURNACI;Initial Catalog=DbOgrenciDersler;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select ogrenciAd,ogrenciSoyad,ogrenciNo,ogrenciBolum from ogrenciTBL where ogrenciAd=@p1 and ogrenciSoyad=@p2 and ogrenciNo=@p3 and ogrenciBolum=@p4", sqlCon);
            command.Parameters.AddWithValue("@p1", txtName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", txtNumber.Text);
            command.Parameters.AddWithValue("@p4", txtProgram.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Bu Öğrenci Sistemde Mevcuttur..");
            }
            else
            {
                if ((txtName.Text!="")&&(txtName.Text!=" ") && (txtName.Text != null)&& (txtLastName.Text != "") && (txtLastName.Text != " ") && (txtLastName.Text != null)&& (txtNumber.Text != "") && (txtNumber.Text != " ") && (txtNumber.Text != null)&&(txtProgram.Text != "") && (txtProgram.Text != " ") && (txtProgram.Text != null))
                {
                    ogrenciTBL ogrencitbl = new ogrenciTBL
                    {
                        ogrenciAd = txtName.Text,
                        ogrenciSoyad = txtLastName.Text,
                        ogrenciNo = txtNumber.Text,
                        ogrenciBolum = txtProgram.Text,
                        ogrenciTamAD = txtName.Text + " " + txtLastName.Text

                    };
                    DbogrenciDers.ogrenciTBLs.Add(ogrencitbl);
                    DbogrenciDers.SaveChanges();
                    MessageBox.Show("Yeni Öğrencimiz Sisteme Kaydedilmiştir..");
                }
                else
                {
                    throw new Exception("Boşlukları Eksiksiz Doldurunuz.");
                }
                
            }
                sqlCon.Close();
            TextBoxlarıTemizle();
            DegerleriListele();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select ogrenciID from ogrenciTBL where ogrenciID=@p1",sqlCon);
            command.Parameters.AddWithValue("@p1", txtID.Text);
            SqlDataReader dr=command.ExecuteReader();
            if(dr.Read()&&txtID.Text!=null)
            {
                var index = DbogrenciDers.ogrenciTBLs.Find(int.Parse(txtID.Text));
                DbogrenciDers.ogrenciTBLs.Remove(index);
                DbogrenciDers.SaveChanges();
                MessageBox.Show("Seçilen ID'li Öğrenci Sistemden Silinmiştir...");
                
            }
            else
            {
                MessageBox.Show("Bu ID'li Öğrenci Sistemde Mevcut Değildir. \nLütfen Geçerli Bir ID Değeri Giriniz...");
            }
            sqlCon.Close();
            TextBoxlarıTemizle();
            DegerleriListele();
        }
        public void TextBoxlarıTemizle()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            txtNumber.Text = "";
            txtProgram.Text = "";
        }
        public void DegerleriListele()
        {
            var degerler = from x in DbogrenciDers.ogrenciTBLs
                           select new
                           {
                               x.ogrenciID,
                               x.ogrenciAd,
                               x.ogrenciSoyad,
                               x.ogrenciNo,
                               x.ogrenciBolum,
                           };
            dataGridView1.DataSource = degerler.ToList();
        }
        public string ogrenciAd;
        public string ogrenciSoyad;
        public string ogrenciBolum;
        public string ogrenciNo;
         public string ogrenciTamAD;
        private void button4_Click(object sender, EventArgs e)
        {
            
          
            sqlCon.Open();
            SqlCommand command2 = new SqlCommand("Select * from ogrenciTBL where ogrenciID=@p1", sqlCon);
            command2.Parameters.AddWithValue("@p1", txtID.Text);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                ogrenciAd = dr2[1].ToString();
                ogrenciSoyad = dr2[2].ToString();
                ogrenciNo = dr2[3].ToString();
                ogrenciBolum = dr2[4].ToString();
                ogrenciTamAD = dr2[5].ToString();
            }



            sqlCon.Close();
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select ogrenciID from ogrenciTBL where ogrenciID=@p1", sqlCon);
            command.Parameters.AddWithValue("@p1", txtID.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read() && (txtID.Text != "" || txtID.Text != " ") )
            {
                var degerler = DbogrenciDers.ogrenciTBLs.Find(int.Parse(txtID.Text));
                if (txtName.Text==""|| txtName.Text == " ")
                {
                    degerler.ogrenciAd = ogrenciAd;
                }
                else
                {
                    degerler.ogrenciAd = txtName.Text;
                }

                if ( txtLastName.Text == "" || txtLastName.Text == " ")
                {
                    degerler.ogrenciSoyad = ogrenciSoyad;
                }
                else
                {
                    degerler.ogrenciSoyad = txtLastName.Text;
                }

                if ( txtProgram.Text == "" || txtProgram.Text == " ")
                {
                    degerler.ogrenciBolum = ogrenciBolum;
                }
                else
                {
                    degerler.ogrenciBolum = txtProgram.Text;
                }

                if ( txtNumber.Text == "" || txtNumber.Text == " ")
                {
                    degerler.ogrenciNo = ogrenciNo;
                }
                else
                {
                    degerler.ogrenciNo = txtNumber.Text;
                }

                if ((txtName.Text == ""&& txtLastName.Text == "") || (txtName.Text == " "&& txtLastName.Text == " "))
                {
                    degerler.ogrenciTamAD = ogrenciTamAD;
                }
                else
                {
                    degerler.ogrenciTamAD = txtName.Text + " " + txtLastName.Text;
                }
                DbogrenciDers.SaveChanges();
                MessageBox.Show("Seçilen ID'li Öğrencinin Bilgileri Güncellenmiştir...");

            }
            else
            {
                MessageBox.Show("Bu ID'li Öğrenci Sistemde Mevcut Değildir. \nLütfen Geçerli Bir ID Değeri Giriniz...");
            }
            sqlCon.Close();
            TextBoxlarıTemizle();
            DegerleriListele();
           
        }
    }
}
