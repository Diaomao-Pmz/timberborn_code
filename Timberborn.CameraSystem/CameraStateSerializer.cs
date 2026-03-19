using System;
using Timberborn.Persistence;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000010 RID: 16
	public class CameraStateSerializer : IValueSerializer<CameraState>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public void Serialize(CameraState value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(CameraStateSerializer.TargetKey, value.Target);
			objectSaver.Set(CameraStateSerializer.ZoomLevelKey, value.ZoomLevel);
			objectSaver.Set(CameraStateSerializer.HorizontalAngleKey, value.HorizontalAngle);
			objectSaver.Set(CameraStateSerializer.VerticalAngleKey, value.VerticalAngle);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B00 File Offset: 0x00001D00
		public Obsoletable<CameraState> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new CameraState(objectLoader.Get(CameraStateSerializer.TargetKey), objectLoader.Get(CameraStateSerializer.ZoomLevelKey), objectLoader.Get(CameraStateSerializer.HorizontalAngleKey), objectLoader.Get(CameraStateSerializer.VerticalAngleKey));
		}

		// Token: 0x04000057 RID: 87
		public static readonly PropertyKey<Vector3> TargetKey = new PropertyKey<Vector3>("Target");

		// Token: 0x04000058 RID: 88
		public static readonly PropertyKey<float> ZoomLevelKey = new PropertyKey<float>("ZoomLevel");

		// Token: 0x04000059 RID: 89
		public static readonly PropertyKey<float> HorizontalAngleKey = new PropertyKey<float>("HorizontalAngle");

		// Token: 0x0400005A RID: 90
		public static readonly PropertyKey<float> VerticalAngleKey = new PropertyKey<float>("VerticalAngle");
	}
}
