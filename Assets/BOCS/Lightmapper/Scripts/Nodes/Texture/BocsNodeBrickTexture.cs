namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeBrickTexture : BocsNodeBase
    {
        public BocsNodeBrickTexture()
        {
            //NodeType* type = NodeType::add("brick_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(BrickTextureNode);

            //SOCKET_FLOAT(offset, "Offset", 0.5f);
            //SOCKET_INT(offset_frequency, "Offset Frequency", 2);
            //SOCKET_FLOAT(squash, "Squash", 1.0f);
            //SOCKET_INT(squash_frequency, "Squash Frequency", 2);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_IN_COLOR(color1, "Color1", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_COLOR(color2, "Color2", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_COLOR(mortar, "Mortar", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_FLOAT(scale, "Scale", 5.0f);
            //SOCKET_IN_FLOAT(mortar_size, "Mortar Size", 0.02f);
            //SOCKET_IN_FLOAT(bias, "Bias", 0.0f);
            //SOCKET_IN_FLOAT(brick_width, "Brick Width", 0.5f);
            //SOCKET_IN_FLOAT(row_height, "Row Height", 0.25f);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Brick Texture";
            NodeName = "brick_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Offset", "offset", BocsSlotBase.BocsSlotType.Value, .5f));
            Slots.Add(new BocsSlotInt(this, "Frequency", "offset_frequency", BocsSlotBase.BocsSlotType.Value, 2));

            Slots.Add(new BocsSlotFloat(this, "Squash", "squash", BocsSlotBase.BocsSlotType.Value, 1));
            Slots.Add(new BocsSlotInt(this, "Frequency", "squash_frequency", BocsSlotBase.BocsSlotType.Value, 2));

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotColor(this, "Color1", "color1", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotColor(this, "Color2", "color2", BocsSlotBase.BocsSlotType.Input, Color.gray));
            Slots.Add(new BocsSlotColor(this, "Mortar", "mortar", BocsSlotBase.BocsSlotType.Input, Color.black));

            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 5));

            Slots.Add(new BocsSlotFloat(this, "Mortar Size", "mortar_size", BocsSlotBase.BocsSlotType.Input, .02f));
            Slots.Add(new BocsSlotFloat(this, "Bias", "bias", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "Brick Width", "brick_width", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotFloat(this, "Row Height", "row_height", BocsSlotBase.BocsSlotType.Input, .25f));
        }
    }
}