using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitirmeProjesiErp.Models
{
    public class WSModel
    {

        // bir tane web servis kullanıcısı depolanacak ama ileride eklenmesi gerekebilir diye bu şekilde yapıyorum.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int WSID { get; set; } = 1;
        public string ApiKey { get; set; } = "736c1d33-e92f-4131-86f1-775dd7885d47";
        public string UserName { get; set; }
        public string Sifre { get; set; }
        public string SunucuAdi { get; set; }
        public string Sube { get; set; }
        public string Depo { get; set; }
        public string FirmaKod { get; set; }
        public string DonemKod { get; set; }


    }
}
