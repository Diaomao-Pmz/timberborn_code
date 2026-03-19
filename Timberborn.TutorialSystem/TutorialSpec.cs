using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.LocalizationSerialization;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000014 RID: 20
	public class TutorialSpec : ComponentSpec, IEquatable<TutorialSpec>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002977 File Offset: 0x00000B77
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TutorialSpec);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002983 File Offset: 0x00000B83
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000298B File Offset: 0x00000B8B
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000299C File Offset: 0x00000B9C
		[Serialize("NameLocKey")]
		public LocalizedText DisplayName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000029A5 File Offset: 0x00000BA5
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000029AD File Offset: 0x00000BAD
		[Serialize]
		public string NameLocKey { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000029B6 File Offset: 0x00000BB6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000029BE File Offset: 0x00000BBE
		[Serialize]
		public ImmutableArray<string> RequiredTutorialIds { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029C7 File Offset: 0x00000BC7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000029CF File Offset: 0x00000BCF
		[BackwardCompatible(2026, 1, 15, Compatibility.Save)]
		[Serialize]
		public string SkipIfTutorialFinished { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000029E0 File Offset: 0x00000BE0
		[Serialize]
		public int SortOrder { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029E9 File Offset: 0x00000BE9
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000029F1 File Offset: 0x00000BF1
		[Serialize]
		public ImmutableArray<string> Stages { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x000029FC File Offset: 0x00000BFC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TutorialSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A48 File Offset: 0x00000C48
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", NameLocKey = ");
			builder.Append(this.NameLocKey);
			builder.Append(", RequiredTutorialIds = ");
			builder.Append(this.RequiredTutorialIds.ToString());
			builder.Append(", SkipIfTutorialFinished = ");
			builder.Append(this.SkipIfTutorialFinished);
			builder.Append(", SortOrder = ");
			builder.Append(this.SortOrder.ToString());
			builder.Append(", Stages = ");
			builder.Append(this.Stages.ToString());
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B44 File Offset: 0x00000D44
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TutorialSpec left, TutorialSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B50 File Offset: 0x00000D50
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TutorialSpec left, TutorialSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B64 File Offset: 0x00000D64
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NameLocKey>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<RequiredTutorialIds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SkipIfTutorialFinished>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<SortOrder>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Stages>k__BackingField);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C18 File Offset: 0x00000E18
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TutorialSpec);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002197 File Offset: 0x00000397
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C28 File Offset: 0x00000E28
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TutorialSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<LocalizedText>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NameLocKey>k__BackingField, other.<NameLocKey>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<RequiredTutorialIds>k__BackingField, other.<RequiredTutorialIds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SkipIfTutorialFinished>k__BackingField, other.<SkipIfTutorialFinished>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<SortOrder>k__BackingField, other.<SortOrder>k__BackingField) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Stages>k__BackingField, other.<Stages>k__BackingField));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D00 File Offset: 0x00000F00
		[CompilerGenerated]
		protected TutorialSpec([Nullable(1)] TutorialSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.NameLocKey = original.<NameLocKey>k__BackingField;
			this.RequiredTutorialIds = original.<RequiredTutorialIds>k__BackingField;
			this.SkipIfTutorialFinished = original.<SkipIfTutorialFinished>k__BackingField;
			this.SortOrder = original.<SortOrder>k__BackingField;
			this.Stages = original.<Stages>k__BackingField;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000021C0 File Offset: 0x000003C0
		public TutorialSpec()
		{
		}
	}
}
