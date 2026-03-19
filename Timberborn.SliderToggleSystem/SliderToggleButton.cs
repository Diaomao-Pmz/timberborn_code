using System;
using UnityEngine.UIElements;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x02000005 RID: 5
	public class SliderToggleButton
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		public SliderToggleButton(Button button, Func<SliderToggleState> stateGetter, Action clickAction)
		{
			this._button = button;
			this._stateGetter = stateGetter;
			this._clickAction = clickAction;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002255 File Offset: 0x00000455
		public SliderToggleState CurrentState
		{
			get
			{
				return this._stateGetter();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002264 File Offset: 0x00000464
		public void Update()
		{
			switch (this.CurrentState)
			{
			case SliderToggleState.None:
				this._button.RemoveFromClassList(SliderToggleButton.ActiveButtonClass);
				this._button.RemoveFromClassList(SliderToggleButton.LockedButtonClass);
				return;
			case SliderToggleState.Active:
				this._button.AddToClassList(SliderToggleButton.ActiveButtonClass);
				this._button.RemoveFromClassList(SliderToggleButton.LockedButtonClass);
				return;
			case SliderToggleState.Locked:
				this._button.AddToClassList(SliderToggleButton.LockedButtonClass);
				return;
			case SliderToggleState.Unclickable:
				this._button.SetEnabled(false);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F5 File Offset: 0x000004F5
		public void Select()
		{
			this._clickAction();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002302 File Offset: 0x00000502
		public void Clear()
		{
			this._button.SetEnabled(true);
			this._button.RemoveFromClassList(SliderToggleButton.LockedButtonClass);
			this._button.RemoveFromClassList(SliderToggleButton.ActiveButtonClass);
		}

		// Token: 0x0400000B RID: 11
		public static readonly string ActiveButtonClass = "slider-toggle__element--active";

		// Token: 0x0400000C RID: 12
		public static readonly string LockedButtonClass = "slider-toggle--locked";

		// Token: 0x0400000D RID: 13
		public readonly Button _button;

		// Token: 0x0400000E RID: 14
		public readonly Func<SliderToggleState> _stateGetter;

		// Token: 0x0400000F RID: 15
		public readonly Action _clickAction;
	}
}
