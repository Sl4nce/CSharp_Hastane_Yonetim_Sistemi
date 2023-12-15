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
    public partial class frm_sekreterdetay : Form
    {
        public frm_sekreterdetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglanti bgl = new sqlbaglanti();
        private void frm_sekreterdetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            //Sekreter giriş
            SqlCommand komut=new SqlCommand("Select sekreteradsoyad from tbl_sekreter where sekretertc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
            
            //Branşları aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_brans", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            //Doktorları Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select doktorad,doktorsoyad,doktorbrans from tbl_doktor", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            //Bransı comboboxa cekme
            SqlCommand komut3 = new SqlCommand("select bransad from tbl_brans",bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbbrans.Items.Add(dr3[0].ToString());
            }

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            //Randevu Oluşturma
            SqlCommand komut2 = new SqlCommand("insert into tbl_randevu (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txttarih.Text);
            komut2.Parameters.AddWithValue("@p2", txtsaat.Text);
            komut2.Parameters.AddWithValue("@p3", cmbbrans.Text);
            komut2.Parameters.AddWithValue("@p4", cmbdoktor.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);




        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();
            SqlCommand komut4 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktor where doktorbrans=@p1",bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1",cmbbrans.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                cmbdoktor.Items.Add(dr4[0]+" " + dr4[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("insert into tbl_duyuru (duyuru) values (@p1)", bgl.baglanti());
            komut5.Parameters.AddWithValue("@p1",txtduyuru.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu!","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            frm_doktorpanel frm_Doktorpanel = new frm_doktorpanel();
            frm_Doktorpanel.Show();
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            frm_bransekle frm_Bransekle = new frm_bransekle();
            frm_Bransekle.Show();
        }

        private void btnrandevuliste_Click(object sender, EventArgs e)
        {
            frm_randevuliste frm_Randevuliste = new frm_randevuliste();
            frm_Randevuliste.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_duyurular frm_Duyurular = new frm_duyurular();
            frm_Duyurular.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
