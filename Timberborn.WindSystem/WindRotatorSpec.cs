using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WindSystem
{
	// Token: 0x02000010 RID: 16
	public class WindRotatorSpec : IEquatable<WindRotatorSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002951 File Offset: 0x00000B51
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(WindRotatorSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000295D File Offset: 0x00000B5D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002965 File Offset: 0x00000B65
		[Serialize]
		public string TransformName { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000296E File Offset: 0x00000B6E
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002976 File Offset: 0x00000B76
		[Serialize]
		public Vector3 RotationAxis { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000297F File Offset: 0x00000B7F
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002987 File Offset: 0x00000B87
		[Serialize]
		public float RotationSpeed { get; set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002990 File Offset: 0x00000B90
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WindRotatorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000029DC File Offset: 0x00000BDC
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("TransformName = ");
			builder.Append(this.TransformName);
			builder.Append(", RotationAxis = ");
			builder.Append(this.RotationAxis.ToString());
			builder.Append(", RotationSpeed = ");
			builder.Append(this.RotationSpeed.ToString());
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002A56 File Offset: 0x00000C56
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(WindRotatorSpec left, WindRotatorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A62 File Offset: 0x00000C62
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(WindRotatorSpec left, WindRotatorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002A78 File Offset: 0x00000C78
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TransformName>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<RotationAxis>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RotationSpeed>k__BackingField);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002ADA File Offset: 0x00000CDA
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WindRotatorSpec);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002AE8 File Offset: 0x00000CE8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(WindRotatorSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TransformName>k__BackingField, other.<TransformName>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<RotationAxis>k__BackingField, other.<RotationAxis>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RotationSpeed>k__BackingField, other.<RotationSpeed>k__BackingField));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B61 File Offset: 0x00000D61
		[CompilerGenerated]
		protected WindRotatorSpec([Nullable(1)] WindRotatorSpec original)
		{
			this.TransformName = original.<TransformName>k__BackingField;
			this.RotationAxis = original.<RotationAxis>k__BackingField;
			this.RotationSpeed = original.<RotationSpeed>k__BackingField;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000020F6 File Offset: 0x000002F6
		public WindRotatorSpec()
		{
		}
	}
}
