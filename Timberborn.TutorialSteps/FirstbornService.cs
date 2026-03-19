using System;
using Timberborn.Beavers;
using Timberborn.SingletonSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002A RID: 42
	public class FirstbornService : ILoadableSingleton
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004938 File Offset: 0x00002B38
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00004940 File Offset: 0x00002B40
		public bool FirstbornBorn { get; private set; }

		// Token: 0x0600012D RID: 301 RVA: 0x00004949 File Offset: 0x00002B49
		public FirstbornService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004958 File Offset: 0x00002B58
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004966 File Offset: 0x00002B66
		[OnEvent]
		public void OnBeaverBorn(BeaverBornEvent beaverBornEvent)
		{
			this.FirstbornBorn = true;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0400008D RID: 141
		public readonly EventBus _eventBus;
	}
}
