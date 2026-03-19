using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public class SphereColliderSpec : IEquatable<SphereColliderSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003150 File Offset: 0x00001350
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SphereColliderSpec);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003164 File Offset: 0x00001364
		[Serialize]
		public Vector3 Center { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000316D File Offset: 0x0000136D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003175 File Offset: 0x00001375
		[Serialize]
		public float Radius { get; set; }

		// Token: 0x06000075 RID: 117 RVA: 0x00003180 File Offset: 0x00001380
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SphereColliderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031CC File Offset: 0x000013CC
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Center = ");
			builder.Append(this.Center.ToString());
			builder.Append(", Radius = ");
			builder.Append(this.Radius.ToString());
			return true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000322D File Offset: 0x0000142D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SphereColliderSpec left, SphereColliderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003239 File Offset: 0x00001439
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SphereColliderSpec left, SphereColliderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000324D File Offset: 0x0000144D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Radius>k__BackingField);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000328D File Offset: 0x0000148D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SphereColliderSpec);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000329C File Offset: 0x0000149C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SphereColliderSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032FD File Offset: 0x000014FD
		[CompilerGenerated]
		protected SphereColliderSpec(SphereColliderSpec original)
		{
			this.Center = original.<Center>k__BackingField;
			this.Radius = original.<Radius>k__BackingField;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000020F6 File Offset: 0x000002F6
		public SphereColliderSpec()
		{
		}
	}
}
