using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model;

namespace Model 
{
    public class Livre : IconversionXML
    {

        
        public Livre(int isbn13, string titre, string auteur, string editeur, int annee) 
        { ISBN13=isbn13;
            Titre = titre;
            Auteur = auteur;
            Editeur = editeur;  
            Annee = annee;

        }
        public int ISBN13
        {private set;
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
        public void DeXML(XmlElement elem)
        {


            XmlNodeList livresNodes = elem.SelectNodes("./livre");
           
            foreach (XmlNode livreNode in livresNodes)
            {
                ISBN13 = int.Parse(livreNode.Attributes["ISBN-13"].Value);
                Titre = livreNode.SelectSingleNode("titre").InnerText;
                Auteur = livreNode.SelectSingleNode("auteur").InnerText;
                Editeur = livreNode.SelectSingleNode("editeur").InnerText;
                Annee = int.Parse(livreNode.SelectSingleNode("annee").InnerText);

            }
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
