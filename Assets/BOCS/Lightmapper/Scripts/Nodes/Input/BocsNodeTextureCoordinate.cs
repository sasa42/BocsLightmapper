namespace BOCSLIGHTMAPPER
{
    public class BocsNodeTextureCoordinate : BocsNodeBase
    {
        public BocsNodeTextureCoordinate()
        {
            //NodeType* type = NodeType::add("texture_coordinate", create, NodeType::SHADER);

            //SOCKET_BOOLEAN(from_dupli, "From Dupli", false);
            //SOCKET_BOOLEAN(use_transform, "Use Transform", false);
            //SOCKET_TRANSFORM(ob_tfm, "Object Transform", transform_identity());

            //SOCKET_IN_NORMAL(normal_osl, "NormalIn", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);

            //SOCKET_OUT_POINT(generated, "Generated");
            //SOCKET_OUT_NORMAL(normal, "Normal");
            //SOCKET_OUT_POINT(UV, "UV");
            //SOCKET_OUT_POINT(object, "Object");
            //SOCKET_OUT_POINT(camera, "Camera");
            //SOCKET_OUT_POINT(window, "Window");
            //SOCKET_OUT_NORMAL(reflection, "Reflection");

            NodeTitle = "Texture Coordinate";
            NodeName = "texture_coordinate";

            Slots.Add(new BocsSlotClosure(this, "Generated", "generated", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "UV", "UV", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Object", "object", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Camera", "camera", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Window", "window", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Reflection", "reflection", BocsSlotBase.BocsSlotType.Output));
        }
    }
}