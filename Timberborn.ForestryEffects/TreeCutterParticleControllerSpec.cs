using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.ForestryEffects
{
	// Token: 0x02000009 RID: 9
	public class TreeCutterParticleControllerSpec : ComponentSpec, IEquatable<TreeCutterParticleControllerSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002299 File Offset: 0x00000499
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TreeCutterParticleControllerSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022A5 File Offset: 0x000004A5
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022AD File Offset: 0x000004AD
		[Serialize]
		public string ParticlesAttachmentId { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x000022B8 File Offset: 0x000004B8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TreeCutterParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002304 File Offset: 0x00000504
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ParticlesAttachmentId = ");
			builder.Append(this.ParticlesAttachmentId);
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002335 File Offset: 0x00000535
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TreeCutterParticleControllerSpec left, TreeCutterParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002341 File Offset: 0x00000541
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TreeCutterParticleControllerSpec left, TreeCutterParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002355 File Offset: 0x00000555
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParticlesAttachmentId>k__BackingField);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002374 File Offset: 0x00000574
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreeCutterParticleControllerSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002382 File Offset: 0x00000582
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000238B File Offset: 0x0000058B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TreeCutterParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ParticlesAttachmentId>k__BackingField, other.<ParticlesAttachmentId>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023BC File Offset: 0x000005BC
		[CompilerGenerated]
		protected TreeCutterParticleControllerSpec([Nullable(1)] TreeCutterParticleControllerSpec original) : base(original)
		{
			this.ParticlesAttachmentId = original.<ParticlesAttachmentId>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023D1 File Offset: 0x000005D1
		public TreeCutterParticleControllerSpec()
		{
		}
	}
}
