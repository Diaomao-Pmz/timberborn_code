using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class EnterableAnimationControllerSpec : ComponentSpec, IEquatable<EnterableAnimationControllerSpec>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000264F File Offset: 0x0000084F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EnterableAnimationControllerSpec);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000265B File Offset: 0x0000085B
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002663 File Offset: 0x00000863
		[Serialize]
		public bool ResetAnimationUponExit { get; set; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000266C File Offset: 0x0000086C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EnterableAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026B8 File Offset: 0x000008B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ResetAnimationUponExit = ");
			builder.Append(this.ResetAnimationUponExit.ToString());
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002702 File Offset: 0x00000902
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EnterableAnimationControllerSpec left, EnterableAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000270E File Offset: 0x0000090E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EnterableAnimationControllerSpec left, EnterableAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002722 File Offset: 0x00000922
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ResetAnimationUponExit>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002741 File Offset: 0x00000941
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EnterableAnimationControllerSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EnterableAnimationControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<ResetAnimationUponExit>k__BackingField, other.<ResetAnimationUponExit>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002789 File Offset: 0x00000989
		[CompilerGenerated]
		protected EnterableAnimationControllerSpec(EnterableAnimationControllerSpec original) : base(original)
		{
			this.ResetAnimationUponExit = original.<ResetAnimationUponExit>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000279E File Offset: 0x0000099E
		public EnterableAnimationControllerSpec()
		{
		}
	}
}
