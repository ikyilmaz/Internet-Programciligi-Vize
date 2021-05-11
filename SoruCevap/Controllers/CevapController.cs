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
    public class CevapController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        // GET: api/Cevap
        public List<CevapModel> Get()
        {
            List<CevapModel> cevaplar = db.Cevaps.Select(x => new CevapModel()
            {
                Id = x.Id,
                Icerik = x.Icerik,
                SoruId = x.SoruId,
                UyeId = x.UyeId
            }).ToList();

            return cevaplar;
        }

        // GET: api/Cevap/5
        public CevapModel Get(int id)
        {
            CevapModel cevap = db.Cevaps.Where(s => s.Id == id).Select(x => new CevapModel()
            {
                Id = x.Id,
                Icerik = x.Icerik,
                SoruId = x.SoruId,
                UyeId = x.UyeId

            }).SingleOrDefault();

            return cevap;
        }

        // POST: api/Cevap
        public SonucModel Post([FromBody]CevapModel model)
        {
            db.Cevaps.Add(new Cevap()
            {
                Icerik = model.Icerik,
                SoruId = model.SoruId,
                UyeId = model.UyeId

            });

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Cevap Oluşturuldu";

            return sonuc;
        }

        // PUT: api/Cevap/5
        public SonucModel Put(int id, [FromBody]CevapModel model)
        {
            Cevap cevap = db.Cevaps.Where(s => s.Id == id).SingleOrDefault();

            if (cevap == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            cevap.Icerik = model.Icerik;

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Cevap Güncellendi";

            return sonuc;
        }

        // DELETE: api/Cevap/5
        public SonucModel Delete(int id)
        {
            Cevap cevap= db.Cevaps.Where(s => s.Id == id).SingleOrDefault();

            if (cevap == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Cevaps.Remove(cevap);
            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Cevap Silindi";
            return sonuc;
        }
    }
}
