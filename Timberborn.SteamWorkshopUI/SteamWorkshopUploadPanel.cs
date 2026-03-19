using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.SteamOverlaySystem;
using Timberborn.SteamWorkshop;
using UnityEngine.UIElements;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x02000006 RID: 6
	public class SteamWorkshopUploadPanel : ILoadableSingleton, IPanelController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002108 File Offset: 0x00000308
		public SteamWorkshopUploadPanel(VisualElementLoader visualElementLoader, PanelStack panelStack, SteamWorkshopItemCreator steamWorkshopItemCreator, SteamWorkshopItemUpdater steamWorkshopItemUpdater, SteamOverlayOpener steamOverlayOpener, DialogBoxShower dialogBoxShower, ILoc loc, UploadPanelElements uploadPanelElements, SteamWorkshopUploadProgress steamWorkshopUploadProgress, HyperlinkInitializer hyperlinkInitializer)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._steamWorkshopItemCreator = steamWorkshopItemCreator;
			this._steamWorkshopItemUpdater = steamWorkshopItemUpdater;
			this._steamOverlayOpener = steamOverlayOpener;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
			this._uploadPanelElements = uploadPanelElements;
			this._steamWorkshopUploadProgress = steamWorkshopUploadProgress;
			this._hyperlinkInitializer = hyperlinkInitializer;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002168 File Offset: 0x00000368
		public void Load()
		{
			string elementName = "Common/SteamWorkshop/SteamWorkshopUploadPanel";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnCancelClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "UploadButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnUploadClicked), 0);
			this._uploadPanelElements.Initialize(this._root);
			this._hyperlinkInitializer.Initialize(UQueryExtensions.Q<Label>(this._root, "LegalAgreement", null), delegate
			{
				this._steamOverlayOpener.OpenLegalAgreement();
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000220C File Offset: 0x0000040C
		public void Open(ISteamWorkshopUploadable steamWorkshopUploadable)
		{
			this.OpenInternal(steamWorkshopUploadable);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002215 File Offset: 0x00000415
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000221D File Offset: 0x0000041D
		public bool OnUIConfirmed()
		{
			this.ValidateAndUpload();
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002226 File Offset: 0x00000426
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000222E File Offset: 0x0000042E
		public void OpenInternal(ISteamWorkshopUploadable steamWorkshopUploadable)
		{
			Asserts.FieldIsNull<SteamWorkshopUploadPanel>(this, this._steamWorkshopUploadable, "_steamWorkshopUploadable");
			this._steamWorkshopUploadable = steamWorkshopUploadable;
			this._uploadPanelElements.Open(this._steamWorkshopUploadable);
			this._panelStack.HideAndPushOverlay(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002226 File Offset: 0x00000426
		public void OnCancelClicked(ClickEvent evt)
		{
			this.Close();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002265 File Offset: 0x00000465
		public void OnUploadClicked(ClickEvent evt)
		{
			this.ValidateAndUpload();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000226D File Offset: 0x0000046D
		public void ValidateAndUpload()
		{
			if (this._steamWorkshopUploadable.ValidateName(this._uploadPanelElements.Name))
			{
				this.Upload();
				return;
			}
			this._dialogBoxShower.Create().SetLocalizedMessage(SteamWorkshopUploadPanel.InvalidNameLocKey).Show();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022A9 File Offset: 0x000004A9
		public void Upload()
		{
			this._steamWorkshopUploadProgress.Open();
			if (this._uploadPanelElements.ShouldCreateNew())
			{
				this._steamWorkshopItemCreator.CreateItem(new Action<SteamWorkshopCreateResponse>(this.CreateItemCallback));
				return;
			}
			this.UpdateExistingItem();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022E4 File Offset: 0x000004E4
		public void CreateItemCallback(SteamWorkshopCreateResponse createResponse)
		{
			if (createResponse.Successful)
			{
				this._steamWorkshopUploadable.OnItemCreated(createResponse.ItemId, this._uploadPanelElements.Name, this._uploadPanelElements.Visibility, this._uploadPanelElements.ChosenTags);
				this.UpdateExistingItem();
				return;
			}
			this.NotifyUploadFailure(createResponse.ResultMessage);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002340 File Offset: 0x00000540
		public void UpdateExistingItem()
		{
			this._steamWorkshopUploadable.OnUpdateStarted(this._uploadPanelElements.Name);
			SteamWorkshopUpdateRequest steamWorkshopUpdateRequest = this._uploadPanelElements.CreateUpdateRequest();
			this._steamWorkshopUploadable.OnUpdateRequestCreated(steamWorkshopUpdateRequest);
			SteamWorkshopUpdateHandle updateHandle = this._steamWorkshopItemUpdater.Update(steamWorkshopUpdateRequest, new Action<SteamWorkshopUpdateResponse>(this.UpdateItemCallback));
			this._steamWorkshopUploadProgress.SetUpdateHandle(updateHandle);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023A0 File Offset: 0x000005A0
		public void UpdateItemCallback(SteamWorkshopUpdateResponse updateResponse)
		{
			this._steamWorkshopUploadable.OnUpdateFinished(updateResponse);
			if (updateResponse.Successful)
			{
				this.NotifyUploadSuccess(updateResponse);
				return;
			}
			this.NotifyUploadFailure(updateResponse.ResultMessage);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023CC File Offset: 0x000005CC
		public void NotifyUploadSuccess(SteamWorkshopUpdateResponse updateResponse)
		{
			this._steamWorkshopUploadProgress.Close();
			this._dialogBoxShower.Create().SetLocalizedMessage(SteamWorkshopUploadPanel.UploadSuccessMessageLocKey).SetConfirmButton(new Action(this.Close), this._loc.T(CommonLocKeys.OKKey)).SetInfoButton(delegate
			{
				this._steamOverlayOpener.OpenWorkshopItem(updateResponse.Request.ItemId);
			}, this._loc.T(SteamWorkshopUploadPanel.ShowWorkshopItemLocKey)).Show();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002458 File Offset: 0x00000658
		public void NotifyUploadFailure(string resultMessage)
		{
			this._steamWorkshopUploadProgress.Close();
			string message = this._loc.T<string>(SteamWorkshopUploadPanel.UploadFailedMessageLocKey, resultMessage);
			this._dialogBoxShower.Create().SetMessage(message).Show();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002499 File Offset: 0x00000699
		public void Close()
		{
			this._steamWorkshopUploadable.Clear();
			this._steamWorkshopUploadable = null;
			this._uploadPanelElements.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string UploadSuccessMessageLocKey = "SteamWorkshop.UploadSuccess";

		// Token: 0x04000007 RID: 7
		public static readonly string UploadFailedMessageLocKey = "SteamWorkshop.UploadFailed";

		// Token: 0x04000008 RID: 8
		public static readonly string ShowWorkshopItemLocKey = "SteamWorkshop.ShowWorkshopItem";

		// Token: 0x04000009 RID: 9
		public static readonly string InvalidNameLocKey = "Saving.InvalidName";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly PanelStack _panelStack;

		// Token: 0x0400000C RID: 12
		public readonly SteamWorkshopItemCreator _steamWorkshopItemCreator;

		// Token: 0x0400000D RID: 13
		public readonly SteamWorkshopItemUpdater _steamWorkshopItemUpdater;

		// Token: 0x0400000E RID: 14
		public readonly SteamOverlayOpener _steamOverlayOpener;

		// Token: 0x0400000F RID: 15
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;

		// Token: 0x04000011 RID: 17
		public readonly UploadPanelElements _uploadPanelElements;

		// Token: 0x04000012 RID: 18
		public readonly SteamWorkshopUploadProgress _steamWorkshopUploadProgress;

		// Token: 0x04000013 RID: 19
		public readonly HyperlinkInitializer _hyperlinkInitializer;

		// Token: 0x04000014 RID: 20
		public VisualElement _root;

		// Token: 0x04000015 RID: 21
		public ISteamWorkshopUploadable _steamWorkshopUploadable;
	}
}
