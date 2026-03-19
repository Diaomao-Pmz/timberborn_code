using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200000C RID: 12
	public class BuildingDetailTextureSpec : ComponentSpec, IEquatable<BuildingDetailTextureSpec>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025C0 File Offset: 0x000007C0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(BuildingDetailTextureSpec);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000025D4 File Offset: 0x000007D4
		[Serialize]
		public AssetRef<Texture> Texture { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000025DD File Offset: 0x000007DD
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000025E5 File Offset: 0x000007E5
		[Serialize]
		public Color Color { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x000025F0 File Offset: 0x000007F0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BuildingDetailTextureSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000263C File Offset: 0x0000083C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Texture = ");
			builder.Append(this.Texture);
			builder.Append(", Color = ");
			builder.Append(this.Color.ToString());
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000269F File Offset: 0x0000089F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BuildingDetailTextureSpec left, BuildingDetailTextureSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026AB File Offset: 0x000008AB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BuildingDetailTextureSpec left, BuildingDetailTextureSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026BF File Offset: 0x000008BF
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Texture>>.Default.GetHashCode(this.<Texture>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026F5 File Offset: 0x000008F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BuildingDetailTextureSpec);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000023ED File Offset: 0x000005ED
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002704 File Offset: 0x00000904
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BuildingDetailTextureSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<UnityEngine.Texture>>.Default.Equals(this.<Texture>k__BackingField, other.<Texture>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002758 File Offset: 0x00000958
		[CompilerGenerated]
		protected BuildingDetailTextureSpec([Nullable(1)] BuildingDetailTextureSpec original) : base(original)
		{
			this.Texture = original.<Texture>k__BackingField;
			this.Color = original.<Color>k__BackingField;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000246D File Offset: 0x0000066D
		public BuildingDetailTextureSpec()
		{
		}
	}
}
