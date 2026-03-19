using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSpeedButtonSystem;
using Timberborn.TimeSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000006 RID: 6
	public class SpeedControlPanel : ILoadableSingleton, IDevModule, IInputProcessor
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002338 File Offset: 0x00000538
		public SpeedControlPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, SpeedManager speedManager, Ticker ticker, InputService inputService, TimeSpeedButtonGroup timeSpeedButtonGroup, EventBus eventBus, ITooltipRegistrar tooltipRegistrar)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._speedManager = speedManager;
			this._ticker = ticker;
			this._inputService = inputService;
			this._timeSpeedButtonGroup = timeSpeedButtonGroup;
			this._eventBus = eventBus;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002394 File Offset: 0x00000594
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Speed: x0.25", SpeedControlPanel.SlowDevGameSpeedKey, delegate
			{
				this.SetSpeed(SpeedControlPanel.SlowDevGameSpeed);
			})).AddMethod(DevMethod.CreateBindable("Speed: x30", SpeedControlPanel.FastDevGameSpeedKey, delegate
			{
				this.SetSpeed(SpeedControlPanel.FastDevGameSpeed);
			})).AddMethod(DevMethod.CreateBindable("Speed: x99", SpeedControlPanel.SuperFastDevGameSpeedKey, delegate
			{
				this.SetSpeed(SpeedControlPanel.SuperFastDevGameSpeed);
			})).Build();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000240C File Offset: 0x0000060C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/SpeedControlPanel");
			this._eventBus.Register(this);
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Speed0", null), "Speed0");
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Speed1", null), "Speed1");
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Speed3", null), "Speed2");
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Speed7", null), "Speed3");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024C0 File Offset: 0x000006C0
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(SpeedControlPanel.TickOnceKey))
			{
				this.PauseOrTickOnce();
			}
			else if (this._inputService.IsKeyDown(SpeedControlPanel.SlowDevGameSpeedKey))
			{
				this.SetSpeed(SpeedControlPanel.SlowDevGameSpeed);
			}
			else if (this._inputService.IsKeyDown(SpeedControlPanel.FastDevGameSpeedKey))
			{
				this.SetSpeed(SpeedControlPanel.FastDevGameSpeed);
			}
			else if (this._inputService.IsKeyDown(SpeedControlPanel.SuperFastDevGameSpeedKey))
			{
				this.SetSpeed(SpeedControlPanel.SuperFastDevGameSpeed);
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002544 File Offset: 0x00000744
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._timeSpeedButtonGroup.Initialize(UQueryExtensions.Query<Button>(this._root, null, null).Build(), () => this._speedManager.CurrentSpeed, delegate(int speed)
			{
				this.SetSpeed((float)speed);
			});
			this._inputService.AddInputProcessor(this);
			this._uiLayout.AddTopRight(this._root, 2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025AC File Offset: 0x000007AC
		public void PauseOrTickOnce()
		{
			if (this._speedManager.CurrentSpeed == 0f)
			{
				this._ticker.TickOnce();
				return;
			}
			this.SetSpeed(0f);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025D8 File Offset: 0x000007D8
		public void SetSpeed(float timeSpeed)
		{
			if (timeSpeed != 0f)
			{
				this._speedManager.ChangeSpeed(timeSpeed);
				return;
			}
			float currentSpeed = this._speedManager.CurrentSpeed;
			if (currentSpeed == 0f)
			{
				this._speedManager.ChangeSpeed(this._speedBeforePause);
				return;
			}
			this._speedBeforePause = currentSpeed;
			this._speedManager.ChangeSpeed(0f);
		}

		// Token: 0x04000011 RID: 17
		public static readonly float SlowDevGameSpeed = 0.25f;

		// Token: 0x04000012 RID: 18
		public static readonly float FastDevGameSpeed = 30f;

		// Token: 0x04000013 RID: 19
		public static readonly float SuperFastDevGameSpeed = 99f;

		// Token: 0x04000014 RID: 20
		public static readonly string TickOnceKey = "TickOnce";

		// Token: 0x04000015 RID: 21
		public static readonly string SlowDevGameSpeedKey = "SlowDevGameSpeed";

		// Token: 0x04000016 RID: 22
		public static readonly string FastDevGameSpeedKey = "FastDevGameSpeed";

		// Token: 0x04000017 RID: 23
		public static readonly string SuperFastDevGameSpeedKey = "SuperFastDevGameSpeed";

		// Token: 0x04000018 RID: 24
		public readonly UILayout _uiLayout;

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly SpeedManager _speedManager;

		// Token: 0x0400001B RID: 27
		public readonly Ticker _ticker;

		// Token: 0x0400001C RID: 28
		public readonly InputService _inputService;

		// Token: 0x0400001D RID: 29
		public readonly TimeSpeedButtonGroup _timeSpeedButtonGroup;

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000020 RID: 32
		public float _speedBeforePause = 1f;

		// Token: 0x04000021 RID: 33
		public bool _pauseNextTick;

		// Token: 0x04000022 RID: 34
		public VisualElement _root;
	}
}
