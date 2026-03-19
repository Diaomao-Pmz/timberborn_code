using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public class PowerMeterSpec : ComponentSpec, IEquatable<PowerMeterSpec>
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006AC4 File Offset: 0x00004CC4
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PowerMeterSpec);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006AD0 File Offset: 0x00004CD0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PowerMeterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006B1C File Offset: 0x00004D1C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PowerMeterSpec left, PowerMeterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006B28 File Offset: 0x00004D28
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PowerMeterSpec left, PowerMeterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006B3C File Offset: 0x00004D3C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PowerMeterSpec);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PowerMeterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected PowerMeterSpec(PowerMeterSpec original) : base(original)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00002778 File Offset: 0x00000978
		public PowerMeterSpec()
		{
		}
	}
}
