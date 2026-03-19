using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200001D RID: 29
	public class ConstructionSiteReservations : BaseComponent
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003E2D File Offset: 0x0000202D
		public bool HasFreeSpots
		{
			get
			{
				return this._builders.Count < this._capacity;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E42 File Offset: 0x00002042
		public void SetCapacity(int capacity)
		{
			this._capacity = capacity;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E4B File Offset: 0x0000204B
		public void Reserve(Builder builder)
		{
			if (!this._builders.Contains(builder) && !this.HasFreeSpots)
			{
				throw new InvalidOperationException("Error while assigning builder " + builder.Name + ": construction site is already full.");
			}
			this._builders.Add(builder);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003E8B File Offset: 0x0000208B
		public void Unreserve(Builder builder)
		{
			if (!this._builders.Contains(builder))
			{
				throw new InvalidOperationException("Builder " + builder.Name + " did not reserve this construction site.");
			}
			this._builders.Remove(builder);
		}

		// Token: 0x04000062 RID: 98
		public int _capacity = 1;

		// Token: 0x04000063 RID: 99
		public readonly HashSet<Builder> _builders = new HashSet<Builder>();
	}
}
