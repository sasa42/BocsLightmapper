namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeDisneyBsdf : BocsNodeBase
    {
        public BocsNodeDisneyBsdf()
        {
            //NodeType* type = NodeType::add("disney_bsdf", create, NodeType::SHADER);
            NodeTitle = "Disney BSDF";
            NodeName = "principled_bsdf";

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");
            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            //static NodeEnum distribution_enum;
            //distribution_enum.insert("GGX", CLOSURE_BSDF_MICROFACET_GGX_GLASS_ID);
            //distribution_enum.insert("Multiscatter GGX", CLOSURE_BSDF_MICROFACET_MULTI_GGX_GLASS_ID);
            //SOCKET_ENUM(distribution, "Distribution", distribution_enum, CLOSURE_BSDF_MICROFACET_MULTI_GGX_GLASS_ID);
            BocsSlotStringList bsl = new BocsSlotStringList(this, "distribution", "distribution", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "Multiscatter GGX", "GGX" };
            Slots.Add(bsl);

            //SOCKET_IN_COLOR(base_color, "Base Color", make_float3(0.8f, 0.8f, 0.8f));
            Slots.Add(new BocsSlotColor(this, "Base Color", "base_color", BocsSlotBase.BocsSlotType.Input, Color.white));

            //SOCKET_IN_COLOR(subsurface_color, "Subsurface Color", make_float3(0.8f, 0.8f, 0.8f));
            Slots.Add(new BocsSlotColor(this, "Subsurface Color", "subsurface_color", BocsSlotBase.BocsSlotType.Input, Color.red));
            //SOCKET_IN_FLOAT(subsurface, "Subsurface", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Subsurface", "subsurface", BocsSlotBase.BocsSlotType.Input, 0));
            //SOCKET_IN_VECTOR(subsurface_radius, "Subsurface Radius", make_float3(0.1f, 0.1f, 0.1f));
            Slots.Add(new BocsSlotVector3(this, "Subsurface Radius", "subsurface_radius", BocsSlotBase.BocsSlotType.Input, new Vector3(1, 1, 1)));

            //SOCKET_IN_FLOAT(metallic, "Metallic", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Metallic", "metallic", BocsSlotBase.BocsSlotType.Input, 0));

            //SOCKET_IN_FLOAT(specular, "Specular", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Specular", "specular", BocsSlotBase.BocsSlotType.Input, 0.5f));
            //SOCKET_IN_FLOAT(specular_tint, "Specular Tint", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Specular Tint", "specular_tint", BocsSlotBase.BocsSlotType.Input, 0));

            //SOCKET_IN_FLOAT(roughness, "Roughness", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Roughness", "roughness", BocsSlotBase.BocsSlotType.Input, 0.5f));

            //SOCKET_IN_FLOAT(anisotropic, "Anisotropic", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Anisotropic", "anisotropic", BocsSlotBase.BocsSlotType.Input, 0));
            //SOCKET_IN_FLOAT(anisotropic_rotation, "Anisotropic Rotation", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Anisotropic Rotation", "anisotropic_rotation", BocsSlotBase.BocsSlotType.Input, 0));

            //SOCKET_IN_FLOAT(sheen, "Sheen", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Sheen", "sheen", BocsSlotBase.BocsSlotType.Input, 0));
            //SOCKET_IN_FLOAT(sheen_tint, "Sheen Tint", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Sheen Tint", "sheen_tint", BocsSlotBase.BocsSlotType.Input, 0.5f));

            //SOCKET_IN_FLOAT(clearcoat, "Clearcoat", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Clearcoat", "clearcoat", BocsSlotBase.BocsSlotType.Input, 0));
            //SOCKET_IN_FLOAT(clearcoat_gloss, "Clearcoat Gloss", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Clearcoat Gloss", "clearcoat_gloss", BocsSlotBase.BocsSlotType.Input, 1));

            //SOCKET_IN_FLOAT(ior, "IOR", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "IOR", "ior", BocsSlotBase.BocsSlotType.Input, 1.45f));
            //SOCKET_IN_FLOAT(transparency, "Transparency", 0.0f);
            Slots.Add(new BocsSlotFloat(this, "Transparency", "transparency", BocsSlotBase.BocsSlotType.Input, 0));

            //SOCKET_IN_FLOAT(refraction_roughness, "Refraction Roughness", 0.0f);
            //_slots.Add(new BocsSlotFloat(this,"Refraction Roughness","refraction_roughness",BocsSlotBase.BocsSlotType.Input,0));

            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
            //SOCKET_IN_NORMAL(clearcoat_normal, "Clearcoat Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            Slots.Add(new BocsSlotClosure(this, "Clearcoat Normal", "clearcoat_normal", BocsSlotBase.BocsSlotType.Input));
            //SOCKET_IN_NORMAL(tangent, "Tangent", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TANGENT);
            Slots.Add(new BocsSlotClosure(this, "Tangent", "tangent", BocsSlotBase.BocsSlotType.Input));

            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);
        }
    }
}