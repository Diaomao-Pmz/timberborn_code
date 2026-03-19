using System;
using Timberborn.AchievementSystem;
using Timberborn.GameOver;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200004F RID: 79
	public class SurviveDroughtAchievement : Achievement
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00004B7A File Offset: 0x00002D7A
		public SurviveDroughtAchievement(EventBus eventBus, IGameOverChecker gameOverChecker)
		{
			this._eventBus = eventBus;
			this._gameOverChecker = gameOverChecker;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004B90 File Offset: 0x00002D90
		public override string Id
		{
			get
			{
				return "SURVIVE_DROUGHT";
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004B97 File Offset: 0x00002D97
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (!this._gameOverChecker.IsGameOver() && hazardousWeatherEndedEvent.HazardousWeather is DroughtWeather)
			{
				base.Unlock();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004BB9 File Offset: 0x00002DB9
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x040000AD RID: 173
		public readonly EventBus _eventBus;

		// Token: 0x040000AE RID: 174
		public readonly IGameOverChecker _gameOverChecker;
	}
}
