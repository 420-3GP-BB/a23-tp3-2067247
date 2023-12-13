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
        //utile pour le bindinf de la fenetre admin
        public string InfoCmd
        {
            get { return $"{Titre}, {Auteur}({Annee}) ==> {Proprietaire.Nom}"; }
        }

        /// <summary>
        /// methode permettant de convertir un élément xml en objet
        /// </summary>
        /// <param name="elem">element a convertir</param>
        public override void DeXML(XmlElement elem)
            {
                 base.DeXML(elem);

           
                 Statut = elem.GetAttribute("statut");
            
             }

        /// <summary>
        /// methode permettant de convertir un objet en un element xml
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>XmlELEMENT</returns>
        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement commandeElement = doc.CreateElement("commande");
            commandeElement.SetAttribute("statut", Statut);
            commandeElement.SetAttribute("ISBN-13", ISBN13.ToString());

            return commandeElement;
        }
    }
}
