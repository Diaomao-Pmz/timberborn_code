using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000015 RID: 21
	public class GateModelSpec : ComponentSpec, IEquatable<GateModelSpec>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003A6C File Offset: 0x00001C6C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GateModelSpec);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003A78 File Offset: 0x00001C78
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003A80 File Offset: 0x00001C80
		[Serialize]
		public string OpenModelName { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003A89 File Offset: 0x00001C89
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003A91 File Offset: 0x00001C91
		[Serialize]
		public string ClosedModelName { get; set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00003A9C File Offset: 0x00001C9C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GateModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003AE8 File Offset: 0x00001CE8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OpenModelName = ");
			builder.Append(this.OpenModelName);
			builder.Append(", ClosedModelName = ");
			builder.Append(this.ClosedModelName);
			return true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003B3D File Offset: 0x00001D3D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GateModelSpec left, GateModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003B49 File Offset: 0x00001D49
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GateModelSpec left, GateModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003B5D File Offset: 0x00001D5D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OpenModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClosedModelName>k__BackingField);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003B93 File Offset: 0x00001D93
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GateModelSpec);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003BA4 File Offset: 0x00001DA4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GateModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<OpenModelName>k__BackingField, other.<OpenModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClosedModelName>k__BackingField, other.<ClosedModelName>k__BackingField));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003BF8 File Offset: 0x00001DF8
		[CompilerGenerated]
		protected GateModelSpec([Nullable(1)] GateModelSpec original) : base(original)
		{
			this.OpenModelName = original.<OpenModelName>k__BackingField;
			this.ClosedModelName = original.<ClosedModelName>k__BackingField;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002778 File Offset: 0x00000978
		public GateModelSpec()
		{
		}
	}
}
