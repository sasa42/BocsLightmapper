namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeGlossyBsdf : BocsNodeBase
    {
        public BocsNodeGlossyBsdf()
        {
            //NodeType* type = NodeType::add("glossy_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum distribution_enum;
            //distribution_enum.insert("sharp", CLOSURE_BSDF_REFLECTION_ID);
            //distribution_enum.insert("beckmann", CLOSURE_BSDF_MICROFACET_BECKMANN_ID);
            //distribution_enum.insert("GGX", CLOSURE_BSDF_MICROFACET_GGX_ID);
            //distribution_enum.insert("ashikhmin_shirley", CLOSURE_BSDF_ASHIKHMIN_SHIRLEY_ID);
            //distribution_enum.insert("Multiscatter GGX", CLOSURE_BSDF_MICROFACET_MULTI_GGX_ID);
            //SOCKET_ENUM(distribution, "Distribution", distribution_enum, CLOSURE_BSDF_MICROFACET_GGX_ID);
            //SOCKET_IN_FLOAT(roughness, "Roughness", 0.2f);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Glossy BSDF";
            NodeName = "glossy_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "distribution", "distribution", BocsSlotBase.BocsSlotType.Value, 2);
            bsl.List = new string[] { "sharp", "beckmann", "GGX", "ashikhmin_shirley", "Multiscatter GGX" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Roughness", "roughness", BocsSlotBase.BocsSlotType.Input, .2f));
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}