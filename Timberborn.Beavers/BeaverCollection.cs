using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Beavers
{
	// Token: 0x0200000A RID: 10
	public class BeaverCollection
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021D0 File Offset: 0x000003D0
		public ReadOnlyList<Beaver> Beavers
		{
			get
			{
				return this._beavers.AsReadOnlyList<Beaver>();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021DD File Offset: 0x000003DD
		public ReadOnlyList<Beaver> Adults
		{
			get
			{
				return this._adults.AsReadOnlyList<Beaver>();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021EA File Offset: 0x000003EA
		public ReadOnlyList<Beaver> Children
		{
			get
			{
				return this._children.AsReadOnlyList<Beaver>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021F8 File Offset: 0x000003F8
		public int NumberOfBeavers
		{
			get
			{
				return this.Beavers.Count;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002214 File Offset: 0x00000414
		public int NumberOfAdults
		{
			get
			{
				return this.Adults.Count;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002230 File Offset: 0x00000430
		public int NumberOfChildren
		{
			get
			{
				return this.Children.Count;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000224B File Offset: 0x0000044B
		public void AddBeaver(Beaver beaver)
		{
			if (beaver.HasComponent<ChildSpec>())
			{
				this._children.Add(beaver);
			}
			else
			{
				this._adults.Add(beaver);
			}
			this._beavers.Add(beaver);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000227B File Offset: 0x0000047B
		public void RemoveBeaver(Beaver beaver)
		{
			this._adults.Remove(beaver);
			this._children.Remove(beaver);
			this._beavers.Remove(beaver);
		}

		// Token: 0x04000008 RID: 8
		public readonly List<Beaver> _beavers = new List<Beaver>();

		// Token: 0x04000009 RID: 9
		public readonly List<Beaver> _adults = new List<Beaver>();

		// Token: 0x0400000A RID: 10
		public readonly List<Beaver> _children = new List<Beaver>();
	}
}
