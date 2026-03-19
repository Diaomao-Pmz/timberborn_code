using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000015 RID: 21
	public class TileDrawerFactorySpec : ComponentSpec, IEquatable<TileDrawerFactorySpec>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003628 File Offset: 0x00001828
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TileDrawerFactorySpec);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003634 File Offset: 0x00001834
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000363C File Offset: 0x0000183C
		[Serialize]
		public string MeshResourcePath { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003645 File Offset: 0x00001845
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000364D File Offset: 0x0000184D
		[Serialize]
		public string TilesOnSameLevelMaterialResourcePath { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003656 File Offset: 0x00001856
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000365E File Offset: 0x0000185E
		[Serialize]
		public string TilesBelowMaterialResourcePath { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003667 File Offset: 0x00001867
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000366F File Offset: 0x0000186F
		[Serialize]
		public string TilesAboveMaterialResourcePath { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003678 File Offset: 0x00001878
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003680 File Offset: 0x00001880
		[Serialize]
		public string FootprintTilesMaterialResourcePath { get; set; }

		// Token: 0x06000077 RID: 119 RVA: 0x0000368C File Offset: 0x0000188C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TileDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036D8 File Offset: 0x000018D8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MeshResourcePath = ");
			builder.Append(this.MeshResourcePath);
			builder.Append(", TilesOnSameLevelMaterialResourcePath = ");
			builder.Append(this.TilesOnSameLevelMaterialResourcePath);
			builder.Append(", TilesBelowMaterialResourcePath = ");
			builder.Append(this.TilesBelowMaterialResourcePath);
			builder.Append(", TilesAboveMaterialResourcePath = ");
			builder.Append(this.TilesAboveMaterialResourcePath);
			builder.Append(", FootprintTilesMaterialResourcePath = ");
			builder.Append(this.FootprintTilesMaterialResourcePath);
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003778 File Offset: 0x00001978
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TileDrawerFactorySpec left, TileDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003784 File Offset: 0x00001984
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TileDrawerFactorySpec left, TileDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003798 File Offset: 0x00001998
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MeshResourcePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TilesOnSameLevelMaterialResourcePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TilesBelowMaterialResourcePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TilesAboveMaterialResourcePath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FootprintTilesMaterialResourcePath>k__BackingField);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000381E File Offset: 0x00001A1E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TileDrawerFactorySpec);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000313F File Offset: 0x0000133F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000382C File Offset: 0x00001A2C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TileDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<MeshResourcePath>k__BackingField, other.<MeshResourcePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TilesOnSameLevelMaterialResourcePath>k__BackingField, other.<TilesOnSameLevelMaterialResourcePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TilesBelowMaterialResourcePath>k__BackingField, other.<TilesBelowMaterialResourcePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TilesAboveMaterialResourcePath>k__BackingField, other.<TilesAboveMaterialResourcePath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FootprintTilesMaterialResourcePath>k__BackingField, other.<FootprintTilesMaterialResourcePath>k__BackingField));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000038CC File Offset: 0x00001ACC
		[CompilerGenerated]
		protected TileDrawerFactorySpec([Nullable(1)] TileDrawerFactorySpec original) : base(original)
		{
			this.MeshResourcePath = original.<MeshResourcePath>k__BackingField;
			this.TilesOnSameLevelMaterialResourcePath = original.<TilesOnSameLevelMaterialResourcePath>k__BackingField;
			this.TilesBelowMaterialResourcePath = original.<TilesBelowMaterialResourcePath>k__BackingField;
			this.TilesAboveMaterialResourcePath = original.<TilesAboveMaterialResourcePath>k__BackingField;
			this.FootprintTilesMaterialResourcePath = original.<FootprintTilesMaterialResourcePath>k__BackingField;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000318E File Offset: 0x0000138E
		public TileDrawerFactorySpec()
		{
		}
	}
}
