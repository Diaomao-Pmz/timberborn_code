using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000011 RID: 17
	public class GoodVisualizationSpec : ComponentSpec, IEquatable<GoodVisualizationSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003162 File Offset: 0x00001362
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(GoodVisualizationSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000316E File Offset: 0x0000136E
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003176 File Offset: 0x00001376
		[Serialize]
		public string Id { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000317F File Offset: 0x0000137F
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003187 File Offset: 0x00001387
		[Serialize]
		public string Variant { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003198 File Offset: 0x00001398
		[Serialize]
		public Vector3 Offset { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000031A1 File Offset: 0x000013A1
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000031A9 File Offset: 0x000013A9
		[Serialize]
		public float LimitingAmount { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000031B2 File Offset: 0x000013B2
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000031BA File Offset: 0x000013BA
		[Serialize]
		public AssetRef<Mesh> PrimaryMesh { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000031C3 File Offset: 0x000013C3
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000031CB File Offset: 0x000013CB
		[Serialize]
		public AssetRef<Mesh> SecondaryMesh { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000031D4 File Offset: 0x000013D4
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000031DC File Offset: 0x000013DC
		[Serialize]
		public AssetRef<Material> Material { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000031E5 File Offset: 0x000013E5
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000031ED File Offset: 0x000013ED
		[Serialize]
		public float NonLinearity { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000031F6 File Offset: 0x000013F6
		public int LimitingAmountFlooredToInt
		{
			get
			{
				return (int)this.LimitingAmount;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003200 File Offset: 0x00001400
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodVisualizationSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000324C File Offset: 0x0000144C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Variant = ");
			builder.Append(this.Variant);
			builder.Append(", Offset = ");
			builder.Append(this.Offset.ToString());
			builder.Append(", LimitingAmount = ");
			builder.Append(this.LimitingAmount.ToString());
			builder.Append(", PrimaryMesh = ");
			builder.Append(this.PrimaryMesh);
			builder.Append(", SecondaryMesh = ");
			builder.Append(this.SecondaryMesh);
			builder.Append(", Material = ");
			builder.Append(this.Material);
			builder.Append(", NonLinearity = ");
			builder.Append(this.NonLinearity.ToString());
			builder.Append(", LimitingAmountFlooredToInt = ");
			builder.Append(this.LimitingAmountFlooredToInt.ToString());
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003388 File Offset: 0x00001588
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodVisualizationSpec left, GoodVisualizationSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003394 File Offset: 0x00001594
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodVisualizationSpec left, GoodVisualizationSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000033A8 File Offset: 0x000015A8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Variant>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Offset>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<LimitingAmount>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<PrimaryMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<SecondaryMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Material>>.Default.GetHashCode(this.<Material>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<NonLinearity>k__BackingField);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003473 File Offset: 0x00001673
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodVisualizationSpec);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003481 File Offset: 0x00001681
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000348C File Offset: 0x0000168C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodVisualizationSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Variant>k__BackingField, other.<Variant>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Offset>k__BackingField, other.<Offset>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<LimitingAmount>k__BackingField, other.<LimitingAmount>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<PrimaryMesh>k__BackingField, other.<PrimaryMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<SecondaryMesh>k__BackingField, other.<SecondaryMesh>k__BackingField) && EqualityComparer<AssetRef<UnityEngine.Material>>.Default.Equals(this.<Material>k__BackingField, other.<Material>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<NonLinearity>k__BackingField, other.<NonLinearity>k__BackingField));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000357C File Offset: 0x0000177C
		[CompilerGenerated]
		protected GoodVisualizationSpec([Nullable(1)] GoodVisualizationSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Variant = original.<Variant>k__BackingField;
			this.Offset = original.<Offset>k__BackingField;
			this.LimitingAmount = original.<LimitingAmount>k__BackingField;
			this.PrimaryMesh = original.<PrimaryMesh>k__BackingField;
			this.SecondaryMesh = original.<SecondaryMesh>k__BackingField;
			this.Material = original.<Material>k__BackingField;
			this.NonLinearity = original.<NonLinearity>k__BackingField;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035F0 File Offset: 0x000017F0
		public GoodVisualizationSpec()
		{
		}
	}
}
