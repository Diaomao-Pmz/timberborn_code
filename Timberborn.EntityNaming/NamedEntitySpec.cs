using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class NamedEntitySpec : ComponentSpec, IEquatable<NamedEntitySpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000270A File Offset: 0x0000090A
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NamedEntitySpec);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000271E File Offset: 0x0000091E
		[Serialize]
		public bool IsEditable { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002728 File Offset: 0x00000928
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NamedEntitySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002774 File Offset: 0x00000974
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("IsEditable = ");
			builder.Append(this.IsEditable.ToString());
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027BE File Offset: 0x000009BE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NamedEntitySpec left, NamedEntitySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027CA File Offset: 0x000009CA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NamedEntitySpec left, NamedEntitySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027DE File Offset: 0x000009DE
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEditable>k__BackingField);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027FD File Offset: 0x000009FD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NamedEntitySpec);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000280B File Offset: 0x00000A0B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002814 File Offset: 0x00000A14
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NamedEntitySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<IsEditable>k__BackingField, other.<IsEditable>k__BackingField));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002845 File Offset: 0x00000A45
		[CompilerGenerated]
		protected NamedEntitySpec(NamedEntitySpec original) : base(original)
		{
			this.IsEditable = original.<IsEditable>k__BackingField;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000285A File Offset: 0x00000A5A
		public NamedEntitySpec()
		{
		}
	}
}
