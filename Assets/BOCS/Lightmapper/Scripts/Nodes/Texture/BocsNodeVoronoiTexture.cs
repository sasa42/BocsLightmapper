namespace BOCSLIGHTMAPPER
{
    public class BocsNodeVoronoiTexture : BocsNodeBase
    {
        public BocsNodeVoronoiTexture()
        {
            //NodeType* type = NodeType::add("voronoi_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(VoronoiTextureNode);

            //static NodeEnum coloring_enum;
            //coloring_enum.insert("intensity", NODE_VORONOI_INTENSITY);
            //coloring_enum.insert("cells", NODE_VORONOI_CELLS);
            //SOCKET_ENUM(coloring, "Coloring", coloring_enum, NODE_VORONOI_INTENSITY);

            //SOCKET_IN_FLOAT(scale, "Scale", 1.0f);
            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Voronoi Texture";
            NodeName = "voronoi_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "coloring", "coloring", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "intensity", "cells" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}