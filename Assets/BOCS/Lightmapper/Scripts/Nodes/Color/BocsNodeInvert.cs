namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeInvert : BocsNodeBase
    {
        public BocsNodeInvert()
        {
            //NodeType* type = NodeType::add("invert", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(fac, "Fac", 1.0f);
            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Invert";
            NodeName = "invert";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.black));
        }
    }
}