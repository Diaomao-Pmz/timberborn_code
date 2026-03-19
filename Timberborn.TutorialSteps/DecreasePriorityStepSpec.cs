using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000029 RID: 41
	public class DecreasePriorityStepSpec : ComponentSpec, IEquatable<DecreasePriorityStepSpec>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000480A File Offset: 0x00002A0A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DecreasePriorityStepSpec);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004816 File Offset: 0x00002A16
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000481E File Offset: 0x00002A1E
		[Serialize]
		public string TemplateName { get; set; }

		// Token: 0x06000120 RID: 288 RVA: 0x00004828 File Offset: 0x00002A28
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DecreasePriorityStepSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004874 File Offset: 0x00002A74
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

		// Token: 0x06000122 RID: 290 RVA: 0x000048A5 File Offset: 0x00002AA5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DecreasePriorityStepSpec left, DecreasePriorityStepSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000048B1 File Offset: 0x00002AB1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DecreasePriorityStepSpec left, DecreasePriorityStepSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000048C5 File Offset: 0x00002AC5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TemplateName>k__BackingField);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000048E4 File Offset: 0x00002AE4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DecreasePriorityStepSpec);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000234E File Offset: 0x0000054E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000048F2 File Offset: 0x00002AF2
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DecreasePriorityStepSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TemplateName>k__BackingField, other.<TemplateName>k__BackingField));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004923 File Offset: 0x00002B23
		[CompilerGenerated]
		protected DecreasePriorityStepSpec([Nullable(1)] DecreasePriorityStepSpec original) : base(original)
		{
			this.TemplateName = original.<TemplateName>k__BackingField;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000239D File Offset: 0x0000059D
		public DecreasePriorityStepSpec()
		{
		}
	}
}
