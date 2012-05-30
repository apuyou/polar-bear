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

            this.IsDataLoaded = true;
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