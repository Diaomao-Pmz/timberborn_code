using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000008 RID: 8
	public class BlockObjectAccessesSpec : ComponentSpec, IEquatable<BlockObjectAccessesSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021D7 File Offset: 0x000003D7
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BlockObjectAccessesSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E3 File Offset: 0x000003E3
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021EB File Offset: 0x000003EB
		[Serialize]
		public ImmutableArray<Vector3Int> BlockingCoordinates { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021FC File Offset: 0x000003FC
		[Serialize]
		public ImmutableArray<Vector3Int> AllowedCoordinates { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002208 File Offset: 0x00000408
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BlockObjectAccessesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002254 File Offset: 0x00000454
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BlockingCoordinates = ");
			builder.Append(this.BlockingCoordinates.ToString());
			builder.Append(", AllowedCoordinates = ");
			builder.Append(this.AllowedCoordinates.ToString());
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C5 File Offset: 0x000004C5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BlockObjectAccessesSpec left, BlockObjectAccessesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022D1 File Offset: 0x000004D1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BlockObjectAccessesSpec left, BlockObjectAccessesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E5 File Offset: 0x000004E5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<BlockingCoordinates>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<AllowedCoordinates>k__BackingField);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000231B File Offset: 0x0000051B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockObjectAccessesSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002329 File Offset: 0x00000529
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002334 File Offset: 0x00000534
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BlockObjectAccessesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<BlockingCoordinates>k__BackingField, other.<BlockingCoordinates>k__BackingField) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<AllowedCoordinates>k__BackingField, other.<AllowedCoordinates>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002388 File Offset: 0x00000588
		[CompilerGenerated]
		protected BlockObjectAccessesSpec([Nullable(1)] BlockObjectAccessesSpec original) : base(original)
		{
			this.BlockingCoordinates = original.<BlockingCoordinates>k__BackingField;
			this.AllowedCoordinates = original.<AllowedCoordinates>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023A9 File Offset: 0x000005A9
		public BlockObjectAccessesSpec()
		{
		}
	}
}
