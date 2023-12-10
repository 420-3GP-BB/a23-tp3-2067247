using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model;

namespace Model 
{
    public class Livre : Article
    {

        
        public Livre(long isbn13, string titre, string auteur, string editeur, int annee) : base(isbn13, titre, auteur, editeur, annee)
        { 
        }
        public Livre (XmlElement element) : base(element) { }

        public override void DeXML(XmlElement elem)
        {
            base.DeXML(elem);
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
