using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.Explosions;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ExplosionsUI
{
	// Token: 0x0200000A RID: 10
	public class UnstableCoreFragment : IEntityPanelFragment
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000272D File Offset: 0x0000092D
		public UnstableCoreFragment(EntityChangeRecorderFactory entityChangeRecorderFactory, VisualElementLoader visualElementLoader, DevModeManager devModeManager, MapEditorMode mapEditorMode, EventBus eventBus)
		{
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
			this._visualElementLoader = visualElementLoader;
			this._devModeManager = devModeManager;
			this._mapEditorMode = mapEditorMode;
			this._eventBus = eventBus;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000275C File Offset: 0x0000095C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/UnstableCoreFragment");
			this._explosionRadiusInput = UQueryExtensions.Q<IntegerField>(this._root, "ExplosionRadiusInput", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._explosionRadiusInput, new EventCallback<ChangeEvent<int>>(this.OnRadiusChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027C0 File Offset: 0x000009C0
		public void ShowFragment(BaseComponent entity)
		{
			this._unstableCore = entity.GetComponent<UnstableCore>();
			if (this._unstableCore && this.Visible)
			{
				this._explosionRadiusInput.SetValueWithoutNotify(this._unstableCore.ExplosionRadius);
				this._root.ToggleDisplayStyle(true);
			}
			this._eventBus.Register(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000281C File Offset: 0x00000A1C
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._unstableCore = null;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002126 File Offset: 0x00000326
		public void UpdateFragment()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000283D File Offset: 0x00000A3D
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			if (this._unstableCore)
			{
				this._root.ToggleDisplayStyle(this.Visible);
				if (this.Visible)
				{
					this._explosionRadiusInput.SetValueWithoutNotify(this._unstableCore.ExplosionRadius);
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000287B File Offset: 0x00000A7B
		public bool Visible
		{
			get
			{
				return this._devModeManager.Enabled || this._mapEditorMode.IsMapEditor;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002898 File Offset: 0x00000A98
		public void OnRadiusChanged(ChangeEvent<int> evt)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._unstableCore))
			{
				int radius = Mathf.Clamp(evt.newValue, this._unstableCore.MinExplosionRadius, this._unstableCore.MaxExplosionRadius);
				this._unstableCore.SetRadius(radius);
			}
		}

		// Token: 0x04000026 RID: 38
		public readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x04000027 RID: 39
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000028 RID: 40
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000029 RID: 41
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400002A RID: 42
		public readonly EventBus _eventBus;

		// Token: 0x0400002B RID: 43
		public VisualElement _root;

		// Token: 0x0400002C RID: 44
		public UnstableCore _unstableCore;

		// Token: 0x0400002D RID: 45
		public IntegerField _explosionRadiusInput;
	}
}
