namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotStringList : BocsSlotBase
    {
        private string[] list;
        private int selected = 0;

        public BocsSlotStringList(BocsNodeBase n, string description, string name, BocsSlotType type, int defaultValue) : base(n, description, name, type)
        {
            Selected = defaultValue;
        }

        public string[] List
        {
            get { return list; }
            set { list = value; }
        }

        public int Selected
        {
            get { return selected; }
            set { selected = value; }
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
            Selected = UnityEditor.EditorGUILayout.Popup(Selected, List);
#endif
        }

        public override void SetString(string val)
        {
            Selected = int.Parse(val);
        }

        public override string GetString()
        {
            return Selected.ToString();
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
            return List[Selected];
        }
    }
}