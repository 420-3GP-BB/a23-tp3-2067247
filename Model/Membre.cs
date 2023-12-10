using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection <long>  ListeISBNLivres { get; set; }
        public ObservableCollection<long> ListeISBNCommandesTraites { get; set; }
        public ObservableCollection<long> ListeISBNCommandesAttente { get; set; }

      

        public Membre(XmlElement element)
        {
            DeXML(element);
            
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public void DeXML(XmlElement elem)
        {
            ListeISBNLivres = new ObservableCollection<long>();
            ListeISBNCommandesTraites = new ObservableCollection<long>();
            ListeISBNCommandesAttente = new ObservableCollection<long>();
            Nom = elem.GetAttribute("nom");
            Administrateur = bool.Parse(elem.GetAttribute("administrateur"));



            XmlNodeList livresNodes = elem.SelectNodes("livre");
            foreach (XmlElement livreXml in livresNodes)
            {
                long isbn = long.Parse( livreXml.GetAttribute("ISBN-13"));
                ListeISBNLivres.Add(isbn);
            }
            XmlNodeList commandesNodes = elem.SelectNodes("commande");
            foreach (XmlElement commandeXml in commandesNodes)
            {
                long isbnCommande = long.Parse(commandeXml.GetAttribute("ISBN-13"));
                
                if (commandeXml.GetAttribute("statut")==("Traitee"))
                {
                    ListeISBNCommandesTraites.Add(isbnCommande);
                }
                else
                {
                    ListeISBNCommandesAttente.Add(isbnCommande);
                }
            }


        }


    }
}
