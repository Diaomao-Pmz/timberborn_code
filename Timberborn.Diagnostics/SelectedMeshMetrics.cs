using System;
using Timberborn.Debugging;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Diagnostics
{
	// Token: 0x0200000D RID: 13
	public class SelectedMeshMetrics : ILoadableSingleton
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002817 File Offset: 0x00000A17
		public MeshMetrics MeshMetrics { get; private set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002820 File Offset: 0x00000A20
		public SelectedMeshMetrics(EventBus eventBus, MeshMetricsRetriever meshMetricsRetriever, DebugModeManager debugModeManager)
		{
			this._eventBus = eventBus;
			this._meshMetricsRetriever = meshMetricsRetriever;
			this._debugModeManager = debugModeManager;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000283D File Offset: 0x00000A3D
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000284C File Offset: 0x00000A4C
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			if (this._debugModeManager.Enabled)
			{
				SelectableObject selectableObject = selectableObjectSelectedEvent.SelectableObject;
				this.MeshMetrics = (selectableObject ? this._meshMetricsRetriever.GetMeshMetrics(selectableObject.GameObject) : null);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000288F File Offset: 0x00000A8F
		[OnEvent]
		public void OnSelectableObjectUnselectedEvent(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.MeshMetrics = null;
		}

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly MeshMetricsRetriever _meshMetricsRetriever;

		// Token: 0x04000020 RID: 32
		public readonly DebugModeManager _debugModeManager;
	}
}
