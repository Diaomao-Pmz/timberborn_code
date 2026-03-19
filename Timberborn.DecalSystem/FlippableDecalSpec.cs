using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000014 RID: 20
	public class FlippableDecalSpec : ComponentSpec, IEquatable<FlippableDecalSpec>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003005 File Offset: 0x00001205
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FlippableDecalSpec);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003011 File Offset: 0x00001211
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003019 File Offset: 0x00001219
		[Serialize]
		public string DecalName { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003024 File Offset: 0x00001224
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FlippableDecalSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003070 File Offset: 0x00001270
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DecalName = ");
			builder.Append(this.DecalName);
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000030A1 File Offset: 0x000012A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FlippableDecalSpec left, FlippableDecalSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000030AD File Offset: 0x000012AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FlippableDecalSpec left, FlippableDecalSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000030C1 File Offset: 0x000012C1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DecalName>k__BackingField);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030E0 File Offset: 0x000012E0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FlippableDecalSpec);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002806 File Offset: 0x00000A06
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030EE File Offset: 0x000012EE
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FlippableDecalSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DecalName>k__BackingField, other.<DecalName>k__BackingField));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000311F File Offset: 0x0000131F
		[CompilerGenerated]
		protected FlippableDecalSpec([Nullable(1)] FlippableDecalSpec original) : base(original)
		{
			this.DecalName = original.<DecalName>k__BackingField;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000028A9 File Offset: 0x00000AA9
		public FlippableDecalSpec()
		{
		}
	}
}
