using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000016 RID: 22
	public class ProbabilityGroupSpec : IEquatable<ProbabilityGroupSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000030F9 File Offset: 0x000012F9
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ProbabilityGroupSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003105 File Offset: 0x00001305
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000310D File Offset: 0x0000130D
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003116 File Offset: 0x00001316
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000311E File Offset: 0x0000131E
		[Serialize]
		public float Low { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003127 File Offset: 0x00001327
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000312F File Offset: 0x0000132F
		[Serialize]
		public float Medium { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003138 File Offset: 0x00001338
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003140 File Offset: 0x00001340
		[Serialize]
		public float High { get; set; }

		// Token: 0x06000092 RID: 146 RVA: 0x0000314C File Offset: 0x0000134C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProbabilityGroupSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003198 File Offset: 0x00001398
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Low = ");
			builder.Append(this.Low.ToString());
			builder.Append(", Medium = ");
			builder.Append(this.Medium.ToString());
			builder.Append(", High = ");
			builder.Append(this.High.ToString());
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003239 File Offset: 0x00001439
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ProbabilityGroupSpec left, ProbabilityGroupSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003245 File Offset: 0x00001445
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ProbabilityGroupSpec left, ProbabilityGroupSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000325C File Offset: 0x0000145C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Low>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Medium>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<High>k__BackingField);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000032D5 File Offset: 0x000014D5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProbabilityGroupSpec);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000032E4 File Offset: 0x000014E4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ProbabilityGroupSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Low>k__BackingField, other.<Low>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Medium>k__BackingField, other.<Medium>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<High>k__BackingField, other.<High>k__BackingField));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003375 File Offset: 0x00001575
		[CompilerGenerated]
		protected ProbabilityGroupSpec([Nullable(1)] ProbabilityGroupSpec original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Low = original.<Low>k__BackingField;
			this.Medium = original.<Medium>k__BackingField;
			this.High = original.<High>k__BackingField;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000020F8 File Offset: 0x000002F8
		public ProbabilityGroupSpec()
		{
		}
	}
}
