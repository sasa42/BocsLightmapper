namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotVector3 : BocsSlotBase
    {
        private Vector3 vect3 = Vector3.down;

        public BocsSlotVector3(BocsNodeBase n, string description, string name, BocsSlotType type, Vector3 defaultValue = default(Vector3)) : base(n, description, name, type)
        {
            SlotColor = Color.blue;
            Vect3 = defaultValue;
        }

        public Vector3 Vect3
        {
            get { return vect3; }
            set { vect3 = value; }
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
            Vect3 = UnityEditor.EditorGUILayout.Vector3Field(SlotTitle, Vect3);
#endif
        }

        public override string GetString()
        {
            return Vect3.x + " " + Vect3.y + " " + Vect3.z;
        }

        public override void SetString(string val)
        {
            string[] v = val.Split(' ');
            Vector3 vv = Vector3.zero;
            if (v.Length > 0) vv.x = float.Parse(v[0]);
            if (v.Length > 1) vv.y = float.Parse(v[1]);
            if (v.Length > 2) vv.z = float.Parse(v[2]);
            vect3 = vv;
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
            return Vect3.x + ", " + Vect3.y + ", " + Vect3.z;
        }
    }
}