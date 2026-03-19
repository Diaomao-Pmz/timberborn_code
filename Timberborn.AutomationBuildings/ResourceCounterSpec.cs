using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	public class ResourceCounterSpec : ComponentSpec, IEquatable<ResourceCounterSpec>
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00007543 File Offset: 0x00005743
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ResourceCounterSpec);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007550 File Offset: 0x00005750
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ResourceCounterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000759C File Offset: 0x0000579C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ResourceCounterSpec left, ResourceCounterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000075A8 File Offset: 0x000057A8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ResourceCounterSpec left, ResourceCounterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000075BC File Offset: 0x000057BC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResourceCounterSpec);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ResourceCounterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected ResourceCounterSpec(ResourceCounterSpec original) : base(original)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00002778 File Offset: 0x00000978
		public ResourceCounterSpec()
		{
		}
	}
}
