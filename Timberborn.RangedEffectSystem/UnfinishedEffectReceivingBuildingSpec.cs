using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public class UnfinishedEffectReceivingBuildingSpec : ComponentSpec, IEquatable<UnfinishedEffectReceivingBuildingSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003544 File Offset: 0x00001744
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UnfinishedEffectReceivingBuildingSpec);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003550 File Offset: 0x00001750
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnfinishedEffectReceivingBuildingSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000359C File Offset: 0x0000179C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000035A5 File Offset: 0x000017A5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnfinishedEffectReceivingBuildingSpec left, UnfinishedEffectReceivingBuildingSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000035B1 File Offset: 0x000017B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnfinishedEffectReceivingBuildingSpec left, UnfinishedEffectReceivingBuildingSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000035C5 File Offset: 0x000017C5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035CD File Offset: 0x000017CD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnfinishedEffectReceivingBuildingSpec);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000023FB File Offset: 0x000005FB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035DB File Offset: 0x000017DB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnfinishedEffectReceivingBuildingSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000035F2 File Offset: 0x000017F2
		[CompilerGenerated]
		protected UnfinishedEffectReceivingBuildingSpec(UnfinishedEffectReceivingBuildingSpec original) : base(original)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000244A File Offset: 0x0000064A
		public UnfinishedEffectReceivingBuildingSpec()
		{
		}
	}
}
