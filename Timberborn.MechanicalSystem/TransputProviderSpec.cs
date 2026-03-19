using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000023 RID: 35
	public class TransputProviderSpec : ComponentSpec, IEquatable<TransputProviderSpec>
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004850 File Offset: 0x00002A50
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TransputProviderSpec);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000485C File Offset: 0x00002A5C
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004864 File Offset: 0x00002A64
		[Serialize]
		public ImmutableArray<TransputSpec> Transputs { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000486D File Offset: 0x00002A6D
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00004875 File Offset: 0x00002A75
		[Serialize]
		public bool IgnoreRotation { get; set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00004880 File Offset: 0x00002A80
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransputProviderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000048CC File Offset: 0x00002ACC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Transputs = ");
			builder.Append(this.Transputs.ToString());
			builder.Append(", IgnoreRotation = ");
			builder.Append(this.IgnoreRotation.ToString());
			return true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000493D File Offset: 0x00002B3D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransputProviderSpec left, TransputProviderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004949 File Offset: 0x00002B49
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransputProviderSpec left, TransputProviderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000495D File Offset: 0x00002B5D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<TransputSpec>>.Default.GetHashCode(this.<Transputs>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IgnoreRotation>k__BackingField);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004993 File Offset: 0x00002B93
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransputProviderSpec);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000049A4 File Offset: 0x00002BA4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransputProviderSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<TransputSpec>>.Default.Equals(this.<Transputs>k__BackingField, other.<Transputs>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IgnoreRotation>k__BackingField, other.<IgnoreRotation>k__BackingField));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000049F8 File Offset: 0x00002BF8
		[CompilerGenerated]
		protected TransputProviderSpec([Nullable(1)] TransputProviderSpec original) : base(original)
		{
			this.Transputs = original.<Transputs>k__BackingField;
			this.IgnoreRotation = original.<IgnoreRotation>k__BackingField;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000257C File Offset: 0x0000077C
		public TransputProviderSpec()
		{
		}
	}
}
