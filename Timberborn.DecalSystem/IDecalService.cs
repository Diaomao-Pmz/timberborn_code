using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000015 RID: 21
	public interface IDecalService
	{
		// Token: 0x06000087 RID: 135
		IEnumerable<Decal> GetDecals(string category);

		// Token: 0x06000088 RID: 136
		Decal GetValidatedDecal(Decal decal);

		// Token: 0x06000089 RID: 137
		Texture2D GetDecalTexture(Decal decal);

		// Token: 0x0600008A RID: 138
		void ReloadCustomDecals(string category);
	}
}
