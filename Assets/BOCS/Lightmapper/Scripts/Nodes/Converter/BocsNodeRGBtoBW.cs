namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeRGBtoBW : BocsNodeBase
    {
        public BocsNodeRGBtoBW()
        {
            //NodeType* type = NodeType::add("rgb_to_bw", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_OUT_FLOAT(val, "Val");

            NodeTitle = "RGB to BW";
            NodeName = "rgb_to_bw";

            Slots.Add(new BocsSlotClosure(this, "Val", "val", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.black));
        }
    }
}