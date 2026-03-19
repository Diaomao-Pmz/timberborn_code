using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.TitleScreenUI
{
	// Token: 0x02000007 RID: 7
	public class TitleScreenFooter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000227F File Offset: 0x0000047F
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002287 File Offset: 0x00000487
		public VisualElement Root { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		public TitleScreenFooter(EventBus eventBus, DevModeManager devModeManager)
		{
			this._eventBus = eventBus;
			this._devModeManager = devModeManager;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A8 File Offset: 0x000004A8
		public void Initialize(VisualElement parent)
		{
			this.Root = UQueryExtensions.Q<VisualElement>(parent, "Footer", null);
			UQueryExtensions.Q<Label>(this.Root, "GameVersion", null).text = GameVersions.CurrentVersion.Formatted;
			this._devModeAlert = UQueryExtensions.Q<Label>(this.Root, "DevModeAlert", null);
			this.UpdateDevModeAlert(false);
			this._eventBus.Register(this);
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002320 File Offset: 0x00000520
		public void Show()
		{
			this.Root.ToggleDisplayStyle(true);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000232E File Offset: 0x0000052E
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			this.UpdateDevModeAlert(this._devModeManager.Enabled);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002341 File Offset: 0x00000541
		public void UpdateDevModeAlert(bool newState)
		{
			this._devModeAlert.ToggleDisplayStyle(!Application.isEditor && newState);
		}

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000017 RID: 23
		public VisualElement _devModeAlert;
	}
}
