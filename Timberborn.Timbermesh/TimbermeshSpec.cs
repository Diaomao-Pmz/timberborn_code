using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;

namespace Timberborn.Timbermesh
{
	// Token: 0x02000011 RID: 17
	public class TimbermeshSpec : ComponentSpec, IEquatable<TimbermeshSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000296A File Offset: 0x00000B6A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TimbermeshSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002976 File Offset: 0x00000B76
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000297E File Offset: 0x00000B7E
		[Serialize]
		public AssetRef<BinaryData> Model { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002988 File Offset: 0x00000B88
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TimbermeshSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029D4 File Offset: 0x00000BD4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Model = ");
			builder.Append(this.Model);
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A05 File Offset: 0x00000C05
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TimbermeshSpec left, TimbermeshSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A11 File Offset: 0x00000C11
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TimbermeshSpec left, TimbermeshSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A25 File Offset: 0x00000C25
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<BinaryData>>.Default.GetHashCode(this.<Model>k__BackingField);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A44 File Offset: 0x00000C44
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimbermeshSpec);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A52 File Offset: 0x00000C52
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A5B File Offset: 0x00000C5B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TimbermeshSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<BinaryData>>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A8C File Offset: 0x00000C8C
		[CompilerGenerated]
		protected TimbermeshSpec([Nullable(1)] TimbermeshSpec original) : base(original)
		{
			this.Model = original.<Model>k__BackingField;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA1 File Offset: 0x00000CA1
		public TimbermeshSpec()
		{
		}
	}
}
