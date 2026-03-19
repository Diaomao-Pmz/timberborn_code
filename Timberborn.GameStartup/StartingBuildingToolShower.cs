using System;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000015 RID: 21
	public class StartingBuildingToolShower : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002988 File Offset: 0x00000B88
		public StartingBuildingToolShower(EventBus eventBus, InputService inputService, StartingBuildingInitializer startingBuildingInitializer, ToolService toolService, StartingBuildingSpawner startingBuildingSpawner, StartingBuildingToolFactory startingBuildingToolFactory, ISettlementNamePromptShower settlementNamePromptShower)
		{
			this._eventBus = eventBus;
			this._inputService = inputService;
			this._startingBuildingInitializer = startingBuildingInitializer;
			this._toolService = toolService;
			this._startingBuildingSpawner = startingBuildingSpawner;
			this._startingBuildingToolFactory = startingBuildingToolFactory;
			this._settlementNamePromptShower = settlementNamePromptShower;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void Load()
		{
			PlaceableBlockObjectSpec spec = this._startingBuildingSpawner.StartingBuildingTemplateSpec.GetSpec<PlaceableBlockObjectSpec>();
			this._buildingPlacementTool = this._startingBuildingToolFactory.Create(spec);
			this._eventBus.Register(this);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A04 File Offset: 0x00000C04
		[OnEvent]
		public void OnRelocateSettlement(RelocateSettlementEvent relocateSettlementEvent)
		{
			Building startingBuilding = this._startingBuildingSpawner.StartingBuilding;
			if (startingBuilding)
			{
				BlockObject component = startingBuilding.GetComponent<BlockObject>();
				this._previousBuildingPlacement = new Placement?(component.Placement);
				this._startingBuildingSpawner.DeleteStartingBuilding();
			}
			this._toolService.SwitchTool(this._buildingPlacementTool);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A65 File Offset: 0x00000C65
		[OnEvent]
		public void OnStartingBuildingPlacedEvent(StartingBuildingPlacedEvent startingBuildingPlacedEvent)
		{
			this._inputService.RemoveInputProcessor(this);
			this.Place(new Placement?(startingBuildingPlacedEvent.Placement));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A84 File Offset: 0x00000C84
		[OnEvent]
		public void OnResetStartingLocation(ResetStartingLocationEvent resetStartingLocationEvent)
		{
			this._previousBuildingPlacement = null;
			this._startingBuildingSpawner.DeleteStartingBuilding();
			this.Place(this._startingBuildingInitializer.InitialPlacement);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002ABA File Offset: 0x00000CBA
		public bool ProcessInput()
		{
			if (this._inputService.Cancel)
			{
				this.PlaceStartingBuildingOnPreviousCoordinates();
				this._inputService.RemoveInputProcessor(this);
				return true;
			}
			return false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002ADE File Offset: 0x00000CDE
		public void PlaceStartingBuildingOnPreviousCoordinates()
		{
			this.Place(this._previousBuildingPlacement);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AEC File Offset: 0x00000CEC
		public void Place(Placement? placement)
		{
			this._startingBuildingSpawner.Place(placement);
			bool includeResetStartLocationLink = placement != this._startingBuildingInitializer.InitialPlacement;
			this._settlementNamePromptShower.PromptDisallowingCancelling(includeResetStartLocationLink);
		}

		// Token: 0x0400003D RID: 61
		public readonly EventBus _eventBus;

		// Token: 0x0400003E RID: 62
		public readonly InputService _inputService;

		// Token: 0x0400003F RID: 63
		public readonly StartingBuildingInitializer _startingBuildingInitializer;

		// Token: 0x04000040 RID: 64
		public readonly ToolService _toolService;

		// Token: 0x04000041 RID: 65
		public readonly StartingBuildingSpawner _startingBuildingSpawner;

		// Token: 0x04000042 RID: 66
		public readonly StartingBuildingToolFactory _startingBuildingToolFactory;

		// Token: 0x04000043 RID: 67
		public readonly ISettlementNamePromptShower _settlementNamePromptShower;

		// Token: 0x04000044 RID: 68
		public BlockObjectTool _buildingPlacementTool;

		// Token: 0x04000045 RID: 69
		public Placement? _previousBuildingPlacement;
	}
}
