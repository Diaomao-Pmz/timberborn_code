using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000016 RID: 22
	public class RecoveredGoodStackModelSpec : ComponentSpec, IEquatable<RecoveredGoodStackModelSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000033DA File Offset: 0x000015DA
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RecoveredGoodStackModelSpec);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000033E6 File Offset: 0x000015E6
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000033EE File Offset: 0x000015EE
		[Serialize]
		public string ModelName { get; set; }

		// Token: 0x0600008E RID: 142 RVA: 0x000033F8 File Offset: 0x000015F8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RecoveredGoodStackModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003444 File Offset: 0x00001644
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ModelName = ");
			builder.Append(this.ModelName);
			return true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003475 File Offset: 0x00001675
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RecoveredGoodStackModelSpec left, RecoveredGoodStackModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003481 File Offset: 0x00001681
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RecoveredGoodStackModelSpec left, RecoveredGoodStackModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003495 File Offset: 0x00001695
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelName>k__BackingField);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000034B4 File Offset: 0x000016B4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RecoveredGoodStackModelSpec);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002F69 File Offset: 0x00001169
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034C2 File Offset: 0x000016C2
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RecoveredGoodStackModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ModelName>k__BackingField, other.<ModelName>k__BackingField));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000034F3 File Offset: 0x000016F3
		[CompilerGenerated]
		protected RecoveredGoodStackModelSpec([Nullable(1)] RecoveredGoodStackModelSpec original) : base(original)
		{
			this.ModelName = original.<ModelName>k__BackingField;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FE9 File Offset: 0x000011E9
		public RecoveredGoodStackModelSpec()
		{
		}
	}
}
