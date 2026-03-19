using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.FeatureToggleSystem;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000022 RID: 34
	public class RequiredFeatureToggleSpec : ComponentSpec, IEquatable<RequiredFeatureToggleSpec>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000040A9 File Offset: 0x000022A9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RequiredFeatureToggleSpec);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000040B5 File Offset: 0x000022B5
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000040BD File Offset: 0x000022BD
		[Serialize]
		public string Toggle { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000040C6 File Offset: 0x000022C6
		public bool Disabled
		{
			get
			{
				return !FeatureToggleService.IsToggleOn(this.Toggle);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000040D8 File Offset: 0x000022D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RequiredFeatureToggleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004124 File Offset: 0x00002324
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

		// Token: 0x060000EB RID: 235 RVA: 0x00004187 File Offset: 0x00002387
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RequiredFeatureToggleSpec left, RequiredFeatureToggleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004193 File Offset: 0x00002393
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RequiredFeatureToggleSpec left, RequiredFeatureToggleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000041A7 File Offset: 0x000023A7
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toggle>k__BackingField);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000041C6 File Offset: 0x000023C6
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RequiredFeatureToggleSpec);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000041D4 File Offset: 0x000023D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RequiredFeatureToggleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Toggle>k__BackingField, other.<Toggle>k__BackingField));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004205 File Offset: 0x00002405
		[CompilerGenerated]
		protected RequiredFeatureToggleSpec([Nullable(1)] RequiredFeatureToggleSpec original) : base(original)
		{
			this.Toggle = original.<Toggle>k__BackingField;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003D2F File Offset: 0x00001F2F
		public RequiredFeatureToggleSpec()
		{
		}
	}
}
