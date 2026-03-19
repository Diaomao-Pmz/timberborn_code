using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000D RID: 13
	public class ObservatoryAnimatorSpec : ComponentSpec, IEquatable<ObservatoryAnimatorSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000029B9 File Offset: 0x00000BB9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ObservatoryAnimatorSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000029CD File Offset: 0x00000BCD
		[Serialize]
		public string DomeName { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000029D6 File Offset: 0x00000BD6
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000029DE File Offset: 0x00000BDE
		[Serialize]
		public string TelescopeName { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x000029E8 File Offset: 0x00000BE8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ObservatoryAnimatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A34 File Offset: 0x00000C34
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DomeName = ");
			builder.Append(this.DomeName);
			builder.Append(", TelescopeName = ");
			builder.Append(this.TelescopeName);
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A89 File Offset: 0x00000C89
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ObservatoryAnimatorSpec left, ObservatoryAnimatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A95 File Offset: 0x00000C95
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ObservatoryAnimatorSpec left, ObservatoryAnimatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AA9 File Offset: 0x00000CA9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DomeName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TelescopeName>k__BackingField);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ADF File Offset: 0x00000CDF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ObservatoryAnimatorSpec);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AF0 File Offset: 0x00000CF0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ObservatoryAnimatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DomeName>k__BackingField, other.<DomeName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TelescopeName>k__BackingField, other.<TelescopeName>k__BackingField));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B44 File Offset: 0x00000D44
		[CompilerGenerated]
		protected ObservatoryAnimatorSpec([Nullable(1)] ObservatoryAnimatorSpec original) : base(original)
		{
			this.DomeName = original.<DomeName>k__BackingField;
			this.TelescopeName = original.<TelescopeName>k__BackingField;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002422 File Offset: 0x00000622
		public ObservatoryAnimatorSpec()
		{
		}
	}
}
