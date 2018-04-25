namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeEmission : BocsNodeBase
    {
        public BocsNodeEmission()
        {
            //NodeType* type = NodeType::add("emission", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_FLOAT(strength, "Strength", 10.0f);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(emission, "Emission");

            NodeTitle = "Emission";
            NodeName = "emission";

            Slots.Add(new BocsSlotClosure(this, "Emission", "emission", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Strength", "strength", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}