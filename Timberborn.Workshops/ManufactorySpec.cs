using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000018 RID: 24
	public class ManufactorySpec : ComponentSpec, IEquatable<ManufactorySpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003754 File Offset: 0x00001954
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ManufactorySpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003760 File Offset: 0x00001960
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003768 File Offset: 0x00001968
		[Serialize]
		public ImmutableArray<string> ProductionRecipeIds { get; set; }

		// Token: 0x06000088 RID: 136 RVA: 0x00003774 File Offset: 0x00001974
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000037C0 File Offset: 0x000019C0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ProductionRecipeIds = ");
			builder.Append(this.ProductionRecipeIds.ToString());
			return true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000380A File Offset: 0x00001A0A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactorySpec left, ManufactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003816 File Offset: 0x00001A16
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactorySpec left, ManufactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000382A File Offset: 0x00001A2A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<ProductionRecipeIds>k__BackingField);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003849 File Offset: 0x00001A49
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactorySpec);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003860 File Offset: 0x00001A60
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<ProductionRecipeIds>k__BackingField, other.<ProductionRecipeIds>k__BackingField));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003891 File Offset: 0x00001A91
		[CompilerGenerated]
		protected ManufactorySpec([Nullable(1)] ManufactorySpec original) : base(original)
		{
			this.ProductionRecipeIds = original.<ProductionRecipeIds>k__BackingField;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000038A6 File Offset: 0x00001AA6
		public ManufactorySpec()
		{
		}
	}
}
