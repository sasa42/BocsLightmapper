namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotColor : BocsSlotBase
    {
        private Color colorVal = Color.white;

        public BocsSlotColor(BocsNodeBase n, string description, string name, BocsSlotType type, Color defaultValue = default(Color)) : base(n, description, name, type)
        {
            SlotColor = Color.yellow;
            ColorVal = defaultValue;
        }

        public Color ColorVal
        {
            get { return colorVal; }
            set { colorVal = value; }
        }

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
            return hex;
        }

        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, a);
        }

        public override void DrawSlotGUI()
        {
#if UNITY_EDITOR
            if (!CanDrawSlot()) return;

            if (InputSlot != null)
            {
                GUILayout.Label(this.SlotTitle);
                return;
            }
            ColorVal = UnityEditor.EditorGUILayout.ColorField(SlotTitle, ColorVal);
#endif
        }

        public override void SetString(string val)
        {
            ColorVal = HexToColor(val);
        }

        public override string GetString()
        {
            return ColorToHex(ColorVal);
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
            //return _color.r + ", " + _color.g + ", " + _color.b;
            return ColorVal.r + " " + ColorVal.g + " " + ColorVal.b;
        }
    }
}