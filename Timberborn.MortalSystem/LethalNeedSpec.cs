using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.LocalizationSerialization;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000C RID: 12
	public class LethalNeedSpec : ComponentSpec, IEquatable<LethalNeedSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002699 File Offset: 0x00000899
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(LethalNeedSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026A5 File Offset: 0x000008A5
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026AD File Offset: 0x000008AD
		[Serialize("DeathWarningLocKey")]
		public LocalizedText DeathWarning { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026B6 File Offset: 0x000008B6
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026BE File Offset: 0x000008BE
		[Serialize("DeathMessageLocKey")]
		public LocalizedText DeathMessage { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026C7 File Offset: 0x000008C7
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026CF File Offset: 0x000008CF
		[Serialize]
		private string DeathWarningLocKey { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026D8 File Offset: 0x000008D8
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026E0 File Offset: 0x000008E0
		[Serialize]
		private string DeathMessageLocKey { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x000026EC File Offset: 0x000008EC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LethalNeedSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002738 File Offset: 0x00000938
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DeathWarning = ");
			builder.Append(this.DeathWarning);
			builder.Append(", DeathMessage = ");
			builder.Append(this.DeathMessage);
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000278D File Offset: 0x0000098D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LethalNeedSpec left, LethalNeedSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002799 File Offset: 0x00000999
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LethalNeedSpec left, LethalNeedSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027B0 File Offset: 0x000009B0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DeathWarning>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DeathMessage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeathWarningLocKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeathMessageLocKey>k__BackingField);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000281F File Offset: 0x00000A1F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LethalNeedSpec);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002642 File Offset: 0x00000842
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002830 File Offset: 0x00000A30
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LethalNeedSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<LocalizedText>.Default.Equals(this.<DeathWarning>k__BackingField, other.<DeathWarning>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DeathMessage>k__BackingField, other.<DeathMessage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeathWarningLocKey>k__BackingField, other.<DeathWarningLocKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeathMessageLocKey>k__BackingField, other.<DeathMessageLocKey>k__BackingField));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028B4 File Offset: 0x00000AB4
		[CompilerGenerated]
		protected LethalNeedSpec([Nullable(1)] LethalNeedSpec original) : base(original)
		{
			this.DeathWarning = original.<DeathWarning>k__BackingField;
			this.DeathMessage = original.<DeathMessage>k__BackingField;
			this.DeathWarningLocKey = original.<DeathWarningLocKey>k__BackingField;
			this.DeathMessageLocKey = original.<DeathMessageLocKey>k__BackingField;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002691 File Offset: 0x00000891
		public LethalNeedSpec()
		{
		}
	}
}
