using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000010 RID: 16
	public class EntityPanelModule
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000390A File Offset: 0x00001B0A
		public ImmutableArray<OrderedEntityPanelFragment> LeftHeaderFragments { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003912 File Offset: 0x00001B12
		public ImmutableArray<OrderedEntityPanelFragment> RightHeaderFragments { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000391A File Offset: 0x00001B1A
		public ImmutableArray<IEntityPanelFragment> MiddleHeaderFragments { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003922 File Offset: 0x00001B22
		public ImmutableArray<OrderedEntityPanelFragment> ContentFragments { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000392A File Offset: 0x00001B2A
		public ImmutableArray<IEntityPanelFragment> SideFragments { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003932 File Offset: 0x00001B32
		public ImmutableArray<IEntityPanelFragment> DiagnosticFragments { get; }

		// Token: 0x06000086 RID: 134 RVA: 0x0000393C File Offset: 0x00001B3C
		public EntityPanelModule(IEnumerable<OrderedEntityPanelFragment> leftHeaderFragments, IEnumerable<OrderedEntityPanelFragment> rightHeaderFragments, IEnumerable<IEntityPanelFragment> middleHeaderFragments, IEnumerable<OrderedEntityPanelFragment> contentFragments, IEnumerable<IEntityPanelFragment> sideFragments, IEnumerable<IEntityPanelFragment> diagnosticFragments)
		{
			this.LeftHeaderFragments = leftHeaderFragments.ToImmutableArray<OrderedEntityPanelFragment>();
			this.RightHeaderFragments = rightHeaderFragments.ToImmutableArray<OrderedEntityPanelFragment>();
			this.MiddleHeaderFragments = middleHeaderFragments.ToImmutableArray<IEntityPanelFragment>();
			this.ContentFragments = contentFragments.ToImmutableArray<OrderedEntityPanelFragment>();
			this.SideFragments = sideFragments.ToImmutableArray<IEntityPanelFragment>();
			this.DiagnosticFragments = diagnosticFragments.ToImmutableArray<IEntityPanelFragment>();
		}

		// Token: 0x0400005E RID: 94
		public static readonly int TopOrder = 0;

		// Token: 0x0400005F RID: 95
		public static readonly int MiddleOrder = 1000;

		// Token: 0x04000060 RID: 96
		public static readonly int BottomOrder = 2000;

		// Token: 0x04000061 RID: 97
		public static readonly int FooterOrder = 3000;

		// Token: 0x02000011 RID: 17
		public class Builder
		{
			// Token: 0x06000088 RID: 136 RVA: 0x000039C0 File Offset: 0x00001BC0
			public void AddLeftHeaderFragment(IEntityPanelFragment panelFragment, int order)
			{
				this._leftHeaderFragments.Add(new OrderedEntityPanelFragment(panelFragment, order));
			}

			// Token: 0x06000089 RID: 137 RVA: 0x000039D4 File Offset: 0x00001BD4
			public void AddRightHeaderFragment(IEntityPanelFragment panelFragment, int order)
			{
				this._rightHeaderFragments.Add(new OrderedEntityPanelFragment(panelFragment, order));
			}

			// Token: 0x0600008A RID: 138 RVA: 0x000039E8 File Offset: 0x00001BE8
			public void AddMiddleHeaderFragment(IEntityPanelFragment panelFragment)
			{
				this._middleHeaderFragments.Add(panelFragment);
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000039F6 File Offset: 0x00001BF6
			public void AddTopFragment(IEntityPanelFragment panelFragment, int order = 0)
			{
				this.AddContentFragment(panelFragment, EntityPanelModule.TopOrder + order);
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00003A06 File Offset: 0x00001C06
			public void AddMiddleFragment(IEntityPanelFragment panelFragment, int order = 0)
			{
				this.AddContentFragment(panelFragment, EntityPanelModule.MiddleOrder + order);
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003A16 File Offset: 0x00001C16
			public void AddBottomFragment(IEntityPanelFragment panelFragment, int order = 0)
			{
				this.AddContentFragment(panelFragment, EntityPanelModule.BottomOrder + order);
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00003A26 File Offset: 0x00001C26
			public void AddFooterFragment(IEntityPanelFragment panelFragment, int order = 0)
			{
				this.AddContentFragment(panelFragment, EntityPanelModule.FooterOrder + order);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003A36 File Offset: 0x00001C36
			public void AddSideFragment(IEntityPanelFragment panelFragment)
			{
				this._sideFragments.Add(panelFragment);
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00003A44 File Offset: 0x00001C44
			public void AddDiagnosticFragment(IEntityPanelFragment panelFragment)
			{
				this._diagnosticFragments.Add(panelFragment);
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00003A52 File Offset: 0x00001C52
			public EntityPanelModule Build()
			{
				return new EntityPanelModule(this._leftHeaderFragments, this._rightHeaderFragments, this._middleHeaderFragments, this._contentFragments, this._sideFragments, this._diagnosticFragments);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00003A7D File Offset: 0x00001C7D
			public void AddContentFragment(IEntityPanelFragment panelFragment, int order)
			{
				this._contentFragments.Add(new OrderedEntityPanelFragment(panelFragment, order));
			}

			// Token: 0x04000068 RID: 104
			public readonly List<OrderedEntityPanelFragment> _leftHeaderFragments = new List<OrderedEntityPanelFragment>();

			// Token: 0x04000069 RID: 105
			public readonly List<OrderedEntityPanelFragment> _rightHeaderFragments = new List<OrderedEntityPanelFragment>();

			// Token: 0x0400006A RID: 106
			public readonly List<IEntityPanelFragment> _middleHeaderFragments = new List<IEntityPanelFragment>();

			// Token: 0x0400006B RID: 107
			public readonly List<OrderedEntityPanelFragment> _contentFragments = new List<OrderedEntityPanelFragment>();

			// Token: 0x0400006C RID: 108
			public readonly List<IEntityPanelFragment> _sideFragments = new List<IEntityPanelFragment>();

			// Token: 0x0400006D RID: 109
			public readonly List<IEntityPanelFragment> _diagnosticFragments = new List<IEntityPanelFragment>();
		}
	}
}
