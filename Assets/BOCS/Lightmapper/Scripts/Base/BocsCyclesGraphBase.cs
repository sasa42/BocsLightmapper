// Header

namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    // Shaders are saved as simple strings
    public class BocsCyclesGraphBase : MonoBehaviour
    {
        public string[] Nodes = new string[32]; //If your model has more then 32 materials...fix it

        public virtual int GetGraphCount()
        {
            return 1;
        }

        public virtual string GetGraphName(int i)
        {
            return string.Empty;
        }

        public virtual string GetGraph(int i)
        {
            if (i > this.Nodes.Length) return string.Empty;
            return this.Nodes[i];
        }

        public virtual void SetGraph(int i, string g)
        {
            if (i > this.Nodes.Length) return;
            this.Nodes[i] = g;
        }
    }
}