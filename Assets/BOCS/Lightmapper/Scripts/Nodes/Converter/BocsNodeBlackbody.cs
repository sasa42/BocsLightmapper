namespace BOCSLIGHTMAPPER
{
    public class BocsNodeBlackbody : BocsNodeBase
    {
        public BocsNodeBlackbody()
        {
            //NodeType* type = NodeType::add("blackbody", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(temperature, "Temperature", 1200.0f);
            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Blackbody";
            NodeName = "blackbody";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Temperature", "temperature", BocsSlotBase.BocsSlotType.Input, 1200));
        }
    }
}