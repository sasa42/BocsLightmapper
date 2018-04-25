namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeCheckerTexture : BocsNodeBase
    {
        public BocsNodeCheckerTexture()
        {
            //NodeType* type = NodeType::add("checker_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(CheckerTextureNode);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TEXTURE_GENERATED);
            //SOCKET_IN_COLOR(color1, "Color1", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_COLOR(color2, "Color2", make_float3(0.0f, 0.0f, 0.0f));
            //SOCKET_IN_FLOAT(scale, "Scale", 1.0f);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Checker Texture";
            NodeName = "checker_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotColor(this, "Color1", "color1", BocsSlotBase.BocsSlotType.Input, Color.black));
            Slots.Add(new BocsSlotColor(this, "Color2", "color2", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
        }
    }
}