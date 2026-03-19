using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.IlluminationUI
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public class NightTimeLightControllerSpec : ComponentSpec, IEquatable<NightTimeLightControllerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002689 File Offset: 0x00000889
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NightTimeLightControllerSpec);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002698 File Offset: 0x00000898
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NightTimeLightControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026E4 File Offset: 0x000008E4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026ED File Offset: 0x000008ED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NightTimeLightControllerSpec left, NightTimeLightControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026F9 File Offset: 0x000008F9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NightTimeLightControllerSpec left, NightTimeLightControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000270D File Offset: 0x0000090D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002715 File Offset: 0x00000915
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NightTimeLightControllerSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002723 File Offset: 0x00000923
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000272C File Offset: 0x0000092C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NightTimeLightControllerSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002743 File Offset: 0x00000943
		[CompilerGenerated]
		protected NightTimeLightControllerSpec(NightTimeLightControllerSpec original) : base(original)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000274C File Offset: 0x0000094C
		public NightTimeLightControllerSpec()
		{
		}
	}
}
