using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.HttpApiSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.HttpApiSystemUI
{
	// Token: 0x02000004 RID: 4
	public class HttpAdapterFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public HttpAdapterFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, EnumDropdownProviderFactory enumDropdownProviderFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/HttpAdapterFragment");
			this._methodDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Method", null);
			this._methodDropdownProvider = this._enumDropdownProviderFactory.Create<HttpWebhookMethod>(() => this._httpAdapter.Method, delegate(HttpWebhookMethod method)
			{
				this._httpAdapter.Method = method;
			}, (string method) => method.ToString().ToUpper());
			this._switchedOnWebhookEnabledToggle = UQueryExtensions.Q<Toggle>(this._root, "SwitchedOnWebhookEnabled", null);
			this._switchedOffWebhookEnabledToggle = UQueryExtensions.Q<Toggle>(this._root, "SwitchedOffWebhookEnabled", null);
			this._switchedOnWebhookUrlTextField = UQueryExtensions.Q<TextField>(this._root, "SwitchedOnWebhookUrl", null);
			this._switchedOffWebhookUrlTextField = UQueryExtensions.Q<TextField>(this._root, "SwitchedOffWebhookUrl", null);
			this._switchedOnWebhookStatusLabel = UQueryExtensions.Q<Label>(this._root, "SwitchedOnWebhookStatus", null);
			this._switchedOffWebhookStatusLabel = UQueryExtensions.Q<Label>(this._root, "SwitchedOffWebhookStatus", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._switchedOnWebhookEnabledToggle, delegate(ChangeEvent<bool> _)
			{
				this.OnWebhooksChanged();
			});
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._switchedOffWebhookEnabledToggle, delegate(ChangeEvent<bool> _)
			{
				this.OnWebhooksChanged();
			});
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._switchedOnWebhookUrlTextField, delegate(ChangeEvent<string> _)
			{
				this.OnWebhooksChanged();
			});
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._switchedOffWebhookUrlTextField, delegate(ChangeEvent<string> _)
			{
				this.OnWebhooksChanged();
			});
			this._switchedOnWebhookUrlTextField.isDelayed = true;
			this._switchedOffWebhookUrlTextField.isDelayed = true;
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000227C File Offset: 0x0000047C
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<HttpAdapter>(out this._httpAdapter))
			{
				this._switchedOnWebhookEnabledToggle.SetValueWithoutNotify(this._httpAdapter.SwitchedOnWebhookEnabled);
				this._switchedOffWebhookEnabledToggle.SetValueWithoutNotify(this._httpAdapter.SwitchedOffWebhookEnabled);
				this._switchedOnWebhookUrlTextField.SetValueWithoutNotify(this._httpAdapter.SwitchedOnWebhookUrl);
				this._switchedOffWebhookUrlTextField.SetValueWithoutNotify(this._httpAdapter.SwitchedOffWebhookUrl);
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._methodDropdown, this._methodDropdownProvider);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002312 File Offset: 0x00000512
		public void UpdateFragment()
		{
			if (this._httpAdapter)
			{
				this.UpdateStatusLabel(this._switchedOnWebhookStatusLabel, this._httpAdapter.LastOnCallSuccessful);
				this.UpdateStatusLabel(this._switchedOffWebhookStatusLabel, this._httpAdapter.LastOffCallSuccessful);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000234F File Offset: 0x0000054F
		public void ClearFragment()
		{
			this._httpAdapter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002364 File Offset: 0x00000564
		public void UpdateStatusLabel(Label label, bool? successful)
		{
			if (successful != null)
			{
				label.text = this._loc.T<string>(HttpAdapterFragment.StatusLocKey, successful.Value ? this._loc.T(HttpAdapterFragment.StatusOKLocKey) : this._loc.T(HttpAdapterFragment.StatusErrorLocKey));
				label.ToggleDisplayStyle(true);
				return;
			}
			label.ToggleDisplayStyle(false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023CC File Offset: 0x000005CC
		public void OnWebhooksChanged()
		{
			this._httpAdapter.SwitchedOnWebhookEnabled = this._switchedOnWebhookEnabledToggle.value;
			this._httpAdapter.SwitchedOffWebhookEnabled = this._switchedOffWebhookEnabledToggle.value;
			this._httpAdapter.SwitchedOnWebhookUrl = this._switchedOnWebhookUrlTextField.value;
			this._httpAdapter.SwitchedOffWebhookUrl = this._switchedOffWebhookUrlTextField.value;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string StatusLocKey = "Building.HttpAdapter.Status";

		// Token: 0x04000007 RID: 7
		public static readonly string StatusOKLocKey = "Building.HttpAdapter.Status.OK";

		// Token: 0x04000008 RID: 8
		public static readonly string StatusErrorLocKey = "Building.HttpAdapter.Status.Error";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400000B RID: 11
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public EnumDropdownProvider<HttpWebhookMethod> _methodDropdownProvider;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;

		// Token: 0x0400000F RID: 15
		public Dropdown _methodDropdown;

		// Token: 0x04000010 RID: 16
		public Toggle _switchedOnWebhookEnabledToggle;

		// Token: 0x04000011 RID: 17
		public Toggle _switchedOffWebhookEnabledToggle;

		// Token: 0x04000012 RID: 18
		public TextField _switchedOnWebhookUrlTextField;

		// Token: 0x04000013 RID: 19
		public TextField _switchedOffWebhookUrlTextField;

		// Token: 0x04000014 RID: 20
		public Label _switchedOnWebhookStatusLabel;

		// Token: 0x04000015 RID: 21
		public Label _switchedOffWebhookStatusLabel;

		// Token: 0x04000016 RID: 22
		public HttpAdapter _httpAdapter;
	}
}
