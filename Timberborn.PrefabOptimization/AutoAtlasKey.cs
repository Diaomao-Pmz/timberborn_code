using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200000E RID: 14
	public struct AutoAtlasKey : IEquatable<AutoAtlasKey>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002EE0 File Offset: 0x000010E0
		public AutoAtlasKey(Texture originalMainTex, Texture originalBumpMap, Texture originalColorMask, Texture originalAmbientOcclusion, Texture originalMetallicGlossMap, Texture originalLightingMap)
		{
			this._originalMainTex = originalMainTex;
			this._originalBumpMap = originalBumpMap;
			this._originalColorMask = originalColorMask;
			this._originalAmbientOcclusion = originalAmbientOcclusion;
			this._originalMetallicGlossMap = originalMetallicGlossMap;
			this._originalLightingMap = originalLightingMap;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F10 File Offset: 0x00001110
		public bool Equals(AutoAtlasKey other)
		{
			return object.Equals(this._originalMainTex, other._originalMainTex) && object.Equals(this._originalBumpMap, other._originalBumpMap) && object.Equals(this._originalColorMask, other._originalColorMask) && object.Equals(this._originalAmbientOcclusion, other._originalAmbientOcclusion) && object.Equals(this._originalMetallicGlossMap, other._originalMetallicGlossMap) && object.Equals(this._originalLightingMap, other._originalLightingMap);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F90 File Offset: 0x00001190
		public override bool Equals(object obj)
		{
			if (obj is AutoAtlasKey)
			{
				AutoAtlasKey other = (AutoAtlasKey)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FB8 File Offset: 0x000011B8
		public override int GetHashCode()
		{
			return ((((((this._originalMainTex != null) ? this._originalMainTex.GetHashCode() : 0) * 397 ^ ((this._originalBumpMap != null) ? this._originalBumpMap.GetHashCode() : 0)) * 397 ^ ((this._originalColorMask != null) ? this._originalColorMask.GetHashCode() : 0)) * 397 ^ ((this._originalAmbientOcclusion != null) ? this._originalAmbientOcclusion.GetHashCode() : 0)) * 397 ^ ((this._originalMetallicGlossMap != null) ? this._originalMetallicGlossMap.GetHashCode() : 0)) * 397 ^ ((this._originalLightingMap != null) ? this._originalLightingMap.GetHashCode() : 0);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003090 File Offset: 0x00001290
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}", "_originalMainTex", this._originalMainTex),
				string.Format(", {0}: {1}", "_originalBumpMap", this._originalBumpMap),
				string.Format(", {0}: {1}", "_originalColorMask", this._originalColorMask),
				string.Format(", {0}: {1}", "_originalAmbientOcclusion", this._originalAmbientOcclusion),
				string.Format(", {0}: {1}", "_originalMetallicGlossMap", this._originalMetallicGlossMap),
				string.Format(", {0}: {1}", "_originalLightingMap", this._originalLightingMap)
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003138 File Offset: 0x00001338
		public static bool operator ==(AutoAtlasKey left, AutoAtlasKey right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003142 File Offset: 0x00001342
		public static bool operator !=(AutoAtlasKey left, AutoAtlasKey right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400002C RID: 44
		public readonly Texture _originalMainTex;

		// Token: 0x0400002D RID: 45
		public readonly Texture _originalBumpMap;

		// Token: 0x0400002E RID: 46
		public readonly Texture _originalColorMask;

		// Token: 0x0400002F RID: 47
		public readonly Texture _originalAmbientOcclusion;

		// Token: 0x04000030 RID: 48
		public readonly Texture _originalMetallicGlossMap;

		// Token: 0x04000031 RID: 49
		public readonly Texture _originalLightingMap;
	}
}
