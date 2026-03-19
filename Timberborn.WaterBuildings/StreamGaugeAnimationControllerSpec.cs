using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000025 RID: 37
	public class StreamGaugeAnimationControllerSpec : ComponentSpec, IEquatable<StreamGaugeAnimationControllerSpec>
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00004E3B File Offset: 0x0000303B
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(StreamGaugeAnimationControllerSpec);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004E47 File Offset: 0x00003047
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00004E4F File Offset: 0x0000304F
		[Serialize]
		public string MarkerName { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004E58 File Offset: 0x00003058
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00004E60 File Offset: 0x00003060
		[Serialize]
		public float MaxHeight { get; set; }

		// Token: 0x0600017E RID: 382 RVA: 0x00004E6C File Offset: 0x0000306C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StreamGaugeAnimationControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004EB8 File Offset: 0x000030B8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MarkerName = ");
			builder.Append(this.MarkerName);
			builder.Append(", MaxHeight = ");
			builder.Append(this.MaxHeight.ToString());
			return true;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004F1B File Offset: 0x0000311B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(StreamGaugeAnimationControllerSpec left, StreamGaugeAnimationControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004F27 File Offset: 0x00003127
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(StreamGaugeAnimationControllerSpec left, StreamGaugeAnimationControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004F3B File Offset: 0x0000313B
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MarkerName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxHeight>k__BackingField);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004F71 File Offset: 0x00003171
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StreamGaugeAnimationControllerSpec);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00002B9B File Offset: 0x00000D9B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004F80 File Offset: 0x00003180
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(StreamGaugeAnimationControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<MarkerName>k__BackingField, other.<MarkerName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxHeight>k__BackingField, other.<MaxHeight>k__BackingField));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004FD4 File Offset: 0x000031D4
		[CompilerGenerated]
		protected StreamGaugeAnimationControllerSpec([Nullable(1)] StreamGaugeAnimationControllerSpec original) : base(original)
		{
			this.MarkerName = original.<MarkerName>k__BackingField;
			this.MaxHeight = original.<MaxHeight>k__BackingField;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00002CBC File Offset: 0x00000EBC
		public StreamGaugeAnimationControllerSpec()
		{
		}
	}
}
