using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesiErp.Models
{
    [Keyless]
    public class TeklifViewModel
    {

        public Teklif teklif { get; set; }
        public ICollection<Teklif> teklifs { get; set; }
        public /*List<CariKart>*/ IEnumerable<SelectListItem> Caris { get; set; }
        public /*List<CariKart>*/ IEnumerable<SelectListItem> Rpr_dinamik_raporparametreleri_getirs { get; set; }
        public /*List<CariKart>*/ IEnumerable<SelectListItem> OdemePlanis { get; set; }
        public /*IEnumerable<SelectListItem>*/string carikey { get; set; }
        public /*List<TeklifKalemi>*/List<TeklifKalemi> TeklifKalemis { get; set; }
        public /*List<TeklifKalemi>*/IEnumerable<SelectListItem> SatisEleman { get; set; }
        public /*List<TeklifKalemi>*/IEnumerable<SelectListItem> Dovizs { get; set; }
        public /*List<TeklifKalemi>*/IEnumerable<SelectListItem> Adress { get; set; }

        public /*List<TeklifKalemi>*/IEnumerable<SelectListItem> yetkili { get; set; }

        public CariKart CariKart { get; set; }
        public virtual ICollection<TeklifKalemi> TeklifKalemiss { get; set; }
        public virtual ICollection<SatisElemanlari> SatisElemanlariss { get; set; }
    }
}
