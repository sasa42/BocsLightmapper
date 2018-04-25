namespace BOCSLIGHTMAPPER
{
    public class BocsNodeCombineRGB : BocsNodeBase
    {
        public BocsNodeCombineRGB()
        {
            //NodeType* type = NodeType::add("combine_rgb", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(r, "R", 0.0f);
            //SOCKET_IN_FLOAT(g, "G", 0.0f);
            //SOCKET_IN_FLOAT(b, "B", 0.0f);

            //SOCKET_OUT_COLOR(image, "Image");

            NodeTitle = "Combine RGB";
            NodeName = "combine_rgb";

            Slots.Add(new BocsSlotClosure(this, "Image", "image", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "R", "r", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "G", "g", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "B", "b", BocsSlotBase.BocsSlotType.Input));
        }
    }
}