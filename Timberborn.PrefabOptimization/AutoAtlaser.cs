using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000007 RID: 7
	public class AutoAtlaser : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AutoAtlaser(TextureFactory textureFactory, ISpecService specService)
		{
			this._textureFactory = textureFactory;
			this._specService = specService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002121 File Offset: 0x00000321
		public void Load()
		{
			this._keyToSpec = new Dictionary<AutoAtlasKey, AutoAtlasSpec>();
			this._generatedAutoAtlases = new HashSet<AutoAtlasSpec>();
			this._fragments = new Dictionary<AutoAtlasKey, AutoAtlasFragment>();
			this._usages = new Dictionary<AutoAtlasSpec, HashSet<string>>();
			this.PopulateKeyToSpecs();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002158 File Offset: 0x00000358
		public void Unload()
		{
			foreach (AutoAtlasFragment autoAtlasFragment in this._fragments.Values)
			{
				autoAtlasFragment.DestroyTextures();
			}
			this.PrintUsagesIfTooManyAtlases();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B4 File Offset: 0x000003B4
		public bool TryGetAutoAtlasFragment(AutoAtlasKey key, string usageName, out AutoAtlasFragment autoAtlasFragment)
		{
			AutoAtlasSpec autoAtlasSpec;
			if (this._keyToSpec.TryGetValue(key, out autoAtlasSpec))
			{
				if (this._generatedAutoAtlases.Add(autoAtlasSpec))
				{
					this._usages[autoAtlasSpec] = new HashSet<string>();
					this.GenerateAutoAtlasFragments(autoAtlasSpec);
					this.WarnIfTooManyAtlases();
				}
				autoAtlasFragment = this._fragments[key];
				this._usages[autoAtlasSpec].Add(usageName);
				return true;
			}
			autoAtlasFragment = null;
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		public void PopulateKeyToSpecs()
		{
			foreach (AutoAtlasSpec autoAtlasSpec in this._specService.GetSingleSpec<AutoAtlaserSpec>().AutoAtlases)
			{
				foreach (AssetRef<Material> assetRef in autoAtlasSpec.Fragments)
				{
					this._keyToSpec[AutoAtlaser.KeyFromMaterial(assetRef.Asset)] = autoAtlasSpec;
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229C File Offset: 0x0000049C
		public void GenerateAutoAtlasFragments(AutoAtlasSpec autoAtlasSpec)
		{
			ImmutableArray<AssetRef<Material>> fragments = autoAtlasSpec.Fragments;
			if (!fragments.IsEmpty<AssetRef<Material>>())
			{
				int num = AutoAtlaser.CalculateSizeMultiplier(fragments.Length);
				Texture2D combinedMainTex = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.MainTex, new Color32(0, 0, 0, byte.MaxValue), "MainTex");
				Texture2D combinedBumpMap = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.BumpMap, new Color32(128, 128, byte.MaxValue, byte.MaxValue), "BumpMap");
				Texture2D combinedColorMask = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.ColorMask, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), "ColorMask");
				Texture2D combinedAmbientOcclusion = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.AmbientOcclusion, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), "AmbientOcclusion");
				Texture2D combinedMetallicGlossMap = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.MetallicGlossMap, new Color32(0, 0, 0, 0), "MetallicGlossMap");
				Texture2D combinedLightingMap = this.CombineFragments(autoAtlasSpec, num, (EnvironmentMaterialProperties fragment) => fragment.LightingMap, new Color32(0, 0, 0, 0), "LightingMap");
				int num2 = 0;
				int num3 = 0;
				Vector2 uvScale = Vector2.one / (float)num;
				foreach (AssetRef<Material> assetRef in fragments)
				{
					AutoAtlasKey key = AutoAtlaser.KeyFromMaterial(assetRef.Asset);
					Vector2 uvOffset;
					uvOffset..ctor((float)num2 / (float)num, (float)num3 / (float)num);
					AutoAtlasFragment value = new AutoAtlasFragment(autoAtlasSpec.Name, combinedMainTex, combinedBumpMap, combinedColorMask, combinedAmbientOcclusion, combinedMetallicGlossMap, combinedLightingMap, uvScale, uvOffset);
					this._fragments.Add(key, value);
					num2++;
					if (num2 == num)
					{
						num2 = 0;
						num3++;
					}
				}
			}
			this._reusableColorArray.Clear();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024E0 File Offset: 0x000006E0
		public static AutoAtlasKey KeyFromMaterial(Material fragmentMaterial)
		{
			EnvironmentMaterialProperties environmentMaterialProperties = EnvironmentMaterialProperties.FromMaterial(fragmentMaterial);
			return new AutoAtlasKey(AutoAtlaser.NormalizeNull(environmentMaterialProperties.MainTex), AutoAtlaser.NormalizeNull(environmentMaterialProperties.BumpMap), AutoAtlaser.NormalizeNull(environmentMaterialProperties.ColorMask), AutoAtlaser.NormalizeNull(environmentMaterialProperties.AmbientOcclusion), AutoAtlaser.NormalizeNull(environmentMaterialProperties.MetallicGlossMap), AutoAtlaser.NormalizeNull(environmentMaterialProperties.LightingMap));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000253C File Offset: 0x0000073C
		public Texture2D CombineFragments(AutoAtlasSpec autoAtlas, int sizeMultiplier, Func<EnvironmentMaterialProperties, Texture2D> textureSupplier, Color32 defaultColor, string texturePostfix)
		{
			ImmutableArray<AssetRef<Material>> fragments = autoAtlas.Fragments;
			ImmutableArray<Texture2D> immutableArray = (from fragment in fragments
			select textureSupplier(EnvironmentMaterialProperties.FromMaterial(fragment.Asset)) into texture
			where texture
			select texture).ToImmutableArray<Texture2D>();
			ImmutableArray<Vector2Int> immutableArray2 = (from texture in immutableArray
			select new Vector2Int(texture.width, texture.height)).ToImmutableArray<Vector2Int>();
			if (!immutableArray2.AllAreEqual(null))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"All '",
					texturePostfix,
					"' fragment textures  of atlas '",
					autoAtlas.Name,
					"' must be the same size."
				}));
			}
			Vector2Int vector2Int = (immutableArray2.Length > 0) ? immutableArray2[0] : Vector2Int.one;
			Vector2Int vector2Int2 = vector2Int * sizeMultiplier;
			FilterMode filterMode = (immutableArray.Length > 0) ? immutableArray[0].filterMode : 0;
			int anisoLevel = (immutableArray.Length > 0) ? immutableArray[0].anisoLevel : 0;
			TextureSettings textureSettings = new TextureSettings.Builder().SetSize(vector2Int2.x, vector2Int2.y).SetFilterMode(filterMode).SetAnisoLevel(anisoLevel).SetName(autoAtlas.Name + "-" + texturePostfix).Build();
			Texture2D texture2D = this._textureFactory.CreateTexture(textureSettings);
			int num = sizeMultiplier * sizeMultiplier;
			int length = fragments.Length;
			for (int i = 0; i < num; i++)
			{
				Texture2D texture2D2 = (i < length) ? textureSupplier(EnvironmentMaterialProperties.FromMaterial(fragments[i].Asset)) : null;
				Color32[] array = texture2D2 ? texture2D2.GetPixels32() : this._reusableColorArray.Get(vector2Int.x * vector2Int.y, defaultColor);
				int num2 = i % sizeMultiplier;
				int num3 = i / sizeMultiplier;
				texture2D.SetPixels32(num2 * vector2Int.x, num3 * vector2Int.y, vector2Int.x, vector2Int.y, array);
			}
			texture2D.Apply(true, true);
			return texture2D;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002779 File Offset: 0x00000979
		public void WarnIfTooManyAtlases()
		{
			if (this.TooManyAtlases())
			{
				Debug.LogWarning(string.Format("Too many atlases loaded ({0})! This should not happen.", this._generatedAutoAtlases.Count) + " Exit game to print usages.");
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027AC File Offset: 0x000009AC
		public void PrintUsagesIfTooManyAtlases()
		{
			if (this.TooManyAtlases())
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Too many auto atlases loaded! Usages:");
				foreach (KeyValuePair<AutoAtlasSpec, HashSet<string>> keyValuePair in this._usages)
				{
					stringBuilder.AppendLine(keyValuePair.Key.Name);
					foreach (string value in keyValuePair.Value)
					{
						stringBuilder.Append("- ");
						stringBuilder.Append(value);
						stringBuilder.AppendLine();
					}
				}
				Debug.LogWarning(stringBuilder.ToString());
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000288C File Offset: 0x00000A8C
		public bool TooManyAtlases()
		{
			return this._generatedAutoAtlases.Count((AutoAtlasSpec atlas) => atlas.IsUnique) > AutoAtlaser.MaxUniqueAutoAtlases;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028BF File Offset: 0x00000ABF
		public static Texture2D NormalizeNull(Texture2D texture)
		{
			if (!texture)
			{
				return null;
			}
			return texture;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028CC File Offset: 0x00000ACC
		public static int CalculateSizeMultiplier(int numberOfTextures)
		{
			int num = 1;
			while (num * num < numberOfTextures)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000008 RID: 8
		public static readonly int MaxUniqueAutoAtlases = 1;

		// Token: 0x04000009 RID: 9
		public readonly TextureFactory _textureFactory;

		// Token: 0x0400000A RID: 10
		public readonly ISpecService _specService;

		// Token: 0x0400000B RID: 11
		public Dictionary<AutoAtlasKey, AutoAtlasSpec> _keyToSpec;

		// Token: 0x0400000C RID: 12
		public HashSet<AutoAtlasSpec> _generatedAutoAtlases;

		// Token: 0x0400000D RID: 13
		public Dictionary<AutoAtlasKey, AutoAtlasFragment> _fragments;

		// Token: 0x0400000E RID: 14
		public Dictionary<AutoAtlasSpec, HashSet<string>> _usages;

		// Token: 0x0400000F RID: 15
		public readonly ReusableColorArray _reusableColorArray = new ReusableColorArray();
	}
}
