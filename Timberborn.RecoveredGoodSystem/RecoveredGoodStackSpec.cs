using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class RecoveredGoodStackSpec : ComponentSpec, IEquatable<RecoveredGoodStackSpec>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003968 File Offset: 0x00001B68
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RecoveredGoodStackSpec);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003974 File Offset: 0x00001B74
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecoveredGoodStackSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000039C0 File Offset: 0x00001BC0
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039C9 File Offset: 0x00001BC9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecoveredGoodStackSpec left, RecoveredGoodStackSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000039D5 File Offset: 0x00001BD5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecoveredGoodStackSpec left, RecoveredGoodStackSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000039E9 File Offset: 0x00001BE9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039F1 File Offset: 0x00001BF1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecoveredGoodStackSpec);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002F69 File Offset: 0x00001169
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000039FF File Offset: 0x00001BFF
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecoveredGoodStackSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A16 File Offset: 0x00001C16
		[CompilerGenerated]
		protected RecoveredGoodStackSpec(RecoveredGoodStackSpec original) : base(original)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002FE9 File Offset: 0x000011E9
		public RecoveredGoodStackSpec()
		{
		}
	}
}
