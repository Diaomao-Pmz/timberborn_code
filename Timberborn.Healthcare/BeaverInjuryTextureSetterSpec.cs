using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Healthcare
{
	// Token: 0x02000009 RID: 9
	public class BeaverInjuryTextureSetterSpec : ComponentSpec, IEquatable<BeaverInjuryTextureSetterSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025D3 File Offset: 0x000007D3
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BeaverInjuryTextureSetterSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025DF File Offset: 0x000007DF
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000025E7 File Offset: 0x000007E7
		[Serialize]
		public ImmutableArray<BeaverInjuryTextureSet> InjuryTextureSets { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x000025F0 File Offset: 0x000007F0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BeaverInjuryTextureSetterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000263C File Offset: 0x0000083C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("InjuryTextureSets = ");
			builder.Append(this.InjuryTextureSets.ToString());
			return true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002686 File Offset: 0x00000886
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BeaverInjuryTextureSetterSpec left, BeaverInjuryTextureSetterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002692 File Offset: 0x00000892
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BeaverInjuryTextureSetterSpec left, BeaverInjuryTextureSetterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026A6 File Offset: 0x000008A6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<BeaverInjuryTextureSet>>.Default.GetHashCode(this.<InjuryTextureSets>k__BackingField);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026C5 File Offset: 0x000008C5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BeaverInjuryTextureSetterSpec);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026D3 File Offset: 0x000008D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026DC File Offset: 0x000008DC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BeaverInjuryTextureSetterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<BeaverInjuryTextureSet>>.Default.Equals(this.<InjuryTextureSets>k__BackingField, other.<InjuryTextureSets>k__BackingField));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000270D File Offset: 0x0000090D
		[CompilerGenerated]
		protected BeaverInjuryTextureSetterSpec([Nullable(1)] BeaverInjuryTextureSetterSpec original) : base(original)
		{
			this.InjuryTextureSets = original.<InjuryTextureSets>k__BackingField;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002722 File Offset: 0x00000922
		public BeaverInjuryTextureSetterSpec()
		{
		}
	}
}
