namespace BOCSLIGHTMAPPER
{
    public class BocsNodeSeparateHSV : BocsNodeBase
    {
        public BocsNodeSeparateHSV()
        {
            //NodeType* type = NodeType::add("separate_hsv", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_FLOAT(h, "H");
            //SOCKET_OUT_FLOAT(s, "S");
            //SOCKET_OUT_FLOAT(v, "V");

            NodeTitle = "Separate HSV";
            NodeName = "separate_hsv";

            Slots.Add(new BocsSlotFloat(this, "H", "h", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotFloat(this, "S", "s", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotFloat(this, "V", "v", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input));
        }
    }
}