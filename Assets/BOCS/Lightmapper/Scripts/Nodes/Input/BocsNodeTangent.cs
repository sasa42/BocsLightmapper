namespace BOCSLIGHTMAPPER
{
    public class BocsNodeTangent : BocsNodeBase
    {
        public BocsNodeTangent()
        {
            //NodeType* type = NodeType::add("tangent", create, NodeType::SHADER);

            //static NodeEnum direction_type_enum;
            //direction_type_enum.insert("radial", NODE_TANGENT_RADIAL);
            //direction_type_enum.insert("uv_map", NODE_TANGENT_UVMAP);
            //SOCKET_ENUM(direction_type, "Direction Type", direction_type_enum, NODE_TANGENT_RADIAL);

            //static NodeEnum axis_enum;
            //axis_enum.insert("x", NODE_TANGENT_AXIS_X);
            //axis_enum.insert("y", NODE_TANGENT_AXIS_Y);
            //axis_enum.insert("z", NODE_TANGENT_AXIS_Z);
            //SOCKET_ENUM(axis, "Axis", axis_enum, NODE_TANGENT_AXIS_X);

            //SOCKET_STRING(attribute, "Attribute", ustring(""));

            //SOCKET_IN_NORMAL(normal_osl, "NormalIn", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);
            //SOCKET_OUT_NORMAL(tangent, "Tangent");

            NodeTitle = "Tangent";
            NodeName = "tangent";

            Slots.Add(new BocsSlotClosure(this, "Tangent", "tangent", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "direction_type", "direction_type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "radial", "uv_map" };
            Slots.Add(bsl);

            BocsSlotStringList bsl2 = new BocsSlotStringList(this, "axis", "axis", BocsSlotBase.BocsSlotType.Value, 0);
            bsl2.List = new string[] { "x", "y", "z" };
            Slots.Add(bsl2);
        }
    }
}