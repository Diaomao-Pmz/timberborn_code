using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	public class TransformSpec : ComponentSpec, IEquatable<TransformSpec>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000331D File Offset: 0x0000151D
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TransformSpec);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003329 File Offset: 0x00001529
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003331 File Offset: 0x00001531
		[Serialize]
		public Vector3 Position { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000333A File Offset: 0x0000153A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003342 File Offset: 0x00001542
		[Serialize]
		public Vector3 Rotation { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000334B File Offset: 0x0000154B
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003353 File Offset: 0x00001553
		[Serialize]
		public Vector3 Scale { get; set; }

		// Token: 0x06000086 RID: 134 RVA: 0x0000335C File Offset: 0x0000155C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TransformSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033A8 File Offset: 0x000015A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Position = ");
			builder.Append(this.Position.ToString());
			builder.Append(", Rotation = ");
			builder.Append(this.Rotation.ToString());
			builder.Append(", Scale = ");
			builder.Append(this.Scale.ToString());
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003440 File Offset: 0x00001640
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TransformSpec left, TransformSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000344C File Offset: 0x0000164C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TransformSpec left, TransformSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003460 File Offset: 0x00001660
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Position>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Rotation>k__BackingField)) * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(this.<Scale>k__BackingField);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000034B8 File Offset: 0x000016B8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TransformSpec);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002F5E File Offset: 0x0000115E
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000034C8 File Offset: 0x000016C8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TransformSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3>.Default.Equals(this.<Position>k__BackingField, other.<Position>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Rotation>k__BackingField, other.<Rotation>k__BackingField) && EqualityComparer<Vector3>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003534 File Offset: 0x00001734
		[CompilerGenerated]
		protected TransformSpec(TransformSpec original) : base(original)
		{
			this.Position = original.<Position>k__BackingField;
			this.Rotation = original.<Rotation>k__BackingField;
			this.Scale = original.<Scale>k__BackingField;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003561 File Offset: 0x00001761
		public TransformSpec()
		{
			this.Scale = Vector3.one;
			base..ctor();
		}
	}
}
