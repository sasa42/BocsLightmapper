namespace BOCSLIGHTMAPPER
{
    using System.Linq;
    using UnityEngine;
    using Type = System.Type;

    [System.Serializable]
    public class GradientWrapper
    {
        private static Type typeGradient;
        private Gradient gradient = new Gradient();

        static GradientWrapper()
        {
            TypeGradient = typeof(Gradient);
        }

        public static Type TypeGradient
        {
            get { return typeGradient; }
            set { typeGradient = value; }
        }

        public object GradientData
        {
            get { return gradient; }
            set { gradient = value as Gradient; }
        }

        public Color Evaluate(float time)
        {
            return gradient.Evaluate(time);
        }

        public void SetKeys(ColorKey[] colorKeys, AlphaKey[] alphaKeys)
        {
            GradientColorKey[] actualColorKeys = null;
            GradientAlphaKey[] actualAlphaKeys = null;

            if (colorKeys != null)
                actualColorKeys = colorKeys.Select(key => new GradientColorKey(key.SlotColor, key.SlotTime)).ToArray();
            if (alphaKeys != null)
                actualAlphaKeys = alphaKeys.Select(key => new GradientAlphaKey(key.SlotAlpha, key.SlotTime)).ToArray();

            gradient.SetKeys(actualColorKeys, actualAlphaKeys);
        }

        public struct ColorKey
        {
            public Color SlotColor;
            public float SlotTime;

            public ColorKey(Color color, float time)
            {
                this.SlotColor = color;
                this.SlotTime = time;
            }
        }

        public struct AlphaKey
        {
            public float SlotAlpha;
            public float SlotTime;

            public AlphaKey(float alpha, float time)
            {
                this.SlotAlpha = alpha;
                this.SlotTime = time;
            }
        }
    }
}