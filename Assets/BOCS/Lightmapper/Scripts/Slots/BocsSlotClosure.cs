namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsSlotClosure : BocsSlotBase
    {
        public BocsSlotClosure(BocsNodeBase n, string description, string name, BocsSlotType type) : base(n, description, name, type)
        {
            //Color Known Types...
            if (description == "Color") SlotColor = Color.yellow;

            if (description == "Surface") SlotColor = Color.green;
            if (description == "BSDF") SlotColor = Color.green;
            if (description == "Shader") SlotColor = Color.green;
            if (description == "Volume") SlotColor = Color.green;
            if (description == "Emission") SlotColor = Color.green;
            if (description == "BSSRDF") SlotColor = Color.green;
            if (description == "Background") SlotColor = Color.green;
            if (description == "AO") SlotColor = Color.green;
            if (description == "Holdout") SlotColor = Color.green;

            if (description == "Normal") SlotColor = Color.blue;
            if (description == "Tangent") SlotColor = Color.blue;
            if (description == "Vector") SlotColor = Color.blue;
            if (description == "Clearcoat Normal") SlotColor = Color.blue;
            if (description == "Generated") SlotColor = Color.blue;
            if (description == "UV") SlotColor = Color.blue;
            if (description == "Object") SlotColor = Color.blue;
            if (description == "Camera") SlotColor = Color.blue;
            if (description == "Window") SlotColor = Color.blue;
            if (description == "Reflection") SlotColor = Color.blue;
        }

        public override void DrawSlotGUI()
        {
            if (!CanDrawSlot()) return;
            GUILayout.Label(this.SlotTitle);
        }
    }
}