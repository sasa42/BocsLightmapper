namespace BOCSLIGHTMAPPER
{
    public class BocsNodeAddShader : BocsNodeBase
    {
        public BocsNodeAddShader()
        {
            //NodeType* type = NodeType::add("add_closure", create, NodeType::SHADER);

            //SOCKET_IN_CLOSURE(closure1, "Closure1");
            //SOCKET_IN_CLOSURE(closure2, "Closure2");
            //SOCKET_OUT_CLOSURE(closure, "Closure");

            NodeTitle = "Add Shader";
            NodeName = "add_closure";

            Slots.Add(new BocsSlotClosure(this, "Shader", "closure", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotClosure(this, "Shader", "closure1", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Shader", "closure2", BocsSlotBase.BocsSlotType.Input));
        }
    }
}