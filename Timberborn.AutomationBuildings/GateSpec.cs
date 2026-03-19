using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class GateSpec : ComponentSpec, IEquatable<GateSpec>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000404F File Offset: 0x0000224F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GateSpec);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000405B File Offset: 0x0000225B
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004063 File Offset: 0x00002263
		[Serialize]
		public Vector3Int Start { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000406C File Offset: 0x0000226C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004074 File Offset: 0x00002274
		[Serialize]
		public Vector3Int End { get; set; }

		// Token: 0x06000102 RID: 258 RVA: 0x00004080 File Offset: 0x00002280
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GateSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000040CC File Offset: 0x000022CC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Start = ");
			builder.Append(this.Start.ToString());
			builder.Append(", End = ");
			builder.Append(this.End.ToString());
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000413D File Offset: 0x0000233D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GateSpec left, GateSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004149 File Offset: 0x00002349
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GateSpec left, GateSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000415D File Offset: 0x0000235D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Start>k__BackingField)) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<End>k__BackingField);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004193 File Offset: 0x00002393
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GateSpec);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000274F File Offset: 0x0000094F
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000041A4 File Offset: 0x000023A4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GateSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<Start>k__BackingField, other.<Start>k__BackingField) && EqualityComparer<Vector3Int>.Default.Equals(this.<End>k__BackingField, other.<End>k__BackingField));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000041F8 File Offset: 0x000023F8
		[CompilerGenerated]
		protected GateSpec(GateSpec original) : base(original)
		{
			this.Start = original.<Start>k__BackingField;
			this.End = original.<End>k__BackingField;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002778 File Offset: 0x00000978
		public GateSpec()
		{
		}
	}
}
