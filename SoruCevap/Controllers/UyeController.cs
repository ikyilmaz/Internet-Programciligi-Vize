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
    public class UyeController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        // GET: api/Uye
        public List<UyeModel> Get()
        {
            List<UyeModel> uyeler = db.Uyes.Select(x => new UyeModel()
            {
                Id = x.Id,
                Ad= x.Ad,
                Soyad= x.Soyad,
                Email= x.Email,
                Rol = x.Rol,
                Sifre = x.Sifre,
            }).ToList();

            return uyeler;
        }

        // GET: api/Uye/5
        public UyeModel Get(int id)
        {
            UyeModel uye= db.Uyes.Where(s => s.Id == id).Select(x => new UyeModel()
            {
                Id = x.Id,
                Ad = x.Ad,
                Soyad = x.Soyad,
                Email = x.Email,
                Rol = x.Rol,
                Sifre = x.Sifre,

            }).SingleOrDefault();

            return uye;
        }

        // POST: api/Uye
        public SonucModel Post([FromBody]UyeModel model)
        {
            db.Uyes.Add(new Uye()
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                Rol = model.Rol,
                Sifre = model.Sifre,

            });

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Uye Oluşturuldu";

            return sonuc;
        }

        // PUT: api/Uye/5
        public SonucModel Put(int id, [FromBody]UyeModel model)
        {

            Uye uye = db.Uyes.Where(s => s.Id == id).SingleOrDefault();

            if (uye == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            uye.Ad = model.Ad;
            uye.Soyad = model.Soyad;
            uye.Email = model.Email;
            uye.Rol = model.Rol;
            uye.Sifre = model.Sifre;

            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Üye Güncellendi";
            return sonuc;
        }

        // DELETE: api/Uye/5
        public SonucModel Delete(int id)
        {
            Uye uye = db.Uyes.Where(s => s.Id == id).SingleOrDefault();

            if (uye == null)
            {
                sonuc.Islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Uyes.Remove(uye);
            db.SaveChanges();

            sonuc.Islem = true;
            sonuc.Mesaj = "Üye Silindi";
            return sonuc;
        }
    }
}
