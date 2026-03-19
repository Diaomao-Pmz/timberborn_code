using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200000B RID: 11
	public class AutoAtlasFragment
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002ABE File Offset: 0x00000CBE
		public string AtlasName { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public Texture2D CombinedMainTex { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002ACE File Offset: 0x00000CCE
		public Texture2D CombinedBumpMap { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public Texture2D CombinedColorMask { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002ADE File Offset: 0x00000CDE
		public Texture2D CombinedAmbientOcclusion { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public Texture2D CombinedMetallicGlossMap { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AEE File Offset: 0x00000CEE
		public Texture2D CombinedLightingMap { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public Vector2 UVScale { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002AFE File Offset: 0x00000CFE
		public Vector2 UVOffset { get; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002B08 File Offset: 0x00000D08
		public AutoAtlasFragment(string atlasName, Texture2D combinedMainTex, Texture2D combinedBumpMap, Texture2D combinedColorMask, Texture2D combinedAmbientOcclusion, Texture2D combinedMetallicGlossMap, Texture2D combinedLightingMap, Vector2 uvScale, Vector2 uvOffset)
		{
			this.AtlasName = atlasName;
			this.CombinedMainTex = combinedMainTex;
			this.CombinedBumpMap = combinedBumpMap;
			this.CombinedColorMask = combinedColorMask;
			this.CombinedAmbientOcclusion = combinedAmbientOcclusion;
			this.CombinedMetallicGlossMap = combinedMetallicGlossMap;
			this.CombinedLightingMap = combinedLightingMap;
			this.UVScale = uvScale;
			this.UVOffset = uvOffset;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B60 File Offset: 0x00000D60
		public void DestroyTextures()
		{
			Object.Destroy(this.CombinedMainTex);
			Object.Destroy(this.CombinedBumpMap);
			Object.Destroy(this.CombinedColorMask);
			Object.Destroy(this.CombinedAmbientOcclusion);
			Object.Destroy(this.CombinedMetallicGlossMap);
			Object.Destroy(this.CombinedLightingMap);
		}
	}
}
