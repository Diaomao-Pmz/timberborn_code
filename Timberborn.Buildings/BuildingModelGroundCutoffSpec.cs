using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Buildings
{
	// Token: 0x0200000F RID: 15
	public class BuildingModelGroundCutoffSpec : ComponentSpec, IEquatable<BuildingModelGroundCutoffSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B15 File Offset: 0x00000D15
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingModelGroundCutoffSpec);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002B21 File Offset: 0x00000D21
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002B29 File Offset: 0x00000D29
		[Serialize]
		public ImmutableArray<string> Targets { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002B32 File Offset: 0x00000D32
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002B3A File Offset: 0x00000D3A
		[Serialize]
		public float Offset { get; set; }

		// Token: 0x0600006A RID: 106 RVA: 0x00002B44 File Offset: 0x00000D44
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingModelGroundCutoffSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B90 File Offset: 0x00000D90
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Targets = ");
			builder.Append(this.Targets.ToString());
			builder.Append(", Offset = ");
			builder.Append(this.Offset.ToString());
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C01 File Offset: 0x00000E01
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingModelGroundCutoffSpec left, BuildingModelGroundCutoffSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C0D File Offset: 0x00000E0D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingModelGroundCutoffSpec left, BuildingModelGroundCutoffSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C21 File Offset: 0x00000E21
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Targets>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Offset>k__BackingField);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C57 File Offset: 0x00000E57
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingModelGroundCutoffSpec);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002C68 File Offset: 0x00000E68
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingModelGroundCutoffSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Targets>k__BackingField, other.<Targets>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Offset>k__BackingField, other.<Offset>k__BackingField));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002CBC File Offset: 0x00000EBC
		[CompilerGenerated]
		protected BuildingModelGroundCutoffSpec([Nullable(1)] BuildingModelGroundCutoffSpec original) : base(original)
		{
			this.Targets = original.<Targets>k__BackingField;
			this.Offset = original.<Offset>k__BackingField;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000246D File Offset: 0x0000066D
		public BuildingModelGroundCutoffSpec()
		{
		}
	}
}
