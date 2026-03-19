using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.EntitySystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200000B RID: 11
	public class CableKey : IEquatable<CableKey>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002394 File Offset: 0x00000594
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CableKey);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023A0 File Offset: 0x000005A0
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023A8 File Offset: 0x000005A8
		public ZiplineTower ZiplineTower { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023B1 File Offset: 0x000005B1
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000023B9 File Offset: 0x000005B9
		public ZiplineTower OtherZiplineTower { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000023C4 File Offset: 0x000005C4
		public static CableKey Create(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			Guid entityId = ziplineTower.GetComponent<EntityComponent>().EntityId;
			Guid entityId2 = otherZiplineTower.GetComponent<EntityComponent>().EntityId;
			if (entityId.CompareTo(entityId2) <= 0)
			{
				return new CableKey
				{
					ZiplineTower = otherZiplineTower,
					OtherZiplineTower = ziplineTower
				};
			}
			return new CableKey
			{
				ZiplineTower = ziplineTower,
				OtherZiplineTower = otherZiplineTower
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000241C File Offset: 0x0000061C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CableKey");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002468 File Offset: 0x00000668
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("ZiplineTower = ");
			builder.Append(this.ZiplineTower);
			builder.Append(", OtherZiplineTower = ");
			builder.Append(this.OtherZiplineTower);
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024A2 File Offset: 0x000006A2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CableKey left, CableKey right)
		{
			return !(left == right);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024AE File Offset: 0x000006AE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CableKey left, CableKey right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024C2 File Offset: 0x000006C2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ZiplineTower>.Default.GetHashCode(this.<ZiplineTower>k__BackingField)) * -1521134295 + EqualityComparer<ZiplineTower>.Default.GetHashCode(this.<OtherZiplineTower>k__BackingField);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002502 File Offset: 0x00000702
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CableKey);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002510 File Offset: 0x00000710
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CableKey other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ZiplineTower>.Default.Equals(this.<ZiplineTower>k__BackingField, other.<ZiplineTower>k__BackingField) && EqualityComparer<ZiplineTower>.Default.Equals(this.<OtherZiplineTower>k__BackingField, other.<OtherZiplineTower>k__BackingField));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002571 File Offset: 0x00000771
		[CompilerGenerated]
		protected CableKey([Nullable(1)] CableKey original)
		{
			this.ZiplineTower = original.<ZiplineTower>k__BackingField;
			this.OtherZiplineTower = original.<OtherZiplineTower>k__BackingField;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000020F8 File Offset: 0x000002F8
		public CableKey()
		{
		}
	}
}
