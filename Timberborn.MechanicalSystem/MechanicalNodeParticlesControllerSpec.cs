using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001B RID: 27
	public class MechanicalNodeParticlesControllerSpec : ComponentSpec, IEquatable<MechanicalNodeParticlesControllerSpec>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003A4F File Offset: 0x00001C4F
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeParticlesControllerSpec);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003A5B File Offset: 0x00001C5B
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003A63 File Offset: 0x00001C63
		[Serialize]
		public float MinEfficiency { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003A6C File Offset: 0x00001C6C
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003A74 File Offset: 0x00001C74
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A80 File Offset: 0x00001C80
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeParticlesControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003ACC File Offset: 0x00001CCC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinEfficiency = ");
			builder.Append(this.MinEfficiency.ToString());
			builder.Append(", AttachmentIds = ");
			builder.Append(this.AttachmentIds.ToString());
			return true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003B3D File Offset: 0x00001D3D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeParticlesControllerSpec left, MechanicalNodeParticlesControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003B49 File Offset: 0x00001D49
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeParticlesControllerSpec left, MechanicalNodeParticlesControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003B5D File Offset: 0x00001D5D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinEfficiency>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003B93 File Offset: 0x00001D93
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeParticlesControllerSpec);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003BA4 File Offset: 0x00001DA4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeParticlesControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinEfficiency>k__BackingField, other.<MinEfficiency>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003BF8 File Offset: 0x00001DF8
		[CompilerGenerated]
		protected MechanicalNodeParticlesControllerSpec([Nullable(1)] MechanicalNodeParticlesControllerSpec original) : base(original)
		{
			this.MinEfficiency = original.<MinEfficiency>k__BackingField;
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000257C File Offset: 0x0000077C
		public MechanicalNodeParticlesControllerSpec()
		{
		}
	}
}
