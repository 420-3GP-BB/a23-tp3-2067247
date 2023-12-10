using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ViewModelBibliotheque
    {
       
        private Bibliotheque bibliotheque;
       
        private static char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private static string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                        $"Fichiers-3GP{DIR_SEPARATOR}bibliotheque.xml";


        public ObservableCollection<Membre> listeMembres {
            get => bibliotheque.ListeMembres; }
        public ViewModelBibliotheque()
        {
            
        bibliotheque = new Bibliotheque();
        bibliotheque.ChargerEntrees(pathFichier);
        }
    }
}
