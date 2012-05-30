using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Browser;
using System.IO;
using Newtonsoft.Json;

namespace Polar
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.UVs = new ObservableCollection<UVViewModel>();
        }

        /// <summary>
        /// Collection pour les objets ItemViewModel.
        /// </summary>
        public ObservableCollection<UVViewModel> UVs { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Crée et ajoute quelques objets ItemViewModel dans la collection Items.
        /// </summary>
        public void LoadData()
        {
            // Exemple de données ; remplacer par des données réelles
            this.UVs.Add(new UVViewModel() { NomUV = "MT22", NbPages = 10 });
            this.UVs.Add(new UVViewModel() { NomUV = "CM11", NbPages = 90 });
            this.UVs.Add(new UVViewModel() { NomUV = "MT22", NbPages = 10 });
            this.UVs.Add(new UVViewModel() { NomUV = "PI31", NbPages = 100 });
            this.UVs.Add(new UVViewModel() { NomUV = "MT22", NbPages = 10 });
            this.UVs.Add(new UVViewModel() { NomUV = "MT22", NbPages = 10 });

            WebRequest.RegisterPrefix("http://assos.utc.fr/polar", WebRequestCreator.ClientHttp);
            Uri horairesUri = new Uri("http://assos.utc.fr/polar/membres/horaires?json");
            WebClient horairesDownloader = new WebClient();
            horairesDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(horairesDownloader_OpenReadCompleted);
            horairesDownloader.OpenReadAsync(horairesUri);

            this.IsDataLoaded = true;
        }

        private void horairesDownloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                JsonTextReader horaires = new JsonTextReader(new StreamReader(e.Result));
                JsonSerializer se = new JsonSerializer();
                object parsedData = se.Deserialize(horaires);

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}