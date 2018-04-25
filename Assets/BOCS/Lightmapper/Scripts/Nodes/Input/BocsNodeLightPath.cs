namespace BOCSLIGHTMAPPER
{
    public class BocsNodeLightPath : BocsNodeBase
    {
        public BocsNodeLightPath()
        {
            //NodeType* type = NodeType::add("light_path", create, NodeType::SHADER);

            //SOCKET_OUT_FLOAT(is_camera_ray, "Is Camera Ray");
            //SOCKET_OUT_FLOAT(is_shadow_ray, "Is Shadow Ray");
            //SOCKET_OUT_FLOAT(is_diffuse_ray, "Is Diffuse Ray");
            //SOCKET_OUT_FLOAT(is_glossy_ray, "Is Glossy Ray");
            //SOCKET_OUT_FLOAT(is_singular_ray, "Is Singular Ray");
            //SOCKET_OUT_FLOAT(is_reflection_ray, "Is Reflection Ray");
            //SOCKET_OUT_FLOAT(is_transmission_ray, "Is Transmission Ray");
            //SOCKET_OUT_FLOAT(is_volume_scatter_ray, "Is Volume Scatter Ray");
            //SOCKET_OUT_FLOAT(ray_length, "Ray Length");
            //SOCKET_OUT_FLOAT(ray_depth, "Ray Depth");
            //SOCKET_OUT_FLOAT(transparent_depth, "Transparent Depth");
            //SOCKET_OUT_FLOAT(transmission_depth, "Transmission Depth");

            NodeTitle = "Light Path";
            NodeName = "light_path";

            Slots.Add(new BocsSlotClosure(this, "Is Camera Ray", "is_camera_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Shadow Ray", "is_shadow_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Diffuse Ray", "is_diffuse_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Glossy Ray", "is_glossy_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Singular Ray", "is_singular_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Reflection Ray", "is_reflection_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Transmission Ray", "is_transmission_ray", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Is Volume Scatter Ray", "is_volume_scatter_ray", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotClosure(this, "Ray Length", "ray_length", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Ray Depth", "ray_depth", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotClosure(this, "Transparent Depth", "transparent_depth", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Transmission Depth", "transmission_depth", BocsSlotBase.BocsSlotType.Output));
        }
    }
}