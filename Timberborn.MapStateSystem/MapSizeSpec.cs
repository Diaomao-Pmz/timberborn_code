using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.MapStateSystem
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class MapSizeSpec : ComponentSpec, IEquatable<MapSizeSpec>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002294 File Offset: 0x00000494
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MapSizeSpec);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022A0 File Offset: 0x000004A0
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000022A8 File Offset: 0x000004A8
		[Serialize]
		public Vector2Int DefaultMapSize { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022B1 File Offset: 0x000004B1
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000022B9 File Offset: 0x000004B9
		[Serialize]
		public int MinMapSize { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022C2 File Offset: 0x000004C2
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022CA File Offset: 0x000004CA
		[Serialize]
		public int MaxMapSize { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022D3 File Offset: 0x000004D3
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022DB File Offset: 0x000004DB
		[Serialize]
		public int MaxMapEditorTerrainHeight { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022E4 File Offset: 0x000004E4
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022EC File Offset: 0x000004EC
		[Serialize]
		public int MaxGameTerrainHeight { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022F5 File Offset: 0x000004F5
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022FD File Offset: 0x000004FD
		[Serialize]
		public int MaxHeightAboveTerrain { get; set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002308 File Offset: 0x00000508
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapSizeSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002354 File Offset: 0x00000554
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("DefaultMapSize = ");
			builder.Append(this.DefaultMapSize.ToString());
			builder.Append(", MinMapSize = ");
			builder.Append(this.MinMapSize.ToString());
			builder.Append(", MaxMapSize = ");
			builder.Append(this.MaxMapSize.ToString());
			builder.Append(", MaxMapEditorTerrainHeight = ");
			builder.Append(this.MaxMapEditorTerrainHeight.ToString());
			builder.Append(", MaxGameTerrainHeight = ");
			builder.Append(this.MaxGameTerrainHeight.ToString());
			builder.Append(", MaxHeightAboveTerrain = ");
			builder.Append(this.MaxHeightAboveTerrain.ToString());
			return true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002461 File Offset: 0x00000661
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MapSizeSpec left, MapSizeSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000246D File Offset: 0x0000066D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MapSizeSpec left, MapSizeSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002484 File Offset: 0x00000684
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(this.<DefaultMapSize>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinMapSize>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxMapSize>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxMapEditorTerrainHeight>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxGameTerrainHeight>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxHeightAboveTerrain>k__BackingField);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002521 File Offset: 0x00000721
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapSizeSpec);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000252F File Offset: 0x0000072F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002538 File Offset: 0x00000738
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MapSizeSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector2Int>.Default.Equals(this.<DefaultMapSize>k__BackingField, other.<DefaultMapSize>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MinMapSize>k__BackingField, other.<MinMapSize>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxMapSize>k__BackingField, other.<MaxMapSize>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxMapEditorTerrainHeight>k__BackingField, other.<MaxMapEditorTerrainHeight>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxGameTerrainHeight>k__BackingField, other.<MaxGameTerrainHeight>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxHeightAboveTerrain>k__BackingField, other.<MaxHeightAboveTerrain>k__BackingField));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025F4 File Offset: 0x000007F4
		[CompilerGenerated]
		protected MapSizeSpec(MapSizeSpec original) : base(original)
		{
			this.DefaultMapSize = original.<DefaultMapSize>k__BackingField;
			this.MinMapSize = original.<MinMapSize>k__BackingField;
			this.MaxMapSize = original.<MaxMapSize>k__BackingField;
			this.MaxMapEditorTerrainHeight = original.<MaxMapEditorTerrainHeight>k__BackingField;
			this.MaxGameTerrainHeight = original.<MaxGameTerrainHeight>k__BackingField;
			this.MaxHeightAboveTerrain = original.<MaxHeightAboveTerrain>k__BackingField;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002650 File Offset: 0x00000850
		public MapSizeSpec()
		{
		}
	}
}
