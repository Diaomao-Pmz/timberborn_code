using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x0200000C RID: 12
	public class NaturalResourceBrushSpec : ComponentSpec, IEquatable<NaturalResourceBrushSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000026DB File Offset: 0x000008DB
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000026E7 File Offset: 0x000008E7
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000026EF File Offset: 0x000008EF
		[Serialize]
		public Color RemovalTileColor { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026F8 File Offset: 0x000008F8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002700 File Offset: 0x00000900
		[Serialize]
		public Color SpawnTileColor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002709 File Offset: 0x00000909
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002711 File Offset: 0x00000911
		[Serialize]
		public string DefaultNaturalResourceId { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x0000271C File Offset: 0x0000091C
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002768 File Offset: 0x00000968
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

		// Token: 0x0600002A RID: 42 RVA: 0x000027F2 File Offset: 0x000009F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NaturalResourceBrushSpec left, NaturalResourceBrushSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027FE File Offset: 0x000009FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NaturalResourceBrushSpec left, NaturalResourceBrushSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002814 File Offset: 0x00000A14
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RemovalTileColor>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SpawnTileColor>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DefaultNaturalResourceId>k__BackingField);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000286C File Offset: 0x00000A6C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NaturalResourceBrushSpec);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000287A File Offset: 0x00000A7A
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002884 File Offset: 0x00000A84
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NaturalResourceBrushSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<RemovalTileColor>k__BackingField, other.<RemovalTileColor>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SpawnTileColor>k__BackingField, other.<SpawnTileColor>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DefaultNaturalResourceId>k__BackingField, other.<DefaultNaturalResourceId>k__BackingField));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028F0 File Offset: 0x00000AF0
		[CompilerGenerated]
		protected NaturalResourceBrushSpec([Nullable(1)] NaturalResourceBrushSpec original) : base(original)
		{
			this.RemovalTileColor = original.<RemovalTileColor>k__BackingField;
			this.SpawnTileColor = original.<SpawnTileColor>k__BackingField;
			this.DefaultNaturalResourceId = original.<DefaultNaturalResourceId>k__BackingField;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000291D File Offset: 0x00000B1D
		public NaturalResourceBrushSpec()
		{
		}
	}
}
