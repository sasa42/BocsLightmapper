namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(BocsCyclesLight))]
    public class EditorCyclesLight : Editor
    {
        private bool debug = false;

        private Vector3 lastPositon;
        private Quaternion lastRostation;
        private Vector3 lastScale;

        public override void OnInspectorGUI()
        {
            BocsCyclesLight script = (BocsCyclesLight)target;

            script.AutoSync = EditorGUILayout.Toggle("Auto Sync", script.AutoSync);

            if (script.AutoSync) return;

            if (GUILayout.Button("Editor"))
            {
                EditorWindow.GetWindow<EditorNodeEdit>();
            }

            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical(GUI.skin.box);

            //script._enabled = EditorGUILayout.Toggle("Enabled",script._enabled);
            script.TypeSelected = EditorGUILayout.Popup("Type", script.TypeSelected, script.Type);
            if (script.TypeSelected == 2) script.SpotAngle = EditorGUILayout.FloatField("Spot Angle", script.SpotAngle);

            script.Shadow = EditorGUILayout.Toggle("Shadow", script.Shadow);
            script.UseMis = EditorGUILayout.Toggle("Multiple Importance", script.UseMis);
            script.Size = EditorGUILayout.FloatField("Size", script.Size);
            script.MaxBounces = EditorGUILayout.IntField("Max Bounces", script.MaxBounces);
            script.Diffuse = EditorGUILayout.Toggle("Diffuse", script.Diffuse);
            script.Glossy = EditorGUILayout.Toggle("Glossy", script.Glossy);
            script.Transmission = EditorGUILayout.Toggle("Transmission", script.Transmission);
            script.Scatter = EditorGUILayout.Toggle("Scatter", script.Scatter);

            script.IsPortal = EditorGUILayout.Toggle("Portal", script.IsPortal);

            GUILayout.EndVertical();

            bool needUpdate = false;
            if (EditorGUI.EndChangeCheck()) needUpdate = true;

            if (script.transform.root.position != lastPositon)
            {
                lastPositon = script.transform.root.position;
                needUpdate = true;
            }
            if (script.transform.root.rotation != lastRostation)
            {
                lastRostation = script.transform.root.rotation;
                needUpdate = true;
            }
            if (script.transform.root.localScale != lastScale)
            {
                lastScale = script.transform.root.localScale;
                needUpdate = true;
            }

            //Some Checks...

            if (script.Size < 0) script.Size = 0;
            if (script.MaxBounces < 0) script.MaxBounces = 0;

            if (needUpdate)
            {
                BocsCyclesAPI.UpdateObject(script.gameObject);
            }

            debug = EditorGUILayout.Foldout(debug, "Debug");
            if (debug)
            {
                //Debug.Log(script.GetShaderCount());
                for (int i = 0; i < script.GetGraphCount(); i++)
                {
                    //GUILayout.TextField(script._nodes);
                    GUI.skin.textArea.wordWrap = true;
                    EditorGUILayout.TextArea(script.Nodes[i]);
                }
            }
        }

    }
}