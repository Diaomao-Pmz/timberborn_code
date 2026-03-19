using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MergeableObjects
{
	// Token: 0x02000009 RID: 9
	public class MergeableObjectModelSpec : ComponentSpec, IEquatable<MergeableObjectModelSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024C8 File Offset: 0x000006C8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MergeableObjectModelSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024DC File Offset: 0x000006DC
		[Serialize]
		public string ModelNamePrefix { get; set; }

		// Token: 0x06000021 RID: 33 RVA: 0x000024E8 File Offset: 0x000006E8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MergeableObjectModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002534 File Offset: 0x00000734
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ModelNamePrefix = ");
			builder.Append(this.ModelNamePrefix);
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002565 File Offset: 0x00000765
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MergeableObjectModelSpec left, MergeableObjectModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002571 File Offset: 0x00000771
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MergeableObjectModelSpec left, MergeableObjectModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002585 File Offset: 0x00000785
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelNamePrefix>k__BackingField);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025A4 File Offset: 0x000007A4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MergeableObjectModelSpec);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002497 File Offset: 0x00000697
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025B2 File Offset: 0x000007B2
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MergeableObjectModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ModelNamePrefix>k__BackingField, other.<ModelNamePrefix>k__BackingField));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025E3 File Offset: 0x000007E3
		[CompilerGenerated]
		protected MergeableObjectModelSpec([Nullable(1)] MergeableObjectModelSpec original) : base(original)
		{
			this.ModelNamePrefix = original.<ModelNamePrefix>k__BackingField;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024C0 File Offset: 0x000006C0
		public MergeableObjectModelSpec()
		{
		}
	}
}
