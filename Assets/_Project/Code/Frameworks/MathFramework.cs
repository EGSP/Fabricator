using UnityEngine;

namespace Game.Code.Frameworks
{
    public static class MathFramework
    {

        public static int ToInt(this float x)
        {
            return Mathf.RoundToInt(x);
        }
    }
}