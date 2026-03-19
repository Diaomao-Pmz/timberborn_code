using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000011 RID: 17
	public class PatrollingSlotSpec : IEquatable<PatrollingSlotSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BF5 File Offset: 0x00000DF5
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PatrollingSlotSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002C01 File Offset: 0x00000E01
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002C09 File Offset: 0x00000E09
		[Serialize]
		public float BaseMovementSpeed { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002C12 File Offset: 0x00000E12
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002C1A File Offset: 0x00000E1A
		[Serialize]
		public float MaxRandomDeviationOfMovementSpeed { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002C23 File Offset: 0x00000E23
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Serialize]
		public string SlotKeyword { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002C3C File Offset: 0x00000E3C
		[Serialize]
		public string Animation { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C45 File Offset: 0x00000E45
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002C4D File Offset: 0x00000E4D
		[Serialize]
		public bool WaterSlot { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002C58 File Offset: 0x00000E58
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PatrollingSlotSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("BaseMovementSpeed = ");
			builder.Append(this.BaseMovementSpeed.ToString());
			builder.Append(", MaxRandomDeviationOfMovementSpeed = ");
			builder.Append(this.MaxRandomDeviationOfMovementSpeed.ToString());
			builder.Append(", SlotKeyword = ");
			builder.Append(this.SlotKeyword);
			builder.Append(", Animation = ");
			builder.Append(this.Animation);
			builder.Append(", WaterSlot = ");
			builder.Append(this.WaterSlot.ToString());
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002D5E File Offset: 0x00000F5E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PatrollingSlotSpec left, PatrollingSlotSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D6A File Offset: 0x00000F6A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PatrollingSlotSpec left, PatrollingSlotSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D80 File Offset: 0x00000F80
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BaseMovementSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxRandomDeviationOfMovementSpeed>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SlotKeyword>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Animation>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<WaterSlot>k__BackingField);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E10 File Offset: 0x00001010
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PatrollingSlotSpec);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E20 File Offset: 0x00001020
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PatrollingSlotSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float>.Default.Equals(this.<BaseMovementSpeed>k__BackingField, other.<BaseMovementSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxRandomDeviationOfMovementSpeed>k__BackingField, other.<MaxRandomDeviationOfMovementSpeed>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SlotKeyword>k__BackingField, other.<SlotKeyword>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Animation>k__BackingField, other.<Animation>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<WaterSlot>k__BackingField, other.<WaterSlot>k__BackingField));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002ED0 File Offset: 0x000010D0
		[CompilerGenerated]
		protected PatrollingSlotSpec([Nullable(1)] PatrollingSlotSpec original)
		{
			this.BaseMovementSpeed = original.<BaseMovementSpeed>k__BackingField;
			this.MaxRandomDeviationOfMovementSpeed = original.<MaxRandomDeviationOfMovementSpeed>k__BackingField;
			this.SlotKeyword = original.<SlotKeyword>k__BackingField;
			this.Animation = original.<Animation>k__BackingField;
			this.WaterSlot = original.<WaterSlot>k__BackingField;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000020F8 File Offset: 0x000002F8
		public PatrollingSlotSpec()
		{
		}
	}
}
