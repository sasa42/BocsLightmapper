namespace BOCSLIGHTMAPPER
{
    public class BocsNodeValue : BocsNodeBase
    {
        public BocsNodeValue()
        {
            //NodeType* type = NodeType::add("value", create, NodeType::SHADER);

            //SOCKET_FLOAT(value, "Value", 0.0f);
            //SOCKET_OUT_FLOAT(value, "Value");

            NodeTitle = "Value";
            NodeName = "value";

            Slots.Add(new BocsSlotClosure(this, "Value", "value", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotFloat(this, "Value", "value", BocsSlotBase.BocsSlotType.Value));
        }
    }
}