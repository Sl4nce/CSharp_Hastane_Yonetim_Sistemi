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
    public partial class frm_hastagiris : Form
    {
        public frm_hastagiris()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl=new sqlbaglanti();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_kayıt fr =new frm_kayıt();
            fr.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("Select * from tbl_hasta where hastatc=@p1 and hastaşifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr =komut.ExecuteReader();
            if (dr.Read())
            {
                frm_hastadetay fr = new frm_hastadetay();
                fr.tc = maskedTextBox1.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tc veya Şifre hatalıdır. \nLütfen tekrar deneyin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();

        }

        private void frm_hastagiris_Load(object sender, EventArgs e)
        {

        }
    }
}
