using Model;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour CommandeLivre.xaml
    /// </summary>
    public partial class CommandeLivre : Window
    {
        ViewModelBibliotheque _viewModel;
        public long ISBN { get; private set; }
        public string TITRE { get; private set; }
        public string AUTEUR { get; private set; }
        public string EDITEUR { get; private set; }
        public int ANNEE { get; private set; }

        public CommandeLivre(ViewModelBibliotheque viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        public static RoutedCommand ComfirmerCmd = new RoutedCommand();
        //valide que les cases sont remplies avec les bonnes données
        private void Comfirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {//isbn doit etre un nombre de 13 caract`res
            if ((long.TryParse(CommandeISBN.Text, out long parsedISBN) && CommandeISBN.Text.Length == 13 &&
       !string.IsNullOrEmpty(CommandeTitre.Text) &&
       !string.IsNullOrEmpty(CommandeAuteur.Text) &&
       !string.IsNullOrEmpty(CommandeEditeur.Text)))
            {
                //verifier si l'année est un entier plus grand que -3000, cette structure a été réalisé avec l'aide de chat gpt, parceque fenetre crash < chque fois que j'essayais de modifier la saisie
                if (int.TryParse(CommandeAnnee.Text, out int parsedAnnee) && parsedAnnee > -3000)
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        //donnée accesibles dans le main window,pour la creation de la commande
        private void Comfirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           ISBN=long.Parse(CommandeISBN.Text);
           TITRE = CommandeTitre.Text;
           AUTEUR= CommandeAuteur.Text;
           EDITEUR= CommandeEditeur.Text;
           ANNEE=int.Parse(CommandeAnnee.Text);

            this.Close();
        }
    }
}
