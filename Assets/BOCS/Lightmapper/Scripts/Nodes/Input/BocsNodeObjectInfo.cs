namespace BOCSLIGHTMAPPER
{
    public class BocsNodeObjectInfo : BocsNodeBase
    {
        public BocsNodeObjectInfo()
        {
            //NodeType* type = NodeType::add("object_info", create, NodeType::SHADER);

            //SOCKET_OUT_VECTOR(location, "Location");
            //SOCKET_OUT_FLOAT(object_index, "Object Index");
            //SOCKET_OUT_FLOAT(material_index, "Material Index");
            //SOCKET_OUT_FLOAT(random, "Random");

            NodeTitle = "Object Info";
            NodeName = "object_info";

            Slots.Add(new BocsSlotClosure(this, "Location", "location", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Object Index", "object_index", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Material Index", "material_index", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Random", "random", BocsSlotBase.BocsSlotType.Output));
        }
    }
}