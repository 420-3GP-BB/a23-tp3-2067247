using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Logique d'interaction pour FenetreAdmin.xaml
    /// </summary>
    public partial class FenetreAdmin : Window
    {
      
        ViewModelBibliotheque _viewModel;
        public FenetreAdmin(ViewModelBibliotheque viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            CommandesAttente.ItemsSource = _viewModel.ListeToutesCommandesAttente;
            CommandesTraites.ItemsSource = _viewModel.ListeToutesCommandesTraites;
           

        }
        //ferme la fenetre
        private void Revenir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //control du clickdroit sur un element la liste,dépendemment de la liste, la commamde est soit traitée ou délivré au bon utilisateur sous forme de livre
        private void CommandesTraites_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Commande commandeSelectionne = CommandesTraites.SelectedItem as Commande;
            if (commandeSelectionne != null)
            {
                _viewModel.RetirerCommandeTraites(commandeSelectionne.Proprietaire, commandeSelectionne);
                _viewModel.AjouterLivre(commandeSelectionne.Proprietaire, commandeSelectionne);
            }
        }
        private void CommandesAttente_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Commande commandeSelectionne = CommandesAttente.SelectedItem as Commande;
            if (commandeSelectionne != null)
            {
                _viewModel.AnnulerCommandeAttente(commandeSelectionne.Proprietaire, commandeSelectionne);
                _viewModel.AjouterCommandeTraites(commandeSelectionne.Proprietaire, commandeSelectionne);
            }
        }
    }
}
