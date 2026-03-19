using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	public class TransputSpec : IEquatable<TransputSpec>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004A19 File Offset: 0x00002C19
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TransputSpec);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004A25 File Offset: 0x00002C25
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004A2D File Offset: 0x00002C2D
		[Serialize]
		public Vector3Int Coordinates { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004A36 File Offset: 0x00002C36
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004A3E File Offset: 0x00002C3E
		[Serialize]
		public Directions3D Directions { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004A47 File Offset: 0x00002C47
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004A4F File Offset: 0x00002C4F
		[Serialize]
		public bool ReverseRotation { get; set; }

		// Token: 0x06000140 RID: 320 RVA: 0x00004A58 File Offset: 0x00002C58
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransputSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004AA4 File Offset: 0x00002CA4
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			builder.Append(", Directions = ");
			builder.Append(this.Directions.ToString());
			builder.Append(", ReverseRotation = ");
			builder.Append(this.ReverseRotation.ToString());
			return true;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004B2C File Offset: 0x00002D2C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransputSpec left, TransputSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004B38 File Offset: 0x00002D38
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransputSpec left, TransputSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004B4C File Offset: 0x00002D4C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Coordinates>k__BackingField)) * -1521134295 + EqualityComparer<Directions3D>.Default.GetHashCode(this.<Directions>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ReverseRotation>k__BackingField);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004BAE File Offset: 0x00002DAE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransputSpec);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004BBC File Offset: 0x00002DBC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransputSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3Int>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField) && EqualityComparer<Directions3D>.Default.Equals(this.<Directions>k__BackingField, other.<Directions>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ReverseRotation>k__BackingField, other.<ReverseRotation>k__BackingField));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004C35 File Offset: 0x00002E35
		[CompilerGenerated]
		protected TransputSpec(TransputSpec original)
		{
			this.Coordinates = original.<Coordinates>k__BackingField;
			this.Directions = original.<Directions>k__BackingField;
			this.ReverseRotation = original.<ReverseRotation>k__BackingField;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000020F8 File Offset: 0x000002F8
		public TransputSpec()
		{
		}
	}
}
