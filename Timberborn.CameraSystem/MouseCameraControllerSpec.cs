using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class MouseCameraControllerSpec : ComponentSpec, IEquatable<MouseCameraControllerSpec>
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005312 File Offset: 0x00003512
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MouseCameraControllerSpec);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000531E File Offset: 0x0000351E
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005326 File Offset: 0x00003526
		[Serialize]
		public float RmbRotationSpeed { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000532F File Offset: 0x0000352F
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005337 File Offset: 0x00003537
		[Serialize]
		public float RmbRotationMinDistance { get; set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00005340 File Offset: 0x00003540
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MouseCameraControllerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000538C File Offset: 0x0000358C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("RmbRotationSpeed = ");
			builder.Append(this.RmbRotationSpeed.ToString());
			builder.Append(", RmbRotationMinDistance = ");
			builder.Append(this.RmbRotationMinDistance.ToString());
			return true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000053FD File Offset: 0x000035FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MouseCameraControllerSpec left, MouseCameraControllerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005409 File Offset: 0x00003609
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MouseCameraControllerSpec left, MouseCameraControllerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000541D File Offset: 0x0000361D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RmbRotationSpeed>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<RmbRotationMinDistance>k__BackingField);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005453 File Offset: 0x00003653
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MouseCameraControllerSpec);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003460 File Offset: 0x00001660
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005464 File Offset: 0x00003664
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MouseCameraControllerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<RmbRotationSpeed>k__BackingField, other.<RmbRotationSpeed>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<RmbRotationMinDistance>k__BackingField, other.<RmbRotationMinDistance>k__BackingField));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000054B8 File Offset: 0x000036B8
		[CompilerGenerated]
		protected MouseCameraControllerSpec(MouseCameraControllerSpec original) : base(original)
		{
			this.RmbRotationSpeed = original.<RmbRotationSpeed>k__BackingField;
			this.RmbRotationMinDistance = original.<RmbRotationMinDistance>k__BackingField;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003694 File Offset: 0x00001894
		public MouseCameraControllerSpec()
		{
		}
	}
}
