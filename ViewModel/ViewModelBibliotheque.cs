using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace ViewModel
{
    public class ViewModelBibliotheque : INotifyPropertyChanged
    {//permet de notifier les element ui du changement
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private Bibliotheque bibliotheque;
        //chemin nécessaire a la sauvegrade
        private static char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private static string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                        $"Fichiers-3GP{DIR_SEPARATOR}bibliotheque.xml";
        //liste contenant toutes les commandes ensembles pour la fenetre administartion
        public ObservableCollection<Commande> ListeToutesCommandesAttente = new ObservableCollection<Commande>();
        public ObservableCollection<Commande> ListeToutesCommandesTraites = new ObservableCollection<Commande>();

        //retourne bibliotheque.ListeMembres pour respecter le mvvm
        public ObservableCollection<Membre> listeMembres
        {
            get => bibliotheque.ListeMembres;
        }



        //Utilisateur affiché
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
        //stocke toute l'information sur les livres
        public Dictionary<long, Livre> DictionnaireLivres { get => bibliotheque.DictionnaireLivres; }
       // constructeur
        public ViewModelBibliotheque()
        {
            bibliotheque = new Bibliotheque();
            bibliotheque.ChargerEntrees(pathFichier);
            _utilisateurActif = bibliotheque.DernierUtilisateur;
            CombinerCommandesAttente();
            CombinerCommandesTraites();
            Debug.WriteLine(ListeToutesCommandesAttente.Count);

        }
        //permet de notifier de changement de la propriété
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //permet de changer l'utilisateur actif
        public void ChangerDernierUtilisateur(Membre utilisateur)
        {
            UtilisateurActif = utilisateur;
        }
        //permet d'ajouter une commande dans la liste de commnades en attente appropriée
        public void AjouterCommandeAttente(Membre utilisateur, Commande commande)
        {
            commande.Proprietaire = utilisateur;
            utilisateur.ListeCommandesAttente.Add(commande);
            utilisateur.ISBNCommandesAttente.Add(commande.ISBN13);
            ListeToutesCommandesAttente.Add(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesAttente));
            
        }
        //permet d'enlever un commande en attente soit en l'annulant ou pour la traiter
        public void AnnulerCommandeAttente(Membre utilisateur, Commande commande)
        {
            utilisateur.ListeCommandesAttente.Remove(commande);
            ListeToutesCommandesAttente.Remove(commande);
            utilisateur.ISBNCommandesAttente.Remove(commande.ISBN13);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesAttente));
            OnPropertyChanged(nameof(ListeToutesCommandesAttente));
           
        }
        //ajoute la commande traitée a la bonne lsite
        public void AjouterCommandeTraites(Membre utilisateur, Commande commande)
        {
            commande.Statut = "Traitee";
            utilisateur.ListeCommandesTraites.Add(commande);
            utilisateur.ISBNCommandesTraites.Add(commande.ISBN13);
            ListeToutesCommandesTraites.Add(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesTraites));
            OnPropertyChanged(nameof(ListeToutesCommandesTraites));
        }
        //permet d'enlever la commande traitée de la liste
        public void RetirerCommandeTraites(Membre utilisateur, Commande commande)
        {
            utilisateur.ListeCommandesTraites.Remove(commande);
            utilisateur.ISBNCommandesTraites.Remove(commande.ISBN13);
            ListeToutesCommandesTraites.Remove(commande);
            OnPropertyChanged(nameof(utilisateur.ListeCommandesTraites));
            OnPropertyChanged(nameof(ListeToutesCommandesTraites));


        }
        //permet d'ajouter un livre soit en le transfererant d'un autre utilisateur ou a partir d'une commande
        public void AjouterLivre(Membre utilisateur, Livre livre)
        {
            utilisateur.ListeLivres.Add(livre);
            utilisateur.ISBNLivres.Add(livre.ISBN13);
            OnPropertyChanged(nameof(utilisateur.ListeLivres));
        }

        public void AjouterLivre(Membre utilisateur, Commande commande)
        {
            Livre livre = new Livre(commande.ISBN13, commande.Titre, commande.Auteur, commande.Editeur, commande.Annee);
            utilisateur.ISBNLivres.Add(livre.ISBN13);
            utilisateur.ListeLivres.Add(livre);
        }
        //permet d'enlever un livre d'un utiliateur
        public void RetirerLivre(Membre utilisateur, Livre livre)
        {
            utilisateur.ListeLivres.Remove(livre);
            utilisateur.ISBNLivres.Remove(livre.ISBN13);
            OnPropertyChanged(nameof(utilisateur.ListeLivres));
        }

        //pour combiner toutes les commandes des différents users ensembre
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
        //pour sauvegarder le fichier
       public void Sauvegarder()
        {
            bibliotheque.Sauvegarder(pathFichier);
        }


    

}
}
