using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000C RID: 12
	public class ModularShaftCoverSpec : ComponentSpec, IEquatable<ModularShaftCoverSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000265B File Offset: 0x0000085B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ModularShaftCoverSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002667 File Offset: 0x00000867
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000266F File Offset: 0x0000086F
		[Serialize]
		public string CoverModelName { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002678 File Offset: 0x00000878
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModularShaftCoverSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026C4 File Offset: 0x000008C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CoverModelName = ");
			builder.Append(this.CoverModelName);
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026F5 File Offset: 0x000008F5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModularShaftCoverSpec left, ModularShaftCoverSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002701 File Offset: 0x00000901
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModularShaftCoverSpec left, ModularShaftCoverSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002715 File Offset: 0x00000915
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CoverModelName>k__BackingField);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002734 File Offset: 0x00000934
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModularShaftCoverSpec);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002742 File Offset: 0x00000942
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000274B File Offset: 0x0000094B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModularShaftCoverSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<CoverModelName>k__BackingField, other.<CoverModelName>k__BackingField));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000277C File Offset: 0x0000097C
		[CompilerGenerated]
		protected ModularShaftCoverSpec([Nullable(1)] ModularShaftCoverSpec original) : base(original)
		{
			this.CoverModelName = original.<CoverModelName>k__BackingField;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002791 File Offset: 0x00000991
		public ModularShaftCoverSpec()
		{
		}
	}
}
