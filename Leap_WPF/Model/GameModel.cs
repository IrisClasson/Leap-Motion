using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Leap_WPF.Model
{
    public class GameModel : INotifyPropertyChanged
    {
        private double _canvasLeft;

        public double CanvasLeft
        {
            get { return _canvasLeft; }

            set
            {
                if (value != this._canvasLeft)
                {
                    _canvasLeft = value;
                    NotifyPropertyChanged("CanvasLeft");
                }
            }
        }

        private double _canvasTop;

        public double CanvasTop
        {
            get { return _canvasTop; }

            set
            {
                if (value != this._canvasTop)
                {
                    _canvasTop = value;
                    NotifyPropertyChanged("CanvasTop");
                }
            }
        }

        private Visibility _showCloud;

        public Visibility ShowCloud
        {
            get { return _showCloud; }

            set
            {
                if (value != this._showCloud)
                {
                    _showCloud = value;
                    NotifyPropertyChanged("ShowCloud");
                }
            }
        }

        private double _score;

        public double Score
        {
            get { return _score; }

            set
            {
                if (value != this._score)
                {
                    _score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        private string _gestureMade;

        public string GestureMade
        {
            get { return _gestureMade; }

            set
            {
                if (value != this._gestureMade)
                {
                    _gestureMade = value;
                    NotifyPropertyChanged("GestureMade");
                }
            }
        }

        private double _fingers;

        public double Fingers
        {
            get { return _fingers; }

            set
            {
                if (value != this._fingers)
                {
                    _fingers = value;
                    NotifyPropertyChanged("Fingers");
                }
            }
        }

        private SolidColorBrush _ellipseColor;

        public SolidColorBrush EllipseColor
        {
            get { return _ellipseColor; }

            set
            {
                if (value != this._ellipseColor)
                {
                    _ellipseColor = value;
                    NotifyPropertyChanged("EllipseColor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
