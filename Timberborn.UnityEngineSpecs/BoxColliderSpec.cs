using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public class BoxColliderSpec : IEquatable<BoxColliderSpec>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002917 File Offset: 0x00000B17
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(BoxColliderSpec);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002923 File Offset: 0x00000B23
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000292B File Offset: 0x00000B2B
		[Serialize]
		public Vector3 Center { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002934 File Offset: 0x00000B34
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000293C File Offset: 0x00000B3C
		[Serialize]
		public Vector3 Size { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002948 File Offset: 0x00000B48
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BoxColliderSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002994 File Offset: 0x00000B94
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Center = ");
			builder.Append(this.Center.ToString());
			builder.Append(", Size = ");
			builder.Append(this.Size.ToString());
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029F5 File Offset: 0x00000BF5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(BoxColliderSpec left, BoxColliderSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A01 File Offset: 0x00000C01
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(BoxColliderSpec left, BoxColliderSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A15 File Offset: 0x00000C15
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Center>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Size>k__BackingField);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A55 File Offset: 0x00000C55
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BoxColliderSpec);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A64 File Offset: 0x00000C64
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(BoxColliderSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Vector3>.Default.Equals(this.<Center>k__BackingField, other.<Center>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AC5 File Offset: 0x00000CC5
		[CompilerGenerated]
		protected BoxColliderSpec(BoxColliderSpec original)
		{
			this.Center = original.<Center>k__BackingField;
			this.Size = original.<Size>k__BackingField;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000020F6 File Offset: 0x000002F6
		public BoxColliderSpec()
		{
		}
	}
}
