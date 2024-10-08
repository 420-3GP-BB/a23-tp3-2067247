﻿using System.Text;
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
using Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;

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
            DataContext = _viewModel;
            Closing += MainWindow_Closing;



        }


        //Sauvegarder < la fermeture de la fenetre
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            _viewModel.Sauvegarder(); 
        }
        //routed command pour changer l'utilisateur courant
        //permet d'ouvrir une nouvelle fenetre pour choisir l'utilisateur
        public static RoutedCommand ChangerUserCmd = new RoutedCommand();

        private void ChangerUser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ChangerUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            choixUtilisateur _choixUtilisateur = new choixUtilisateur(_viewModel);
            _choixUtilisateur.Owner = this;
            _choixUtilisateur.ShowDialog();
            if (_choixUtilisateur.selection != null)
            {
                _viewModel.ChangerDernierUtilisateur(_choixUtilisateur.selection);
            }


        }


        //permet d'ouvrir une nouvelle fenetre pour rentrer les information de la commande
        public static RoutedCommand CommanderLivreCmd = new RoutedCommand();

        private void CommanderLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Le livre est stocké dans le dictionnaire et la commande est créee
        /// </summary>
        
        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandeLivre commandeLivre = new CommandeLivre(_viewModel);
            commandeLivre.Owner = this;
            commandeLivre.ShowDialog();


            Dictionary<long, Livre> DictionnaireLivres = _viewModel.DictionnaireLivres;
            long isbn = commandeLivre.ISBN;
            string titre = commandeLivre.TITRE;
            string auteur = commandeLivre.AUTEUR;
            string editeur = commandeLivre.EDITEUR;
            int annee = commandeLivre.ANNEE;

            //ajout seulement s'il n'existe pas deja 
            if (!DictionnaireLivres.ContainsKey(isbn))
            {
                DictionnaireLivres.Add(isbn, new Livre(isbn, titre, auteur, editeur, annee));
            }
          
            Commande nouvelleCommande = new Commande(isbn, titre, auteur, editeur, annee, "Attente");
            _viewModel.AjouterCommandeAttente(_viewModel.UtilisateurActif,nouvelleCommande);

        }
        //permet d'ouvrir une fenetre d'administarteur pour les utilisateurs avec le statut administrateur seulement
        public static RoutedCommand OuvrirAdminCmd = new RoutedCommand();

        private void OuvrirAdmin_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {//_viewModel != null grâce a chat gpt, car sinon ça ne fonctionnait pas
            if (_viewModel != null && _viewModel.UtilisateurActif != null)
            {
                e.CanExecute = _viewModel.UtilisateurActif.Administrateur;
            }
            else
            {
                e.CanExecute = false;
            }

        }

        private void OuvrirAdmin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreAdmin fenetreAdmin = new FenetreAdmin(_viewModel);
            fenetreAdmin.Owner = this;
            fenetreAdmin.ShowDialog();
        }
        //permet de fermer la fenetre
        public static RoutedCommand QuitterCmd = new RoutedCommand();

        private void Quitter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //permet de transferer les livres entre les utilisateur grace a une fenetre qui nous permet de choisir l'utilisateur désiré

        public static RoutedCommand TransfererLivreCmd = new RoutedCommand();
        private void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            {
                if (LivresUser.SelectedItem != null)
                {
                    e.CanExecute = true;
                }
                else { e.CanExecute = false; }
            }
        }

        private void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            choixUtilisateur choixUtilisateur = new choixUtilisateur(_viewModel);
            choixUtilisateur.Owner = this;
            choixUtilisateur.ShowDialog();
            Livre livreSelectionne = LivresUser.SelectedItem as Livre;
            _viewModel.AjouterLivre(choixUtilisateur.selection, livreSelectionne);
            _viewModel.RetirerLivre(_viewModel.UtilisateurActif, livreSelectionne);

        }
        //permet denlever une commande de la liste des commandes en attente de l'utilisateur avant qu'elle ne soit traité, la commande doit etre séléctionnée
        public static RoutedCommand AnnulerCommandeCmd = new RoutedCommand();
        private void AnnulerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {if(UserCommandesAttente.SelectedItem != null)
            {
                e.CanExecute = true;
            }else { e.CanExecute = false; }
        }

        private void AnnulerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UserCommandesAttente.SelectedItem != null)
            {
                Commande commandeAnnuler =UserCommandesAttente.SelectedItem as Commande;
           _viewModel.AnnulerCommandeAttente(_viewModel.UtilisateurActif, commandeAnnuler);
        }

    }
    }
}