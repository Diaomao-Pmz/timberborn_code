using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200000F RID: 15
	public class PlantablePreviewSpec : ComponentSpec, IEquatable<PlantablePreviewSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B4C File Offset: 0x00000D4C
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlantablePreviewSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002B60 File Offset: 0x00000D60
		[Serialize]
		public AssetRef<BinaryData> Model { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002B6C File Offset: 0x00000D6C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlantablePreviewSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BB8 File Offset: 0x00000DB8
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

		// Token: 0x06000043 RID: 67 RVA: 0x00002BE9 File Offset: 0x00000DE9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlantablePreviewSpec left, PlantablePreviewSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BF5 File Offset: 0x00000DF5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlantablePreviewSpec left, PlantablePreviewSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C09 File Offset: 0x00000E09
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<BinaryData>>.Default.GetHashCode(this.<Model>k__BackingField);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C28 File Offset: 0x00000E28
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlantablePreviewSpec);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027CE File Offset: 0x000009CE
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C36 File Offset: 0x00000E36
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlantablePreviewSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<BinaryData>>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C67 File Offset: 0x00000E67
		[CompilerGenerated]
		protected PlantablePreviewSpec([Nullable(1)] PlantablePreviewSpec original) : base(original)
		{
			this.Model = original.<Model>k__BackingField;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000281D File Offset: 0x00000A1D
		public PlantablePreviewSpec()
		{
		}
	}
}
