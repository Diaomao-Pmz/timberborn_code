using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.FeatureToggleSystem;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001C RID: 28
	public class DisablingFeatureToggleSpec : ComponentSpec, IEquatable<DisablingFeatureToggleSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003BB9 File Offset: 0x00001DB9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DisablingFeatureToggleSpec);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003BC5 File Offset: 0x00001DC5
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003BCD File Offset: 0x00001DCD
		[Serialize]
		public string Toggle { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003BD6 File Offset: 0x00001DD6
		public bool Disabled
		{
			get
			{
				return FeatureToggleService.IsToggleOn(this.Toggle);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DisablingFeatureToggleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003C30 File Offset: 0x00001E30
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Toggle = ");
			builder.Append(this.Toggle);
			builder.Append(", Disabled = ");
			builder.Append(this.Disabled.ToString());
			return true;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003C93 File Offset: 0x00001E93
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DisablingFeatureToggleSpec left, DisablingFeatureToggleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003C9F File Offset: 0x00001E9F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DisablingFeatureToggleSpec left, DisablingFeatureToggleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003CB3 File Offset: 0x00001EB3
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toggle>k__BackingField);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CD2 File Offset: 0x00001ED2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DisablingFeatureToggleSpec);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003CE9 File Offset: 0x00001EE9
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DisablingFeatureToggleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Toggle>k__BackingField, other.<Toggle>k__BackingField));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003D1A File Offset: 0x00001F1A
		[CompilerGenerated]
		protected DisablingFeatureToggleSpec([Nullable(1)] DisablingFeatureToggleSpec original) : base(original)
		{
			this.Toggle = original.<Toggle>k__BackingField;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003D2F File Offset: 0x00001F2F
		public DisablingFeatureToggleSpec()
		{
		}
	}
}
