using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class MechanicalConnectorTargetSpec : ComponentSpec, IEquatable<MechanicalConnectorTargetSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026DA File Offset: 0x000008DA
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalConnectorTargetSpec);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026E8 File Offset: 0x000008E8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalConnectorTargetSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002734 File Offset: 0x00000934
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000273D File Offset: 0x0000093D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalConnectorTargetSpec left, MechanicalConnectorTargetSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002749 File Offset: 0x00000949
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalConnectorTargetSpec left, MechanicalConnectorTargetSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000275D File Offset: 0x0000095D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002765 File Offset: 0x00000965
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalConnectorTargetSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002532 File Offset: 0x00000732
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002773 File Offset: 0x00000973
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalConnectorTargetSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000278A File Offset: 0x0000098A
		[CompilerGenerated]
		protected MechanicalConnectorTargetSpec(MechanicalConnectorTargetSpec original) : base(original)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002581 File Offset: 0x00000781
		public MechanicalConnectorTargetSpec()
		{
		}
	}
}
