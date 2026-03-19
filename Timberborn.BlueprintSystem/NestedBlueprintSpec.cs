using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000020 RID: 32
	public class NestedBlueprintSpec : ComponentSpec, IEquatable<NestedBlueprintSpec>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003D37 File Offset: 0x00001F37
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NestedBlueprintSpec);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003D43 File Offset: 0x00001F43
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003D4B File Offset: 0x00001F4B
		[Serialize]
		public string BlueprintPath { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003D54 File Offset: 0x00001F54
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003D5C File Offset: 0x00001F5C
		[Serialize]
		public string Modification { get; set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D68 File Offset: 0x00001F68
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NestedBlueprintSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BlueprintPath = ");
			builder.Append(this.BlueprintPath);
			builder.Append(", Modification = ");
			builder.Append(this.Modification);
			return true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003E09 File Offset: 0x00002009
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NestedBlueprintSpec left, NestedBlueprintSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003E15 File Offset: 0x00002015
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NestedBlueprintSpec left, NestedBlueprintSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003E29 File Offset: 0x00002029
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BlueprintPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Modification>k__BackingField);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003E5F File Offset: 0x0000205F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NestedBlueprintSpec);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E70 File Offset: 0x00002070
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NestedBlueprintSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<BlueprintPath>k__BackingField, other.<BlueprintPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Modification>k__BackingField, other.<Modification>k__BackingField));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003EC4 File Offset: 0x000020C4
		[CompilerGenerated]
		protected NestedBlueprintSpec([Nullable(1)] NestedBlueprintSpec original) : base(original)
		{
			this.BlueprintPath = original.<BlueprintPath>k__BackingField;
			this.Modification = original.<Modification>k__BackingField;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D2F File Offset: 0x00001F2F
		public NestedBlueprintSpec()
		{
		}

		// Token: 0x0400004C RID: 76
		public static readonly string BlueprintPathKey = "BlueprintPath";

		// Token: 0x0400004D RID: 77
		public static readonly string ModificationKey = "Modification";
	}
}
