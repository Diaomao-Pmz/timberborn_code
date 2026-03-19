using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200000C RID: 12
	public class DelayedButtonEnabler : IUpdatableSingleton
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023F0 File Offset: 0x000005F0
		public void Add(Button button)
		{
			DelayedButtonEnabler.DelayedButton item = new DelayedButtonEnabler.DelayedButton(button, Time.unscaledTime + (float)DelayedButtonEnabler.DelayInSeconds);
			this._delayedButtons.Add(item);
			button.SetEnabled(false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002424 File Offset: 0x00000624
		public void UpdateSingleton()
		{
			for (int i = this._delayedButtons.Count - 1; i >= 0; i--)
			{
				DelayedButtonEnabler.DelayedButton delayedButton = this._delayedButtons[i];
				if (delayedButton.EnableTime - Time.unscaledTime <= 0f)
				{
					delayedButton.Button.SetEnabled(true);
					this._delayedButtons.RemoveAt(i);
				}
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly int DelayInSeconds = 2;

		// Token: 0x04000011 RID: 17
		public readonly List<DelayedButtonEnabler.DelayedButton> _delayedButtons = new List<DelayedButtonEnabler.DelayedButton>();

		// Token: 0x0200000D RID: 13
		public readonly struct DelayedButton
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001A RID: 26 RVA: 0x0000249E File Offset: 0x0000069E
			public Button Button { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000024A6 File Offset: 0x000006A6
			public float EnableTime { get; }

			// Token: 0x0600001C RID: 28 RVA: 0x000024AE File Offset: 0x000006AE
			public DelayedButton(Button button, float enableTime)
			{
				this.Button = button;
				this.EnableTime = enableTime;
			}
		}
	}
}
