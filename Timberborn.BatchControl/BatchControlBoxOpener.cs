using System;
using System.Collections.Generic;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;

namespace Timberborn.BatchControl
{
	// Token: 0x02000007 RID: 7
	public class BatchControlBoxOpener : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000024C2 File Offset: 0x000006C2
		public BatchControlBoxOpener(IBatchControlBox batchControlBox, BatchControlBoxTabController batchControlBoxTabController, InputService inputService, EventBus eventBus)
		{
			this._batchControlBox = batchControlBox;
			this._batchControlBoxTabController = batchControlBoxTabController;
			this._inputService = inputService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024F2 File Offset: 0x000006F2
		public void Load()
		{
			this._batchControlTabs.AddRange(this._batchControlBoxTabController.Tabs);
			this._eventBus.Register(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002516 File Offset: 0x00000716
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002524 File Offset: 0x00000724
		public bool ProcessInput()
		{
			foreach (BatchControlTab batchControlTab in this._batchControlTabs)
			{
				if (this._inputService.IsKeyDown(batchControlTab.BindingKey))
				{
					this.OpenTab(batchControlTab);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002594 File Offset: 0x00000794
		public void OpenTab(BatchControlTab batchControlTab)
		{
			int tabIndex = this._batchControlBoxTabController.GetTabIndex(batchControlTab);
			this._batchControlBox.OpenTab(tabIndex);
		}

		// Token: 0x0400001B RID: 27
		public readonly IBatchControlBox _batchControlBox;

		// Token: 0x0400001C RID: 28
		public readonly BatchControlBoxTabController _batchControlBoxTabController;

		// Token: 0x0400001D RID: 29
		public readonly InputService _inputService;

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly List<BatchControlTab> _batchControlTabs = new List<BatchControlTab>();
	}
}
