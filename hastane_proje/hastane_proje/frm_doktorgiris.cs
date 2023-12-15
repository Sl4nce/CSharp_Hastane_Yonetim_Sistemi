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
    public partial class frm_doktorgiris : Form
    {
        public frm_doktorgiris()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl=new sqlbaglanti();
        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("Select * from tbl_doktor where doktortc=@p1 and doktorsifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            //Ad soyad çekme
            SqlDataReader dataReader =komut.ExecuteReader();
            if (dataReader.Read())
            {
                frm_doktordetay frm_Doktordetay = new frm_doktordetay();
                frm_Doktordetay.tc = maskedTextBox1.Text;
                frm_Doktordetay.Show();
                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tc veya Şifre hatalıdır. \nLütfen tekrar deneyin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
            

            
        }
    }
}
