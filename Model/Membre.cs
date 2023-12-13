using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Membre : IconversionXML
    {
        public string Nom { get; set; }
        public bool Administrateur { get; set; }
        public  HashSet<long> ISBNLivres {  get; set; }
        public HashSet<long> ISBNCommandesTraites { get; set; }
        public HashSet<long> ISBNCommandesAttente { get; set; }
        public ObservableCollection<Livre> ListeLivres { get; set; }
        public ObservableCollection<Commande> ListeCommandesTraites { get; set; }
        public ObservableCollection<Commande> ListeCommandesAttente { get; set; }


        public Membre(XmlElement element)
        {
            ISBNLivres= new HashSet<long>();
            ListeLivres = new ObservableCollection<Livre>();
            ISBNCommandesTraites = new HashSet<long>();
            ListeCommandesTraites =new ObservableCollection<Commande>();
            ISBNCommandesAttente = new HashSet<long>();
            ListeCommandesAttente =new ObservableCollection<Commande>();
            DeXML(element);
        }



        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement membreElement = doc.CreateElement("membre");
            membreElement.SetAttribute("nom", Nom);
            membreElement.SetAttribute("administrateur", Administrateur.ToString());

            foreach (long isbn in ISBNLivres)
            {
                XmlElement livreElement = doc.CreateElement("livre");
                livreElement.SetAttribute("ISBN-13", isbn.ToString());
                membreElement.AppendChild(livreElement);
            }

            foreach (long isbn in ISBNCommandesTraites)
            {
                XmlElement commandeElement = doc.CreateElement("commande");
                commandeElement.SetAttribute("statut", "Traitee");
                commandeElement.SetAttribute("ISBN-13", isbn.ToString());
                membreElement.AppendChild(commandeElement);
            }

            foreach (long isbn in ISBNCommandesAttente)
            {
                XmlElement commandeElement = doc.CreateElement("commande");
                commandeElement.SetAttribute("statut", "Attente");
                commandeElement.SetAttribute("ISBN-13", isbn.ToString());
                membreElement.AppendChild(commandeElement);
            }

            return membreElement;
        }

        public void DeXML(XmlElement elem)
        {
            
            Nom = elem.GetAttribute("nom");
            Administrateur = bool.Parse(elem.GetAttribute("administrateur"));



            XmlNodeList livresNodes = elem.SelectNodes("livre");
            foreach (XmlElement livreXml in livresNodes)
            {
                long isbn = long.Parse( livreXml.GetAttribute("ISBN-13"));
                ISBNLivres.Add(isbn);
            }
           

            XmlNodeList commandesNodes = elem.SelectNodes("commande");
            foreach (XmlElement commandeXml in commandesNodes)
            {
                long isbnCommande = long.Parse(commandeXml.GetAttribute("ISBN-13"));
                
                if (commandeXml.GetAttribute("statut")==("Traitee"))
                {
                    ISBNCommandesTraites.Add(isbnCommande);
                }
                else
                {
                    ISBNCommandesAttente.Add(isbnCommande);
                }
            }


        }

       
    }
}
