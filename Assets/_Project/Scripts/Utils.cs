using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Proxima_K.Utils {
    public class PK {
        public static Vector2 GetMouseWorldPosition2D(Camera cam) {
            return cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}