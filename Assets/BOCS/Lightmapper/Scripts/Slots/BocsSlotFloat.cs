namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotFloat : BocsSlotBase
    {
        private float val = 0.0f;

        public BocsSlotFloat(BocsNodeBase n, string description, string name, BocsSlotType type, float defaultValue = 1.0f) : base(n, description, name, type)
        {
            Val = defaultValue;
        }

        public float Val
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
            UnityEditor.EditorGUILayout.BeginHorizontal();
            Val = UnityEditor.EditorGUILayout.FloatField(SlotTitle, Val);
            GUI.skin = null;
            if (GUILayout.Button("0", GUILayout.Width(20), GUILayout.Height(16))) Val = 0;
            if (GUILayout.Button("1", GUILayout.Width(20), GUILayout.Height(16))) Val = 1;

            UnityEditor.EditorGUILayout.EndHorizontal();
#endif
        }

        public override string GetString()
        {
            return Val.ToString();
        }

        public override void SetString(string val)
        {
            Val = float.Parse(val);
        }

        public override bool HasValue()
        {
            return true;
        }
    }
}