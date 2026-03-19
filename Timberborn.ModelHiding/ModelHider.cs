using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000D RID: 13
	public class ModelHider : ILoadableSingleton, IModelAdder
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002690 File Offset: 0x00000890
		public ModelHider(EventBus eventBus, ILevelVisibilityService levelVisibilityService, MapSize mapSize, HidableModels hidableModels, UndergroundModelHider undergroundModelHider, UncoveredModelHider uncoveredModelHider, FloorModelHider floorModelHider)
		{
			this._eventBus = eventBus;
			this._levelVisibilityService = levelVisibilityService;
			this._mapSize = mapSize;
			this._hidableModels = hidableModels;
			this._undergroundModelHider = undergroundModelHider;
			this._uncoveredModelHider = uncoveredModelHider;
			this._floorModelHider = floorModelHider;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026E3 File Offset: 0x000008E3
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026F4 File Offset: 0x000008F4
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			BlockObjectModelController component = entityInitializedEvent.Entity.GetComponent<BlockObjectModelController>();
			if (component != null)
			{
				this.AddModel(component);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002718 File Offset: 0x00000918
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			BlockObjectModelController component = entityDeletedEvent.Entity.GetComponent<BlockObjectModelController>();
			if (component != null)
			{
				this.RemoveModel(component);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000273C File Offset: 0x0000093C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			if (!blockObject.GetComponent<EntityComponent>().Deleted)
			{
				BlockObjectModelController component = blockObject.GetComponent<BlockObjectModelController>();
				if (component != null)
				{
					this.AddModel(component);
					this.UpdateLevelsWithAnythingHidable();
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002774 File Offset: 0x00000974
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			int oldMaxVisibleLevel = maxVisibleLevelChangedEvent.OldMaxVisibleLevel;
			int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
			this.UpdateVisibility(oldMaxVisibleLevel, maxVisibleLevel);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000279C File Offset: 0x0000099C
		public void AddModel(BlockObjectModelController model)
		{
			if (this.FitsInMap(model))
			{
				ModelHider.ResetModel(model);
				if (!this._levelVisibilityService.BlockIsVisible(model.BlockObject.CoordinatesAtBaseZ))
				{
					model.BlockModel();
				}
				this._hidableModels.Add(model);
				this._uncoveredModelHider.ShowModelIfPossible(model);
				this._undergroundModelHider.ShowModelIfPossible(model);
				this._floorModelHider.ShowModelIfPossible(model);
				this.UpdateLevelsWithAnythingHidable();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000280C File Offset: 0x00000A0C
		public void RemoveModel(BlockObjectModelController model)
		{
			if (this.FitsInMap(model))
			{
				ModelHider.ResetModel(model);
				this._hidableModels.Remove(model);
				this.UpdateLevelsWithAnythingHidable();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002830 File Offset: 0x00000A30
		public bool FitsInMap(BlockObjectModelController model)
		{
			return model.BlockObject.GetTopLevel() <= this._mapSize.TotalSize.z;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002860 File Offset: 0x00000A60
		public static void ResetModel(BlockObjectModelController model)
		{
			model.UnblockModel();
			model.HideUncoveredModel();
			model.HideUndergroundModel();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002874 File Offset: 0x00000A74
		public void UpdateLevelsWithAnythingHidable()
		{
			int minLevel;
			int maxLevel;
			if (this._hidableModels.TryGetHidableRange(out minLevel, out maxLevel))
			{
				this._levelVisibilityService.SetLevelsWithAnythingHidable(minLevel, maxLevel);
				return;
			}
			this._levelVisibilityService.ResetLevelsWithAnythingHidable();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028AC File Offset: 0x00000AAC
		public void UpdateVisibility(int oldLevel, int newLevel)
		{
			int minLevel = Math.Max(0, Math.Min(oldLevel, newLevel) - 1);
			int maxLevel = Math.Min(this._mapSize.TotalSize.z, Math.Max(oldLevel, newLevel) + 1);
			this.UpdateBaseVisibility(minLevel, maxLevel, this._modelsToUnblock);
			this._undergroundModelHider.UpdateVisibility(minLevel, maxLevel, this._modelsToUnblock);
			this._uncoveredModelHider.UpdateVisibility(minLevel, maxLevel);
			this._floorModelHider.UpdateVisibility(minLevel, maxLevel, this._modelsToUnblock);
			this.UpdateModelBlockage(minLevel, maxLevel);
			this._modelsToUnblock.Clear();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002940 File Offset: 0x00000B40
		public void UpdateBaseVisibility(int minLevel, int maxLevel, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			for (int i = minLevel; i <= maxLevel; i++)
			{
				foreach (BlockObjectModelController blockObjectModelController in this._hidableModels.ModelsAt(i))
				{
					BlockObject blockObject = blockObjectModelController.BlockObject;
					if (this._levelVisibilityService.BlockIsVisible(blockObject.CoordinatesAtBaseZ))
					{
						modelsToUnblock.Add(blockObjectModelController);
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void UpdateModelBlockage(int minLevel, int maxLevel)
		{
			for (int i = minLevel; i <= maxLevel; i++)
			{
				foreach (BlockObjectModelController blockObjectModelController in this._hidableModels.ModelsAt(i))
				{
					if (this._modelsToUnblock.Contains(blockObjectModelController))
					{
						blockObjectModelController.UnblockModel();
					}
					else
					{
						blockObjectModelController.BlockModel();
					}
				}
			}
		}

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000013 RID: 19
		public readonly MapSize _mapSize;

		// Token: 0x04000014 RID: 20
		public readonly HidableModels _hidableModels;

		// Token: 0x04000015 RID: 21
		public readonly UndergroundModelHider _undergroundModelHider;

		// Token: 0x04000016 RID: 22
		public readonly UncoveredModelHider _uncoveredModelHider;

		// Token: 0x04000017 RID: 23
		public readonly FloorModelHider _floorModelHider;

		// Token: 0x04000018 RID: 24
		public readonly HashSet<BlockObjectModelController> _modelsToUnblock = new HashSet<BlockObjectModelController>();
	}
}
