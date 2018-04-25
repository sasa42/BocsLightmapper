namespace BOCSLIGHTMAPPER
{
    public class BocsNodeNormal : BocsNodeBase
    {
        public BocsNodeNormal()
        {
            //NodeType* type = NodeType::add("normal", create, NodeType::SHADER);

            //SOCKET_VECTOR(direction, "direction", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_NORMAL(normal, "Normal");
            //SOCKET_OUT_FLOAT(dot, "Dot");

            NodeTitle = "Normal";
            NodeName = "normal";

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Dot", "dot", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotVector3(this, "Direction", "direction", BocsSlotBase.BocsSlotType.Value));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}