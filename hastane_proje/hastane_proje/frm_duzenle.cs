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

namespace hastane_proje
{
    public partial class frm_duzenle : Form
    {
        public frm_duzenle()
        {
            InitializeComponent();
        }
        public string tcno;
        sqlbaglanti bgl=new sqlbaglanti();
        private void frm_duzenle_Load(object sender, EventArgs e)
        {
            txttc.Text = tcno;
            SqlCommand komut=new SqlCommand("Select * from tbl_hasta where hastatc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txttc.Text);
            SqlDataReader dataReader = komut.ExecuteReader();
            while (dataReader.Read())
            {
                txtad.Text = dataReader[1].ToString();
                txtsoyad.Text = dataReader[2].ToString();
                txttel.Text = dataReader[4].ToString();
                txtsifre.Text = dataReader[5].ToString(); 
                txtcinsiyet.Text = dataReader[6].ToString();

            }
            bgl.baglanti().Close();
        }

        private void btnduzenle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_hasta set hastaad=@p1,hastasoyad=@p2,hastatel=@p3,hastaşifre=@p4,hastacinsiyet=@p5 where hastatc=@p6",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtad.Text);
            komut2.Parameters.AddWithValue("@p2",txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p3",txttel.Text);
            komut2.Parameters.AddWithValue("@p4",txtsifre.Text);
            komut2.Parameters.AddWithValue("@p5",txtcinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6",txttc.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }
    }
}
