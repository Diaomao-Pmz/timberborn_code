using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine.UIElements;

namespace Timberborn.TimeSpeedButtonSystem
{
	// Token: 0x02000007 RID: 7
	public class TimeSpeedButtonGroup : IUpdatableSingleton, IInputProcessor
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021CA File Offset: 0x000003CA
		public TimeSpeedButtonGroup(EventBus eventBus, InputService inputService, TimeSpeedButtonFactory timeSpeedButtonFactory)
		{
			this._eventBus = eventBus;
			this._inputService = inputService;
			this._timeSpeedButtonFactory = timeSpeedButtonFactory;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021FD File Offset: 0x000003FD
		public void Initialize(IEnumerable<Button> buttons, Func<float> currentSpeedGetter, Action<int> speedSetter)
		{
			this._currentSpeedGetter = currentSpeedGetter;
			this._speedSetter = speedSetter;
			this.InitializeButtons(buttons);
			this._eventBus.Register(this);
			this._inputService.AddInputProcessor(this);
			this._enabled = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002234 File Offset: 0x00000434
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				float num = this._currentSpeedGetter();
				if (this._previousSpeed != num)
				{
					this.HighlightButton(num);
					this._previousSpeed = num;
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(TimeSpeedButtonGroup.DecreaseSpeedKey))
			{
				this.DecreaseSpeedIfPossible();
			}
			else if (this._inputService.IsKeyDown(TimeSpeedButtonGroup.IncreaseSpeedKey))
			{
				this.IncreaseSpeedIfPossible();
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A4 File Offset: 0x000004A4
		[OnEvent]
		public void OnSpeedLockChanged(SpeedLockChangedEvent speedLockChangedEvent)
		{
			foreach (TimeSpeedButton timeSpeedButton in this._buttons)
			{
				timeSpeedButton.Button.SetEnabled(!speedLockChangedEvent.IsLocked);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002304 File Offset: 0x00000504
		public void InitializeButtons(IEnumerable<Button> buttons)
		{
			int num = 0;
			foreach (Button button in buttons)
			{
				this._buttons.Add(this._timeSpeedButtonFactory.Create(button, num++, this._speedSetter));
			}
			this._customSpeedButton = this._buttons.Last<TimeSpeedButton>().Button;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002380 File Offset: 0x00000580
		public void DecreaseSpeedIfPossible()
		{
			TimeSpeedButton currentButton = this.GetCurrentButton();
			if (currentButton != null && currentButton != this._buttons[0])
			{
				this.SetSpeed(this._buttons.IndexOf(currentButton) - 1);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023BC File Offset: 0x000005BC
		public void IncreaseSpeedIfPossible()
		{
			TimeSpeedButton currentButton = this.GetCurrentButton();
			if (currentButton != null)
			{
				TimeSpeedButton timeSpeedButton = currentButton;
				List<TimeSpeedButton> buttons = this._buttons;
				if (timeSpeedButton != buttons[buttons.Count - 1])
				{
					this.SetSpeed(this._buttons.IndexOf(currentButton) + 1);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002400 File Offset: 0x00000600
		public TimeSpeedButton GetCurrentButton()
		{
			float currentSpeed = this._currentSpeedGetter();
			return this._buttons.SingleOrDefault((TimeSpeedButton button) => (float)button.TimeSpeed == currentSpeed);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000243C File Offset: 0x0000063C
		public void SetSpeed(int buttonIndex)
		{
			int timeSpeed = this._buttons[buttonIndex].TimeSpeed;
			this._speedSetter(timeSpeed);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002468 File Offset: 0x00000668
		public void HighlightButton(float speed)
		{
			TimeSpeedButton timeSpeedButton = this._buttons.SingleOrDefault((TimeSpeedButton button) => (float)button.TimeSpeed == speed);
			if (timeSpeedButton != null)
			{
				timeSpeedButton.Highlight();
				this._customSpeedButton.RemoveFromClassList(TimeSpeedButtonGroup.CustomButtonClass);
				this._customSpeedButton.text = "";
			}
			else
			{
				this._customSpeedButton.AddToClassList(TimeSpeedButtonGroup.CustomButtonClass);
				this._customSpeedButton.text = "x" + speed.ToString(CultureInfo.InvariantCulture);
			}
			foreach (TimeSpeedButton timeSpeedButton2 in this._buttons)
			{
				if (timeSpeedButton2 != timeSpeedButton)
				{
					timeSpeedButton2.Unhighlight();
				}
			}
		}

		// Token: 0x0400000E RID: 14
		public static readonly string CustomButtonClass = "speed-button--custom";

		// Token: 0x0400000F RID: 15
		public static readonly string DecreaseSpeedKey = "DecreaseSpeed";

		// Token: 0x04000010 RID: 16
		public static readonly string IncreaseSpeedKey = "IncreaseSpeed";

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public readonly InputService _inputService;

		// Token: 0x04000013 RID: 19
		public readonly TimeSpeedButtonFactory _timeSpeedButtonFactory;

		// Token: 0x04000014 RID: 20
		public readonly List<TimeSpeedButton> _buttons = new List<TimeSpeedButton>();

		// Token: 0x04000015 RID: 21
		public Button _customSpeedButton;

		// Token: 0x04000016 RID: 22
		public float _previousSpeed = -1f;

		// Token: 0x04000017 RID: 23
		public Func<float> _currentSpeedGetter;

		// Token: 0x04000018 RID: 24
		public Action<int> _speedSetter;

		// Token: 0x04000019 RID: 25
		public bool _enabled;
	}
}
