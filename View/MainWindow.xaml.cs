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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ViewModelBibliotheque _viewModel;
        public ObservableCollection<Livre> LivresUtilisateur;
        public ObservableCollection<Commande> CommandesAttente;
        public ObservableCollection<Commande> CommandesTraitees;
        public MainWindow()

        {
            InitializeComponent();
            _viewModel = new ViewModelBibliotheque();
            DataContext = _viewModel;
            NomUtilisateur.Text = UtilisateurActif.Nom;
            LivresUtilisateur = UtilisateurActif.ListeLivres;
            CommandesAttente = UtilisateurActif.ListeCommandesAttente;
            CommandesTraitees = UtilisateurActif.ListeCommandesTraites;
            LivresUser.ItemsSource = LivresUtilisateur;
            UserCommandesAttente.ItemsSource = CommandesAttente;
            UserCommandesTraitees.ItemsSource= CommandesTraitees;


        }

        public Membre UtilisateurActif
        {
            get { return _viewModel.MembreActif; }
            set { _viewModel.MembreActif = value; }
        }


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
            UtilisateurActif = _choixUtilisateur.selection;
            OnPropertyChanged(nameof(*));


        }

        public static RoutedCommand CommanderLivreCmd = new RoutedCommand();

        private void CommanderLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandeLivre commandeLivre = new CommandeLivre(_viewModel);
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
            FenetreAdmin fenetreAdmin = new FenetreAdmin(_viewModel);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            choixUtilisateur choixUtilisateur = new choixUtilisateur(_viewModel);
            choixUtilisateur.Owner = this;
            choixUtilisateur.ShowDialog();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}