using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000008 RID: 8
	public class ContinuousEffectSpec : IEquatable<ContinuousEffectSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022BD File Offset: 0x000004BD
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ContinuousEffectSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C9 File Offset: 0x000004C9
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022D1 File Offset: 0x000004D1
		[Serialize]
		public string NeedId { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022DA File Offset: 0x000004DA
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000022E2 File Offset: 0x000004E2
		[Serialize]
		public float PointsPerHour { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022EB File Offset: 0x000004EB
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000022F3 File Offset: 0x000004F3
		[Serialize]
		public bool SatisfyToMaxValue { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000022FC File Offset: 0x000004FC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ContinuousEffectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002348 File Offset: 0x00000548
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("NeedId = ");
			builder.Append(this.NeedId);
			builder.Append(", PointsPerHour = ");
			builder.Append(this.PointsPerHour.ToString());
			builder.Append(", SatisfyToMaxValue = ");
			builder.Append(this.SatisfyToMaxValue.ToString());
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C2 File Offset: 0x000005C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ContinuousEffectSpec left, ContinuousEffectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023CE File Offset: 0x000005CE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ContinuousEffectSpec left, ContinuousEffectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023E4 File Offset: 0x000005E4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NeedId>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<PointsPerHour>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SatisfyToMaxValue>k__BackingField);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002446 File Offset: 0x00000646
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ContinuousEffectSpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002454 File Offset: 0x00000654
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ContinuousEffectSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NeedId>k__BackingField, other.<NeedId>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<PointsPerHour>k__BackingField, other.<PointsPerHour>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SatisfyToMaxValue>k__BackingField, other.<SatisfyToMaxValue>k__BackingField));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024CD File Offset: 0x000006CD
		[CompilerGenerated]
		protected ContinuousEffectSpec([Nullable(1)] ContinuousEffectSpec original)
		{
			this.NeedId = original.<NeedId>k__BackingField;
			this.PointsPerHour = original.<PointsPerHour>k__BackingField;
			this.SatisfyToMaxValue = original.<SatisfyToMaxValue>k__BackingField;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000020F6 File Offset: 0x000002F6
		public ContinuousEffectSpec()
		{
		}
	}
}
