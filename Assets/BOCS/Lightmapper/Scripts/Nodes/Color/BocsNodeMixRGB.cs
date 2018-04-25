namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMixRGB : BocsNodeBase
    {
        public BocsNodeMixRGB()
        {
            //NodeType* type = NodeType::add("mix", create, NodeType::SHADER);

            //static NodeEnum type_enum;
            //type_enum.insert("mix", NODE_MIX_BLEND);
            //type_enum.insert("add", NODE_MIX_ADD);
            //type_enum.insert("multiply", NODE_MIX_MUL);
            //type_enum.insert("screen", NODE_MIX_SCREEN);
            //type_enum.insert("overlay", NODE_MIX_OVERLAY);
            //type_enum.insert("subtract", NODE_MIX_SUB);
            //type_enum.insert("divide", NODE_MIX_DIV);
            //type_enum.insert("difference", NODE_MIX_DIFF);
            //type_enum.insert("darken", NODE_MIX_DARK);
            //type_enum.insert("lighten", NODE_MIX_LIGHT);
            //type_enum.insert("dodge", NODE_MIX_DODGE);
            //type_enum.insert("burn", NODE_MIX_BURN);
            //type_enum.insert("hue", NODE_MIX_HUE);
            //type_enum.insert("saturation", NODE_MIX_SAT);
            //type_enum.insert("value", NODE_MIX_VAL);
            //type_enum.insert("color", NODE_MIX_COLOR);
            //type_enum.insert("soft_light", NODE_MIX_SOFT);
            //type_enum.insert("linear_light", NODE_MIX_LINEAR);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_MIX_BLEND);

            //SOCKET_BOOLEAN(use_clamp, "Use Clamp", false);

            //SOCKET_IN_FLOAT(fac, "Fac", 0.5f);
            //SOCKET_IN_COLOR(color1, "Color1", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_COLOR(color2, "Color2", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Mix";
            NodeName = "mix";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "mix", "add", "multiply", "screen", "overlay", "subtract", "divide", "difference", "darken", "lighten", "dodge", "burn", "hue", "saturation", "value", "color", "soft_light", "linear_light" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotBool(this, "Clamp", "use_clamp", BocsSlotBase.BocsSlotType.Value, false));

            Slots.Add(new BocsSlotFloat(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotColor(this, "Color1", "color1", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotColor(this, "Color2", "color2", BocsSlotBase.BocsSlotType.Input));
        }
    }
}