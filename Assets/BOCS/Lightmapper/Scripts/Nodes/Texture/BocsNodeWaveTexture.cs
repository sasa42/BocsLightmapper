namespace BOCSLIGHTMAPPER
{
    public class BocsNodeWaveTexture : BocsNodeBase
    {
        public BocsNodeWaveTexture()
        {
            //NodeType* type = NodeType::add("wave_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(WaveTextureNode);

            //static NodeEnum type_enum;
            //type_enum.insert("bands", NODE_WAVE_BANDS);
            //type_enum.insert("rings", NODE_WAVE_RINGS);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_WAVE_BANDS);

            //static NodeEnum profile_enum;
            //profile_enum.insert("sine", NODE_WAVE_PROFILE_SIN);
            //profile_enum.insert("saw", NODE_WAVE_PROFILE_SAW);
            //SOCKET_ENUM(profile, "Profile", profile_enum, NODE_WAVE_PROFILE_SIN);

            //SOCKET_IN_FLOAT(scale, "Scale", 1.0f);
            //SOCKET_IN_FLOAT(distortion, "Distortion", 0.0f);
            //SOCKET_IN_FLOAT(detail, "Detail", 2.0f);
            //SOCKET_IN_FLOAT(detail_scale, "Detail Scale", 0.0f);
            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Wave Texture";
            NodeName = "wave_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl1 = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl1.List = new string[] { "bands", "rings" };
            Slots.Add(bsl1);

            BocsSlotStringList bsl2 = new BocsSlotStringList(this, "profile", "profile", BocsSlotBase.BocsSlotType.Value, 0);
            bsl2.List = new string[] { "sine", "saw" };
            Slots.Add(bsl2);

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Distortion", "distortion", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "Detail", "detail", BocsSlotBase.BocsSlotType.Input, 2));
            Slots.Add(new BocsSlotFloat(this, "Detail Scale", "detail_scale", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}