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
    public partial class frm_bransekle : Form
    {
        public frm_bransekle()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl=new sqlbaglanti();
        private void frm_bransekle_Load(object sender, EventArgs e)
        {
            
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from tbl_brans", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            bgl.baglanti().Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_brans (bransad) values (@p1) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", btnsoyad.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_brans set bransad=@p2 where bransid=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", btnad.Text);
            komut2.Parameters.AddWithValue("@p2", btnsoyad.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("delete from tbl_brans where bransid=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",btnad.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
