namespace BOCSLIGHTMAPPER
{
    public class BocsNodeUVmap : BocsNodeBase
    {
        public BocsNodeUVmap()
        {
            //NodeType* type = NodeType::add("uvmap", create, NodeType::SHADER);

            //SOCKET_IN_STRING(attribute, "attribute", ustring(""));
            //SOCKET_IN_BOOLEAN(from_dupli, "from dupli", false);

            //SOCKET_OUT_POINT(UV, "UV");

            NodeTitle = "UV Map";
            NodeName = "uv_map";

            Slots.Add(new BocsSlotClosure(this, "UV", "uv", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotString(this, "attribute", "attribute", BocsSlotBase.BocsSlotType.Value));
        }
    }
}