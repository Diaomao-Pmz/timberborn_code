using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MergeableObjects
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	public class MergeableObjectModelEnforcerSpec : ComponentSpec, IEquatable<MergeableObjectModelEnforcerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023FF File Offset: 0x000005FF
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MergeableObjectModelEnforcerSpec);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000240C File Offset: 0x0000060C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MergeableObjectModelEnforcerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002458 File Offset: 0x00000658
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002461 File Offset: 0x00000661
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MergeableObjectModelEnforcerSpec left, MergeableObjectModelEnforcerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000246D File Offset: 0x0000066D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MergeableObjectModelEnforcerSpec left, MergeableObjectModelEnforcerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002481 File Offset: 0x00000681
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002489 File Offset: 0x00000689
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MergeableObjectModelEnforcerSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002497 File Offset: 0x00000697
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024A0 File Offset: 0x000006A0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MergeableObjectModelEnforcerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B7 File Offset: 0x000006B7
		[CompilerGenerated]
		protected MergeableObjectModelEnforcerSpec(MergeableObjectModelEnforcerSpec original) : base(original)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C0 File Offset: 0x000006C0
		public MergeableObjectModelEnforcerSpec()
		{
		}
	}
}
