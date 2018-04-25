namespace BOCSLIGHTMAPPER
{
    public class BocsNodeHueSaturation : BocsNodeBase
    {
        public BocsNodeHueSaturation()
        {
            //NodeType* type = NodeType::add("hsv", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(hue, "Hue", 0.5f);
            //SOCKET_IN_FLOAT(saturation, "Saturation", 1.0f);
            //SOCKET_IN_FLOAT(value, "Value", 1.0f);
            //SOCKET_IN_FLOAT(fac, "Fac", 1.0f);
            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Hue Saturation";
            NodeName = "hsv";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Hue", "hue", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotFloat(this, "Saturation", "saturation", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Value", "value", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Input, 1));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input));
        }
    }
}