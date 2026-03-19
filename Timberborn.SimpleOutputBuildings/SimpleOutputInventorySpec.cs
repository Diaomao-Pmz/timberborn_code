using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class SimpleOutputInventorySpec : ComponentSpec, IEquatable<SimpleOutputInventorySpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002384 File Offset: 0x00000584
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SimpleOutputInventorySpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002390 File Offset: 0x00000590
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002398 File Offset: 0x00000598
		[Serialize]
		public int Capacity { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023A1 File Offset: 0x000005A1
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000023A9 File Offset: 0x000005A9
		[Serialize]
		public bool IgnorableCapacity { get; set; }

		// Token: 0x0600001F RID: 31 RVA: 0x000023B4 File Offset: 0x000005B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimpleOutputInventorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002400 File Offset: 0x00000600
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Capacity = ");
			builder.Append(this.Capacity.ToString());
			builder.Append(", IgnorableCapacity = ");
			builder.Append(this.IgnorableCapacity.ToString());
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002471 File Offset: 0x00000671
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SimpleOutputInventorySpec left, SimpleOutputInventorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000247D File Offset: 0x0000067D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SimpleOutputInventorySpec left, SimpleOutputInventorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002491 File Offset: 0x00000691
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Capacity>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IgnorableCapacity>k__BackingField);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024C7 File Offset: 0x000006C7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SimpleOutputInventorySpec);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024D5 File Offset: 0x000006D5
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E0 File Offset: 0x000006E0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SimpleOutputInventorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Capacity>k__BackingField, other.<Capacity>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IgnorableCapacity>k__BackingField, other.<IgnorableCapacity>k__BackingField));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002534 File Offset: 0x00000734
		[CompilerGenerated]
		protected SimpleOutputInventorySpec(SimpleOutputInventorySpec original) : base(original)
		{
			this.Capacity = original.<Capacity>k__BackingField;
			this.IgnorableCapacity = original.<IgnorableCapacity>k__BackingField;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002555 File Offset: 0x00000755
		public SimpleOutputInventorySpec()
		{
		}
	}
}
