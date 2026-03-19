using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000014 RID: 20
	public class TerrainMaterialMapSpec : ComponentSpec, IEquatable<TerrainMaterialMapSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003C02 File Offset: 0x00001E02
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TerrainMaterialMapSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003C0E File Offset: 0x00001E0E
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003C16 File Offset: 0x00001E16
		[Serialize]
		public AssetRef<Texture2D> DesertTexture { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003C1F File Offset: 0x00001E1F
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003C27 File Offset: 0x00001E27
		[Serialize]
		public AssetRef<Texture2D> DryFieldTexture { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003C38 File Offset: 0x00001E38
		[Serialize]
		public AssetRef<Texture2D> WetFieldTexture { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003C41 File Offset: 0x00001E41
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003C49 File Offset: 0x00001E49
		[Serialize]
		public AssetRef<Texture2D> BlendingNoise { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003C52 File Offset: 0x00001E52
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003C5A File Offset: 0x00001E5A
		[Serialize]
		public float BlendingNoiseScale { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003C63 File Offset: 0x00001E63
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003C6B File Offset: 0x00001E6B
		[Serialize]
		public float BlendingNoiseMultiplier { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003C74 File Offset: 0x00001E74
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003C7C File Offset: 0x00001E7C
		[Serialize]
		public float BlendingSoftness { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003C85 File Offset: 0x00001E85
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003C8D File Offset: 0x00001E8D
		[Serialize]
		public float BlendingMargin { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003C96 File Offset: 0x00001E96
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003C9E File Offset: 0x00001E9E
		[Serialize]
		public float AltitudeCeiling { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003CA7 File Offset: 0x00001EA7
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003CAF File Offset: 0x00001EAF
		[Serialize]
		public AssetRef<Texture> AltitudeMultiplier { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003CB8 File Offset: 0x00001EB8
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003CC0 File Offset: 0x00001EC0
		[Serialize]
		public AssetRef<Texture> DesertAltitudeMultiplier { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003CC9 File Offset: 0x00001EC9
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003CD1 File Offset: 0x00001ED1
		[Serialize]
		public float CutoutMargin { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00003CDC File Offset: 0x00001EDC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TerrainMaterialMapSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D28 File Offset: 0x00001F28
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DesertTexture = ");
			builder.Append(this.DesertTexture);
			builder.Append(", DryFieldTexture = ");
			builder.Append(this.DryFieldTexture);
			builder.Append(", WetFieldTexture = ");
			builder.Append(this.WetFieldTexture);
			builder.Append(", BlendingNoise = ");
			builder.Append(this.BlendingNoise);
			builder.Append(", BlendingNoiseScale = ");
			builder.Append(this.BlendingNoiseScale.ToString());
			builder.Append(", BlendingNoiseMultiplier = ");
			builder.Append(this.BlendingNoiseMultiplier.ToString());
			builder.Append(", BlendingSoftness = ");
			builder.Append(this.BlendingSoftness.ToString());
			builder.Append(", BlendingMargin = ");
			builder.Append(this.BlendingMargin.ToString());
			builder.Append(", AltitudeCeiling = ");
			builder.Append(this.AltitudeCeiling.ToString());
			builder.Append(", AltitudeMultiplier = ");
			builder.Append(this.AltitudeMultiplier);
			builder.Append(", DesertAltitudeMultiplier = ");
			builder.Append(this.DesertAltitudeMultiplier);
			builder.Append(", CutoutMargin = ");
			builder.Append(this.CutoutMargin.ToString());
			return true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003ECB File Offset: 0x000020CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TerrainMaterialMapSpec left, TerrainMaterialMapSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003ED7 File Offset: 0x000020D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TerrainMaterialMapSpec left, TerrainMaterialMapSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003EEC File Offset: 0x000020EC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<DesertTexture>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<DryFieldTexture>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<WetFieldTexture>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<BlendingNoise>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BlendingNoiseScale>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BlendingNoiseMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BlendingSoftness>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BlendingMargin>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<AltitudeCeiling>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture>>.Default.GetHashCode(this.<AltitudeMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture>>.Default.GetHashCode(this.<DesertAltitudeMultiplier>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<CutoutMargin>k__BackingField);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004013 File Offset: 0x00002213
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TerrainMaterialMapSpec);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004021 File Offset: 0x00002221
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000402C File Offset: 0x0000222C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TerrainMaterialMapSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<DesertTexture>k__BackingField, other.<DesertTexture>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<DryFieldTexture>k__BackingField, other.<DryFieldTexture>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<WetFieldTexture>k__BackingField, other.<WetFieldTexture>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<BlendingNoise>k__BackingField, other.<BlendingNoise>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BlendingNoiseScale>k__BackingField, other.<BlendingNoiseScale>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BlendingNoiseMultiplier>k__BackingField, other.<BlendingNoiseMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BlendingSoftness>k__BackingField, other.<BlendingSoftness>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BlendingMargin>k__BackingField, other.<BlendingMargin>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<AltitudeCeiling>k__BackingField, other.<AltitudeCeiling>k__BackingField) && EqualityComparer<AssetRef<Texture>>.Default.Equals(this.<AltitudeMultiplier>k__BackingField, other.<AltitudeMultiplier>k__BackingField) && EqualityComparer<AssetRef<Texture>>.Default.Equals(this.<DesertAltitudeMultiplier>k__BackingField, other.<DesertAltitudeMultiplier>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<CutoutMargin>k__BackingField, other.<CutoutMargin>k__BackingField));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004188 File Offset: 0x00002388
		[CompilerGenerated]
		protected TerrainMaterialMapSpec([Nullable(1)] TerrainMaterialMapSpec original) : base(original)
		{
			this.DesertTexture = original.<DesertTexture>k__BackingField;
			this.DryFieldTexture = original.<DryFieldTexture>k__BackingField;
			this.WetFieldTexture = original.<WetFieldTexture>k__BackingField;
			this.BlendingNoise = original.<BlendingNoise>k__BackingField;
			this.BlendingNoiseScale = original.<BlendingNoiseScale>k__BackingField;
			this.BlendingNoiseMultiplier = original.<BlendingNoiseMultiplier>k__BackingField;
			this.BlendingSoftness = original.<BlendingSoftness>k__BackingField;
			this.BlendingMargin = original.<BlendingMargin>k__BackingField;
			this.AltitudeCeiling = original.<AltitudeCeiling>k__BackingField;
			this.AltitudeMultiplier = original.<AltitudeMultiplier>k__BackingField;
			this.DesertAltitudeMultiplier = original.<DesertAltitudeMultiplier>k__BackingField;
			this.CutoutMargin = original.<CutoutMargin>k__BackingField;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000422C File Offset: 0x0000242C
		public TerrainMaterialMapSpec()
		{
		}
	}
}
