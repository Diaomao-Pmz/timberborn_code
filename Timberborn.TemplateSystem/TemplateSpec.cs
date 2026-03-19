using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.FeatureToggleSystem;

namespace Timberborn.TemplateSystem
{
	// Token: 0x0200000D RID: 13
	public class TemplateSpec : ComponentSpec, IEquatable<TemplateSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023E9 File Offset: 0x000005E9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TemplateSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023FD File Offset: 0x000005FD
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002406 File Offset: 0x00000606
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000240E File Offset: 0x0000060E
		[Serialize]
		public ImmutableArray<string> BackwardCompatibleTemplateNames { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002417 File Offset: 0x00000617
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000241F File Offset: 0x0000061F
		[Serialize]
		public string RequiredFeatureToggle { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002428 File Offset: 0x00000628
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002430 File Offset: 0x00000630
		[Serialize]
		public string DisablingFeatureToggle { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002439 File Offset: 0x00000639
		public bool UsableWithCurrentFeatureToggles
		{
			get
			{
				return FeatureToggleService.IsToggleOn(this.RequiredFeatureToggle) && (string.IsNullOrEmpty(this.DisablingFeatureToggle) || !FeatureToggleService.IsToggleOn(this.DisablingFeatureToggle));
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002467 File Offset: 0x00000667
		public bool IsNamed(string templateName)
		{
			return this.IsNamedExactly(templateName) || this.BackwardCompatibleTemplateNames.FastContains(templateName);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002485 File Offset: 0x00000685
		public bool IsNamedExactly(string templateName)
		{
			return this.TemplateName == templateName;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002494 File Offset: 0x00000694
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TemplateSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024E0 File Offset: 0x000006E0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateName = ");
			builder.Append(this.TemplateName);
			builder.Append(", BackwardCompatibleTemplateNames = ");
			builder.Append(this.BackwardCompatibleTemplateNames.ToString());
			builder.Append(", RequiredFeatureToggle = ");
			builder.Append(this.RequiredFeatureToggle);
			builder.Append(", DisablingFeatureToggle = ");
			builder.Append(this.DisablingFeatureToggle);
			builder.Append(", UsableWithCurrentFeatureToggles = ");
			builder.Append(this.UsableWithCurrentFeatureToggles.ToString());
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000259C File Offset: 0x0000079C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TemplateSpec left, TemplateSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025A8 File Offset: 0x000007A8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TemplateSpec left, TemplateSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025BC File Offset: 0x000007BC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<BackwardCompatibleTemplateNames>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RequiredFeatureToggle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisablingFeatureToggle>k__BackingField);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000262B File Offset: 0x0000082B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TemplateSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002639 File Offset: 0x00000839
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002644 File Offset: 0x00000844
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TemplateSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<BackwardCompatibleTemplateNames>k__BackingField, other.<BackwardCompatibleTemplateNames>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RequiredFeatureToggle>k__BackingField, other.<RequiredFeatureToggle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisablingFeatureToggle>k__BackingField, other.<DisablingFeatureToggle>k__BackingField));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026C8 File Offset: 0x000008C8
		[CompilerGenerated]
		protected TemplateSpec([Nullable(1)] TemplateSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
			this.BackwardCompatibleTemplateNames = original.<BackwardCompatibleTemplateNames>k__BackingField;
			this.RequiredFeatureToggle = original.<RequiredFeatureToggle>k__BackingField;
			this.DisablingFeatureToggle = original.<DisablingFeatureToggle>k__BackingField;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002701 File Offset: 0x00000901
		public TemplateSpec()
		{
		}
	}
}
