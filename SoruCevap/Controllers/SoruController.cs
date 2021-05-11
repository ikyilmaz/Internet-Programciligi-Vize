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
    public class SoruController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        [HttpGet]
        [Route("api/sorular")]
        public List<SoruModel> SoruListe()
        {
            List<SoruModel> sorular = db.Sorus.Select(x => new SoruModel()
            {
                Id = x.Id,
                Baslik = x.Baslik,
                Icerik = x.Icerik,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                KatId = x.KatId

            }).ToList();

            return sorular;
        } 

        [HttpGet]
        [Route("api/sorular/{id}")]
        public SoruModel SoruById(int id)
        {
            SoruModel soru = db.Sorus.Where(s => s.Id == id).Select(x => new SoruModel()
            {
                Id = x.Id,
                Baslik = x.Baslik,
                Icerik = x.Icerik,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                KatId = x.KatId

            }).SingleOrDefault();

            return soru;
        }

        [HttpPost]
        [Route("api/sorular")]
        public SonucModel SoruOlustur(SoruModel model)
        {
            db.Sorus.Add(new Soru()
            {
                Baslik = model.Baslik,
                Icerik = model.Icerik,
                KatId = model.KatId,
                Tarih = model.Tarih,
                UyeId = model.UyeId

            });

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Soru Oluşturuldu";

            return sonuc;
        }

        [HttpPut]
        [Route("api/sorular/{id}")]
        public SonucModel SoruGuncelle(int id, [FromBody]SoruModel model)
        {

            Soru soru = db.Sorus.Where(s => s.Id == id).SingleOrDefault();

            if (soru == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            soru.Baslik = model.Baslik;
            soru.Icerik = model.Icerik;
            soru.KatId = model.KatId;

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Soru Güncellendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorular/{id}")]
        public SonucModel SoruSil(int id)
        {
            Soru soru = db.Sorus.Where(s => s.Id == id).SingleOrDefault();

            if (soru == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Sorus.Remove(soru);

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Soru Silindi";

            return sonuc;
        }

    }
}
