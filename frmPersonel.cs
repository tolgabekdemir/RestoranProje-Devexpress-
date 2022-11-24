using DevExpress.XtraEditors;
using RestoranProje.Formlar.Entity;
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



namespace RestoranProje.Formlar
{
    public partial class frmPersonel : Form
    {
        public frmPersonel()
        {
            InitializeComponent();
        }

        DBRestoranEntities db = new DBRestoranEntities();

        private void CmbRollerListele()
        {
            var roller = (from x in db.TblRoller
                          select new
                          {
                              x.ID,
                              x.Gorevi
                          }).ToList();
            cmbRoller.Properties.ValueMember = "ID";
            cmbRoller.Properties.DisplayMember = "Gorevi";
            cmbRoller.Properties.DataSource = roller;
        }

        void Listele()
        {
            var degerler = (from x in db.TblPersoneller
                          select new 
                          {
                              x.ID,
                              x.Ad,
                              x.Soyad,
                              x.TblRoller.Gorevi,
                              x.KullaniciAdi,
                              x.Sifre,
                              x.Durum
                          }).ToList();

            gridControl1.DataSource = degerler.Where(x=>x.Durum==true).ToList();

        }//Veri tabanı kayıtların listelenmesi
        private void BtnListele_Click(object sender, EventArgs e)
        {
            Listele();
        } //Listele butonuna basınca çalıştırılacak komut

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TblPersoneller t = new TblPersoneller();
            t.Ad = TxtAdi.Text;
            t.Soyad = TxtSoyadi.Text;
            t.GorevID = byte.Parse(cmbRoller.EditValue.ToString());
            t.KullaniciAdi= TxtKullaniciAdi.Text;
            t.Sifre=TxtSifre.Text;
            db.TblPersoneller.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Personel başarılı bir şekilde sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        } //Veri tabanına kayıt ekleme

        private void BtnSil_Click(object sender, EventArgs e)
        {
            var x = int.Parse(TxtID.Text);
            var deger = db.TblPersoneller.Find(x);
            deger.Durum = false;
            db.SaveChanges();
            XtraMessageBox.Show("Personel silme işlemi başarılı bir şekilde gerçekleşti","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            Listele();
        } //Veri tabanı kayıt silme

        private void frmPersonel_Load(object sender, EventArgs e)
        {
            CmbRollerListele();
            Listele();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TxtID.Text = gridView1.GetFocusedRowCellValue("ID").ToString();
            TxtAdi.Text=gridView1.GetFocusedRowCellValue("Ad").ToString();
            TxtSoyadi.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            cmbRoller.Text = gridView1.GetFocusedRowCellValue("Gorevi").ToString();
            TxtKullaniciAdi.Text = gridView1.GetFocusedRowCellValue("KullaniciAdi").ToString();
            TxtSifre.Text = gridView1.GetFocusedRowCellValue("Sifre").ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int x = int.Parse(TxtID.Text);
            var deger = db.TblPersoneller.Find(x);
            deger.Ad = TxtAdi.Text;
            deger.Soyad = TxtSoyadi.Text;
            deger.GorevID = byte.Parse(cmbRoller.EditValue.ToString());
            deger.KullaniciAdi = TxtKullaniciAdi.Text;
            deger.Sifre = TxtSifre.Text;
            db.SaveChanges();
            XtraMessageBox.Show("Personel bilgiler başarılı bir şekilde güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }
    }
}
