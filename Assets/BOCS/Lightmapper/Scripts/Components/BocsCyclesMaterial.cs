namespace BOCSLIGHTMAPPER
{
    using System;
    using System.Text.RegularExpressions;
    using UnityEngine;

    [ExecuteInEditMode]
    public class BocsCyclesMaterial : BocsCyclesGraphBase
    {
        public Material[] Smats = null;
        public Mesh Mesh = null;
        public PathRayFlag Visibility = PathRayFlag.PATH_RAY_CAMERA | PathRayFlag.PATH_RAY_REFLECT | PathRayFlag.PATH_RAY_TRANSMIT | PathRayFlag.PATH_RAY_DIFFUSE | PathRayFlag.PATH_RAY_DIFFUSE | PathRayFlag.PATH_RAY_GLOSSY | PathRayFlag.PATH_RAY_SINGULAR | PathRayFlag.PATH_RAY_TRANSPARENT | PathRayFlag.PATH_RAY_SHADOW_OPAQUE | PathRayFlag.PATH_RAY_SHADOW_TRANSPARENT | PathRayFlag.PATH_RAY_CURVE | PathRayFlag.PATH_RAY_VOLUME_SCATTER;

        //Vars to keep cycles in sync..

        private Vector3 lastPositon;
        private Quaternion lastRostation;
        private Vector3 lastScale;

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

        public override int GetGraphCount()
        {
            UpdateMaterials();
            if (Smats == null) return 0;
            return Smats.Length;
        }

        public override string GetGraphName(int i)
        {
            UpdateMaterials();
            if (Smats == null) return string.Empty;
            if (i >= Smats.Length) return string.Empty;
            if (Smats[i] == null) return string.Empty;
            return Smats[i].name;
        }

        public void NSync()
        {
            lastPositon = transform.position;
            lastRostation = transform.rotation;
            lastScale = transform.lossyScale;
        }

        public void Sync()
        {
            bool needUpdate = false;
            if (transform.position != lastPositon)
            {
                lastPositon = transform.position;
                needUpdate = true;
            }
            if (transform.rotation != lastRostation)
            {
                lastRostation = transform.rotation;
                needUpdate = true;
            }
            if (transform.lossyScale != lastScale)
            {
                lastScale = transform.lossyScale;
                needUpdate = true;
            }

            if (needUpdate) BocsCyclesAPI.UpdateObject(this.gameObject);
        }

        // Reset to default values.
        public void Reset()
        {
            UpdateMaterials();

            for (int i = 0; i < GetGraphCount(); i++)
            {
                string m = BocsCyclesShaderConvert.Convert(Smats[i]);
                m = Regex.Replace(m, "[^a-zA-Z0-9_.:,|=+-;<>/| ]", string.Empty);
                Nodes[i] = m;
            }

            NSync();
            BocsCyclesAPI.ObjectDelete(this.gameObject);
            BocsCyclesAPI.AddMesh(this.gameObject);
        }

        protected void Update()
        {
            Sync();
        }
        //Dup object
        protected void Awake()
        {
            NSync();
            BocsCyclesAPI.AddMesh(this.gameObject);
        }

        //Delete object
        protected void OnDestroy()
        {
            BocsCyclesAPI.ObjectDelete(this.gameObject);
        }

        private void UpdateMaterials()
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
            if (mf == null && smr == null) return;

            if (mf)
            {
                Mesh = mf.sharedMesh;
                MeshRenderer mr = GetComponent<MeshRenderer>();
                if (mr) Smats = mr.sharedMaterials;
            }
            if (smr)
            {
                Mesh = smr.sharedMesh;
                Smats = smr.sharedMaterials;
            }
        }
    }
}