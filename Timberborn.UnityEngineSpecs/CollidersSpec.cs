using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000C RID: 12
	public class CollidersSpec : ComponentSpec, IEquatable<CollidersSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DB4 File Offset: 0x00000FB4
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CollidersSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002DC0 File Offset: 0x00000FC0
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002DC8 File Offset: 0x00000FC8
		[Serialize]
		public ImmutableArray<BoxColliderSpec> BoxColliders { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002DD1 File Offset: 0x00000FD1
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002DD9 File Offset: 0x00000FD9
		[Serialize]
		public ImmutableArray<SphereColliderSpec> SphereColliders { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002DE2 File Offset: 0x00000FE2
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002DEA File Offset: 0x00000FEA
		[Serialize]
		public ImmutableArray<CapsuleColliderSpec> CapsuleColliders { get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002DF4 File Offset: 0x00000FF4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CollidersSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E40 File Offset: 0x00001040
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("BoxColliders = ");
			builder.Append(this.BoxColliders.ToString());
			builder.Append(", SphereColliders = ");
			builder.Append(this.SphereColliders.ToString());
			builder.Append(", CapsuleColliders = ");
			builder.Append(this.CapsuleColliders.ToString());
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002ED8 File Offset: 0x000010D8
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CollidersSpec left, CollidersSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EE4 File Offset: 0x000010E4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CollidersSpec left, CollidersSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EF8 File Offset: 0x000010F8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BoxColliderSpec>>.Default.GetHashCode(this.<BoxColliders>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<SphereColliderSpec>>.Default.GetHashCode(this.<SphereColliders>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<CapsuleColliderSpec>>.Default.GetHashCode(this.<CapsuleColliders>k__BackingField);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F50 File Offset: 0x00001150
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CollidersSpec);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F5E File Offset: 0x0000115E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F68 File Offset: 0x00001168
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CollidersSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BoxColliderSpec>>.Default.Equals(this.<BoxColliders>k__BackingField, other.<BoxColliders>k__BackingField) && EqualityComparer<ImmutableArray<SphereColliderSpec>>.Default.Equals(this.<SphereColliders>k__BackingField, other.<SphereColliders>k__BackingField) && EqualityComparer<ImmutableArray<CapsuleColliderSpec>>.Default.Equals(this.<CapsuleColliders>k__BackingField, other.<CapsuleColliders>k__BackingField));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FD4 File Offset: 0x000011D4
		[CompilerGenerated]
		protected CollidersSpec([Nullable(1)] CollidersSpec original) : base(original)
		{
			this.BoxColliders = original.<BoxColliders>k__BackingField;
			this.SphereColliders = original.<SphereColliders>k__BackingField;
			this.CapsuleColliders = original.<CapsuleColliders>k__BackingField;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003001 File Offset: 0x00001201
		public CollidersSpec()
		{
		}
	}
}
