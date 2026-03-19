using System;
using Timberborn.AchievementSystem;
using Timberborn.GameOver;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200004E RID: 78
	public class SurviveBadtideAchievement : Achievement
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00004B1F File Offset: 0x00002D1F
		public SurviveBadtideAchievement(EventBus eventBus, IGameOverChecker gameOverChecker)
		{
			this._eventBus = eventBus;
			this._gameOverChecker = gameOverChecker;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004B35 File Offset: 0x00002D35
		public override string Id
		{
			get
			{
				return "SURVIVE_BADTIDE";
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004B3C File Offset: 0x00002D3C
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (!this._gameOverChecker.IsGameOver() && hazardousWeatherEndedEvent.HazardousWeather is BadtideWeather)
			{
				base.Unlock();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004B5E File Offset: 0x00002D5E
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004B6C File Offset: 0x00002D6C
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x040000AB RID: 171
		public readonly EventBus _eventBus;

		// Token: 0x040000AC RID: 172
		public readonly IGameOverChecker _gameOverChecker;
	}
}
