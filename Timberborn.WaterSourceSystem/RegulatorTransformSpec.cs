using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000012 RID: 18
	public class RegulatorTransformSpec : ComponentSpec, IEquatable<RegulatorTransformSpec>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A88 File Offset: 0x00000C88
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(RegulatorTransformSpec);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002A94 File Offset: 0x00000C94
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002A9C File Offset: 0x00000C9C
		[Serialize]
		public string TransformName { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002AA5 File Offset: 0x00000CA5
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002AAD File Offset: 0x00000CAD
		[Serialize]
		public Vector3 TargetOffset { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002AB6 File Offset: 0x00000CB6
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002ABE File Offset: 0x00000CBE
		[Serialize]
		public Vector3 TargetRotation { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002AC8 File Offset: 0x00000CC8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RegulatorTransformSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B14 File Offset: 0x00000D14
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("TransformName = ");
			builder.Append(this.TransformName);
			builder.Append(", TargetOffset = ");
			builder.Append(this.TargetOffset.ToString());
			builder.Append(", TargetRotation = ");
			builder.Append(this.TargetRotation.ToString());
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B9E File Offset: 0x00000D9E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RegulatorTransformSpec left, RegulatorTransformSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002BAA File Offset: 0x00000DAA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RegulatorTransformSpec left, RegulatorTransformSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002BC0 File Offset: 0x00000DC0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TransformName>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<TargetOffset>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<TargetRotation>k__BackingField);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C18 File Offset: 0x00000E18
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RegulatorTransformSpec);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000022F7 File Offset: 0x000004F7
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002C28 File Offset: 0x00000E28
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RegulatorTransformSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TransformName>k__BackingField, other.<TransformName>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<TargetOffset>k__BackingField, other.<TargetOffset>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<TargetRotation>k__BackingField, other.<TargetRotation>k__BackingField));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002C94 File Offset: 0x00000E94
		[CompilerGenerated]
		protected RegulatorTransformSpec([Nullable(1)] RegulatorTransformSpec original) : base(original)
		{
			this.TransformName = original.<TransformName>k__BackingField;
			this.TargetOffset = original.<TargetOffset>k__BackingField;
			this.TargetRotation = original.<TargetRotation>k__BackingField;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002320 File Offset: 0x00000520
		public RegulatorTransformSpec()
		{
		}
	}
}
