using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Attractions
{
	// Token: 0x0200000B RID: 11
	public class AttractionFireSpec : ComponentSpec, IEquatable<AttractionFireSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000268B File Offset: 0x0000088B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(AttractionFireSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002697 File Offset: 0x00000897
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000269F File Offset: 0x0000089F
		[Serialize]
		public string WoodstackName { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x000026A8 File Offset: 0x000008A8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AttractionFireSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026F4 File Offset: 0x000008F4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("WoodstackName = ");
			builder.Append(this.WoodstackName);
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002725 File Offset: 0x00000925
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(AttractionFireSpec left, AttractionFireSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002731 File Offset: 0x00000931
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(AttractionFireSpec left, AttractionFireSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002745 File Offset: 0x00000945
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WoodstackName>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002764 File Offset: 0x00000964
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AttractionFireSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002772 File Offset: 0x00000972
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000277B File Offset: 0x0000097B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(AttractionFireSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<WoodstackName>k__BackingField, other.<WoodstackName>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027AC File Offset: 0x000009AC
		[CompilerGenerated]
		protected AttractionFireSpec([Nullable(1)] AttractionFireSpec original) : base(original)
		{
			this.WoodstackName = original.<WoodstackName>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027C1 File Offset: 0x000009C1
		public AttractionFireSpec()
		{
		}
	}
}
