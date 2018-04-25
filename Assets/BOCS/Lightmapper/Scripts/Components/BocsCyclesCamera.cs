namespace BOCSLIGHTMAPPER
{
    using System;
    using UnityEngine;

    public class BocsCyclesCamera : BocsCyclesGraphBase
    {
        //Film
        public float _exposure = 2.2f;

        public string[] _filter_types = new string[] { "box", "gaussian", "blackman_harris" };
        public int _filter_type = 2;//filter_type
        public float _filter_width = 1;

        public float _mist_start = 0;
        public float _mist_depth = 100;
        public float _mist_falloff = 1;

        public bool _use_sample_clamp = false;

        //Background
        public bool _use_ao = true;

        public float _ao_factor = .025f;
        public float _ao_distance = 10;

        public bool _use_shader = true;
        public bool _transparent = true;

        [Flags]
        public enum PathRayFlag
        {
            PATH_RAY_CAMERA = 1,
            PATH_RAY_REFLECT = 2,
            PATH_RAY_TRANSMIT = 4,
            PATH_RAY_DIFFUSE = 8,
            PATH_RAY_GLOSSY = 16,
            PATH_RAY_SINGULAR = 32,
            PATH_RAY_TRANSPARENT = 64,

            PATH_RAY_SHADOW_OPAQUE = 128,
            PATH_RAY_SHADOW_TRANSPARENT = 256,

            PATH_RAY_CURVE = 512, /* visibility flag to define curve segments */
            PATH_RAY_VOLUME_SCATTER = 1024 /* volume scattering */
        }

        public PathRayFlag _visibility = PathRayFlag.PATH_RAY_CAMERA | PathRayFlag.PATH_RAY_REFLECT | PathRayFlag.PATH_RAY_TRANSMIT | PathRayFlag.PATH_RAY_DIFFUSE | PathRayFlag.PATH_RAY_DIFFUSE | PathRayFlag.PATH_RAY_GLOSSY | PathRayFlag.PATH_RAY_SINGULAR | PathRayFlag.PATH_RAY_TRANSPARENT | PathRayFlag.PATH_RAY_SHADOW_OPAQUE | PathRayFlag.PATH_RAY_SHADOW_TRANSPARENT | PathRayFlag.PATH_RAY_CURVE | PathRayFlag.PATH_RAY_VOLUME_SCATTER;

        //Integrator
        public int _min_bounce = 3;

        public int _max_bounce = 10;

        public int _max_diffuse_bounce = 4;
        public int _max_glossy_bounce = 4;
        public int _max_transmission_bounce = 10;
        public int _max_volume_bounce = 4;

        public int _transparent_min_bounce = 5;
        public int _transparent_max_bounce = 5;
        public bool _transparent_shadows = true;

        public int _ao_bounces = 3;

        public int _volume_max_steps = 1024;
        public float _volume_step_size = .1f;

        public bool _caustics_reflective = true;
        public bool _caustics_refractive = true;

        public float _filter_glossy = .35f;
        public float _sample_clamp_direct = 2;
        public float _sample_clamp_indirect = 2;

        public bool _motion_blur = false;

        public int _aa_samples = 0;
        public int _diffuse_samples = 1;
        public int _glossy_samples = 1;
        public int _transmission_samples = 1;
        public int _ao_samples = 1;
        public int _mesh_light_samples = 1;
        public int _subsurface_samples = 1;
        public int _volume_samples = 1;

        public bool _sample_all_lights_direct = true;
        public bool _sample_all_lights_indirect = true;
        public float _light_sampling_threshold = 0.05f;

        public string[] _methods = new string[] { "path", "branched_path" };
        public int _method = 0;

        public string[] _sampling_patterns = new string[] { "sobol", "cmj" };
        public int _sampling_pattern = 0;

        //Camera

        public float _shuttertime = 0;

        public string[] _motion_positions = new string[] { "start", "center", "end" };
        public int _motion_position = 1;

        public string[] _rolling_shutter_types = new string[] { "none", "top" };
        public int _rolling_shutter_type = 0;
        public float _rolling_shutter_duration = .1f;
        //SOCKET_FLOAT_ARRAY(shutter_curve, "Shutter Curve", array<float>());

        public float _aperturesize = 0;
        public float _focaldistance = 10;

        public int _blades = 5;
        public float _bladesrotation = 0;

        public float _aperture_ratio = 1;

        public string[] _types = new string[] { "perspective", "orthograph", "panorama" };
        public int _type = 0;

        public string[] _panorama_types = new string[] { "equirectangular", "mirrorball", "fisheye_equidistant", "fisheye_equisolid" };
        public int _panorama_type = 0;

        public float _fisheye_fov = 3.141592f;
        public float _fisheye_lens = 10.5f;
        public float _latitude_min = -1.5707f;
        public float _latitude_max = 1.5707f;
        public float _longitude_min = -3.141592f;
        public float _longitude_max = 3.141592f;

        public float _fov = 60;
        public float _fov_pre = 0.7853f;
        public float _fov_post = 0.7853f;

        public string[] _stereo_eyes = new string[] { "none", "left", "right" };
        public int _stereo_eye = 0;

        public float _interocular_distance = 0.065f;
        public float _convergence_distance = 30.0f * 0.065f;

        //SOCKET_BOOLEAN(use_pole_merge, "Use Pole Merge", false);
        //SOCKET_FLOAT(pole_merge_angle_from, "Pole Merge Angle From",  60.0f * M_PI_F / 180.0f);
        //SOCKET_FLOAT(pole_merge_angle_to, "Pole Merge Angle To", 75.0f * M_PI_F / 180.0f);

        //SOCKET_FLOAT(sensorwidth, "Sensor Width", 0.036f);
        //SOCKET_FLOAT(sensorheight, "Sensor Height", 0.024f);

        public float _nearclip = .001f;
        public float _farclip = 1000000;

        //SOCKET_FLOAT(viewplane.left, "Viewplane Left", 0);
        //SOCKET_FLOAT(viewplane.right, "Viewplane Right", 0);
        //SOCKET_FLOAT(viewplane.bottom, "Viewplane Bottom", 0);
        //SOCKET_FLOAT(viewplane.top, "Viewplane Top", 0);

        //SOCKET_FLOAT(border.left, "Border Left", 0);
        //SOCKET_FLOAT(border.right, "Border Right", 0);
        //SOCKET_FLOAT(border.bottom, "Border Bottom", 0);
        //SOCKET_FLOAT(border.top, "Border Top", 0);

        public override string GetGraphName(int i)
        {
            return "Background";
        }

        // Reset to default values.
        protected void Reset()
        {
            Camera c = GetComponent<Camera>();
            _fov = c.fieldOfView;

            _type = 0;
            if (c.orthographic) _type = 1;

            //_nearClip = c.nearClipPlane;
            _nearclip = .001f;//default unity is too far away...
            _farclip = c.farClipPlane;

            Nodes[0] = "node|t=BocsNodeOutput,x=940,y=60,c=0:node|t=BocsNodeBackground,x=530,y=40,c=0:node|t=BocsNodeSkyTexture,x=42,y=30,c=0:node|t=BocsNodeEnviromentTexture,x=80,y=200,c=0:val|n=1,s=color,v=FFFFFFFF:val|n=1,s=strength,v=0.75:val|n=2,s=sun_direction,v=0 0 1:val|n=2,s=turbidity,v=2.2:val|n=2,s=ground_albedo,v=0.3:val|n=3,s=filename,v=:val|n=3,s=color_space,v=1:val|n=3,s=use_alpha,v=True:val|n=3,s=interpolation,v=1:val|n=3,s=projection,v=0:connect|n1=1,n2=0,s1=background,s2=surface,:connect|n1=2,n2=1,s1=color,s2=color,:";

            BocsCyclesAPI.Cycles_request_settings();
            BocsCyclesAPI.Cycles_request_reset();
        }
    }
}