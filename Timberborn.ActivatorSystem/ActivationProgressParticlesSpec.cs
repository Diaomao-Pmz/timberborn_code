using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ActivatorSystem
{
	// Token: 0x02000008 RID: 8
	public class ActivationProgressParticlesSpec : ComponentSpec, IEquatable<ActivationProgressParticlesSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002242 File Offset: 0x00000442
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ActivationProgressParticlesSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000224E File Offset: 0x0000044E
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002256 File Offset: 0x00000456
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000225F File Offset: 0x0000045F
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002267 File Offset: 0x00000467
		[Serialize]
		public int MinEmission { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002270 File Offset: 0x00000470
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002278 File Offset: 0x00000478
		[Serialize]
		public int MaxEmission { get; set; }

		// Token: 0x06000015 RID: 21 RVA: 0x00002284 File Offset: 0x00000484
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ActivationProgressParticlesSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022D0 File Offset: 0x000004D0
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			builder.Append(", MinEmission = ");
			builder.Append(this.MinEmission.ToString());
			builder.Append(", MaxEmission = ");
			builder.Append(this.MaxEmission.ToString());
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002368 File Offset: 0x00000568
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ActivationProgressParticlesSpec left, ActivationProgressParticlesSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002374 File Offset: 0x00000574
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ActivationProgressParticlesSpec left, ActivationProgressParticlesSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002388 File Offset: 0x00000588
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinEmission>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxEmission>k__BackingField);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023E0 File Offset: 0x000005E0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ActivationProgressParticlesSpec);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023EE File Offset: 0x000005EE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F8 File Offset: 0x000005F8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ActivationProgressParticlesSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MinEmission>k__BackingField, other.<MinEmission>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxEmission>k__BackingField, other.<MaxEmission>k__BackingField));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002464 File Offset: 0x00000664
		[CompilerGenerated]
		protected ActivationProgressParticlesSpec([Nullable(1)] ActivationProgressParticlesSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
			this.MinEmission = original.<MinEmission>k__BackingField;
			this.MaxEmission = original.<MaxEmission>k__BackingField;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002491 File Offset: 0x00000691
		public ActivationProgressParticlesSpec()
		{
		}
	}
}
