namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeRefractionBsdf : BocsNodeBase
    {
        public BocsNodeRefractionBsdf()
        {
            //NodeType* type = NodeType::add("refraction_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum distribution_enum;
            //distribution_enum.insert("sharp", CLOSURE_BSDF_REFRACTION_ID);
            //distribution_enum.insert("beckmann", CLOSURE_BSDF_MICROFACET_BECKMANN_REFRACTION_ID);
            //distribution_enum.insert("GGX", CLOSURE_BSDF_MICROFACET_GGX_REFRACTION_ID);
            //SOCKET_ENUM(distribution, "Distribution", distribution_enum, CLOSURE_BSDF_MICROFACET_GGX_REFRACTION_ID);

            //SOCKET_IN_FLOAT(roughness, "Roughness", 0.0f);
            //SOCKET_IN_FLOAT(IOR, "IOR", 0.3f);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Refraction BSDF";
            NodeName = "refraction_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "distribution", "distribution", BocsSlotBase.BocsSlotType.Value, 2);
            bsl.List = new string[] { "sharp", "beckmann", "GGX" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Roughness", "roughness", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "IOR", "IOR", BocsSlotBase.BocsSlotType.Input, 1.45f));
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}