using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200001A RID: 26
	public class ManufactoryTogglableRecipesSpec : ComponentSpec, IEquatable<ManufactoryTogglableRecipesSpec>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003903 File Offset: 0x00001B03
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryTogglableRecipesSpec);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000390F File Offset: 0x00001B0F
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003917 File Offset: 0x00001B17
		[Serialize]
		public string LabelLocKey { get; set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003920 File Offset: 0x00001B20
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryTogglableRecipesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000396C File Offset: 0x00001B6C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("LabelLocKey = ");
			builder.Append(this.LabelLocKey);
			return true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000399D File Offset: 0x00001B9D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryTogglableRecipesSpec left, ManufactoryTogglableRecipesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000039A9 File Offset: 0x00001BA9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryTogglableRecipesSpec left, ManufactoryTogglableRecipesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000039BD File Offset: 0x00001BBD
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LabelLocKey>k__BackingField);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000039DC File Offset: 0x00001BDC
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryTogglableRecipesSpec);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003857 File Offset: 0x00001A57
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000039EA File Offset: 0x00001BEA
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryTogglableRecipesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<LabelLocKey>k__BackingField, other.<LabelLocKey>k__BackingField));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003A1B File Offset: 0x00001C1B
		[CompilerGenerated]
		protected ManufactoryTogglableRecipesSpec([Nullable(1)] ManufactoryTogglableRecipesSpec original) : base(original)
		{
			this.LabelLocKey = original.<LabelLocKey>k__BackingField;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000038A6 File Offset: 0x00001AA6
		public ManufactoryTogglableRecipesSpec()
		{
		}
	}
}
