using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	public class BlockSpec : IEquatable<BlockSpec>
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005753 File Offset: 0x00003953
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BlockSpec);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000575F File Offset: 0x0000395F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005767 File Offset: 0x00003967
		[Serialize]
		public MatterBelow MatterBelow { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005770 File Offset: 0x00003970
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00005778 File Offset: 0x00003978
		[Serialize]
		public BlockOccupations Occupations { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005781 File Offset: 0x00003981
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00005789 File Offset: 0x00003989
		[Serialize]
		public BlockStackable Stackable { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005792 File Offset: 0x00003992
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000579A File Offset: 0x0000399A
		[Serialize]
		public bool OccupyAllBelow { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000057A3 File Offset: 0x000039A3
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000057AB File Offset: 0x000039AB
		[Serialize]
		public bool Underground { get; set; }

		// Token: 0x0600016B RID: 363 RVA: 0x000057B4 File Offset: 0x000039B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005800 File Offset: 0x00003A00
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("MatterBelow = ");
			builder.Append(this.MatterBelow.ToString());
			builder.Append(", Occupations = ");
			builder.Append(this.Occupations.ToString());
			builder.Append(", Stackable = ");
			builder.Append(this.Stackable.ToString());
			builder.Append(", OccupyAllBelow = ");
			builder.Append(this.OccupyAllBelow.ToString());
			builder.Append(", Underground = ");
			builder.Append(this.Underground.ToString());
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000058D6 File Offset: 0x00003AD6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockSpec left, BlockSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000058E2 File Offset: 0x00003AE2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockSpec left, BlockSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000058F8 File Offset: 0x00003AF8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<MatterBelow>.Default.GetHashCode(this.<MatterBelow>k__BackingField)) * -1521134295 + EqualityComparer<BlockOccupations>.Default.GetHashCode(this.<Occupations>k__BackingField)) * -1521134295 + EqualityComparer<BlockStackable>.Default.GetHashCode(this.<Stackable>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OccupyAllBelow>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Underground>k__BackingField);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005988 File Offset: 0x00003B88
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockSpec);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005998 File Offset: 0x00003B98
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<MatterBelow>.Default.Equals(this.<MatterBelow>k__BackingField, other.<MatterBelow>k__BackingField) && EqualityComparer<BlockOccupations>.Default.Equals(this.<Occupations>k__BackingField, other.<Occupations>k__BackingField) && EqualityComparer<BlockStackable>.Default.Equals(this.<Stackable>k__BackingField, other.<Stackable>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OccupyAllBelow>k__BackingField, other.<OccupyAllBelow>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Underground>k__BackingField, other.<Underground>k__BackingField));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A48 File Offset: 0x00003C48
		[CompilerGenerated]
		protected BlockSpec(BlockSpec original)
		{
			this.MatterBelow = original.<MatterBelow>k__BackingField;
			this.Occupations = original.<Occupations>k__BackingField;
			this.Stackable = original.<Stackable>k__BackingField;
			this.OccupyAllBelow = original.<OccupyAllBelow>k__BackingField;
			this.Underground = original.<Underground>k__BackingField;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000020F8 File Offset: 0x000002F8
		public BlockSpec()
		{
		}
	}
}
