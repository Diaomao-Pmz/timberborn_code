using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Attractions
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class GoodConsumingAttractionSpec : ComponentSpec, IEquatable<GoodConsumingAttractionSpec>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F11 File Offset: 0x00001111
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GoodConsumingAttractionSpec);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F20 File Offset: 0x00001120
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GoodConsumingAttractionSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F6C File Offset: 0x0000116C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F75 File Offset: 0x00001175
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GoodConsumingAttractionSpec left, GoodConsumingAttractionSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F81 File Offset: 0x00001181
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GoodConsumingAttractionSpec left, GoodConsumingAttractionSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F95 File Offset: 0x00001195
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F9D File Offset: 0x0000119D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GoodConsumingAttractionSpec);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002772 File Offset: 0x00000972
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FAB File Offset: 0x000011AB
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GoodConsumingAttractionSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FC2 File Offset: 0x000011C2
		[CompilerGenerated]
		protected GoodConsumingAttractionSpec(GoodConsumingAttractionSpec original) : base(original)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000027C1 File Offset: 0x000009C1
		public GoodConsumingAttractionSpec()
		{
		}
	}
}
