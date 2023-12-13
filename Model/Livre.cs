﻿using System;
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
        public Livre(XmlElement element) : base(element) { }

        public override void DeXML(XmlElement elem)
        {
            base.DeXML(elem);
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement livreElement = doc.CreateElement("livre");
            livreElement.SetAttribute("ISBN-13", ISBN13.ToString());

            XmlElement titreElement = doc.CreateElement("titre");
            titreElement.InnerText = Titre;
            livreElement.AppendChild(titreElement);

            XmlElement auteurElement = doc.CreateElement("auteur");
            auteurElement.InnerText = Auteur;
            livreElement.AppendChild(auteurElement);

            XmlElement editeurElement = doc.CreateElement("editeur");
            editeurElement.InnerText = Editeur;
            livreElement.AppendChild(editeurElement);

            XmlElement anneeElement = doc.CreateElement("annee");
            anneeElement.InnerText = Annee.ToString();
            livreElement.AppendChild(anneeElement);

            return livreElement;
        }
    }
}
