using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000018 RID: 24
	public class WorkerSpec : ComponentSpec, IEquatable<WorkerSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003298 File Offset: 0x00001498
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WorkerSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000032A4 File Offset: 0x000014A4
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000032AC File Offset: 0x000014AC
		[Serialize]
		public string WorkerType { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x000032B8 File Offset: 0x000014B8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WorkerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003304 File Offset: 0x00001504
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WorkerType = ");
			builder.Append(this.WorkerType);
			return true;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003335 File Offset: 0x00001535
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WorkerSpec left, WorkerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003341 File Offset: 0x00001541
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WorkerSpec left, WorkerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003355 File Offset: 0x00001555
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkerType>k__BackingField);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003374 File Offset: 0x00001574
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkerSpec);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002511 File Offset: 0x00000711
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003382 File Offset: 0x00001582
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WorkerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WorkerType>k__BackingField, other.<WorkerType>k__BackingField));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000033B3 File Offset: 0x000015B3
		[CompilerGenerated]
		protected WorkerSpec([Nullable(1)] WorkerSpec original) : base(original)
		{
			this.WorkerType = original.<WorkerType>k__BackingField;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002591 File Offset: 0x00000791
		public WorkerSpec()
		{
		}
	}
}
