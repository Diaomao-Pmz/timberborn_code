using System;
using System.Collections.Generic;
using Timberborn.EntitySystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000A RID: 10
	public class CharacterModelHider : ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000025FC File Offset: 0x000007FC
		public CharacterModelHider(EventBus eventBus, ILevelVisibilityService levelVisibilityService)
		{
			this._eventBus = eventBus;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000261D File Offset: 0x0000081D
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000262B File Offset: 0x0000082B
		public void Tick()
		{
			if (!this._levelVisibilityService.LevelIsAtMax)
			{
				this.UpdateVisibilityOfModels();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002640 File Offset: 0x00000840
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			CharacterModel component = entityInitializedEvent.Entity.GetComponent<CharacterModel>();
			if (component)
			{
				this._characters.Add(component);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002670 File Offset: 0x00000870
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			CharacterModel component = entityDeletedEvent.Entity.GetComponent<CharacterModel>();
			if (component)
			{
				this._characters.Remove(component);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000269E File Offset: 0x0000089E
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this.UpdateVisibilityOfModels();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026A8 File Offset: 0x000008A8
		public void UpdateVisibilityOfModels()
		{
			foreach (CharacterModel characterModel in this._characters)
			{
				Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(characterModel.Transform.position);
				if (this._levelVisibilityService.BlockIsVisible(coordinates))
				{
					characterModel.UnblockModel();
				}
				else
				{
					characterModel.BlockModel();
				}
			}
		}

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000020 RID: 32
		public readonly List<CharacterModel> _characters = new List<CharacterModel>();
	}
}
