using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x02000009 RID: 9
	public class DemolishableMarkerServiceSpec : ComponentSpec, IEquatable<DemolishableMarkerServiceSpec>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000026A9 File Offset: 0x000008A9
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DemolishableMarkerServiceSpec);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000026BD File Offset: 0x000008BD
		[Serialize]
		public AssetRef<Mesh> Mesh { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000026C6 File Offset: 0x000008C6
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000026CE File Offset: 0x000008CE
		[Serialize]
		public AssetRef<Material> Material { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000026D8 File Offset: 0x000008D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DemolishableMarkerServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002724 File Offset: 0x00000924
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Mesh = ");
			builder.Append(this.Mesh);
			builder.Append(", Material = ");
			builder.Append(this.Material);
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002779 File Offset: 0x00000979
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DemolishableMarkerServiceSpec left, DemolishableMarkerServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002785 File Offset: 0x00000985
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DemolishableMarkerServiceSpec left, DemolishableMarkerServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002799 File Offset: 0x00000999
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Mesh>>.Default.GetHashCode(this.<Mesh>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<UnityEngine.Material>>.Default.GetHashCode(this.<Material>k__BackingField);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027CF File Offset: 0x000009CF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DemolishableMarkerServiceSpec);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027DD File Offset: 0x000009DD
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027E8 File Offset: 0x000009E8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DemolishableMarkerServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<AssetRef<UnityEngine.Mesh>>.Default.Equals(this.<Mesh>k__BackingField, other.<Mesh>k__BackingField) && EqualityComparer<AssetRef<UnityEngine.Material>>.Default.Equals(this.<Material>k__BackingField, other.<Material>k__BackingField));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000283C File Offset: 0x00000A3C
		[CompilerGenerated]
		protected DemolishableMarkerServiceSpec([Nullable(1)] DemolishableMarkerServiceSpec original) : base(original)
		{
			this.Mesh = original.<Mesh>k__BackingField;
			this.Material = original.<Material>k__BackingField;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000285D File Offset: 0x00000A5D
		public DemolishableMarkerServiceSpec()
		{
		}
	}
}
