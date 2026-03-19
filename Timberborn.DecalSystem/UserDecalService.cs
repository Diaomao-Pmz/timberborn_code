using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000016 RID: 22
	public class UserDecalService
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003134 File Offset: 0x00001334
		public UserDecalService(UserDecalTextureRepository userDecalTextureRepository)
		{
			this._userDecalTextureRepository = userDecalTextureRepository;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003143 File Offset: 0x00001343
		public IEnumerable<DecalSpec> GetCustomDecals(string category)
		{
			IEnumerable<Texture2D> enumerable = this._userDecalTextureRepository.LoadCustomTextures(category);
			foreach (Texture2D texture2D in enumerable)
			{
				yield return new DecalSpec
				{
					FactionId = string.Empty,
					Category = category,
					Texture = new AssetRef<Texture2D>(string.Empty, new Lazy<Texture2D>(texture2D))
				};
			}
			IEnumerator<Texture2D> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400002E RID: 46
		public readonly UserDecalTextureRepository _userDecalTextureRepository;
	}
}
