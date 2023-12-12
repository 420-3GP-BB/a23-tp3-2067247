using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Model
{
    public class Bibliotheque : IconversionXML
    {
        public Membre DernierUtilisateur { get; private set; }
        public ObservableCollection<Membre> ListeMembres { get; private set; }
        public Dictionary<long, Livre> DictionnaireLivres { get; private set; }
        private string DernierUtilisateurNom;

        public Bibliotheque()
        {
            ListeMembres = new ObservableCollection<Membre>();
            DictionnaireLivres = new Dictionary<long, Livre>();
           
        }
        public void ChangerDernierUtilisateur(Membre utilisateur)
        {
            DernierUtilisateur = utilisateur;
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public void DeXML(XmlElement elem)
        {


            XmlNodeList livresNodes = elem.SelectNodes("livres/livre");
            foreach (XmlElement livreNode in livresNodes)
            {
                Livre livre = new Livre(livreNode);
                DictionnaireLivres.Add(livre.ISBN13, livre);
            }


            DernierUtilisateurNom = elem.GetAttribute("dernierUtilisateur").Trim();

            XmlNodeList membresNodes = elem.SelectNodes("membres/membre");
            foreach (XmlElement membreNode in membresNodes)
            {
                Membre membre = new Membre(membreNode);
                string nomMembre= membre.Nom.Trim();
                membre.DeXML(membreNode);
                ListeMembres.Add(membre);
              if(nomMembre.Equals(DernierUtilisateurNom))
                { DernierUtilisateur = membre; }
              foreach(long isbn in membre.ISBNLivres)
                {
                   if (DictionnaireLivres.ContainsKey(isbn))
                   {
                        Livre livre = DictionnaireLivres[isbn];
                        membre.ListeLivres.Add(livre);
                   }

                    
                }
                foreach (long isbn in membre.ISBNCommandesAttente)
                {
                    if (DictionnaireLivres.ContainsKey(isbn))
                    {
                        Livre livreApproprie = DictionnaireLivres[isbn];
                        Commande commande = new Commande(livreApproprie.ISBN13, livreApproprie.Titre, livreApproprie.Auteur, livreApproprie.Editeur, livreApproprie.Annee, "Attente");
                        membre.ListeCommandesAttente.Add(commande);
                     
                    }
                }
                foreach (long isbn in membre.ISBNCommandesTraites)
                {
                    if (DictionnaireLivres.ContainsKey(isbn))
                    {
                        Livre livreApproprie = DictionnaireLivres[isbn];
                        Commande commande = new Commande(livreApproprie.ISBN13, livreApproprie.Titre, livreApproprie.Auteur, livreApproprie.Editeur, livreApproprie.Annee, "Attente");
                        membre.ListeCommandesTraites.Add(commande);

                    }
                }




            }
        }
        public void ChargerEntrees(string nomFichier)
        {


            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlElement racine = doc.DocumentElement;
            DeXML(racine);

        }




    }

}

    

