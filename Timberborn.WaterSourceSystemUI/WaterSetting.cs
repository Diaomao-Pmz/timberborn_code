using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x02000004 RID: 4
	public class WaterSetting
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public WaterSetting(DevModeManager devModeManager, MapEditorMode mapEditorMode, VisualElement root, FloatField inputField, Func<float> getter, bool devModeOnly)
		{
			this._devModeManager = devModeManager;
			this._mapEditorMode = mapEditorMode;
			this.Root = root;
			this._inputField = inputField;
			this._getter = getter;
			this._devModeOnly = devModeOnly;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020FB File Offset: 0x000002FB
		public bool Visible
		{
			get
			{
				return this._devModeManager.Enabled || (!this._devModeOnly && this._mapEditorMode.IsMapEditor);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002121 File Offset: 0x00000321
		public void UpdateState()
		{
			this._inputField.parent.ToggleDisplayStyle(this.Visible);
			if (!this._inputField.IsFocused())
			{
				this._inputField.SetValueWithoutNotify(this._getter());
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000008 RID: 8
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000009 RID: 9
		public readonly FloatField _inputField;

		// Token: 0x0400000A RID: 10
		public readonly Func<float> _getter;

		// Token: 0x0400000B RID: 11
		public readonly bool _devModeOnly;
	}
}
