using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000028 RID: 40
	public class WaterOutputParticleSpec : ComponentSpec, IEquatable<WaterOutputParticleSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000056A8 File Offset: 0x000038A8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterOutputParticleSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000056B4 File Offset: 0x000038B4
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000056BC File Offset: 0x000038BC
		[Serialize]
		public string AttachmentId { get; set; }

		// Token: 0x060000FA RID: 250 RVA: 0x000056C8 File Offset: 0x000038C8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterOutputParticleSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005714 File Offset: 0x00003914
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AttachmentId = ");
			builder.Append(this.AttachmentId);
			return true;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005745 File Offset: 0x00003945
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterOutputParticleSpec left, WaterOutputParticleSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005751 File Offset: 0x00003951
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterOutputParticleSpec left, WaterOutputParticleSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005765 File Offset: 0x00003965
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AttachmentId>k__BackingField);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005784 File Offset: 0x00003984
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterOutputParticleSpec);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000055C7 File Offset: 0x000037C7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005792 File Offset: 0x00003992
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterOutputParticleSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AttachmentId>k__BackingField, other.<AttachmentId>k__BackingField));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000057C3 File Offset: 0x000039C3
		[CompilerGenerated]
		protected WaterOutputParticleSpec([Nullable(1)] WaterOutputParticleSpec original) : base(original)
		{
			this.AttachmentId = original.<AttachmentId>k__BackingField;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005616 File Offset: 0x00003816
		public WaterOutputParticleSpec()
		{
		}
	}
}
