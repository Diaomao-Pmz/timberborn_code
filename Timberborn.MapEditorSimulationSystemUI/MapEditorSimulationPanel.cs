using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.InputSystemUI;
using Timberborn.MapEditorSimulationSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSpeedButtonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorSimulationSystemUI
{
	// Token: 0x02000003 RID: 3
	public class MapEditorSimulationPanel : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public MapEditorSimulationPanel(MapEditorSimulation mapEditorSimulation, VisualElementLoader visualElementLoader, UILayout uiLayout, ITooltipRegistrar tooltipRegistrar, TimeSpeedButtonGroup timeSpeedButtonGroup, BindableButtonFactory bindableButtonFactory, InputService inputService)
		{
			this._mapEditorSimulation = mapEditorSimulation;
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._tooltipRegistrar = tooltipRegistrar;
			this._timeSpeedButtonGroup = timeSpeedButtonGroup;
			this._bindableButtonFactory = bindableButtonFactory;
			this._inputService = inputService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/MapEditorSimulationPanel");
			this._tooltipRegistrar.RegisterLocalizable(visualElement, MapEditorSimulationPanel.TooltipLocKey);
			TimeSpeedButtonGroup timeSpeedButtonGroup = this._timeSpeedButtonGroup;
			UQueryBuilder<Button> uqueryBuilder = visualElement.Query(null, null);
			uqueryBuilder = from button in uqueryBuilder
			where button.name.StartsWith("Speed")
			select button;
			timeSpeedButtonGroup.Initialize(uqueryBuilder.Build(), () => (float)this._mapEditorSimulation.SimulationSpeed, new Action<int>(this.SetSpeed));
			Button button2 = visualElement.Q("Reset", null);
			this._bindableButtonFactory.CreateAndBind(button2, MapEditorSimulationPanel.ResetMapEditorSimulationKey, new Action(this.ResetSimulation));
			this._tooltipRegistrar.RegisterLocalizable(button2, MapEditorSimulationPanel.ResetLocKey);
			this._inputService.AddInputProcessor(this);
			this._uiLayout.AddTopRight(visualElement, 1);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021F3 File Offset: 0x000003F3
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(MapEditorSimulationPanel.MapEditorDevSimulationSpeedKey))
			{
				this.SetSpeed(MapEditorSimulationPanel.DevSimulationSpeed);
				return true;
			}
			return false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002218 File Offset: 0x00000418
		private void SetSpeed(int speed)
		{
			if (speed != 0)
			{
				this._mapEditorSimulation.SetSimulationSpeed(speed);
				return;
			}
			int simulationSpeed = this._mapEditorSimulation.SimulationSpeed;
			if (simulationSpeed == 0)
			{
				this._mapEditorSimulation.SetSimulationSpeed(this._speedBeforePause);
				return;
			}
			this._speedBeforePause = simulationSpeed;
			this._mapEditorSimulation.SetSimulationSpeed(0);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002269 File Offset: 0x00000469
		private void ResetSimulation()
		{
			this._mapEditorSimulation.ResetSimulation();
		}

		// Token: 0x04000001 RID: 1
		private static readonly string TooltipLocKey = "MapEditor.SimulationControls.Tooltip";

		// Token: 0x04000002 RID: 2
		private static readonly string ResetLocKey = "MapEditor.SimulationControls.Reset";

		// Token: 0x04000003 RID: 3
		private static readonly string ResetMapEditorSimulationKey = "ResetMapEditorSimulation";

		// Token: 0x04000004 RID: 4
		private static readonly string MapEditorDevSimulationSpeedKey = "MapEditorDevSimulationSpeed";

		// Token: 0x04000005 RID: 5
		private static readonly int DevSimulationSpeed = 50;

		// Token: 0x04000006 RID: 6
		private readonly MapEditorSimulation _mapEditorSimulation;

		// Token: 0x04000007 RID: 7
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		private readonly UILayout _uiLayout;

		// Token: 0x04000009 RID: 9
		private readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000A RID: 10
		private readonly TimeSpeedButtonGroup _timeSpeedButtonGroup;

		// Token: 0x0400000B RID: 11
		private readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400000C RID: 12
		private readonly InputService _inputService;

		// Token: 0x0400000D RID: 13
		private int _speedBeforePause = 1;
	}
}
