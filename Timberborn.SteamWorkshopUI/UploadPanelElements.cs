using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.SteamWorkshop;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x02000009 RID: 9
	public class UploadPanelElements
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002656 File Offset: 0x00000856
		public UploadPanelElements(VisibilityDropdownProvider visibilityDropdownProvider, DropdownItemsSetter dropdownItemsSetter, UploadPanelTags uploadPanelTags)
		{
			this._visibilityDropdownProvider = visibilityDropdownProvider;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._uploadPanelTags = uploadPanelTags;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002673 File Offset: 0x00000873
		public string Name
		{
			get
			{
				return this._nameTextField.value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002680 File Offset: 0x00000880
		public SteamWorkshopVisibility Visibility
		{
			get
			{
				return this._visibilityDropdownProvider.CurrentValue;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000268D File Offset: 0x0000088D
		public IEnumerable<string> ChosenTags
		{
			get
			{
				return this._uploadPanelTags.GetChosenTags();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000269C File Offset: 0x0000089C
		public void Initialize(VisualElement root)
		{
			this._nameTextField = UQueryExtensions.Q<TextField>(root, "Name", null);
			this._descriptionTextField = UQueryExtensions.Q<TextField>(root, "Description", null);
			this._updateDescriptionToggle = UQueryExtensions.Q<Toggle>(root, "UpdateDescription", null);
			this._visibilityDropdown = UQueryExtensions.Q<Dropdown>(root, "Visibility", null);
			this._updateVisibilityToggle = UQueryExtensions.Q<Toggle>(root, "UpdateVisibility", null);
			this._changelogTextField = UQueryExtensions.Q<TextField>(root, "Changelog", null);
			this._previewImage = UQueryExtensions.Q<Image>(root, "ThumbnailImage", null);
			this._previewInfo = UQueryExtensions.Q<Label>(root, "ThumbnailInfoLabel", null);
			this._refreshPreviewButton = UQueryExtensions.Q<Button>(root, "RefreshThumbnailButton", null);
			this._refreshPreviewButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.UpdatePreviewState();
			}, 0);
			this._updatePreviewToggle = UQueryExtensions.Q<Toggle>(root, "UpdatePreview", null);
			this._updateTagsToggle = UQueryExtensions.Q<Toggle>(root, "UpdateTags", null);
			this._uploadAsNewToggle = UQueryExtensions.Q<Toggle>(root, "UploadAsNew", null);
			this._descriptionTextField.RegisterCallback<ChangeEvent<string>>(delegate(ChangeEvent<string> _)
			{
				this._updateDescriptionToggle.value = true;
			}, 0);
			this._uploadAsNewToggle.RegisterCallback<ChangeEvent<bool>>(delegate(ChangeEvent<bool> _)
			{
				this.UpdateTogglesState();
			}, 0);
			this._visibilityDropdownProvider.Initialize(this._updateVisibilityToggle);
			this._uploadPanelTags.Initialize(UQueryExtensions.Q<VisualElement>(root, "TagsContent", null));
			this._uploadPanelTags.TagsChanged += delegate(object _, EventArgs _)
			{
				this._updateTagsToggle.value = true;
			};
			this._dropdownItemsSetter.SetLocalizableItems(this._visibilityDropdown, this._visibilityDropdownProvider);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002820 File Offset: 0x00000A20
		public void Open(ISteamWorkshopUploadable steamWorkshopUploadable)
		{
			this._steamWorkshopUploadable = steamWorkshopUploadable;
			this._nameTextField.SetValueWithoutNotify(this._steamWorkshopUploadable.Name);
			this._nameTextField.SetEnabled(!this._steamWorkshopUploadable.NameIsReadOnly);
			this._descriptionTextField.SetValueWithoutNotify(this._steamWorkshopUploadable.Description);
			this._visibilityDropdownProvider.SetInitialValue(this._steamWorkshopUploadable.Visibility);
			this._visibilityDropdown.UpdateSelectedValue();
			this._uploadAsNewToggle.SetValueWithoutNotify(false);
			this._changelogTextField.value = string.Empty;
			this._uploadPanelTags.Open(steamWorkshopUploadable);
			this.UpdatePreviewState();
			this.UpdateTogglesState();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028CE File Offset: 0x00000ACE
		public void Clear()
		{
			this._steamWorkshopUploadable = null;
			this._uploadPanelTags.Clear();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028E2 File Offset: 0x00000AE2
		public bool ShouldCreateNew()
		{
			return !this._uploadAsNewToggle.IsDisplayed() || this._uploadAsNewToggle.value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002900 File Offset: 0x00000B00
		public SteamWorkshopUpdateRequest CreateUpdateRequest()
		{
			if (this._steamWorkshopUploadable.ItemId == null)
			{
				throw new NotSupportedException("Cannot create update request for item that has not been created yet");
			}
			SteamWorkshopUpdateRequest.Builder builder = new SteamWorkshopUpdateRequest.Builder(this._steamWorkshopUploadable.ItemId.Value, this._nameTextField.value).SetContentPath(this._steamWorkshopUploadable.ContentPath);
			if (!this._updateDescriptionToggle.IsDisplayed() || this._updateDescriptionToggle.value)
			{
				builder.SetDescription(this._descriptionTextField.value);
			}
			if (!this._updateVisibilityToggle.IsDisplayed() || this._updateVisibilityToggle.value)
			{
				builder.SetVisibility(new SteamWorkshopVisibility?(this._visibilityDropdownProvider.CurrentValue));
			}
			if (!this._updatePreviewToggle.IsDisplayed() || this._updatePreviewToggle.value)
			{
				builder.SetPreviewPath(this._steamWorkshopUploadable.PreviewPath);
			}
			if (!this._updateTagsToggle.IsDisplayed() || this._updateTagsToggle.value)
			{
				builder.AddMandatoryTags(this._steamWorkshopUploadable.MandatoryTags);
				builder.AddChosenTags(this._uploadPanelTags.GetChosenTags());
			}
			if (!string.IsNullOrEmpty(this._changelogTextField.value))
			{
				builder.SetChangelog(this._changelogTextField.value);
			}
			return builder.Build();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A50 File Offset: 0x00000C50
		public void UpdatePreviewState()
		{
			Texture2D preview = this._steamWorkshopUploadable.Preview;
			this._steamWorkshopUploadable.RefreshPreview();
			this._previewImage.image = this._steamWorkshopUploadable.Preview;
			this._previewImage.EnableInClassList(UploadPanelElements.ThumbnailBackgroundClass, this._previewImage.image);
			this._previewInfo.text = this._steamWorkshopUploadable.PreviewInfo;
			this._refreshPreviewButton.ToggleDisplayStyle(!this._previewImage.image);
			if (!this._updatePreviewToggle.value && !preview && this._steamWorkshopUploadable.Preview)
			{
				this._updatePreviewToggle.value = true;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B14 File Offset: 0x00000D14
		public void UpdateTogglesState()
		{
			bool flag = this._steamWorkshopUploadable.ItemId != null;
			this._uploadAsNewToggle.ToggleDisplayStyle(flag);
			bool visible = flag && !this._uploadAsNewToggle.value;
			this._updateDescriptionToggle.ToggleDisplayStyle(visible);
			this._updateVisibilityToggle.ToggleDisplayStyle(visible);
			this._updatePreviewToggle.ToggleDisplayStyle(visible);
			this._updateTagsToggle.ToggleDisplayStyle(visible);
			this._updateDescriptionToggle.value = this._steamWorkshopUploadable.UpdateDescription;
			this._updateVisibilityToggle.value = this._steamWorkshopUploadable.UpdateVisibility;
			this._updatePreviewToggle.value = this._steamWorkshopUploadable.UpdatePreview;
			this._updateTagsToggle.value = this._steamWorkshopUploadable.UpdateTags;
		}

		// Token: 0x04000021 RID: 33
		public static readonly string ThumbnailBackgroundClass = "steam-workshop-upload-panel__thumbnail-background";

		// Token: 0x04000022 RID: 34
		public readonly VisibilityDropdownProvider _visibilityDropdownProvider;

		// Token: 0x04000023 RID: 35
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000024 RID: 36
		public readonly UploadPanelTags _uploadPanelTags;

		// Token: 0x04000025 RID: 37
		public TextField _nameTextField;

		// Token: 0x04000026 RID: 38
		public TextField _descriptionTextField;

		// Token: 0x04000027 RID: 39
		public Toggle _updateDescriptionToggle;

		// Token: 0x04000028 RID: 40
		public Dropdown _visibilityDropdown;

		// Token: 0x04000029 RID: 41
		public Toggle _updateVisibilityToggle;

		// Token: 0x0400002A RID: 42
		public TextField _changelogTextField;

		// Token: 0x0400002B RID: 43
		public Image _previewImage;

		// Token: 0x0400002C RID: 44
		public Label _previewInfo;

		// Token: 0x0400002D RID: 45
		public Button _refreshPreviewButton;

		// Token: 0x0400002E RID: 46
		public Toggle _updatePreviewToggle;

		// Token: 0x0400002F RID: 47
		public Toggle _updateTagsToggle;

		// Token: 0x04000030 RID: 48
		public Toggle _uploadAsNewToggle;

		// Token: 0x04000031 RID: 49
		public ISteamWorkshopUploadable _steamWorkshopUploadable;
	}
}
