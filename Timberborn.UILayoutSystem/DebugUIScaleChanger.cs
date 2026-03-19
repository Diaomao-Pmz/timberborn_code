using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.UILayoutSystem
{
	// Token: 0x02000004 RID: 4
	public class DebugUIScaleChanger : IUpdatableSingleton, IDevModule
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DebugUIScaleChanger(InputService inputService, UIScaler uiScaler, UISettings uiSettings)
		{
			this._inputService = inputService;
			this._uiScaler = uiScaler;
			this._uiSettings = uiSettings;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void UpdateSingleton()
		{
			if (this._inputService.IsKeyHeld(DebugUIScaleChanger.IncreaseUIScaleKey))
			{
				this.IncreaseUIScale();
				return;
			}
			if (this._inputService.IsKeyHeld(DebugUIScaleChanger.DecreaseUIScaleKey))
			{
				this.DecreaseUIScale();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("UI Scale: Increase", DebugUIScaleChanger.IncreaseUIScaleKey, new Action(this.IncreaseUIScale))).AddMethod(DevMethod.CreateBindable("UI Scale: Decrease", DebugUIScaleChanger.DecreaseUIScaleKey, new Action(this.DecreaseUIScale))).Build();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002167 File Offset: 0x00000367
		public void IncreaseUIScale()
		{
			this.ChangeUIScaleSetting(UISettings.UIScaleStep);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002174 File Offset: 0x00000374
		public void DecreaseUIScale()
		{
			this.ChangeUIScaleSetting(-UISettings.UIScaleStep);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002184 File Offset: 0x00000384
		public void ChangeUIScaleSetting(float change)
		{
			this._uiSettings.UIScaleFactor = this._uiScaler.ClampScaleFactor(this._uiSettings.UIScaleFactor + change);
			Debug.Log(string.Format("New UIScale: {0:P0}", this._uiSettings.UIScaleFactor));
		}

		// Token: 0x04000006 RID: 6
		public static readonly string IncreaseUIScaleKey = "IncreaseUIScale";

		// Token: 0x04000007 RID: 7
		public static readonly string DecreaseUIScaleKey = "DecreaseUIScale";

		// Token: 0x04000008 RID: 8
		public readonly InputService _inputService;

		// Token: 0x04000009 RID: 9
		public readonly UIScaler _uiScaler;

		// Token: 0x0400000A RID: 10
		public readonly UISettings _uiSettings;
	}
}
