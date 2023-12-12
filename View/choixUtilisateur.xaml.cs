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
        public Membre selection{ get; private set; }
        public choixUtilisateur(ViewModelBibliotheque viewModel)
        {
            InitializeComponent();
           _viewModel = viewModel;
            DataContext = viewModel;
            



        }

        public ObservableCollection<Membre> listeMembres
        {
            get => _viewModel.listeMembres;
        }
       
        public static RoutedCommand Comfirmercmd = new RoutedCommand();

        private void Comfirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ComboBoxUser.SelectedItem != null;
        }

        private void Comfirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            selection = (Membre)ComboBoxUser.SelectedItem;

            this.Close();
        }
       
    }
}
