using System;
using Timberborn.Persistence;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000005 RID: 5
	public class CameraConfigurationSerializer : IValueSerializer<CameraConfiguration>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020EF File Offset: 0x000002EF
		public void Serialize(CameraConfiguration value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(CameraConfigurationSerializer.PositionKey, value.Position);
			objectSaver.Set(CameraConfigurationSerializer.RotationKey, value.Rotation);
			objectSaver.Set(CameraConfigurationSerializer.ShadowDistanceKey, value.ShadowDistance);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public Obsoletable<CameraConfiguration> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new Obsoletable<CameraConfiguration>(new CameraConfiguration(objectLoader.Get(CameraConfigurationSerializer.PositionKey), objectLoader.Get(CameraConfigurationSerializer.RotationKey), objectLoader.Get(CameraConfigurationSerializer.ShadowDistanceKey)));
		}

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<Quaternion> RotationKey = new PropertyKey<Quaternion>("Rotation");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> ShadowDistanceKey = new PropertyKey<float>("ShadowDistance");
	}
}
