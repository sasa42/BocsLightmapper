namespace BOCSLIGHTMAPPER
{
    public class BocsNodeSeparateXYZ : BocsNodeBase
    {
        public BocsNodeSeparateXYZ()
        {
            //NodeType* type = NodeType::add("separate_xyz", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_FLOAT(x, "X");
            //SOCKET_OUT_FLOAT(y, "Y");
            //SOCKET_OUT_FLOAT(z, "Z");

            NodeTitle = "Separate XYZ";
            NodeName = "separate_xyz";

            Slots.Add(new BocsSlotFloat(this, "X", "x", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotFloat(this, "Y", "y", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotFloat(this, "Z", "z", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotVector3(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
        }
    }
}