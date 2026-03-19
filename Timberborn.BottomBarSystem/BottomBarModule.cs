using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.BottomBarSystem
{
	// Token: 0x02000005 RID: 5
	public class BottomBarModule
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F0 File Offset: 0x000002F0
		public FrozenDictionary<int, IBottomBarElementsProvider> LeftElements { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		public ImmutableArray<IBottomBarElementsProvider> MiddleElements { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002100 File Offset: 0x00000300
		public ImmutableArray<IBottomBarElementsProvider> RightElements { get; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002108 File Offset: 0x00000308
		public BottomBarModule(Dictionary<int, IBottomBarElementsProvider> leftElements, IEnumerable<IBottomBarElementsProvider> middleElements, IEnumerable<IBottomBarElementsProvider> rightElements)
		{
			this.LeftElements = leftElements.ToFrozenDictionary(null);
			this.MiddleElements = middleElements.ToImmutableArray<IBottomBarElementsProvider>();
			this.RightElements = rightElements.ToImmutableArray<IBottomBarElementsProvider>();
		}

		// Token: 0x02000006 RID: 6
		public class Builder
		{
			// Token: 0x0600000C RID: 12 RVA: 0x00002135 File Offset: 0x00000335
			public void AddLeftSectionElement(IBottomBarElementsProvider provider, int order)
			{
				this._leftElements.Add(order, provider);
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002144 File Offset: 0x00000344
			public void AddMiddleSectionElements(IBottomBarElementsProvider provider)
			{
				this._middleElements.Add(provider);
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002152 File Offset: 0x00000352
			public void AddRightSectionElement(IBottomBarElementsProvider provider)
			{
				this._rightElements.Add(provider);
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002160 File Offset: 0x00000360
			public BottomBarModule Build()
			{
				return new BottomBarModule(this._leftElements, this._middleElements, this._rightElements);
			}

			// Token: 0x0400000B RID: 11
			public readonly Dictionary<int, IBottomBarElementsProvider> _leftElements = new Dictionary<int, IBottomBarElementsProvider>();

			// Token: 0x0400000C RID: 12
			public readonly List<IBottomBarElementsProvider> _middleElements = new List<IBottomBarElementsProvider>();

			// Token: 0x0400000D RID: 13
			public readonly List<IBottomBarElementsProvider> _rightElements = new List<IBottomBarElementsProvider>();
		}
	}
}
