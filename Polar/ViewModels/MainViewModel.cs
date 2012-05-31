﻿using System;
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
            this.News = new ObservableCollection<NewsViewModel>();
            this.Horaires = new ObservableCollection<HorairesViewModel>();
        }

        /// <summary>
        /// Collection pour les objets ItemViewModel.
        /// </summary>
        public ObservableCollection<UVViewModel> UVs { get; private set; }
        public ObservableCollection<NewsViewModel> News { get; private set; }
        public ObservableCollection<HorairesViewModel> Horaires { get; private set; }

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
            WebRequest.RegisterPrefix("http://assos.utc.fr/polar", WebRequestCreator.ClientHttp);
            WebClient annalesDownloader = new WebClient();
            annalesDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(annalesRecuperees);
            annalesDownloader.OpenReadAsync(new Uri("http://assos.utc.fr/polar/annales/json?liste-annales"));

            WebClient newsDownloader = new WebClient();
            newsDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(newsRecuperees);
            newsDownloader.OpenReadAsync(new Uri("http://assos.utc.fr/polar/news/index?json"));

            Uri horairesUri = new Uri("http://assos.utc.fr/polar/membres/horaires?json");
            WebClient horairesDownloader = new WebClient();
            horairesDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(horairesDownloader_OpenReadCompleted);
            horairesDownloader.OpenReadAsync(horairesUri);

            this.IsDataLoaded = true;
        }

        private void annalesRecuperees(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<Annale> deserializedUser = new List<Annale>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
                deserializedUser = ser.ReadObject(e.Result) as List<Annale>;
                foreach (Annale i in deserializedUser)
                {
                    UVs.Add(new UVViewModel() { NomUV = i.Nom, NbPages = i.Pages });
                }
            }
        }

        private void newsRecuperees(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<News> deserializedUser = new List<News>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
                deserializedUser = ser.ReadObject(e.Result) as List<News>;
                foreach (News i in deserializedUser)
                {
                    News.Add(new NewsViewModel() { prenom = i.Prenom, date = i.date, contenu = i.news, titre = i.titre });
                }
            }
        }

        private void horairesDownloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                List<Horaire> deserializedUser = new List<Horaire>();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
                deserializedUser = ser.ReadObject(e.Result) as List<Horaire>;
                foreach (Horaire i in deserializedUser)
                {
                    Horaires.Add(new HorairesViewModel() { Jour = i.jour, Heures = i.heures});
                }
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

    [DataContract]
    public class Horaire
    {
        [DataMember]
        public string jour;
        [DataMember]
        public string heures;
    }

    [DataContract]
    public class News
    {
        [DataMember]
        public string Prenom;
        [DataMember]
        public string date;
        [DataMember]
        public string titre;
        [DataMember]
        public string news;
    }
}