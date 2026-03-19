using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class CapsuleColliderSpec : IEquatable<CapsuleColliderSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AE5 File Offset: 0x00000CE5
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CapsuleColliderSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002AF1 File Offset: 0x00000CF1
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002AF9 File Offset: 0x00000CF9
		[Serialize]
		public Vector3 Center { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B02 File Offset: 0x00000D02
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B0A File Offset: 0x00000D0A
		[Serialize]
		public float Radius { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B13 File Offset: 0x00000D13
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B1B File Offset: 0x00000D1B
		[Serialize]
		public float Height { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B24 File Offset: 0x00000D24
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002B2C File Offset: 0x00000D2C
		[Serialize]
		public Axis Axis { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002B38 File Offset: 0x00000D38
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CapsuleColliderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B84 File Offset: 0x00000D84
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Center = ");
			builder.Append(this.Center.ToString());
			builder.Append(", Radius = ");
			builder.Append(this.Radius.ToString());
			builder.Append(", Height = ");
			builder.Append(this.Height.ToString());
			builder.Append(", Axis = ");
			builder.Append(this.Axis.ToString());
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C33 File Offset: 0x00000E33
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CapsuleColliderSpec left, CapsuleColliderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C3F File Offset: 0x00000E3F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CapsuleColliderSpec left, CapsuleColliderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C54 File Offset: 0x00000E54
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Radius>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Height>k__BackingField)) * -1521134295 + EqualityComparer<Axis>.Default.GetHashCode(this.<Axis>k__BackingField);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CCD File Offset: 0x00000ECD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CapsuleColliderSpec);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CDC File Offset: 0x00000EDC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CapsuleColliderSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField) && EqualityComparer<Axis>.Default.Equals(this.<Axis>k__BackingField, other.<Axis>k__BackingField));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D6D File Offset: 0x00000F6D
		[CompilerGenerated]
		protected CapsuleColliderSpec(CapsuleColliderSpec original)
		{
			this.Center = original.<Center>k__BackingField;
			this.Radius = original.<Radius>k__BackingField;
			this.Height = original.<Height>k__BackingField;
			this.Axis = original.<Axis>k__BackingField;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DA5 File Offset: 0x00000FA5
		public CapsuleColliderSpec()
		{
			this.Axis = Axis.Y;
			base..ctor();
		}
	}
}
