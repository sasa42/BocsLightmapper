namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeGamma : BocsNodeBase
    {
        public BocsNodeGamma()
        {
            //NodeType* type = NodeType::add("gamma", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_FLOAT(gamma, "Gamma", 1.0f);
            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Gamma";
            NodeName = "gamma";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.black));
            Slots.Add(new BocsSlotFloat(this, "Gamma", "gamma", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}