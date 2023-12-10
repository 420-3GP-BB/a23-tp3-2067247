using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Model
{
    public class Bibliotheque : IconversionXML
    {
        public string DernierUtilisateur { get; set; }
        public ObservableCollection<Membre> ListeMembres { get; set; }
        public Dictionary<long, Livre> DictionnaireLivres { get; set; }
        

        public Bibliotheque()
        {
            ListeMembres = new ObservableCollection<Membre>();
            DictionnaireLivres = new Dictionary<long, Livre>();
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public void DeXML(XmlElement elem)
        {
            
            DernierUtilisateur = elem.GetAttribute("dernierUtilisateur");

            XmlNodeList membresNodes = elem.SelectNodes("membres/membre");
            foreach (XmlNode membreNode in membresNodes)
            {
                Membre membre = new Membre();
                membre.DeXML((XmlElement)membreNode);
                ListeMembres.Add(membre);
            }

            XmlNodeList livresNodes = elem.SelectNodes("livres/livre");
            foreach (XmlNode livreNode in livresNodes)
            {
                Livre livre = new Livre(livreNode);
                DictionnaireLivres.Add(livre.ISBN13, livre);
            }
        }
        public void ChargerEntrees(string nomFichier)
        {


            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;
            DernierUtilisateur = rootElement.GetAttribute("dernierUtilisateur");
            DeXML(rootElement);

        }




    }

}

    

