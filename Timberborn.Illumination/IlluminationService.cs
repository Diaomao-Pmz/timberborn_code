using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x02000010 RID: 16
	public class IlluminationService : ILoadableSingleton
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002C5D File Offset: 0x00000E5D
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002C65 File Offset: 0x00000E65
		public Color DefaultColor { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002C6E File Offset: 0x00000E6E
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002C76 File Offset: 0x00000E76
		public ImmutableArray<Color> PresetColors { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002C7F File Offset: 0x00000E7F
		public IlluminationService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002C90 File Offset: 0x00000E90
		public void Load()
		{
			IlluminationServiceSpec singleSpec = this._specService.GetSingleSpec<IlluminationServiceSpec>();
			this._iconExponent = singleSpec.IconExponent;
			this._iconMultiplier = singleSpec.IconMultiplier;
			ImmutableArray<IlluminationColorSpec> immutableArray = this.LoadUniqueColors();
			this._colorsById = immutableArray.ToFrozenDictionary((IlluminationColorSpec spec) => spec.Id, (IlluminationColorSpec spec) => spec.Color, null);
			this.PresetColors = (from spec in immutableArray
			select spec.GetSpec<IlluminationPresetSpec>() into spec
			where spec != null
			orderby spec.Order
			select spec.GetSpec<IlluminationColorSpec>().Color).ToImmutableArray<Color>();
			this.DefaultColor = this.FindColorById(singleSpec.DefaultColorId);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public Color FindColorById(string id)
		{
			Color result;
			if (this._colorsById.TryGetValue(id, out result))
			{
				return result;
			}
			Debug.LogWarning("IlluminationColorSpec with id '" + id + "' does not exist!");
			return Color.white;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E04 File Offset: 0x00001004
		public Color LightingColorToIconColor(Color lightingColor)
		{
			float num;
			float num2;
			float num3;
			Color.RGBToHSV(lightingColor, ref num, ref num2, ref num3);
			return Color.HSVToRGB(num, Mathf.Pow(num2, this._iconExponent) * this._iconMultiplier, 1f);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E3C File Offset: 0x0000103C
		public ImmutableArray<IlluminationColorSpec> LoadUniqueColors()
		{
			Dictionary<string, IlluminationColorSpec> dictionary = new Dictionary<string, IlluminationColorSpec>();
			foreach (IlluminationColorSpec illuminationColorSpec in this._specService.GetSpecs<IlluminationColorSpec>())
			{
				IlluminationColorSpec illuminationColorSpec2;
				if (dictionary.TryGetValue(illuminationColorSpec.Id, out illuminationColorSpec2))
				{
					Debug.LogWarning("IlluminationColorSpec with id '" + illuminationColorSpec.Id + "' already exists." + string.Format(" Replacing {0} with {1}.", illuminationColorSpec2.Color, illuminationColorSpec.Color));
				}
				dictionary[illuminationColorSpec.Id] = illuminationColorSpec;
			}
			return dictionary.Values.ToImmutableArray<IlluminationColorSpec>();
		}

		// Token: 0x0400001F RID: 31
		public readonly ISpecService _specService;

		// Token: 0x04000020 RID: 32
		public float _iconExponent;

		// Token: 0x04000021 RID: 33
		public float _iconMultiplier;

		// Token: 0x04000022 RID: 34
		public FrozenDictionary<string, Color> _colorsById;
	}
}
