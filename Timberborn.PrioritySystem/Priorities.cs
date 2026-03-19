using System;
using System.Collections.Immutable;
using System.Linq;

namespace Timberborn.PrioritySystem
{
	// Token: 0x02000005 RID: 5
	public static class Priorities
	{
		// Token: 0x04000006 RID: 6
		public static readonly ImmutableArray<Priority> Ascending = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToImmutableArray<Priority>();

		// Token: 0x04000007 RID: 7
		public static readonly ImmutableArray<Priority> Descending = (from priority in Priorities.Ascending
		orderby priority descending
		select priority).ToImmutableArray<Priority>();
	}
}
