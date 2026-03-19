using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000012 RID: 18
	public class EnvironmentMaterialProperties : IMaterialProperties, IEquatable<EnvironmentMaterialProperties>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003429 File Offset: 0x00001629
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(EnvironmentMaterialProperties);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003435 File Offset: 0x00001635
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000343D File Offset: 0x0000163D
		public Color32 Color { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003446 File Offset: 0x00001646
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000344E File Offset: 0x0000164E
		public Texture2D MainTex { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003457 File Offset: 0x00001657
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000345F File Offset: 0x0000165F
		public Texture2D BumpMap { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003468 File Offset: 0x00001668
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003470 File Offset: 0x00001670
		public Texture2D ColorMask { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003479 File Offset: 0x00001679
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003481 File Offset: 0x00001681
		public Texture2D AmbientOcclusion { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000348A File Offset: 0x0000168A
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003492 File Offset: 0x00001692
		public Texture2D MetallicGlossMap { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000349B File Offset: 0x0000169B
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000034A3 File Offset: 0x000016A3
		public Texture2D LightingMap { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000034AC File Offset: 0x000016AC
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000034B4 File Offset: 0x000016B4
		private bool CutoutWithAlpha { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000034BD File Offset: 0x000016BD
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000034C5 File Offset: 0x000016C5
		private float Cutoff { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000034CE File Offset: 0x000016CE
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000034D6 File Offset: 0x000016D6
		private Texture2D CutoutTex { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000034DF File Offset: 0x000016DF
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000034E7 File Offset: 0x000016E7
		private Texture2D DetailAlbedoMap { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000034F8 File Offset: 0x000016F8
		private Texture2D DetailAlbedoMap2 { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003501 File Offset: 0x00001701
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003509 File Offset: 0x00001709
		private Texture2D DetailAlbedoMap3 { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003512 File Offset: 0x00001712
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000351A File Offset: 0x0000171A
		private Color32 DetailAlbedoColor2 { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003523 File Offset: 0x00001723
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000352B File Offset: 0x0000172B
		private float DetailAlbedoGradient3 { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000353C File Offset: 0x0000173C
		private Color32 EmissionColor { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003545 File Offset: 0x00001745
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000354D File Offset: 0x0000174D
		private bool MainUVFromCoordinates { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003556 File Offset: 0x00001756
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000355E File Offset: 0x0000175E
		private float MainUVFromCoordinatesScale { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003567 File Offset: 0x00001767
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000356F File Offset: 0x0000176F
		private bool EnableInstancing { get; set; }

		// Token: 0x06000089 RID: 137 RVA: 0x00003578 File Offset: 0x00001778
		public static EnvironmentMaterialProperties FromMaterial(Material material)
		{
			return new EnvironmentMaterialProperties
			{
				Color = material.GetColor(EnvironmentMaterialProperties.ColorProperty),
				MainTex = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.MainTexProperty),
				ColorMask = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.ColorMaskProperty),
				AmbientOcclusion = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.AmbientOcclusionProperty),
				MetallicGlossMap = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.MetallicGlossMapProperty),
				LightingMap = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.LightingMapProperty),
				CutoutWithAlpha = (material.GetFloat(EnvironmentMaterialProperties.CutoutWithAlphaProperty) > 0.5f),
				CutoutTex = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.CutoutTexProperty),
				Cutoff = material.GetFloat(EnvironmentMaterialProperties.CutoffProperty),
				BumpMap = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.BumpMapProperty),
				DetailAlbedoMap = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.DetailAlbedoMapProperty),
				DetailAlbedoMap2 = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.DetailAlbedoMap2Property),
				DetailAlbedoMap3 = (Texture2D)material.GetTexture(EnvironmentMaterialProperties.DetailAlbedoMap3Property),
				DetailAlbedoColor2 = material.GetColor(EnvironmentMaterialProperties.DetailAlbedoColor2Property),
				DetailAlbedoGradient3 = material.GetFloat(EnvironmentMaterialProperties.DetailAlbedoGradient3Property),
				EmissionColor = material.GetColor(EnvironmentMaterialProperties.EmissionColorProperty),
				MainUVFromCoordinates = (material.GetFloat(EnvironmentMaterialProperties.MainUVFromCoordinatesProperty) > 0.5f),
				MainUVFromCoordinatesScale = material.GetFloat(EnvironmentMaterialProperties.MainUVFromCoordinatesScaleProperty),
				EnableInstancing = material.enableInstancing
			};
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003718 File Offset: 0x00001918
		public void ApplyToMaterial(Material material)
		{
			material.SetColor(EnvironmentMaterialProperties.ColorProperty, this.Color);
			material.SetTexture(EnvironmentMaterialProperties.MainTexProperty, this.MainTex);
			material.SetTexture(EnvironmentMaterialProperties.BumpMapProperty, this.BumpMap);
			material.SetTexture(EnvironmentMaterialProperties.ColorMaskProperty, this.ColorMask);
			material.SetTexture(EnvironmentMaterialProperties.AmbientOcclusionProperty, this.AmbientOcclusion);
			material.SetTexture(EnvironmentMaterialProperties.MetallicGlossMapProperty, this.MetallicGlossMap);
			material.SetTexture(EnvironmentMaterialProperties.LightingMapProperty, this.LightingMap);
			material.SetFloat(EnvironmentMaterialProperties.CutoutWithAlphaProperty, (float)(this.CutoutWithAlpha ? 1 : 0));
			material.SetTexture(EnvironmentMaterialProperties.CutoutTexProperty, this.CutoutTex);
			material.SetFloat(EnvironmentMaterialProperties.CutoffProperty, this.Cutoff);
			material.SetTexture(EnvironmentMaterialProperties.DetailAlbedoMapProperty, this.DetailAlbedoMap);
			material.SetTexture(EnvironmentMaterialProperties.DetailAlbedoMap2Property, this.DetailAlbedoMap2);
			material.SetTexture(EnvironmentMaterialProperties.DetailAlbedoMap3Property, this.DetailAlbedoMap3);
			material.SetColor(EnvironmentMaterialProperties.DetailAlbedoColor2Property, this.DetailAlbedoColor2);
			material.SetFloat(EnvironmentMaterialProperties.DetailAlbedoGradient3Property, this.DetailAlbedoGradient3);
			material.SetColor(EnvironmentMaterialProperties.EmissionColorProperty, this.EmissionColor);
			material.SetFloat(EnvironmentMaterialProperties.MainUVFromCoordinatesProperty, (float)(this.MainUVFromCoordinates ? 1 : 0));
			material.SetFloat(EnvironmentMaterialProperties.MainUVFromCoordinatesScaleProperty, this.MainUVFromCoordinatesScale);
			material.enableInstancing = this.EnableInstancing;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003880 File Offset: 0x00001A80
		public IMaterialProperties GetWithoutColor()
		{
			EnvironmentMaterialProperties environmentMaterialProperties = this.<Clone>$();
			environmentMaterialProperties.Color = UnityEngine.Color.white;
			return environmentMaterialProperties;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003898 File Offset: 0x00001A98
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnvironmentMaterialProperties");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000038E4 File Offset: 0x00001AE4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Color = ");
			builder.Append(this.Color.ToString());
			builder.Append(", MainTex = ");
			builder.Append(this.MainTex);
			builder.Append(", BumpMap = ");
			builder.Append(this.BumpMap);
			builder.Append(", ColorMask = ");
			builder.Append(this.ColorMask);
			builder.Append(", AmbientOcclusion = ");
			builder.Append(this.AmbientOcclusion);
			builder.Append(", MetallicGlossMap = ");
			builder.Append(this.MetallicGlossMap);
			builder.Append(", LightingMap = ");
			builder.Append(this.LightingMap);
			return true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039B4 File Offset: 0x00001BB4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnvironmentMaterialProperties left, EnvironmentMaterialProperties right)
		{
			return !(left == right);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039C0 File Offset: 0x00001BC0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnvironmentMaterialProperties left, EnvironmentMaterialProperties right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039D4 File Offset: 0x00001BD4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color32>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<MainTex>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<BumpMap>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<ColorMask>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<AmbientOcclusion>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<MetallicGlossMap>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<LightingMap>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CutoutWithAlpha>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Cutoff>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<CutoutTex>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<DetailAlbedoMap>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<DetailAlbedoMap2>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.<DetailAlbedoMap3>k__BackingField)) * -1521134295 + EqualityComparer<Color32>.Default.GetHashCode(this.<DetailAlbedoColor2>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DetailAlbedoGradient3>k__BackingField)) * -1521134295 + EqualityComparer<Color32>.Default.GetHashCode(this.<EmissionColor>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<MainUVFromCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MainUVFromCoordinatesScale>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<EnableInstancing>k__BackingField);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003BA6 File Offset: 0x00001DA6
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnvironmentMaterialProperties);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003BB4 File Offset: 0x00001DB4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnvironmentMaterialProperties other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color32>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<MainTex>k__BackingField, other.<MainTex>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<BumpMap>k__BackingField, other.<BumpMap>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<ColorMask>k__BackingField, other.<ColorMask>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<AmbientOcclusion>k__BackingField, other.<AmbientOcclusion>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<MetallicGlossMap>k__BackingField, other.<MetallicGlossMap>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<LightingMap>k__BackingField, other.<LightingMap>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CutoutWithAlpha>k__BackingField, other.<CutoutWithAlpha>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Cutoff>k__BackingField, other.<Cutoff>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<CutoutTex>k__BackingField, other.<CutoutTex>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<DetailAlbedoMap>k__BackingField, other.<DetailAlbedoMap>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<DetailAlbedoMap2>k__BackingField, other.<DetailAlbedoMap2>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this.<DetailAlbedoMap3>k__BackingField, other.<DetailAlbedoMap3>k__BackingField) && EqualityComparer<Color32>.Default.Equals(this.<DetailAlbedoColor2>k__BackingField, other.<DetailAlbedoColor2>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DetailAlbedoGradient3>k__BackingField, other.<DetailAlbedoGradient3>k__BackingField) && EqualityComparer<Color32>.Default.Equals(this.<EmissionColor>k__BackingField, other.<EmissionColor>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<MainUVFromCoordinates>k__BackingField, other.<MainUVFromCoordinates>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MainUVFromCoordinatesScale>k__BackingField, other.<MainUVFromCoordinatesScale>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<EnableInstancing>k__BackingField, other.<EnableInstancing>k__BackingField));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[CompilerGenerated]
		protected EnvironmentMaterialProperties([Nullable(1)] EnvironmentMaterialProperties original)
		{
			this.Color = original.<Color>k__BackingField;
			this.MainTex = original.<MainTex>k__BackingField;
			this.BumpMap = original.<BumpMap>k__BackingField;
			this.ColorMask = original.<ColorMask>k__BackingField;
			this.AmbientOcclusion = original.<AmbientOcclusion>k__BackingField;
			this.MetallicGlossMap = original.<MetallicGlossMap>k__BackingField;
			this.LightingMap = original.<LightingMap>k__BackingField;
			this.CutoutWithAlpha = original.<CutoutWithAlpha>k__BackingField;
			this.Cutoff = original.<Cutoff>k__BackingField;
			this.CutoutTex = original.<CutoutTex>k__BackingField;
			this.DetailAlbedoMap = original.<DetailAlbedoMap>k__BackingField;
			this.DetailAlbedoMap2 = original.<DetailAlbedoMap2>k__BackingField;
			this.DetailAlbedoMap3 = original.<DetailAlbedoMap3>k__BackingField;
			this.DetailAlbedoColor2 = original.<DetailAlbedoColor2>k__BackingField;
			this.DetailAlbedoGradient3 = original.<DetailAlbedoGradient3>k__BackingField;
			this.EmissionColor = original.<EmissionColor>k__BackingField;
			this.MainUVFromCoordinates = original.<MainUVFromCoordinates>k__BackingField;
			this.MainUVFromCoordinatesScale = original.<MainUVFromCoordinatesScale>k__BackingField;
			this.EnableInstancing = original.<EnableInstancing>k__BackingField;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000020F8 File Offset: 0x000002F8
		public EnvironmentMaterialProperties()
		{
		}

		// Token: 0x04000037 RID: 55
		public static readonly int ColorProperty = Shader.PropertyToID("_Color");

		// Token: 0x04000038 RID: 56
		public static readonly int MainTexProperty = Shader.PropertyToID("_MainTex");

		// Token: 0x04000039 RID: 57
		public static readonly int ColorMaskProperty = Shader.PropertyToID("_ColorMask");

		// Token: 0x0400003A RID: 58
		public static readonly int AmbientOcclusionProperty = Shader.PropertyToID("_AmbientOcclusion");

		// Token: 0x0400003B RID: 59
		public static readonly int MetallicGlossMapProperty = Shader.PropertyToID("_MetallicGlossMap");

		// Token: 0x0400003C RID: 60
		public static readonly int LightingMapProperty = Shader.PropertyToID("_LightingMap");

		// Token: 0x0400003D RID: 61
		public static readonly int CutoutWithAlphaProperty = Shader.PropertyToID("_CutoutWithAlpha");

		// Token: 0x0400003E RID: 62
		public static readonly int CutoutTexProperty = Shader.PropertyToID("_CutoutTex");

		// Token: 0x0400003F RID: 63
		public static readonly int CutoffProperty = Shader.PropertyToID("_Cutoff");

		// Token: 0x04000040 RID: 64
		public static readonly int BumpMapProperty = Shader.PropertyToID("_BumpMap");

		// Token: 0x04000041 RID: 65
		public static readonly int DetailAlbedoMapProperty = Shader.PropertyToID("_DetailAlbedoMap");

		// Token: 0x04000042 RID: 66
		public static readonly int DetailAlbedoMap2Property = Shader.PropertyToID("_DetailAlbedoMap2");

		// Token: 0x04000043 RID: 67
		public static readonly int DetailAlbedoMap3Property = Shader.PropertyToID("_DetailAlbedoMap3");

		// Token: 0x04000044 RID: 68
		public static readonly int DetailAlbedoColor2Property = Shader.PropertyToID("_DetailAlbedoUV2Color");

		// Token: 0x04000045 RID: 69
		public static readonly int DetailAlbedoGradient3Property = Shader.PropertyToID("_DetailAlbedoUV3Gradient");

		// Token: 0x04000046 RID: 70
		public static readonly int EmissionColorProperty = Shader.PropertyToID("_EmissionColor");

		// Token: 0x04000047 RID: 71
		public static readonly int MainUVFromCoordinatesProperty = Shader.PropertyToID("_MainUVFromCoordinates");

		// Token: 0x04000048 RID: 72
		public static readonly int MainUVFromCoordinatesScaleProperty = Shader.PropertyToID("_MainUVFromCoordinatesScale");
	}
}
