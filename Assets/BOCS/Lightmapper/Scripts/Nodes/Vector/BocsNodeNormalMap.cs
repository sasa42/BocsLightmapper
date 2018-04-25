namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeNormalMap : BocsNodeBase
    {
        public BocsNodeNormalMap()
        {
            //NodeType* type = NodeType::add("normal_map", create, NodeType::SHADER);

            //static NodeEnum space_enum;
            //space_enum.insert("tangent", NODE_NORMAL_MAP_TANGENT);
            //space_enum.insert("object", NODE_NORMAL_MAP_OBJECT);
            //space_enum.insert("world", NODE_NORMAL_MAP_WORLD);
            //space_enum.insert("blender_object", NODE_NORMAL_MAP_BLENDER_OBJECT);
            //space_enum.insert("blender_world", NODE_NORMAL_MAP_BLENDER_WORLD);
            //SOCKET_ENUM(space, "Space", space_enum, NODE_TANGENT_RADIAL);

            //SOCKET_STRING(attribute, "Attribute", ustring(""));

            //SOCKET_IN_NORMAL(normal_osl, "NormalIn", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);
            //SOCKET_IN_FLOAT(strength, "Strength", 1.0f);
            //SOCKET_IN_COLOR(color, "Color", make_float3(0.5f, 0.5f, 1.0f));

            //SOCKET_OUT_NORMAL(normal, "Normal");

            NodeTitle = "Normal Map";
            NodeName = "normal_map";

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "space", "space", BocsSlotBase.BocsSlotType.Value, 1);
            bsl.List = new string[] { "object", "tangent", "world", "blender_object", "blender_world" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotString(this, "attribute", "attribute", BocsSlotBase.BocsSlotType.Value));

            Slots.Add(new BocsSlotFloat(this, "Strength", "strength", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.gray));
        }
    }
}