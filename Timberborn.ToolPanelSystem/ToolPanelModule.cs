using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.ToolPanelSystem
{
	// Token: 0x02000008 RID: 8
	public class ToolPanelModule
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021F9 File Offset: 0x000003F9
		public ImmutableArray<OrderedToolFragment> ToolFragments { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002201 File Offset: 0x00000401
		public ToolPanelModule(IEnumerable<OrderedToolFragment> toolFragments)
		{
			this.ToolFragments = toolFragments.ToImmutableArray<OrderedToolFragment>();
		}

		// Token: 0x02000009 RID: 9
		public class Builder
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002215 File Offset: 0x00000415
			public void AddFragment(IToolFragment toolFragment, int order)
			{
				this._toolFragments.Add(new OrderedToolFragment(toolFragment, order));
			}

			// Token: 0x06000010 RID: 16 RVA: 0x00002229 File Offset: 0x00000429
			public ToolPanelModule Build()
			{
				return new ToolPanelModule(this._toolFragments);
			}

			// Token: 0x0400000E RID: 14
			public readonly List<OrderedToolFragment> _toolFragments = new List<OrderedToolFragment>();
		}
	}
}
