namespace BOCSLIGHTMAPPER
{
    public class BocsNodeWaveLength : BocsNodeBase
    {
        public BocsNodeWaveLength()
        {
            //NodeType* type = NodeType::add("wavelength", create, NodeType::SHADER);

            //SOCKET_IN_FLOAT(wavelength, "Wavelength", 500.0f);
            //SOCKET_OUT_COLOR(color, "Color");

            NodeTitle = "Wavelength";
            NodeName = "wavelength";

            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));

            Slots.Add(new BocsSlotFloat(this, "Wavelength", "wavelength", BocsSlotBase.BocsSlotType.Input, 500));
        }
    }
}