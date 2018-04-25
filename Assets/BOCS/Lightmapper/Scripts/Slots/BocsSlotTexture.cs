namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotTexture : BocsSlotBase
    {
        private Texture2D tex = null;
        private string guid;

        public BocsSlotTexture(BocsNodeBase n, string description, string name, BocsSlotType type) : base(n, description, name, type)
        {
            SlotColor = Color.yellow;
        }

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
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
            GUISkin oldSkin = GUI.skin;
            GUI.skin = null;
            Tex = (Texture2D)UnityEditor.EditorGUILayout.ObjectField(Tex, typeof(Texture2D), false, GUILayout.Width(128), GUILayout.Height(128));
            GUI.skin = oldSkin;
#endif
        }

        public override string GetString()
        {
#if UNITY_EDITOR
            Guid = UnityEditor.AssetDatabase.AssetPathToGUID(UnityEditor.AssetDatabase.GetAssetPath(Tex));
#endif
            return Guid;
        }

        public override void SetString(string val)
        {
            Guid = val;
#if UNITY_EDITOR
            Tex = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(UnityEditor.AssetDatabase.GUIDToAssetPath(val));
#endif
        }

        public override bool HasValue()
        {
            return true;
        }

        public override string GetXML()
        {
#if UNITY_EDITOR
            if (Tex == null) return string.Empty;
            return Application.dataPath + UnityEditor.AssetDatabase.GetAssetPath(Tex).Substring(6);
#else
            if (_guid == "") return "";
            return Application.dataPath + "/cache/" + _guid;
#endif
        }
    }
}