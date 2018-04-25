namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeHairBsdf : BocsNodeBase
    {
        public BocsNodeHairBsdf()
        {
            //NodeType* type = NodeType::add("hair_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum component_enum;
            //component_enum.insert("reflection", CLOSURE_BSDF_HAIR_REFLECTION_ID);
            //component_enum.insert("transmission", CLOSURE_BSDF_HAIR_TRANSMISSION_ID);
            //SOCKET_ENUM(component, "Component", component_enum, CLOSURE_BSDF_HAIR_REFLECTION_ID);
            //SOCKET_IN_FLOAT(offset, "Offset", 0.0f);
            //SOCKET_IN_FLOAT(roughness_u, "RoughnessU", 0.2f);
            //SOCKET_IN_FLOAT(roughness_v, "RoughnessV", 0.2f);
            //SOCKET_IN_VECTOR(tangent, "Tangent", make_float3(0.0f, 0.0f, 0.0f));

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Hair BSDF";
            NodeName = "hair_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "component", "component", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "reflection", "transmission" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Offset", "offset", BocsSlotBase.BocsSlotType.Input, 0));
            Slots.Add(new BocsSlotFloat(this, "RoughnessU", "roughness_u", BocsSlotBase.BocsSlotType.Input, .2f));
            Slots.Add(new BocsSlotFloat(this, "RoughnessV", "roughness_v", BocsSlotBase.BocsSlotType.Input, 1));

            Slots.Add(new BocsSlotClosure(this, "Tangent", "tangent", BocsSlotBase.BocsSlotType.Input));
        }
    }
}