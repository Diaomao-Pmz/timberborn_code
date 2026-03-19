using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class DryObjectSpec : ComponentSpec, IEquatable<DryObjectSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002751 File Offset: 0x00000951
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DryObjectSpec);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002760 File Offset: 0x00000960
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DryObjectSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027AC File Offset: 0x000009AC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027B5 File Offset: 0x000009B5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DryObjectSpec left, DryObjectSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027C1 File Offset: 0x000009C1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DryObjectSpec left, DryObjectSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027D5 File Offset: 0x000009D5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027DD File Offset: 0x000009DD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DryObjectSpec);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026C9 File Offset: 0x000008C9
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027EB File Offset: 0x000009EB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DryObjectSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002802 File Offset: 0x00000A02
		[CompilerGenerated]
		protected DryObjectSpec(DryObjectSpec original) : base(original)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002749 File Offset: 0x00000949
		public DryObjectSpec()
		{
		}
	}
}
