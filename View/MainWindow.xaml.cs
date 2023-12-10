using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;


namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelBibliotheque _viewModel;
        public MainWindow()

        {
            InitializeComponent();
            _viewModel = new ViewModelBibliotheque();
           // InitializeComponent();
            DataContext = _viewModel;

        }

        public static RoutedCommand ChangerUserCmd = new RoutedCommand();

        private void ChangerUser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ChangerUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            choixUtilisateur choixUtilisateur = new choixUtilisateur();
            choixUtilisateur.Owner = this;
            choixUtilisateur.ShowDialog();
        }

        public static RoutedCommand CommanderLivreCmd = new RoutedCommand();

        private void CommanderLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandeLivre commandeLivre = new CommandeLivre();
            commandeLivre.Owner = this;
            commandeLivre.ShowDialog();
        }

        public static RoutedCommand OuvrirAdminCmd = new RoutedCommand();

        private void OuvrirAdmin_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OuvrirAdmin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreAdmin fenetreAdmin = new FenetreAdmin();
            fenetreAdmin.Owner = this;
            fenetreAdmin.ShowDialog();
        }

        public static RoutedCommand QuitterCmd = new RoutedCommand();

        private void Quitter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           this.Close();
        }

        public static RoutedCommand TransfererLivreCmd = new RoutedCommand();

        private void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            choixUtilisateur choixUtilisateur = new choixUtilisateur();
            choixUtilisateur.Owner = this;
            choixUtilisateur.ShowDialog();
        }
    }
}