namespace BOCSLIGHTMAPPER
{
    using System.Text.RegularExpressions;
    using UnityEngine;

    [ExecuteInEditMode]
    public class BocsCyclesLight : BocsCyclesGraphBase
    {
        public bool AutoSync = true;

        public float Size = .1f;

        public bool UseMis = true;
        public bool Diffuse = true;
        public bool Glossy = true;
        public bool Transmission = true;
        public bool Scatter = true;
        public bool Shadow = true;
        public float SpotAngle = 30;
        public int MaxBounces = 1024;
        public bool IsPortal = false;

        public string[] Type = new string[] { "point", "distant", "spot", "area" };
        public int TypeSelected = 1;

        //Sync Vars...
        private Vector3 lastPositon;

        private Quaternion lastRostation;
        private Vector3 lastScale;
        private Color lastColor;
        private LightType lastType;
        private float lastIntensity;
        private float lastRange;
        private float lastAngle;
        private LightShadows lastShadow;

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
            return hex;
        }

        public override string GetGraphName(int i)
        {
            return "Light";
        }

        public void Sync()
        {
            bool needUpdate = false;
            if (transform.position != lastPositon)
            {
                needUpdate = true;
            }
            if (transform.rotation != lastRostation)
            {
                needUpdate = true;
            }
            if (transform.lossyScale != lastScale)
            {
                needUpdate = true;
            }

            bool needReset = false;
            if (AutoSync)
            {
                Light l = GetComponent<Light>();
                if (lastColor != l.color) needReset = true;
                if (lastType != l.type) needReset = true;
                if (lastIntensity != l.intensity) needReset = true;
                if (lastRange != l.range) needReset = true;
                if (lastAngle != l.spotAngle) needReset = true;
                if (lastShadow != l.shadows) needReset = true;
            }
	        
	        NSync();
	        
            if (needReset)
            {
                Reset();
                return;
            }

            if (needUpdate) BocsCyclesAPI.UpdateObject(this.gameObject);
        }

        protected void Update()
        {
            Sync();
        }

        // Reset to default values.
        protected void Reset()
        {
            Light l = GetComponent<Light>();

            if (l.shadows == LightShadows.None)
                Shadow = false;
            else
                Shadow = true;

            if (l.type == LightType.Point)
            {
                ConvertPoint(l);
                TypeSelected = 0;
            }
            if (l.type == LightType.Directional)
            {
                ConvertGeneric(l);
                TypeSelected = 1;
            }
            if (l.type == LightType.Spot)
            {
                ConvertSpot(l);
                TypeSelected = 2;
            }
            if (l.type == LightType.Area)
            {
                ConvertGeneric(l);
                TypeSelected = 3;
            }

            //Debug.Log("Reset Light");
            NSync();
            BocsCyclesAPI.AddLight(this.gameObject);
        }

        //Dup object
        protected void Awake()
        {
            //Debug.Log("Awake Light");
            NSync();
            BocsCyclesAPI.AddLight(this.gameObject);
        }

        //Delete object
        protected void OnDestroy()
        {
            BocsCyclesAPI.ObjectDelete(this.gameObject);
        }

        private void NSync()
        {
            lastPositon = transform.position;
            lastRostation = transform.rotation;
            lastScale = transform.lossyScale;

            if (AutoSync)
            {
                Light l = GetComponent<Light>();
                lastColor = l.color;
                lastType = l.type;
                lastIntensity = l.intensity;
                lastRange = l.range;
                lastAngle = l.spotAngle;
                lastShadow = l.shadows;
            }
        }

        private void ConvertPoint(Light l)
        {
            float s = l.intensity * Mathf.Clamp(l.range, 0, 10);
            if (l.intensity < 1) s = l.intensity;

            if (l.shadows == LightShadows.Hard) Size = 0;
            if (l.shadows == LightShadows.Soft) Size = .03f;

            string n = string.Empty;
            n += @"
node|t=BocsNodeOutput,x=650,y=10,c=0:
node|t=BocsNodeEmission,x=350,y=10,c=0:
node|t=BocsNodeLightFalloff,x=10,y=10,c=0:
val|n=1,s=color,v=" + ColorToHex(l.color) + @":
val |n=1,s=strength,v=1:
val|n=2,s=strength,v=" + s + @":
val|n=2,s=smooth,v=0:
connect|n1=1,n2=0,s1=emission,s2=surface,:
connect|n1=2,n2=1,s1=quadratic,s2=strength,:";

            Nodes[0] = Regex.Replace(n, "[^a-zA-Z0-9_.:,|=+-;<>/| ]", string.Empty);
        }

        private void ConvertSpot(Light l)
        {
            float s = l.intensity * Mathf.Clamp(l.range, 0, 100);
            //if (l.intensity < 1) s = l.intensity * 10;

            SpotAngle = l.spotAngle;
            Size = .05f;

            string n = string.Empty;
            n += @"
node|t=BocsNodeOutput,x=650,y=10,c=0:
node|t=BocsNodeEmission,x=350,y=10,c=0:
node|t=BocsNodeLightFalloff,x=10,y=10,c=0:
val|n=1,s=color,v=" + ColorToHex(l.color) + @":
val |n=1,s=strength,v=1:
val|n=2,s=strength,v=" + s + @":
val|n=2,s=smooth,v=0:
connect|n1=1,n2=0,s1=emission,s2=surface,:
connect|n1=2,n2=1,s1=quadratic,s2=strength,:";

            Nodes[0] = Regex.Replace(n, "[^a-zA-Z0-9_.:,|=+-;<>/| ]", string.Empty);
        }

        private void ConvertGeneric(Light l)
        {
            if (l.shadows == LightShadows.Hard) Size = 0;
            if (l.shadows == LightShadows.Soft) Size = .03f;

            string n = string.Empty;
            n = "node|t=BocsNodeOutput,x=410,y=30:node|t=BocsNodeEmission,x=110,y=30:val|n=1,s=color,";
            n += "v=" + ColorToHex(l.color) + ":val|n=1,";
            n += "s=strength,v=" + l.intensity + ":connect|n1=1,n2=0,s1=emission,s2=surface,:";
            Nodes[0] = n;
        }
    }
}