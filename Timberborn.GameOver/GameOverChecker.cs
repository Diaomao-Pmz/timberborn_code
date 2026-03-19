using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Population;
using Timberborn.Reproduction;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.GameOver
{
	// Token: 0x02000004 RID: 4
	public class GameOverChecker : IGameOverChecker, ILoadableSingleton, ITickableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GameOverChecker(EventBus eventBus, EntityComponentRegistry entityComponentRegistry, EntityRegistry entityRegistry, PopulationService populationService, GameOverDisabler gameOverDisabler)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
			this._entityRegistry = entityRegistry;
			this._populationService = populationService;
			this._gameOverDisabler = gameOverDisabler;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EB File Offset: 0x000002EB
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F9 File Offset: 0x000002F9
		public void PostLoad()
		{
			this.CheckGameEndedState();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F9 File Offset: 0x000002F9
		[OnEvent]
		public void OnNewGameInitialized(NewGameInitializedEvent newGameInitializedEvent)
		{
			this.CheckGameEndedState();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002104 File Offset: 0x00000304
		public void Tick()
		{
			if (!this._gameEnded && !this._gameOverDisabler.Disabled)
			{
				this._ticksElapsed = (this.IsGameOver() ? (this._ticksElapsed + 1) : 0);
				if (this._ticksElapsed == GameOverChecker.FailTickDelay)
				{
					this._eventBus.Post(new GameOverEvent());
					this._gameEnded = true;
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
		public bool IsGameOver()
		{
			if (this._populationService.AllDead)
			{
				if (this._entityRegistry.Entities.Count((EntityComponent entity) => entity.GetComponent<Character>()) == 0)
				{
					return !this.PhoenixProtocolActive();
				}
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BF File Offset: 0x000003BF
		public void CheckGameEndedState()
		{
			this._gameEnded = this._populationService.AllDead;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D4 File Offset: 0x000003D4
		public bool PhoenixProtocolActive()
		{
			using (IEnumerator<BreedingPod> enumerator = this._entityComponentRegistry.GetEnabled<BreedingPod>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasResourcesToFinish())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000006 RID: 6
		public static readonly int FailTickDelay = 10;

		// Token: 0x04000007 RID: 7
		public readonly EventBus _eventBus;

		// Token: 0x04000008 RID: 8
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000009 RID: 9
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x0400000A RID: 10
		public readonly PopulationService _populationService;

		// Token: 0x0400000B RID: 11
		public readonly GameOverDisabler _gameOverDisabler;

		// Token: 0x0400000C RID: 12
		public bool _gameEnded;

		// Token: 0x0400000D RID: 13
		public int _ticksElapsed;
	}
}
