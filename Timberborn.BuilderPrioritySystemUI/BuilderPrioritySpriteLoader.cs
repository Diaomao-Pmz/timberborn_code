using System;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using UnityEngine;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x0200000C RID: 12
	public class BuilderPrioritySpriteLoader : IPrioritySpriteLoader
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002553 File Offset: 0x00000753
		public BuilderPrioritySpriteLoader(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002562 File Offset: 0x00000762
		public Sprite LoadSprite(Priority priority)
		{
			return this.LoadSprite(priority, BuilderPrioritySpriteLoader.PanelDirectory);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002570 File Offset: 0x00000770
		public Sprite LoadButtonSprite(Priority priority)
		{
			return this.LoadSprite(priority, BuilderPrioritySpriteLoader.ButtonsDirectory);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002580 File Offset: 0x00000780
		public Sprite LoadSprite(Priority priority, string subfolder)
		{
			string path = Path.Combine(BuilderPrioritySpriteLoader.PrioritySpriteDirectory, subfolder, priority.ToString());
			return this._assetLoader.Load<Sprite>(path);
		}

		// Token: 0x04000021 RID: 33
		public static readonly string PrioritySpriteDirectory = "Sprites/Priority";

		// Token: 0x04000022 RID: 34
		public static readonly string PanelDirectory = "Panel";

		// Token: 0x04000023 RID: 35
		public static readonly string ButtonsDirectory = "Buttons";

		// Token: 0x04000024 RID: 36
		public readonly IAssetLoader _assetLoader;
	}
}
