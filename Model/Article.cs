using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Article : IconversionXML
    {
        public Article(long isbn13, string titre, string auteur, string editeur, int annee)
        {
            ISBN13 = isbn13;
            Titre = titre;
            Auteur = auteur;
            Editeur = editeur;
            Annee = annee;

        }
        public Article(XmlElement elem){
            DeXML(elem);
        }
        public long ISBN13
        {
            private set;
            get;

        }

        public string Titre
        {
            private set;
            get;

        }

        public string Auteur
        {
            private set;
            get;

        }

        public string Editeur
        {
            private set;
            get;

        }
        public int Annee
        {
            private set;
            get;

        }
        public virtual void DeXML(XmlElement elem)
        {
            ISBN13 = long.Parse(elem.GetAttribute("ISBN-13"));
            Titre = elem.SelectSingleNode("titre").InnerText;
            Auteur=elem.SelectSingleNode("auteur").InnerText;
            Editeur = elem.SelectSingleNode("editeur").InnerText;
            Annee = int.Parse(elem.SelectSingleNode("annee").InnerText);

        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
