using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class ViewModelBibliotheque :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Bibliotheque bibliotheque;
       
        private static char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private static string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                        $"Fichiers-3GP{DIR_SEPARATOR}bibliotheque.xml";

        

        public ObservableCollection<Membre> listeMembres {
            get => bibliotheque.ListeMembres; }
        
      


        private Membre _utilisateurActif;
        public Membre UtilisateurActif
        {
            get { return _utilisateurActif; }
            set
            {
                if (_utilisateurActif != value)
                {
                    _utilisateurActif = value;
                    OnPropertyChanged(nameof(UtilisateurActif));
                }
            }
        }
        public ViewModelBibliotheque()
        {
            
        bibliotheque = new Bibliotheque();
        bibliotheque.ChargerEntrees(pathFichier);
            _utilisateurActif = bibliotheque.DernierUtilisateur;
        
        }
        private void OnPropertyChanged(string propertyName) 
        { 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ChangerDernierUtilisateur(Membre utilisateur) {
            UtilisateurActif=utilisateur;
        }
    }
}
