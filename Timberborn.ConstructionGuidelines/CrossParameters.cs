using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class CrossParameters : IEquatable<CrossParameters>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000031B9 File Offset: 0x000013B9
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CrossParameters);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000031C5 File Offset: 0x000013C5
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000031CD File Offset: 0x000013CD
		public Vector3Int Center { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000031D6 File Offset: 0x000013D6
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000031DE File Offset: 0x000013DE
		public Vector2Int Min { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000031E7 File Offset: 0x000013E7
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000031EF File Offset: 0x000013EF
		public Vector2Int Max { get; private set; }

		// Token: 0x06000057 RID: 87 RVA: 0x000031F8 File Offset: 0x000013F8
		public bool CrossParametersUpdated(Vector3Int center, Vector2Int min, Vector2Int max, bool isFromPreview)
		{
			if (this.Center != center || this.Min != min || this.Max != max || this._isFromPreview != isFromPreview)
			{
				this.Center = center;
				this.Min = min;
				this.Max = max;
				this._isFromPreview = isFromPreview;
				return true;
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003259 File Offset: 0x00001459
		public void Reset()
		{
			this.Center = new Vector3Int(-1, -1, -1);
			this.Min = new Vector2Int(-1, -1);
			this.Max = new Vector2Int(-1, -1);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003284 File Offset: 0x00001484
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CrossParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032D0 File Offset: 0x000014D0
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Center = ");
			builder.Append(this.Center.ToString());
			builder.Append(", Min = ");
			builder.Append(this.Min.ToString());
			builder.Append(", Max = ");
			builder.Append(this.Max.ToString());
			return true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003358 File Offset: 0x00001558
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CrossParameters left, CrossParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003364 File Offset: 0x00001564
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CrossParameters left, CrossParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003378 File Offset: 0x00001578
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<Vector2Int>.Default.GetHashCode(this.<Max>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this._isFromPreview);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033F1 File Offset: 0x000015F1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CrossParameters);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003400 File Offset: 0x00001600
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CrossParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3Int>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<Vector2Int>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<Vector2Int>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField) && EqualityComparer<bool>.Default.Equals(this._isFromPreview, other._isFromPreview));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003491 File Offset: 0x00001691
		[CompilerGenerated]
		protected CrossParameters(CrossParameters original)
		{
			this.Center = original.<Center>k__BackingField;
			this.Min = original.<Min>k__BackingField;
			this.Max = original.<Max>k__BackingField;
			this._isFromPreview = original._isFromPreview;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000034C9 File Offset: 0x000016C9
		public CrossParameters()
		{
			this.Center = new Vector3Int(-1, -1, -1);
			this.Min = new Vector2Int(-1, -1);
			this.Max = new Vector2Int(-1, -1);
			base..ctor();
		}

		// Token: 0x0400004F RID: 79
		public bool _isFromPreview;
	}
}
