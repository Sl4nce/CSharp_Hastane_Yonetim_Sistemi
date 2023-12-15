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
    public partial class frm_doktorduzenle : Form
    {
        public frm_doktorduzenle()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        public string tcn;
        private void frm_doktorduzenle_Load(object sender, EventArgs e)
        {
            txttc.Text = tcn;
            SqlCommand komut = new SqlCommand("select * from tbl_doktor where doktortc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txttc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                txtbrans.Text = dr[3].ToString();
                txtsifre.Text = dr[5].ToString();
                
            }
            bgl.baglanti().Close();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_doktor set doktorad=@p1,doktorsoyad=@p2,doktorbrans=@p3,doktorsifre=@p4 where doktortc=@p5", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtad.Text);
            komut2.Parameters.AddWithValue("@p2",txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p3",txtbrans.Text);
            komut2.Parameters.AddWithValue("@p4",txtsifre.Text);
            komut2.Parameters.AddWithValue("@p5",txttc.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi!!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
