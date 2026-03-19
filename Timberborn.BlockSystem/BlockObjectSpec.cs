using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000019 RID: 25
	public class BlockObjectSpec : ComponentSpec, IEquatable<BlockObjectSpec>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000039AE File Offset: 0x00001BAE
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectSpec);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000039BA File Offset: 0x00001BBA
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000039C2 File Offset: 0x00001BC2
		[Serialize]
		public Vector3Int Size { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000039CB File Offset: 0x00001BCB
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000039D3 File Offset: 0x00001BD3
		[Serialize]
		public ImmutableArray<BlockSpec> Blocks { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000039DC File Offset: 0x00001BDC
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000039E4 File Offset: 0x00001BE4
		[Serialize]
		public EntranceBlockSpec Entrance { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000039F5 File Offset: 0x00001BF5
		[Serialize]
		public int BaseZ { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000039FE File Offset: 0x00001BFE
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003A06 File Offset: 0x00001C06
		[Serialize]
		public bool Overridable { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003A0F File Offset: 0x00001C0F
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003A17 File Offset: 0x00001C17
		[Serialize]
		public bool Flippable { get; set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A20 File Offset: 0x00001C20
		public Blocks GetBlocks()
		{
			return Timberborn.BlockSystem.Blocks.From(this);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003A28 File Offset: 0x00001C28
		public IEnumerable<Block> GetBlocks(Placement placement)
		{
			int num;
			for (int x = 0; x < this.Size.x; x = num)
			{
				for (int y = 0; y < this.Size.y; y = num)
				{
					for (int z = 0; z < this.Size.z; z = num)
					{
						Vector3Int coordinates;
						coordinates..ctor(x, y, z);
						BlockSpec blockSpec = this.BlockSpecFromCoordinates(coordinates);
						Vector3Int coordinates2 = placement.Orientation.Transform(placement.FlipMode.Transform(coordinates, this.Size.x)) + placement.Coordinates;
						yield return Block.From(coordinates2, blockSpec);
						num = z + 1;
					}
					num = y + 1;
				}
				num = x + 1;
			}
			yield break;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A40 File Offset: 0x00001C40
		public BlockSpec BlockSpecFromCoordinates(Vector3Int coordinates)
		{
			int index = (coordinates.z * this.Size.y + coordinates.y) * this.Size.x + coordinates.x;
			return this.Blocks[index];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A94 File Offset: 0x00001C94
		public Vector3 CalculateCenterOffset(Orientation orientation)
		{
			return orientation.Transform(new Vector3((float)this.Size.x, (float)this.Size.y, (float)this.Size.z) / 2f);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003B30 File Offset: 0x00001D30
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Size = ");
			builder.Append(this.Size.ToString());
			builder.Append(", Blocks = ");
			builder.Append(this.Blocks.ToString());
			builder.Append(", Entrance = ");
			builder.Append(this.Entrance);
			builder.Append(", BaseZ = ");
			builder.Append(this.BaseZ.ToString());
			builder.Append(", Overridable = ");
			builder.Append(this.Overridable.ToString());
			builder.Append(", Flippable = ");
			builder.Append(this.Flippable.ToString());
			return true;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C2F File Offset: 0x00001E2F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectSpec left, BlockObjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C3B File Offset: 0x00001E3B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectSpec left, BlockObjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003C50 File Offset: 0x00001E50
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Size>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BlockSpec>>.Default.GetHashCode(this.<Blocks>k__BackingField)) * -1521134295 + EqualityComparer<EntranceBlockSpec>.Default.GetHashCode(this.<Entrance>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BaseZ>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Overridable>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Flippable>k__BackingField);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003CED File Offset: 0x00001EED
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectSpec);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003CFB File Offset: 0x00001EFB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D04 File Offset: 0x00001F04
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField) && EqualityComparer<ImmutableArray<BlockSpec>>.Default.Equals(this.<Blocks>k__BackingField, other.<Blocks>k__BackingField) && EqualityComparer<EntranceBlockSpec>.Default.Equals(this.<Entrance>k__BackingField, other.<Entrance>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<BaseZ>k__BackingField, other.<BaseZ>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Overridable>k__BackingField, other.<Overridable>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Flippable>k__BackingField, other.<Flippable>k__BackingField));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003DC0 File Offset: 0x00001FC0
		[CompilerGenerated]
		protected BlockObjectSpec([Nullable(1)] BlockObjectSpec original) : base(original)
		{
			this.Size = original.<Size>k__BackingField;
			this.Blocks = original.<Blocks>k__BackingField;
			this.Entrance = original.<Entrance>k__BackingField;
			this.BaseZ = original.<BaseZ>k__BackingField;
			this.Overridable = original.<Overridable>k__BackingField;
			this.Flippable = original.<Flippable>k__BackingField;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E1C File Offset: 0x0000201C
		public BlockObjectSpec()
		{
		}
	}
}
