namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotGradient : BocsSlotBase
    {
        private Gradient grad;
        private GradientWrapper gradient;

        public BocsSlotGradient(BocsNodeBase n, string description, string name, BocsSlotType type) : base(n, description, name, type)
        {
        }

        public Gradient Grad
        {
            get { return grad; }
            set { grad = value; }
        }

        public override void DrawSlotGUI()
        {
            if (!CanDrawSlot()) return;

            if (InputSlot != null)
            {
                GUILayout.Label(this.SlotTitle);
                return;
            }
            Grad = GUIGradientField.GradientField(Grad);
        }

        public override string GetString()
        {
            //return AssetDatabase.GetAssetPath(_tex);
            return string.Empty;
        }

        public override void SetString(string val)
        {
            //_tex = AssetDatabase.LoadAssetAtPath<Texture2D>(val);
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
            //return Application.dataPath + AssetDatabase.GetAssetPath(_tex).Substring(6);
            return string.Empty;
        }
    }
}