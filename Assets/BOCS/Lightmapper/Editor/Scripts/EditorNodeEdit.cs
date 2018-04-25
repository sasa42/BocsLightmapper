namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    public static class RectExtensions
    {
        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;

            //"translate" the top left to something like an origin
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;

            //Scale the rect
            result.xMin *= scale;
            result.yMin *= scale;
            result.xMax *= scale;
            result.yMax *= scale;

            //"translate" the top left back to its original position
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;

            return result;
        }
    }

    public class EditorZoomArea
    {
        private static Matrix4x4 prevMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup(); //End the group that Unity began so we're not bound by the EditorWindow

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.center);
            clippedArea.y += 21; //Account for the window tab

            GUI.BeginGroup(clippedArea);

            prevMatrix = GUI.matrix;

            //Perform scaling
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.center, Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            GUI.matrix = translation * scale * translation.inverse;

            return clippedArea;
        }

        public static void End()
        {
            GUI.matrix = prevMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0, 21, Screen.width, Screen.height));
        }
    }

    public class EditorNodeEdit : EditorWindow
    {
        private static Texture2D tex;

        //[MenuItem("Node/Editor")]
        //static public void ShowEditor()
        //{
        //	EditorWindow.GetWindow<BocsNodeEditor>();
        //}

        private static BocsCyclesGraphBase bcg = null;
        private static int selectedGraph = 0;

        //private static GUISkin skin;
        private static Texture2D grid;
        private static Texture2D logo;

        private static Vector2 lastMouse;
        private static string paste = string.Empty;
        private GraphType graphType = GraphType.None;
        private BocsSlotBase dragSlot = null;
        private bool leftMouseDown = false;

        //private bool bRightMouseDown = false;
        private bool middleMouseDown = false;

        private Vector2 scrollAmount = Vector2.zero;
        private float zoomScale = 1.0f;
        //private Vector2 scrollPosition = new Vector2(0, 0);

        private bool cyclesNeedsUpdate = false;

        private enum GraphType
        {
            None, Material, Light, World
        }

        public void ReloadChange()
        {
            selectedGraph = 0;
            bcg = null;

            BocsCyclesNodeManager.Reset();

            if (Selection.activeGameObject != null)
            {
                bcg = Selection.activeGameObject.GetComponent<BocsCyclesMaterial>();
                if (bcg != null)
                {
                    graphType = GraphType.Material;
                    LoadNodes();
                    return;
                }

                bcg = Selection.activeGameObject.GetComponent<BocsCyclesLight>();
                if (bcg != null)
                {
                    graphType = GraphType.Light;
                    LoadNodes();
                    return;
                }

                bcg = Selection.activeGameObject.GetComponent<BocsCyclesCamera>();
                if (bcg != null)
                {
                    graphType = GraphType.World;
                    LoadNodes();
                    return;
                }
            }
        }

        public void DrawNodeCurve(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + (start.width / 2), start.y + (start.height / 2), 0);
            Vector3 endPos = new Vector3(end.x + (end.width / 2), end.y + (end.height / 2), 0);
            Vector3 startTan = startPos + (Vector3.right * 50);
            Vector3 endTan = endPos + (Vector3.left * 50);
            //Color shadowCol = new Color(0, 0, 0, 0.06f);
            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 3);
        }

        protected void OnEnable()
        {
	        //skin = (GUISkin)EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
            grid = (Texture2D)Resources.Load("grid");
            logo = (Texture2D)Resources.Load("BlenderLogo");

            //this.title = "Editor";
            if (EditorGUIUtility.isProSkin)
                this.titleContent = new GUIContent("Editor", Resources.Load<Texture>("cyclesEditIcon_d"));
            else
                this.titleContent = new GUIContent("Editor", Resources.Load<Texture>("cyclesEditIcon"));

            tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            tex.SetPixel(0, 0, new Color(0.25f, 0.4f, 0.25f));
            tex.Apply();

            OnSelectionChange();
        }

        protected void Update()
        {
            Repaint();
        }

        protected void OnSelectionChange()
        {
            SaveNodes();
            ReloadChange();
        }

        protected void OnDestroy()
        {
            SaveNodes();
            bcg = null;
            selectedGraph = 0;
            graphType = GraphType.None;
        }

        protected void OnGUI()
        {
            if (bcg == null)
            {
                if (grid) GUI.DrawTextureWithTexCoords(new Rect(0, 0, 10000, 10000), grid, new Rect(0, 0, 10000 / grid.width, 10000 / grid.height));
                GUI.DrawTexture(new Rect(position.width - 128, position.height - 128, 128, 128), logo, ScaleMode.ScaleToFit, true);
                return;
            }

            //GUI.skin = skin;

            UpdateEditorInput();
            DrawGUIShaderSelect();
            BocsCyclesNodeManager.ClearSlots();

            EditorGUI.BeginChangeCheck();

            Rect lr = GUILayoutUtility.GetLastRect();
            EditorZoomArea.Begin(zoomScale, new Rect(0, lr.yMax + 2, position.width, position.height - lr.yMax - 2));

            if (grid)
            {
                grid.wrapMode = TextureWrapMode.Repeat;
                GUI.DrawTextureWithTexCoords(new Rect(0, 0, 10000, 10000), grid, new Rect(0, 0, 10000 / grid.width, 10000 / grid.height));
            }

            //GUI.DrawTextureWithTexCoords(lr,_grid,new Rect(0,0,1,1),false);
            //GUI.DrawTextureWithTexCoords(new Rect(0,0,256,256),_grid,new Rect(0,0,1,1),false);

            DrawGUINodes();
            DrawGUISockets();
            DrawGUIConnections();

            DrawGUIConnectDrag();

            GUIScroll();
            GUIZoom();

            EditorZoomArea.End();

            GUIMenu();
            GUIKeys();

            if (EditorGUI.EndChangeCheck())
            {
                SaveNodes();
                //Debug.Log("GUI Change");
                cyclesNeedsUpdate = true;
            }

            UpdateCycles();
        }

        private void UpdateEditorInput()
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0) leftMouseDown = true;
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0) leftMouseDown = false;
            //if (Event.current.type == EventType.MouseDown && Event.current.button == 1) bRightMouseDown = true;
            //if (Event.current.type == EventType.MouseUp && Event.current.button == 1) bRightMouseDown = false;
            if (Event.current.type == EventType.MouseDown && Event.current.button == 2) middleMouseDown = true;
            if (Event.current.type == EventType.MouseUp && Event.current.button == 2) middleMouseDown = false;
        }

        private void DrawGUIShaderSelect()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < bcg.GetGraphCount(); i++)
            {
                if (selectedGraph == i)
                    GUI.backgroundColor = Color.cyan;
                else
                    GUI.backgroundColor = Color.white;

                if (GUILayout.Button(bcg.GetGraphName(i)))
                {
                    SaveNodes();
                    selectedGraph = i;
                    LoadNodes();
                    //bCyclesNeedsUpdate = true;
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
        }

        private void DrawGUINodes()
        {
            int count = 0;
            BeginWindows();
            foreach (BocsNodeBase n in BocsCyclesNodeManager.Nodes)
            {
                n.DrawNode(count);
                count++;
            }
            EndWindows();
        }

        private void DrawGUISockets()
        {
            foreach (BocsSlotBase s in BocsCyclesNodeManager.Slots)
            {
                s.DrawSlotSocket();
                if (leftMouseDown && dragSlot == null && s.SlotType == BocsSlotBase.BocsSlotType.Input && s.InputSlot != null && s.SlotRect.Contains(Event.current.mousePosition))
                {
                    s.RemoveConnection();
                    SaveNodes();
                    cyclesNeedsUpdate = true;
                }

                if (leftMouseDown && dragSlot == null && s.SlotType == BocsSlotBase.BocsSlotType.Output && s.SlotRect.Contains(Event.current.mousePosition))
                {
                    dragSlot = s;
                }
            }
        }

        private void DrawGUIConnections()
        {
            foreach (BocsNodeBase n in BocsCyclesNodeManager.Nodes)
            {
                n.DrawConnections();
            }
        }

        private void DrawGUIConnectDrag()
        {
            if (dragSlot != null)
            {
                Rect cp = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 0, 0);
                DrawNodeCurve(dragSlot.SlotRect, cp);
            }

            if (Event.current.type == EventType.Repaint && !leftMouseDown && dragSlot != null)
            {
                foreach (BocsSlotBase s in BocsCyclesNodeManager.Slots)
                {
                    if (s.SlotRect.Contains(Event.current.mousePosition))
                    {
                        if (s.SlotType == BocsSlotBase.BocsSlotType.Input)
                        {
                            if (s.Node != dragSlot.Node)
                            {
                                //if (s._slotValType == _dragSlot._slotValType)
                                {
                                    s.RemoveConnection();
                                    dragSlot.AddConnection(s);
                                    SaveNodes();
                                    cyclesNeedsUpdate = true;
                                }
                            }
                        }
                    }
                }
                dragSlot = null;
            }
        }

        private void GUIScroll()
        {
            if (middleMouseDown)
            {
                if (Event.current.type == EventType.Repaint)
                {
                    if (scrollAmount == Vector2.zero) scrollAmount = Event.current.mousePosition;

                    Vector2 v = Event.current.mousePosition - scrollAmount;
                    if (v != Vector2.zero)
                    {
                        foreach (BocsNodeBase n in BocsCyclesNodeManager.Nodes)
                        {
                            Rect np = n.NodeRect;
                            np.x += v.x;
                            np.y += v.y;
                            n.NodeRect = np;
                        }
                    }
                    scrollAmount = Event.current.mousePosition;
                }
            }
            else
                scrollAmount = Vector2.zero;
        }

        private void GUIZoom()
        {
            var e = Event.current;
            if (e.type == EventType.ScrollWheel)
            {
                var zoomDelta = 0.05f;
                zoomDelta = e.delta.y < 0 ? zoomDelta : -zoomDelta;
                zoomScale += zoomDelta;
                zoomScale = Mathf.Clamp(zoomScale, 0.25f, 1.0f);
                e.Use();
            }
        }

        private void GUIMenu()
        {
            if (Event.current.type == EventType.ContextClick)
            {
                lastMouse = Event.current.mousePosition;

                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Clear"), false, Callback, "Clear");
                menu.AddItem(new GUIContent("Copy"), false, Callback, "Copy");
                menu.AddItem(new GUIContent("Paste"), false, Callback, "Paste");

                menu.AddSeparator(string.Empty);

                menu.AddItem(new GUIContent("Presets/Common/Diffuse"), false, Callback, "ps_diffuse");
                menu.AddItem(new GUIContent("Presets/Common/Textured"), false, Callback, "ps_textured");
                menu.AddItem(new GUIContent("Presets/Common/Textured + Normal Map"), false, Callback, "ps_texturednormalmap");

                menu.AddItem(new GUIContent("Presets/Materials/Checker"), false, Callback, "ps_checker");
                menu.AddItem(new GUIContent("Presets/Materials/Glass"), false, Callback, "ps_glass");
                menu.AddItem(new GUIContent("Presets/Materials/Chrome"), false, Callback, "ps_chrome");
                menu.AddItem(new GUIContent("Presets/Materials/Shiny"), false, Callback, "ps_shiny");
                menu.AddItem(new GUIContent("Presets/Materials/Brush Metal"), false, Callback, "ps_brushedmetal");
                menu.AddItem(new GUIContent("Presets/Materials/Sub Surface"), false, Callback, "ps_subsurface");

                menu.AddItem(new GUIContent("Presets/Billboard"), false, Callback, "ps_billboard");
                menu.AddItem(new GUIContent("Presets/Wire Frame"), false, Callback, "ps_wireframe");
                menu.AddItem(new GUIContent("Presets/Mesh Light"), false, Callback, "ps_meshlight");

                menu.AddSeparator(string.Empty);

                menu.AddItem(new GUIContent("Output"), false, Callback, "BocsNodeOutput");

                menu.AddItem(new GUIContent("Input/Attribute"), false, Callback, "BocsNodeAttribute");
                menu.AddItem(new GUIContent("Input/Camera Data"), false, Callback, "BocsNodeCameraData");
                menu.AddItem(new GUIContent("Input/Fresnel"), false, Callback, "BocsNodeFresnel");
                menu.AddItem(new GUIContent("Input/Geometry"), false, Callback, "BocsNodeGeometry");
                menu.AddItem(new GUIContent("Input/Hair Info"), false, Callback, "BocsNodeHairInfo");
                menu.AddItem(new GUIContent("Input/Layer Weight"), false, Callback, "BocsNodeLayerWeight");
                menu.AddItem(new GUIContent("Input/Light Path"), false, Callback, "BocsNodeLightPath");
                menu.AddItem(new GUIContent("Input/Object Info"), false, Callback, "BocsNodeObjectInfo");
                menu.AddItem(new GUIContent("Input/Particle Info"), false, Callback, "BocsNodeParticleInfo");
                menu.AddItem(new GUIContent("Input/RGB"), false, Callback, "BocsNodeColor");
                menu.AddItem(new GUIContent("Input/Tangent"), false, Callback, "BocsNodeTangent");
                menu.AddItem(new GUIContent("Input/Texture Coordinate"), false, Callback, "BocsNodeTextureCoordinate");
                menu.AddItem(new GUIContent("Input/UV Map"), false, Callback, "BocsNodeUVmap");
                menu.AddItem(new GUIContent("Input/Value"), false, Callback, "BocsNodeValue");
                menu.AddItem(new GUIContent("Input/Wireframe"), false, Callback, "BocsNodeWireframe");

                menu.AddItem(new GUIContent("Shader/Add"), false, Callback, "BocsNodeAddShader");
                menu.AddItem(new GUIContent("Shader/Ambient Occlusion"), false, Callback, "BocsNodeAmbientOcclusion");
                menu.AddItem(new GUIContent("Shader/Anisotropic"), false, Callback, "BocsNodeAnisotropicBsdf");
                menu.AddItem(new GUIContent("Shader/Background"), false, Callback, "BocsNodeBackground");
                menu.AddItem(new GUIContent("Shader/Diffuse"), false, Callback, "BocsNodeDiffuseBsdf");
                menu.AddItem(new GUIContent("Shader/Disney Bsdf"), false, Callback, "BocsNodeDisneyBsdf");
                menu.AddItem(new GUIContent("Shader/Emission"), false, Callback, "BocsNodeEmission");
                menu.AddItem(new GUIContent("Shader/Glass"), false, Callback, "BocsNodeGlassBsdf");
                menu.AddItem(new GUIContent("Shader/Glossy"), false, Callback, "BocsNodeGlossyBsdf");
                menu.AddItem(new GUIContent("Shader/Hair"), false, Callback, "BocsNodeHairBsdf");
                menu.AddItem(new GUIContent("Shader/Holdout"), false, Callback, "BocsNodeHoldout");
                menu.AddItem(new GUIContent("Shader/Mix"), false, Callback, "BocsNodeMixShader");
                menu.AddItem(new GUIContent("Shader/Refraction"), false, Callback, "BocsNodeRefractionBsdf");
                menu.AddItem(new GUIContent("Shader/Subsurface Scattering"), false, Callback, "BocsNodeSubsurfaceScattering");
                menu.AddItem(new GUIContent("Shader/Toon"), false, Callback, "BocsNodeToonBsdf");
                menu.AddItem(new GUIContent("Shader/Translucent"), false, Callback, "BocsNodeTranslucentBsdf");
                menu.AddItem(new GUIContent("Shader/Transparent"), false, Callback, "BocsNodeTransparentBsdf");
                menu.AddItem(new GUIContent("Shader/Velvet"), false, Callback, "BocsNodeVelvetBsdf");
                menu.AddItem(new GUIContent("Shader/Volume Absorption"), false, Callback, "BocsNodeAbsorptionVolume");
                menu.AddItem(new GUIContent("Shader/Volume Scatter"), false, Callback, "BocsNodeScatterVolume");

                menu.AddItem(new GUIContent("Texture/Brick"), false, Callback, "BocsNodeBrickTexture");
                menu.AddItem(new GUIContent("Texture/Checker"), false, Callback, "BocsNodeCheckerTexture");
                menu.AddItem(new GUIContent("Texture/Environment"), false, Callback, "BocsNodeEnviromentTexture");
                menu.AddItem(new GUIContent("Texture/Gradient"), false, Callback, "BocsNodeGradientTexture");
                menu.AddItem(new GUIContent("Texture/Image"), false, Callback, "BocsNodeTexture");
                menu.AddItem(new GUIContent("Texture/Magic"), false, Callback, "BocsNodeMagicTexture");
                menu.AddItem(new GUIContent("Texture/Musgrave"), false, Callback, "BocsNodeMusgraveTexture");
                menu.AddItem(new GUIContent("Texture/Noise"), false, Callback, "BocsNodeNoiseTexture");
                //(MISSING)Point Density
                menu.AddItem(new GUIContent("Texture/Sky"), false, Callback, "BocsNodeSkyTexture");
                menu.AddItem(new GUIContent("Texture/Voronoi"), false, Callback, "BocsNodeVoronoiTexture");
                menu.AddItem(new GUIContent("Texture/Wave"), false, Callback, "BocsNodeWaveTexture");

                menu.AddItem(new GUIContent("Color/Bright Contrast"), false, Callback, "BocsNodeBrightContrast");
                menu.AddItem(new GUIContent("Color/Gamma"), false, Callback, "BocsNodeGamma");
                menu.AddItem(new GUIContent("Color/Hue Saturation"), false, Callback, "BocsNodeHueSaturation");
                menu.AddItem(new GUIContent("Color/Invert"), false, Callback, "BocsNodeInvert");
                menu.AddItem(new GUIContent("Color/Light Falloff"), false, Callback, "BocsNodeLightFalloff");
                menu.AddItem(new GUIContent("Color/Mix"), false, Callback, "BocsNodeMixRGB");
                //(MISSING)RGB Curves

                menu.AddItem(new GUIContent("Vector/Bump"), false, Callback, "BocsNodeBump");
                menu.AddItem(new GUIContent("Vector/Mapping"), false, Callback, "BocsNodeMapping");
                menu.AddItem(new GUIContent("Vector/Normal"), false, Callback, "BocsNodeNormal");
                menu.AddItem(new GUIContent("Vector/Normal Map"), false, Callback, "BocsNodeNormalMap");
                //(MISSING)Vector Curves
                menu.AddItem(new GUIContent("Vector/Vector Transform"), false, Callback, "BocsNodeVectorTransform");

                menu.AddItem(new GUIContent("Converter/Blackbody"), false, Callback, "BocsNodeBlackbody");
                //(MISSING)Color Ramp
                menu.AddItem(new GUIContent("Converter/Combine HSV"), false, Callback, "BocsNodeCombineHSV");
                menu.AddItem(new GUIContent("Converter/Combine RGB"), false, Callback, "BocsNodeCombineRGB");
                menu.AddItem(new GUIContent("Converter/Combine XYZ"), false, Callback, "BocsNodeCombineXYZ");
                menu.AddItem(new GUIContent("Converter/Math"), false, Callback, "BocsNodeMath");
                menu.AddItem(new GUIContent("Converter/RGB to BW"), false, Callback, "BocsNodeRGBtoBW");
                menu.AddItem(new GUIContent("Converter/Separate HSV"), false, Callback, "BocsNodeSeparateHSV");
                menu.AddItem(new GUIContent("Converter/Separate RGB"), false, Callback, "BocsNodeSeparateRGB");
                menu.AddItem(new GUIContent("Converter/Separate XYZ"), false, Callback, "BocsNodeSeparateXYZ");
                menu.AddItem(new GUIContent("Converter/Vector Math"), false, Callback, "BocsNodeVectorMath");
                menu.AddItem(new GUIContent("Converter/Wave Length"), false, Callback, "BocsNodeWaveLength");

                menu.ShowAsContext();
                Event.current.Use();
            }
        }

        private void GUIKeys()
        {
            if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Delete)
            {
                BocsCyclesNodeManager.DeleteNode(BocsCyclesNodeManager.SelectedNode);
                SaveNodes();
                cyclesNeedsUpdate = true;
            }
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
            {
                BocsCyclesNodeManager.SnapToGrid();
                SaveNodes();
            }
        }

        private void UpdateCycles()
        {
            if (cyclesNeedsUpdate == false) return;

            if (graphType == GraphType.Material) BocsCyclesAPI.UpdateMaterial(bcg.gameObject);
            if (graphType == GraphType.Light) BocsCyclesAPI.UpdateObject(bcg.gameObject);
            if (graphType == GraphType.World)
            {
                BocsCyclesAPI.Cycles_request_settings();
                BocsCyclesAPI.Cycles_request_reset();
            }

            cyclesNeedsUpdate = false;
        }

        private void DoPreset(string p)
        {
            if (bcg == null) return;
            bcg.SetGraph(selectedGraph, p);
            LoadNodes();
            cyclesNeedsUpdate = true;
        }

        private void Callback(object obj)
        {
            if (bcg == null) return;
            if ((string)obj == "Clear") DoPreset(string.Empty);
            if ((string)obj == "Copy") paste = bcg.GetGraph(selectedGraph);
            if ((string)obj == "Paste") DoPreset(paste);

            //if (obj == "ps_") DoPreset("");
            if ((string)obj == "ps_subsurface") DoPreset("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFE0BDFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=1:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=1:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=0:val|n=1,s=sheen_tint,v=1:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_shiny") DoPreset("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FF0000FF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=1:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_chrome") DoPreset("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=1:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_brushedmetal") DoPreset("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=1:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.25:val|n=1,s=anisotropic,v=1:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=0:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=1:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_diffuse") DoPreset("node|t=BocsNodeOutput,x=380,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=10:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_textured") DoPreset("node|t=BocsNodeOutput,x=590,y=10:node|t=BocsNodeTexture,x=20,y=10:node|t=BocsNodeDisneyBsdf,x=250,y=10:val|n=1,s=filename,v=:val|n=1,s=color_space,v=1:val|n=1,s=use_alpha,v=True:val|n=1,s=interpolation,v=1:val|n=1,s=extension,v=0:val|n=1,s=projection,v=0:val|n=2,s=distribution,v=0:val|n=2,s=base_color,v=FFFFFFFF:val|n=2,s=subsurface_color,v=FF0000FF:val|n=2,s=subsurface,v=0:val|n=2,s=subsurface_radius,v=1 1 1:val|n=2,s=metallic,v=0:val|n=2,s=specular,v=0:val|n=2,s=specular_tint,v=0:val|n=2,s=roughness,v=0:val|n=2,s=anisotropic,v=0.5:val|n=2,s=anisotropic_rotation,v=0:val|n=2,s=sheen,v=1:val|n=2,s=sheen_tint,v=0:val|n=2,s=clearcoat,v=0:val|n=2,s=clearcoat_gloss,v=1:val|n=2,s=ior,v=1.45:val|n=2,s=transparency,v=0:connect|n1=1,n2=2,s1=color,s2=base_color,:connect|n1=2,n2=0,s1=bsdf,s2=surface,:");
            if ((string)obj == "ps_texturednormalmap") DoPreset("node|t=BocsNodeOutput,x=590,y=10,c=0:node|t=BocsNodeTexture,x=20,y=10,c=0:node|t=BocsNodeDisneyBsdf,x=250,y=10,c=0:node|t=BocsNodeTexture,x=20,y=350,c=0:node|t=BocsNodeNormalMap,x=230,y=520,c=0:val|n=1,s=filename,v=:val|n=1,s=color_space,v=1:val|n=1,s=use_alpha,v=True:val|n=1,s=interpolation,v=1:val|n=1,s=extension,v=0:val|n=1,s=projection,v=0:val|n=2,s=distribution,v=0:val|n=2,s=base_color,v=FFFFFFFF:val|n=2,s=subsurface_color,v=FF0000FF:val|n=2,s=subsurface,v=0:val|n=2,s=subsurface_radius,v=1 1 1:val|n=2,s=metallic,v=0:val|n=2,s=specular,v=0:val|n=2,s=specular_tint,v=0:val|n=2,s=roughness,v=0:val|n=2,s=anisotropic,v=0.5:val|n=2,s=anisotropic_rotation,v=0:val|n=2,s=sheen,v=1:val|n=2,s=sheen_tint,v=0:val|n=2,s=clearcoat,v=0:val|n=2,s=clearcoat_gloss,v=1:val|n=2,s=ior,v=1.45:val|n=2,s=transparency,v=0:val|n=3,s=filename,v=:val|n=3,s=color_space,v=0:val|n=3,s=use_alpha,v=False:val|n=3,s=interpolation,v=1:val|n=3,s=extension,v=0:val|n=3,s=projection,v=0:val|n=4,s=space,v=1:val|n=4,s=attribute,v=:val|n=4,s=strength,v=1:val|n=4,s=color,v=7F7F7FFF:connect|n1=1,n2=2,s1=color,s2=base_color,:connect|n1=2,n2=0,s1=bsdf,s2=surface,:connect|n1=3,n2=4,s1=color,s2=color,:connect|n1=4,n2=2,s1=normal,s2=normal,:");
            if ((string)obj == "ps_billboard") DoPreset("node|t=BocsNodeOutput,x=730,y=10:node|t=BocsNodeTexture,x=10,y=10:node|t=BocsNodeMixShader,x=560,y=10:node|t=BocsNodeTransparentBsdf,x=270,y=100:node|t=BocsNodeDiffuseBsdf,x=250,y=210:val|n=1,s=filename,v=:val|n=1,s=color_space,v=1:val|n=1,s=use_alpha,v=True:val|n=1,s=interpolation,v=1:val|n=1,s=extension,v=0:val|n=1,s=projection,v=0:val|n=2,s=fac,v=1:val|n=3,s=color,v=FFFFFFFF:val|n=4,s=color,v=FFFFFFFF:val|n=4,s=roughness,v=0:connect|n1=1,n2=4,s1=color,s2=color,:connect|n1=1,n2=2,s1=alpha,s2=fac,:connect|n1=2,n2=0,s1=closure,s2=surface,:connect|n1=3,n2=2,s1=bsdf,s2=closure1,:connect|n1=4,n2=2,s1=bsdf,s2=closure2,:");
            if ((string)obj == "ps_checker") DoPreset("node|t=BocsNodeOutput,x=690,y=20:node|t=BocsNodeDisneyBsdf,x=350,y=20:node|t=BocsNodeCheckerTexture,x=20,y=20:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.5:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:val|n=2,s=color1,v=848484FF:val|n=2,s=color2,v=FFFFFFFF:val|n=2,s=scale,v=1:connect|n1=1,n2=0,s1=bsdf,s2=surface,:connect|n1=2,n2=1,s1=color,s2=base_color,:");
            if ((string)obj == "ps_glass") DoPreset("node|t=BocsNodeOutput,x=350,y=10:node|t=BocsNodeDisneyBsdf,x=30,y=10:node|t=BocsNodeAbsorptionVolume,x=30,y=460:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=1:val|n=2,s=color,v=FF0000FF:val|n=2,s=density,v=0:connect|n1=1,n2=0,s1=bsdf,s2=surface,:connect|n1=2,n2=0,s1=volume,s2=volume,:");
            if ((string)obj == "ps_wireframe") DoPreset("node|t=BocsNodeOutput,x=540,y=10:node|t=BocsNodeDisneyBsdf,x=40,y=120:node|t=BocsNodeWireframe,x=40,y=10:node|t=BocsNodeMixShader,x=380,y=10:node|t=BocsNodeDiffuseBsdf,x=40,y=580:val|n=1,s=distribution,v=0:val|n=1,s=base_color,v=FFFFFFFF:val|n=1,s=subsurface_color,v=FF0000FF:val|n=1,s=subsurface,v=0:val|n=1,s=subsurface_radius,v=1 1 1:val|n=1,s=metallic,v=0:val|n=1,s=specular,v=0:val|n=1,s=specular_tint,v=0:val|n=1,s=roughness,v=0.5:val|n=1,s=anisotropic,v=0.5:val|n=1,s=anisotropic_rotation,v=0:val|n=1,s=sheen,v=1:val|n=1,s=sheen_tint,v=0:val|n=1,s=clearcoat,v=0:val|n=1,s=clearcoat_gloss,v=1:val|n=1,s=ior,v=1.45:val|n=1,s=transparency,v=0:val|n=2,s=use_pixel_size,v=False:val|n=2,s=size,v=0.01:val|n=3,s=fac,v=1:val|n=4,s=color,v=FF0000FF:val|n=4,s=roughness,v=0:connect|n1=1,n2=3,s1=bsdf,s2=closure1,:connect|n1=2,n2=3,s1=fac,s2=fac,:connect|n1=3,n2=0,s1=closure,s2=surface,:connect|n1=4,n2=3,s1=bsdf,s2=closure2,:");
            if ((string)obj == "ps_meshlight") DoPreset("node|t=BocsNodeOutput,x=370,y=20:node|t=BocsNodeEmission,x=30,y=20:val|n=1,s=color,v=FAF3B5FF:val|n=1,s=strength,v=10:connect|n1=1,n2=0,s1=emission,s2=surface,:");

            BocsCyclesNodeManager.CreateNode(obj.ToString(), lastMouse.x, lastMouse.y, 0);
        }

        private void LoadNodes()
        {
            if (bcg == null) return;
            BocsCyclesNodeManager.LoadGraph(bcg.GetGraph(selectedGraph));
        }

        private void SaveNodes()
        {
            if (bcg == null) return;
            bcg.SetGraph(selectedGraph, BocsCyclesNodeManager.SaveGraph());
        }
    }
}