namespace BOCSLIGHTMAPPER
{
    using UnityEditor;
    using UnityEngine;

    public class EditorUtils
    {
	    [MenuItem("Window/BOCS Lightmapper/Remove Components", false, 50)]
        public static void CleanAll()
        {
            CleanMaterials();
            CleanLights();
            CleanCameras();
            CleanSkips();
        }

        public static void CleanMaterials()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject o in objs)
            {
                BocsCyclesMaterial m = o.GetComponent<BocsCyclesMaterial>();
                Object.DestroyImmediate(m);
            }
        }

        public static void CleanLights()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject o in objs)
            {
                BocsCyclesLight l = o.GetComponent<BocsCyclesLight>();
                Object.DestroyImmediate(l);
            }
        }

        public static void CleanCameras()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject o in objs)
            {
                BocsCyclesCamera c = o.GetComponent<BocsCyclesCamera>();
                Object.DestroyImmediate(c);
            }
        }

        public static void CleanSkips()
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject o in objs)
            {
                BocsCyclesSkip c = o.GetComponent<BocsCyclesSkip>();
                Object.DestroyImmediate(c);
            }
        }

        [MenuItem("Window/BOCS Lightmapper/Update Components", false, 50)]
        public static void ShowProgressUpdateComponents()
        {
            if (Camera.main == null)
            {
                GameObject cam = new GameObject();
                cam.AddComponent<Camera>();
                cam.name = "BOCS Camera";
                cam.tag = "MainCamera";
            }

            //SKIP LOD1+ Meshes...Optimization
            int skipped = 0;
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
                                skipped++;
                            }
                        }
                    }
                }
            }

            Camera[] cams = GameObject.FindObjectsOfType<Camera>();
            for (int o = 0; o < cams.Length; o++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("Updating Cameras", "Please Wait..." + o + "/" + cams.Length, (float)o / (float)cams.Length))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                if (cams[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
                if (cams[o].gameObject.GetComponent<BocsCyclesCamera>() == null) cams[o].gameObject.AddComponent<BocsCyclesCamera>();
            }

            Light[] lights = GameObject.FindObjectsOfType<Light>();
            for (int o = 0; o < lights.Length; o++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("Updating Lights", "Please Wait..." + o + "/" + lights.Length, (float)o / (float)lights.Length))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                if (lights[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
                if (lights[o].gameObject.GetComponent<BocsCyclesLight>() == null) lights[o].gameObject.AddComponent<BocsCyclesLight>();
            }

            MeshRenderer[] meshs = GameObject.FindObjectsOfType<MeshRenderer>();
            for (int o = 0; o < meshs.Length; o++)
            {
                if (o % 25 == 0)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("Updating Static Meshes", "Please Wait..." + o + "/" + meshs.Length, (float)o / (float)meshs.Length))
                    {
                        EditorUtility.ClearProgressBar();
                        return;
                    }
                }

                if (meshs[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
                if (meshs[o].gameObject.GetComponent<BocsCyclesMaterial>() == null) meshs[o].gameObject.AddComponent<BocsCyclesMaterial>();
            }

            SkinnedMeshRenderer[] skins = GameObject.FindObjectsOfType<SkinnedMeshRenderer>();
            for (int o = 0; o < skins.Length; o++)
            {
                if (o % 25 == 0)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("Updating Skin Meshes", "Please Wait..." + o + "/" + skins.Length, (float)o / (float)skins.Length))
                    {
                        EditorUtility.ClearProgressBar();
                        return;
                    }
                }

                if (skins[o].gameObject.GetComponent<BocsCyclesSkip>() != null) continue;
                if (skins[o].gameObject.GetComponent<BocsCyclesMaterial>() == null) skins[o].gameObject.AddComponent<BocsCyclesMaterial>();
            }

            EditorUtility.ClearProgressBar();
        }
	    
	    public static void UpdateObjectComponent(GameObject o)
	    {
		    Camera[] cs = o.GetComponentsInChildren<Camera>();
		    foreach (Camera c in cs)
		    {
			    if (c.GetComponent<BocsCyclesSkip>() != null) continue;
			    if (c.GetComponent<BocsCyclesCamera>() == null) c.gameObject.AddComponent<BocsCyclesCamera>();
		    }
		    
		    Light[] ls = o.GetComponentsInChildren<Light>();
		    foreach (Light l in ls)
		    {
			    if (l.GetComponent<BocsCyclesSkip>() != null) continue;
			    if (l.GetComponent<BocsCyclesLight>() == null) l.gameObject.AddComponent<BocsCyclesLight>();
		    }
		    
		    MeshRenderer[] mrl = o.GetComponentsInChildren<MeshRenderer>();
		    foreach (MeshRenderer mr in mrl)
		    {
			    if (mr.GetComponent<BocsCyclesSkip>() != null) continue;
			    if (mr.GetComponent<BocsCyclesMaterial>() == null) mr.gameObject.AddComponent<BocsCyclesMaterial>();
		    }
		    
		    SkinnedMeshRenderer[] smrl = o.GetComponentsInChildren<SkinnedMeshRenderer>();
		    foreach (SkinnedMeshRenderer smr in smrl)
		    {
			    if (smr.GetComponent<BocsCyclesSkip>() != null) continue;
			    if (smr.GetComponent<BocsCyclesMaterial>() == null) smr.gameObject.AddComponent<BocsCyclesMaterial>();
		    }
	    }
	    
    }
}