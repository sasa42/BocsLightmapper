namespace BOCSLIGHTMAPPER
{
    using System.Reflection;
    using UnityEngine;
    using Type = System.Type;

    public static class GUIGradientField
    {
        private static MethodInfo methodGradientField1;
        private static MethodInfo methodGradientField2;

        static GUIGradientField()
        {
#if UNITY_EDITOR
            Type typeEditorGUILayout = typeof(UnityEditor.EditorGUILayout);
            methodGradientField1 = typeEditorGUILayout.GetMethod("GradientField", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(string), typeof(Gradient), typeof(GUILayoutOption[]) }, null);
            methodGradientField2 = typeEditorGUILayout.GetMethod("GradientField", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(Gradient), typeof(GUILayoutOption[]) }, null);
#endif
        }

        public static Gradient GradientField(string label, Gradient gradient, params GUILayoutOption[] options)
        {
            if (gradient == null)
                gradient = new Gradient();

            methodGradientField1.Invoke(null, new object[] { label, gradient, options });
            return gradient;
        }

        public static Gradient GradientField(Gradient gradient, params GUILayoutOption[] options)
        {
            if (gradient == null)
                gradient = new Gradient();

            methodGradientField2.Invoke(null, new object[] { gradient, options });
            return gradient;
        }
    }
}