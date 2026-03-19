using System;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct Decal : IEquatable<Decal>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public string Category { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public Decal(string id, string category)
		{
			this.Id = id;
			this.Category = category;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.Id);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000212D File Offset: 0x0000032D
		public bool Equals(Decal other)
		{
			return this.Id == other.Id && this.Category == other.Category;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002158 File Offset: 0x00000358
		public override bool Equals(object obj)
		{
			if (obj is Decal)
			{
				Decal other = (Decal)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217D File Offset: 0x0000037D
		public override int GetHashCode()
		{
			return HashCode.Combine<string, string>(this.Id, this.Category);
		}
	}
}
