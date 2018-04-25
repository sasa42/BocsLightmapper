namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsCyclesShaderStandardSpec
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

            emissionPower = emissionColor.maxColorComponent * .5f;
            gloss = 1 - gloss;
            spec = metal;
            if (gloss > spec) spec = gloss;

            string n = string.Empty;

            n += @"
node|t=BocsNodeOutput,x=2150,y=300,c=0:
node|t=BocsNodeTexture,x=440,y=150,c=0:
node|t=BocsNodeTexture,x=440,y=490,c=0:
node|t=BocsNodeTexture,x=440,y=1170,c=0:
node|t=BocsNodeNormalMap,x=930,y=1010,c=0:
node|t=BocsNodeTextureCoordinate,x=10,y=260,c=0:
node|t=BocsNodeMapping,x=170,y=260,c=0:
node|t=BocsNodeTexture,x=440,y=1510,c=0:
node|t=BocsNodeEmission,x=1140,y=1210,c=0:
node|t=BocsNodeDiffuseBsdf,x=1410,y=270,c=0:
node|t=BocsNodeGlossyBsdf,x=1430,y=450,c=0:
node|t=BocsNodeColor,x=690,y=270,c=0:
node|t=BocsNodeColor,x=1010,y=430,c=0:
node|t=BocsNodeValue,x=680,y=720,c=0:
node|t=BocsNodeMath,x=1010,y=570,c=0:
node|t=BocsNodeInvert,x=680,y=830,c=0:
node|t=BocsNodeTexture,x=440,y=830,c=0:
node|t=BocsNodeAddShader,x=1940,y=300,c=0:
node|t=BocsNodeMixRGB,x=900,y=20,c=0:
node|t=BocsNodeAddShader,x=1740,y=180,c=0:
val|n=1,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(mainTex) + @":
val|n=1,s=color_space,v=1:
val|n=1,s=use_alpha,v=False:
val|n=1,s=interpolation,v=1:
val|n=1,s=extension,v=0:
val|n=1,s=projection,v=0:
val|n=2,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(specTex) + @":
val|n=2,s=color_space,v=1:
val|n=2,s=use_alpha,v=False:
val|n=2,s=interpolation,v=1:
val|n=2,s=extension,v=0:
val|n=2,s=projection,v=0:
val|n=3,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(normalTex) + @":
val|n=3,s=color_space,v=0:
val|n=3,s=use_alpha,v=False:
val|n=3,s=interpolation,v=1:
val|n=3,s=extension,v=0:
val|n=3,s=projection,v=0:
val|n=4,s=space,v=1:
val|n=4,s=attribute,v=:
val|n=4,s=strength,v=" + normal + @":
val|n=4,s=color,v=7F7F7FFF:
val|n=6,s=tex_mapping.type,v=1:
val|n=6,s=tex_mapping.translation,v=" + mat.mainTextureOffset.x + " " + mat.mainTextureOffset.y + @" 0:
val|n=6,s=tex_mapping.rotation,v=0 0 0:
val|n=6,s=tex_mapping.scale,v=" + mat.mainTextureScale.x + " " + mat.mainTextureScale.y + @" 0:
val|n=6,s=vector,v=0 0 0:
val|n=7,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(emissionTex) + @":
val|n=7,s=color_space,v=0:
val|n=7,s=use_alpha,v=False:
val|n=7,s=interpolation,v=1:
val|n=7,s=extension,v=0:
val|n=7,s=projection,v=0:
val|n=8,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(emissionColor) + @":
val|n=8,s=strength,v=" + emissionPower + @":
val|n=9,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(color) + @":
val|n=9,s=roughness,v=0.1:
val|n=10,s=distribution,v=2:
val|n=10,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(specColor) + @":
val|n=10,s=roughness,v=0.1:
val|n=11,s=value,v=" + BocsCyclesShaderConvert.ColorToHex(color) + @":
val|n=12,s=value,v=" + BocsCyclesShaderConvert.ColorToHex(specColor) + @":
val|n=13,s=value,v=" + gloss + @":
val|n=14,s=type,v=10:
val|n=14,s=use_clamp,v=False:
val|n=14,s=value1,v=" + gloss + @":
val|n=14,s=value2,v=2:
val|n=15,s=fac,v=1:
val|n=15,s=color,v=000000FF:
val|n=16,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(specTex) + @":
val|n=16,s=color_space,v=0:
val|n=16,s=use_alpha,v=True:
val|n=16,s=interpolation,v=1:
val|n=16,s=extension,v=0:
val|n=16,s=projection,v=0:
val|n=18,s=type,v=2:
val|n=18,s=use_clamp,v=False:
val|n=18,s=fac,v=1:
val|n=18,s=color1,v=00000000:
val|n=18,s=color2,v=00000000:
connect|n1=1,n2=18,s1=color,s2=color1,:
connect|n1=3,n2=4,s1=color,s2=color,:
connect|n1=5,n2=6,s1=UV,s2=vector,:
connect|n1=6,n2=1,s1=vector,s2=vector,:
connect|n1=6,n2=3,s1=vector,s2=vector,:
connect|n1=6,n2=7,s1=vector,s2=vector,:
connect|n1=6,n2=16,s1=vector,s2=vector,:
connect|n1=6,n2=2,s1=vector,s2=vector,:
connect|n1=8,n2=17,s1=emission,s2=closure2,:
connect|n1=9,n2=19,s1=bsdf,s2=closure1,:
connect|n1=10,n2=19,s1=bsdf,s2=closure2,:
connect|n1=11,n2=18,s1=color,s2=color2,:
connect|n1=14,n2=10,s1=value,s2=roughness,:
connect|n1=14,n2=9,s1=value,s2=roughness,:
connect|n1=16,n2=15,s1=alpha,s2=color,:
connect|n1=17,n2=0,s1=closure,s2=surface,:
connect|n1=19,n2=17,s1=closure,s2=closure1,:";

            if (mainTex != null)
            {
                n += @"connect|n1=18,n2=9,s1=color,s2=color,:";
            }
            else
            {
                //n += @"";
            }

            if (normalTex != null)
            {
                n += @"connect|n1=4,n2=9,s1=normal,s2=normal,:";
                n += @"connect|n1=4,n2=10,s1=normal,s2=normal,:";
            }

            if (emissionTex != null)
            {
                n += @"connect|n1=7,n2=8,s1=color,s2=color,:";
            }

            if (specTex != null)
            {
                n += @"connect|n1=2,n2=10,s1=color,s2=color,:";
                n += @"connect|n1=15,n2=14,s1=color,s2=value1,:";
            }
            else
            {
                //n += @"connect|n1=13,n2=10,s1=color,s2=color,:";
                //n += @"connect|n1=14,n2=15,s1=value,s2=value1,:";
            }

            return n;
        }
    }
}