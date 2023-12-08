using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public interface IconversionXML
    {
        /// <summary>
        /// interface permettant de convertir des element xml en objet et vice verça
        /// </summary>

        // convertit un objet en element xml
        public XmlElement VersXML(XmlDocument doc);
        // convertit un élément xml en objet
        public void DeXML(XmlElement elem);
    }
}
