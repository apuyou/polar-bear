using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;

namespace Polar
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            panier = new List<string>();

            // Affecter l'exemple de données au contexte de données du contrôle ListBox
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Charger les données pour les éléments ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();

            }
        }

        private void StackPanel_Tap(object sender, GestureEventArgs e)
        {
            var a = (((StackPanel)sender).Children[0]);
            Date.Text = ((TextBlock)a).Text;
            ajouter_au_panier(((TextBlock)a).Text);
        }

        private void ajouter_au_panier(string nom)
        {
            if (panier.Count < 8) {
                panier.Add(nom);
            }
        }

        private List<string> panier;
    }
}