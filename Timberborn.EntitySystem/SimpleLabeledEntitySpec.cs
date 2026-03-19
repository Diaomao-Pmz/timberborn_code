using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200001B RID: 27
	public class SimpleLabeledEntitySpec : ComponentSpec, IEquatable<SimpleLabeledEntitySpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C36 File Offset: 0x00000E36
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SimpleLabeledEntitySpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C42 File Offset: 0x00000E42
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002C4A File Offset: 0x00000E4A
		[Serialize]
		public string EntityNameLocKey { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002C54 File Offset: 0x00000E54
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimpleLabeledEntitySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CA0 File Offset: 0x00000EA0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("EntityNameLocKey = ");
			builder.Append(this.EntityNameLocKey);
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CD1 File Offset: 0x00000ED1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SimpleLabeledEntitySpec left, SimpleLabeledEntitySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002CDD File Offset: 0x00000EDD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SimpleLabeledEntitySpec left, SimpleLabeledEntitySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002CF1 File Offset: 0x00000EF1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<EntityNameLocKey>k__BackingField);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D10 File Offset: 0x00000F10
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SimpleLabeledEntitySpec);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002ABD File Offset: 0x00000CBD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D1E File Offset: 0x00000F1E
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SimpleLabeledEntitySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<EntityNameLocKey>k__BackingField, other.<EntityNameLocKey>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D4F File Offset: 0x00000F4F
		[CompilerGenerated]
		protected SimpleLabeledEntitySpec([Nullable(1)] SimpleLabeledEntitySpec original) : base(original)
		{
			this.EntityNameLocKey = original.<EntityNameLocKey>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002B85 File Offset: 0x00000D85
		public SimpleLabeledEntitySpec()
		{
		}
	}
}
