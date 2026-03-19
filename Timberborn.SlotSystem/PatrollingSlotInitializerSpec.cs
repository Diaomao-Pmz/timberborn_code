using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000010 RID: 16
	public class PatrollingSlotInitializerSpec : ComponentSpec, IEquatable<PatrollingSlotInitializerSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002AAF File Offset: 0x00000CAF
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PatrollingSlotInitializerSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002ABB File Offset: 0x00000CBB
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002AC3 File Offset: 0x00000CC3
		[Serialize]
		public ImmutableArray<PatrollingSlotSpec> PatrollingSlots { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002ACC File Offset: 0x00000CCC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PatrollingSlotInitializerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B18 File Offset: 0x00000D18
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PatrollingSlots = ");
			builder.Append(this.PatrollingSlots.ToString());
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B62 File Offset: 0x00000D62
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PatrollingSlotInitializerSpec left, PatrollingSlotInitializerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B6E File Offset: 0x00000D6E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PatrollingSlotInitializerSpec left, PatrollingSlotInitializerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B82 File Offset: 0x00000D82
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<PatrollingSlotSpec>>.Default.GetHashCode(this.<PatrollingSlots>k__BackingField);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BA1 File Offset: 0x00000DA1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PatrollingSlotInitializerSpec);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BAF File Offset: 0x00000DAF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PatrollingSlotInitializerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<PatrollingSlotSpec>>.Default.Equals(this.<PatrollingSlots>k__BackingField, other.<PatrollingSlots>k__BackingField));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BE0 File Offset: 0x00000DE0
		[CompilerGenerated]
		protected PatrollingSlotInitializerSpec([Nullable(1)] PatrollingSlotInitializerSpec original) : base(original)
		{
			this.PatrollingSlots = original.<PatrollingSlots>k__BackingField;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000238C File Offset: 0x0000058C
		public PatrollingSlotInitializerSpec()
		{
		}
	}
}
