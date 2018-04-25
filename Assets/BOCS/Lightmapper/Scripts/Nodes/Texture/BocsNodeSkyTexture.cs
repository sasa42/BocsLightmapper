namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeSkyTexture : BocsNodeBase
    {
        public BocsNodeSkyTexture()
        {
            //NodeType* type = NodeType::add("sky_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(SkyTextureNode);

            //static NodeEnum type_enum;
            //type_enum.insert("preetham", NODE_SKY_OLD);
            //type_enum.insert("hosek_wilkie", NODE_SKY_NEW);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_SKY_NEW);

            //SOCKET_VECTOR(sun_direction, "Sun Direction", make_float3(0.0f, 0.0f, 1.0f));
            //SOCKET_FLOAT(turbidity, "Turbidity", 2.2f);
            //SOCKET_FLOAT(ground_albedo, "Ground Albedo", 0.3f);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Sky Texture";
            NodeName = "sky_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotVector3(this, "Direction", "sun_direction", BocsSlotBase.BocsSlotType.Value, new Vector3(0, 0, 1)));
            Slots.Add(new BocsSlotFloat(this, "Turbidity", "turbidity", BocsSlotBase.BocsSlotType.Value, 2.2f));
            Slots.Add(new BocsSlotFloat(this, "Ground Albedo", "ground_albedo", BocsSlotBase.BocsSlotType.Value, .3f));

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
        }
    }
}