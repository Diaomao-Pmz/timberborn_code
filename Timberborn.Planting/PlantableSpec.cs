using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Planting
{
	// Token: 0x0200000F RID: 15
	public class PlantableSpec : ComponentSpec, IEquatable<PlantableSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002587 File Offset: 0x00000787
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlantableSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002593 File Offset: 0x00000793
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000259B File Offset: 0x0000079B
		[Serialize]
		public string ResourceGroup { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000025AC File Offset: 0x000007AC
		[Serialize]
		public float PlantTimeInHours { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025B5 File Offset: 0x000007B5
		public string TemplateName
		{
			get
			{
				return base.GetSpec<TemplateSpec>().TemplateName;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025C4 File Offset: 0x000007C4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantableSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002610 File Offset: 0x00000810
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ResourceGroup = ");
			builder.Append(this.ResourceGroup);
			builder.Append(", PlantTimeInHours = ");
			builder.Append(this.PlantTimeInHours.ToString());
			builder.Append(", TemplateName = ");
			builder.Append(this.TemplateName);
			return true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000268C File Offset: 0x0000088C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantableSpec left, PlantableSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002698 File Offset: 0x00000898
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantableSpec left, PlantableSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026AC File Offset: 0x000008AC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResourceGroup>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<PlantTimeInHours>k__BackingField);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026E2 File Offset: 0x000008E2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantableSpec);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026F0 File Offset: 0x000008F0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026FC File Offset: 0x000008FC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantableSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ResourceGroup>k__BackingField, other.<ResourceGroup>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<PlantTimeInHours>k__BackingField, other.<PlantTimeInHours>k__BackingField));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002750 File Offset: 0x00000950
		[CompilerGenerated]
		protected PlantableSpec([Nullable(1)] PlantableSpec original) : base(original)
		{
			this.ResourceGroup = original.<ResourceGroup>k__BackingField;
			this.PlantTimeInHours = original.<PlantTimeInHours>k__BackingField;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002771 File Offset: 0x00000971
		public PlantableSpec()
		{
		}
	}
}
