namespace BOCSLIGHTMAPPER
{
    public class BocsNodeHoldout : BocsNodeBase
    {
        public BocsNodeHoldout()
        {
            //NodeType* type = NodeType::add("holdout", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(surface_mix_weight, "SurfaceMixWeight", 0.0f, SocketType::SVM_INTERNAL);
            //SOCKET_IN_FLOAT(volume_mix_weight, "VolumeMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(holdout, "Holdout");

            NodeTitle = "Holdout";
            NodeName = "holdout";

            Slots.Add(new BocsSlotClosure(this, "Holdout", "holdout", BocsSlotBase.BocsSlotType.Output));
        }
    }
}