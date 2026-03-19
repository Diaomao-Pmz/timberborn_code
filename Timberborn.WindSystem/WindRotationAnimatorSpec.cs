using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x0200000F RID: 15
	public class WindRotationAnimatorSpec : ComponentSpec, IEquatable<WindRotationAnimatorSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002799 File Offset: 0x00000999
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WindRotationAnimatorSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000027A5 File Offset: 0x000009A5
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000027AD File Offset: 0x000009AD
		[Serialize]
		public ImmutableArray<WindRotatorSpec> WindRotators { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000027B6 File Offset: 0x000009B6
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000027BE File Offset: 0x000009BE
		[Serialize]
		public WindRotatorSpec Tower { get; set; }

		// Token: 0x06000051 RID: 81 RVA: 0x000027C8 File Offset: 0x000009C8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindRotationAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002814 File Offset: 0x00000A14
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WindRotators = ");
			builder.Append(this.WindRotators.ToString());
			builder.Append(", Tower = ");
			builder.Append(this.Tower);
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002877 File Offset: 0x00000A77
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindRotationAnimatorSpec left, WindRotationAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002883 File Offset: 0x00000A83
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindRotationAnimatorSpec left, WindRotationAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002897 File Offset: 0x00000A97
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<WindRotatorSpec>>.Default.GetHashCode(this.<WindRotators>k__BackingField)) * -1521134295 + EqualityComparer<WindRotatorSpec>.Default.GetHashCode(this.<Tower>k__BackingField);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000028CD File Offset: 0x00000ACD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindRotationAnimatorSpec);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002263 File Offset: 0x00000463
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000028DC File Offset: 0x00000ADC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindRotationAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<WindRotatorSpec>>.Default.Equals(this.<WindRotators>k__BackingField, other.<WindRotators>k__BackingField) && EqualityComparer<WindRotatorSpec>.Default.Equals(this.<Tower>k__BackingField, other.<Tower>k__BackingField));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002930 File Offset: 0x00000B30
		[CompilerGenerated]
		protected WindRotationAnimatorSpec([Nullable(1)] WindRotationAnimatorSpec original) : base(original)
		{
			this.WindRotators = original.<WindRotators>k__BackingField;
			this.Tower = original.<Tower>k__BackingField;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000228C File Offset: 0x0000048C
		public WindRotationAnimatorSpec()
		{
		}
	}
}
