using System;
using Timberborn.Common;
using Timberborn.Debugging;
using Timberborn.Modding;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameScene
{
	// Token: 0x02000004 RID: 4
	public class DateSalter : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DateSalter(EventBus eventBus, IRandomNumberGenerator randomNumberGenerator, DevModeManager devModeManager, ISingletonLoader singletonLoader)
		{
			this._eventBus = eventBus;
			this._randomNumberGenerator = randomNumberGenerator;
			this._devModeManager = devModeManager;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(DateSalter.DateSalterKey, out objectLoader))
			{
				this._saltDateIdWithOddNumber = DateSalter.IsOddNumber(objectLoader.Get(DateSalter.DateSaltedIdKey));
				this._saltTimeIdWithOddNumber = DateSalter.IsOddNumber(objectLoader.Get(DateSalter.TimeSaltedIdKey));
			}
			if (this._devModeManager.Enabled)
			{
				this._saltDateIdWithOddNumber = true;
			}
			if (ModdedState.IsModded)
			{
				this._saltTimeIdWithOddNumber = true;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215F File Offset: 0x0000035F
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(DateSalter.DateSalterKey);
			singleton.Set(DateSalter.DateSaltedIdKey, this.GenerateRandomNumber(this._saltDateIdWithOddNumber));
			singleton.Set(DateSalter.TimeSaltedIdKey, this.GenerateRandomNumber(this._saltTimeIdWithOddNumber));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002199 File Offset: 0x00000399
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			if (devModeToggledEvent.Enabled)
			{
				this._saltDateIdWithOddNumber = true;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		public int GenerateRandomNumber(bool odd)
		{
			int num = this._randomNumberGenerator.Range(0, 1000000);
			if (odd != DateSalter.IsOddNumber(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D9 File Offset: 0x000003D9
		public static bool IsOddNumber(int number)
		{
			return number % 2 == 1;
		}

		// Token: 0x04000006 RID: 6
		public static readonly SingletonKey DateSalterKey = new SingletonKey("DateSaltService");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<int> DateSaltedIdKey = new PropertyKey<int>("DateSaltedId");

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<int> TimeSaltedIdKey = new PropertyKey<int>("TimeSaltedId");

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000C RID: 12
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000D RID: 13
		public bool _saltDateIdWithOddNumber;

		// Token: 0x0400000E RID: 14
		public bool _saltTimeIdWithOddNumber;
	}
}
