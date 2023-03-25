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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OgrenciKayıtDerssistemi_EntityFramework
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private static readonly SqlConnection sqlConnection = new SqlConnection(@"Data Source=EMREZURNACI;Initial Catalog=DbOgrenciDersler;Integrated Security=True");
        SqlConnection sqlCon = sqlConnection;
        DbOgrenciDerslerEntities db = new DbOgrenciDerslerEntities();
        notlarTBL notlarTbl = new notlarTBL();
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        public void degerleriListele()
        {
            var degerler = from x in db.notlarTBLs
                           select new
                           {
                               x.ogrenciID,
                               x.ogrenciTBL.ogrenciTamAD,
                               x.derslerTBL.dersAd,
                               x.vizeNot,
                               x.finalNot,
                               x.notOrtalaması,
                               x.gecmeDurumu
                           };
            dataGridView1.DataSource = degerler.ToList();
        }
        public void textBoxlariTemizle()
        {
            txtID.Text = "";
            txtDurum.Text = "";
            txtOrt.Text = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            numericUpDown1.Value = 00;
            numericUpDown2.Value = 00;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = db.ogrenciTBLs.ToList();
            comboBox1.ValueMember = "ogrenciID";
            comboBox1.DisplayMember = "ogrenciTamAD";
            comboBox2.DataSource = db.derslerTBLs.ToList();
            comboBox2.ValueMember = "dersID";
            comboBox2.DisplayMember = "dersAd";
            degerleriListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            if (comboBox1.SelectedText != null && comboBox2.SelectedText != null)
            {

                notlarTbl.ogrenci = int.Parse((comboBox1.SelectedValue).ToString());
                notlarTbl.ders = byte.Parse((comboBox1.SelectedValue).ToString());
                notlarTbl.vizeNot = (numericUpDown1.Value);
                notlarTbl.finalNot = (numericUpDown2.Value);
                decimal ort = (((numericUpDown1.Value) * Convert.ToDecimal(0.4)) + ((numericUpDown2.Value) * Convert.ToDecimal(0.6)));
                notlarTbl.notOrtalaması = ort;
                txtOrt.Text = ort.ToString();
                if (ort < Convert.ToDecimal(44.5))
                {
                    txtDurum.Text = "KALDI";
                    notlarTbl.gecmeDurumu = "KALDI";
                }
                else
                {
                    txtDurum.Text = "GEÇTİ";
                    notlarTbl.gecmeDurumu = "GEÇTİ";
                }
                db.notlarTBLs.Add(notlarTbl);
                db.SaveChanges();
                MessageBox.Show($"{(comboBox1.Text).ToString()} 'ın Notları ve Durumu Kaydedilmiştir.");
                degerleriListele();
                textBoxlariTemizle();
            }
            else
            {
                MessageBox.Show($"Lütfen Gerekli Bilgileri Doldurun.");
                degerleriListele();
                textBoxlariTemizle();
            }
            sqlCon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select ogrenciID from notlarTBL where ogrenciID=@p1",sqlCon);
            command.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
            SqlDataReader dr=command.ExecuteReader();
            if (dr.Read())
            {
                db.notlarTBLs.Remove(db.notlarTBLs.Find(int.Parse(txtID.Text)));
                db.SaveChanges();
                MessageBox.Show($"{txtID.Text} ID'li Öğrencinin Bilgileri Not Sisteminden Silinmiştir.", "Not Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{txtID.Text} ID'li Öğrencinin Bilgileri Not Sistemde Mevcut Değildir.\nGeçerli Bir ID Değeri Giriniz.", "Not Silme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            sqlCon.Close();
            textBoxlariTemizle();
            degerleriListele();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select ogrenciID from notlarTBL where ogrenciID=@p1", sqlCon);
            command.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read() && (txtID.Text != "" || txtID.Text != " "))
            {
                var degerler = db.notlarTBLs.Find(int.Parse(txtID.Text));
                degerler.ogrenci = int.Parse((comboBox1.SelectedValue).ToString());
                degerler.ders = byte.Parse((comboBox2.SelectedValue).ToString());
                degerler.vizeNot = (numericUpDown1.Value);
                degerler.finalNot = (numericUpDown2.Value);
                decimal ort = (((numericUpDown1.Value) * Convert.ToDecimal(0.4)) + ((numericUpDown2.Value) * Convert.ToDecimal(0.6)));
                degerler.notOrtalaması = ort;
                if (ort < Convert.ToDecimal(44.5))
                {
                    degerler.gecmeDurumu = "KALDI";
                }
                else
                {
                    degerler.gecmeDurumu = "GEÇTİ";
                }
                db.SaveChanges();
                textBoxlariTemizle();
                degerleriListele();
                MessageBox.Show("Seçilen ID'li Öğrencinin Bilgileri Güncellenmiştir...");

            }
            else
            {
                MessageBox.Show("Bu ID'li Öğrenci Sistemde Mevcut Değildir. \nLütfen Geçerli Bir ID Değeri Giriniz...");
                textBoxlariTemizle();
                degerleriListele();
            }
            sqlCon.Close();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            numericUpDown1.Value= decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            numericUpDown2.Value = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtOrt.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }
    }
}
