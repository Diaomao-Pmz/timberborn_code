using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000011 RID: 17
	public class TubePlatformSpec : ComponentSpec, IEquatable<TubePlatformSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B5C File Offset: 0x00000D5C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TubePlatformSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002B68 File Offset: 0x00000D68
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002B70 File Offset: 0x00000D70
		[Serialize]
		public string PlatformModelName { get; set; }

		// Token: 0x06000051 RID: 81 RVA: 0x00002B7C File Offset: 0x00000D7C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TubePlatformSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BC8 File Offset: 0x00000DC8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PlatformModelName = ");
			builder.Append(this.PlatformModelName);
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BF9 File Offset: 0x00000DF9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TubePlatformSpec left, TubePlatformSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C05 File Offset: 0x00000E05
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TubePlatformSpec left, TubePlatformSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C19 File Offset: 0x00000E19
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PlatformModelName>k__BackingField);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C38 File Offset: 0x00000E38
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TubePlatformSpec);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A3E File Offset: 0x00000C3E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C46 File Offset: 0x00000E46
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TubePlatformSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<PlatformModelName>k__BackingField, other.<PlatformModelName>k__BackingField));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C77 File Offset: 0x00000E77
		[CompilerGenerated]
		protected TubePlatformSpec([Nullable(1)] TubePlatformSpec original) : base(original)
		{
			this.PlatformModelName = original.<PlatformModelName>k__BackingField;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A8D File Offset: 0x00000C8D
		public TubePlatformSpec()
		{
		}
	}
}
