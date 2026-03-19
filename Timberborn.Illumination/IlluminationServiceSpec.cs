using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Illumination
{
	// Token: 0x02000012 RID: 18
	public class IlluminationServiceSpec : ComponentSpec, IEquatable<IlluminationServiceSpec>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002F32 File Offset: 0x00001132
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(IlluminationServiceSpec);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002F3E File Offset: 0x0000113E
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002F46 File Offset: 0x00001146
		[Serialize]
		public string DefaultColorId { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002F4F File Offset: 0x0000114F
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002F57 File Offset: 0x00001157
		[Serialize]
		public float IconExponent { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002F60 File Offset: 0x00001160
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002F68 File Offset: 0x00001168
		[Serialize]
		public float IconMultiplier { get; set; }

		// Token: 0x06000087 RID: 135 RVA: 0x00002F74 File Offset: 0x00001174
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IlluminationServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002FC0 File Offset: 0x000011C0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DefaultColorId = ");
			builder.Append(this.DefaultColorId);
			builder.Append(", IconExponent = ");
			builder.Append(this.IconExponent.ToString());
			builder.Append(", IconMultiplier = ");
			builder.Append(this.IconMultiplier.ToString());
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000304A File Offset: 0x0000124A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IlluminationServiceSpec left, IlluminationServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003056 File Offset: 0x00001256
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IlluminationServiceSpec left, IlluminationServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000306C File Offset: 0x0000126C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DefaultColorId>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<IconExponent>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<IconMultiplier>k__BackingField);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000030C4 File Offset: 0x000012C4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IlluminationServiceSpec);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002267 File Offset: 0x00000467
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030D4 File Offset: 0x000012D4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IlluminationServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DefaultColorId>k__BackingField, other.<DefaultColorId>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<IconExponent>k__BackingField, other.<IconExponent>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<IconMultiplier>k__BackingField, other.<IconMultiplier>k__BackingField));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003140 File Offset: 0x00001340
		[CompilerGenerated]
		protected IlluminationServiceSpec([Nullable(1)] IlluminationServiceSpec original) : base(original)
		{
			this.DefaultColorId = original.<DefaultColorId>k__BackingField;
			this.IconExponent = original.<IconExponent>k__BackingField;
			this.IconMultiplier = original.<IconMultiplier>k__BackingField;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002290 File Offset: 0x00000490
		public IlluminationServiceSpec()
		{
		}
	}
}
