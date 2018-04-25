namespace BOCSLIGHTMAPPER
{
    public class BocsNodeLayerWeight : BocsNodeBase
    {
        public BocsNodeLayerWeight()
        {
            //NodeType* type = NodeType::add("layer_weight", create, NodeType::SHADER);

            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);
            //SOCKET_IN_FLOAT(blend, "Blend", 0.5f);

            //SOCKET_OUT_FLOAT(fresnel, "Fresnel");
            //SOCKET_OUT_FLOAT(facing, "Facing");

            NodeTitle = "Layer Weight";
            NodeName = "layer_weight";

            Slots.Add(new BocsSlotClosure(this, "Fresnel", "fresnel", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Facing", "facing", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Blend", "blend", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}