namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMagicTexture : BocsNodeBase
    {
        public BocsNodeMagicTexture()
        {
            //NodeType* type = NodeType::add("magic_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(MagicTextureNode);

            //SOCKET_INT(depth, "Depth", 2);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);
            //SOCKET_IN_FLOAT(scale, "Scale", 5.0f);
            //SOCKET_IN_FLOAT(distortion, "Distortion", 1.0f);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Magic Texture";
            NodeName = "magic_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotInt(this, "Depth", "depth", BocsSlotBase.BocsSlotType.Value, 2));

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 5));
            Slots.Add(new BocsSlotFloat(this, "Distortion", "distortion", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}