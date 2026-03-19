using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000025 RID: 37
	public class VegetationMaterialProperties : IMaterialProperties, IEquatable<VegetationMaterialProperties>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005362 File Offset: 0x00003562
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(VegetationMaterialProperties);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000536E File Offset: 0x0000356E
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005376 File Offset: 0x00003576
		public Color32 Color { get; private set; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00005380 File Offset: 0x00003580
		public static VegetationMaterialProperties FromMaterial(Material material)
		{
			return new VegetationMaterialProperties
			{
				Color = material.GetColor(VegetationMaterialProperties.ColorId),
				_mainTex = (Texture2D)material.GetTexture(VegetationMaterialProperties.MainTexId),
				_metallicGlossMap = (Texture2D)material.GetTexture(VegetationMaterialProperties.MetallicGlossMapId),
				_bumpMap = (Texture2D)material.GetTexture(VegetationMaterialProperties.BumpMapId),
				_detailMap = (Texture2D)material.GetTexture(VegetationMaterialProperties.DetailMapId),
				_emissionColor = material.GetColor(VegetationMaterialProperties.EmissionColorId),
				_useEmission = material.IsKeywordEnabled(VegetationMaterialProperties.UseEmissionKeyword),
				_windModifier = material.GetFloat(VegetationMaterialProperties.WindModifierId),
				_swayStrength = material.GetFloat(VegetationMaterialProperties.SwayStrengthId),
				_swaySpeed = material.GetFloat(VegetationMaterialProperties.SwaySpeedId),
				_swayExponent = material.GetFloat(VegetationMaterialProperties.SwayExponentId),
				_flutterStrength = material.GetFloat(VegetationMaterialProperties.FlutterStrengthId),
				_flutterSpeed = material.GetFloat(VegetationMaterialProperties.FlutterSpeedId),
				_flutterExponent = material.GetFloat(VegetationMaterialProperties.FlutterExponentId),
				_flutterThreshold = material.GetFloat(VegetationMaterialProperties.FlutterThresholdId),
				_detailUseColor = material.GetFloat(VegetationMaterialProperties.DetailUseColorId),
				_detailColorBoost = material.GetFloat(VegetationMaterialProperties.DetailColorBoostId),
				_enableDetail = material.GetFloat(VegetationMaterialProperties.EnableDetailId),
				_enableInstancing = material.enableInstancing
			};
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000054F0 File Offset: 0x000036F0
		public void ApplyToMaterial(Material material)
		{
			material.SetColor(VegetationMaterialProperties.ColorId, this.Color);
			material.SetTexture(VegetationMaterialProperties.MainTexId, this._mainTex);
			material.SetTexture(VegetationMaterialProperties.BumpMapId, this._bumpMap);
			material.SetTexture(VegetationMaterialProperties.MetallicGlossMapId, this._metallicGlossMap);
			material.SetTexture(VegetationMaterialProperties.DetailMapId, this._detailMap);
			material.SetColor(VegetationMaterialProperties.EmissionColorId, this._emissionColor);
			LocalKeyword localKeyword = new LocalKeyword(material.shader, VegetationMaterialProperties.UseEmissionKeyword);
			material.SetKeyword(ref localKeyword, this._useEmission);
			material.SetFloat(VegetationMaterialProperties.WindModifierId, this._windModifier);
			material.SetFloat(VegetationMaterialProperties.SwayStrengthId, this._swayStrength);
			material.SetFloat(VegetationMaterialProperties.SwaySpeedId, this._swaySpeed);
			material.SetFloat(VegetationMaterialProperties.SwayExponentId, this._swayExponent);
			material.SetFloat(VegetationMaterialProperties.FlutterStrengthId, this._flutterStrength);
			material.SetFloat(VegetationMaterialProperties.FlutterSpeedId, this._flutterSpeed);
			material.SetFloat(VegetationMaterialProperties.FlutterExponentId, this._flutterExponent);
			material.SetFloat(VegetationMaterialProperties.FlutterThresholdId, this._flutterThreshold);
			material.SetFloat(VegetationMaterialProperties.DetailUseColorId, this._detailUseColor);
			material.SetFloat(VegetationMaterialProperties.DetailColorBoostId, this._detailColorBoost);
			material.SetFloat(VegetationMaterialProperties.EnableDetailId, this._enableDetail);
			material.enableInstancing = this._enableInstancing;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005653 File Offset: 0x00003853
		public IMaterialProperties GetWithoutColor()
		{
			VegetationMaterialProperties vegetationMaterialProperties = this.<Clone>$();
			vegetationMaterialProperties.Color = UnityEngine.Color.white;
			return vegetationMaterialProperties;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000566C File Offset: 0x0000386C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("VegetationMaterialProperties");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000056B8 File Offset: 0x000038B8
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Color = ");
			builder.Append(this.Color.ToString());
			return true;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000056F2 File Offset: 0x000038F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(VegetationMaterialProperties left, VegetationMaterialProperties right)
		{
			return !(left == right);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000056FE File Offset: 0x000038FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(VegetationMaterialProperties left, VegetationMaterialProperties right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005714 File Offset: 0x00003914
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color32>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this._mainTex)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this._bumpMap)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this._metallicGlossMap)) * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this._detailMap)) * -1521134295 + EqualityComparer<Color32>.Default.GetHashCode(this._emissionColor)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this._useEmission)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._windModifier)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._swayStrength)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._swaySpeed)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._swayExponent)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._flutterStrength)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._flutterSpeed)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._flutterExponent)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._flutterThreshold)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._detailUseColor)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._detailColorBoost)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this._enableDetail)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this._enableInstancing);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000058E6 File Offset: 0x00003AE6
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VegetationMaterialProperties);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000058F4 File Offset: 0x00003AF4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(VegetationMaterialProperties other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color32>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<Texture2D>.Default.Equals(this._mainTex, other._mainTex) && EqualityComparer<Texture2D>.Default.Equals(this._bumpMap, other._bumpMap) && EqualityComparer<Texture2D>.Default.Equals(this._metallicGlossMap, other._metallicGlossMap) && EqualityComparer<Texture2D>.Default.Equals(this._detailMap, other._detailMap) && EqualityComparer<Color32>.Default.Equals(this._emissionColor, other._emissionColor) && EqualityComparer<bool>.Default.Equals(this._useEmission, other._useEmission) && EqualityComparer<float>.Default.Equals(this._windModifier, other._windModifier) && EqualityComparer<float>.Default.Equals(this._swayStrength, other._swayStrength) && EqualityComparer<float>.Default.Equals(this._swaySpeed, other._swaySpeed) && EqualityComparer<float>.Default.Equals(this._swayExponent, other._swayExponent) && EqualityComparer<float>.Default.Equals(this._flutterStrength, other._flutterStrength) && EqualityComparer<float>.Default.Equals(this._flutterSpeed, other._flutterSpeed) && EqualityComparer<float>.Default.Equals(this._flutterExponent, other._flutterExponent) && EqualityComparer<float>.Default.Equals(this._flutterThreshold, other._flutterThreshold) && EqualityComparer<float>.Default.Equals(this._detailUseColor, other._detailUseColor) && EqualityComparer<float>.Default.Equals(this._detailColorBoost, other._detailColorBoost) && EqualityComparer<float>.Default.Equals(this._enableDetail, other._enableDetail) && EqualityComparer<bool>.Default.Equals(this._enableInstancing, other._enableInstancing));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005B20 File Offset: 0x00003D20
		[CompilerGenerated]
		protected VegetationMaterialProperties([Nullable(1)] VegetationMaterialProperties original)
		{
			this.Color = original.<Color>k__BackingField;
			this._mainTex = original._mainTex;
			this._bumpMap = original._bumpMap;
			this._metallicGlossMap = original._metallicGlossMap;
			this._detailMap = original._detailMap;
			this._emissionColor = original._emissionColor;
			this._useEmission = original._useEmission;
			this._windModifier = original._windModifier;
			this._swayStrength = original._swayStrength;
			this._swaySpeed = original._swaySpeed;
			this._swayExponent = original._swayExponent;
			this._flutterStrength = original._flutterStrength;
			this._flutterSpeed = original._flutterSpeed;
			this._flutterExponent = original._flutterExponent;
			this._flutterThreshold = original._flutterThreshold;
			this._detailUseColor = original._detailUseColor;
			this._detailColorBoost = original._detailColorBoost;
			this._enableDetail = original._enableDetail;
			this._enableInstancing = original._enableInstancing;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000020F8 File Offset: 0x000002F8
		public VegetationMaterialProperties()
		{
		}

		// Token: 0x04000092 RID: 146
		public static readonly string UseEmissionKeyword = "_USE_EMISSION";

		// Token: 0x04000093 RID: 147
		public static readonly int ColorId = Shader.PropertyToID("_Color");

		// Token: 0x04000094 RID: 148
		public static readonly int MainTexId = Shader.PropertyToID("_MainTex");

		// Token: 0x04000095 RID: 149
		public static readonly int MetallicGlossMapId = Shader.PropertyToID("_MetallicGlossMap");

		// Token: 0x04000096 RID: 150
		public static readonly int BumpMapId = Shader.PropertyToID("_BumpMap");

		// Token: 0x04000097 RID: 151
		public static readonly int DetailMapId = Shader.PropertyToID("_DetailMap");

		// Token: 0x04000098 RID: 152
		public static readonly int EmissionColorId = Shader.PropertyToID("_EmissionColor");

		// Token: 0x04000099 RID: 153
		public static readonly int WindModifierId = Shader.PropertyToID("_WindModifier");

		// Token: 0x0400009A RID: 154
		public static readonly int SwayStrengthId = Shader.PropertyToID("_SwayStrength");

		// Token: 0x0400009B RID: 155
		public static readonly int SwaySpeedId = Shader.PropertyToID("_SwaySpeed");

		// Token: 0x0400009C RID: 156
		public static readonly int SwayExponentId = Shader.PropertyToID("_SwayExponent");

		// Token: 0x0400009D RID: 157
		public static readonly int FlutterStrengthId = Shader.PropertyToID("_FlutterStrength");

		// Token: 0x0400009E RID: 158
		public static readonly int FlutterSpeedId = Shader.PropertyToID("_FlutterSpeed");

		// Token: 0x0400009F RID: 159
		public static readonly int FlutterExponentId = Shader.PropertyToID("_FlutterExponent");

		// Token: 0x040000A0 RID: 160
		public static readonly int FlutterThresholdId = Shader.PropertyToID("_FlutterThreshold");

		// Token: 0x040000A1 RID: 161
		public static readonly int DetailUseColorId = Shader.PropertyToID("_DetailUseColor");

		// Token: 0x040000A2 RID: 162
		public static readonly int DetailColorBoostId = Shader.PropertyToID("_DetailColorBoost");

		// Token: 0x040000A3 RID: 163
		public static readonly int EnableDetailId = Shader.PropertyToID("_EnableDetail");

		// Token: 0x040000A5 RID: 165
		public Texture2D _mainTex;

		// Token: 0x040000A6 RID: 166
		public Texture2D _bumpMap;

		// Token: 0x040000A7 RID: 167
		public Texture2D _metallicGlossMap;

		// Token: 0x040000A8 RID: 168
		public Texture2D _detailMap;

		// Token: 0x040000A9 RID: 169
		public Color32 _emissionColor;

		// Token: 0x040000AA RID: 170
		public bool _useEmission;

		// Token: 0x040000AB RID: 171
		public float _windModifier;

		// Token: 0x040000AC RID: 172
		public float _swayStrength;

		// Token: 0x040000AD RID: 173
		public float _swaySpeed;

		// Token: 0x040000AE RID: 174
		public float _swayExponent;

		// Token: 0x040000AF RID: 175
		public float _flutterStrength;

		// Token: 0x040000B0 RID: 176
		public float _flutterSpeed;

		// Token: 0x040000B1 RID: 177
		public float _flutterExponent;

		// Token: 0x040000B2 RID: 178
		public float _flutterThreshold;

		// Token: 0x040000B3 RID: 179
		public float _detailUseColor;

		// Token: 0x040000B4 RID: 180
		public float _detailColorBoost;

		// Token: 0x040000B5 RID: 181
		public float _enableDetail;

		// Token: 0x040000B6 RID: 182
		public bool _enableInstancing;
	}
}
