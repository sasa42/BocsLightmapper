namespace BOCSLIGHTMAPPER
{
    public class BocsNodeSeparateRGB : BocsNodeBase
    {
        public BocsNodeSeparateRGB()
        {
            //NodeType* type = NodeType::add("separate_rgb", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Image", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_FLOAT(r, "R");
            //SOCKET_OUT_FLOAT(g, "G");
            //SOCKET_OUT_FLOAT(b, "B");

            NodeTitle = "Separate RGB";
            NodeName = "separate_rgb";

            Slots.Add(new BocsSlotClosure(this, "R", "r", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "G", "g", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "B", "b", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Image", "color", BocsSlotBase.BocsSlotType.Input));
        }
    }
}