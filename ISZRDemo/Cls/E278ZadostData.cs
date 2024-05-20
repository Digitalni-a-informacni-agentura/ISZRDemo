using Autocont.ISZRDemo.RobCtiPodleUdaju2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocont.ISZRDemo.Cls
{
    internal class E278ZadostData
    {
        public string Jmeno { get; set; }
        public bool PouzitJmeno { get; set; }
        public string Prijmeni { get; set; }
        public bool PouzitPrijmeni { get; set; }
        public int? Adresa { get; set; }
        public bool PouzitAdresa { get; set; }
        public DateTime? DatumNarozeni { get; set; }
        public DateTime? DatumUmrti { get; set; }
        public string DatovaSchrankaId { get; set; }
        public bool PouzitDatovaSchrankaId { get; set; }
        public int? MistoNarozeni { get; set; }
        public bool PouzitMistoNarozeni { get; set; }
        public int? MistoUmrti { get; set; }
        public bool PouzitMistoUmrti { get; set; }
        public string RodnePrijmeni { get; set; }
        public bool PouzitRodnePrijmeni { get; set; }
        public Nullable<OmezeniSvepravnostiType> OmezeniSvepravnosti { get; set; }
        public bool PouzitOmezeniSvepravnosti { get; set; }
        public Nullable<RodinnyStavPartnerstviType> RodinnyStav { get; set; }
        public bool PouzitRodinnyStav { get; set; }
        public Nullable<PohlaviType> Pohlavi { get; set; }
        public bool PouzitPohlavi { get; set; }
    }
}
