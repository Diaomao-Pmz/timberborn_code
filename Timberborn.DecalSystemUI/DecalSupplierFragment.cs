using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DecalSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DecalSupplierFragment : IEntityPanelFragment
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002469 File Offset: 0x00000669
		public DecalSupplierFragment(VisualElementLoader visualElementLoader, IDecalService decalService, DecalButtonContainer decalButtonContainer, IExplorerOpener explorerOpener, EventBus eventBus, UserDecalTextureRepository userDecalTextureRepository)
		{
			this._visualElementLoader = visualElementLoader;
			this._decalService = decalService;
			this._decalButtonContainer = decalButtonContainer;
			this._explorerOpener = explorerOpener;
			this._eventBus = eventBus;
			this._userDecalTextureRepository = userDecalTextureRepository;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024A0 File Offset: 0x000006A0
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DecalSupplierFragment");
			this._root.ToggleDisplayStyle(false);
			this._decalButtonContainer.Initialize(this._root);
			UQueryExtensions.Q<Button>(this._root, "BrowseButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseButtonClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "RefreshButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnRefreshButtonClicked), 0);
			return this._root;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000252C File Offset: 0x0000072C
		public void ShowFragment(BaseComponent entity)
		{
			this._decalSupplier = entity.GetComponent<DecalSupplier>();
			if (this._decalSupplier)
			{
				this._decalButtonContainer.Show(this._decalSupplier);
				this._root.ToggleDisplayStyle(true);
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000257B File Offset: 0x0000077B
		public void UpdateFragment()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000257D File Offset: 0x0000077D
		public void ClearFragment()
		{
			if (this._decalSupplier)
			{
				this._decalButtonContainer.Clear();
				this._decalSupplier = null;
				this._root.ToggleDisplayStyle(false);
				this._eventBus.Unregister(this);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025B6 File Offset: 0x000007B6
		[OnEvent]
		public void OnDecalsReloaded(DecalsReloadedEvent decalsReloadedEvent)
		{
			this._decalButtonContainer.Show(this._decalSupplier);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025C9 File Offset: 0x000007C9
		public void OnBrowseButtonClicked(ClickEvent evt)
		{
			this._explorerOpener.OpenDirectory(this._userDecalTextureRepository.GetCustomDecalDirectory(this._decalSupplier.Category));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025EC File Offset: 0x000007EC
		public void OnRefreshButtonClicked(ClickEvent evt)
		{
			this._decalService.ReloadCustomDecals(this._decalSupplier.Category);
		}

		// Token: 0x04000014 RID: 20
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000015 RID: 21
		public readonly IDecalService _decalService;

		// Token: 0x04000016 RID: 22
		public readonly DecalButtonContainer _decalButtonContainer;

		// Token: 0x04000017 RID: 23
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public readonly UserDecalTextureRepository _userDecalTextureRepository;

		// Token: 0x0400001A RID: 26
		public VisualElement _root;

		// Token: 0x0400001B RID: 27
		public DecalSupplier _decalSupplier;
	}
}
