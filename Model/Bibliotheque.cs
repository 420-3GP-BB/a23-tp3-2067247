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
{/// <summary>
/// C'est la classe qui gére les liste, les transfert entre les liste, la comparaison avec le disctionnaire, la construction des données
/// </summary>
    public class Bibliotheque : IconversionXML
    {//utilisateur affiché
        public Membre DernierUtilisateur { get; set; }
        //liste des utilisateurs membres de la bibliotheque
        public ObservableCollection<Membre> ListeMembres { get; private set; }
        //dictionnaire regroupant tous les livres, avec le isbn comme clé
        public Dictionary<long, Livre> DictionnaireLivres { get; private set; }
        //string privé qui retourne le nom du DernierUtilisateurNom
        private string DernierUtilisateurNom;
        /// <summary>
        /// constructeur de la biblioteque
        /// </summary>
        public Bibliotheque()
        {
            ListeMembres = new ObservableCollection<Membre>();
            DictionnaireLivres = new Dictionary<long, Livre>();

        }
       
        /// <summary>
        /// Permet de transformer un objet en elemenxml
        /// </summary>
        /// <param name="doc">le path du fichier</param>
        /// <returns>l'element xml</returns>
        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement bibliothequeElement = doc.CreateElement("bibliotheque");
            bibliothequeElement.SetAttribute("dernierUtilisateur", DernierUtilisateurNom);

            XmlElement membresElement = doc.CreateElement("membres");
            foreach (Membre membre in ListeMembres)
            {
                membresElement.AppendChild(membre.VersXML(doc));
            }
            bibliothequeElement.AppendChild(membresElement);

            XmlElement livresElement = doc.CreateElement("livres");
            foreach (Livre livre in DictionnaireLivres.Values)
            {
                livresElement.AppendChild(livre.VersXML(doc));
            }
            bibliothequeElement.AppendChild(livresElement);

            return bibliothequeElement;
        }
        /// <summary>
        /// permet de trabsformer un element xml en objet, J'ai eu recours  chat gpt pour maider à genérer cette partie vue la complexité de la structure
        /// </summary>
        /// <param name="elem">L'element xml a transformer</param>
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
                string nomMembre = membre.Nom.Trim();
                membre.DeXML(membreNode);
                ListeMembres.Add(membre);
                if (nomMembre.Equals(DernierUtilisateurNom))
                { DernierUtilisateur = membre; }
                foreach (long isbn in membre.ISBNLivres)
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
                        commande.Proprietaire = membre;
                        membre.ListeCommandesAttente.Add(commande);

                    }
                }
                foreach (long isbn in membre.ISBNCommandesTraites)
                {
                    if (DictionnaireLivres.ContainsKey(isbn))
                    {
                        Livre livreApproprie = DictionnaireLivres[isbn];
                        Commande commande = new Commande(livreApproprie.ISBN13, livreApproprie.Titre, livreApproprie.Auteur, livreApproprie.Editeur, livreApproprie.Annee, "Attente");
                        commande.Proprietaire = membre;
                        membre.ListeCommandesTraites.Add(commande);

                    }
                }




            }
        }
        //cette methode est appeler pour charger les données à l'ouverture de l'application
        public void ChargerEntrees(string nomFichier)
        {


            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlElement racine = doc.DocumentElement;
            DeXML(racine);

        }
        //permet de sauvegarder le document en commençant par la liste des membre en appelant membre.VersXML(doc), pour enregistrer leurs attributs respectifs et ensuite les livres du ditionnaire

        public void Sauvegarder(string pathFichier)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement racine = doc.CreateElement("bibliotheque");
            racine.SetAttribute("dernierUtilisateur", DernierUtilisateur.Nom);
            doc.AppendChild(racine);
            XmlElement membresElement = doc.CreateElement("membres");
            foreach (Membre membre in ListeMembres)
            {
                membresElement.AppendChild(membre.VersXML(doc));
            }
            racine.AppendChild(membresElement);
            
            XmlElement livresElement = doc.CreateElement("livres");
            //cette partie en bas est générée grace à chat gpt
            foreach (KeyValuePair<long, Livre> entry in DictionnaireLivres)
            {
                livresElement.AppendChild(entry.Value.VersXML(doc));
            }
            racine.AppendChild(livresElement);

            doc.Save(pathFichier);
        }

    }

}



