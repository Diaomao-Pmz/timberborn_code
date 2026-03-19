using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	public class EntranceBlockSpec : IEquatable<EntranceBlockSpec>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000061CF File Offset: 0x000043CF
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EntranceBlockSpec);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000061DB File Offset: 0x000043DB
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000061E3 File Offset: 0x000043E3
		[Serialize]
		public bool HasEntrance { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000061EC File Offset: 0x000043EC
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000061F4 File Offset: 0x000043F4
		[Serialize]
		public Vector3Int Coordinates { get; set; }

		// Token: 0x060001AA RID: 426 RVA: 0x00006200 File Offset: 0x00004400
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntranceBlockSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000624C File Offset: 0x0000444C
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("HasEntrance = ");
			builder.Append(this.HasEntrance.ToString());
			builder.Append(", Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			return true;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000062AD File Offset: 0x000044AD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EntranceBlockSpec left, EntranceBlockSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000062B9 File Offset: 0x000044B9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EntranceBlockSpec left, EntranceBlockSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000062CD File Offset: 0x000044CD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasEntrance>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Coordinates>k__BackingField);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000630D File Offset: 0x0000450D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntranceBlockSpec);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000631C File Offset: 0x0000451C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EntranceBlockSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<HasEntrance>k__BackingField, other.<HasEntrance>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000637D File Offset: 0x0000457D
		[CompilerGenerated]
		protected EntranceBlockSpec(EntranceBlockSpec original)
		{
			this.HasEntrance = original.<HasEntrance>k__BackingField;
			this.Coordinates = original.<Coordinates>k__BackingField;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000020F8 File Offset: 0x000002F8
		public EntranceBlockSpec()
		{
		}
	}
}
