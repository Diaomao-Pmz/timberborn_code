using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000051 RID: 81
	public class TimerModelSpec : ComponentSpec, IEquatable<TimerModelSpec>
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000095EA File Offset: 0x000077EA
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TimerModelSpec);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000095F6 File Offset: 0x000077F6
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000095FE File Offset: 0x000077FE
		[Serialize]
		public string ProgressObjectName { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009607 File Offset: 0x00007807
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000960F File Offset: 0x0000780F
		[Serialize]
		public float MinHeight { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00009618 File Offset: 0x00007818
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00009620 File Offset: 0x00007820
		[Serialize]
		public float MaxHeight { get; set; }

		// Token: 0x0600035E RID: 862 RVA: 0x0000962C File Offset: 0x0000782C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TimerModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009678 File Offset: 0x00007878
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ProgressObjectName = ");
			builder.Append(this.ProgressObjectName);
			builder.Append(", MinHeight = ");
			builder.Append(this.MinHeight.ToString());
			builder.Append(", MaxHeight = ");
			builder.Append(this.MaxHeight.ToString());
			return true;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009702 File Offset: 0x00007902
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TimerModelSpec left, TimerModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000970E File Offset: 0x0000790E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TimerModelSpec left, TimerModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009724 File Offset: 0x00007924
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgressObjectName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinHeight>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxHeight>k__BackingField);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000977C File Offset: 0x0000797C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimerModelSpec);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000978C File Offset: 0x0000798C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TimerModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ProgressObjectName>k__BackingField, other.<ProgressObjectName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinHeight>k__BackingField, other.<MinHeight>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxHeight>k__BackingField, other.<MaxHeight>k__BackingField));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000097F8 File Offset: 0x000079F8
		[CompilerGenerated]
		protected TimerModelSpec([Nullable(1)] TimerModelSpec original) : base(original)
		{
			this.ProgressObjectName = original.<ProgressObjectName>k__BackingField;
			this.MinHeight = original.<MinHeight>k__BackingField;
			this.MaxHeight = original.<MaxHeight>k__BackingField;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00002778 File Offset: 0x00000978
		public TimerModelSpec()
		{
		}
	}
}
