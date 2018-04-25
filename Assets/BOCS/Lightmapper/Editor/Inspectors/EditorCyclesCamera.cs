namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(BocsCyclesCamera))]
    public class EditorCyclesCamera : Editor
    {
        private static Texture texlogo;
        private static Texture texSky;
        private static Texture texSoftlight;
        private static Texture texHDR;

        //Foldouts
        private bool quick = true;

        private bool camera = false;
        private bool film = false;
        private bool integrator = false;
        private bool background = false;
        private bool debug = false;

        public override void OnInspectorGUI()
        {
            if (texlogo == null) Init();
            //Rect imgRect = GUILayoutUtility.GetRect(Screen.width - 64, 32);
            //GUI.DrawTexture(imgRect, _logo, ScaleMode.ScaleToFit);

            BocsCyclesCamera script = (BocsCyclesCamera)target;

            bool needUpdate = false;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(texSky, GUILayout.Width(45), GUILayout.Height(45)))
            {
                script.Nodes[0] = "node|t=BocsNodeOutput,x=940,y=60,c=0:node|t=BocsNodeBackground,x=530,y=40,c=0:node|t=BocsNodeSkyTexture,x=42,y=30,c=0:node|t=BocsNodeEnviromentTexture,x=80,y=200,c=0:val|n=1,s=color,v=FFFFFFFF:val|n=1,s=strength,v=0.75:val|n=2,s=sun_direction,v=0 0 1:val|n=2,s=turbidity,v=2.2:val|n=2,s=ground_albedo,v=0.3:val|n=3,s=filename,v=:val|n=3,s=color_space,v=1:val|n=3,s=use_alpha,v=True:val|n=3,s=interpolation,v=1:val|n=3,s=projection,v=0:connect|n1=1,n2=0,s1=background,s2=surface,:connect|n1=2,n2=1,s1=color,s2=color,:";
                UpdateNodeEditor();
                needUpdate = true;
            }
            if (GUILayout.Button(texSoftlight, GUILayout.Width(45), GUILayout.Height(45)))
            {
                script.Nodes[0] = "node|t=BocsNodeOutput,x=940,y=60,c=0:node|t=BocsNodeBackground,x=530,y=40,c=0:node|t=BocsNodeSkyTexture,x=42,y=30,c=0:node|t=BocsNodeEnviromentTexture,x=80,y=200,c=0:val|n=1,s=color,v=FFFFFFFF:val|n=1,s=strength,v=0.25:val|n=2,s=sun_direction,v=0 0 1:val|n=2,s=turbidity,v=2.2:val|n=2,s=ground_albedo,v=0.3:val|n=3,s=filename,v=:val|n=3,s=color_space,v=1:val|n=3,s=use_alpha,v=True:val|n=3,s=interpolation,v=1:val|n=3,s=projection,v=0:connect|n1=1,n2=0,s1=background,s2=surface,:";
                UpdateNodeEditor();
                needUpdate = true;
            }
            if (GUILayout.Button(texHDR, GUILayout.Width(45), GUILayout.Height(45)))
            {
                script.Nodes[0] = "node|t=BocsNodeOutput,x=940,y=60,c=0:node|t=BocsNodeBackground,x=530,y=40,c=0:node|t=BocsNodeSkyTexture,x=42,y=30,c=0:node|t=BocsNodeEnviromentTexture,x=80,y=200,c=0:val|n=1,s=color,v=FFFFFFFF:val|n=1,s=strength,v=0.5:val|n=2,s=sun_direction,v=0 0 1:val|n=2,s=turbidity,v=2.2:val|n=2,s=ground_albedo,v=0.3:val|n=3,s=filename,v=12a586c687c7a544890dc2fe09370550:val|n=3,s=color_space,v=1:val|n=3,s=use_alpha,v=True:val|n=3,s=interpolation,v=1:val|n=3,s=projection,v=0:connect|n1=1,n2=0,s1=background,s2=surface,:connect|n1=3,n2=1,s1=color,s2=color,:";
                UpdateNodeEditor();
                needUpdate = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Background Shader Editor"))
            {
                EditorWindow.GetWindow<EditorNodeEdit>();
            }

            quick = EditorGUILayout.Foldout(quick, "Quick Settings");
            if (quick)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);

                EditorGUI.BeginChangeCheck();

                script._type = EditorGUILayout.Popup("Projection", script._type, script._types);

                script._fov = EditorGUILayout.FloatField("Field of View", script._fov);
                script._farclip = EditorGUILayout.FloatField("Far Clip", script._farclip);

                script._exposure = EditorGUILayout.FloatField("Exposure", script._exposure);

                //script._aperturesize = EditorGUILayout.FloatField("Aperture Size", script._aperturesize);

                //EditorGUILayout.BeginHorizontal();
                //script._focaldistance = EditorGUILayout.FloatField("Focal Distance", script._focaldistance);
                //EditorGUILayout.EndHorizontal();

                script._visibility = (BocsCyclesCamera.PathRayFlag)EditorGUILayout.EnumMaskField("Background Visibility", (System.Enum)script._visibility);
                script._use_shader = GUILayout.Toggle(script._use_shader, "Use Shader");

                script._filter_glossy = EditorGUILayout.FloatField("Filter Glossy", script._filter_glossy);
                script._sample_clamp_direct = EditorGUILayout.FloatField("Clamp Direct", script._sample_clamp_direct);
                script._sample_clamp_indirect = EditorGUILayout.FloatField("Clamp Indirect", script._sample_clamp_indirect);
                script._light_sampling_threshold = EditorGUILayout.FloatField("Light Sampling Threshold", script._light_sampling_threshold);

                script._caustics_reflective = GUILayout.Toggle(script._caustics_reflective, "Reflective Caustics");
                script._caustics_refractive = GUILayout.Toggle(script._caustics_refractive, "Refractive Caustics");

                if (EditorGUI.EndChangeCheck()) needUpdate = true;
                EditorGUILayout.EndVertical();
            }

            camera = EditorGUILayout.Foldout(camera, "Camera");
            if (camera)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);

                EditorGUI.BeginChangeCheck();

                script._shuttertime = EditorGUILayout.FloatField("Shutter Time", script._shuttertime);

                script._motion_position = EditorGUILayout.Popup("Motion Positon", script._motion_position, script._motion_positions);
                script._rolling_shutter_type = EditorGUILayout.Popup("Rolling Shutter Type", script._rolling_shutter_type, script._rolling_shutter_types);
                script._rolling_shutter_duration = EditorGUILayout.FloatField("Rolling Shutter Duration", script._rolling_shutter_duration);

                script._aperturesize = EditorGUILayout.FloatField("Aperture Size", script._aperturesize);

                EditorGUILayout.BeginHorizontal();
                script._focaldistance = EditorGUILayout.FloatField("Focal Distance", script._focaldistance);
                EditorGUILayout.EndHorizontal();

                script._blades = EditorGUILayout.IntField("Blades", script._blades);
                script._bladesrotation = EditorGUILayout.FloatField("Blades Rotation", script._bladesrotation);

                script._aperture_ratio = EditorGUILayout.FloatField("Aperture Ratio", script._aperture_ratio);

                script._type = EditorGUILayout.Popup("Projection", script._type, script._types);

                script._panorama_type = EditorGUILayout.Popup("Panorama", script._panorama_type, script._panorama_types);

                script._fisheye_fov = EditorGUILayout.FloatField("Fisheye Field of View", script._fisheye_fov);
                script._fisheye_lens = EditorGUILayout.FloatField("Fisheye Lens", script._fisheye_lens);

                //public float _latitude_min = -1.5707f;
                //public float _latitude_max = 1.5707f;
                //public float _longitude_min = -3.141592f;
                //public float _longitude_max = 3.141592f;

                script._fov = EditorGUILayout.FloatField("Field of View", script._fov);
                script._fov_pre = EditorGUILayout.FloatField("Field of View Pre", script._fov_pre);
                script._fov_post = EditorGUILayout.FloatField("Field of View Post", script._fov_post);

                script._stereo_eye = EditorGUILayout.Popup("Stereo Eye", script._stereo_eye, script._stereo_eyes);
                script._interocular_distance = EditorGUILayout.FloatField("Interocular Distance", script._interocular_distance);
                script._convergence_distance = EditorGUILayout.FloatField("Convergence Distance", script._convergence_distance);

                script._nearclip = EditorGUILayout.FloatField("Near Clip", script._nearclip);
                script._farclip = EditorGUILayout.FloatField("Far Clip", script._farclip);

                //SOCKET_FLOAT(viewplane.left, "Viewplane Left", 0);
                //SOCKET_FLOAT(viewplane.right, "Viewplane Right", 0);
                //SOCKET_FLOAT(viewplane.bottom, "Viewplane Bottom", 0);
                //SOCKET_FLOAT(viewplane.top, "Viewplane Top", 0);

                //SOCKET_FLOAT(border.left, "Border Left", 0);
                //SOCKET_FLOAT(border.right, "Border Right", 0);
                //SOCKET_FLOAT(border.bottom, "Border Bottom", 0);
                //SOCKET_FLOAT(border.top, "Border Top", 0);

                if (EditorGUI.EndChangeCheck()) needUpdate = true;
                EditorGUILayout.EndVertical();
            }

            background = EditorGUILayout.Foldout(background, "Background");
            if (background)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.BeginChangeCheck();

                script._use_ao = GUILayout.Toggle(script._use_ao, "Use AO");
                script._ao_factor = EditorGUILayout.FloatField("AO Factor", script._ao_factor);
                script._ao_distance = EditorGUILayout.FloatField("AO Distance", script._ao_distance);
                script._use_shader = GUILayout.Toggle(script._use_shader, "Use Shader");

                script._visibility = (BocsCyclesCamera.PathRayFlag)EditorGUILayout.EnumMaskField("Visibility", (System.Enum)script._visibility);

                if (EditorGUI.EndChangeCheck()) needUpdate = true;
                GUILayout.EndVertical();
            }

            film = EditorGUILayout.Foldout(film, "Film");
            if (film)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.BeginChangeCheck();

                script._exposure = EditorGUILayout.FloatField("Exposure", script._exposure);

                script._filter_type = EditorGUILayout.Popup("Filter Type", script._filter_type, script._filter_types);
                script._filter_width = EditorGUILayout.FloatField("Filter Width", script._filter_width);

                script._mist_start = EditorGUILayout.FloatField("Mist Start", script._mist_start);
                script._mist_depth = EditorGUILayout.FloatField("Mist Depth", script._mist_depth);
                script._mist_falloff = EditorGUILayout.FloatField("Mist Falloff", script._mist_falloff);

                script._use_sample_clamp = GUILayout.Toggle(script._use_sample_clamp, "Use Sample Clamp");

                if (EditorGUI.EndChangeCheck()) needUpdate = true;
                GUILayout.EndVertical();
            }

            integrator = EditorGUILayout.Foldout(integrator, "Integrator");
            if (integrator)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.BeginChangeCheck();

                script._min_bounce = EditorGUILayout.IntField("Min Bounce", script._min_bounce);
                script._max_bounce = EditorGUILayout.IntField("Max Bounce", script._max_bounce);

                script._max_diffuse_bounce = EditorGUILayout.IntField("Max Diffuse Bounce", script._max_diffuse_bounce);
                script._max_glossy_bounce = EditorGUILayout.IntField("Max Glossy Bounce", script._max_glossy_bounce);
                script._max_transmission_bounce = EditorGUILayout.IntField("Max Transmission Bounce", script._max_transmission_bounce);
                script._max_volume_bounce = EditorGUILayout.IntField("Max Volume Bounce", script._max_volume_bounce);

                script._transparent_min_bounce = EditorGUILayout.IntField("Min Transparent Bounce", script._transparent_min_bounce);
                script._transparent_max_bounce = EditorGUILayout.IntField("Max Transparent Bounce", script._transparent_max_bounce);

                script._transparent_shadows = GUILayout.Toggle(script._transparent_shadows, "Transparent Shadows");

                script._ao_bounces = EditorGUILayout.IntField("AO Bounces", script._ao_bounces);

                script._volume_max_steps = EditorGUILayout.IntField("Max Volume Steps", script._volume_max_steps);
                script._volume_step_size = EditorGUILayout.FloatField("Volume Step Size", script._volume_step_size);

                script._caustics_reflective = GUILayout.Toggle(script._caustics_reflective, "Reflective Caustics");
                script._caustics_refractive = GUILayout.Toggle(script._caustics_refractive, "Refractive Caustics");

                script._filter_glossy = EditorGUILayout.FloatField("Filter Glossy", script._filter_glossy);
                script._sample_clamp_direct = EditorGUILayout.FloatField("Clamp Direct", script._sample_clamp_direct);
                script._sample_clamp_indirect = EditorGUILayout.FloatField("Clamp Indirect", script._sample_clamp_indirect);

                script._motion_blur = GUILayout.Toggle(script._motion_blur, "Motion Blur");

                script._aa_samples = EditorGUILayout.IntField("AA Samples", script._aa_samples);
                script._diffuse_samples = EditorGUILayout.IntField("Diffuse Samples", script._diffuse_samples);
                script._glossy_samples = EditorGUILayout.IntField("Glossy Samples", script._glossy_samples);
                script._transmission_samples = EditorGUILayout.IntField("Transmission Samples", script._transmission_samples);
                script._ao_samples = EditorGUILayout.IntField("AO Samples", script._ao_samples);
                script._mesh_light_samples = EditorGUILayout.IntField("Mesh Light Samples", script._mesh_light_samples);
                script._subsurface_samples = EditorGUILayout.IntField("Subsurface Samples", script._subsurface_samples);
                script._volume_samples = EditorGUILayout.IntField("Volume Samples", script._volume_samples);

                script._sample_all_lights_direct = GUILayout.Toggle(script._sample_all_lights_direct, "Sample All Lights Direct");
                script._sample_all_lights_indirect = GUILayout.Toggle(script._sample_all_lights_indirect, "Sample All Lights Indirect");
                script._light_sampling_threshold = EditorGUILayout.FloatField("Light Sampling Threshold", script._light_sampling_threshold);

                script._method = EditorGUILayout.Popup("Path Type", script._method, script._methods);
                script._sampling_pattern = EditorGUILayout.Popup("Sampling Pattern", script._sampling_pattern, script._sampling_patterns);

                if (EditorGUI.EndChangeCheck()) needUpdate = true;
                GUILayout.EndVertical();
            }

            //Some Checks...
            if (script._nearclip <= 0) script._nearclip = .001f;

            if (needUpdate)
            {
                BocsCyclesAPI.Cycles_request_settings();
                BocsCyclesAPI.Cycles_request_reset();
            }

            debug = EditorGUILayout.Foldout(debug, "Debug");
            if (debug)
            {
                //Debug.Log(script.GetShaderCount());
                for (int i = 0; i < script.GetGraphCount(); i++)
                {
                    //GUILayout.TextField(script._nodes);
                    GUI.skin.textArea.wordWrap = false;
                    EditorGUILayout.TextArea(script.Nodes[i]);
                }
            }
        }

        private void Init()
        {
            texlogo = Resources.Load("logo") as Texture;
            texSky = Resources.Load("ebSky") as Texture;
            texSoftlight = Resources.Load("ebSoftlight") as Texture;
            texHDR = Resources.Load("ebHDR") as Texture;
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
    }
}