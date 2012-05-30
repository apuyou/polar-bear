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
using System.Net.Browser;
using System.IO;

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
            ajouterAuPanier(((TextBlock)a).Text);
            passerCommande("superman");
        }

        private void ajouterAuPanier(string nom)
        {
            if (panier.Count < 8) {
                panier.Add(nom);
            }
        }

        private List<string> panier;

        private void passerCommande(string login)
        {
            if (panier.Count == 0 || panier.Count > 8) 
            {
                return;
            }

            var client = new WebClient();
            var texte = "login=" + login + "&annales=";
            int i = 0;
            foreach (string nom in panier)
            {
                if (i > 0)
                {
                    texte += ",";
                } 
                texte += nom;
                i++;
            }
            Date.Text = texte; // DEBUG
            //client.UploadStringCompleted += new UploadStringCompletedEventHandler(handlerCommande);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(handlerCommande);
            client.OpenReadAsync(new Uri("http://assos.utc.fr/polar/annales/json?"+texte));
            //client.UploadStringAsync(new Uri("http://assos.utc.fr/polar/annales/borne?commander"), texte);
        }

        private void handlerCommande(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var reply = (Stream)e.Result;
                Date.Text = new StreamReader(reply).ReadToEnd();
            }
        }
    }
}