namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeAnisotropicBsdf : BocsNodeBase
    {
        public BocsNodeAnisotropicBsdf()
        {
            //NodeType* type = NodeType::add("anisotropic_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum distribution_enum;
            //distribution_enum.insert("beckmann", CLOSURE_BSDF_MICROFACET_BECKMANN_ANISO_ID);
            //distribution_enum.insert("GGX", CLOSURE_BSDF_MICROFACET_GGX_ANISO_ID);
            //distribution_enum.insert("Multiscatter GGX", CLOSURE_BSDF_MICROFACET_MULTI_GGX_ANISO_ID);
            //distribution_enum.insert("ashikhmin_shirley", CLOSURE_BSDF_ASHIKHMIN_SHIRLEY_ANISO_ID);
            //SOCKET_ENUM(distribution, "Distribution", distribution_enum, CLOSURE_BSDF_MICROFACET_GGX_ANISO_ID);

            //SOCKET_IN_VECTOR(tangent, "Tangent", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_TANGENT);

            //SOCKET_IN_FLOAT(roughness, "Roughness", 0.2f);
            //SOCKET_IN_FLOAT(anisotropy, "Anisotropy", 0.5f);
            //SOCKET_IN_FLOAT(rotation, "Rotation", 0.0f);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Anisotropic BSDF";
            NodeName = "anisotropic_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "distribution", "distribution", BocsSlotBase.BocsSlotType.Value, 1);
            bsl.List = new string[] { "beckmann", "GGX", "Multiscatter GGX", "ashikhmin_shirley" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Roughness", "roughness", BocsSlotBase.BocsSlotType.Input, .2f));
            Slots.Add(new BocsSlotFloat(this, "Anisotropy", "anisotropy", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotFloat(this, "Rotation", "rotation", BocsSlotBase.BocsSlotType.Input, 0));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Tangent", "tangent", BocsSlotBase.BocsSlotType.Input));
        }
    }
}