using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public class WaterDepthStrengthModifierSpec : ComponentSpec, IEquatable<WaterDepthStrengthModifierSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002F95 File Offset: 0x00001195
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(WaterDepthStrengthModifierSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FA1 File Offset: 0x000011A1
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002FA9 File Offset: 0x000011A9
		[Serialize]
		public float DepthLimit { get; set; }

		// Token: 0x06000097 RID: 151 RVA: 0x00002FB4 File Offset: 0x000011B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterDepthStrengthModifierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003000 File Offset: 0x00001200
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DepthLimit = ");
			builder.Append(this.DepthLimit.ToString());
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000304A File Offset: 0x0000124A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterDepthStrengthModifierSpec left, WaterDepthStrengthModifierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003056 File Offset: 0x00001256
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterDepthStrengthModifierSpec left, WaterDepthStrengthModifierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000306A File Offset: 0x0000126A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DepthLimit>k__BackingField);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003089 File Offset: 0x00001289
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterDepthStrengthModifierSpec);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003097 File Offset: 0x00001297
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterDepthStrengthModifierSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<DepthLimit>k__BackingField, other.<DepthLimit>k__BackingField));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000030C8 File Offset: 0x000012C8
		[CompilerGenerated]
		protected WaterDepthStrengthModifierSpec(WaterDepthStrengthModifierSpec original) : base(original)
		{
			this.DepthLimit = original.<DepthLimit>k__BackingField;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000030DD File Offset: 0x000012DD
		public WaterDepthStrengthModifierSpec()
		{
			this.DepthLimit = 0.8f;
			base..ctor();
		}
	}
}
