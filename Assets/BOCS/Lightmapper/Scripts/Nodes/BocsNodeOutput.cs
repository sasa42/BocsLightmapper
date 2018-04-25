namespace BOCSLIGHTMAPPER
{
    public class BocsNodeOutput : BocsNodeBase
    {
        public BocsNodeOutput()
        {
            NodeTitle = "Output";
            NodeName = "output";

            Slots.Add(new BocsSlotClosure(this, "Surface", "surface", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Volume", "volume", BocsSlotBase.BocsSlotType.Input));
            Slots.Add(new BocsSlotClosure(this, "Displacement", "displacement", BocsSlotBase.BocsSlotType.Input));
        }
    }
}