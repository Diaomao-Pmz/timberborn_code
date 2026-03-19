using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.HttpApiSystem;
using UnityEngine.UIElements;

namespace Timberborn.HttpApiSystemUI
{
	// Token: 0x0200000A RID: 10
	public class HttpLeverFragment : IEntityPanelFragment
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000296F File Offset: 0x00000B6F
		public HttpLeverFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002980 File Offset: 0x00000B80
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/HttpLeverFragment");
			this._switchOnUrlTextField = UQueryExtensions.Q<TextField>(this._root, "SwitchOnUrl", null);
			this._switchOffUrlTextField = UQueryExtensions.Q<TextField>(this._root, "SwitchOffUrl", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029E3 File Offset: 0x00000BE3
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<HttpLever>(out this._httpLever))
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029FF File Offset: 0x00000BFF
		public void UpdateFragment()
		{
			if (this._httpLever)
			{
				this._switchOnUrlTextField.SetValueWithoutNotify(this._httpLever.SwitchOnUrl);
				this._switchOffUrlTextField.SetValueWithoutNotify(this._httpLever.SwitchOffUrl);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A3A File Offset: 0x00000C3A
		public void ClearFragment()
		{
			this._httpLever = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000030 RID: 48
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000031 RID: 49
		public VisualElement _root;

		// Token: 0x04000032 RID: 50
		public TextField _switchOnUrlTextField;

		// Token: 0x04000033 RID: 51
		public TextField _switchOffUrlTextField;

		// Token: 0x04000034 RID: 52
		public HttpLever _httpLever;
	}
}
