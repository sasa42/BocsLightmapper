namespace BOCSLIGHTMAPPER
{
    public class BocsNodeHairInfo : BocsNodeBase
    {
        public BocsNodeHairInfo()
        {
            //NodeType* type = NodeType::add("hair_info", create, NodeType::SHADER);

            //SOCKET_OUT_FLOAT(is_strand, "Is Strand");
            //SOCKET_OUT_FLOAT(intercept, "Intercept");
            //SOCKET_OUT_FLOAT(thickness, "Thickness");
            //SOCKET_OUT_NORMAL(tangent Normal, "Tangent Normal");

            NodeTitle = "Hair Info";
            NodeName = "hair_info";

            Slots.Add(new BocsSlotClosure(this, "Is Strand", "is_strand", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Intercept", "intercept", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Thickness", "thickness", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Tangent Normal", "Normal", BocsSlotBase.BocsSlotType.Output));//error in cycles code?
        }
    }
}