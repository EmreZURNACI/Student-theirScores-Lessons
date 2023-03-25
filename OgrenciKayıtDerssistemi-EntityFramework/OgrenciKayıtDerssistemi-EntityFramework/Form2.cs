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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            dersleriListele();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=EMREZURNACI;Initial Catalog=DbOgrenciDersler;Integrated Security=True");
        DbOgrenciDerslerEntities db = new DbOgrenciDerslerEntities();
        derslerTBL derslerTBL = new derslerTBL();
        private void button1_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select dersAd from derslerTBL where dersAd=@p1",sqlCon);
            command.Parameters.AddWithValue("@p1", textBox2.Text);
            SqlDataReader dr=command.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show($"{textBox2.Text} Dersi Sistemde Zaten Bulunmaktadır.\nLütfen Farklı Dersler Eklemeyi Deneyin.", "Ders Mevcuttur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxlariTemizle();
            }
            else
            {
                
                derslerTBL.dersAd = textBox2.Text;
                db.derslerTBLs.Add(derslerTBL);
                db.SaveChanges();
                MessageBox.Show($"{textBox2.Text} Dersi Sisteme Eklenmiştir.","Ders Ekleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dersleriListele();
                textBoxlariTemizle();
            }
            sqlCon.Close();
        }
        public void dersleriListele()
        {
            var degerler = from x in db.derslerTBLs
                           select new
                           {
                               x.dersID,
                               x.dersAd,
                           };
            dataGridView1.DataSource = degerler.ToList();
        }
        public void textBoxlariTemizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select dersID from derslerTBL where dersID=@p1", sqlCon);
            command.Parameters.AddWithValue("@p1", byte.Parse(textBox1.Text));
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                var degerler = db.derslerTBLs.Find(byte.Parse(textBox1.Text));
                db.derslerTBLs.Remove(degerler);
                db.SaveChanges();
                MessageBox.Show($"{textBox1.Text} ID'li Ders Sistemden Silinmiştir.", "Ders Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dersleriListele();
                textBoxlariTemizle();
            }
            else
            {
                MessageBox.Show($"{textBox1.Text} ID'li Ders Sistemde Mevcut Değildir.\nGeçerli Bir ID Değeri Giriniz.", "Ders Silme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxlariTemizle();
            }
            sqlCon.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select dersID from derslerTBL where dersID=@p1",sqlCon);
            command.Parameters.AddWithValue("@p1", byte.Parse(textBox1.Text));
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                if ( textBox2.Text != null && textBox2.Text != "" && textBox2.Text != " ")
                {
                    var satir = db.derslerTBLs.Find(byte.Parse(textBox1.Text));
                    satir.dersAd = textBox2.Text;
                    db.SaveChanges();
                    MessageBox.Show($"{textBox1.Text} ID'li Ders  {textBox2.Text}  Güncellenmiştir.", "Ders Güncellemiştir.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dersleriListele();
                    textBoxlariTemizle();
                }
                else
                {
                    throw new Exception("Boşlukları Eksiksiz Doldurunuz.");
                }
            }
            else
            {
                MessageBox.Show($"{textBox1.Text} ID'li Ders Sistemde Mevcut Değildir.\nGeçerli Bir ID Değeri Giriniz.", "Ders Güncelleme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dersleriListele();
                textBoxlariTemizle();
            }
            sqlCon.Close();
        }
    }
    
}
