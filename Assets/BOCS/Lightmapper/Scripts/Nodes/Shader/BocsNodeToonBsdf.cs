namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeToonBsdf : BocsNodeBase
    {
        public BocsNodeToonBsdf()
        {
            //NodeType* type = NodeType::add("toon_bsdf", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //static NodeEnum component_enum;
            //component_enum.insert("diffuse", CLOSURE_BSDF_DIFFUSE_TOON_ID);
            //component_enum.insert("glossy", CLOSURE_BSDF_GLOSSY_TOON_ID);
            //SOCKET_ENUM(component, "Component", component_enum, CLOSURE_BSDF_DIFFUSE_TOON_ID);
            //SOCKET_IN_FLOAT(size, "Size", 0.5f);
            //SOCKET_IN_FLOAT(smooth, "Smooth", 0.0f);

            //SOCKET_OUT_CLOSURE(BSDF, "BSDF");

            NodeTitle = "Toon BSDF";
            NodeName = "toon_bsdf";

            Slots.Add(new BocsSlotClosure(this, "BSDF", "bsdf", BocsSlotBase.BocsSlotType.Output));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "component", "component", BocsSlotBase.BocsSlotType.Value, 0);
            bsl.List = new string[] { "diffuse", "glossy" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Size", "size", BocsSlotBase.BocsSlotType.Input, .5f));
            Slots.Add(new BocsSlotFloat(this, "Smooth", "smooth", BocsSlotBase.BocsSlotType.Input, 0));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}