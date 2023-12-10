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
        public CommandeLivre(ViewModelBibliotheque viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        
    }
}
