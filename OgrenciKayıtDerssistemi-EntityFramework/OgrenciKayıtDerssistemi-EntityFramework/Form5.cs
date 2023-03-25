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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }


        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection("Data Source=EMREZURNACI;Initial Catalog=DbOgrenciDersler;Integrated Security=True");
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select * from adminTBL where kullaniciAdi=@p1 and password=@p2", sqlCon);
            command.Parameters.AddWithValue("@p1", txtUsername.Text);
            command.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read()&&txtKod.Text==kod)
            {
                Form4 frm = new Form4();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girdiğiniz bilgiler eksik veya hatalıdır","Giriş Başarısız",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            sqlCon.Close();
        }
        public string kod = null;
        private void Form5_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            kod = random.Next(100000,999999).ToString();
            txtKod.Text = kod;
        }
    }
}
