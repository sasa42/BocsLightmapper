// _____ _____ _____ _____    __    _     _   _
//| __  |     |     |   __|  |  |  |_|___| |_| |_ _____ ___ ___ ___ ___ ___
//| __ -|  |  |   --|__   |  |  |__| | . |   |  _|     | .'| . | . | -_|  _|
//|_____|_____|_____|_____|  |_____|_|_  |_|_|_| |_|_|_|__,|  _|  _|___|_|
//                                   |___|                 |_| |_|

namespace BOCSLIGHTMAPPER
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using UnityEngine;

    public class BocsCyclesAPI
    {
        //Unity Native
        [DllImport("cycles")]
        public static extern IntPtr GetRenderEventFunc();

        //Global Control
        [DllImport("cycles")]
        public static extern void Cycles_reset();

        [DllImport("cycles")]
        public static extern void Cycles_set_active();

        [DllImport("cycles")]
        public static extern void Cycles_set_inactive();

        //Render Control
        [DllImport("cycles")]
        public static extern void Cycles_render();

        [DllImport("cycles")]
        public static extern void Cycles_render_async();

        [DllImport("cycles")]
        public static extern void Cycles_pause();

        [DllImport("cycles")]
        public static extern void Cycles_resume();

        //Updating Functions
        [DllImport("cycles")]
        public static extern void Cycles_xml([MarshalAs(UnmanagedType.LPStr)] string xml);

        [DllImport("cycles", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cycles_set_meshdata(int vertexCount, IntPtr sourceVertices, IntPtr sourceNormals, IntPtr sourceUVs);

        [DllImport("cycles", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Cycles_set_submeshdata(int indexCount, IntPtr sourceIndexes);

        [DllImport("cycles")]
        public static extern void Cycles_set_texture(System.IntPtr texture, int w, int h);

        //Status Functions
        [DllImport("cycles")]
        public static extern int Cycles_progress();

        [DllImport("cycles")]
        public static extern bool Cycles_image_ready();

        [DllImport("cycles")]
        public static extern bool Cycles_is_active();

        [DllImport("cycles")]
        public static extern bool Cycles_need_reset();

        [DllImport("cycles")]
        public static extern bool Cycles_need_settings();

        //Requests
        [DllImport("cycles")]
        public static extern void Cycles_request_reset();

        [DllImport("cycles")]
        public static extern void Cycles_request_settings();

        //Other
        [DllImport("cycles")]
        public static extern void Cycles_debug();

        public static void UpdateCameraPositon(Camera camera, BocsCyclesCamera cameraSettings, int width, int height)
        {
            if (camera == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            xml.WriteStartElement("transform");

            Vector3 angles = camera.transform.eulerAngles;
            angles.x += 90;

            xml.WriteAttributeString("translate", _positionToString(camera.transform.position));
            xml.WriteAttributeString("scale", _scaleToString(camera.transform.localScale));
            xml.WriteAttributeString("euler", _eulerToString(angles));

            xml.WriteStartElement("camera");

            xml.WriteAttributeString("width", width.ToString());
            xml.WriteAttributeString("height", height.ToString());
            xml.WriteAttributeString("fov", (cameraSettings._fov * Mathf.Deg2Rad).ToString());

            xml.WriteEndElement();

            xml.WriteEndElement();//transform

            Cycles_xml(_xmlEnd(xml, ms));
        }

        public static void UpdateSettings(Camera camera, BocsCyclesCamera cameraSettings, int width, int height, int samples)
        {
            if (camera == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            _addSamples(xml, samples);
            _addCamera(xml, camera, cameraSettings, width, height);
            _addFilm(xml, cameraSettings);
            _addIntegrator(xml, cameraSettings);
            _addBackground(xml, cameraSettings);

            Cycles_xml(_xmlEnd(xml, ms));
        }

        public static void UpdateMaterial(GameObject o)
        {
            BocsCyclesMaterial bcm = o.GetComponent<BocsCyclesMaterial>();
            if (bcm == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            string objName = "mesh_" + o.GetInstanceID().ToString();
            _addShaderGraph(xml, bcm, objName);

            Cycles_xml(_xmlEnd(xml, ms));
        }

        //public static void AddAllLights()
        //{
        //    BocsCyclesLight[] lights = GameObject.FindObjectsOfType<BocsCyclesLight>();
        //    for (int o = 0; o < lights.Length; o++)
        //    {
        //        if (lights[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
        //        AddLight(lights[o].gameObject);
        //    }
        //}

        //public static void AddAllMeshes()
        //{
        //    BocsCyclesMaterial[] meshs = GameObject.FindObjectsOfType<BocsCyclesMaterial>();
        //    for (int o = 0; o < meshs.Length; o++)
        //    {
        //        if (meshs[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
        //        AddMesh(meshs[o].gameObject);
        //    }
        //}

        public static void AddLight(GameObject o)
        {
            BocsCyclesLight bcl = o.GetComponent<BocsCyclesLight>();
            if (bcl == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            _addLight(xml, o);

            Cycles_xml(_xmlEnd(xml, ms));
        }

        public static void ObjectAdd(GameObject o)
        {
            ObjectDelete(o);

            BocsCyclesMaterial bcm = o.GetComponent<BocsCyclesMaterial>();
            BocsCyclesLight bcl = o.GetComponent<BocsCyclesLight>();

            if (bcm == null && bcl == null) return;

            if (bcl) AddLight(o);
            if (bcm) AddMesh(o);
        }

        public static void ObjectDelete(GameObject o)
        {
            BocsCyclesMaterial bcm = o.GetComponent<BocsCyclesMaterial>();
            BocsCyclesLight bcl = o.GetComponent<BocsCyclesLight>();

            if (bcm == null && bcl == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            string objName;

            if (bcm)
            {
                objName = "mesh_" + o.GetInstanceID().ToString();

                for (int i = 0; i < bcm.GetGraphCount(); i++)
                {
                    xml.WriteStartElement("delete");
                    xml.WriteAttributeString("name", objName + i);
                    xml.WriteEndElement();
                }
            }

            if (bcl)
            {
                objName = "light_" + o.GetInstanceID().ToString();

                xml.WriteStartElement("delete");
                xml.WriteAttributeString("name", objName);
                xml.WriteEndElement();
            }

            Cycles_xml(_xmlEnd(xml, ms));
        }

        public static void UpdateObject(GameObject o)
        {
            BocsCyclesLight bcl = o.GetComponent<BocsCyclesLight>();
            BocsCyclesMaterial bcm = o.GetComponent<BocsCyclesMaterial>();

            if (bcl == null && bcm == null) return;

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            if (bcl) _addLight(xml, o);

            if (bcm)
            {
                string objName = "mesh_" + o.GetInstanceID().ToString();
                _transform(xml, o);

                xml.WriteStartElement("state");
                //xml.WriteAttributeString("interpolation",bcm._smooth ? "smooth" : "flat");
                xml.WriteAttributeString("visibility", ((int)bcm.Visibility).ToString());

                for (int i = 0; i < bcm.GetGraphCount(); i++)
                {
                    xml.WriteStartElement("object");
                    xml.WriteAttributeString("name", objName + i);
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();//state
                xml.WriteEndElement();//Transform
            }

            Cycles_xml(_xmlEnd(xml, ms));
        }

        public static void AddMesh(GameObject o)
        {
            BocsCyclesMaterial bcm = o.GetComponent<BocsCyclesMaterial>();
            if (bcm == null) return;

            Mesh m = bcm.Mesh;
            if (m == null) return;

            string objName = "mesh_" + o.GetInstanceID().ToString();

            _sendShaderGraph(objName, bcm);

            GCHandle gcVertices = GCHandle.Alloc(m.vertices, GCHandleType.Pinned);
            GCHandle gcNormals = GCHandle.Alloc(m.normals, GCHandleType.Pinned);
            GCHandle gcUV = GCHandle.Alloc(m.uv, GCHandleType.Pinned);

            Cycles_set_meshdata(m.vertices.Length, gcVertices.AddrOfPinnedObject(), gcNormals.AddrOfPinnedObject(), gcUV.AddrOfPinnedObject());

            gcVertices.Free();
            gcNormals.Free();
            gcUV.Free();

            for (int sm = 0; sm < m.subMeshCount; sm++)
            {
                int[] idx = m.GetTriangles(sm);
                GCHandle gcIndexes = GCHandle.Alloc(idx, GCHandleType.Pinned);
                Cycles_set_submeshdata(idx.Length, gcIndexes.AddrOfPinnedObject());
                gcIndexes.Free();

                MemoryStream ms = new MemoryStream();
                XmlTextWriter xml = _xmlStart(ms);

                _transform(xml, o);

                xml.WriteStartElement("state");
                xml.WriteAttributeString("interpolation", "smooth");
                xml.WriteAttributeString("shader", objName + sm);
                xml.WriteAttributeString("visibility", ((int)bcm.Visibility).ToString());

                xml.WriteStartElement("submesh");
                xml.WriteAttributeString("name", objName + sm);

                xml.WriteEndElement();

                xml.WriteEndElement();//state

                xml.WriteEndElement();//transform

                Cycles_xml(_xmlEnd(xml, ms));
            }
        }

        private static void _addSamples(XmlTextWriter xml, int samples)
        {
            xml.WriteStartElement("state");
            xml.WriteAttributeString("samples", samples.ToString());
            xml.WriteEndElement();//state
        }

        private static void _addCamera(XmlTextWriter xml, Camera camera, BocsCyclesCamera cameraSettings, int width, int height)
        {
            if (camera == null) return;

            xml.WriteStartElement("transform");

            Vector3 angles = camera.transform.eulerAngles;
            angles.x += 90;

            xml.WriteAttributeString("translate", _positionToString(camera.transform.position));
            xml.WriteAttributeString("scale", _scaleToString(camera.transform.localScale));
            xml.WriteAttributeString("euler", _eulerToString(angles));

            xml.WriteStartElement("camera");

            xml.WriteAttributeString("width", width.ToString());
            xml.WriteAttributeString("height", height.ToString());

            _addCameraOptions(xml, camera, cameraSettings);

            xml.WriteEndElement();

            xml.WriteEndElement();//transform
        }

        private static void _addCameraOptions(XmlTextWriter xml, Camera camera, BocsCyclesCamera cameraSettings)
        {
            xml.WriteAttributeString("shuttertime", cameraSettings._shuttertime.ToString());

            xml.WriteAttributeString("motion_position", cameraSettings._motion_positions[cameraSettings._motion_position]);

            xml.WriteAttributeString("rolling_shutter_type", cameraSettings._rolling_shutter_types[cameraSettings._rolling_shutter_type]);

            xml.WriteAttributeString("rolling_shutter_duration", cameraSettings._rolling_shutter_duration.ToString());

            xml.WriteAttributeString("aperturesize", cameraSettings._aperturesize.ToString());

            xml.WriteAttributeString("blades", cameraSettings._blades.ToString());
            xml.WriteAttributeString("bladesrotation", cameraSettings._bladesrotation.ToString());

            xml.WriteAttributeString("aperture_ratio", cameraSettings._aperture_ratio.ToString());

            xml.WriteAttributeString("type", cameraSettings._types[cameraSettings._type]);

            xml.WriteAttributeString("panorama_type", cameraSettings._panorama_types[cameraSettings._panorama_type]);

            xml.WriteAttributeString("fisheye_fov", cameraSettings._fisheye_fov.ToString());
            xml.WriteAttributeString("fisheye_lens", cameraSettings._fisheye_lens.ToString());

            //public float _latitude_min = -1.5707f;
            //public float _latitude_max = 1.5707f;
            //public float _longitude_min = -3.141592f;
            //public float _longitude_max = 3.141592f;

            xml.WriteAttributeString("fov", (cameraSettings._fov * Mathf.Deg2Rad).ToString());
            //public float _fov_pre = 0.7853f;
            //public float _fov_post = 0.7853f;

            xml.WriteAttributeString("stereo_eye", cameraSettings._stereo_eyes[cameraSettings._stereo_eye]);

            xml.WriteAttributeString("interocular_distance", cameraSettings._interocular_distance.ToString());
            xml.WriteAttributeString("convergence_distance", cameraSettings._convergence_distance.ToString());

            //SOCKET_BOOLEAN(use_pole_merge, "Use Pole Merge", false);
            //SOCKET_FLOAT(pole_merge_angle_from, "Pole Merge Angle From",  60.0f * M_PI_F / 180.0f);
            //SOCKET_FLOAT(pole_merge_angle_to, "Pole Merge Angle To", 75.0f * M_PI_F / 180.0f);
            //SOCKET_FLOAT(sensorwidth, "Sensor Width", 0.036f);
            //SOCKET_FLOAT(sensorheight, "Sensor Height", 0.024f);

            xml.WriteAttributeString("nearclip", cameraSettings._nearclip.ToString());
            xml.WriteAttributeString("farclip", cameraSettings._farclip.ToString());

            //SOCKET_FLOAT(viewplane.left, "Viewplane Left", 0);
            //SOCKET_FLOAT(viewplane.right, "Viewplane Right", 0);
            //SOCKET_FLOAT(viewplane.bottom, "Viewplane Bottom", 0);
            //SOCKET_FLOAT(viewplane.top, "Viewplane Top", 0);

            //SOCKET_FLOAT(border.left, "Border Left", 0);
            //SOCKET_FLOAT(border.right, "Border Right", 0);
            //SOCKET_FLOAT(border.bottom, "Border Bottom", 0);
            //SOCKET_FLOAT(border.top, "Border Top", 0);
        }

        private static void _addFilm(XmlTextWriter xml, BocsCyclesCamera cameraSettings)
        {
            if (cameraSettings == null) return;

            //Update Film Settings...
            xml.WriteStartElement("film");

            xml.WriteAttributeString("exposure", cameraSettings._exposure.ToString());

            xml.WriteAttributeString("filter_type", cameraSettings._filter_types[cameraSettings._filter_type]);
            xml.WriteAttributeString("filter_width", cameraSettings._filter_width.ToString());

            xml.WriteAttributeString("mist_start", cameraSettings._mist_start.ToString());
            xml.WriteAttributeString("mist_depth", cameraSettings._mist_depth.ToString());
            xml.WriteAttributeString("mist_falloff", cameraSettings._mist_falloff.ToString());

            xml.WriteAttributeString("use_sample_clamp", cameraSettings._use_sample_clamp.ToString());

            xml.WriteEndElement();
        }

        private static void _addIntegrator(XmlTextWriter xml, BocsCyclesCamera cameraSettings)
        {
            if (cameraSettings == null) return;

            //Update Integrator Settings...
            xml.WriteStartElement("integrator");

            xml.WriteAttributeString("min_bounce", cameraSettings._min_bounce.ToString());
            xml.WriteAttributeString("max_bounce", cameraSettings._max_bounce.ToString());

            xml.WriteAttributeString("max_diffuse_bounce", cameraSettings._max_diffuse_bounce.ToString());
            xml.WriteAttributeString("max_glossy_bounce", cameraSettings._max_diffuse_bounce.ToString());
            xml.WriteAttributeString("max_transmission_bounce", cameraSettings._max_transmission_bounce.ToString());
            xml.WriteAttributeString("max_volume_bounce", cameraSettings._max_volume_bounce.ToString());

            xml.WriteAttributeString("transparent_min_bounce", cameraSettings._transparent_min_bounce.ToString());
            xml.WriteAttributeString("transparent_max_bounce", cameraSettings._transparent_max_bounce.ToString());
            xml.WriteAttributeString("transparent_shadows", cameraSettings._transparent_shadows.ToString());

            xml.WriteAttributeString("volume_max_steps", cameraSettings._volume_max_steps.ToString());
            xml.WriteAttributeString("volume_step_size", cameraSettings._volume_step_size.ToString());

            xml.WriteAttributeString("caustics_reflective", cameraSettings._caustics_reflective.ToString());
            xml.WriteAttributeString("caustics_refractive", cameraSettings._caustics_refractive.ToString());

            xml.WriteAttributeString("filter_glossy", cameraSettings._filter_glossy.ToString());
            xml.WriteAttributeString("sample_clamp_direct", cameraSettings._sample_clamp_direct.ToString());
            xml.WriteAttributeString("sample_clamp_indirect", cameraSettings._sample_clamp_indirect.ToString());

            xml.WriteAttributeString("motion_blur", cameraSettings._motion_blur.ToString());

            xml.WriteAttributeString("aa_samples", cameraSettings._aa_samples.ToString());
            xml.WriteAttributeString("diffuse_samples", cameraSettings._diffuse_samples.ToString());
            xml.WriteAttributeString("glossy_samples", cameraSettings._glossy_samples.ToString());
            xml.WriteAttributeString("transmission_samples", cameraSettings._transmission_samples.ToString());
            xml.WriteAttributeString("ao_samples", cameraSettings._ao_samples.ToString());
            xml.WriteAttributeString("mesh_light_samples", cameraSettings._mesh_light_samples.ToString());
            xml.WriteAttributeString("subsurface_samples", cameraSettings._subsurface_samples.ToString());
            xml.WriteAttributeString("volume_samples", cameraSettings._volume_samples.ToString());
            xml.WriteAttributeString("motion_blur", cameraSettings._motion_blur.ToString());

            xml.WriteAttributeString("sample_all_lights_direct", cameraSettings._sample_all_lights_direct.ToString());
            xml.WriteAttributeString("sample_all_lights_indirect", cameraSettings._sample_all_lights_indirect.ToString());

            xml.WriteAttributeString("method", cameraSettings._methods[cameraSettings._method]);
            xml.WriteAttributeString("sampling_pattern", cameraSettings._sampling_patterns[cameraSettings._sampling_pattern]);

            xml.WriteAttributeString("ao_bounces", cameraSettings._ao_bounces.ToString());
            xml.WriteAttributeString("light_sampling_threshold", cameraSettings._light_sampling_threshold.ToString());

            xml.WriteEndElement();
        }

        private static void _addBackground(XmlTextWriter xml, BocsCyclesCamera cameraSettings)
        {
            if (cameraSettings == null) return;

            //Set Background
            xml.WriteStartElement("background");

            xml.WriteAttributeString("use_ao", cameraSettings._use_ao.ToString());
            xml.WriteAttributeString("ao_factor", cameraSettings._ao_factor.ToString());
            xml.WriteAttributeString("ao_distance", cameraSettings._ao_distance.ToString());

            xml.WriteAttributeString("use_shader", cameraSettings._use_shader.ToString());
            xml.WriteAttributeString("transparent", cameraSettings._transparent.ToString());

            xml.WriteAttributeString("visibility", ((int)cameraSettings._visibility).ToString());

            _addShaderGraph(xml, cameraSettings, string.Empty);

            xml.WriteEndElement();
        }

        private static void _addLight(XmlTextWriter xml, GameObject o)
        {
            BocsCyclesLight bcl = o.GetComponent<BocsCyclesLight>();
            if (bcl == null) return;
            //Light l = o.GetComponent<Light>();
            //if (l == null) return;

            string objName = "light_" + o.GetInstanceID().ToString();
            _addShaderGraph(xml, bcl, objName);

            //BEGIN_transform(xml,o);

            xml.WriteStartElement("state");
            //xml.WriteAttributeString("interpolation","smooth");
            xml.WriteAttributeString("shader", objName + "0");

            xml.WriteStartElement("light");

            xml.WriteAttributeString("name", objName);

            //string lt = "point";
            //if (l.type == LightType.Point) lt		= "point";
            //if (l.type == LightType.Directional) lt = "distant";
            //if (l.type == LightType.Area) lt		= "area";
            //if (l.type == LightType.Spot) lt		= "spot";

            //xml.WriteAttributeString("is_enabled",bcl._enabled.ToString());

            xml.WriteAttributeString("type", bcl.Type[bcl.TypeSelected]);

            xml.WriteAttributeString("size", bcl.Size.ToString());
            xml.WriteAttributeString("dir", _positionToString(o.transform.forward));
            xml.WriteAttributeString("co", _positionToString(o.transform.position));

            xml.WriteAttributeString("max_bounces", bcl.MaxBounces.ToString());

            xml.WriteAttributeString("use_mis", bcl.UseMis.ToString());

            xml.WriteAttributeString("use_diffuse", bcl.Diffuse.ToString());
            xml.WriteAttributeString("use_glossy", bcl.Glossy.ToString());
            xml.WriteAttributeString("use_transmission", bcl.Transmission.ToString());
            xml.WriteAttributeString("use_scatter", bcl.Scatter.ToString());

            xml.WriteAttributeString("cast_shadow", bcl.Shadow.ToString());

            xml.WriteAttributeString("spot_angle", (bcl.SpotAngle * Mathf.Deg2Rad).ToString());

            xml.WriteAttributeString("is_portal", bcl.IsPortal.ToString());

            xml.WriteEndElement();//state
        }

        private static void _addShaderGraph(XmlTextWriter xml, BocsCyclesGraphBase graph, string name)
        {
            string saved = BocsCyclesNodeManager.SaveGraph();

            for (int graphIndex = 0; graphIndex < graph.GetGraphCount(); graphIndex++)
            {
                BocsCyclesNodeManager.LoadGraph(graph.GetGraph(graphIndex));

                if (name != string.Empty)
                {
                    xml.WriteStartElement("shader");
                    xml.WriteAttributeString("name", name + graphIndex);
                }

                for (int nid = 0; nid < BocsCyclesNodeManager.Nodes.Count; nid++)
                {
                    BocsNodeBase n = BocsCyclesNodeManager.Nodes[nid];

                    if (n.NodeName == "output") continue;//shaders have one by default

                    xml.WriteStartElement(n.NodeName);
                    xml.WriteAttributeString("name", n.NodeName + nid);
                    for (int sid = 0; sid < n.Slots.Count; sid++)
                    {
                        BocsSlotBase slot = n.Slots[sid];
                        string val = slot.GetXML();
                        if (val != string.Empty) xml.WriteAttributeString(slot.SlotName, val);
                    }
                    xml.WriteEndElement();
                }
                for (int nid = 0; nid < BocsCyclesNodeManager.Nodes.Count; nid++)
                {
                    BocsNodeBase n = BocsCyclesNodeManager.Nodes[nid];
                    for (int sid = 0; sid < n.Slots.Count; sid++)
                    {
                        BocsSlotBase slot = n.Slots[sid];
                        foreach (BocsSlotBase c in slot.OutputSlots)
                        {
                            int toID = BocsCyclesNodeManager.FindNodeFromSlot(c);
                            BocsNodeBase toNode = BocsCyclesNodeManager.Nodes[toID];
                            string toConnect = "output";
                            if (toNode.NodeName != "output") toConnect = toNode.NodeName + toID;

                            xml.WriteStartElement("connect");
                            xml.WriteAttributeString("from", n.NodeName + nid + " " + slot.SlotName);
                            xml.WriteAttributeString("to", toConnect + " " + c.SlotName);
                            xml.WriteEndElement();
                        }
                    }
                }

                if (name != string.Empty) xml.WriteEndElement();
            }

            BocsCyclesNodeManager.LoadGraph(saved);
        }

        private static void _transform(XmlTextWriter xml, GameObject o)
        {
            xml.WriteStartElement("transform");
            xml.WriteAttributeString("translate", _positionToString(o.transform.position));
            xml.WriteAttributeString("scale", _scaleToString(o.transform.lossyScale));
            Vector3 angles = o.transform.eulerAngles;
            angles.z = -angles.z;
            xml.WriteAttributeString("euler", _eulerToString(angles));
        }

        private static XmlTextWriter _xmlStart(MemoryStream ms)
        {
            XmlTextWriter xml = new XmlTextWriter(ms, System.Text.Encoding.ASCII);
            xml.Formatting = Formatting.Indented;
            xml.WriteStartElement("cycles");
            return xml;
        }

        private static string _xmlEnd(XmlTextWriter xml, MemoryStream ms)
        {
            xml.WriteEndElement();//cycles
            xml.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        private static void _sendShaderGraph(string objName, BocsCyclesMaterial bcm)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = _xmlStart(ms);

            _addShaderGraph(xml, bcm, objName);

            Cycles_xml(_xmlEnd(xml, ms));
        }

        private static string _positionToString(Vector3 v)
        {
            return v.x.ToString() + " " + (-v.z).ToString() + " " + v.y.ToString();
        }

        private static string _normalToString(Vector3 v)
        {
            return v.x.ToString() + " " + (-v.z).ToString() + " " + v.y.ToString();
        }

        private static string _uvToString(Vector2 v)
        {
            return v.x.ToString() + " " + v.y.ToString();
        }

        private static string _scaleToString(Vector3 v)
        {
            return v.x.ToString() + " " + v.z.ToString() + " " + v.y.ToString();
        }

        private static string _eulerToString(Vector3 v)
        {
            return v.x.ToString() + " " + v.z.ToString() + " " + v.y.ToString();
        }
    }
}