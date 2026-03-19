using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000021 RID: 33
	public class ZiplineTowerSpec : ComponentSpec, IEquatable<ZiplineTowerSpec>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004AE3 File Offset: 0x00002CE3
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ZiplineTowerSpec);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004AEF File Offset: 0x00002CEF
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004AF7 File Offset: 0x00002CF7
		[Serialize]
		public Vector3 CableAnchorPoint { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004B00 File Offset: 0x00002D00
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004B08 File Offset: 0x00002D08
		[Serialize]
		public int MaxConnections { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004B11 File Offset: 0x00002D11
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00004B19 File Offset: 0x00002D19
		[Serialize]
		public int MaxDistance { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004B22 File Offset: 0x00002D22
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004B2A File Offset: 0x00002D2A
		[Serialize]
		public ImmutableArray<Vector3Int> UnobstructedCoordinates { get; set; }

		// Token: 0x0600011A RID: 282 RVA: 0x00004B34 File Offset: 0x00002D34
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ZiplineTowerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004B80 File Offset: 0x00002D80
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("CableAnchorPoint = ");
			builder.Append(this.CableAnchorPoint.ToString());
			builder.Append(", MaxConnections = ");
			builder.Append(this.MaxConnections.ToString());
			builder.Append(", MaxDistance = ");
			builder.Append(this.MaxDistance.ToString());
			builder.Append(", UnobstructedCoordinates = ");
			builder.Append(this.UnobstructedCoordinates.ToString());
			return true;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004C3F File Offset: 0x00002E3F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ZiplineTowerSpec left, ZiplineTowerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004C4B File Offset: 0x00002E4B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ZiplineTowerSpec left, ZiplineTowerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004C60 File Offset: 0x00002E60
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<CableAnchorPoint>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxConnections>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxDistance>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<Vector3Int>>.Default.GetHashCode(this.<UnobstructedCoordinates>k__BackingField);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004CCF File Offset: 0x00002ECF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ZiplineTowerSpec);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002363 File Offset: 0x00000563
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004CE0 File Offset: 0x00002EE0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ZiplineTowerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<CableAnchorPoint>k__BackingField, other.<CableAnchorPoint>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxConnections>k__BackingField, other.<MaxConnections>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxDistance>k__BackingField, other.<MaxDistance>k__BackingField) && EqualityComparer<ImmutableArray<Vector3Int>>.Default.Equals(this.<UnobstructedCoordinates>k__BackingField, other.<UnobstructedCoordinates>k__BackingField));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004D64 File Offset: 0x00002F64
		[CompilerGenerated]
		protected ZiplineTowerSpec([Nullable(1)] ZiplineTowerSpec original) : base(original)
		{
			this.CableAnchorPoint = original.<CableAnchorPoint>k__BackingField;
			this.MaxConnections = original.<MaxConnections>k__BackingField;
			this.MaxDistance = original.<MaxDistance>k__BackingField;
			this.UnobstructedCoordinates = original.<UnobstructedCoordinates>k__BackingField;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000238C File Offset: 0x0000058C
		public ZiplineTowerSpec()
		{
		}
	}
}
