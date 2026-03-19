using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.CoreUI
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class GradientPointSpec : IEquatable<GradientPointSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D10 File Offset: 0x00000F10
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GradientPointSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002D24 File Offset: 0x00000F24
		[Serialize]
		public Color Color { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D2D File Offset: 0x00000F2D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002D35 File Offset: 0x00000F35
		[Serialize]
		public float Time { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002D40 File Offset: 0x00000F40
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GradientPointSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D8C File Offset: 0x00000F8C
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Color = ");
			builder.Append(this.Color.ToString());
			builder.Append(", Time = ");
			builder.Append(this.Time.ToString());
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DED File Offset: 0x00000FED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GradientPointSpec left, GradientPointSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DF9 File Offset: 0x00000FF9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GradientPointSpec left, GradientPointSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E0D File Offset: 0x0000100D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Time>k__BackingField);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E4D File Offset: 0x0000104D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GradientPointSpec);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E5C File Offset: 0x0000105C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GradientPointSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Time>k__BackingField, other.<Time>k__BackingField));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EBD File Offset: 0x000010BD
		[CompilerGenerated]
		protected GradientPointSpec(GradientPointSpec original)
		{
			this.Color = original.<Color>k__BackingField;
			this.Time = original.<Time>k__BackingField;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000020F8 File Offset: 0x000002F8
		public GradientPointSpec()
		{
		}
	}
}
