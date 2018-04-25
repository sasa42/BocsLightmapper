namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeVelvetBsdf : BocsNodeBase
    {
        public BocsNodeVelvetBsdf()
        {
            //NodeType* type = NodeType::add("velvet_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);
            //SOCKET_IN_FLOAT(sigma, "Sigma", 1.0f);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Velvet BSDF";
            NodeName = "velvet_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Sigma", "sigma", BocsSlotBase.BocsSlotType.Input, 1));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}