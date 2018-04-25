namespace BOCSLIGHTMAPPER
{
    public class BocsNodeCombineXYZ : BocsNodeBase
    {
        public BocsNodeCombineXYZ()
        {
            //NodeType* type = NodeType::add("combine_xyz", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(x, "X", 0.0f);
            //SOCKET_IN_FLOAT(y, "Y", 0.0f);
            //SOCKET_IN_FLOAT(z, "Z", 0.0f);

            //SOCKET_OUT_VECTOR(vector, "Vector");

            NodeTitle = "Combine XYZ";
            NodeName = "combine_xyz";

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "X", "x", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "Y", "y", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "Z", "z", BocsSlotBase.BocsSlotType.Input));
        }
    }
}