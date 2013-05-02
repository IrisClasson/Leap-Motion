using System;
using System.Threading.Tasks;
using Leap;

namespace Leap_WPF.Util
{
    public class CustomLeapListener : Listener
    {
        public event Action<FingerList> OnFingersRegistered;
        public event Action<GestureList> OnGestureMade;
        private long _now;
        private long _previous;
        private long _timeDifference;

        public override void OnInit(Controller controller) { }

        public override void OnConnect(Controller controller)
        {
            foreach (var gesture in (Gesture.GestureType[])Enum.GetValues(typeof(Gesture.GestureType)))
                controller.EnableGesture(gesture);
        }

        public override void OnDisconnect(Controller controller) { }

        public override void OnExit(Controller controller) { }

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();
            _now = frame.Timestamp;
            _timeDifference = _now - _previous;

            if (frame.Hands.Empty) return;

            _previous = frame.Timestamp;

            if (_timeDifference < 1000) return;
            // Run async
            if (frame.Gestures().Count > 0)
                Task.Factory.StartNew(()=> OnGestureMade(frame.Gestures()));
            if (frame.Fingers.Count > 0)
                Task.Factory.StartNew(() => OnFingersRegistered(frame.Fingers));
        }
    }
}
