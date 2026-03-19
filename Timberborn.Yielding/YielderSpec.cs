using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Goods;

namespace Timberborn.Yielding
{
	// Token: 0x02000015 RID: 21
	public class YielderSpec : IEquatable<YielderSpec>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000352E File Offset: 0x0000172E
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(YielderSpec);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000353A File Offset: 0x0000173A
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003542 File Offset: 0x00001742
		[Serialize]
		public string YielderComponentName { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000354B File Offset: 0x0000174B
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003553 File Offset: 0x00001753
		[Serialize]
		public GoodAmountSpec Yield { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000355C File Offset: 0x0000175C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003564 File Offset: 0x00001764
		[Serialize]
		public float RemovalTimeInHours { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000356D File Offset: 0x0000176D
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003575 File Offset: 0x00001775
		[Serialize]
		public string ResourceGroup { get; set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00003580 File Offset: 0x00001780
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YielderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000035CC File Offset: 0x000017CC
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("YielderComponentName = ");
			builder.Append(this.YielderComponentName);
			builder.Append(", Yield = ");
			builder.Append(this.Yield);
			builder.Append(", RemovalTimeInHours = ");
			builder.Append(this.RemovalTimeInHours.ToString());
			builder.Append(", ResourceGroup = ");
			builder.Append(this.ResourceGroup);
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003651 File Offset: 0x00001851
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(YielderSpec left, YielderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000365D File Offset: 0x0000185D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(YielderSpec left, YielderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003674 File Offset: 0x00001874
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<YielderComponentName>k__BackingField)) * -1521134295 + EqualityComparer<GoodAmountSpec>.Default.GetHashCode(this.<Yield>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RemovalTimeInHours>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResourceGroup>k__BackingField);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000036ED File Offset: 0x000018ED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as YielderSpec);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000036FC File Offset: 0x000018FC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(YielderSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<YielderComponentName>k__BackingField, other.<YielderComponentName>k__BackingField) && EqualityComparer<GoodAmountSpec>.Default.Equals(this.<Yield>k__BackingField, other.<Yield>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RemovalTimeInHours>k__BackingField, other.<RemovalTimeInHours>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ResourceGroup>k__BackingField, other.<ResourceGroup>k__BackingField));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000378D File Offset: 0x0000198D
		[CompilerGenerated]
		protected YielderSpec([Nullable(1)] YielderSpec original)
		{
			this.YielderComponentName = original.<YielderComponentName>k__BackingField;
			this.Yield = original.<Yield>k__BackingField;
			this.RemovalTimeInHours = original.<RemovalTimeInHours>k__BackingField;
			this.ResourceGroup = original.<ResourceGroup>k__BackingField;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000020F8 File Offset: 0x000002F8
		public YielderSpec()
		{
		}
	}
}
