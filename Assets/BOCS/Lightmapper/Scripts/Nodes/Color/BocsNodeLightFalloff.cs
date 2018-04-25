namespace BOCSLIGHTMAPPER
{
    public class BocsNodeLightFalloff : BocsNodeBase
    {
        public BocsNodeLightFalloff()
        {
            //NodeType* type = NodeType::add("light_fallof", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(strength, "Strength", 100.0f);
            //SOCKET_IN_FLOAT(smooth, "Smooth", 0.0f);

            //SOCKET_OUT_FLOAT(quadratic, "Quadratic");
            //SOCKET_OUT_FLOAT(linear, "Linear");
            //SOCKET_OUT_FLOAT(constant, "Constant");

            NodeTitle = "Light Falloff";
            NodeName = "light_fallof";//Yes cycles bug

            Slots.Add(new BocsSlotClosure(this, "Quadratic", "quadratic", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Linear", "linear", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Constant", "constant", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Strength", "strength", BocsSlotBase.BocsSlotType.Input, 100));
            Slots.Add(new BocsSlotFloat(this, "Smooth", "smooth", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}