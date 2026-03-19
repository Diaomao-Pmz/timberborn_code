using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x0200000B RID: 11
	public class DrillScrewBuilderSpec : ComponentSpec, IEquatable<DrillScrewBuilderSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002921 File Offset: 0x00000B21
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DrillScrewBuilderSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000292D File Offset: 0x00000B2D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002935 File Offset: 0x00000B35
		[Serialize]
		public string ParentName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000293E File Offset: 0x00000B3E
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002946 File Offset: 0x00000B46
		[Serialize]
		public Vector3 AnchorPosition { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000294F File Offset: 0x00000B4F
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002957 File Offset: 0x00000B57
		[Serialize]
		public float DrillRadius { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002968 File Offset: 0x00000B68
		[Serialize]
		public string ScrewHeadPrefabPath { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002971 File Offset: 0x00000B71
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002979 File Offset: 0x00000B79
		[Serialize]
		public string ScrewAxisPrefabPath { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002984 File Offset: 0x00000B84
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrillScrewBuilderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029D0 File Offset: 0x00000BD0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ParentName = ");
			builder.Append(this.ParentName);
			builder.Append(", AnchorPosition = ");
			builder.Append(this.AnchorPosition.ToString());
			builder.Append(", DrillRadius = ");
			builder.Append(this.DrillRadius.ToString());
			builder.Append(", ScrewHeadPrefabPath = ");
			builder.Append(this.ScrewHeadPrefabPath);
			builder.Append(", ScrewAxisPrefabPath = ");
			builder.Append(this.ScrewAxisPrefabPath);
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A8C File Offset: 0x00000C8C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrillScrewBuilderSpec left, DrillScrewBuilderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A98 File Offset: 0x00000C98
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrillScrewBuilderSpec left, DrillScrewBuilderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AAC File Offset: 0x00000CAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParentName>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<AnchorPosition>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DrillRadius>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ScrewHeadPrefabPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ScrewAxisPrefabPath>k__BackingField);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B32 File Offset: 0x00000D32
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrillScrewBuilderSpec);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000262F File Offset: 0x0000082F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B40 File Offset: 0x00000D40
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrillScrewBuilderSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ParentName>k__BackingField, other.<ParentName>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<AnchorPosition>k__BackingField, other.<AnchorPosition>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DrillRadius>k__BackingField, other.<DrillRadius>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ScrewHeadPrefabPath>k__BackingField, other.<ScrewHeadPrefabPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ScrewAxisPrefabPath>k__BackingField, other.<ScrewAxisPrefabPath>k__BackingField));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BE0 File Offset: 0x00000DE0
		[CompilerGenerated]
		protected DrillScrewBuilderSpec([Nullable(1)] DrillScrewBuilderSpec original) : base(original)
		{
			this.ParentName = original.<ParentName>k__BackingField;
			this.AnchorPosition = original.<AnchorPosition>k__BackingField;
			this.DrillRadius = original.<DrillRadius>k__BackingField;
			this.ScrewHeadPrefabPath = original.<ScrewHeadPrefabPath>k__BackingField;
			this.ScrewAxisPrefabPath = original.<ScrewAxisPrefabPath>k__BackingField;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026AD File Offset: 0x000008AD
		public DrillScrewBuilderSpec()
		{
		}
	}
}
