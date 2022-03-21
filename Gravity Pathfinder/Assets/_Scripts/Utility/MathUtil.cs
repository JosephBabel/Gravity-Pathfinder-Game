using UnityEngine;

namespace Utility 
{
    public static class MathUtil
    {
        public static float ClampAngle(float angle, float minAngle, float maxAngle)
        {
            bool isOffset = false;
            if (angle > 180) { isOffset = true; angle -= 360; }

            if (isOffset)
            {
                return Mathf.Clamp(angle, minAngle, maxAngle) + 360;
            }
            else
            {
                return Mathf.Clamp(angle, minAngle, maxAngle);
            }
        }
    }
}

