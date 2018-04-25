namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeAmbientOcclusion : BocsNodeBase
    {
        public BocsNodeAmbientOcclusion()
        {
            //NodeType* type = NodeType::add("ambient_occlusion", create, NodeType::SHADER);

            //SOCKET_IN_NORMAL(normal_osl, "NormalIn", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);
            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(AO, "AO");

            NodeTitle = "Ambient Occlusion";
            NodeName = "ambient_occlusion";

            Slots.Add(new BocsSlotClosure(this, "AO", "AO", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
        }
    }
}