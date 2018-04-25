namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeBrightContrast : BocsNodeBase
    {
        public BocsNodeBrightContrast()
        {
            //NodeType* type = NodeType::add("brightness_contrast", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_FLOAT(bright, "Bright", 0.0f);
            //SOCKET_IN_FLOAT(contrast, "Contrast", 0.0f);

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Bright Contrast";
            NodeName = "brightness_contrast";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Bright", "bright", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "Contrast", "contrast", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}