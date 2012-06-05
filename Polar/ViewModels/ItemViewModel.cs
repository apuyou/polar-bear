using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Polar
{
    public class UVViewModel : INotifyPropertyChanged
    {
        private string _NomUV;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string NomUV
        {
            get
            {
                return _NomUV;
            }
            set
            {
                if (value != _NomUV)
                {
                    _NomUV = value;
                    NotifyPropertyChanged("NomUV");
                }
            }
        }

        private int _NbPages;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public int NbPages
        {
            get
            {
                return _NbPages;
            }
            set
            {
                if (value != _NbPages)
                {
                    _NbPages = value;
                    NotifyPropertyChanged("NbPages");
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


    public class NewsViewModel : INotifyPropertyChanged
    {
        private string _prenom;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                if (value != _prenom)
                {
                    _prenom = value;
                    NotifyPropertyChanged("prenom");
                }
            }
        }

        private string _date;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("date");
                }
            }
        }

        private string _titre;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string titre
        {
            get
            {
                return _titre;
            }
            set
            {
                if (value != _titre)
                {
                    _titre = value;
                    NotifyPropertyChanged("titre");
                }
            }
        }

        private string _contenu;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string contenu
        {
            get
            {
                return _contenu;
            }
            set
            {
                if (value != _contenu)
                {
                    _contenu = value;
                    NotifyPropertyChanged("contenu");
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
    public class HorairesViewModel : INotifyPropertyChanged
    {
        private string _Jour;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string Jour
        {
            get
            {
                return _Jour;
            }
            set
            {
                if (value != _Jour)
                {
                    _Jour = value;
                    NotifyPropertyChanged("Jour");
                }
            }
        }

        private string _Heures;
        /// <summary>
        /// Exemple de propriété ViewModel ; cette propriété est utilisée dans la vue pour afficher sa valeur à l'aide d'une liaison.
        /// </summary>
        /// <returns></returns>
        public string Heures
        {
            get
            {
                return _Heures;
            }
            set
            {
                if (value != _Heures)
                {
                    _Heures = value;
                    NotifyPropertyChanged("Heures");
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
}