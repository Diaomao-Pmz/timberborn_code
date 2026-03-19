using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class ChronometerSpec : ComponentSpec, IEquatable<ChronometerSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000026B7 File Offset: 0x000008B7
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ChronometerSpec);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026C4 File Offset: 0x000008C4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChronometerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002719 File Offset: 0x00000919
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChronometerSpec left, ChronometerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002725 File Offset: 0x00000925
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChronometerSpec left, ChronometerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002741 File Offset: 0x00000941
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChronometerSpec);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChronometerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected ChronometerSpec(ChronometerSpec original) : base(original)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002778 File Offset: 0x00000978
		public ChronometerSpec()
		{
		}
	}
}
