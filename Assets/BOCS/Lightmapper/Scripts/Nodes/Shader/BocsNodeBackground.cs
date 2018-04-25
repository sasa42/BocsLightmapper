namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeBackground : BocsNodeBase
    {
        public BocsNodeBackground()
        {
            //NodeType* type = NodeType::add("background_shader", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_FLOAT(strength, "Strength", 1.0f);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(background, "Background");

            NodeTitle = "Background";
            NodeName = "background";

            Slots.Add(new BocsSlotClosure(this, "Background", "background", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Strength", "strength", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}