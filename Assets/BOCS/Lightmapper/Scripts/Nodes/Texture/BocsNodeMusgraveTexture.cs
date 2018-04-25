namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMusgraveTexture : BocsNodeBase
    {
        public BocsNodeMusgraveTexture()
        {
            //NodeType* type = NodeType::add("musgrave_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(MusgraveTextureNode);

            //static NodeEnum type_enum;
            //type_enum.insert("multifractal", NODE_MUSGRAVE_MULTIFRACTAL);
            //type_enum.insert("fBM", NODE_MUSGRAVE_FBM);
            //type_enum.insert("hybrid_multifractal", NODE_MUSGRAVE_HYBRID_MULTIFRACTAL);
            //type_enum.insert("ridged_multifractal", NODE_MUSGRAVE_RIDGED_MULTIFRACTAL);
            //type_enum.insert("hetero_terrain", NODE_MUSGRAVE_HETERO_TERRAIN);
            //SOCKET_ENUM(type, "Type", type_enum, NODE_MUSGRAVE_FBM);

            //SOCKET_IN_FLOAT(scale, "Scale", 1.0f);
            //SOCKET_IN_FLOAT(detail, "Detail", 2.0f);
            //SOCKET_IN_FLOAT(dimension, "Dimension", 2.0f);
            //SOCKET_IN_FLOAT(lacunarity, "Lacunarity", 1.0f);
            //SOCKET_IN_FLOAT(offset, "Offset", 0.0f);
            //SOCKET_IN_FLOAT(gain, "Gain", 1.0f);
            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Musgrave Texture";
            NodeName = "musgrave_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "type", "type", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "fBM", "Multifractal", "hybrid_multifractal", "ridged_multifractal", "hetero_terrain" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotClosure(this, "Vector", "Vector", BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Detail", "detail", BocsSlotBase.BocsSlotType.Input, 2));
            Slots.Add(new BocsSlotFloat(this, "Dimension", "dimension", BocsSlotBase.BocsSlotType.Input, 2));
            Slots.Add(new BocsSlotFloat(this, "Lacunarity", "lacunarity", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Offset", "offset", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "Gain", "gain", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}