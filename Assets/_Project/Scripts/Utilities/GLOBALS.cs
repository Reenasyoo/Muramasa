using UnityEngine;

namespace Muramasa.Utilities
{ 
    public static class GLOBALS
    {
        public const string HORIZONTAL_STRING = "Horizontal";
        public const string VERTICAL_STRING = "Vertical";
        
        public static readonly Vector3 _DownDirection = new Vector3(0, -1, 0);
        public static readonly Vector3 _UpDirection = new Vector3(0, 1, 0);
        public static readonly Vector3 _ZeroVector = new Vector3(0, 0, 0);

        public const float FLOAT_TOLERANCE = 0.000000001f;
        
        public static readonly Vector3 Gravity = Physics.gravity;
    }
}