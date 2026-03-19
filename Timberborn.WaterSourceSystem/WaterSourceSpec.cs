using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000023 RID: 35
	public class WaterSourceSpec : ComponentSpec, IEquatable<WaterSourceSpec>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003D3A File Offset: 0x00001F3A
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WaterSourceSpec);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003D46 File Offset: 0x00001F46
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003D4E File Offset: 0x00001F4E
		[Serialize]
		public ImmutableArray<Vector2Int> Coordinates { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003D57 File Offset: 0x00001F57
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00003D5F File Offset: 0x00001F5F
		[Serialize]
		public float DefaultStrength { get; set; }

		// Token: 0x06000130 RID: 304 RVA: 0x00003D68 File Offset: 0x00001F68
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WaterSourceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			builder.Append(", DefaultStrength = ");
			builder.Append(this.DefaultStrength.ToString());
			return true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003E25 File Offset: 0x00002025
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WaterSourceSpec left, WaterSourceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003E31 File Offset: 0x00002031
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WaterSourceSpec left, WaterSourceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003E45 File Offset: 0x00002045
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<Vector2Int>>.Default.GetHashCode(this.<Coordinates>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<DefaultStrength>k__BackingField);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003E7B File Offset: 0x0000207B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WaterSourceSpec);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003E8C File Offset: 0x0000208C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WaterSourceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<Vector2Int>>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<DefaultStrength>k__BackingField, other.<DefaultStrength>k__BackingField));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003EE0 File Offset: 0x000020E0
		[CompilerGenerated]
		protected WaterSourceSpec([Nullable(1)] WaterSourceSpec original) : base(original)
		{
			this.Coordinates = original.<Coordinates>k__BackingField;
			this.DefaultStrength = original.<DefaultStrength>k__BackingField;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002320 File Offset: 0x00000520
		public WaterSourceSpec()
		{
		}
	}
}
