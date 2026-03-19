using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000008 RID: 8
	public class DecalCategory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public DecalCategory(IEnumerable<DecalSpec> categorySpecs)
		{
			foreach (DecalSpec decalSpec in categorySpecs)
			{
				this.TryAdd(decalSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
		public IEnumerable<DecalSpec> CategorySpecs
		{
			get
			{
				return this._categorySpecs.Values;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F9 File Offset: 0x000003F9
		public bool TryAdd(DecalSpec decalSpec)
		{
			return this._categorySpecs.TryAdd(decalSpec.Id, decalSpec);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000220D File Offset: 0x0000040D
		public bool TryGet(string decalId, out DecalSpec decalSpec)
		{
			return this._categorySpecs.TryGetValue(decalId, out decalSpec);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000221C File Offset: 0x0000041C
		public void Remove(string decalId)
		{
			this._categorySpecs.Remove(decalId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000222B File Offset: 0x0000042B
		public Texture2D GetDecalTexture(Decal decal)
		{
			return this._categorySpecs[decal.Id].Texture.Asset;
		}

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, DecalSpec> _categorySpecs = new Dictionary<string, DecalSpec>();
	}
}
