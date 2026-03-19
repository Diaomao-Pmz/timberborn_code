using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001E RID: 30
	public class MechanicalNodeTransformHeightSpec : ComponentSpec, IEquatable<MechanicalNodeTransformHeightSpec>
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000400D File Offset: 0x0000220D
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MechanicalNodeTransformHeightSpec);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004019 File Offset: 0x00002219
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004021 File Offset: 0x00002221
		[Serialize]
		public string TransformName { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000402A File Offset: 0x0000222A
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004032 File Offset: 0x00002232
		[Serialize]
		public float Range { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000403B File Offset: 0x0000223B
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004043 File Offset: 0x00002243
		[Serialize]
		public float ChangeSpeed { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x0000404C File Offset: 0x0000224C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MechanicalNodeTransformHeightSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004098 File Offset: 0x00002298
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
			builder.Append(", Range = ");
			builder.Append(this.Range.ToString());
			builder.Append(", ChangeSpeed = ");
			builder.Append(this.ChangeSpeed.ToString());
			return true;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004122 File Offset: 0x00002322
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MechanicalNodeTransformHeightSpec left, MechanicalNodeTransformHeightSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000412E File Offset: 0x0000232E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MechanicalNodeTransformHeightSpec left, MechanicalNodeTransformHeightSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004144 File Offset: 0x00002344
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TransformName>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<Range>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ChangeSpeed>k__BackingField);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000419C File Offset: 0x0000239C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MechanicalNodeTransformHeightSpec);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002553 File Offset: 0x00000753
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000041AC File Offset: 0x000023AC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MechanicalNodeTransformHeightSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TransformName>k__BackingField, other.<TransformName>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<Range>k__BackingField, other.<Range>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<ChangeSpeed>k__BackingField, other.<ChangeSpeed>k__BackingField));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004218 File Offset: 0x00002418
		[CompilerGenerated]
		protected MechanicalNodeTransformHeightSpec([Nullable(1)] MechanicalNodeTransformHeightSpec original) : base(original)
		{
			this.TransformName = original.<TransformName>k__BackingField;
			this.Range = original.<Range>k__BackingField;
			this.ChangeSpeed = original.<ChangeSpeed>k__BackingField;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000257C File Offset: 0x0000077C
		public MechanicalNodeTransformHeightSpec()
		{
		}
	}
}
