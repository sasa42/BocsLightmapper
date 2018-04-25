namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(BocsCyclesMaterial))]
    //[CanEditMultipleObjects]
    public class EditorCyclesMaterial : Editor
    {
        private Texture texLogo;
        private Texture texShiny;
        private Texture texGlass;
        private Texture texChrome;
        private Texture texMetal;
        private Texture texChecker;
        private Texture texWire;
        private Texture texLight;
        private Texture texReset;

        private bool debug = false;
        private BocsCyclesMaterial script;

        public override void OnInspectorGUI()
        {
            if (texLogo == null) Init();
            //Rect imgRect = GUILayoutUtility.GetRect(Screen.width - 64, 32);
            //GUI.DrawTexture(imgRect, _logo, ScaleMode.ScaleToFit);

            script = (BocsCyclesMaterial)target;

            int w = 32;
            int h = 32;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(texReset, GUILayout.Width(w), GUILayout.Height(h)))
            {
                script.Reset();
                UpdateNodeEditor();
            }

            if (GUILayout.Button(texShiny, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FF0000FF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=1:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texGlass, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=350,y=10:node|t=BocsNodeDisneyBsdf,x=30,y=10:node|t=BocsNodeAbsorptionVolume,x=30,y=460:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=1:val|n=2,s=color,v=FF0000FF:val|n=2,s=density,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:connect|n1=2,n2=0,s1=volume,s2=volume,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texChrome, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=1:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texMetal, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=1:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.25:val|n=1,s=anisotropic,v=1:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=0:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=1:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texChecker, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=690,y=20,c=0:node|t=BocsNodeDisneyBsdf,x=350,y=20,c=0:node|t=BocsNodeCheckerTexture,x=20,y=20,c=0:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.5:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:val|n=2,s=color1,v=848484FF:val|n=2,s=color2,v=FFFFFFFF:val|n=2,s=scale,v=4:connect|n1=1,n2=0,s1=bsdf,s2=surface,:connect|n1=2,n2=1,s1=color,s2=base_color,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texWire, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=540,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=120:node|t=BocsNodeWireframe,x=40,y=10:node|t=BocsNodeMixShader,x=380,y=10:node|t=BocsNodeDiffuseBsdf,x=40,y=580:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.5:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:val|n=2,s=use_pixel_size,v=False:val|n=2,s=size,v=0.01:val|n=3,s=fac,v=1:val|n=4,s=color,v=FF0000FF:val|n=4,s=roughness,v=0:connect|n1=1,n2=3,s1=bsdf,s2=closure1,:connect|n1=2,n2=3,s1=fac,s2=fac,:connect|n1=3,n2=0,s1=closure,s2=surface,:connect|n1=4,n2=3,s1=bsdf,s2=closure2,:");
                UpdateNodeEditor();
            }
            if (GUILayout.Button(texLight, GUILayout.Width(w), GUILayout.Height(h)))
            {
                UpdateMat("node|t=BocsNodeOutput,x=370,y=20,c=0:node|t=BocsNodeEmission,x=30,y=20,c=0:val|n=1,s=color,v=FAF3B5FF:val|n=1,s=strength,v=1:connect|n1=1,n2=0,s1=emission,s2=surface,:");
                UpdateNodeEditor();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Editor"))
            {
                EditorWindow.GetWindow<EditorNodeEdit>();
            }
            //script._cacheMesh = EditorGUILayout.Toggle("Cache Mesh",script._cacheMesh);

            EditorGUI.BeginChangeCheck();//Don't Update Because we click Editor!!

            GUILayout.BeginVertical(GUI.skin.box);

            script.Visibility = (BocsCyclesMaterial.PathRayFlag)EditorGUILayout.EnumMaskField("Visibility", (System.Enum)script.Visibility);
            //script._smooth = EditorGUILayout.Toggle("Smooth",script._smooth);

            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck()) BocsCyclesAPI.UpdateObject(script.gameObject);

            //EditorGUI.BeginChangeCheck();//Don't Update Because we click Editor!!

            //GUILayout.BeginVertical(GUI.skin.box);

            //script._use_mis = EditorGUILayout.Toggle("Multiple Importance",script._use_mis);
            //script._use_transparent_shadow = EditorGUILayout.Toggle("Transparent Shadow",script._use_transparent_shadow);
            //script._heterogeneous_volume = EditorGUILayout.Toggle("Heterogeneous Volume",script._heterogeneous_volume);
            //script._volume_sampling_method =  EditorGUILayout.Popup("Volume Sampling",script._volume_sampling_method,script._volume_sampling_methods);
            //script._volume_interpolation_method =  EditorGUILayout.Popup("Volume Interpolation",script._volume_interpolation_method,script._volume_interpolation_methods);

            //GUILayout.EndVertical();

            //if (EditorGUI.EndChangeCheck()) BocsCycles.CyclesUpdateMaterial(script.gameObject);

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

        private void Init()
        {
            texLogo = Resources.Load("logo") as Texture;
            texShiny = Resources.Load("ebShiny") as Texture;
            texGlass = Resources.Load("ebGlass") as Texture;
            texChrome = Resources.Load("ebChrome") as Texture;
            texMetal = Resources.Load("ebMetal") as Texture;
            texChecker = Resources.Load("ebChecker") as Texture;
            texWire = Resources.Load("ebWire") as Texture;
            texLight = Resources.Load("ebLight") as Texture;
            texReset = Resources.Load("ebReset") as Texture;
        }

        private void UpdateNodeEditor()
        {
            EditorNodeEdit[] windows = Resources.FindObjectsOfTypeAll<EditorNodeEdit>();
            if (windows != null && windows.Length > 0)
            {
                foreach (EditorNodeEdit w in windows)
                {
                    w.ReloadChange();
                }
            }
        }

        private void UpdateMat(string mat)
        {
            for (int i = 0; i < script.GetGraphCount(); i++)
            {
                script.Nodes[i] = mat;
            }
            UpdateNodeEditor();
            BocsCyclesAPI.UpdateMaterial(script.gameObject);
        }
    }
}