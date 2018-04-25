namespace BOCSLIGHTMAPPER
{
    public class BocsNodeAttribute : BocsNodeBase
    {
        public BocsNodeAttribute()
        {
            //NodeType* type = NodeType::add("attribute", create, NodeType::SHADER);

            //SOCKET_STRING(attribute, "Attribute", ustring(""));

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_VECTOR(vector, "Vector");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Attribute";
            NodeName = "attribute";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotString(this, "Name", "attribute", BocsSlotBase.BocsSlotType.Value));
        }
    }
}