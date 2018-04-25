namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotBool : BocsSlotBase
    {
        private bool val;

        public BocsSlotBool(BocsNodeBase n, string description, string name, BocsSlotType type, bool defaultValue = true) : base(n, description, name, type)
        {
            Val = defaultValue;
        }

        public bool Val
        {
            get { return val; }
            set { val = value; }
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
            Val = UnityEditor.EditorGUILayout.Toggle(SlotTitle, Val);
#endif
        }

        public override void SetString(string val)
        {
            Val = bool.Parse(val);
        }

        public override string GetString()
        {
            return Val.ToString();
        }

        public override bool HasValue()
        {
            return true;
        }

        //public override string GetXML()
        //{
        //	return _list[_selected];
        //}
    }
}