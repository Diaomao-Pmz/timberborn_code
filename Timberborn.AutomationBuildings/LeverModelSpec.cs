using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000023 RID: 35
	public class LeverModelSpec : ComponentSpec, IEquatable<LeverModelSpec>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000502F File Offset: 0x0000322F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(LeverModelSpec);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000503B File Offset: 0x0000323B
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005043 File Offset: 0x00003243
		[Serialize]
		public string OnModelName { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000504C File Offset: 0x0000324C
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005054 File Offset: 0x00003254
		[Serialize]
		public string OffModelName { get; set; }

		// Token: 0x0600017D RID: 381 RVA: 0x00005060 File Offset: 0x00003260
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LeverModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000050AC File Offset: 0x000032AC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OnModelName = ");
			builder.Append(this.OnModelName);
			builder.Append(", OffModelName = ");
			builder.Append(this.OffModelName);
			return true;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005101 File Offset: 0x00003301
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LeverModelSpec left, LeverModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000510D File Offset: 0x0000330D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LeverModelSpec left, LeverModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005121 File Offset: 0x00003321
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OnModelName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OffModelName>k__BackingField);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005157 File Offset: 0x00003357
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LeverModelSpec);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005168 File Offset: 0x00003368
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LeverModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<OnModelName>k__BackingField, other.<OnModelName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OffModelName>k__BackingField, other.<OffModelName>k__BackingField));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000051BC File Offset: 0x000033BC
		[CompilerGenerated]
		protected LeverModelSpec([Nullable(1)] LeverModelSpec original) : base(original)
		{
			this.OnModelName = original.<OnModelName>k__BackingField;
			this.OffModelName = original.<OffModelName>k__BackingField;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00002778 File Offset: 0x00000978
		public LeverModelSpec()
		{
		}
	}
}
