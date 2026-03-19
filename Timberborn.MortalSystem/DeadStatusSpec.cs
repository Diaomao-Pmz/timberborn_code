using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000B RID: 11
	public class DeadStatusSpec : ComponentSpec, IEquatable<DeadStatusSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002558 File Offset: 0x00000758
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DeadStatusSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000256C File Offset: 0x0000076C
		[Serialize]
		public string DeadStatusLocKey { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002578 File Offset: 0x00000778
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DeadStatusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025C4 File Offset: 0x000007C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DeadStatusLocKey = ");
			builder.Append(this.DeadStatusLocKey);
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025F5 File Offset: 0x000007F5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DeadStatusSpec left, DeadStatusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002601 File Offset: 0x00000801
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DeadStatusSpec left, DeadStatusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002615 File Offset: 0x00000815
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeadStatusLocKey>k__BackingField);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002634 File Offset: 0x00000834
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DeadStatusSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002642 File Offset: 0x00000842
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000264B File Offset: 0x0000084B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DeadStatusSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DeadStatusLocKey>k__BackingField, other.<DeadStatusLocKey>k__BackingField));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000267C File Offset: 0x0000087C
		[CompilerGenerated]
		protected DeadStatusSpec([Nullable(1)] DeadStatusSpec original) : base(original)
		{
			this.DeadStatusLocKey = original.<DeadStatusLocKey>k__BackingField;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002691 File Offset: 0x00000891
		public DeadStatusSpec()
		{
		}
	}
}
