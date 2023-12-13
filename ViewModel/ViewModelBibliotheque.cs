using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace ViewModel
{
    public class ViewModelBibliotheque : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Bibliotheque bibliotheque;

        private static char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private static string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                        $"Fichiers-3GP{DIR_SEPARATOR}bibliotheque.xml";

        public ObservableCollection<Commande> ListeToutesCommandesAttente = new ObservableCollection<Commande>();
        public ObservableCollection<Commande> ListeToutesCommandesTraites = new ObservableCollection<Commande>();

        public ObservableCollection<Membre> listeMembres
        {
            get => bibliotheque.ListeMembres;
        }




        private Membre _utilisateurActif;
        public Membre UtilisateurActif
        {
            get { return _utilisateurActif; }
            set
            {
                if (_utilisateurActif != value)
                {
                    _utilisateurActif = value;
                    bibliotheque.DernierUtilisateur=value;
                    OnPropertyChanged(nameof(UtilisateurActif));
                }
            }
        }

        public Dictionary<long, Livre> DictionnaireLivres { get => bibliotheque.DictionnaireLivres; }
        public ViewModelBibliotheque()
        {
            bibliotheque = new Bibliotheque();
            bibliotheque.ChargerEntrees(pathFichier);
            _utilisateurActif = bibliotheque.DernierUtilisateur;
            CombinerCommandesAttente();
            CombinerCommandesTraites();
            Debug.WriteLine(ListeToutesCommandesAttente.Count);

        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ChangerDernierUtilisateur(Membre utilisateur)
        {
            UtilisateurActif = utilisateur;
        }

        public void AjouterCommandeAttente(Membre utilisateur, Commande commande)
        {
            commande.Proprietaire = utilisateur;
            utilisateur.ListeCommandesAttente.Add(commande);
            ListeToutesCommandesAttente.Add(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesAttente));
            
        }

        public void AnnulerCommandeAttente(Membre utilisateur, Commande commande)
        {
            utilisateur.ListeCommandesAttente.Remove(commande);
            ListeToutesCommandesAttente.Remove(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesAttente));
            OnPropertyChanged(nameof(ListeToutesCommandesAttente));
           
        }

        public void AjouterCommandeTraites(Membre utilisateur, Commande commande)
        {
            commande.Statut = "Traitee";
            utilisateur.ListeCommandesTraites.Add(commande);
            ListeToutesCommandesTraites.Add(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesTraites));
            OnPropertyChanged(nameof(ListeToutesCommandesTraites));
        }

        public void RetirerCommandeTraites(Membre utilisateur, Commande commande)
        {
            utilisateur.ListeCommandesTraites.Remove(commande);
            ListeToutesCommandesTraites.Remove(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesTraites));
            OnPropertyChanged(nameof(ListeToutesCommandesTraites));


        }

        public void AjouterLivre(Membre utilisateur, Livre livre)
        {
            utilisateur.ListeLivres.Add(livre);
            OnPropertyChanged(nameof(utilisateur.ListeLivres));
        }

        public void AjouterLivre(Membre utilisateur, Commande commande)
        {
            Livre livre = new Livre(commande.ISBN13, commande.Titre, commande.Auteur, commande.Editeur, commande.Annee);
            utilisateur.ListeLivres.Add(livre);
        }

        public void RetirerLivre(Membre utilisateur, Livre livre)
        {
            utilisateur.ListeLivres.Remove(livre);
            OnPropertyChanged(nameof(utilisateur.ListeLivres));
        }


        private void CombinerCommandesAttente()
        {
            foreach (Membre membre in listeMembres)
            {
                foreach (Commande commande in membre.ListeCommandesAttente)
                {
                    ListeToutesCommandesAttente.Add(commande);
                }
            }
        }

        private void CombinerCommandesTraites()
        {
            foreach (Membre membre in listeMembres)
            {
                foreach (Commande commande in membre.ListeCommandesTraites)
                {
                    ListeToutesCommandesTraites.Add(commande);
                }
            }

        }

       public void Sauvegarder()
        {
            bibliotheque.Sauvegarder(pathFichier);
        }


    

}
}
