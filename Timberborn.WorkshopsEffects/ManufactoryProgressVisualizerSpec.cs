using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000009 RID: 9
	public class ManufactoryProgressVisualizerSpec : ComponentSpec, IEquatable<ManufactoryProgressVisualizerSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022D0 File Offset: 0x000004D0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryProgressVisualizerSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022DC File Offset: 0x000004DC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		[Serialize]
		public ImmutableArray<ProgressStepSpec> ProgressSteps { get; set; }

		// Token: 0x06000018 RID: 24 RVA: 0x000022F0 File Offset: 0x000004F0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryProgressVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000233C File Offset: 0x0000053C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ProgressSteps = ");
			builder.Append(this.ProgressSteps.ToString());
			return true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002386 File Offset: 0x00000586
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryProgressVisualizerSpec left, ManufactoryProgressVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002392 File Offset: 0x00000592
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryProgressVisualizerSpec left, ManufactoryProgressVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023A6 File Offset: 0x000005A6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<ProgressStepSpec>>.Default.GetHashCode(this.<ProgressSteps>k__BackingField);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023C5 File Offset: 0x000005C5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryProgressVisualizerSpec);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023DC File Offset: 0x000005DC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryProgressVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<ProgressStepSpec>>.Default.Equals(this.<ProgressSteps>k__BackingField, other.<ProgressSteps>k__BackingField));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000240D File Offset: 0x0000060D
		[CompilerGenerated]
		protected ManufactoryProgressVisualizerSpec([Nullable(1)] ManufactoryProgressVisualizerSpec original) : base(original)
		{
			this.ProgressSteps = original.<ProgressSteps>k__BackingField;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002422 File Offset: 0x00000622
		public ManufactoryProgressVisualizerSpec()
		{
		}
	}
}
