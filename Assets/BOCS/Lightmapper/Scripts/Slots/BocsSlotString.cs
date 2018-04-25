namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotString : BocsSlotBase
    {
        private string str = string.Empty;

        public BocsSlotString(BocsNodeBase n, string description, string name, BocsSlotType type) : base(n, description, name, type)
        {
        }

        public string Str
        {
            get { return str; }
            set { str = value; }
        }

        public override void DrawSlotGUI()
        {
            if (!CanDrawSlot()) return;

            if (InputSlot != null)
            {
                GUILayout.Label(this.SlotTitle);
                return;
            }
            Str = GUILayout.TextField(Str);
        }

        public override void SetString(string val)
        {
            Str = val;
        }

        public override string GetString()
        {
            return Str;
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
            return Str;
        }
    }
}