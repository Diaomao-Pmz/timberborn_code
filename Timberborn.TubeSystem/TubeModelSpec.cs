using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x0200000F RID: 15
	public class TubeModelSpec : ComponentSpec, IEquatable<TubeModelSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002954 File Offset: 0x00000B54
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TubeModelSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002968 File Offset: 0x00000B68
		[Serialize]
		public string ModelPrefix { get; set; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002974 File Offset: 0x00000B74
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TubeModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029C0 File Offset: 0x00000BC0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ModelPrefix = ");
			builder.Append(this.ModelPrefix);
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029F1 File Offset: 0x00000BF1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TubeModelSpec left, TubeModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029FD File Offset: 0x00000BFD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TubeModelSpec left, TubeModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A11 File Offset: 0x00000C11
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ModelPrefix>k__BackingField);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A30 File Offset: 0x00000C30
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TubeModelSpec);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A3E File Offset: 0x00000C3E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A47 File Offset: 0x00000C47
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TubeModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ModelPrefix>k__BackingField, other.<ModelPrefix>k__BackingField));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A78 File Offset: 0x00000C78
		[CompilerGenerated]
		protected TubeModelSpec([Nullable(1)] TubeModelSpec original) : base(original)
		{
			this.ModelPrefix = original.<ModelPrefix>k__BackingField;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A8D File Offset: 0x00000C8D
		public TubeModelSpec()
		{
		}
	}
}
