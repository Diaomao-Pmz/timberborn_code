using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200000A RID: 10
	public class BinaryHeapFactory
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public BinaryHeapFactory(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public BinaryHeap<TValue> Create<TValue>() where TValue : IOrderable<TValue>
		{
			return new BinaryHeap<TValue>(this.InitialCapacity);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D04 File Offset: 0x00000F04
		public int InitialCapacity
		{
			get
			{
				return this._nodeIdService.NumberOfNodes / 4;
			}
		}

		// Token: 0x0400001D RID: 29
		public readonly NodeIdService _nodeIdService;
	}
}
