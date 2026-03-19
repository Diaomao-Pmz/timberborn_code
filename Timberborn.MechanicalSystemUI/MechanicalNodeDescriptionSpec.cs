using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200001B RID: 27
	public class MechanicalNodeDescriptionSpec : ComponentSpec, IEquatable<MechanicalNodeDescriptionSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003307 File Offset: 0x00001507
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeDescriptionSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003313 File Offset: 0x00001513
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000331B File Offset: 0x0000151B
		[Serialize]
		public string AlternativePowerUnitLocKey { get; set; }

		// Token: 0x06000075 RID: 117 RVA: 0x00003324 File Offset: 0x00001524
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeDescriptionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003370 File Offset: 0x00001570
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AlternativePowerUnitLocKey = ");
			builder.Append(this.AlternativePowerUnitLocKey);
			return true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033A1 File Offset: 0x000015A1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeDescriptionSpec left, MechanicalNodeDescriptionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033AD File Offset: 0x000015AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeDescriptionSpec left, MechanicalNodeDescriptionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033C1 File Offset: 0x000015C1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AlternativePowerUnitLocKey>k__BackingField);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033E0 File Offset: 0x000015E0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeDescriptionSpec);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003027 File Offset: 0x00001227
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000033EE File Offset: 0x000015EE
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeDescriptionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AlternativePowerUnitLocKey>k__BackingField, other.<AlternativePowerUnitLocKey>k__BackingField));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000341F File Offset: 0x0000161F
		[CompilerGenerated]
		protected MechanicalNodeDescriptionSpec([Nullable(1)] MechanicalNodeDescriptionSpec original) : base(original)
		{
			this.AlternativePowerUnitLocKey = original.<AlternativePowerUnitLocKey>k__BackingField;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003076 File Offset: 0x00001276
		public MechanicalNodeDescriptionSpec()
		{
		}
	}
}
