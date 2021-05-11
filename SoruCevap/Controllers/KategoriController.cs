using SoruCevap.Models;
using SoruCevap.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoruCevap.Controllers
{
    public class KategoriController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        // GET: api/Kategori
        public List<KategoriModel> Get()
        {
            List<KategoriModel> kategoriler = db.Kategoris.Select(x => new KategoriModel()
            {
                Id = x.Id,
                KatAdi = x.KatAdi
            }).ToList();

            return kategoriler;
        }

        // GET: api/Kategori/5
        public KategoriModel Get(int id)
        {
            KategoriModel kategori = db.Kategoris.Where(s => s.Id == id).Select(x => new KategoriModel()
            {
                Id = x.Id,
                KatAdi = x.KatAdi
            }).SingleOrDefault();

            return kategori;
        }

        // POST: api/Kategori
        public SonucModel Post([FromBody]KategoriModel model)
        {
            db.Kategoris.Add(new Kategori()
            {
                KatAdi = model.KatAdi
            });

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Kategori Oluşturuldu";
            return sonuc;
        }

        // PUT: api/Kategori/5
        public SonucModel Put(int id, [FromBody]KategoriModel model)
        {
            Kategori kategori= db.Kategoris.Where(s => s.Id == id).SingleOrDefault();

            if (kategori == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kategori.KatAdi = model.KatAdi;

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Kategori Güncellendi";

            return sonuc;
        }

        // DELETE: api/Kategori/5
        public SonucModel Delete(int id)
        {
            Kategori kategori = db.Kategoris.Where(s => s.Id == id).SingleOrDefault();

            if (kategori == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Kategoris.Remove(kategori);
            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Kategori Silindi";
            return sonuc;
        }
    }
}
