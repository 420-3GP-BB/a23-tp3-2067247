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
   
    public partial class choixUtilisateur : Window
    {
        ViewModelBibliotheque _viewModel;
        
        public choixUtilisateur(ViewModelBibliotheque viewModel)
        {
            InitializeComponent();
           _viewModel = viewModel;
            DataContext = viewModel;
            Membre membreSelectionne = MembreActif;
            ComboBoxUser.SelectedItem=membreSelectionne;
           
            
        }

        public ObservableCollection<Membre> listeMembres
        {
            get => _viewModel.listeMembres;
        }
        public Membre MembreActif
        {
            get => _viewModel.MembreActif;
        }
        
        private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
