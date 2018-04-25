namespace BOCSLIGHTMAPPER
{
    public class BocsNodeParticleInfo : BocsNodeBase
    {
        public BocsNodeParticleInfo()
        {
            //		NodeType* type = NodeType::add("particle_info", create, NodeType::SHADER);

            //		SOCKET_OUT_FLOAT(index, "Index");
            //		SOCKET_OUT_FLOAT(age, "Age");
            //		SOCKET_OUT_FLOAT(lifetime, "Lifetime");
            //		SOCKET_OUT_POINT(location, "Location");
            //#if 0	/* not yet supported */
            //		SOCKET_OUT_QUATERNION(rotation, "Rotation");
            //#endif
            //		SOCKET_OUT_FLOAT(size, "Size");
            //		SOCKET_OUT_VECTOR(velocity, "Velocity");
            //		SOCKET_OUT_VECTOR(angular_velocity, "Angular Velocity");

            NodeTitle = "Particle Info";
            NodeName = "particle_info";

            Slots.Add(new BocsSlotClosure(this, "Index", "index", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Age", "age", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Lifetime", "lifetime", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Location", "location", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Size", "size", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Velocity", "velocity", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Angular Velocity", "angularvelocity", BocsSlotBase.BocsSlotType.Output));
        }
    }
}