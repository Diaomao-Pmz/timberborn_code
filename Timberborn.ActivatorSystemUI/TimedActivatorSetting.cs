using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x02000008 RID: 8
	public class TimedActivatorSetting
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002284 File Offset: 0x00000484
		public VisualElement Root { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000228C File Offset: 0x0000048C
		public TimedActivatorSetting(DevModeManager devModeManager, MapEditorMode mapEditorMode, VisualElement root, FloatField floatField, Func<float> getter)
		{
			this._devModeManager = devModeManager;
			this._mapEditorMode = mapEditorMode;
			this.Root = root;
			this._floatField = floatField;
			this._getter = getter;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B9 File Offset: 0x000004B9
		public void UpdateState()
		{
			this.Root.ToggleDisplayStyle(this.Visible);
			if (!this._floatField.IsFocused())
			{
				this._floatField.SetValueWithoutNotify(this._getter());
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022EF File Offset: 0x000004EF
		public bool Visible
		{
			get
			{
				return this._devModeManager.Enabled || this._mapEditorMode.IsMapEditor;
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000014 RID: 20
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000015 RID: 21
		public readonly FloatField _floatField;

		// Token: 0x04000016 RID: 22
		public readonly Func<float> _getter;
	}
}
