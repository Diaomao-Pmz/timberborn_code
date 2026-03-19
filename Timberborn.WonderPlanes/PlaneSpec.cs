using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000012 RID: 18
	public class PlaneSpec : ComponentSpec, IEquatable<PlaneSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000036EC File Offset: 0x000018EC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlaneSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000036F8 File Offset: 0x000018F8
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003700 File Offset: 0x00001900
		[Serialize]
		public string PilotSeatName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003709 File Offset: 0x00001909
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003711 File Offset: 0x00001911
		[Serialize]
		public float RotationSpeed { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x0000371C File Offset: 0x0000191C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaneSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003768 File Offset: 0x00001968
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PilotSeatName = ");
			builder.Append(this.PilotSeatName);
			builder.Append(", RotationSpeed = ");
			builder.Append(this.RotationSpeed.ToString());
			return true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000037CB File Offset: 0x000019CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaneSpec left, PlaneSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037D7 File Offset: 0x000019D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaneSpec left, PlaneSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037EB File Offset: 0x000019EB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PilotSeatName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RotationSpeed>k__BackingField);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003821 File Offset: 0x00001A21
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaneSpec);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003830 File Offset: 0x00001A30
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaneSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<PilotSeatName>k__BackingField, other.<PilotSeatName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RotationSpeed>k__BackingField, other.<RotationSpeed>k__BackingField));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003884 File Offset: 0x00001A84
		[CompilerGenerated]
		protected PlaneSpec([Nullable(1)] PlaneSpec original) : base(original)
		{
			this.PilotSeatName = original.<PilotSeatName>k__BackingField;
			this.RotationSpeed = original.<RotationSpeed>k__BackingField;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public PlaneSpec()
		{
		}
	}
}
