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
using System.ComponentModel;
using System.Threading;

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
            var tblock = (TextBlock) (((StackPanel)sender).Children[0]);

            if (panier.Contains(tblock.Text))
            {
                panier.Remove(tblock.Text);
                tblock.FontStyle = FontStyles.Normal;
            }
            else
            {
                tblock.FontStyle = FontStyles.Italic;
                ajouterAuPanier(tblock.Text);
            }
        }

        private void CommandeAnnales_Tap(object sender, GestureEventArgs e)
        {
            button1.Visibility = Visibility.Collapsed;
            loginCommande.Visibility = Visibility.Collapsed;
            TexteAnnales.Visibility = Visibility.Visible;
            TexteAnnales.Text = "Envoi de la commande...";

            if (panier.Count == 0 || panier.Count > 8)
            {
                TexteAnnales.Text = "Choisissez entre 1 et 8 UVs.";
                Perform(() => finCommande(), 2000);
                return;
            }

            var client = new WebClient();
            var texte = "login=" + loginCommande.Text + "&annales=";
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
            //client.UploadStringCompleted += new UploadStringCompletedEventHandler(handlerCommande);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(handlerCommande);
            client.OpenReadAsync(new Uri("http://assos.utc.fr/polar/annales/json?" + texte));
            //client.UploadStringAsync(new Uri("http://assos.utc.fr/polar/annales/borne?commander"), texte);
        }

        private void ajouterAuPanier(string nom)
        {
            if (panier.Count < 8) {
                panier.Add(nom);
            }
        }

        private List<string> panier;

    
        private void handlerCommande(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var replyText = new StreamReader(e.Result).ReadToEnd();
                int number;
                if (Int32.TryParse(replyText, out number))
                    TexteAnnales.Text = "Commande enregistrée sous le n°" + replyText + " !";
                else
                    TexteAnnales.Text = replyText;
            }
            else
                TexteAnnales.Text = "Une erreur réseau s'est produite.";

            Perform(() => finCommande(), 2000);
        }

        private void Perform(Action myMethod, int delayInMilliseconds)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) => Thread.Sleep(delayInMilliseconds);
            worker.RunWorkerCompleted += (s, e) => myMethod.Invoke();
            worker.RunWorkerAsync();
        }

        private void finCommande()
        {
            TexteAnnales.Text = "";
            TexteAnnales.Visibility = Visibility.Collapsed;
            button1.Visibility = Visibility.Visible;
            loginCommande.Visibility = Visibility.Visible;
        }

    }
}