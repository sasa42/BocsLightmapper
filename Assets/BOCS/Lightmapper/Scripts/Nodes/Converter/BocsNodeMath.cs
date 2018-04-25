namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMath : BocsNodeBase
    {
        public BocsNodeMath()
        {
            //NodeType* type = NodeType::add("math", create, NodeType::SHADER);

            //static NodeEnum type_enum;
            //type_enum.insert("add", NODE_MATH_ADD);
            //type_enum.insert("subtract", NODE_MATH_SUBTRACT);
            //type_enum.insert("multiply", NODE_MATH_MULTIPLY);
            //type_enum.insert("divide", NODE_MATH_DIVIDE);
            //type_enum.insert("sine", NODE_MATH_SINE);
            //type_enum.insert("cosine", NODE_MATH_COSINE);
            //type_enum.insert("tangent", NODE_MATH_TANGENT);
            //type_enum.insert("arcsine", NODE_MATH_ARCSINE);
            //type_enum.insert("arccosine", NODE_MATH_ARCCOSINE);
            //type_enum.insert("arctangent", NODE_MATH_ARCTANGENT);
            //type_enum.insert("power", NODE_MATH_POWER);
            //type_enum.insert("logarithm", NODE_MATH_LOGARITHM);
            //type_enum.insert("minimum", NODE_MATH_MINIMUM);
            //type_enum.insert("maximum", NODE_MATH_MAXIMUM);
            //type_enum.insert("round", NODE_MATH_ROUND);
            //type_enum.insert("less_than", NODE_MATH_LESS_THAN);
            //type_enum.insert("greater_than", NODE_MATH_GREATER_THAN);
            //type_enum.insert("modulo", NODE_MATH_MODULO);
            //type_enum.insert("absolute", NODE_MATH_ABSOLUTE);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_MATH_ADD);

            //SOCKET_BOOLEAN(use_clamp, "Use Clamp", false);

            //SOCKET_IN_FLOAT(value1, "Value1", 0.0f);
            //SOCKET_IN_FLOAT(value2, "Value2", 0.0f);

            //SOCKET_OUT_FLOAT(value, "Value");

            NodeTitle = "Math";
            NodeName = "math";

            Slots.Add(new BocsSlotClosure(this, "Value", "value", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "add", "subtract", "multiply", "divide", "sine", "cosine", "tangent", "arcsine", "arccosine", "arctangent", "power", "logarithm", "minimum", "maximum", "round", "less_than", "greater_than", "modulo", "absolute" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotBool(this, "Clamp", "use_clamp", BocsSlotBase.BocsSlotType.Value, false));

            Slots.Add(new BocsSlotFloat(this, "Value", "value1", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "Value", "value2", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}