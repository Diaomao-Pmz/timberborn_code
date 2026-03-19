using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000009 RID: 9
	public class DiagnosticFragmentController
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023CE File Offset: 0x000005CE
		public DiagnosticFragmentController(DevModeManager devModeManager, EventBus eventBus)
		{
			this._devModeManager = devModeManager;
			this._eventBus = eventBus;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F0 File Offset: 0x000005F0
		public void Initialize(IEnumerable<IEntityPanelFragment> fragments, VisualElement parent)
		{
			this._root = UQueryExtensions.Q<VisualElement>(parent, "DiagnosticFragments", null);
			this._root.ToggleDisplayStyle(this._devModeManager.Enabled);
			foreach (IEntityPanelFragment entityPanelFragment in fragments)
			{
				this._root.Add(entityPanelFragment.InitializeFragment());
				this._diagnosticFragments.Add(entityPanelFragment);
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002484 File Offset: 0x00000684
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			if (devModeToggledEvent.Enabled)
			{
				if (this._shownEntity)
				{
					this.ShowFragments(this._shownEntity);
					return;
				}
			}
			else
			{
				this.ClearFragmentsInternal();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024B0 File Offset: 0x000006B0
		public void ShowFragments(EntityComponent entity)
		{
			if (this._devModeManager.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				foreach (IEntityPanelFragment entityPanelFragment in this._diagnosticFragments)
				{
					entityPanelFragment.ShowFragment(entity);
				}
			}
			this._shownEntity = entity;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002524 File Offset: 0x00000724
		public void ClearFragments()
		{
			this.ClearFragmentsInternal();
			this._shownEntity = null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002534 File Offset: 0x00000734
		public void UpdateFragments()
		{
			if (this._devModeManager.Enabled)
			{
				foreach (IEntityPanelFragment entityPanelFragment in this._diagnosticFragments)
				{
					entityPanelFragment.UpdateFragment();
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002594 File Offset: 0x00000794
		public void ClearFragmentsInternal()
		{
			foreach (IEntityPanelFragment entityPanelFragment in this._diagnosticFragments)
			{
				entityPanelFragment.ClearFragment();
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000010 RID: 16
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public readonly List<IEntityPanelFragment> _diagnosticFragments = new List<IEntityPanelFragment>();

		// Token: 0x04000013 RID: 19
		public VisualElement _root;

		// Token: 0x04000014 RID: 20
		public EntityComponent _shownEntity;
	}
}
