using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000018 RID: 24
	public class MaterialColorer
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000038F3 File Offset: 0x00001AF3
		public MaterialColorer(ColoredMaterialCache coloredMaterialCache, MaterialLightingEnabler materialLightingEnabler)
		{
			this._coloredMaterialCache = coloredMaterialCache;
			this._materialLightingEnabler = materialLightingEnabler;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000392C File Offset: 0x00001B2C
		public void EnableGrayscale(GameObject root)
		{
			this.SetProperties(root, null, new float?(MaterialColorer.Grayscale), null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000395C File Offset: 0x00001B5C
		public void DisableGrayscale(GameObject root)
		{
			this.SetProperties(root, null, new float?(MaterialColorer.Colored), null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000398C File Offset: 0x00001B8C
		public void SetLightingColor(GameObject root, Color lightingColor)
		{
			this.SetProperties(root, null, null, new Color?(lightingColor));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000039B8 File Offset: 0x00001BB8
		public void SetEmissionColor(BaseComponent entity, Color emissionColor)
		{
			this.SetProperties(entity, new Color?(emissionColor), null, null);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039E4 File Offset: 0x00001BE4
		public void ResetEmissionColor(BaseComponent entity)
		{
			this.SetProperties(entity, new Color?(MaterialColorer.UnhighlightedColor), null, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003A14 File Offset: 0x00001C14
		public void EnableGrayscale(BaseComponent entity)
		{
			this.SetProperties(entity, null, new float?(MaterialColorer.Grayscale), null);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003A44 File Offset: 0x00001C44
		public void SetLightingColor(BaseComponent target, Color lightingColor)
		{
			this.SetProperties(target, null, null, new Color?(lightingColor));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A70 File Offset: 0x00001C70
		public void EnableLighting(BaseComponent entity, float? strength = null)
		{
			this._materialLightingEnabler.EnableLighting(entity, strength);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003A7F File Offset: 0x00001C7F
		public void DisableLighting(BaseComponent entity)
		{
			this._materialLightingEnabler.DisableLighting(entity);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A90 File Offset: 0x00001C90
		public void EnableLightingAndDisableChanges(BaseComponent entity, GameObject root)
		{
			this._materialLightingEnabler.EnableLighting(root, new float?(1f));
			MaterialLightingRenderers component = entity.GetComponent<MaterialLightingRenderers>();
			foreach (MeshRenderer renderer in root.GetComponentsInChildren<MeshRenderer>(true))
			{
				component.DisableRendering(renderer);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003ADC File Offset: 0x00001CDC
		public void SetProperties(BaseComponent entity, Color? color, float? grayscale, Color? lightingColor)
		{
			EntityMaterials component = entity.GetComponent<EntityMaterials>();
			if (component != null)
			{
				this.SetEntityMaterialProperties(component, entity.GameObject, color, grayscale, lightingColor);
				return;
			}
			this.SetCachedMaterialProperties(entity.GameObject, color, grayscale, lightingColor);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003B18 File Offset: 0x00001D18
		public void SetProperties(GameObject root, Color? color, float? grayscale, Color? lightingColor)
		{
			EntityMaterials componentInParentSlow = root.GetComponentInParentSlow<EntityMaterials>();
			if (componentInParentSlow != null)
			{
				this.SetEntityMaterialProperties(componentInParentSlow, root, color, grayscale, lightingColor);
				return;
			}
			this.SetCachedMaterialProperties(root, color, grayscale, lightingColor);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003B48 File Offset: 0x00001D48
		public void SetEntityMaterialProperties(EntityMaterials entityMaterials, GameObject root, Color? color, float? grayscale, Color? lightingColor)
		{
			entityMaterials.GetChildMaterials(root.transform, this._readMaterialCache);
			foreach (Material material in this._readMaterialCache)
			{
				MaterialProperties materialProperties = MaterialColorer.GetMaterialProperties(material, color, grayscale, lightingColor);
				MaterialColorer.ColorMaterial(material, materialProperties);
			}
			this._readMaterialCache.Clear();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public void SetCachedMaterialProperties(GameObject root, Color? color, float? grayscale, Color? lightingColor)
		{
			root.GetComponentsInChildren<MeshRenderer>(true, this._rendererCache);
			foreach (MeshRenderer meshRenderer in this._rendererCache)
			{
				this._writeMaterialCache.Clear();
				meshRenderer.GetSharedMaterials(this._readMaterialCache);
				for (int i = 0; i < this._readMaterialCache.Count; i++)
				{
					Material material = this._readMaterialCache[i];
					MaterialProperties materialProperties = MaterialColorer.GetMaterialProperties(material, color, grayscale, lightingColor);
					Material cachedMaterial = this.GetCachedMaterial(material, materialProperties);
					this._writeMaterialCache.Add(cachedMaterial);
				}
				meshRenderer.SetSharedMaterials(this._writeMaterialCache);
				this._readMaterialCache.Clear();
			}
			this._rendererCache.Clear();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public static MaterialProperties GetMaterialProperties(Material material, Color? color, float? grayscale, Color? lightingColor)
		{
			Color color2 = color ?? MaterialColorer.GetEmissionColor(material, MaterialColorer.UnhighlightedColor);
			float grayscale2 = grayscale ?? MaterialColorer.GetGrayscale(material, MaterialColorer.Colored);
			Color lightingColor2 = lightingColor ?? MaterialColorer.GetLightingColor(material, MaterialColorer.DefaultLightingColor);
			return new MaterialProperties(color2, grayscale2, lightingColor2);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003D14 File Offset: 0x00001F14
		public Material GetCachedMaterial(Material material, MaterialProperties materialProperties)
		{
			bool flag;
			Material cachedMaterial = this._coloredMaterialCache.GetCachedMaterial(material, materialProperties, out flag);
			if (flag)
			{
				MaterialColorer.ColorMaterial(cachedMaterial, materialProperties);
			}
			return cachedMaterial;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static void ColorMaterial(Material material, MaterialProperties materialProperties)
		{
			material.SetColor(MaterialColorer.EmissionColorProperty, materialProperties.Color);
			material.SetFloat(MaterialColorer.GrayscaleProperty, materialProperties.Grayscale);
			material.SetColor(MaterialColorer.LightingColorProperty, materialProperties.LightingColor);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003D74 File Offset: 0x00001F74
		public static Color GetEmissionColor(Material material, Color defaultColor)
		{
			if (!MaterialColorer.MaterialSupportsEmission(material))
			{
				return defaultColor;
			}
			return material.GetColor(MaterialColorer.EmissionColorProperty);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003D8B File Offset: 0x00001F8B
		public static float GetGrayscale(Material material, float defaultGrayscale)
		{
			if (!material.HasProperty(MaterialColorer.GrayscaleProperty))
			{
				return defaultGrayscale;
			}
			return material.GetFloat(MaterialColorer.GrayscaleProperty);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003DA7 File Offset: 0x00001FA7
		public static Color GetLightingColor(Material material, Color defaultLightingColor)
		{
			if (!material.HasProperty(MaterialColorer.LightingColorProperty))
			{
				return defaultLightingColor;
			}
			return material.GetColor(MaterialColorer.LightingColorProperty);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public static bool MaterialSupportsEmission(Material material)
		{
			return material.HasProperty(MaterialColorer.EmissionColorProperty);
		}

		// Token: 0x0400003D RID: 61
		public static readonly int EmissionColorProperty = Shader.PropertyToID("_EmissionColor");

		// Token: 0x0400003E RID: 62
		public static readonly int GrayscaleProperty = Shader.PropertyToID("_Grayscale");

		// Token: 0x0400003F RID: 63
		public static readonly int LightingColorProperty = Shader.PropertyToID("_LightingColor");

		// Token: 0x04000040 RID: 64
		public static readonly Color UnhighlightedColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04000041 RID: 65
		public static readonly float Grayscale = 1f;

		// Token: 0x04000042 RID: 66
		public static readonly float Colored = 0f;

		// Token: 0x04000043 RID: 67
		public static readonly Color DefaultLightingColor = Color.white;

		// Token: 0x04000044 RID: 68
		public readonly List<MeshRenderer> _rendererCache = new List<MeshRenderer>();

		// Token: 0x04000045 RID: 69
		public readonly List<Material> _readMaterialCache = new List<Material>();

		// Token: 0x04000046 RID: 70
		public readonly List<Material> _writeMaterialCache = new List<Material>();

		// Token: 0x04000047 RID: 71
		public readonly ColoredMaterialCache _coloredMaterialCache;

		// Token: 0x04000048 RID: 72
		public readonly MaterialLightingEnabler _materialLightingEnabler;
	}
}
