using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Explosions
{
	// Token: 0x0200000A RID: 10
	public class DynamiteSpec : ComponentSpec, IEquatable<DynamiteSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000278A File Offset: 0x0000098A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DynamiteSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002796 File Offset: 0x00000996
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000279E File Offset: 0x0000099E
		[Serialize]
		public int Depth { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027A7 File Offset: 0x000009A7
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000027AF File Offset: 0x000009AF
		[Serialize]
		public string ExplosionPrefabPath { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x000027B8 File Offset: 0x000009B8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DynamiteSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002804 File Offset: 0x00000A04
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Depth = ");
			builder.Append(this.Depth.ToString());
			builder.Append(", ExplosionPrefabPath = ");
			builder.Append(this.ExplosionPrefabPath);
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002867 File Offset: 0x00000A67
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DynamiteSpec left, DynamiteSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002873 File Offset: 0x00000A73
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DynamiteSpec left, DynamiteSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002887 File Offset: 0x00000A87
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Depth>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ExplosionPrefabPath>k__BackingField);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028BD File Offset: 0x00000ABD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DynamiteSpec);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028D4 File Offset: 0x00000AD4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DynamiteSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Depth>k__BackingField, other.<Depth>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ExplosionPrefabPath>k__BackingField, other.<ExplosionPrefabPath>k__BackingField));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002928 File Offset: 0x00000B28
		[CompilerGenerated]
		protected DynamiteSpec([Nullable(1)] DynamiteSpec original) : base(original)
		{
			this.Depth = original.<Depth>k__BackingField;
			this.ExplosionPrefabPath = original.<ExplosionPrefabPath>k__BackingField;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002949 File Offset: 0x00000B49
		public DynamiteSpec()
		{
		}
	}
}
