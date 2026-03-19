using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public class EdgePanningCameraTargetPickerSpec : ComponentSpec, IEquatable<EdgePanningCameraTargetPickerSpec>
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000442B File Offset: 0x0000262B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(EdgePanningCameraTargetPickerSpec);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004437 File Offset: 0x00002637
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000443F File Offset: 0x0000263F
		[Serialize]
		public float MinBaseSpeed { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004448 File Offset: 0x00002648
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004450 File Offset: 0x00002650
		[Serialize]
		public float MaxBaseSpeed { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004459 File Offset: 0x00002659
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004461 File Offset: 0x00002661
		[Serialize]
		public float FastMovementSpeedBonus { get; set; }

		// Token: 0x060000CC RID: 204 RVA: 0x0000446C File Offset: 0x0000266C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EdgePanningCameraTargetPickerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000044B8 File Offset: 0x000026B8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinBaseSpeed = ");
			builder.Append(this.MinBaseSpeed.ToString());
			builder.Append(", MaxBaseSpeed = ");
			builder.Append(this.MaxBaseSpeed.ToString());
			builder.Append(", FastMovementSpeedBonus = ");
			builder.Append(this.FastMovementSpeedBonus.ToString());
			return true;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004550 File Offset: 0x00002750
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(EdgePanningCameraTargetPickerSpec left, EdgePanningCameraTargetPickerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000455C File Offset: 0x0000275C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(EdgePanningCameraTargetPickerSpec left, EdgePanningCameraTargetPickerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004570 File Offset: 0x00002770
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinBaseSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxBaseSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FastMovementSpeedBonus>k__BackingField);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000045C8 File Offset: 0x000027C8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EdgePanningCameraTargetPickerSpec);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000045D8 File Offset: 0x000027D8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(EdgePanningCameraTargetPickerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinBaseSpeed>k__BackingField, other.<MinBaseSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxBaseSpeed>k__BackingField, other.<MaxBaseSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FastMovementSpeedBonus>k__BackingField, other.<FastMovementSpeedBonus>k__BackingField));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004644 File Offset: 0x00002844
		[CompilerGenerated]
		protected EdgePanningCameraTargetPickerSpec(EdgePanningCameraTargetPickerSpec original) : base(original)
		{
			this.MinBaseSpeed = original.<MinBaseSpeed>k__BackingField;
			this.MaxBaseSpeed = original.<MaxBaseSpeed>k__BackingField;
			this.FastMovementSpeedBonus = original.<FastMovementSpeedBonus>k__BackingField;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003694 File Offset: 0x00001894
		public EdgePanningCameraTargetPickerSpec()
		{
		}
	}
}
