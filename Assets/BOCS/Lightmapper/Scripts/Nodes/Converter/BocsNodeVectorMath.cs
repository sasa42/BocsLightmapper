namespace BOCSLIGHTMAPPER
{
    public class BocsNodeVectorMath : BocsNodeBase
    {
        public BocsNodeVectorMath()
        {
            //NodeType* type = NodeType::add("vector_math", create, NodeType::SHADER);

            //static NodeEnum type_enum;
            //type_enum.insert("add", NODE_VECTOR_MATH_ADD);
            //type_enum.insert("subtract", NODE_VECTOR_MATH_SUBTRACT);
            //type_enum.insert("average", NODE_VECTOR_MATH_AVERAGE);
            //type_enum.insert("dot_product", NODE_VECTOR_MATH_DOT_PRODUCT);
            //type_enum.insert("cross_product", NODE_VECTOR_MATH_CROSS_PRODUCT);
            //type_enum.insert("normalize", NODE_VECTOR_MATH_NORMALIZE);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_VECTOR_MATH_ADD);

            //SOCKET_IN_VECTOR(vector1, "Vector1", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_VECTOR(vector2, "Vector2", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_FLOAT(value, "Value");
            //SOCKET_OUT_VECTOR(vector, "Vector");

            NodeTitle = "Vector Math";
            NodeName = "vector_math";

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Value", "value", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "add", "subtract", "average", "dot_product", "cross_product", "normalize" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotVector3(this, "Vector", "vector1", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotVector3(this, "Vector", "vector2", BocsSlotBase.BocsSlotType.Input));
        }
    }
}