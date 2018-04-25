namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeTransparentBsdf : BocsNodeBase
    {
        public BocsNodeTransparentBsdf()
        {
            //NodeType* type = NodeType::add("transparent_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Transparent BSDF";
            NodeName = "transparent_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
        }
    }
}