// _____ _____ _____ _____    __    _     _   _
//| __  |     |     |   __|  |  |  |_|___| |_| |_ _____ ___ ___ ___ ___ ___
//| __ -|  |  |   --|__   |  |  |__| | . |   |  _|     | .'| . | . | -_|  _|
//|_____|_____|_____|_____|  |_____|_|_  |_|_|_| |_|_|_|__,|  _|  _|___|_|
//                                   |___|                 |_| |_|

namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public class EditorSceneview
    {
        //Display Vars
        private static bool fullScreen = false;

        //Async update Vars-----------------------------
        private static bool synced = false;

        private static Light[] syncLights;
        private static MeshRenderer[] syncMeshes;
        private static SkinnedMeshRenderer[] syncSkins;
        private static int syncLightsIndex;
        private static int syncMeshesIndex;
        private static int syncSkinsIndex;
        private static int syncCurrent;
        private static int syncTotal;
        //----------------------------------------------

        //Cycles Vars
        private static Texture2D frameBuffer;

        private static Camera camera;
        private static BocsCyclesCamera cameraSettings;

        private static int samples = 2048;
        private static int width = 256;
        private static int height = 145;

        //.transform.hasChanged not working
        private static Vector3 lastPosition;

        private static Quaternion lastRotation;

        private static Texture texFullScreen = Resources.Load<Texture>("fullscreen");
        private static Texture texRestore = Resources.Load<Texture>("restore");
        private static Texture texClose = Resources.Load<Texture>("close");
        private static GUISkin texSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);

        private static bool started = false;
        private static bool waitForNextFrame = true;
        private static int sceneID = 0;

        private static bool hasUpdated = false;
        private static bool updateCamera = false;
        private static float nextUpdate = 0;

        //static EditorSceneview()
        //{
        //    //Startup();
        //}

        [MenuItem("Window/BOCS Lightmapper/Preview", false, 1)]
        private static void Startup()
        {
            if (started) return;
            if (Application.isPlaying) return;

            if (Camera.main == null)
            {
                Debug.Log("No MainCamera Found!");
                return;
            }
            if (SceneView.lastActiveSceneView == null)
            {
                Debug.Log("No SceneView Active!");
                return;
            }

            camera = SceneView.lastActiveSceneView.camera;

            if (camera == null)
            {
                Debug.Log("No SceneView Camera Found!");
                return;
            }

            cameraSettings = Camera.main.GetComponent<BocsCyclesCamera>();

            if (cameraSettings == null)
            {
                cameraSettings = Camera.main.gameObject.AddComponent<BocsCyclesCamera>();
            }

            if (fullScreen)
                cameraSettings._transparent = true;
            else
                cameraSettings._transparent = false;

            lastPosition = camera.transform.position;
            lastRotation = camera.transform.rotation;

            //Debug.Log("Startup...");

            StartSync();

            EditorApplication.update += Update;
            EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
            EditorApplication.hierarchyWindowChanged += HierarchyWindowChanged;
            SceneView.onSceneGUIDelegate += OnSceneView;
            Selection.selectionChanged += SelectionChanged;

            GameObject o = GameObject.Find("<HDO>");
            if (o == null) o = new GameObject("<HDO>");
            o.hideFlags = HideFlags.HideInHierarchy;
            sceneID = o.GetInstanceID();

            started = true;

            BocsCyclesAPI.Cycles_reset();
            BocsCyclesAPI.Cycles_set_active();
            BocsCyclesAPI.UpdateSettings(camera, cameraSettings, width, height, samples);
        }

        //[MenuItem("Test/Shutdown")]
        private static void Shutdown()
        {
            //Debug.Log("Shutdown");
            EditorApplication.update -= Update;
            EditorApplication.playmodeStateChanged -= PlaymodeStateChanged;
            EditorApplication.hierarchyWindowChanged -= HierarchyWindowChanged;
            SceneView.onSceneGUIDelegate -= OnSceneView;
            Selection.selectionChanged -= SelectionChanged;

            GameObject.DestroyImmediate(GameObject.Find("<HDO>"));
            sceneID = 0;

            started = false;

            BocsCyclesAPI.Cycles_reset();
            BocsCyclesAPI.Cycles_debug();
            frameBuffer = null;
        }

        private static void UpdateFramebuffer()
        {
            frameBuffer = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
            frameBuffer.filterMode = FilterMode.Point;
            frameBuffer.Apply();
            BocsCyclesAPI.Cycles_set_texture(frameBuffer.GetNativeTexturePtr(), frameBuffer.width, frameBuffer.height);
            waitForNextFrame = true;
        }

        private static void OptimizeLODs()
        {
            LODGroup[] lodGroups = GameObject.FindObjectsOfType<LODGroup>();
            for (int o = 0; o < lodGroups.Length; o++)
            {
                LOD[] lods = lodGroups[o].GetLODs();
                for (int l = 1; l < lods.Length; l++)
                {
                    foreach (Renderer rend in lods[l].renderers)
                    {
                        if (rend != null)
                        {
                            if (rend.gameObject.GetComponent<BocsCyclesSkip>() == null)
                            {
                                rend.gameObject.AddComponent<BocsCyclesSkip>();
                            }
                        }
                    }
                }
            }
        }

        private static void StartSync()
        {
            synced = false;

            OptimizeLODs();

            syncLightsIndex = 0;
            syncMeshesIndex = 0;
            syncSkinsIndex = 0;
            syncCurrent = 0;

            syncLights = GameObject.FindObjectsOfType<Light>();
            syncMeshes = GameObject.FindObjectsOfType<MeshRenderer>();
            syncSkins = GameObject.FindObjectsOfType<SkinnedMeshRenderer>();

            syncTotal = syncLights.Length + syncMeshes.Length + syncSkins.Length;
        }

        private static void StepSync()
        {
            if (syncLights.Length > 0)
            {
                if (syncLightsIndex < syncLights.Length)
                {
                    if (syncLights[syncLightsIndex].GetComponent<BocsCyclesSkip>() == null)
                    {
                        if (syncLights[syncLightsIndex].GetComponent<BocsCyclesLight>() == null)
                        {
                            syncLights[syncLightsIndex].gameObject.AddComponent<BocsCyclesLight>();
                        }
                        else
                        {
                            BocsCyclesAPI.AddLight(syncLights[syncLightsIndex].gameObject);
                        }
                    }
                    syncLightsIndex++;
                    syncCurrent++;
                    return;
                }
            }
            if (syncMeshes.Length > 0)
            {
                if (syncMeshesIndex < syncMeshes.Length)
                {
                    if (syncMeshes[syncMeshesIndex].GetComponent<BocsCyclesSkip>() == null)
                    {
                        if (syncMeshes[syncMeshesIndex].GetComponent<BocsCyclesMaterial>() == null)
                        {
                            syncMeshes[syncMeshesIndex].gameObject.AddComponent<BocsCyclesMaterial>();
                        }
                        else
                        {
                            BocsCyclesAPI.AddMesh(syncMeshes[syncMeshesIndex].gameObject);
                        }
                    }
                    syncMeshesIndex++;
                    syncCurrent++;
                    return;
                }
            }

            if (syncSkins.Length > 0)
            {
                if (syncSkinsIndex < syncSkins.Length)
                {
                    if (syncSkins[syncSkinsIndex].GetComponent<BocsCyclesSkip>() == null)
                    {
                        if (syncSkins[syncSkinsIndex].GetComponent<BocsCyclesMaterial>() == null)
                        {
                            syncSkins[syncSkinsIndex].gameObject.AddComponent<BocsCyclesMaterial>();
                        }
                        else
                        {
                            BocsCyclesAPI.AddMesh(syncSkins[syncSkinsIndex].gameObject);
                        }
                    }
                    syncSkinsIndex++;
                    syncCurrent++;
                    return;
                }
            }

            synced = true;
        }

        private static void SyncScene()
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view == null)
            {
                return;
            }

            cameraSettings._fov = camera.fieldOfView;

            int w = 256;
            int h = 145;

            if (fullScreen)
            {
                w = view.camera.pixelWidth / 2;
                h = view.camera.pixelHeight / 2;
            }

            //hack..size keeps toggling by 1 pixel wtf????
            if (Mathf.Abs(w - width) > 2 || Mathf.Abs(h-height) >2)
            {
                //Debug.Log("resize: " + w + "," + h);

                width = w;
                height = h;
                frameBuffer = null;

                updateCamera = true;
                BocsCyclesAPI.Cycles_request_reset();

                hasUpdated = true;
                waitForNextFrame = true;
                nextUpdate = Time.realtimeSinceStartup + .25f;
            }

            if (frameBuffer == null)
            {
                UpdateFramebuffer();
                hasUpdated = true;
                waitForNextFrame = true;
                nextUpdate = Time.realtimeSinceStartup + .25f;
            }

            if (camera.transform.position != lastPosition || camera.transform.rotation != lastRotation)
            {
                lastPosition = camera.transform.position;
                lastRotation = camera.transform.rotation;

                updateCamera = true;
                BocsCyclesAPI.Cycles_request_reset();

                waitForNextFrame = true;
                hasUpdated = true;
                nextUpdate = Time.realtimeSinceStartup + .25f;
            }

            if (BocsCyclesAPI.Cycles_need_reset())
            {
                hasUpdated = true;
                waitForNextFrame = true;
                nextUpdate = Time.realtimeSinceStartup + .25f;
            }

            if (hasUpdated && Time.realtimeSinceStartup > nextUpdate)
            {
                if (BocsCyclesAPI.Cycles_need_settings())
                    BocsCyclesAPI.UpdateSettings(camera, cameraSettings, width, height, samples);
                else if (updateCamera)
                    BocsCyclesAPI.UpdateCameraPositon(camera, cameraSettings, width, height);

                BocsCyclesAPI.Cycles_render_async();

                hasUpdated = false;
                updateCamera = false;
            }
        }

        private static void Update()
        {
            if (synced == false)
            {
                float syncTime = Time.realtimeSinceStartup + .03f;//Try to maintain 30fps til synced...
                while (synced == false)
                {
                    if (Time.realtimeSinceStartup >= syncTime) break;
                    StepSync();
                }
                SceneView.lastActiveSceneView.Repaint();
            }
            else
            {
                SyncScene();

                if (BocsCyclesAPI.Cycles_image_ready())
                {
                    //Debug.Log("UPDATE RENDER " + BocsCyclesAPI.cycles_progress());
                    GL.IssuePluginEvent(BocsCyclesAPI.GetRenderEventFunc(), 1);
                    SceneView.lastActiveSceneView.Repaint();
                    waitForNextFrame = false;
                }
            }
        }

        private static void GUI_Draw_Syncing(SceneView sceneView)
        {
            float barLength = ((float)syncCurrent / (float)syncTotal) * sceneView.position.width;
            EditorGUI.DrawRect(new Rect(0, sceneView.position.height - 24, barLength, 4), Color.yellow);
        }

        private static void GUI_Draw_Updatebar(SceneView sceneView)
        {
            if (waitForNextFrame) return;

            int sample = BocsCyclesAPI.Cycles_progress();
            if (sample == samples) return;

            float barLength = (float)sample / (float)samples;

            if (fullScreen)
                EditorGUI.DrawRect(new Rect(0, sceneView.position.height - 24, barLength * sceneView.position.width, 4), Color.blue);
            else
                EditorGUI.DrawRect(new Rect(10, sceneView.position.height - 24, barLength * 256.0f, 4), Color.blue);
        }

        private static void GUI_Draw_Render(SceneView sceneView)
        {
            if (frameBuffer)
            {
                if (fullScreen)
                {
                    if (!waitForNextFrame)
                        GUI.DrawTexture(new Rect(0, 0, sceneView.camera.pixelWidth, sceneView.camera.pixelHeight), frameBuffer, ScaleMode.ScaleAndCrop);

                    if (GUI.Button(new Rect(sceneView.position.width - 18, sceneView.position.height - 40, 18, 18), texRestore))
                    {
                        fullScreen = false;
                        cameraSettings._transparent = false;
                        BocsCyclesAPI.UpdateSettings(camera, cameraSettings, width, height, samples);
                    }
                }
                else
                {
                    Rect renderRect = new Rect(10 - 2, sceneView.position.height - 145 - 24 - 2, 256 + 4, 145 + 4);
                    EditorGUI.DrawRect(renderRect, Color.black);

                    Rect previewRect = new Rect(10, sceneView.position.height - 145 - 24, 256, 145);
                    GUI.DrawTexture(previewRect, frameBuffer, ScaleMode.ScaleAndCrop);

                    if (GUI.Button(new Rect(10 + 256 - 18, sceneView.position.height - 24 - 18, 18, 18), texFullScreen))
                    {
                        fullScreen = true;
                        cameraSettings._transparent = true;
                        BocsCyclesAPI.UpdateSettings(camera, cameraSettings, width, height, samples);
                    }
                }

                GUI_Draw_Updatebar(sceneView);
            }
        }

        private static void OnSceneView(SceneView sceneView)
        {
            //if ( sceneView.m_RenderMode != DrawCameraMode.Baked) return;

            Handles.BeginGUI();

            GUI.skin = texSkin;

            if (synced)
            {
                GUI_Draw_Render(sceneView);
            }
            else
            {
                GUI_Draw_Syncing(sceneView);
            }

            if (fullScreen || !synced)
            {
                if (GUI.Button(new Rect(0, 0, 18, 18), texClose)) Shutdown();
            }
            else
            {
                if (GUI.Button(new Rect(10, sceneView.position.height - 145 - 24, 18, 18), texClose)) Shutdown();
            }

            //samples = EditorGUI.IntSlider(new Rect(sceneView.position.width/2 - 128, 5, 256, 16), samples, 4, 256);

            Handles.EndGUI();
        }

        private static void PlaymodeStateChanged()
        {
            //Debug.Log("PlaymodeStateChanged");
            Shutdown();
        }

        private static void SceneChanged()
        {
            //Debug.Log("Scene Changed");
            Shutdown();
        }

        private static void HierarchyWindowChanged()
        {
            if (Application.isPlaying) return;

            GameObject o = GameObject.Find("<HDO>");
            if (o == null || o.GetInstanceID() != sceneID) SceneChanged();
        }

        private static void SelectionChanged()
        {
            if (Application.isPlaying) return;

            foreach (Transform obj in Selection.transforms)
	        {
	        	EditorUtils.UpdateObjectComponent(obj.gameObject);
	        }
        }
    }
}