using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public class DraggingCameraTargetPickerSpec : ComponentSpec, IEquatable<DraggingCameraTargetPickerSpec>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004133 File Offset: 0x00002333
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DraggingCameraTargetPickerSpec);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000413F File Offset: 0x0000233F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004147 File Offset: 0x00002347
		[Serialize]
		public float MovementSpeed { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00004150 File Offset: 0x00002350
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DraggingCameraTargetPickerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000419C File Offset: 0x0000239C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MovementSpeed = ");
			builder.Append(this.MovementSpeed.ToString());
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000041E6 File Offset: 0x000023E6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DraggingCameraTargetPickerSpec left, DraggingCameraTargetPickerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000041F2 File Offset: 0x000023F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DraggingCameraTargetPickerSpec left, DraggingCameraTargetPickerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004206 File Offset: 0x00002406
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MovementSpeed>k__BackingField);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004225 File Offset: 0x00002425
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DraggingCameraTargetPickerSpec);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004233 File Offset: 0x00002433
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DraggingCameraTargetPickerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MovementSpeed>k__BackingField, other.<MovementSpeed>k__BackingField));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004264 File Offset: 0x00002464
		[CompilerGenerated]
		protected DraggingCameraTargetPickerSpec(DraggingCameraTargetPickerSpec original) : base(original)
		{
			this.MovementSpeed = original.<MovementSpeed>k__BackingField;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003694 File Offset: 0x00001894
		public DraggingCameraTargetPickerSpec()
		{
		}
	}
}
