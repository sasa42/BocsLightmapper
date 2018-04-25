namespace BOCSLIGHTMAPPER
{
    public class BocsNodeFresnel : BocsNodeBase
    {
        public BocsNodeFresnel()
        {
            //NodeType* type = NodeType::add("fresnel", create, NodeType::SHADER);

            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);
            //SOCKET_IN_FLOAT(IOR, "IOR", 1.45f);

            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Fresnel";
            NodeName = "fresnel";

            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "IOR", "IOR", BocsSlotBase.BocsSlotType.Input, 1.45f));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}