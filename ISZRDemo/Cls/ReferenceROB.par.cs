using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocont.ISZRDemo.RobCtiPodleUdaju2
{
    /// <summary>
    /// Rozsireni pro ROB zadost
    /// </summary>
    public partial class RobCtiPodleUdaju2DataType
    {
        private bool rodnePrijmeniFieldSpecified;

        private bool vydavatelCertifikatuFieldSpecified;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RodnePrijmeniSpecified
        {
            get
            {
                return this.rodnePrijmeniFieldSpecified;
            }
            set
            {
                this.rodnePrijmeniFieldSpecified = value;
                this.RaisePropertyChanged("RodnePrijmeniSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VydavatelCertifikatuSpecified
        {
            get
            {
                return this.vydavatelCertifikatuFieldSpecified;
            }
            set
            {
                this.vydavatelCertifikatuFieldSpecified = value;
                this.RaisePropertyChanged("VydavatelCertifikatuSpecified");
            }
        }
    }

    /// <summary>
    /// Rozsireni pro ROB zadost
    /// </summary>
    public partial class RobCtiPodleUdajuDataType
    {
        private bool adresaPobytuTypeFieldSpecified;

        private bool datovaSchrankaIdFieldSpecified;

        private bool mistoNarozeniFieldSpecified;

        private bool mistoUmrtiFieldSpecified;

        private bool jmenoSpecified;

        private bool prijmeniSpecified;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AdresaPobytuSpecified
        {
            get
            {
                return this.adresaPobytuTypeFieldSpecified;
            }
            set
            {
                this.adresaPobytuTypeFieldSpecified = value;
                this.RaisePropertyChanged("AdresaPobytuSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DatovaSchrankaIdSpecified
        {
            get
            {
                return this.datovaSchrankaIdFieldSpecified;
            }
            set
            {
                this.datovaSchrankaIdFieldSpecified = value;
                this.RaisePropertyChanged("DatovaSchrankaIdSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MistoNarozeniSpecified
        {
            get
            {
                return this.mistoNarozeniFieldSpecified;
            }
            set
            {
                this.mistoNarozeniFieldSpecified = value;
                this.RaisePropertyChanged("MistoNarozeniSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MistoUmrtiSpecified
        {
            get
            {
                return this.mistoUmrtiFieldSpecified;
            }
            set
            {
                this.mistoUmrtiFieldSpecified = value;
                this.RaisePropertyChanged("MistoUmrtiSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool JmenoSpecified
        {
            get
            {
                return this.jmenoSpecified;
            }
            set
            {
                this.jmenoSpecified = value;
                this.RaisePropertyChanged("JmenoSpecified");
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrijmeniSpecified
        {
            get
            {
                return this.prijmeniSpecified;
            }
            set
            {
                this.prijmeniSpecified = value;
                this.RaisePropertyChanged("PrijmeniSpecified");
            }
        }
    }
}
