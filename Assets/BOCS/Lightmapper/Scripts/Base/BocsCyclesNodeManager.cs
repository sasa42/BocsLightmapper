namespace BOCSLIGHTMAPPER
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BocsCyclesNodeManager
    {
        private static List<BocsNodeBase> nodes = new List<BocsNodeBase>();
        private static List<BocsSlotBase> slots = new List<BocsSlotBase>();
        private static int selectedNode = -1;
        private static float snapSize = 10;

        public static List<BocsNodeBase> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        public static List<BocsSlotBase> Slots
        {
            get { return slots; }
            set { slots = value; }
        }

        public static int SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value; }
        }

        public static float SnapSize
        {
            get { return snapSize; }
            set { snapSize = value; }
        }

        public static void Reset()
        {
            Nodes = new List<BocsNodeBase>();
            Slots = new List<BocsSlotBase>();
            SelectedNode = -1;
        }

        public static void ClearSlots()
        {
            Slots = new List<BocsSlotBase>();
        }

        public static void SnapToGrid()
        {
            foreach (BocsNodeBase n in Nodes)
            {
                float x = (int)(n.NodeRect.xMin / SnapSize) * SnapSize;
                float y = (int)(n.NodeRect.yMin / SnapSize) * SnapSize;
                n.NodeRect = new Rect(x, y, n.NodeRect.width, n.NodeRect.height);
            }
        }

        public static void CreateNode(string type, float x, float y, int c)
        {
            BocsNodeBase nb = null;

            //I'm sure there is a better way, maybe Activator or reflection..but meh for now
            if (type == "BocsNodeOutput") nb = new BocsNodeOutput();
            if (type == "BocsNodeDiffuseBsdf") nb = new BocsNodeDiffuseBsdf();
            if (type == "BocsNodeMixShader") nb = new BocsNodeMixShader();
            if (type == "BocsNodeGlossyBsdf") nb = new BocsNodeGlossyBsdf();
            if (type == "BocsNodeTexture") nb = new BocsNodeTexture();
            if (type == "BocsNodeGlassBsdf") nb = new BocsNodeGlassBsdf();
            if (type == "BocsNodeEmission") nb = new BocsNodeEmission();
            if (type == "BocsNodeEnviromentTexture") nb = new BocsNodeEnviromentTexture();
            if (type == "BocsNodeSkyTexture") nb = new BocsNodeSkyTexture();
            if (type == "BocsNodeNoiseTexture") nb = new BocsNodeNoiseTexture();
            if (type == "BocsNodeCheckerTexture") nb = new BocsNodeCheckerTexture();
            if (type == "BocsNodeBrickTexture") nb = new BocsNodeBrickTexture();
            if (type == "BocsNodeGradientTexture") nb = new BocsNodeGradientTexture();
            if (type == "BocsNodeVoronoiTexture") nb = new BocsNodeVoronoiTexture();
            if (type == "BocsNodeMusgraveTexture") nb = new BocsNodeMusgraveTexture();
            if (type == "BocsNodeMagicTexture") nb = new BocsNodeMagicTexture();
            if (type == "BocsNodeWaveTexture") nb = new BocsNodeWaveTexture();
            if (type == "BocsNodeNormal") nb = new BocsNodeNormal();
            if (type == "BocsNodeBump") nb = new BocsNodeBump();
            if (type == "BocsNodeAnisotropicBsdf") nb = new BocsNodeAnisotropicBsdf();
            if (type == "BocsNodeTranslucentBsdf") nb = new BocsNodeTranslucentBsdf();
            if (type == "BocsNodeTransparentBsdf") nb = new BocsNodeTransparentBsdf();
            if (type == "BocsNodeVelvetBsdf") nb = new BocsNodeVelvetBsdf();
            if (type == "BocsNodeToonBsdf") nb = new BocsNodeToonBsdf();
            if (type == "BocsNodeRefractionBsdf") nb = new BocsNodeRefractionBsdf();
            if (type == "BocsNodeHairBsdf") nb = new BocsNodeHairBsdf();
            if (type == "BocsNodeAmbientOcclusion") nb = new BocsNodeAmbientOcclusion();
            if (type == "BocsNodeHoldout") nb = new BocsNodeHoldout();
            if (type == "BocsNodeAbsorptionVolume") nb = new BocsNodeAbsorptionVolume();
            if (type == "BocsNodeScatterVolume") nb = new BocsNodeScatterVolume();
            if (type == "BocsNodeSubsurfaceScattering") nb = new BocsNodeSubsurfaceScattering();
            if (type == "BocsNodeGeometry") nb = new BocsNodeGeometry();
            if (type == "BocsNodeTextureCoordinate") nb = new BocsNodeTextureCoordinate();
            if (type == "BocsNodeLightPath") nb = new BocsNodeLightPath();
            if (type == "BocsNodeLightFalloff") nb = new BocsNodeLightFalloff();
            if (type == "BocsNodeObjectInfo") nb = new BocsNodeObjectInfo();
            if (type == "BocsNodeParticleInfo") nb = new BocsNodeParticleInfo();
            if (type == "BocsNodeHairInfo") nb = new BocsNodeHairInfo();
            if (type == "BocsNodeValue") nb = new BocsNodeValue();
            if (type == "BocsNodeColor") nb = new BocsNodeColor();
            if (type == "BocsNodeAddShader") nb = new BocsNodeAddShader();
            if (type == "BocsNodeInvert") nb = new BocsNodeInvert();
            if (type == "BocsNodeMixRGB") nb = new BocsNodeMixRGB();
            if (type == "BocsNodeGamma") nb = new BocsNodeGamma();
            if (type == "BocsNodeBrightContrast") nb = new BocsNodeBrightContrast();
            if (type == "BocsNodeCombineRGB") nb = new BocsNodeCombineRGB();
            if (type == "BocsNodeSeparateRGB") nb = new BocsNodeSeparateRGB();
            if (type == "BocsNodeCombineHSV") nb = new BocsNodeCombineHSV();
            if (type == "BocsNodeSeparateHSV") nb = new BocsNodeSeparateHSV();
            if (type == "BocsNodeCombineXYZ") nb = new BocsNodeCombineXYZ();
            if (type == "BocsNodeSeparateXYZ") nb = new BocsNodeSeparateXYZ();
            if (type == "BocsNodeHueSaturation") nb = new BocsNodeHueSaturation();
            if (type == "BocsNodeWaveLength") nb = new BocsNodeWaveLength();
            if (type == "BocsNodeBlackbody") nb = new BocsNodeBlackbody();
            if (type == "BocsNodeAttribute") nb = new BocsNodeAttribute();
            if (type == "BocsNodeUVmap") nb = new BocsNodeUVmap();
            if (type == "BocsNodeCameraData") nb = new BocsNodeCameraData();
            if (type == "BocsNodeFresnel") nb = new BocsNodeFresnel();
            if (type == "BocsNodeLayerWeight") nb = new BocsNodeLayerWeight();
            if (type == "BocsNodeWireframe") nb = new BocsNodeWireframe();
            if (type == "BocsNodeNormalMap") nb = new BocsNodeNormalMap();
            if (type == "BocsNodeTangent") nb = new BocsNodeTangent();
            if (type == "BocsNodeMath") nb = new BocsNodeMath();
            if (type == "BocsNodeVectorMath") nb = new BocsNodeVectorMath();
            if (type == "BocsNodeVectorTransform") nb = new BocsNodeVectorTransform();
            if (type == "BocsNodeRGBtoBW") nb = new BocsNodeRGBtoBW();
            if (type == "BocsNodeBackground") nb = new BocsNodeBackground();
            if (type == "BocsNodeDisneyBsdf") nb = new BocsNodeDisneyBsdf();
            if (type == "BocsNodeMapping") nb = new BocsNodeMapping();

            if (nb != null)
            {
                Rect pos = new Rect(x, y, 0, 0);
                nb.NodeRect = pos;
                nb.State = c;
                BocsCyclesNodeManager.Nodes.Add(nb);
            }
        }

        public static void LoadGraph(string g)
        {
            //Debug.Log("LoadGraph");
            Reset();

            if (g == null) return;

            string[] sn = g.Split(':');

            foreach (string n in sn)
            {
                //Debug.Log(n);
                string[] s = n.Split('|');
                //foreach(string ts in s) Debug.Log(ts);

                if (s.Length > 0)
                {
                    if (s[0] == "node")
                    {
                        string[] p = s[1].Split(',');
                        //foreach(string ts in p) Debug.Log(ts);

                        int x = 0;
                        int y = 0;
                        int c = 0;
                        string t = string.Empty;
                        foreach (string ts in p)
                        {
                            string[] v = ts.Split('=');
                            if (v.Length > 1)
                            {
                                if (v[0] == "t") t = v[1];
                                if (v[0] == "x") x = int.Parse(v[1]);
                                if (v[0] == "y") y = int.Parse(v[1]);
                                if (v[0] == "c") c = int.Parse(v[1]);
                            }
                        }
                        CreateNode(t, x, y, c);
                    }

                    if (s[0] == "val")
                    {
                        string[] p = s[1].Split(',');
                        //foreach(string ts in p) Debug.Log(ts);

                        int ni = -1;
                        string ss = string.Empty;
                        string sv = string.Empty;

                        foreach (string ts in p)
                        {
                            string[] v = ts.Split('=');
                            if (v.Length > 1)
                            {
                                if (v[0] == "n") ni = int.Parse(v[1]);
                                if (v[0] == "s") ss = v[1];
                                if (v[0] == "v") sv = v[1];
                            }
                        }

                        if (ni >= 0 && ni < BocsCyclesNodeManager.Nodes.Count)
                        {
                            BocsNodeBase nb = BocsCyclesNodeManager.Nodes[ni];
                            if (nb != null)
                            {
                                foreach (BocsSlotBase us in nb.Slots)
                                {
                                    if (us.SlotName == ss) us.SetString(sv);
                                }
                            }
                        }
                    }
                    if (s[0] == "connect")
                    {
                        string[] p = s[1].Split(',');
                        //foreach(string ts in p) Debug.Log(ts);

                        int n1 = -1;
                        int n2 = -1;
                        string s1 = string.Empty;
                        string s2 = string.Empty;

                        foreach (string ts in p)
                        {
                            string[] v = ts.Split('=');
                            if (v.Length > 1)
                            {
                                if (v[0] == "n1") n1 = int.Parse(v[1]);
                                if (v[0] == "n2") n2 = int.Parse(v[1]);
                                if (v[0] == "s1") s1 = v[1];
                                if (v[0] == "s2") s2 = v[1];
                            }
                        }

                        if (n1 != -1 && n2 != -1 && s1 != string.Empty && s2 != string.Empty && n1 < Nodes.Count && n2 < Nodes.Count)
                        {
                            BocsSlotBase f = FindOutputSlotFromString(BocsCyclesNodeManager.Nodes[n1], s1);
                            BocsSlotBase t = FindInputSlotFromString(BocsCyclesNodeManager.Nodes[n2], s2);
                            if (f != null && t != null)
                            {
                                f.AddConnection(t);
                            }
                        }
                    }
                }
            }
        }

        public static BocsSlotBase FindInputSlotFromString(BocsNodeBase bn, string s)
        {
            for (int i = 0; i < bn.Slots.Count; i++)
            {
                if (bn.Slots[i].SlotType != BocsSlotBase.BocsSlotType.Input) continue;
                if (bn.Slots[i].SlotName == s) return bn.Slots[i];
            }
            return null;
        }

        public static BocsSlotBase FindOutputSlotFromString(BocsNodeBase bn, string s)
        {
            for (int i = 0; i < bn.Slots.Count; i++)
            {
                if (bn.Slots[i].SlotType != BocsSlotBase.BocsSlotType.Output) continue;
                if (bn.Slots[i].SlotName == s) return bn.Slots[i];
            }
            return null;
        }

        public static int FindNodeFromSlot(BocsSlotBase s)
        {
            for (int i = 0; i < BocsCyclesNodeManager.Nodes.Count; i++)
            {
                foreach (BocsSlotBase c in BocsCyclesNodeManager.Nodes[i].Slots)
                {
                    if (c == s) return i;
                }
            }
            return -1;
        }

        public static int FindSlotInNode(BocsNodeBase nb, BocsSlotBase sb)
        {
            for (int i = 0; i < nb.Slots.Count; i++)
            {
                if (nb.Slots[i] == sb) return i;
            }
            return -1;
        }

        public static string SaveGraph()
        {
            //Debug.Log("SaveGraph");

            string s = string.Empty;
            for (int i = 0; i < BocsCyclesNodeManager.Nodes.Count; i++)
            {
                BocsNodeBase n = BocsCyclesNodeManager.Nodes[i];
                s += "node" + "|";
                s += "t=" + n.GetType().Name + ",";
                s += "x=" + (int)n.NodeRect.x + ",";
                s += "y=" + (int)n.NodeRect.y + ",";
                s += "c=" + n.State;
                s += ":";
            }

            for (int i = 0; i < BocsCyclesNodeManager.Nodes.Count; i++)
            {
                BocsNodeBase n = BocsCyclesNodeManager.Nodes[i];
                for (int j = 0; j < n.Slots.Count; j++)
                {
                    if (n.Slots[j].HasValue())
                    {
                        s += "val" + "|";
                        s += "n=" + i + ",";
                        s += "s=" + n.Slots[j].SlotName + ",";
                        s += "v=" + n.Slots[j].GetString();
                        s += ":";
                    }
                }
            }

            for (int i = 0; i < BocsCyclesNodeManager.Nodes.Count; i++)
            {
                BocsNodeBase n = BocsCyclesNodeManager.Nodes[i];
                for (int j = 0; j < n.Slots.Count; j++)
                {
                    for (int k = 0; k < n.Slots[j].OutputSlots.Count; k++)
                    {
                        int cn = FindNodeFromSlot(n.Slots[j].OutputSlots[k]);
                        int cs = FindSlotInNode(BocsCyclesNodeManager.Nodes[cn], n.Slots[j].OutputSlots[k]);

                        s += "connect" + "|";
                        s += "n1=" + i + ",";
                        s += "n2=" + cn + ",";
                        s += "s1=" + n.Slots[j].SlotName + ",";
                        s += "s2=" + BocsCyclesNodeManager.Nodes[cn].Slots[cs].SlotName + ",";
                        s += ":";
                    }
                }
            }

            return s;
        }

        public static void DeleteNode(int i)
        {
            if (i == -1) return;
            if (i > Nodes.Count) return;

            SelectedNode = -1;

            //Debug.Log("Delete:" + i);

            BocsNodeBase bn = BocsCyclesNodeManager.Nodes[i];

            foreach (BocsSlotBase s in bn.Slots)
            {
                s.RemoveConnection();
            }

            BocsCyclesNodeManager.Nodes.Remove(bn);
        }
    }
}