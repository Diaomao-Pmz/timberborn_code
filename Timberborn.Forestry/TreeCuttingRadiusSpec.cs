using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Forestry
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public class TreeCuttingRadiusSpec : ComponentSpec, IEquatable<TreeCuttingRadiusSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003024 File Offset: 0x00001224
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TreeCuttingRadiusSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003038 File Offset: 0x00001238
		[Serialize]
		public float Radius { get; set; }

		// Token: 0x06000086 RID: 134 RVA: 0x00003044 File Offset: 0x00001244
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TreeCuttingRadiusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003090 File Offset: 0x00001290
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Radius = ");
			builder.Append(this.Radius.ToString());
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000030DA File Offset: 0x000012DA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TreeCuttingRadiusSpec left, TreeCuttingRadiusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000030E6 File Offset: 0x000012E6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TreeCuttingRadiusSpec left, TreeCuttingRadiusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000030FA File Offset: 0x000012FA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Radius>k__BackingField);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003119 File Offset: 0x00001319
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreeCuttingRadiusSpec);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003127 File Offset: 0x00001327
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TreeCuttingRadiusSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003158 File Offset: 0x00001358
		[CompilerGenerated]
		protected TreeCuttingRadiusSpec(TreeCuttingRadiusSpec original) : base(original)
		{
			this.Radius = original.<Radius>k__BackingField;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000021C0 File Offset: 0x000003C0
		public TreeCuttingRadiusSpec()
		{
		}
	}
}
