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
    public partial class frm_hastadetay : Form
    {
        public frm_hastadetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl=new sqlbaglanti();
        public string tc;
        private void frm_hastadetay_Load(object sender, EventArgs e)
        {
            //Ad soyad çekme
            lbltc.Text = tc;
            SqlCommand komut = new SqlCommand("Select hastaad,hastasoyad From tbl_hasta where hastatc=@p1 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lbltc.Text);
            SqlDataReader dataReader = komut.ExecuteReader();
            while (dataReader.Read())
            {
                lblad.Text = dataReader[0]+" "+ dataReader[1];
            }
            bgl.baglanti().Close();

            //randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_randevu where hastatc=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //branşları çekme
            SqlCommand komut2 = new SqlCommand("Select bransad from tbl_brans ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                txtbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
            
        }

        private void txtbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doktor çekme
            txtdoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktorbrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", txtbrans.Text);
            SqlDataReader d3 = komut3.ExecuteReader();
            while (d3.Read())
            {
                txtdoktor.Items.Add(d3[0] + " " + d3[1]);
            }
            bgl.baglanti().Close();
        }

        private void txtdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevu where RandevuBrans='"+txtbrans.Text+"'"+"and RandevuDoktor='"+txtdoktor.Text+"'and RandevuDurum IS null",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_duzenle frm = new frm_duzenle();
            frm.tcno=lbltc.Text;
            frm.Show();
            
        }

        private void btnrandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Update tbl_randevu set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 Where Randevuid=@p3", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",lbltc.Text);
            komut3.Parameters.AddWithValue("@p2", txtsikayet.Text);
            komut3.Parameters.AddWithValue("@p3", textBox1.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu!","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
