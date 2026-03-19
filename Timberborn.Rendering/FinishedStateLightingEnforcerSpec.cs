using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Rendering
{
	// Token: 0x0200000F RID: 15
	public class FinishedStateLightingEnforcerSpec : ComponentSpec, IEquatable<FinishedStateLightingEnforcerSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A90 File Offset: 0x00000C90
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FinishedStateLightingEnforcerSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002AA4 File Offset: 0x00000CA4
		[Serialize]
		public ImmutableArray<string> ChildrenNames { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FinishedStateLightingEnforcerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AFC File Offset: 0x00000CFC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ChildrenNames = ");
			builder.Append(this.ChildrenNames.ToString());
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B46 File Offset: 0x00000D46
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FinishedStateLightingEnforcerSpec left, FinishedStateLightingEnforcerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B52 File Offset: 0x00000D52
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FinishedStateLightingEnforcerSpec left, FinishedStateLightingEnforcerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B66 File Offset: 0x00000D66
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<ChildrenNames>k__BackingField);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B85 File Offset: 0x00000D85
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FinishedStateLightingEnforcerSpec);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002675 File Offset: 0x00000875
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B93 File Offset: 0x00000D93
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FinishedStateLightingEnforcerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<ChildrenNames>k__BackingField, other.<ChildrenNames>k__BackingField));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BC4 File Offset: 0x00000DC4
		[CompilerGenerated]
		protected FinishedStateLightingEnforcerSpec([Nullable(1)] FinishedStateLightingEnforcerSpec original) : base(original)
		{
			this.ChildrenNames = original.<ChildrenNames>k__BackingField;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026F5 File Offset: 0x000008F5
		public FinishedStateLightingEnforcerSpec()
		{
		}
	}
}
