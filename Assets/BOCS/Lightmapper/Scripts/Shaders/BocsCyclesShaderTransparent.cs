namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsCyclesShaderTransparent
    {
        public static string Convert(Material mat)
        {
            Texture mainTex = null;
            Texture metalTex = null;
            Texture specTex = null;
            Texture normalTex = null;
            Texture emissionTex = null;
            //Texture _occlusionTex = null;
            //Texture _detailMaskTex = null;
            //Texture _detailAlbedoTex = null;
            //Texture _detailNormalTex = null;

            if (mat.HasProperty("_MainTex")) mainTex = mat.GetTexture("_MainTex");
            if (mat.HasProperty("_MetallicGlossMap")) metalTex = mat.GetTexture("_MetallicGlossMap");
            if (mat.HasProperty("_SpecGlossMap")) specTex = mat.GetTexture("_SpecGlossMap");
            if (mat.HasProperty("_BumpMap")) normalTex = mat.GetTexture("_BumpMap");
            if (mat.HasProperty("_EmissionMap")) emissionTex = mat.GetTexture("_EmissionMap");
            //if (mat.HasProperty("_OcclusionMap")) _occlusionTex = mat.GetTexture("_OcclusionMap");
            //if (mat.HasProperty("_DetailMask")) _detailMaskTex = mat.GetTexture("_DetailMask");
            //if (mat.HasProperty("_DetailAlbedoMap")) _detailAlbedoTex = mat.GetTexture("_DetailAlbedoMap");
            //if (mat.HasProperty("_DetailNormalMap")) _detailNormalTex = mat.GetTexture("_DetailNormalMap");

            Color color = Color.white;
            float metal = 0;
            float gloss = 0;
            float spec = 0;
            float normal = 1;
            Color specColor = Color.black;
            Color emissionColor = Color.black;
            float emissionPower = 0;

            if (mat.HasProperty("_Color")) color = mat.GetColor("_Color");
            if (mat.HasProperty("_Metallic")) metal = mat.GetFloat("_Metallic");
            if (mat.HasProperty("_BumpScale")) normal = mat.GetFloat("_BumpScale");
            if (mat.HasProperty("_Glossiness")) gloss = mat.GetFloat("_Glossiness");
            if (mat.HasProperty("_SpecColor")) specColor = mat.GetColor("_SpecColor");
            if (mat.HasProperty("_EmissionColor")) emissionColor = mat.GetColor("_EmissionColor");

            emissionPower = emissionColor.maxColorComponent;
            gloss = 1 - gloss;
            spec = metal;
            if (gloss > spec) spec = gloss;

            string n = string.Empty;

            n += @"
node|t=BocsNodeOutput,x=1900,y=100,c=0:
node|t=BocsNodeTexture,x=520,y=90,c=0:
node|t=BocsNodeMixShader,x=1300,y=90,c=0:
node|t=BocsNodeTransparentBsdf,x=1010,y=180,c=0:
node|t=BocsNodeDiffuseBsdf,x=990,y=290,c=0:
node|t=BocsNodeValue,x=960,y=20,c=0:
node|t=BocsNodeTexture,x=520,y=460,c=0:
node|t=BocsNodeNormalMap,x=750,y=590,c=0:
node|t=BocsNodeTexture,x=520,y=810,c=0:
node|t=BocsNodeAddShader,x=1660,y=350,c=0:
node|t=BocsNodeEmission,x=1250,y=790,c=0:
node|t=BocsNodeMapping,x=210,y=250,c=0:
node|t=BocsNodeTextureCoordinate,x=30,y=240,c=0:
val|n=1,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(mainTex) + @":
val|n=1,s=color_space,v=1:
val|n=1,s=use_alpha,v=True:
val|n=1,s=interpolation,v=1:
val|n=1,s=extension,v=0:
val|n=1,s=projection,v=0:
val|n=2,s=fac,v=1:
val|n=3,s=color,v=FFFFFFFF:
val|n=4,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(color) + @":
val|n=4,s=roughness,v=0:
val|n=5,s=value,v=" + color.a + @":
val|n=6,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(normalTex) + @":
val|n=6,s=color_space,v=0:
val|n=6,s=use_alpha,v=False:
val|n=6,s=interpolation,v=1:
val|n=6,s=extension,v=0:
val|n=6,s=projection,v=0:
val|n=7,s=space,v=1:
val|n=7,s=attribute,v=:
val|n=7,s=strength,v=" + normal + @":
val|n=7,s=color,v=7F7F7FFF:
val|n=8,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(emissionTex) + @":
val|n=8,s=color_space,v=1:
val|n=8,s=use_alpha,v=True:
val|n=8,s=interpolation,v=1:
val|n=8,s=extension,v=0:
val|n=8,s=projection,v=0:
val|n=10,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(emissionColor) + @":
val|n=10,s=strength,v=" + emissionPower + @":
val|n=11,s=tex_mapping.type,v=1:
val|n=11,s=tex_mapping.translation,v=" + mat.mainTextureOffset.x + " " + mat.mainTextureOffset.y + @" 0:
val|n=11,s=tex_mapping.rotation,v=0 0 0:
val|n=11,s=tex_mapping.scale,v=" + mat.mainTextureScale.x + " " + mat.mainTextureScale.y + @" 0:
val|n=11,s=vector,v=0 0 0:
connect|n1=2,n2=9,s1=closure,s2=closure1,:
connect|n1=3,n2=2,s1=bsdf,s2=closure1,:
connect|n1=4,n2=2,s1=bsdf,s2=closure2,:
connect|n1=6,n2=7,s1=color,s2=color,:
connect|n1=9,n2=0,s1=closure,s2=surface,:
connect|n1=10,n2=9,s1=emission,s2=closure2,:
connect|n1=11,n2=1,s1=vector,s2=vector,:
connect|n1=11,n2=6,s1=vector,s2=vector,:
connect|n1=11,n2=8,s1=vector,s2=vector,:
connect|n1=12,n2=11,s1=UV,s2=vector,:";

            if (mainTex != null)
            {
                n += @"connect|n1=1,n2=4,s1=color,s2=color,:";
                n += @"connect|n1=1,n2=2,s1=alpha,s2=fac,:";
            }
            else
            {
                n += @"connect|n1=5,n2=2,s1=value,s2=fac,:";
            }

            if (normalTex != null)
            {
                n += @"connect|n1=7,n2=4,s1=normal,s2=normal,:";
            }

            if (emissionTex != null)
            {
                n += @"connect|n1=8,n2=10,s1=color,s2=color,:";
            }

            return n;
        }
    }
}