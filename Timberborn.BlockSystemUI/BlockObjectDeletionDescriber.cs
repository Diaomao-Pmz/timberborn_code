using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Localization;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000C RID: 12
	public class BlockObjectDeletionDescriber : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000271F File Offset: 0x0000091F
		public BlockObjectDeletionDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002744 File Offset: 0x00000944
		public void Awake()
		{
			base.GetComponents<IBlockObjectDeletionBlocker>(this._deletionBlockers);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002754 File Offset: 0x00000954
		public string GetDescription()
		{
			this._descriptionBuilder.Clear();
			foreach (IBlockObjectDeletionBlocker blockObjectDeletionBlocker in this._deletionBlockers)
			{
				if (blockObjectDeletionBlocker.IsDeletionBlocked)
				{
					this._descriptionBuilder.Append(" " + this._loc.T(blockObjectDeletionBlocker.ReasonLocKey));
				}
			}
			if (this._descriptionBuilder.Length <= 0)
			{
				return this._loc.T(BlockObjectDeletionDescriber.DemolishTooltipLocKey);
			}
			return string.Format("{0}{1}", this._loc.T(BlockObjectDeletionDescriber.PrefixLocKey), this._descriptionBuilder);
		}

		// Token: 0x04000014 RID: 20
		public static readonly string PrefixLocKey = "DeletionBlocker.Prefix";

		// Token: 0x04000015 RID: 21
		public static readonly string DemolishTooltipLocKey = "Demolish.Mark";

		// Token: 0x04000016 RID: 22
		public readonly ILoc _loc;

		// Token: 0x04000017 RID: 23
		public readonly List<IBlockObjectDeletionBlocker> _deletionBlockers = new List<IBlockObjectDeletionBlocker>();

		// Token: 0x04000018 RID: 24
		public readonly StringBuilder _descriptionBuilder = new StringBuilder();
	}
}
