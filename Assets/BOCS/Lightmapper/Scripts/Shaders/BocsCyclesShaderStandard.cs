namespace BOCSLIGHTMAPPER
{
    using UnityEngine;

    public class BocsCyclesShaderStandard
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
node|t=BocsNodeOutput,x=1940,y=10,c=0:
node|t=BocsNodeTexture,x=450,y=10,c=0:
node|t=BocsNodeDisneyBsdf,x=1180,y=160,c=0:
node|t=BocsNodeTexture,x=450,y=670,c=0:
node|t=BocsNodeNormalMap,x=690,y=680,c=0:
node|t=BocsNodeTexture,x=450,y=340,c=0:
node|t=BocsNodeSeparateRGB,x=820,y=410,c=0:
node|t=BocsNodeInvert,x=750,y=550,c=0:
node|t=BocsNodeColor,x=650,y=250,c=0:
node|t=BocsNodeMixRGB,x=840,y=10,c=0:
node|t=BocsNodeTextureCoordinate,x=10,y=10,c=0:
node|t=BocsNodeMapping,x=170,y=50,c=0:
node|t=BocsNodeTexture,x=450,y=1000,c=0:
node|t=BocsNodeAddShader,x=1700,y=280,c=0:
node|t=BocsNodeEmission,x=1310,y=630,c=0:
val|n=1,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(mainTex) + @":
val|n=1,s=color_space,v=1:
val|n=1,s=use_alpha,v=False:
val|n=1,s=interpolation,v=1:
val|n=1,s=extension,v=0:
val|n=1,s=projection,v=0:
val|n=2,s=distribution,v=0:
val|n=2,s=base_color,v=" + BocsCyclesShaderConvert.ColorToHex(color) + @":
val|n=2,s=subsurface_color,v=FFFFFFFF:
val|n=2,s=subsurface,v=0:
val|n=2,s=subsurface_radius,v=0.1 0.1 0.1:
val|n=2,s=metallic,v=" + metal + @":
val|n=2,s=specular,v=" + spec + @":
val|n=2,s=specular_tint,v=0:
val|n=2,s=roughness,v=" + gloss + @":
val|n=2,s=anisotropic,v=0:
val|n=2,s=anisotropic_rotation,v=0:
val|n=2,s=sheen,v=0:
val|n=2,s=sheen_tint,v=0:
val|n=2,s=clearcoat,v=0:
val|n=2,s=clearcoat_gloss,v=1:
val|n=2,s=ior,v=1.45:
val|n=2,s=transparency,v=0:
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
val|n=5,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(metalTex) + @":
val|n=5,s=color_space,v=0:
val|n=5,s=use_alpha,v=True:
val|n=5,s=interpolation,v=1:
val|n=5,s=extension,v=0:
val|n=5,s=projection,v=0:
val|n=6,s=color,v=00000000:
val|n=7,s=fac,v=1:
val|n=7,s=color,v=000000FF:
val|n=8,s=value,v=" + BocsCyclesShaderConvert.ColorToHex(color) + @":
val|n=9,s=type,v=2:
val|n=9,s=use_clamp,v=True:
val|n=9,s=fac,v=1:
val|n=9,s=color1,v=00000000:
val|n=9,s=color2,v=00000000:
val|n=11,s=tex_mapping.type,v=1:
val|n=11,s=tex_mapping.translation,v=" + mat.mainTextureOffset.x + " " + mat.mainTextureOffset.y + @" 0:
val|n=11,s=tex_mapping.rotation,v=0 0 0:
val|n=11,s=tex_mapping.scale,v=" + mat.mainTextureScale.x + " " + mat.mainTextureScale.y + @" 0:
val|n=11,s=vector,v=0 0 0:
val|n=12,s=filename,v=" + BocsCyclesShaderConvert.TexToGuid(emissionTex) + @":
val|n=12,s=color_space,v=0:
val|n=12,s=use_alpha,v=True:
val|n=12,s=interpolation,v=1:
val|n=12,s=extension,v=0:
val|n=12,s=projection,v=0:
val|n=14,s=color,v=" + BocsCyclesShaderConvert.ColorToHex(emissionColor) + @":
val|n=14,s=strength,v=" + emissionPower + @":
connect|n1=1,n2=9,s1=color,s2=color1,:
connect|n1=2,n2=13,s1=bsdf,s2=closure1,:
connect|n1=3,n2=4,s1=color,s2=color,:
connect|n1=5,n2=6,s1=color,s2=color,:
connect|n1=5,n2=7,s1=alpha,s2=color,:
connect|n1=8,n2=9,s1=color,s2=color2,:
connect|n1=10,n2=11,s1=UV,s2=vector,:
connect|n1=11,n2=1,s1=vector,s2=vector,:
connect|n1=11,n2=5,s1=vector,s2=vector,:
connect|n1=11,n2=3,s1=vector,s2=vector,:
connect|n1=11,n2=12,s1=vector,s2=vector,:
connect|n1=13,n2=0,s1=closure,s2=surface,:
connect|n1=14,n2=13,s1=emission,s2=closure2,:";

            if (mainTex != null)
            {
                n += @"connect|n1=9,n2=2,s1=color,s2=base_color,:";
            }

            if (normalTex != null)
            {
                n += @"connect|n1=4,n2=2,s1=normal,s2=normal,:";
                n += @"connect|n1=4,n2=2,s1=normal,s2=clearcoat_normal,:";
            }
            if (metalTex != null)
            {
                n += @"connect|n1=6,n2=2,s1=r,s2=metallic,:";
                n += @"connect|n1=7,n2=2,s1=color,s2=roughness,:";
            }

            if (emissionTex != null)
            {
                n += @"connect|n1=12,n2=14,s1=color,s2=color,:";
            }

            return n;
        }
    }
}