using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.LifeSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Beavers
{
	// Token: 0x0200000D RID: 13
	public class BeaverLongevity : BaseComponent, IAwakableComponent, ILongevity, IPersistentEntity
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024EC File Offset: 0x000006EC
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024F4 File Offset: 0x000006F4
		public float ExpectedLongevity { get; private set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000024FD File Offset: 0x000006FD
		public BeaverLongevity(IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000250C File Offset: 0x0000070C
		public void Awake()
		{
			this.ExpectedLongevity = this._randomNumberGenerator.Range(BeaverLongevity.MinExpectedLongevity, BeaverLongevity.MaxExpectedLongevity);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002529 File Offset: 0x00000729
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(BeaverLongevity.BeaverLongevityKey).Set(BeaverLongevity.ExpectedLongevityKey, this.ExpectedLongevity);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002548 File Offset: 0x00000748
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(BeaverLongevity.BeaverLongevityKey);
			this.ExpectedLongevity = component.Get(BeaverLongevity.ExpectedLongevityKey);
		}

		// Token: 0x04000013 RID: 19
		public static readonly ComponentKey BeaverLongevityKey = new ComponentKey("BeaverLongevity");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<float> ExpectedLongevityKey = new PropertyKey<float>("ExpectedLongevity");

		// Token: 0x04000015 RID: 21
		public static readonly float MinExpectedLongevity = 0.9f;

		// Token: 0x04000016 RID: 22
		public static readonly float MaxExpectedLongevity = 1.1f;

		// Token: 0x04000018 RID: 24
		public readonly IRandomNumberGenerator _randomNumberGenerator;
	}
}
