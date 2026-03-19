using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Buildings
{
	// Token: 0x02000024 RID: 36
	public class UncoveredModelSwitcherSpec : ComponentSpec, IEquatable<UncoveredModelSwitcherSpec>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000468C File Offset: 0x0000288C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UncoveredModelSwitcherSpec);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004698 File Offset: 0x00002898
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000046A0 File Offset: 0x000028A0
		[Serialize]
		public string FullModelName { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000046A9 File Offset: 0x000028A9
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000046B1 File Offset: 0x000028B1
		[Serialize]
		public string UncoveredModelName { get; set; }

		// Token: 0x0600012E RID: 302 RVA: 0x000046BC File Offset: 0x000028BC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UncoveredModelSwitcherSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004708 File Offset: 0x00002908
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FullModelName = ");
			builder.Append(this.FullModelName);
			builder.Append(", UncoveredModelName = ");
			builder.Append(this.UncoveredModelName);
			return true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000475D File Offset: 0x0000295D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UncoveredModelSwitcherSpec left, UncoveredModelSwitcherSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004769 File Offset: 0x00002969
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UncoveredModelSwitcherSpec left, UncoveredModelSwitcherSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000477D File Offset: 0x0000297D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FullModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UncoveredModelName>k__BackingField);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000047B3 File Offset: 0x000029B3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UncoveredModelSwitcherSpec);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000047C4 File Offset: 0x000029C4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UncoveredModelSwitcherSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<FullModelName>k__BackingField, other.<FullModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UncoveredModelName>k__BackingField, other.<UncoveredModelName>k__BackingField));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004818 File Offset: 0x00002A18
		[CompilerGenerated]
		protected UncoveredModelSwitcherSpec([Nullable(1)] UncoveredModelSwitcherSpec original) : base(original)
		{
			this.FullModelName = original.<FullModelName>k__BackingField;
			this.UncoveredModelName = original.<UncoveredModelName>k__BackingField;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000246D File Offset: 0x0000066D
		public UncoveredModelSwitcherSpec()
		{
		}
	}
}
