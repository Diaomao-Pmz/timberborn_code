using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BonusSystem
{
	// Token: 0x0200000B RID: 11
	public class BonusSpec : IEquatable<BonusSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000268A File Offset: 0x0000088A
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BonusSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002696 File Offset: 0x00000896
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000269E File Offset: 0x0000089E
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026A7 File Offset: 0x000008A7
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026AF File Offset: 0x000008AF
		[Serialize]
		public float MultiplierDelta { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x000026B8 File Offset: 0x000008B8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BonusSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002704 File Offset: 0x00000904
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", MultiplierDelta = ");
			builder.Append(this.MultiplierDelta.ToString());
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002757 File Offset: 0x00000957
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BonusSpec left, BonusSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002763 File Offset: 0x00000963
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BonusSpec left, BonusSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002777 File Offset: 0x00000977
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MultiplierDelta>k__BackingField);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027B7 File Offset: 0x000009B7
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BonusSpec);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027C8 File Offset: 0x000009C8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BonusSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MultiplierDelta>k__BackingField, other.<MultiplierDelta>k__BackingField));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002829 File Offset: 0x00000A29
		[CompilerGenerated]
		protected BonusSpec([Nullable(1)] BonusSpec original)
		{
			this.Id = original.<Id>k__BackingField;
			this.MultiplierDelta = original.<MultiplierDelta>k__BackingField;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000020F6 File Offset: 0x000002F6
		public BonusSpec()
		{
		}
	}
}
