namespace BOCSLIGHTMAPPER
{
    public class BocsNodeCombineHSV : BocsNodeBase
    {
        public BocsNodeCombineHSV()
        {
            //NodeType* type = NodeType::add("combine_hsv", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(h, "H", 0.0f);
            //SOCKET_IN_FLOAT(s, "S", 0.0f);
            //SOCKET_IN_FLOAT(v, "V", 0.0f);

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Combine HSV";
            NodeName = "combine_hsv";

            Slots.Add(new BocsSlotClosure(this, "Color", "Color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "H", "h", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "S", "s", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "V", "v", BocsSlotBase.BocsSlotType.Input));
        }
    }
}