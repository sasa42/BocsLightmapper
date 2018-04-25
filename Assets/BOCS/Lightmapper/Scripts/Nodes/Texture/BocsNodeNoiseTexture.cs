namespace BOCSLIGHTMAPPER
{
    public class BocsNodeNoiseTexture : BocsNodeBase
    {
        public BocsNodeNoiseTexture()
        {
            //NodeType* type = NodeType::add("noise_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(NoiseTextureNode);

            //SOCKET_IN_FLOAT(scale, "Scale", 1.0f);
            //SOCKET_IN_FLOAT(detail, "Detail", 2.0f);
            //SOCKET_IN_FLOAT(distortion, "Distortion", 0.0f);
            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Noise Texture";
            NodeName = "noise_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Detail", "detail", BocsSlotBase.BocsSlotType.Input, 2));
            Slots.Add(new BocsSlotFloat(this, "Distortion", "distortion", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}