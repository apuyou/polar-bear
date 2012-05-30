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
}