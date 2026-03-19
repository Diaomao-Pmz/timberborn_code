using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x02000008 RID: 8
	internal class NaturalResourceBrushSpec : ComponentSpec, IEquatable<NaturalResourceBrushSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(NaturalResourceBrushSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021FC File Offset: 0x000003FC
		[Serialize]
		public Color RemovalTileColor { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002205 File Offset: 0x00000405
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000220D File Offset: 0x0000040D
		[Serialize]
		public Color SpawnTileColor { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002216 File Offset: 0x00000416
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000221E File Offset: 0x0000041E
		[Serialize]
		public string DefaultNaturalResourceId { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002228 File Offset: 0x00000428
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NaturalResourceBrushSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002274 File Offset: 0x00000474
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RemovalTileColor = ");
			builder.Append(this.RemovalTileColor.ToString());
			builder.Append(", SpawnTileColor = ");
			builder.Append(this.SpawnTileColor.ToString());
			builder.Append(", DefaultNaturalResourceId = ");
			builder.Append(this.DefaultNaturalResourceId);
			return true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022FE File Offset: 0x000004FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NaturalResourceBrushSpec left, NaturalResourceBrushSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000230A File Offset: 0x0000050A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NaturalResourceBrushSpec left, NaturalResourceBrushSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002320 File Offset: 0x00000520
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RemovalTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SpawnTileColor>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DefaultNaturalResourceId>k__BackingField);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002378 File Offset: 0x00000578
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NaturalResourceBrushSpec);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002386 File Offset: 0x00000586
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002390 File Offset: 0x00000590
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NaturalResourceBrushSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<RemovalTileColor>k__BackingField, other.<RemovalTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SpawnTileColor>k__BackingField, other.<SpawnTileColor>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DefaultNaturalResourceId>k__BackingField, other.<DefaultNaturalResourceId>k__BackingField));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023FC File Offset: 0x000005FC
		[CompilerGenerated]
		protected NaturalResourceBrushSpec([Nullable(1)] NaturalResourceBrushSpec original) : base(original)
		{
			this.RemovalTileColor = original.<RemovalTileColor>k__BackingField;
			this.SpawnTileColor = original.<SpawnTileColor>k__BackingField;
			this.DefaultNaturalResourceId = original.<DefaultNaturalResourceId>k__BackingField;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002429 File Offset: 0x00000629
		public NaturalResourceBrushSpec()
		{
		}
	}
}
