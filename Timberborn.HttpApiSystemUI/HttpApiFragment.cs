using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.HttpApiSystem;
using Timberborn.Localization;
using Timberborn.WebNavigation;
using UnityEngine.UIElements;

namespace Timberborn.HttpApiSystemUI
{
	// Token: 0x02000006 RID: 6
	public class HttpApiFragment : IEntityPanelFragment
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000248D File Offset: 0x0000068D
		public HttpApiFragment(VisualElementLoader visualElementLoader, ILoc loc, HttpApi httpApi, UrlOpener urlOpener, HttpWebhookRegistry httpWebhookRegistry, DialogBoxShower dialogBoxShower)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._httpApi = httpApi;
			this._urlOpener = urlOpener;
			this._httpWebhookRegistry = httpWebhookRegistry;
			this._dialogBoxShower = dialogBoxShower;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024C4 File Offset: 0x000006C4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/HttpApiFragment");
			this._root.ToggleDisplayStyle(false);
			this._startStopButton = UQueryExtensions.Q<Button>(this._root, "StartStop", null);
			this._portWrapper = UQueryExtensions.Q<VisualElement>(this._root, "PortWrapper", null);
			this._portValue = UQueryExtensions.Q<TextField>(this._root, "PortValue", null);
			this._openBrowserButton = UQueryExtensions.Q<Button>(this._root, "OpenBrowser", null);
			this._urlLabel = UQueryExtensions.Q<Label>(this._root, "Url", null);
			this._startStopButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleServer), 0);
			this._openBrowserButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OpenBrowser), 0);
			this._portValue.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.PortChanged), 0);
			return this._root;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025B4 File Offset: 0x000007B4
		public void ShowFragment(BaseComponent entity)
		{
			this._httpApiController = entity.GetComponent<HttpApiController>();
			this.UpdatePortTextField();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025C8 File Offset: 0x000007C8
		public void ClearFragment()
		{
			this._httpApiController = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025E0 File Offset: 0x000007E0
		public void UpdateFragment()
		{
			if (this._httpApiController && this._httpApiController.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				bool isRunning = this._httpApi.IsRunning;
				this._startStopButton.text = (isRunning ? this._loc.T(HttpApiFragment.StopApiLocKey) : this._loc.T(HttpApiFragment.StartApiLocKey));
				this._portWrapper.ToggleDisplayStyle(!isRunning);
				this._openBrowserButton.ToggleDisplayStyle(isRunning);
				this._urlLabel.ToggleDisplayStyle(isRunning);
				if (isRunning)
				{
					this._urlLabel.text = this._httpApi.Url;
					return;
				}
			}
			else
			{
				this._root.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026A4 File Offset: 0x000008A4
		public void ToggleServer(ClickEvent evt)
		{
			if (this._httpApi.IsRunning)
			{
				this._httpApi.Stop();
				return;
			}
			ImmutableArray<string> unsafeAddresses = this._httpWebhookRegistry.FindUnsafeAddresses();
			if (unsafeAddresses.IsEmpty)
			{
				this.StartAndCheckError();
				return;
			}
			this.StartIfUserAcceptsUnsafeAddresses(unsafeAddresses);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F0 File Offset: 0x000008F0
		public void StartIfUserAcceptsUnsafeAddresses(ImmutableArray<string> unsafeAddresses)
		{
			string str = string.Join("\n", from url in unsafeAddresses
			select SpecialStrings.RowStarter + url);
			this._dialogBoxShower.Create().SetMessage(this._loc.T(HttpApiFragment.UnsafeWebhooksLocKey) + "\n" + str).SetDefaultCancelButton(this._loc.T(HttpApiFragment.CancelLocKey)).SetConfirmButton(new Action(this.StartAndCheckError), this._loc.T(HttpApiFragment.IUnderstandTheRiskLocKey)).Show();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002794 File Offset: 0x00000994
		public void StartAndCheckError()
		{
			this._httpApi.Start();
			if (!this._httpApi.IsRunning)
			{
				this.ShowDialogWithError();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027B4 File Offset: 0x000009B4
		public void ShowDialogWithError()
		{
			string errorMessage = this._httpApi.ErrorMessage;
			if (errorMessage != null)
			{
				this._dialogBoxShower.Create().SetMessage(errorMessage.Substring(0, Math.Min(errorMessage.Length, 1000))).Show();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027FD File Offset: 0x000009FD
		public void OpenBrowser(ClickEvent evt)
		{
			if (this._httpApi.IsRunning)
			{
				this._urlOpener.OpenUrl(this._httpApi.Url);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002824 File Offset: 0x00000A24
		public void PortChanged(FocusOutEvent focusOutEvent)
		{
			ushort num;
			if (ushort.TryParse(this._portValue.value, out num) && num > 0)
			{
				this._httpApi.SetPort(num);
				return;
			}
			this.UpdatePortTextField();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000285C File Offset: 0x00000A5C
		public void UpdatePortTextField()
		{
			this._portValue.SetValueWithoutNotify(this._httpApi.Port.ToString());
		}

		// Token: 0x04000019 RID: 25
		public static readonly string StartApiLocKey = "Automation.Api.StartApi";

		// Token: 0x0400001A RID: 26
		public static readonly string StopApiLocKey = "Automation.Api.StopApi";

		// Token: 0x0400001B RID: 27
		public static readonly string UnsafeWebhooksLocKey = "Automation.Api.UnsafeWebhooksWarning";

		// Token: 0x0400001C RID: 28
		public static readonly string IUnderstandTheRiskLocKey = "Automation.Api.UnsafeWebhooksWarning.IUnderstandTheRisk";

		// Token: 0x0400001D RID: 29
		public static readonly string CancelLocKey = "Core.Cancel";

		// Token: 0x0400001E RID: 30
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;

		// Token: 0x04000020 RID: 32
		public readonly HttpApi _httpApi;

		// Token: 0x04000021 RID: 33
		public readonly UrlOpener _urlOpener;

		// Token: 0x04000022 RID: 34
		public readonly HttpWebhookRegistry _httpWebhookRegistry;

		// Token: 0x04000023 RID: 35
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000024 RID: 36
		public VisualElement _root;

		// Token: 0x04000025 RID: 37
		public VisualElement _portWrapper;

		// Token: 0x04000026 RID: 38
		public TextField _portValue;

		// Token: 0x04000027 RID: 39
		public Button _startStopButton;

		// Token: 0x04000028 RID: 40
		public Button _openBrowserButton;

		// Token: 0x04000029 RID: 41
		public Label _urlLabel;

		// Token: 0x0400002A RID: 42
		public HttpApiController _httpApiController;
	}
}
