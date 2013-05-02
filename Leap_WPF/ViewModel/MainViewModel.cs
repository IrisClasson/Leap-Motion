using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Leap;
using Leap_WPF.Model;
using Leap_WPF.Util;
using Vector = Leap.Vector;

namespace Leap_WPF.ViewModel
{
    public class MainViewModel
    {
        private static Controller _controller;
        private static CustomLeapListener _listener;
        private bool _enableMouse;
        private readonly Dispatcher _dispatcher;
        public GameModel GameModel;
    
        public MainViewModel(Controller controller, CustomLeapListener listener, GameModel gameModel)
        {
            GameModel = gameModel;
            _listener = listener;
            _controller = controller;
            _controller.AddListener(_listener);
            _dispatcher = Application.Current.Dispatcher;
            RegisterEvents();
            _enableMouse = false;
        }

        void RegisterEvents()
        {
            _listener.OnFingersRegistered += OnFingersRegistered;
            _listener.OnGestureMade += OnGestureMade;
        }

        public void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _controller.RemoveListener(_listener);
            _controller.Dispose();
            _listener.Dispose();
        }

        public void MouseOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _enableMouse = true;
        }

        void OnGestureMade(GestureList gestures)
        {
            foreach (var gesture in gestures)
                GameModel.GestureMade = LeapGestures.GestureTypesLookUp[gesture.Type];
        }

        void OnFingersRegistered(FingerList fingers)
        {
            var screen = _controller.CalibratedScreens.ClosestScreenHit(fingers[0]);
            var coordinate = CalculationsHelper.GetNormalizedXAndY(fingers, screen);

            _dispatcher.Invoke(new Action(() =>
                                              {
                                                  GameModel.Fingers = fingers.Count;
                                                  MaintainScore(coordinate);
                                                  UpdateUIProperties(coordinate);
                                              }));
            SetLeapAsMouse(fingers,coordinate);
        }

        void UpdateUIProperties(Point coordinate)
        {
            GameModel.CanvasLeft = coordinate.X;
            GameModel.CanvasTop = coordinate.Y;
        }

        void MaintainScore(Point coordinate)
        {
            if (!CalculationsHelper.IsWithinRangeOfCloud((int) coordinate.X, (int) coordinate.Y)) return;
            GameModel.Score += 100;
            if (GameModel.Score >= 50000) GameModel.ShowCloud = Visibility.Collapsed;
        }

        void SetLeapAsMouse(FingerList fingers, Point coordinate)
        {
            if (!_enableMouse) return;
            var screen = _controller.CalibratedScreens.ClosestScreenHit(fingers[0]);
            if (screen == null || !screen.IsValid) return;
            EnableLeapAsCursor(coordinate, fingers);
            EnableClickWithLeap(coordinate, fingers);
        }

        static void EnableLeapAsCursor(Point coordinate, FingerList fingers)
        {
            if ((int) fingers[0].TipVelocity.Magnitude <= 25) return;
            LeapAsMouse.SetCursorPos((int)coordinate.X, (int)coordinate.Y);
        }

        static void EnableClickWithLeap(Point coordinate,FingerList fingers)
        {
            if (fingers.Count != 2) return;
             LeapAsMouse.mouse_event(0x0002 | 0x0004, 0, (int) coordinate.X, (int) coordinate.Y, 0);
        }


    }
}
