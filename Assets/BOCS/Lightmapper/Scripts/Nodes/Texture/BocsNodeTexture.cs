namespace BOCSLIGHTMAPPER
{
    public class BocsNodeTexture : BocsNodeBase
    {
        public BocsNodeTexture()
        {
            NodeTitle = "Image Texture";
            NodeName = "image_texture";

            //Outputs...
            Slots.Add(new BocsSlotClosure(this, "Color", "color", BocsSlotBase.BocsSlotType.Output));
            Slots.Add(new BocsSlotClosure(this, "Alpha", "alpha", BocsSlotBase.BocsSlotType.Output));

            //Options...
            Slots.Add(new BocsSlotTexture(this, "Image", "filename", BocsSlotBase.BocsSlotType.Value));

            BocsSlotStringList bsl = new BocsSlotStringList(this, "color_space", "color_space", BocsSlotBase.BocsSlotType.Value, 1);
            bsl.List = new string[] { "none", "color" };
            Slots.Add(bsl);

            Slots.Add(new BocsSlotBool(this, "Use Alpha", "use_alpha", BocsSlotBase.BocsSlotType.Value, true));

            BocsSlotStringList bsl1 = new BocsSlotStringList(this, "interpolation", "interpolation", BocsSlotBase.BocsSlotType.Value, 1);
            bsl1.List = new string[] { "closest", "linear", "cubic", "smart" };
            Slots.Add(bsl1);

            BocsSlotStringList bsl2 = new BocsSlotStringList(this, "extension", "extension", BocsSlotBase.BocsSlotType.Value, 0);
            bsl2.List = new string[] { "periodic", "clamp", "black" };
            Slots.Add(bsl2);

            BocsSlotStringList bsl3 = new BocsSlotStringList(this, "projection", "projection", BocsSlotBase.BocsSlotType.Value, 0);
            bsl3.List = new string[] { "flat", "box", "sphere", "tube" };
            Slots.Add(bsl3);

            //_slots.Add(new BocsSlotFloat(this,"Blend","projection_blend",BocsSlotBase.BocsSlotType.Value,0));

            //Inputs...
            Slots.Add(new BocsSlotClosure(this, "Vector", "vector", BocsSlotBase.BocsSlotType.Input));
        }
    }
}