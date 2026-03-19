using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	public class SpeakerSpec : ComponentSpec, IEquatable<SpeakerSpec>
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00008799 File Offset: 0x00006999
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SpeakerSpec);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000087A8 File Offset: 0x000069A8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SpeakerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00002710 File Offset: 0x00000910
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000087F4 File Offset: 0x000069F4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SpeakerSpec left, SpeakerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008800 File Offset: 0x00006A00
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SpeakerSpec left, SpeakerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00002739 File Offset: 0x00000939
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008814 File Offset: 0x00006A14
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SpeakerSpec);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SpeakerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000276F File Offset: 0x0000096F
		[CompilerGenerated]
		protected SpeakerSpec(SpeakerSpec original) : base(original)
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00002778 File Offset: 0x00000978
		public SpeakerSpec()
		{
		}
	}
}
