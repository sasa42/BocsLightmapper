namespace BOCSLIGHTMAPPER
{
    public class BocsNodeMixShader : BocsNodeBase
    {
        public BocsNodeMixShader()
        {
            //NodeType* type = NodeType::add("mix_closure", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(fac, "Fac", 0.5f);
            //SOCKET_IN_CLOSURE(closure1, "Closure1");
            //SOCKET_IN_CLOSURE(closure2, "Closure2");

            //SOCKET_OUT_CLOSURE(closure, "Closure");

            NodeTitle = "Mix Shader";
            NodeName = "mix_closure";

            Slots.Add(new BocsSlotClosure(this, "Shader", "closure", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Shader", "closure1", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Shader", "closure2", BocsSlotBase.BocsSlotType.Input));
        }
    }
}