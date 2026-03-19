using System;
using System.IO;
using Timberborn.AssetSystem;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using UnityEngine;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200001E RID: 30
	public class WorkplacePrioritySpriteLoader : IPrioritySpriteLoader
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003B31 File Offset: 0x00001D31
		public WorkplacePrioritySpriteLoader(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B40 File Offset: 0x00001D40
		public Sprite LoadSprite(Priority priority)
		{
			string path = Path.Combine(WorkplacePrioritySpriteLoader.PrioritySpriteDirectory, priority.ToString());
			return this._assetLoader.Load<Sprite>(path);
		}

		// Token: 0x0400008E RID: 142
		public static readonly string PrioritySpriteDirectory = "Sprites/Priority/Workplace";

		// Token: 0x0400008F RID: 143
		public readonly IAssetLoader _assetLoader;
	}
}
