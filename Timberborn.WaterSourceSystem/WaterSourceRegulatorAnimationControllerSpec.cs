using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000021 RID: 33
	public class WaterSourceRegulatorAnimationControllerSpec : ComponentSpec, IEquatable<WaterSourceRegulatorAnimationControllerSpec>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003B63 File Offset: 0x00001D63
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceRegulatorAnimationControllerSpec);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003B6F File Offset: 0x00001D6F
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00003B77 File Offset: 0x00001D77
		[Serialize]
		public ImmutableArray<RegulatorTransformSpec> RegulatorTransforms { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00003B80 File Offset: 0x00001D80
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceRegulatorAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003BCC File Offset: 0x00001DCC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RegulatorTransforms = ");
			builder.Append(this.RegulatorTransforms.ToString());
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003C16 File Offset: 0x00001E16
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceRegulatorAnimationControllerSpec left, WaterSourceRegulatorAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003C22 File Offset: 0x00001E22
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceRegulatorAnimationControllerSpec left, WaterSourceRegulatorAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003C36 File Offset: 0x00001E36
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<RegulatorTransformSpec>>.Default.GetHashCode(this.<RegulatorTransforms>k__BackingField);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003C55 File Offset: 0x00001E55
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceRegulatorAnimationControllerSpec);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003C63 File Offset: 0x00001E63
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceRegulatorAnimationControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<RegulatorTransformSpec>>.Default.Equals(this.<RegulatorTransforms>k__BackingField, other.<RegulatorTransforms>k__BackingField));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003C94 File Offset: 0x00001E94
		[CompilerGenerated]
		protected WaterSourceRegulatorAnimationControllerSpec([Nullable(1)] WaterSourceRegulatorAnimationControllerSpec original) : base(original)
		{
			this.RegulatorTransforms = original.<RegulatorTransforms>k__BackingField;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceRegulatorAnimationControllerSpec()
		{
		}
	}
}
