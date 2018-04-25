namespace BOCSLIGHTMAPPER
{
    public class BocsNodeEnviromentTexture : BocsNodeBase
    {
        public BocsNodeEnviromentTexture()
        {
            //NodeType* type = NodeType::add("environment_texture", create, NodeType::SHADER);

            //TEXTURE_MAPPING_DEFINE(EnvironmentTextureNode);

            //SOCKET_STRING(filename, "Filename", ustring(""));

            //static NodeEnum color_space_enum;
            //color_space_enum.insert("none", NODE_COLOR_SPACE_NONE);
            //color_space_enum.insert("color", NODE_COLOR_SPACE_COLOR);
            //SOCKET_ENUM(color_space, "Color Space", color_space_enum, NODE_COLOR_SPACE_COLOR);

            //SOCKET_BOOLEAN(use_alpha, "Use Alpha", true);

            //static NodeEnum interpolation_enum;
            //interpolation_enum.insert("closest", INTERPOLATION_CLOSEST);
            //interpolation_enum.insert("linear", INTERPOLATION_LINEAR);
            //interpolation_enum.insert("cubic", INTERPOLATION_CUBIC);
            //interpolation_enum.insert("smart", INTERPOLATION_SMART);
            //SOCKET_ENUM(interpolation, "Interpolation", interpolation_enum, INTERPOLATION_LINEAR);

            //static NodeEnum projection_enum;
            //projection_enum.insert("equirectangular", NODE_ENVIRONMENT_EQUIRECTANGULAR);
            //projection_enum.insert("mirror_ball", NODE_ENVIRONMENT_MIRROR_BALL);
            //SOCKET_ENUM(projection, "Projection", projection_enum, NODE_ENVIRONMENT_EQUIRECTANGULAR);

            //SOCKET_IN_POINT(vector, "Vector", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_POSITION);

            //SOCKET_OUT_COLOR(color, "Color");
            //SOCKET_OUT_FLOAT(alpha, "Alpha");

            NodeTitle = "Enviroment Texture";
            NodeName = "environment_texture";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Alpha", "alpha", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotTexture(this, "Image", "filename", BocsSlotBase.BocsSlotType.Value));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "color_space", "color_space", BocsSlotBase.BocsSlotType.Value, 1);
            bsl.List = new string[] { "none", "color" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotBool(this, "Use Alpha", "use_alpha", BocsSlotBase.BocsSlotType.Value, true));

            BocsSlotStringList bsl1 = new BocsSlotStringList(this, "interpolation", "interpolation", BocsSlotBase.BocsSlotType.Value, 1);
            bsl1.List = new string[] { "closest", "linear", "cubic", "smart" };
            Slots.Add(bsl1);

            BocsSlotStringList bsl2 = new BocsSlotStringList(this, "projection", "projection", BocsSlotBase.BocsSlotType.Value, 0);
            bsl2.List = new string[] { "equirectangular", "mirror_ball" };
            Slots.Add(bsl2);

            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
        }
    }
}