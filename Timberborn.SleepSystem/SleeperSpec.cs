using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.SleepSystem
{
	// Token: 0x0200000A RID: 10
	public class SleeperSpec : ComponentSpec, IEquatable<SleeperSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022EC File Offset: 0x000004EC
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SleeperSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002300 File Offset: 0x00000500
		[Serialize]
		public ImmutableArray<ContinuousEffectSpec> SleepOutsideEffects { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002309 File Offset: 0x00000509
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002311 File Offset: 0x00000511
		[Serialize]
		public float MaxOffsetInHours { get; set; }

		// Token: 0x0600001B RID: 27 RVA: 0x0000231C File Offset: 0x0000051C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SleeperSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002368 File Offset: 0x00000568
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SleepOutsideEffects = ");
			builder.Append(this.SleepOutsideEffects.ToString());
			builder.Append(", MaxOffsetInHours = ");
			builder.Append(this.MaxOffsetInHours.ToString());
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023D9 File Offset: 0x000005D9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SleeperSpec left, SleeperSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023E5 File Offset: 0x000005E5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SleeperSpec left, SleeperSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F9 File Offset: 0x000005F9
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.GetHashCode(this.<SleepOutsideEffects>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxOffsetInHours>k__BackingField);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000242F File Offset: 0x0000062F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SleeperSpec);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000243D File Offset: 0x0000063D
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002448 File Offset: 0x00000648
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SleeperSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ContinuousEffectSpec>>.Default.Equals(this.<SleepOutsideEffects>k__BackingField, other.<SleepOutsideEffects>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxOffsetInHours>k__BackingField, other.<MaxOffsetInHours>k__BackingField));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000249C File Offset: 0x0000069C
		[CompilerGenerated]
		protected SleeperSpec([Nullable(1)] SleeperSpec original) : base(original)
		{
			this.SleepOutsideEffects = original.<SleepOutsideEffects>k__BackingField;
			this.MaxOffsetInHours = original.<MaxOffsetInHours>k__BackingField;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024BD File Offset: 0x000006BD
		public SleeperSpec()
		{
		}
	}
}
