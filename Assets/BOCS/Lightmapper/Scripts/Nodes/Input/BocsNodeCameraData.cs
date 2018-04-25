namespace BOCSLIGHTMAPPER
{
    public class BocsNodeCameraData : BocsNodeBase
    {
        public BocsNodeCameraData()
        {
            //NodeType* type = NodeType::add("camera_info", create, NodeType::SHADER);

            //SOCKET_OUT_VECTOR(view_vector, "View Vector");
            //SOCKET_OUT_FLOAT(view_z_depth, "View Z Depth");
            //SOCKET_OUT_FLOAT(view_distance, "View Distance");

            NodeTitle = "Camera Info";
            NodeName = "camera";

            Slots.Add(new BocsSlotClosure(this, "View Vector", "view_vector", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "View Z Depth", "view_z_depth", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "View Distance", "view_distance", BocsSlotBase.BocsSlotType.Output));
        }
    }
}