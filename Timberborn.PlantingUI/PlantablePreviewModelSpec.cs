using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200000D RID: 13
	public class PlantablePreviewModelSpec : ComponentSpec, IEquatable<PlantablePreviewModelSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026E6 File Offset: 0x000008E6
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlantablePreviewModelSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026F2 File Offset: 0x000008F2
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000026FA File Offset: 0x000008FA
		[Serialize]
		public AssetRef<BinaryData> Model { get; set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002704 File Offset: 0x00000904
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantablePreviewModelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002750 File Offset: 0x00000950
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Model = ");
			builder.Append(this.Model);
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002781 File Offset: 0x00000981
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantablePreviewModelSpec left, PlantablePreviewModelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000278D File Offset: 0x0000098D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantablePreviewModelSpec left, PlantablePreviewModelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027A1 File Offset: 0x000009A1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<BinaryData>>.Default.GetHashCode(this.<Model>k__BackingField);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027C0 File Offset: 0x000009C0
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantablePreviewModelSpec);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027CE File Offset: 0x000009CE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027D7 File Offset: 0x000009D7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantablePreviewModelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<BinaryData>>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002808 File Offset: 0x00000A08
		[CompilerGenerated]
		protected PlantablePreviewModelSpec([Nullable(1)] PlantablePreviewModelSpec original) : base(original)
		{
			this.Model = original.<Model>k__BackingField;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000281D File Offset: 0x00000A1D
		public PlantablePreviewModelSpec()
		{
		}
	}
}
