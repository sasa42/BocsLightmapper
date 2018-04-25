namespace BOCSLIGHTMAPPER
{
    public class BocsNodeGeometry : BocsNodeBase
    {
        public BocsNodeGeometry()
        {
            //NodeType* type = NodeType::add("geometry", create, NodeType::SHADER);

            //SOCKET_IN_NORMAL(normal_osl, "NormalIn", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL | SocketType::OSL_INTERNAL);

            //SOCKET_OUT_POINT(position, "Position");
            //SOCKET_OUT_NORMAL(normal, "Normal");
            //SOCKET_OUT_NORMAL(tangent, "Tangent");
            //SOCKET_OUT_NORMAL(true_normal, "True Normal");
            //SOCKET_OUT_VECTOR(incoming, "Incoming");
            //SOCKET_OUT_POINT(parametric, "Parametric");
            //SOCKET_OUT_FLOAT(backfacing, "Backfacing");
            //SOCKET_OUT_FLOAT(pointiness, "Pointiness");

            NodeTitle = "Geometry";
            NodeName = "geometry";

            Slots.Add(new BocsSlotClosure(this, "Position", "position", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Tangent", "tangent", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "True Normal", "true_normal", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Incoming", "incoming", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Parametric", "parametric", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Backfacing", "backfacing", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Pointiness", "pointiness", BocsSlotBase.BocsSlotType.Output));
        }
    }
}