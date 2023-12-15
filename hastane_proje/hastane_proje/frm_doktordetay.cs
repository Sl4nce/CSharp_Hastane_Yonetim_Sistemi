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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace hastane_proje
{
    public partial class frm_doktordetay : Form
    {
        public frm_doktordetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        public string tc;
        private void frm_doktordetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            //Ad soyad çekme
            SqlCommand komut = new SqlCommand("Select doktorad,doktorsoyad From tbl_doktor where doktortc=@p1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dataReader = komut.ExecuteReader();
            while (dataReader.Read())
            {
                lblad.Text = dataReader[0] + " " + dataReader[1];
            }
            bgl.baglanti().Close();
            //randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevu where RandevuDoktor='"+lblad.Text+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnduzenle_Click(object sender, EventArgs e)
        {
            frm_doktorduzenle frm_Doktorduzenle = new frm_doktorduzenle();
            frm_Doktorduzenle.tcn = lbltc.Text;
            frm_Doktorduzenle.Show();

        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            frm_duyurular frm_Duyurular = new frm_duyurular();
            frm_Duyurular.Show();
        }

        private void btncıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            randevusikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
