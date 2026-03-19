using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000030 RID: 48
	public class IncreaseDesiredWorkersStepSpec : ComponentSpec, IEquatable<IncreaseDesiredWorkersStepSpec>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004E72 File Offset: 0x00003072
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(IncreaseDesiredWorkersStepSpec);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004E7E File Offset: 0x0000307E
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004E86 File Offset: 0x00003086
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x06000153 RID: 339 RVA: 0x00004E90 File Offset: 0x00003090
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IncreaseDesiredWorkersStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004EDC File Offset: 0x000030DC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TemplateName = ");
			builder.Append(this.TemplateName);
			return true;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004F0D File Offset: 0x0000310D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IncreaseDesiredWorkersStepSpec left, IncreaseDesiredWorkersStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004F19 File Offset: 0x00003119
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IncreaseDesiredWorkersStepSpec left, IncreaseDesiredWorkersStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004F2D File Offset: 0x0000312D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004F4C File Offset: 0x0000314C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IncreaseDesiredWorkersStepSpec);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004F5A File Offset: 0x0000315A
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IncreaseDesiredWorkersStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004F8B File Offset: 0x0000318B
		[CompilerGenerated]
		protected IncreaseDesiredWorkersStepSpec([Nullable(1)] IncreaseDesiredWorkersStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000239D File Offset: 0x0000059D
		public IncreaseDesiredWorkersStepSpec()
		{
		}
	}
}
