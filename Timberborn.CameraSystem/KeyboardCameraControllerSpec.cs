using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public class KeyboardCameraControllerSpec : ComponentSpec, IEquatable<KeyboardCameraControllerSpec>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004DE1 File Offset: 0x00002FE1
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(KeyboardCameraControllerSpec);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004DED File Offset: 0x00002FED
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004DF5 File Offset: 0x00002FF5
		[Serialize]
		public int JumpRotationAngle { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004DFE File Offset: 0x00002FFE
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00004E06 File Offset: 0x00003006
		[Serialize]
		public int JumpRotationSpeedInAnglePerUpdate { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004E0F File Offset: 0x0000300F
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00004E17 File Offset: 0x00003017
		[Serialize]
		public float BaseZoomSpeed { get; set; }

		// Token: 0x0600010C RID: 268 RVA: 0x00004E20 File Offset: 0x00003020
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("KeyboardCameraControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004E6C File Offset: 0x0000306C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("JumpRotationAngle = ");
			builder.Append(this.JumpRotationAngle.ToString());
			builder.Append(", JumpRotationSpeedInAnglePerUpdate = ");
			builder.Append(this.JumpRotationSpeedInAnglePerUpdate.ToString());
			builder.Append(", BaseZoomSpeed = ");
			builder.Append(this.BaseZoomSpeed.ToString());
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004F04 File Offset: 0x00003104
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(KeyboardCameraControllerSpec left, KeyboardCameraControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004F10 File Offset: 0x00003110
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(KeyboardCameraControllerSpec left, KeyboardCameraControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004F24 File Offset: 0x00003124
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<JumpRotationAngle>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<JumpRotationSpeedInAnglePerUpdate>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<BaseZoomSpeed>k__BackingField);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004F7C File Offset: 0x0000317C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as KeyboardCameraControllerSpec);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004F8C File Offset: 0x0000318C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(KeyboardCameraControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<JumpRotationAngle>k__BackingField, other.<JumpRotationAngle>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<JumpRotationSpeedInAnglePerUpdate>k__BackingField, other.<JumpRotationSpeedInAnglePerUpdate>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<BaseZoomSpeed>k__BackingField, other.<BaseZoomSpeed>k__BackingField));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004FF8 File Offset: 0x000031F8
		[CompilerGenerated]
		protected KeyboardCameraControllerSpec(KeyboardCameraControllerSpec original) : base(original)
		{
			this.JumpRotationAngle = original.<JumpRotationAngle>k__BackingField;
			this.JumpRotationSpeedInAnglePerUpdate = original.<JumpRotationSpeedInAnglePerUpdate>k__BackingField;
			this.BaseZoomSpeed = original.<BaseZoomSpeed>k__BackingField;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003694 File Offset: 0x00001894
		public KeyboardCameraControllerSpec()
		{
		}
	}
}
