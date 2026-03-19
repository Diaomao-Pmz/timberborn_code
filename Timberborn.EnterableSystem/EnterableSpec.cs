using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class EnterableSpec : ComponentSpec, IEquatable<EnterableSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002BB4 File Offset: 0x00000DB4
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EnterableSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002BC0 File Offset: 0x00000DC0
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002BC8 File Offset: 0x00000DC8
		[Serialize]
		public OperatingState OperatingState { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002BD1 File Offset: 0x00000DD1
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002BD9 File Offset: 0x00000DD9
		[Serialize]
		public bool LimitedCapacityFinished { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002BE2 File Offset: 0x00000DE2
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002BEA File Offset: 0x00000DEA
		[Serialize]
		public int CapacityFinished { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002BF3 File Offset: 0x00000DF3
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002BFB File Offset: 0x00000DFB
		[Serialize]
		public bool LimitedCapacityUnfinished { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002C04 File Offset: 0x00000E04
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002C0C File Offset: 0x00000E0C
		[Serialize]
		public int CapacityUnfinished { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002C18 File Offset: 0x00000E18
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnterableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002C64 File Offset: 0x00000E64
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OperatingState = ");
			builder.Append(this.OperatingState.ToString());
			builder.Append(", LimitedCapacityFinished = ");
			builder.Append(this.LimitedCapacityFinished.ToString());
			builder.Append(", CapacityFinished = ");
			builder.Append(this.CapacityFinished.ToString());
			builder.Append(", LimitedCapacityUnfinished = ");
			builder.Append(this.LimitedCapacityUnfinished.ToString());
			builder.Append(", CapacityUnfinished = ");
			builder.Append(this.CapacityUnfinished.ToString());
			return true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D4A File Offset: 0x00000F4A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnterableSpec left, EnterableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D56 File Offset: 0x00000F56
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnterableSpec left, EnterableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D6C File Offset: 0x00000F6C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<OperatingState>.Default.GetHashCode(this.<OperatingState>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<LimitedCapacityFinished>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CapacityFinished>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<LimitedCapacityUnfinished>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CapacityUnfinished>k__BackingField);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002DF2 File Offset: 0x00000FF2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnterableSpec);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E00 File Offset: 0x00001000
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnterableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<OperatingState>.Default.Equals(this.<OperatingState>k__BackingField, other.<OperatingState>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<LimitedCapacityFinished>k__BackingField, other.<LimitedCapacityFinished>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CapacityFinished>k__BackingField, other.<CapacityFinished>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<LimitedCapacityUnfinished>k__BackingField, other.<LimitedCapacityUnfinished>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CapacityUnfinished>k__BackingField, other.<CapacityUnfinished>k__BackingField));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EA0 File Offset: 0x000010A0
		[CompilerGenerated]
		protected EnterableSpec(EnterableSpec original) : base(original)
		{
			this.OperatingState = original.<OperatingState>k__BackingField;
			this.LimitedCapacityFinished = original.<LimitedCapacityFinished>k__BackingField;
			this.CapacityFinished = original.<CapacityFinished>k__BackingField;
			this.LimitedCapacityUnfinished = original.<LimitedCapacityUnfinished>k__BackingField;
			this.CapacityUnfinished = original.<CapacityUnfinished>k__BackingField;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000279E File Offset: 0x0000099E
		public EnterableSpec()
		{
		}
	}
}
