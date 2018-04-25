namespace BOCSLIGHTMAPPER
{
    public class BocsNodeBump : BocsNodeBase
    {
        public BocsNodeBump()
        {
            NodeTitle = "Bump";
            NodeName = "bump";

            //	NodeType* type = NodeType::add("bump", create, NodeType::SHADER);

            //	SOCKET_BOOLEAN(invert, "Invert", false);
            //	SOCKET_BOOLEAN(use_object_space, "UseObjectSpace", false);

            ///* this input is used by the user, but after graph transform it is no longer
            // * used and moved to sampler center/x/y instead */
            //	SOCKET_IN_FLOAT(height, "Height", 1.0f);

            //	SOCKET_IN_FLOAT(sample_center, "SampleCenter", 0.0f);
            //	SOCKET_IN_FLOAT(sample_x, "SampleX", 0.0f);
            //	SOCKET_IN_FLOAT(sample_y, "SampleY", 0.0f);
            //	SOCKET_IN_NORMAL(normal, "Normal", make_float3(0.0f, 0.0f, 0.0f), SocketType::LINK_NORMAL);
            //	SOCKET_IN_FLOAT(strength, "Strength", 1.0f);
            //	SOCKET_IN_FLOAT(distance, "Distance", 0.1f);

            //	SOCKET_OUT_NORMAL(normal, "Normal");

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotBool(this, "Invert", "invert", BocsSlotBase.BocsSlotType.Value, false));

            Slots.Add(new BocsSlotFloat(this, "Strength", "strength", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Distance", "distance", BocsSlotBase.BocsSlotType.Input, .1f));
            Slots.Add(new BocsSlotFloat(this, "Height", "height", BocsSlotBase.BocsSlotType.Input));

            //_slots.Add(new BocsSlotFloat(this,"SampleX","SampleX",BocsSlotBase.BocsSlotType.Input));
            //_slots.Add(new BocsSlotFloat(this,"SampleY","SampleY",BocsSlotBase.BocsSlotType.Input));
            //_slots.Add(new BocsSlotFloat(this,"SampleCenter","SampleCenter",BocsSlotBase.BocsSlotType.Input));

            Slots.Add(new BocsSlotClosure(this, "Normal", "normal", BocsSlotBase.BocsSlotType.Input));
        }
    }
}