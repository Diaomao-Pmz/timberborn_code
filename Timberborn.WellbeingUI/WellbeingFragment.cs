using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.Effects;
using Timberborn.EntityPanelSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200001F RID: 31
	public class WellbeingFragment : IEntityPanelFragment
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x0000416E File Offset: 0x0000236E
		public WellbeingFragment(VisualElementLoader visualElementLoader, NeedViewFactory needViewFactory, NeedGroupSpecService needGroupSpecService, NeedGroupViewFactory needGroupViewFactory, DevModeManager devModeManager, WellbeingSummaryFactory wellbeingSummaryFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._needViewFactory = needViewFactory;
			this._needGroupSpecService = needGroupSpecService;
			this._needGroupViewFactory = needGroupViewFactory;
			this._devModeManager = devModeManager;
			this._wellbeingSummaryFactory = wellbeingSummaryFactory;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000041B0 File Offset: 0x000023B0
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WellbeingFragment");
			this._needViewsElement = UQueryExtensions.Q<VisualElement>(this._root, "NeedViews", null);
			this._wellbeingSummaryElement = UQueryExtensions.Q<VisualElement>(this._root, "Summary", null);
			this._debugButtons = UQueryExtensions.Q<VisualElement>(this._root, "DebugButtons", null);
			this._editButton = UQueryExtensions.Q<Button>(this._root, "Edit", null);
			this._editButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._editMode = !this._editMode;
			}, 0);
			this._freezeButton = UQueryExtensions.Q<Button>(this._root, "Freeze", null);
			this._freezeButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.FreezeAllNeeds), 0);
			this._unfreezeButton = UQueryExtensions.Q<Button>(this._root, "Unfreeze", null);
			this._unfreezeButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.UnfreezeAllNeeds), 0);
			this._boostButton = UQueryExtensions.Q<Button>(this._root, "Boost", null);
			this._boostButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.BoostAllNeeds), 0);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000042E8 File Offset: 0x000024E8
		public void ShowFragment(BaseComponent entity)
		{
			NeedManager component = entity.GetComponent<NeedManager>();
			if (component)
			{
				this._selectedNeedManager = component;
				this.InitializeSummary();
				this.InitializeNeedGroups();
				this.InitializeNeeds();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004329 File Offset: 0x00002529
		public void ClearFragment()
		{
			this.ClearNeedViews();
			this._wellbeingSummaryElement.Clear();
			this._root.ToggleDisplayStyle(false);
			this._selectedNeedManager = null;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004350 File Offset: 0x00002550
		public void UpdateFragment()
		{
			if (this._selectedNeedManager)
			{
				this._wellbeingSummary.UpdateContent();
				foreach (NeedGroupView needGroupView in this._needGroupViews.Values)
				{
					needGroupView.Update(this._selectedNeedManager, this.InEditMode);
				}
				this._debugButtons.ToggleDisplayStyle(this._devModeManager.Enabled);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000043E0 File Offset: 0x000025E0
		public bool InEditMode
		{
			get
			{
				return this._editMode && this._devModeManager.Enabled;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000043F7 File Offset: 0x000025F7
		public void InitializeSummary()
		{
			this._wellbeingSummary = this._wellbeingSummaryFactory.Create(this._selectedNeedManager);
			this._wellbeingSummaryElement.Add(this._wellbeingSummary.Root);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004428 File Offset: 0x00002628
		public void InitializeNeedGroups()
		{
			foreach (NeedGroupSpec needGroupSpec in this._needGroupSpecService.NeedGroups)
			{
				NeedGroupView needGroupView = this._needGroupViewFactory.Create(needGroupSpec);
				this._needViewsElement.Add(needGroupView.Root);
				this._needGroupViews.Add(needGroupSpec.Id, needGroupView);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000044A4 File Offset: 0x000026A4
		public void InitializeNeeds()
		{
			foreach (NeedSpec needSpec in this._selectedNeedManager.NeedSpecs)
			{
				NeedView needView = this._needViewFactory.Create(needSpec, this._selectedNeedManager);
				this._needGroupViews[needSpec.NeedGroupId].AddNeed(needView);
			}
			foreach (NeedGroupView needGroupView in this._needGroupViews.Values)
			{
				needGroupView.UpdateVisibility();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000454C File Offset: 0x0000274C
		public void ClearNeedViews()
		{
			foreach (NeedGroupView needGroupView in this._needGroupViews.Values)
			{
				needGroupView.ClearNeedViews();
			}
			this._needViewsElement.Clear();
			this._needGroupViews.Clear();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000045B8 File Offset: 0x000027B8
		public void FreezeAllNeeds(ClickEvent evt)
		{
			if (this._selectedNeedManager)
			{
				foreach (NeedSpec needSpec in this._selectedNeedManager.NeedSpecs)
				{
					this._selectedNeedManager.DisableUpdate(needSpec.Id);
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000460C File Offset: 0x0000280C
		public void UnfreezeAllNeeds(ClickEvent evt)
		{
			if (this._selectedNeedManager)
			{
				foreach (NeedSpec needSpec in this._selectedNeedManager.NeedSpecs)
				{
					this._selectedNeedManager.EnableUpdate(needSpec.Id);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004660 File Offset: 0x00002860
		public void BoostAllNeeds(ClickEvent evt)
		{
			if (this._selectedNeedManager)
			{
				foreach (NeedSpec needSpec in this._selectedNeedManager.NeedSpecs)
				{
					NeedManager selectedNeedManager = this._selectedNeedManager;
					InstantEffect instantEffect = new InstantEffect(needSpec.Id, this._selectedNeedManager.NeedPointsToMax(needSpec.Id), 1);
					selectedNeedManager.ApplyEffect(instantEffect);
				}
			}
		}

		// Token: 0x04000091 RID: 145
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000092 RID: 146
		public readonly NeedViewFactory _needViewFactory;

		// Token: 0x04000093 RID: 147
		public readonly NeedGroupSpecService _needGroupSpecService;

		// Token: 0x04000094 RID: 148
		public readonly NeedGroupViewFactory _needGroupViewFactory;

		// Token: 0x04000095 RID: 149
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000096 RID: 150
		public readonly WellbeingSummaryFactory _wellbeingSummaryFactory;

		// Token: 0x04000097 RID: 151
		public NeedManager _selectedNeedManager;

		// Token: 0x04000098 RID: 152
		public readonly Dictionary<string, NeedGroupView> _needGroupViews = new Dictionary<string, NeedGroupView>();

		// Token: 0x04000099 RID: 153
		public VisualElement _root;

		// Token: 0x0400009A RID: 154
		public VisualElement _needViewsElement;

		// Token: 0x0400009B RID: 155
		public VisualElement _wellbeingSummaryElement;

		// Token: 0x0400009C RID: 156
		public VisualElement _debugButtons;

		// Token: 0x0400009D RID: 157
		public Button _editButton;

		// Token: 0x0400009E RID: 158
		public Button _freezeButton;

		// Token: 0x0400009F RID: 159
		public Button _unfreezeButton;

		// Token: 0x040000A0 RID: 160
		public Button _boostButton;

		// Token: 0x040000A1 RID: 161
		public bool _editMode;

		// Token: 0x040000A2 RID: 162
		public WellbeingSummary _wellbeingSummary;
	}
}
