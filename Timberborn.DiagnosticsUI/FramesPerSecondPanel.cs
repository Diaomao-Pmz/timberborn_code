using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.Diagnostics;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x02000007 RID: 7
	public class FramesPerSecondPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000218A File Offset: 0x0000038A
		public FramesPerSecondPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, FramesPerSecondCounter framesPerSecondCounter, DevModeManager devModeManager, UISettings uiSettings)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._framesPerSecondCounter = framesPerSecondCounter;
			this._devModeManager = devModeManager;
			this._uiSettings = uiSettings;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		public void Load()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/FramesPerSecondPanel");
			this._fps = UQueryExtensions.Q<Label>(visualElement, "FPS", null);
			this._uiLayout.AddBottomRight(visualElement, 2);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		public void UpdateSingleton()
		{
			bool flag = this._devModeManager.Enabled || this._uiSettings.ShowFps;
			if (flag && this.ValuesUpdated())
			{
				this._fps.text = string.Format("FPS: {0} / {1}", this._lastAverageFramesPerSecond, this._lastMinFramesPerSecond);
			}
			this._fps.ToggleDisplayStyle(flag);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002263 File Offset: 0x00000463
		public bool ValuesUpdated()
		{
			return this.UpdateAverageFramesPerSecond() || this.UpdateMinFramesPerSecond();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002278 File Offset: 0x00000478
		public bool UpdateAverageFramesPerSecond()
		{
			int num = Mathf.RoundToInt(this._framesPerSecondCounter.AverageFramesPerSecond);
			if (num != this._lastAverageFramesPerSecond)
			{
				this._lastAverageFramesPerSecond = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022AC File Offset: 0x000004AC
		public bool UpdateMinFramesPerSecond()
		{
			int num = Mathf.RoundToInt(this._framesPerSecondCounter.MinFramesPerSecond);
			if (num != this._lastMinFramesPerSecond)
			{
				this._lastMinFramesPerSecond = num;
				return true;
			}
			return false;
		}

		// Token: 0x04000007 RID: 7
		public readonly UILayout _uiLayout;

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly FramesPerSecondCounter _framesPerSecondCounter;

		// Token: 0x0400000A RID: 10
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000B RID: 11
		public readonly UISettings _uiSettings;

		// Token: 0x0400000C RID: 12
		public Label _fps;

		// Token: 0x0400000D RID: 13
		public int _lastAverageFramesPerSecond;

		// Token: 0x0400000E RID: 14
		public int _lastMinFramesPerSecond;
	}
}
