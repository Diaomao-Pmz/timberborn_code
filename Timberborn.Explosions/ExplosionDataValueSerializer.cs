using System;
using Timberborn.Persistence;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x0200000C RID: 12
	public class ExplosionDataValueSerializer : IValueSerializer<ExplosionData>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A11 File Offset: 0x00000C11
		public void Serialize(ExplosionData value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(ExplosionDataValueSerializer.RadiusKey, value.Radius);
			objectSaver.Set(ExplosionDataValueSerializer.CenterKey, value.Center);
			objectSaver.Set(ExplosionDataValueSerializer.CurrentExplosionRadiusKey, value.CurrentExplosionRadius);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A4C File Offset: 0x00000C4C
		public Obsoletable<ExplosionData> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			float radius = objectLoader.Get(ExplosionDataValueSerializer.RadiusKey);
			Vector3 center = objectLoader.Get(ExplosionDataValueSerializer.CenterKey);
			int currentExplosionRadius = objectLoader.Get(ExplosionDataValueSerializer.CurrentExplosionRadiusKey);
			return new ExplosionData(radius, center, currentExplosionRadius);
		}

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<float> RadiusKey = new PropertyKey<float>("Radius");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<Vector3> CenterKey = new PropertyKey<Vector3>("Center");

		// Token: 0x04000025 RID: 37
		public static readonly PropertyKey<int> CurrentExplosionRadiusKey = new PropertyKey<int>("CurrentExplosionRadius");
	}
}
