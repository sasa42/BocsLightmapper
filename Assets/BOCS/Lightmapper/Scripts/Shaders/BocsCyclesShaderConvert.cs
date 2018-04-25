namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsCyclesShaderConvert
    {
        public static string Convert(Material mat)
        {
            if (mat == null) return string.Empty;

            if (mat.HasProperty("_Mode"))
            {
                if (mat.GetInt("_Mode") != 0) return BocsCyclesShaderTransparent.Convert(mat);
            }

            if (mat.shader.name == "Standard") return BocsCyclesShaderStandard.Convert(mat);
            if (mat.shader.name == "Standard (Specular setup)") return BocsCyclesShaderStandardSpec.Convert(mat);

            return BocsCyclesShaderStandard.Convert(mat);//Use as generic unknown shader...
        }

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
            return hex;
        }

        public static string TexToGuid(Texture tex)
        {
#if UNITY_EDITOR
            if (tex == null) return string.Empty;
            return UnityEditor.AssetDatabase.AssetPathToGUID(UnityEditor.AssetDatabase.GetAssetPath(tex));
#else
            return "";
#endif
        }
    }
}