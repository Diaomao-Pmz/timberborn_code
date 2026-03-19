using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.FactionSystem;
using Timberborn.GameFactionSystem;
using Timberborn.MortalComponents;
using UnityEngine;

namespace Timberborn.Beavers
{
	// Token: 0x02000013 RID: 19
	public class BeaverTextureSetter : BaseComponent, IStartableComponent, IDeadNeededComponent
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002976 File Offset: 0x00000B76
		public BeaverTextureSetter(FactionService factionService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._factionService = factionService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000298C File Offset: 0x00000B8C
		public void Start()
		{
			CharacterMaterialModifier component = base.GetComponent<CharacterMaterialModifier>();
			BaseComponent component2 = base.GetComponent<Child>();
			FactionSpec factionSpec = this._factionService.Current;
			ImmutableArray<AssetRef<Texture2D>> immutableArray = component2 ? factionSpec.ChildTextures : factionSpec.Textures;
			AssetRef<Texture2D> enumerableElement = this._randomNumberGenerator.GetEnumerableElement<AssetRef<Texture2D>>(immutableArray);
			component.SetTexture(BeaverTextureSetter.BaseMapId, enumerableElement.Asset);
		}

		// Token: 0x04000025 RID: 37
		public static readonly int BaseMapId = Shader.PropertyToID("_BaseMap");

		// Token: 0x04000026 RID: 38
		public readonly FactionService _factionService;

		// Token: 0x04000027 RID: 39
		public readonly IRandomNumberGenerator _randomNumberGenerator;
	}
}
