﻿namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsNodeScatterVolume : BocsNodeBase
    {
        public BocsNodeScatterVolume()
        {
            //NodeType* type = NodeType::add("scatter_volume", create, NodeType::SHADER);

            //SOCKET_IN_COLOR(color, "Color", make_float3(0.8f, 0.8f, 0.8f));
            //SOCKET_IN_FLOAT(density, "Density", 1.0f);
            //SOCKET_IN_FLOAT(anisotropy, "Anisotropy", 0.0f);
            //SOCKET_IN_FLOAT(volume_mix_weight, "VolumeMixWeight", 0.0f, SocketType::SVM_INTERNAL);

            //SOCKET_OUT_CLOSURE(volume, "Volume");

            NodeTitle = "Scatter Volume";
            NodeName = "scatter_volume";

            Slots.Add(new BocsSlotClosure(this, "Volume", "volume", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotColor(this, "Color", "color", BocsSlotBase.BocsSlotType.Input, Color.white));
            Slots.Add(new BocsSlotFloat(this, "Density", "density", BocsSlotBase.BocsSlotType.Input, 1));
            Slots.Add(new BocsSlotFloat(this, "Anisotropy", "anisotropy", BocsSlotBase.BocsSlotType.Input, 0));
        }
    }
}