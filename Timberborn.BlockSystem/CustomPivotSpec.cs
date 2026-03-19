using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public class CustomPivotSpec : IEquatable<CustomPivotSpec>
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005FD3 File Offset: 0x000041D3
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CustomPivotSpec);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005FDF File Offset: 0x000041DF
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00005FE7 File Offset: 0x000041E7
		[Serialize]
		public bool HasCustomPivot { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005FF0 File Offset: 0x000041F0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00005FF8 File Offset: 0x000041F8
		[Serialize]
		public Vector3 Coordinates { get; set; }

		// Token: 0x06000197 RID: 407 RVA: 0x00006004 File Offset: 0x00004204
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CustomPivotSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006050 File Offset: 0x00004250
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("HasCustomPivot = ");
			builder.Append(this.HasCustomPivot.ToString());
			builder.Append(", Coordinates = ");
			builder.Append(this.Coordinates.ToString());
			return true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000060B1 File Offset: 0x000042B1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CustomPivotSpec left, CustomPivotSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000060BD File Offset: 0x000042BD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CustomPivotSpec left, CustomPivotSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000060D1 File Offset: 0x000042D1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasCustomPivot>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Coordinates>k__BackingField);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006111 File Offset: 0x00004311
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CustomPivotSpec);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006120 File Offset: 0x00004320
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CustomPivotSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<HasCustomPivot>k__BackingField, other.<HasCustomPivot>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Coordinates>k__BackingField, other.<Coordinates>k__BackingField));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006181 File Offset: 0x00004381
		[CompilerGenerated]
		protected CustomPivotSpec(CustomPivotSpec original)
		{
			this.HasCustomPivot = original.<HasCustomPivot>k__BackingField;
			this.Coordinates = original.<Coordinates>k__BackingField;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000020F8 File Offset: 0x000002F8
		public CustomPivotSpec()
		{
		}
	}
}
