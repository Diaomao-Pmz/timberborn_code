using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	public class ValveSpec : ComponentSpec, IEquatable<ValveSpec>
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005FB7 File Offset: 0x000041B7
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ValveSpec);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005FC3 File Offset: 0x000041C3
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00005FCB File Offset: 0x000041CB
		[Serialize]
		public float MaxOutflowLimit { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005FD4 File Offset: 0x000041D4
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00005FDC File Offset: 0x000041DC
		[Serialize]
		public float OutflowLimitStep { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00005FE5 File Offset: 0x000041E5
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00005FED File Offset: 0x000041ED
		[Serialize]
		public bool DefaultOutflowLimitEnabled { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00005FF6 File Offset: 0x000041F6
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00005FFE File Offset: 0x000041FE
		[Serialize]
		public float DefaultOutflowLimit { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006007 File Offset: 0x00004207
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000600F File Offset: 0x0000420F
		[Serialize]
		public bool DefaultAutomationOutflowLimitEnabled { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006018 File Offset: 0x00004218
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006020 File Offset: 0x00004220
		[Serialize]
		public float DefaultAutomationOutflowLimit { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006029 File Offset: 0x00004229
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00006031 File Offset: 0x00004231
		[Serialize]
		public float RateOfChangeHighPrimary { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000603A File Offset: 0x0000423A
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00006042 File Offset: 0x00004242
		[Serialize]
		public float RateOfChangeHighSecondary { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000604B File Offset: 0x0000424B
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00006053 File Offset: 0x00004253
		[Serialize]
		public float RateOfChangeLowPrimary { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000605C File Offset: 0x0000425C
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00006064 File Offset: 0x00004264
		[Serialize]
		public float RateOfChangeLowSecondary { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000606D File Offset: 0x0000426D
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00006075 File Offset: 0x00004275
		[Serialize]
		public int RateOfChangePrimaryTicks { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000607E File Offset: 0x0000427E
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00006086 File Offset: 0x00004286
		[Serialize]
		public int RateOfChangePrimaryToSecondaryTicks { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000608F File Offset: 0x0000428F
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00006097 File Offset: 0x00004297
		[Serialize]
		public float ReactionSpeedExponent { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000060A0 File Offset: 0x000042A0
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000060A8 File Offset: 0x000042A8
		[Serialize]
		public float ReactionSpeedStep { get; set; }

		// Token: 0x06000208 RID: 520 RVA: 0x000060B4 File Offset: 0x000042B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ValveSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006100 File Offset: 0x00004300
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MaxOutflowLimit = ");
			builder.Append(this.MaxOutflowLimit.ToString());
			builder.Append(", OutflowLimitStep = ");
			builder.Append(this.OutflowLimitStep.ToString());
			builder.Append(", DefaultOutflowLimitEnabled = ");
			builder.Append(this.DefaultOutflowLimitEnabled.ToString());
			builder.Append(", DefaultOutflowLimit = ");
			builder.Append(this.DefaultOutflowLimit.ToString());
			builder.Append(", DefaultAutomationOutflowLimitEnabled = ");
			builder.Append(this.DefaultAutomationOutflowLimitEnabled.ToString());
			builder.Append(", DefaultAutomationOutflowLimit = ");
			builder.Append(this.DefaultAutomationOutflowLimit.ToString());
			builder.Append(", RateOfChangeHighPrimary = ");
			builder.Append(this.RateOfChangeHighPrimary.ToString());
			builder.Append(", RateOfChangeHighSecondary = ");
			builder.Append(this.RateOfChangeHighSecondary.ToString());
			builder.Append(", RateOfChangeLowPrimary = ");
			builder.Append(this.RateOfChangeLowPrimary.ToString());
			builder.Append(", RateOfChangeLowSecondary = ");
			builder.Append(this.RateOfChangeLowSecondary.ToString());
			builder.Append(", RateOfChangePrimaryTicks = ");
			builder.Append(this.RateOfChangePrimaryTicks.ToString());
			builder.Append(", RateOfChangePrimaryToSecondaryTicks = ");
			builder.Append(this.RateOfChangePrimaryToSecondaryTicks.ToString());
			builder.Append(", ReactionSpeedExponent = ");
			builder.Append(this.ReactionSpeedExponent.ToString());
			builder.Append(", ReactionSpeedStep = ");
			builder.Append(this.ReactionSpeedStep.ToString());
			return true;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006345 File Offset: 0x00004545
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ValveSpec left, ValveSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006351 File Offset: 0x00004551
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ValveSpec left, ValveSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006368 File Offset: 0x00004568
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxOutflowLimit>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<OutflowLimitStep>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DefaultOutflowLimitEnabled>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultOutflowLimit>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DefaultAutomationOutflowLimitEnabled>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultAutomationOutflowLimit>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RateOfChangeHighPrimary>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RateOfChangeHighSecondary>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RateOfChangeLowPrimary>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RateOfChangeLowSecondary>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RateOfChangePrimaryTicks>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<RateOfChangePrimaryToSecondaryTicks>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ReactionSpeedExponent>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ReactionSpeedStep>k__BackingField);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000064BD File Offset: 0x000046BD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ValveSpec);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000064CC File Offset: 0x000046CC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ValveSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MaxOutflowLimit>k__BackingField, other.<MaxOutflowLimit>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<OutflowLimitStep>k__BackingField, other.<OutflowLimitStep>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DefaultOutflowLimitEnabled>k__BackingField, other.<DefaultOutflowLimitEnabled>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DefaultOutflowLimit>k__BackingField, other.<DefaultOutflowLimit>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<DefaultAutomationOutflowLimitEnabled>k__BackingField, other.<DefaultAutomationOutflowLimitEnabled>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DefaultAutomationOutflowLimit>k__BackingField, other.<DefaultAutomationOutflowLimit>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RateOfChangeHighPrimary>k__BackingField, other.<RateOfChangeHighPrimary>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RateOfChangeHighSecondary>k__BackingField, other.<RateOfChangeHighSecondary>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RateOfChangeLowPrimary>k__BackingField, other.<RateOfChangeLowPrimary>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RateOfChangeLowSecondary>k__BackingField, other.<RateOfChangeLowSecondary>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RateOfChangePrimaryTicks>k__BackingField, other.<RateOfChangePrimaryTicks>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<RateOfChangePrimaryToSecondaryTicks>k__BackingField, other.<RateOfChangePrimaryToSecondaryTicks>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ReactionSpeedExponent>k__BackingField, other.<ReactionSpeedExponent>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ReactionSpeedStep>k__BackingField, other.<ReactionSpeedStep>k__BackingField));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006660 File Offset: 0x00004860
		[CompilerGenerated]
		protected ValveSpec(ValveSpec original) : base(original)
		{
			this.MaxOutflowLimit = original.<MaxOutflowLimit>k__BackingField;
			this.OutflowLimitStep = original.<OutflowLimitStep>k__BackingField;
			this.DefaultOutflowLimitEnabled = original.<DefaultOutflowLimitEnabled>k__BackingField;
			this.DefaultOutflowLimit = original.<DefaultOutflowLimit>k__BackingField;
			this.DefaultAutomationOutflowLimitEnabled = original.<DefaultAutomationOutflowLimitEnabled>k__BackingField;
			this.DefaultAutomationOutflowLimit = original.<DefaultAutomationOutflowLimit>k__BackingField;
			this.RateOfChangeHighPrimary = original.<RateOfChangeHighPrimary>k__BackingField;
			this.RateOfChangeHighSecondary = original.<RateOfChangeHighSecondary>k__BackingField;
			this.RateOfChangeLowPrimary = original.<RateOfChangeLowPrimary>k__BackingField;
			this.RateOfChangeLowSecondary = original.<RateOfChangeLowSecondary>k__BackingField;
			this.RateOfChangePrimaryTicks = original.<RateOfChangePrimaryTicks>k__BackingField;
			this.RateOfChangePrimaryToSecondaryTicks = original.<RateOfChangePrimaryToSecondaryTicks>k__BackingField;
			this.ReactionSpeedExponent = original.<ReactionSpeedExponent>k__BackingField;
			this.ReactionSpeedStep = original.<ReactionSpeedStep>k__BackingField;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00002CBC File Offset: 0x00000EBC
		public ValveSpec()
		{
		}
	}
}
