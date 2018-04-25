namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMapping : BocsNodeBase
    {
        public BocsNodeMapping()
        {
            //NodeType* type = NodeType::add("mapping", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(MappingNode);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_OUT_POINT(vector, "Vector");

            //SOCKET_POINT(tex_mapping.translation, "Translation", make_float3(0.0f, 0.0f, 0.0f)); \
            //SOCKET_VECTOR(tex_mapping.rotation, "Rotation", make_float3(0.0f, 0.0f, 0.0f));      \
            //SOCKET_VECTOR(tex_mapping.scale, "Scale", make_float3(1.0f, 1.0f, 1.0f));            \
            //\
            //SOCKET_VECTOR(tex_mapping.min, "Min", make_float3(-FLT_MAX, -FLT_MAX, -FLT_MAX)); \
            //SOCKET_VECTOR(tex_mapping.max, "Max", make_float3(FLT_MAX, FLT_MAX, FLT_MAX));    \
            //SOCKET_BOOLEAN(tex_mapping.use_minmax, "Use Min Max", false);                     \
            //\
            //static NodeEnum mapping_axis_enum;                      \
            //mapping_axis_enum.insert("none", TextureMapping::NONE); \
            //mapping_axis_enum.insert("x", TextureMapping::X);       \
            //mapping_axis_enum.insert("y", TextureMapping::Y);       \
            //mapping_axis_enum.insert("z", TextureMapping::Z);       \
            //SOCKET_ENUM(tex_mapping.x_mapping, "x_mapping", mapping_axis_enum, TextureMapping::X); \
            //SOCKET_ENUM(tex_mapping.y_mapping, "y_mapping", mapping_axis_enum, TextureMapping::Y); \
            //SOCKET_ENUM(tex_mapping.z_mapping, "z_mapping", mapping_axis_enum, TextureMapping::Z); \
            //\
            //static NodeEnum mapping_type_enum;                            \
            //mapping_type_enum.insert("point", TextureMapping::POINT);     \
            //mapping_type_enum.insert("texture", TextureMapping::TEXTURE); \
            //mapping_type_enum.insert("vector", TextureMapping::VECTOR);   \
            //mapping_type_enum.insert("normal", TextureMapping::NORMAL);   \
            //SOCKET_ENUM(tex_mapping.type, "Type", mapping_type_enum, TextureMapping::TEXTURE); \
            //\
            //static NodeEnum mapping_projection_enum;                                                \
            //mapping_projection_enum.insert("flat", TextureMapping::FLAT);                           \
            //mapping_projection_enum.insert("cube", TextureMapping::CUBE);                           \
            //mapping_projection_enum.insert("tube", TextureMapping::TUBE);                           \
            //mapping_projection_enum.insert("sphere", TextureMapping::SPHERE);                       \
            //SOCKET_ENUM(tex_mapping.projection, "Projection", mapping_projection_enum, TextureMapping::FLAT);

            NodeTitle = "Mapping";
            NodeName = "mapping";

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "tex_mapping.type", "tex_mapping.type", BocsSlotBase.BocsSlotType.Value, 1);
            bsl.List = new string[] { "texture", "point", "vector", "normal" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotVector3(this, "Translation", "tex_mapping.translation", BocsSlotBase.BocsSlotType.Value));
            Slots.Add(new BocsSlotVector3(this, "Rotation", "tex_mapping.rotation", BocsSlotBase.BocsSlotType.Value));
            Slots.Add(new BocsSlotVector3(this, "Scale", "tex_mapping.scale", BocsSlotBase.BocsSlotType.Value));

            //BocsSlotStringList bsl1 = new BocsSlotStringList(this, "convert_from", "convert_from", BocsSlotBase.BocsSlotType.Value, 0);
            //bsl1._list = new string[] { "world", "object", "camera" };
            //_slots.Add(bsl1);

            //BocsSlotStringList bsl2 = new BocsSlotStringList(this, "convert_to", "convert_to", BocsSlotBase.BocsSlotType.Value, 0);
            //bsl2._list = new string[] { "object", "world", "camera" };
            //_slots.Add(bsl2);

            Slots.Add(new BocsSlotVector3(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));

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
        }
    }
}