using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WindSystem
{
	// Token: 0x0200000C RID: 12
	public class WindParticleControllerSpec : ComponentSpec, IEquatable<WindParticleControllerSpec>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002402 File Offset: 0x00000602
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WindParticleControllerSpec);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000240E File Offset: 0x0000060E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002416 File Offset: 0x00000616
		[Serialize]
		public ImmutableArray<string> AttachmentIds { get; set; }

		// Token: 0x06000030 RID: 48 RVA: 0x00002420 File Offset: 0x00000620
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindParticleControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000246C File Offset: 0x0000066C
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
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024B6 File Offset: 0x000006B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindParticleControllerSpec left, WindParticleControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024C2 File Offset: 0x000006C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindParticleControllerSpec left, WindParticleControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024D6 File Offset: 0x000006D6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<AttachmentIds>k__BackingField);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024F5 File Offset: 0x000006F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindParticleControllerSpec);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002263 File Offset: 0x00000463
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002503 File Offset: 0x00000703
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindParticleControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<AttachmentIds>k__BackingField, other.<AttachmentIds>k__BackingField));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002534 File Offset: 0x00000734
		[CompilerGenerated]
		protected WindParticleControllerSpec([Nullable(1)] WindParticleControllerSpec original) : base(original)
		{
			this.AttachmentIds = original.<AttachmentIds>k__BackingField;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000228C File Offset: 0x0000048C
		public WindParticleControllerSpec()
		{
		}
	}
}
