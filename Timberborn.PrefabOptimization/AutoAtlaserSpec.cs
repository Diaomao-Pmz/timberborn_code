using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200000A RID: 10
	public class AutoAtlaserSpec : ComponentSpec, IEquatable<AutoAtlaserSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002967 File Offset: 0x00000B67
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AutoAtlaserSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002973 File Offset: 0x00000B73
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000297B File Offset: 0x00000B7B
		[Serialize]
		public ImmutableArray<AutoAtlasSpec> AutoAtlases { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002984 File Offset: 0x00000B84
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AutoAtlaserSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029D0 File Offset: 0x00000BD0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AutoAtlases = ");
			builder.Append(this.AutoAtlases.ToString());
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A1A File Offset: 0x00000C1A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AutoAtlaserSpec left, AutoAtlaserSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A26 File Offset: 0x00000C26
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AutoAtlaserSpec left, AutoAtlaserSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A3A File Offset: 0x00000C3A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<AutoAtlasSpec>>.Default.GetHashCode(this.<AutoAtlases>k__BackingField);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A59 File Offset: 0x00000C59
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AutoAtlaserSpec);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A67 File Offset: 0x00000C67
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A70 File Offset: 0x00000C70
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AutoAtlaserSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<AutoAtlasSpec>>.Default.Equals(this.<AutoAtlases>k__BackingField, other.<AutoAtlases>k__BackingField));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AA1 File Offset: 0x00000CA1
		[CompilerGenerated]
		protected AutoAtlaserSpec([Nullable(1)] AutoAtlaserSpec original) : base(original)
		{
			this.AutoAtlases = original.<AutoAtlases>k__BackingField;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public AutoAtlaserSpec()
		{
		}
	}
}
