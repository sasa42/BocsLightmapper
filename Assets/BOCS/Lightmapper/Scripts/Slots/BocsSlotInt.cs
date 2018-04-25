namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotInt : BocsSlotBase
    {
        private int val = 0;

        public BocsSlotInt(BocsNodeBase n, string description, string name, BocsSlotType type, int defaultValue = 0) : base(n, description, name, type)
        {
            Val = defaultValue;
        }

        public int Val
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
            Val = UnityEditor.EditorGUILayout.IntField(SlotTitle, Val);
#endif
        }

        public override string GetString()
        {
            return Val.ToString();
        }

        public override void SetString(string val)
        {
            Val = int.Parse(val);
        }

        public override bool HasValue()
        {
            return true;
        }
    }
}