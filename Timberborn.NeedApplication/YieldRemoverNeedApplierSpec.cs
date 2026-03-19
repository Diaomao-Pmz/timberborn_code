using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class YieldRemoverNeedApplierSpec : ComponentSpec, IEquatable<YieldRemoverNeedApplierSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003A03 File Offset: 0x00001C03
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(YieldRemoverNeedApplierSpec);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A10 File Offset: 0x00001C10
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YieldRemoverNeedApplierSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000029D4 File Offset: 0x00000BD4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003A5C File Offset: 0x00001C5C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(YieldRemoverNeedApplierSpec left, YieldRemoverNeedApplierSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A68 File Offset: 0x00001C68
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(YieldRemoverNeedApplierSpec left, YieldRemoverNeedApplierSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000029FD File Offset: 0x00000BFD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A7C File Offset: 0x00001C7C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as YieldRemoverNeedApplierSpec);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002519 File Offset: 0x00000719
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002A13 File Offset: 0x00000C13
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(YieldRemoverNeedApplierSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002A2A File Offset: 0x00000C2A
		[CompilerGenerated]
		protected YieldRemoverNeedApplierSpec(YieldRemoverNeedApplierSpec original) : base(original)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002599 File Offset: 0x00000799
		public YieldRemoverNeedApplierSpec()
		{
		}
	}
}
