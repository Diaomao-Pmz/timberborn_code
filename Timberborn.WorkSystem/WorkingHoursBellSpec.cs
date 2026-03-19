using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class WorkingHoursBellSpec : ComponentSpec, IEquatable<WorkingHoursBellSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003698 File Offset: 0x00001898
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WorkingHoursBellSpec);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036A4 File Offset: 0x000018A4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkingHoursBellSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036F0 File Offset: 0x000018F0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000036F9 File Offset: 0x000018F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkingHoursBellSpec left, WorkingHoursBellSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003705 File Offset: 0x00001905
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkingHoursBellSpec left, WorkingHoursBellSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003719 File Offset: 0x00001919
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003721 File Offset: 0x00001921
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkingHoursBellSpec);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000372F File Offset: 0x0000192F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkingHoursBellSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003746 File Offset: 0x00001946
		[CompilerGenerated]
		protected WorkingHoursBellSpec(WorkingHoursBellSpec original) : base(original)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002591 File Offset: 0x00000791
		public WorkingHoursBellSpec()
		{
		}
	}
}
