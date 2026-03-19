using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000016 RID: 22
	public class MarkerDrawerFactorySpec : ComponentSpec, IEquatable<MarkerDrawerFactorySpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000030DD File Offset: 0x000012DD
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MarkerDrawerFactorySpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000030E9 File Offset: 0x000012E9
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000030F1 File Offset: 0x000012F1
		[Serialize]
		public AssetRef<Mesh> TileMesh { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030FA File Offset: 0x000012FA
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003102 File Offset: 0x00001302
		[Serialize]
		public AssetRef<Mesh> SmallBlockMesh { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000310B File Offset: 0x0000130B
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003113 File Offset: 0x00001313
		[Serialize]
		public AssetRef<Mesh> LargeBlockMesh { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000311C File Offset: 0x0000131C
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003124 File Offset: 0x00001324
		[Serialize]
		public AssetRef<Mesh> TerrainBlockMesh { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000312D File Offset: 0x0000132D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003135 File Offset: 0x00001335
		[Serialize]
		public AssetRef<Mesh> TopTerrainTileMesh { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000313E File Offset: 0x0000133E
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003146 File Offset: 0x00001346
		[Serialize]
		public AssetRef<Material> TileMaterial { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000314F File Offset: 0x0000134F
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003157 File Offset: 0x00001357
		[Serialize]
		public AssetRef<Material> TerrainTileMaterial { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003160 File Offset: 0x00001360
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003168 File Offset: 0x00001368
		[Serialize]
		public AssetRef<Material> TopTerrainTileMaterial { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003171 File Offset: 0x00001371
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003179 File Offset: 0x00001379
		[Serialize]
		public AssetRef<Material> PrioritizedTileMaterial { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003182 File Offset: 0x00001382
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000318A File Offset: 0x0000138A
		[Serialize]
		public AssetRef<Mesh> EntranceMesh { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003193 File Offset: 0x00001393
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000319B File Offset: 0x0000139B
		[Serialize]
		public AssetRef<Material> EntranceMarkerMaterial { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000031A4 File Offset: 0x000013A4
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000031AC File Offset: 0x000013AC
		[Serialize]
		public AssetRef<Mesh> MechanicalInputMesh { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000031B5 File Offset: 0x000013B5
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000031BD File Offset: 0x000013BD
		[Serialize]
		public AssetRef<Mesh> MechanicalOutputMesh { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000031C6 File Offset: 0x000013C6
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000031CE File Offset: 0x000013CE
		[Serialize]
		public AssetRef<Material> MechanicalMarkerMaterial { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000031D7 File Offset: 0x000013D7
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000031DF File Offset: 0x000013DF
		[Serialize]
		public AssetRef<Mesh> ArrowMesh { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000031E8 File Offset: 0x000013E8
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000031F0 File Offset: 0x000013F0
		[Serialize]
		public AssetRef<Material> ArrowMaterial { get; set; }

		// Token: 0x06000097 RID: 151 RVA: 0x000031FC File Offset: 0x000013FC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MarkerDrawerFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003248 File Offset: 0x00001448
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TileMesh = ");
			builder.Append(this.TileMesh);
			builder.Append(", SmallBlockMesh = ");
			builder.Append(this.SmallBlockMesh);
			builder.Append(", LargeBlockMesh = ");
			builder.Append(this.LargeBlockMesh);
			builder.Append(", TerrainBlockMesh = ");
			builder.Append(this.TerrainBlockMesh);
			builder.Append(", TopTerrainTileMesh = ");
			builder.Append(this.TopTerrainTileMesh);
			builder.Append(", TileMaterial = ");
			builder.Append(this.TileMaterial);
			builder.Append(", TerrainTileMaterial = ");
			builder.Append(this.TerrainTileMaterial);
			builder.Append(", TopTerrainTileMaterial = ");
			builder.Append(this.TopTerrainTileMaterial);
			builder.Append(", PrioritizedTileMaterial = ");
			builder.Append(this.PrioritizedTileMaterial);
			builder.Append(", EntranceMesh = ");
			builder.Append(this.EntranceMesh);
			builder.Append(", EntranceMarkerMaterial = ");
			builder.Append(this.EntranceMarkerMaterial);
			builder.Append(", MechanicalInputMesh = ");
			builder.Append(this.MechanicalInputMesh);
			builder.Append(", MechanicalOutputMesh = ");
			builder.Append(this.MechanicalOutputMesh);
			builder.Append(", MechanicalMarkerMaterial = ");
			builder.Append(this.MechanicalMarkerMaterial);
			builder.Append(", ArrowMesh = ");
			builder.Append(this.ArrowMesh);
			builder.Append(", ArrowMaterial = ");
			builder.Append(this.ArrowMaterial);
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033FB File Offset: 0x000015FB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MarkerDrawerFactorySpec left, MarkerDrawerFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003407 File Offset: 0x00001607
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MarkerDrawerFactorySpec left, MarkerDrawerFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000341C File Offset: 0x0000161C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((((((((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<TileMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<SmallBlockMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<LargeBlockMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<TerrainBlockMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<TopTerrainTileMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<TileMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<TerrainTileMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<TopTerrainTileMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<PrioritizedTileMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<EntranceMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<EntranceMarkerMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<MechanicalInputMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<MechanicalOutputMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<MechanicalMarkerMaterial>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Mesh>>.Default.GetHashCode(this.<ArrowMesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Material>>.Default.GetHashCode(this.<ArrowMaterial>k__BackingField);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000359F File Offset: 0x0000179F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MarkerDrawerFactorySpec);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035B0 File Offset: 0x000017B0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MarkerDrawerFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<TileMesh>k__BackingField, other.<TileMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<SmallBlockMesh>k__BackingField, other.<SmallBlockMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<LargeBlockMesh>k__BackingField, other.<LargeBlockMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<TerrainBlockMesh>k__BackingField, other.<TerrainBlockMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<TopTerrainTileMesh>k__BackingField, other.<TopTerrainTileMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<TileMaterial>k__BackingField, other.<TileMaterial>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<TerrainTileMaterial>k__BackingField, other.<TerrainTileMaterial>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<TopTerrainTileMaterial>k__BackingField, other.<TopTerrainTileMaterial>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<PrioritizedTileMaterial>k__BackingField, other.<PrioritizedTileMaterial>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<EntranceMesh>k__BackingField, other.<EntranceMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<EntranceMarkerMaterial>k__BackingField, other.<EntranceMarkerMaterial>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<MechanicalInputMesh>k__BackingField, other.<MechanicalInputMesh>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<MechanicalOutputMesh>k__BackingField, other.<MechanicalOutputMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<MechanicalMarkerMaterial>k__BackingField, other.<MechanicalMarkerMaterial>k__BackingField) && EqualityComparer<AssetRef<Mesh>>.Default.Equals(this.<ArrowMesh>k__BackingField, other.<ArrowMesh>k__BackingField) && EqualityComparer<AssetRef<Material>>.Default.Equals(this.<ArrowMaterial>k__BackingField, other.<ArrowMaterial>k__BackingField));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003778 File Offset: 0x00001978
		[CompilerGenerated]
		protected MarkerDrawerFactorySpec([Nullable(1)] MarkerDrawerFactorySpec original) : base(original)
		{
			this.TileMesh = original.<TileMesh>k__BackingField;
			this.SmallBlockMesh = original.<SmallBlockMesh>k__BackingField;
			this.LargeBlockMesh = original.<LargeBlockMesh>k__BackingField;
			this.TerrainBlockMesh = original.<TerrainBlockMesh>k__BackingField;
			this.TopTerrainTileMesh = original.<TopTerrainTileMesh>k__BackingField;
			this.TileMaterial = original.<TileMaterial>k__BackingField;
			this.TerrainTileMaterial = original.<TerrainTileMaterial>k__BackingField;
			this.TopTerrainTileMaterial = original.<TopTerrainTileMaterial>k__BackingField;
			this.PrioritizedTileMaterial = original.<PrioritizedTileMaterial>k__BackingField;
			this.EntranceMesh = original.<EntranceMesh>k__BackingField;
			this.EntranceMarkerMaterial = original.<EntranceMarkerMaterial>k__BackingField;
			this.MechanicalInputMesh = original.<MechanicalInputMesh>k__BackingField;
			this.MechanicalOutputMesh = original.<MechanicalOutputMesh>k__BackingField;
			this.MechanicalMarkerMaterial = original.<MechanicalMarkerMaterial>k__BackingField;
			this.ArrowMesh = original.<ArrowMesh>k__BackingField;
			this.ArrowMaterial = original.<ArrowMaterial>k__BackingField;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000026F5 File Offset: 0x000008F5
		public MarkerDrawerFactorySpec()
		{
		}
	}
}
