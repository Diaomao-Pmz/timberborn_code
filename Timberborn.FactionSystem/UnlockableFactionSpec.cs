using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.FactionSystem
{
	// Token: 0x02000010 RID: 16
	public class UnlockableFactionSpec : ComponentSpec, IEquatable<UnlockableFactionSpec>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000314B File Offset: 0x0000134B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnlockableFactionSpec);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003157 File Offset: 0x00001357
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000315F File Offset: 0x0000135F
		[Serialize]
		public string PrerequisiteFaction { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003170 File Offset: 0x00001370
		[Serialize]
		public int AverageWellbeingToUnlock { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x0000317C File Offset: 0x0000137C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnlockableFactionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031C8 File Offset: 0x000013C8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PrerequisiteFaction = ");
			builder.Append(this.PrerequisiteFaction);
			builder.Append(", AverageWellbeingToUnlock = ");
			builder.Append(this.AverageWellbeingToUnlock.ToString());
			return true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000322B File Offset: 0x0000142B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnlockableFactionSpec left, UnlockableFactionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003237 File Offset: 0x00001437
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnlockableFactionSpec left, UnlockableFactionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000324B File Offset: 0x0000144B
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PrerequisiteFaction>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AverageWellbeingToUnlock>k__BackingField);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003281 File Offset: 0x00001481
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnlockableFactionSpec);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000028E5 File Offset: 0x00000AE5
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003290 File Offset: 0x00001490
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnlockableFactionSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<PrerequisiteFaction>k__BackingField, other.<PrerequisiteFaction>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<AverageWellbeingToUnlock>k__BackingField, other.<AverageWellbeingToUnlock>k__BackingField));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032E4 File Offset: 0x000014E4
		[CompilerGenerated]
		protected UnlockableFactionSpec([Nullable(1)] UnlockableFactionSpec original) : base(original)
		{
			this.PrerequisiteFaction = original.<PrerequisiteFaction>k__BackingField;
			this.AverageWellbeingToUnlock = original.<AverageWellbeingToUnlock>k__BackingField;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D60 File Offset: 0x00000F60
		public UnlockableFactionSpec()
		{
		}
	}
}
