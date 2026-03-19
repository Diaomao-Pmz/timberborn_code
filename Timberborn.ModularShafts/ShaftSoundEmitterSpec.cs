using System;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class ShaftSoundEmitterSpec : ComponentSpec, IEquatable<ShaftSoundEmitterSpec>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004A75 File Offset: 0x00002C75
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ShaftSoundEmitterSpec);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004A84 File Offset: 0x00002C84
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ShaftSoundEmitterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000036F8 File Offset: 0x000018F8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			return base.PrintMembers(builder);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004AD0 File Offset: 0x00002CD0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ShaftSoundEmitterSpec left, ShaftSoundEmitterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004ADC File Offset: 0x00002CDC
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ShaftSoundEmitterSpec left, ShaftSoundEmitterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003721 File Offset: 0x00001921
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004AF0 File Offset: 0x00002CF0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ShaftSoundEmitterSpec);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002742 File Offset: 0x00000942
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003737 File Offset: 0x00001937
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ShaftSoundEmitterSpec other)
		{
			return this == other || base.Equals(other);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000374E File Offset: 0x0000194E
		[CompilerGenerated]
		protected ShaftSoundEmitterSpec(ShaftSoundEmitterSpec original) : base(original)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002791 File Offset: 0x00000991
		public ShaftSoundEmitterSpec()
		{
		}
	}
}
