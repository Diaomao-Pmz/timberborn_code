using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.GameSound
{
	// Token: 0x0200000D RID: 13
	public class MusicSpec : ComponentSpec, IEquatable<MusicSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BE8 File Offset: 0x00000DE8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MusicSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002BF4 File Offset: 0x00000DF4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002BFC File Offset: 0x00000DFC
		[Serialize]
		public string DroughtTrack { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C05 File Offset: 0x00000E05
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002C0D File Offset: 0x00000E0D
		[Serialize]
		public string StandardTrack { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002C16 File Offset: 0x00000E16
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002C1E File Offset: 0x00000E1E
		[Serialize]
		public string StandardPhrase { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002C27 File Offset: 0x00000E27
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002C2F File Offset: 0x00000E2F
		[Serialize]
		public float MinDelay { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002C40 File Offset: 0x00000E40
		[Serialize]
		public float MaxDelay { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002C4C File Offset: 0x00000E4C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MusicSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C98 File Offset: 0x00000E98
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DroughtTrack = ");
			builder.Append(this.DroughtTrack);
			builder.Append(", StandardTrack = ");
			builder.Append(this.StandardTrack);
			builder.Append(", StandardPhrase = ");
			builder.Append(this.StandardPhrase);
			builder.Append(", MinDelay = ");
			builder.Append(this.MinDelay.ToString());
			builder.Append(", MaxDelay = ");
			builder.Append(this.MaxDelay.ToString());
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D54 File Offset: 0x00000F54
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MusicSpec left, MusicSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D60 File Offset: 0x00000F60
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MusicSpec left, MusicSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D74 File Offset: 0x00000F74
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DroughtTrack>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StandardTrack>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StandardPhrase>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinDelay>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxDelay>k__BackingField);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DFA File Offset: 0x00000FFA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MusicSpec);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002282 File Offset: 0x00000482
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E08 File Offset: 0x00001008
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MusicSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<DroughtTrack>k__BackingField, other.<DroughtTrack>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StandardTrack>k__BackingField, other.<StandardTrack>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<StandardPhrase>k__BackingField, other.<StandardPhrase>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MinDelay>k__BackingField, other.<MinDelay>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxDelay>k__BackingField, other.<MaxDelay>k__BackingField));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EA8 File Offset: 0x000010A8
		[CompilerGenerated]
		protected MusicSpec([Nullable(1)] MusicSpec original) : base(original)
		{
			this.DroughtTrack = original.<DroughtTrack>k__BackingField;
			this.StandardTrack = original.<StandardTrack>k__BackingField;
			this.StandardPhrase = original.<StandardPhrase>k__BackingField;
			this.MinDelay = original.<MinDelay>k__BackingField;
			this.MaxDelay = original.<MaxDelay>k__BackingField;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002325 File Offset: 0x00000525
		public MusicSpec()
		{
		}
	}
}
