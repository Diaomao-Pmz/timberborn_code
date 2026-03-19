using System;
using Timberborn.CameraSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000004 RID: 4
	public class BatchControlBox : ILoadableSingleton, IPanelController, IInputProcessor, IBatchControlBox
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public BatchControlBox(VisualElementLoader visualElementLoader, UILayout uiLayout, PanelStack panelStack, CameraHorizontalShifter cameraHorizontalShifter, InputService inputService, EventBus eventBus, BatchControlBoxTabController batchControlBoxTabController, BatchControlBoxDistrictController batchControlBoxDistrictController, IHideableByBatchControl hideableByBatchControl)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._panelStack = panelStack;
			this._cameraHorizontalShifter = cameraHorizontalShifter;
			this._inputService = inputService;
			this._eventBus = eventBus;
			this._batchControlBoxTabController = batchControlBoxTabController;
			this._batchControlBoxDistrictController = batchControlBoxDistrictController;
			this._hideableByBatchControl = hideableByBatchControl;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlBox");
			this._batchControlBoxTabController.Initialize(this._root);
			this._batchControlBoxDistrictController.Initialize(this._root);
			UQueryExtensions.Q<Button>(this._root, "CancelButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "SettlementButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B0 File Offset: 0x000003B0
		public VisualElement GetPanel()
		{
			this._uiLayout.HideLeftAndCenterItems();
			this._hideableByBatchControl.Hide();
			this._cameraHorizontalShifter.EnableHorizontalCameraShift(BatchControlBox.CameraOffset);
			this._batchControlBoxTabController.UpdateEntities();
			this._inputService.AddInputProcessor(this);
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002200 File Offset: 0x00000400
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002203 File Offset: 0x00000403
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000220B File Offset: 0x0000040B
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(BatchControlBox.ToggleBatchControlBoxKey))
			{
				this.Close();
				return true;
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002228 File Offset: 0x00000428
		public void OpenBatchControlBox()
		{
			this.OpenTab(this._batchControlBoxTabController.LastOpenedTabIndex);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000223B File Offset: 0x0000043B
		public void OpenCharactersTab()
		{
			this.OpenTab(0);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002244 File Offset: 0x00000444
		public void OpenHousingTab()
		{
			this.OpenTab(1);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000224D File Offset: 0x0000044D
		public void OpenWorkplacesTab()
		{
			this.OpenTab(2);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002256 File Offset: 0x00000456
		public void OpenMigrationTab()
		{
			this.OpenTab(6);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000225F File Offset: 0x0000045F
		public void OpenDistributionTab()
		{
			this.OpenTab(7);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002268 File Offset: 0x00000468
		public void OpenTab(int index)
		{
			if (this._batchControlBoxTabController.CurrentTab != null)
			{
				this._batchControlBoxTabController.ShowTab(index);
				return;
			}
			this._panelStack.HideAndPushWithoutPause(this);
			this._batchControlBoxDistrictController.Show();
			this._batchControlBoxTabController.ShowTab(index);
			this._eventBus.Post(new BatchControlBoxShownEvent());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C4 File Offset: 0x000004C4
		public void Close()
		{
			this._batchControlBoxTabController.Clear();
			this._batchControlBoxDistrictController.Clear();
			this._uiLayout.ShowLeftAndCenterItems();
			this._hideableByBatchControl.Show();
			this._panelStack.Pop(this);
			this._cameraHorizontalShifter.DisableCameraShift();
			this._inputService.RemoveInputProcessor(this);
			this._eventBus.Post(new BatchControlBoxHiddenEvent());
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ToggleBatchControlBoxKey = "ToggleBatchControlBox";

		// Token: 0x04000007 RID: 7
		public static readonly float CameraOffset = -0.2f;

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly UILayout _uiLayout;

		// Token: 0x0400000A RID: 10
		public readonly PanelStack _panelStack;

		// Token: 0x0400000B RID: 11
		public readonly CameraHorizontalShifter _cameraHorizontalShifter;

		// Token: 0x0400000C RID: 12
		public readonly InputService _inputService;

		// Token: 0x0400000D RID: 13
		public readonly EventBus _eventBus;

		// Token: 0x0400000E RID: 14
		public readonly BatchControlBoxTabController _batchControlBoxTabController;

		// Token: 0x0400000F RID: 15
		public readonly BatchControlBoxDistrictController _batchControlBoxDistrictController;

		// Token: 0x04000010 RID: 16
		public readonly IHideableByBatchControl _hideableByBatchControl;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;
	}
}
