namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeSubsurfaceScattering : BocsNodeBase
    {
        public BocsNodeSubsurfaceScattering()
        {
            //NodeType* type = NodeType::add("subsurface_scattering", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum falloff_enum;
            //falloff_enum.insert("cubic", CLOSURE_BSSRDF_CUBIC_ID);
            //falloff_enum.insert("gaussian", CLOSURE_BSSRDF_GAUSSIAN_ID);
            //falloff_enum.insert("burley", CLOSURE_BSSRDF_BURLEY_ID);
            //SOCKET_ENUM(falloff, "Falloff", falloff_enum, CLOSURE_BSSRDF_BURLEY_ID);
            //SOCKET_IN_FLOAT(scale, "Scale", 0.01f);
            //SOCKET_IN_VECTOR(radius, "Radius", make_float3(0.1f, 0.1f, 0.1f));
            //SOCKET_IN_FLOAT(sharpness, "Sharpness", 0.0f);
            //SOCKET_IN_FLOAT(texture_blur, "Texture Blur", 1.0f);

            //SOCKET_OUT_CLOSURE(BSSRDF, "BSSRDF");

            NodeTitle = "Subsurface Scattering";
            NodeName = "subsurface_scattering";

            Slots.Add(new BocsSlotClosure(this, "BSSRDF", "bssrdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "falloff", "falloff", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "burley", "cubic", "gaussian" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Scale", "scale", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotVector3(this, "Radius", "radius", BocsSlotBase.BocsSlotType.Input, new Vector3(1, 1, 1)));
            Slots.Add(new BocsSlotFloat(this, "Texture Blur", "texture_blur", BocsSlotBase.BocsSlotType.Input, 0));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}