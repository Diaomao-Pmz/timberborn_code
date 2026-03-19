using System;
using Timberborn.CoreUI;
using Timberborn.DecalSystem;
using UnityEngine.UIElements;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x02000006 RID: 6
	public class DecalButtonFactory
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002420 File Offset: 0x00000620
		public DecalButtonFactory(VisualElementLoader visualElementLoader, IDecalService decalService)
		{
			this._visualElementLoader = visualElementLoader;
			this._decalService = decalService;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002438 File Offset: 0x00000638
		public DecalButton CreateButton(Decal decal)
		{
			VisualElement root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DecalButton");
			DecalButton decalButton = new DecalButton(this._decalService, root, decal);
			decalButton.Initialize();
			return decalButton;
		}

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public readonly IDecalService _decalService;
	}
}
