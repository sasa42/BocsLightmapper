namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeTranslucentBsdf : BocsNodeBase
    {
        public BocsNodeTranslucentBsdf()
        {
            //NodeType* type = NodeType::add("translucent_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Translucent BSDF";
            NodeName = "translucent_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotClosure(this, "Normal", "Normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}