
using System.Collections.ObjectModel;
using Model;


namespace Model
{
    public class GestionnaireListes
    {
        public ObservableCollection<Livre> ListeLivres { get; private set; }
        public ObservableCollection<Membre> ListeMembres { get; private set; }

        public GestionnaireListes()
        {
            ListeLivres = new ObservableCollection<Livre>();
            ListeMembres = new ObservableCollection<Membre>();
        }
    }
}


