using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200000E RID: 14
	public class RemoveYieldStrategySpec : ComponentSpec, IEquatable<RemoveYieldStrategySpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000296B File Offset: 0x00000B6B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RemoveYieldStrategySpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002977 File Offset: 0x00000B77
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000297F File Offset: 0x00000B7F
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002990 File Offset: 0x00000B90
		[Serialize]
		public ImmutableArray<string> CompatibleResourceGroups { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002999 File Offset: 0x00000B99
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000029A1 File Offset: 0x00000BA1
		[Serialize]
		public string Animation { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x000029AC File Offset: 0x00000BAC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RemoveYieldStrategySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029F8 File Offset: 0x00000BF8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", CompatibleResourceGroups = ");
			builder.Append(this.CompatibleResourceGroups.ToString());
			builder.Append(", Animation = ");
			builder.Append(this.Animation);
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A74 File Offset: 0x00000C74
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RemoveYieldStrategySpec left, RemoveYieldStrategySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A80 File Offset: 0x00000C80
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RemoveYieldStrategySpec left, RemoveYieldStrategySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A94 File Offset: 0x00000C94
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<CompatibleResourceGroups>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Animation>k__BackingField);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AEC File Offset: 0x00000CEC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RemoveYieldStrategySpec);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AFA File Offset: 0x00000CFA
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B04 File Offset: 0x00000D04
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RemoveYieldStrategySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<CompatibleResourceGroups>k__BackingField, other.<CompatibleResourceGroups>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Animation>k__BackingField, other.<Animation>k__BackingField));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B70 File Offset: 0x00000D70
		[CompilerGenerated]
		protected RemoveYieldStrategySpec([Nullable(1)] RemoveYieldStrategySpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.CompatibleResourceGroups = original.<CompatibleResourceGroups>k__BackingField;
			this.Animation = original.<Animation>k__BackingField;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B9D File Offset: 0x00000D9D
		public RemoveYieldStrategySpec()
		{
		}
	}
}
