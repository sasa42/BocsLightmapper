namespace BOCSLIGHTMAPPER
{
    public class BocsNodeColor : BocsNodeBase
    {
        public BocsNodeColor()
        {
            //NodeType* type = NodeType::add("color", create, NodeType::SHADER);

            //SOCKET_COLOR(value, "Value", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "RGB";
            NodeName = "color";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotColor(this, "Value", "value", BocsSlotBase.BocsSlotType.Value));
        }
    }
}