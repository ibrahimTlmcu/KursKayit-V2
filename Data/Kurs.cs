using System.ComponentModel.DataAnnotations;

namespace KursKayir.Data
{
    public class Kurs
    {

        [Key]
        public int KursID { get; set; }

        public string? KursBaslik { get; set; }

        public int? OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }
}
