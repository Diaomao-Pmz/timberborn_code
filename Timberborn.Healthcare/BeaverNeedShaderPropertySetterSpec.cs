using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Healthcare
{
	// Token: 0x0200000D RID: 13
	public class BeaverNeedShaderPropertySetterSpec : ComponentSpec, IEquatable<BeaverNeedShaderPropertySetterSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A57 File Offset: 0x00000C57
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BeaverNeedShaderPropertySetterSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A63 File Offset: 0x00000C63
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002A6B File Offset: 0x00000C6B
		[Serialize]
		public ImmutableArray<BeaverNeedShaderPropertySet> PropertySets { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002A74 File Offset: 0x00000C74
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverNeedShaderPropertySetterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AC0 File Offset: 0x00000CC0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PropertySets = ");
			builder.Append(this.PropertySets.ToString());
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B0A File Offset: 0x00000D0A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverNeedShaderPropertySetterSpec left, BeaverNeedShaderPropertySetterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B16 File Offset: 0x00000D16
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverNeedShaderPropertySetterSpec left, BeaverNeedShaderPropertySetterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B2A File Offset: 0x00000D2A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BeaverNeedShaderPropertySet>>.Default.GetHashCode(this.<PropertySets>k__BackingField);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B49 File Offset: 0x00000D49
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverNeedShaderPropertySetterSpec);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000026D3 File Offset: 0x000008D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B57 File Offset: 0x00000D57
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverNeedShaderPropertySetterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BeaverNeedShaderPropertySet>>.Default.Equals(this.<PropertySets>k__BackingField, other.<PropertySets>k__BackingField));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B88 File Offset: 0x00000D88
		[CompilerGenerated]
		protected BeaverNeedShaderPropertySetterSpec([Nullable(1)] BeaverNeedShaderPropertySetterSpec original) : base(original)
		{
			this.PropertySets = original.<PropertySets>k__BackingField;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002722 File Offset: 0x00000922
		public BeaverNeedShaderPropertySetterSpec()
		{
		}
	}
}
