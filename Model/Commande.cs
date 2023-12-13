using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Commande : Article
    {
        public string Statut { get; set; }
        public Membre Proprietaire { get; set; }


        public Commande(long isbn13, string titre, string auteur, string editeur, int annee, string statut)
            : base(isbn13, titre, auteur, editeur, annee)
        {
            Statut = statut;
        }
        public Commande(XmlElement element)
            : base(element)
        {
            DeXML(element); 
        }
        public string InfoCmd
        {
            get { return $"{Titre}, {Auteur}({Annee}) ==> {Proprietaire.Nom}"; }
        }


        public override void DeXML(XmlElement elem)
            {
                 base.DeXML(elem);

           
                 Statut = elem.GetAttribute("statut");
            
             }
        

        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement commandeElement = doc.CreateElement("commande");
            commandeElement.SetAttribute("statut", Statut);
            commandeElement.SetAttribute("ISBN-13", ISBN13.ToString());

            return commandeElement;
        }
    }
}
