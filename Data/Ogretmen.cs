﻿using System.ComponentModel.DataAnnotations;

namespace KursKayir.Data
{
    public class Ogretmen
    {

        [Key]
        public int OgretmenId { get; set; }

        public string? Ad { get; set; }

        public string? Soyad { get; set; }

        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime BaslamaTarihi { get; set; }

        public ICollection<Kurs> Kurslar = new List<Kurs>();
        //Bir ogretmen birden fazla kurs verebilir
        //verdigi kurslar bu yapi icinde tutulur
    }
}
