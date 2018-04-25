namespace BOCSLIGHTMAPPER
{
    public class BocsNodeWireframe : BocsNodeBase
    {
        public BocsNodeWireframe()
        {
            //NodeType* type = NodeType::add("wireframe", create, NodeType::SHADER);

            //SOCKET_BOOLEAN(use_pixel_size, "Use Pixel Size", false);
            //SOCKET_IN_FLOAT(size, "Size", 0.01f);
            //SOCKET_OUT_FLOAT(fac, "Fac");

            NodeTitle = "Wireframe";
            NodeName = "wireframe";

            Slots.Add(new BocsSlotClosure(this, "Fac", "fac", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotBool(this, "Pixel Size", "use_pixel_size", BocsSlotBase.BocsSlotType.Value, false));

            Slots.Add(new BocsSlotFloat(this, "Size", "size", BocsSlotBase.BocsSlotType.Input, .01f));
        }
    }
}