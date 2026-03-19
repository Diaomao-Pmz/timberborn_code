using System;
using System.Collections.Generic;
using Timberborn.EntityNaming;

namespace Timberborn.BatchControl
{
	// Token: 0x02000025 RID: 37
	public class SortableNameRowComparer : IComparer<BatchControlRow>
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003E08 File Offset: 0x00002008
		public int Compare(BatchControlRow x, BatchControlRow y)
		{
			if (x.Entity != y.Entity)
			{
				return x.Entity.GetComponent<NamedEntity>().SortingKey.CompareTo(y.Entity.GetComponent<NamedEntity>().SortingKey);
			}
			return 0;
		}
	}
}
