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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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

            List<Annale> deserializedUser = new List<Annale>();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("[{'Nom':'AP30', 'Pages':21}, {'Nom':'AR03', 'Pages':6}]"));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as List<Annale>;
            ms.Close();
            foreach (Annale i in deserializedUser)
            {
                UVs.Add(new UVViewModel() { NomUV = i.Nom, NbPages = i.Pages });
            }

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

    [DataContract]
    public class Annale
    {
        [DataMember]
        public string Nom;
        [DataMember]
        public int Pages;
    }
}