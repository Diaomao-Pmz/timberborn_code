using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class FloodgateSpec : ComponentSpec, IEquatable<FloodgateSpec>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000037F5 File Offset: 0x000019F5
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FloodgateSpec);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003801 File Offset: 0x00001A01
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003809 File Offset: 0x00001A09
		[Serialize]
		public int MaxHeight { get; set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003814 File Offset: 0x00001A14
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FloodgateSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003860 File Offset: 0x00001A60
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxHeight = ");
			builder.Append(this.MaxHeight.ToString());
			return true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000038AA File Offset: 0x00001AAA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FloodgateSpec left, FloodgateSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000038B6 File Offset: 0x00001AB6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FloodgateSpec left, FloodgateSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038CA File Offset: 0x00001ACA
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxHeight>k__BackingField);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000038E9 File Offset: 0x00001AE9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FloodgateSpec);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000038F7 File Offset: 0x00001AF7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FloodgateSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MaxHeight>k__BackingField, other.<MaxHeight>k__BackingField));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003928 File Offset: 0x00001B28
		[CompilerGenerated]
		protected FloodgateSpec(FloodgateSpec original) : base(original)
		{
			this.MaxHeight = original.<MaxHeight>k__BackingField;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002CBC File Offset: 0x00000EBC
		public FloodgateSpec()
		{
		}
	}
}
