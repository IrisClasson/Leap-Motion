using System.Collections.Generic;
using Leap;

namespace Leap_WPF.Util
{
    public static class LeapGestures
    {
        public static readonly Dictionary<Gesture.GestureType, string> GestureTypesLookUp = new Dictionary<Gesture.GestureType, string>()
                                     {
                                         {Gesture.GestureType.TYPEKEYTAP,"Tap gesture"},
                                         {Gesture.GestureType.TYPECIRCLE, "Circle gesture"},
                                         {Gesture.GestureType.TYPESWIPE, "Swipe gesture"},
                                         {Gesture.GestureType.TYPESCREENTAP, "Screen tap"}
                                     };
    }
}
