using System;
using System.Collections.Immutable;
using Timberborn.BehaviorSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000009 RID: 9
	public readonly struct AppraisedAction
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002493 File Offset: 0x00000693
		public Behavior NeedBehavior { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000249B File Offset: 0x0000069B
		public ImmutableArray<string> AffectedNeeds { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024A3 File Offset: 0x000006A3
		public float Points { get; }

		// Token: 0x0600001A RID: 26 RVA: 0x000024AB File Offset: 0x000006AB
		public AppraisedAction(Behavior needBehavior, ImmutableArray<string> affectedNeeds, float points)
		{
			this.NeedBehavior = needBehavior;
			this.AffectedNeeds = affectedNeeds;
			this.Points = points;
		}
	}
}
