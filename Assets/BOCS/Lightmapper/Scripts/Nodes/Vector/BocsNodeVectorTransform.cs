namespace BOCSLIGHTMAPPER
{
    public class BocsNodeVectorTransform : BocsNodeBase
    {
        public BocsNodeVectorTransform()
        {
            //NodeType* type = NodeType::add("vector_transform", create, NodeType::SHADER);

            //static NodeEnum type_enum;
            //type_enum.insert("vector", NODE_VECTOR_TRANSFORM_TYPE_VECTOR);
            //type_enum.insert("point", NODE_VECTOR_TRANSFORM_TYPE_POINT);
            //type_enum.insert("normal", NODE_VECTOR_TRANSFORM_TYPE_NORMAL);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_VECTOR_TRANSFORM_TYPE_VECTOR);

            //static NodeEnum space_enum;
            //space_enum.insert("world", NODE_VECTOR_TRANSFORM_CONVERT_SPACE_WORLD);
            //space_enum.insert("object", NODE_VECTOR_TRANSFORM_CONVERT_SPACE_OBJECT);
            //space_enum.insert("camera", NODE_VECTOR_TRANSFORM_CONVERT_SPACE_CAMERA);
            //SOCKET_ENUM(convert_from, "Convert From", space_enum, NODE_VECTOR_TRANSFORM_CONVERT_SPACE_WORLD);
            //SOCKET_ENUM(convert_to, "Convert To", space_enum, NODE_VECTOR_TRANSFORM_CONVERT_SPACE_OBJECT);

            //SOCKET_IN_VECTOR(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_OUT_VECTOR(vector, "Vector");

            NodeTitle = "Vector Transform";
            NodeName = "vector_transform";

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "vector", "point", "normal" };
            Slots.Add(bsl);

            BocsSlotStringList bsl1 = new BocsSlotStringList(this, "convert_from", "convert_from", BocsSlotBase.BocsSlotType.Value, 0);
            bsl1.List = new string[] { "world", "object", "camera" };
            Slots.Add(bsl1);

            BocsSlotStringList bsl2 = new BocsSlotStringList(this, "convert_to", "convert_to", BocsSlotBase.BocsSlotType.Value, 0);
            bsl2.List = new string[] { "object", "world", "camera" };
            Slots.Add(bsl2);

            Slots.Add(new BocsSlotVector3(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
        }
    }
}