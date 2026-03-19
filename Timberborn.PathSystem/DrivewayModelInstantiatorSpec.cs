using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200000C RID: 12
	public class DrivewayModelInstantiatorSpec : ComponentSpec, IEquatable<DrivewayModelInstantiatorSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000273E File Offset: 0x0000093E
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DrivewayModelInstantiatorSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000274A File Offset: 0x0000094A
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002752 File Offset: 0x00000952
		[Serialize]
		public AssetRef<GameObject> NarrowLeftDrivewayPrefab { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000275B File Offset: 0x0000095B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002763 File Offset: 0x00000963
		[Serialize]
		public AssetRef<GameObject> NarrowCenterDrivewayPrefab { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002774 File Offset: 0x00000974
		[Serialize]
		public AssetRef<GameObject> NarrowRightDrivewayPrefab { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002785 File Offset: 0x00000985
		[Serialize]
		public AssetRef<GameObject> WideCenterDrivewayPrefab { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002796 File Offset: 0x00000996
		[Serialize]
		public AssetRef<GameObject> LongCenterDrivewayPrefab { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000027A7 File Offset: 0x000009A7
		[Serialize]
		public AssetRef<GameObject> StraightPathDrivewayPrefab { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x000027B0 File Offset: 0x000009B0
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DrivewayModelInstantiatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027FC File Offset: 0x000009FC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("NarrowLeftDrivewayPrefab = ");
			builder.Append(this.NarrowLeftDrivewayPrefab);
			builder.Append(", NarrowCenterDrivewayPrefab = ");
			builder.Append(this.NarrowCenterDrivewayPrefab);
			builder.Append(", NarrowRightDrivewayPrefab = ");
			builder.Append(this.NarrowRightDrivewayPrefab);
			builder.Append(", WideCenterDrivewayPrefab = ");
			builder.Append(this.WideCenterDrivewayPrefab);
			builder.Append(", LongCenterDrivewayPrefab = ");
			builder.Append(this.LongCenterDrivewayPrefab);
			builder.Append(", StraightPathDrivewayPrefab = ");
			builder.Append(this.StraightPathDrivewayPrefab);
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028B5 File Offset: 0x00000AB5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DrivewayModelInstantiatorSpec left, DrivewayModelInstantiatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028C1 File Offset: 0x00000AC1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DrivewayModelInstantiatorSpec left, DrivewayModelInstantiatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028D8 File Offset: 0x00000AD8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<NarrowLeftDrivewayPrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<NarrowCenterDrivewayPrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<NarrowRightDrivewayPrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<WideCenterDrivewayPrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<LongCenterDrivewayPrefab>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<GameObject>>.Default.GetHashCode(this.<StraightPathDrivewayPrefab>k__BackingField);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002975 File Offset: 0x00000B75
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DrivewayModelInstantiatorSpec);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000298C File Offset: 0x00000B8C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DrivewayModelInstantiatorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<NarrowLeftDrivewayPrefab>k__BackingField, other.<NarrowLeftDrivewayPrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<NarrowCenterDrivewayPrefab>k__BackingField, other.<NarrowCenterDrivewayPrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<NarrowRightDrivewayPrefab>k__BackingField, other.<NarrowRightDrivewayPrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<WideCenterDrivewayPrefab>k__BackingField, other.<WideCenterDrivewayPrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<LongCenterDrivewayPrefab>k__BackingField, other.<LongCenterDrivewayPrefab>k__BackingField) && EqualityComparer<AssetRef<GameObject>>.Default.Equals(this.<StraightPathDrivewayPrefab>k__BackingField, other.<StraightPathDrivewayPrefab>k__BackingField));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A48 File Offset: 0x00000C48
		[CompilerGenerated]
		protected DrivewayModelInstantiatorSpec([Nullable(1)] DrivewayModelInstantiatorSpec original) : base(original)
		{
			this.NarrowLeftDrivewayPrefab = original.<NarrowLeftDrivewayPrefab>k__BackingField;
			this.NarrowCenterDrivewayPrefab = original.<NarrowCenterDrivewayPrefab>k__BackingField;
			this.NarrowRightDrivewayPrefab = original.<NarrowRightDrivewayPrefab>k__BackingField;
			this.WideCenterDrivewayPrefab = original.<WideCenterDrivewayPrefab>k__BackingField;
			this.LongCenterDrivewayPrefab = original.<LongCenterDrivewayPrefab>k__BackingField;
			this.StraightPathDrivewayPrefab = original.<StraightPathDrivewayPrefab>k__BackingField;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public DrivewayModelInstantiatorSpec()
		{
		}
	}
}
