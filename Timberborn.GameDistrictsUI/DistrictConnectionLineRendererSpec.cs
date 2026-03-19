using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000013 RID: 19
	public class DistrictConnectionLineRendererSpec : ComponentSpec, IEquatable<DistrictConnectionLineRendererSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FEE File Offset: 0x000011EE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DistrictConnectionLineRendererSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002FFA File Offset: 0x000011FA
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003002 File Offset: 0x00001202
		[Serialize]
		public AssetRef<LineRenderer> LineRendererPrefab { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000300B File Offset: 0x0000120B
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003013 File Offset: 0x00001213
		[Serialize]
		public float ArcAngle { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000301C File Offset: 0x0000121C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003024 File Offset: 0x00001224
		[Serialize]
		public int CurvePoints { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000302D File Offset: 0x0000122D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003035 File Offset: 0x00001235
		[Serialize]
		public float LineCutoff { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00003040 File Offset: 0x00001240
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictConnectionLineRendererSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000308C File Offset: 0x0000128C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LineRendererPrefab = ");
			builder.Append(this.LineRendererPrefab);
			builder.Append(", ArcAngle = ");
			builder.Append(this.ArcAngle.ToString());
			builder.Append(", CurvePoints = ");
			builder.Append(this.CurvePoints.ToString());
			builder.Append(", LineCutoff = ");
			builder.Append(this.LineCutoff.ToString());
			return true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000313D File Offset: 0x0000133D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictConnectionLineRendererSpec left, DistrictConnectionLineRendererSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003149 File Offset: 0x00001349
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictConnectionLineRendererSpec left, DistrictConnectionLineRendererSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003160 File Offset: 0x00001360
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<LineRenderer>>.Default.GetHashCode(this.<LineRendererPrefab>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ArcAngle>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CurvePoints>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<LineCutoff>k__BackingField);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031CF File Offset: 0x000013CF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictConnectionLineRendererSpec);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002437 File Offset: 0x00000637
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031E0 File Offset: 0x000013E0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictConnectionLineRendererSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<LineRenderer>>.Default.Equals(this.<LineRendererPrefab>k__BackingField, other.<LineRendererPrefab>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ArcAngle>k__BackingField, other.<ArcAngle>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CurvePoints>k__BackingField, other.<CurvePoints>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<LineCutoff>k__BackingField, other.<LineCutoff>k__BackingField));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003264 File Offset: 0x00001464
		[CompilerGenerated]
		protected DistrictConnectionLineRendererSpec([Nullable(1)] DistrictConnectionLineRendererSpec original) : base(original)
		{
			this.LineRendererPrefab = original.<LineRendererPrefab>k__BackingField;
			this.ArcAngle = original.<ArcAngle>k__BackingField;
			this.CurvePoints = original.<CurvePoints>k__BackingField;
			this.LineCutoff = original.<LineCutoff>k__BackingField;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002486 File Offset: 0x00000686
		public DistrictConnectionLineRendererSpec()
		{
		}
	}
}
