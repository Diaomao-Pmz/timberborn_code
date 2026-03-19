using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.CursorToolSystem;
using Timberborn.Debugging;
using Timberborn.Navigation;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingsUI
{
	// Token: 0x02000004 RID: 4
	public class AccessibleDebugger : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AccessibleDebugger(EventBus eventBus, CursorDebugger cursorDebugger, INavigationService navigationService, MeshDrawerFactory meshDrawerFactory, DebugModeManager debugModeManager, IAssetLoader assetLoader)
		{
			this._eventBus = eventBus;
			this._cursorDebugger = cursorDebugger;
			this._navigationService = navigationService;
			this._meshDrawerFactory = meshDrawerFactory;
			this._debugModeManager = debugModeManager;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002100 File Offset: 0x00000300
		public void Load()
		{
			this._eventBus.Register(this);
			this._meshDrawer = this._meshDrawerFactory.Create(this._assetLoader.Load<Mesh>(AccessibleDebugger.MarkerMeshPath), this._assetLoader.Load<Material>(AccessibleDebugger.MarkerMaterialPath));
			this._debugModeEnabled = this._debugModeManager.Enabled;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215B File Offset: 0x0000035B
		public void UpdateSingleton()
		{
			if (this._debugModeEnabled && this._selectedAccessible)
			{
				this.DrawSelectedAccessible();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002178 File Offset: 0x00000378
		[OnEvent]
		public void OnDebugModeToggled(DebugModeToggledEvent debugModeToggledEvent)
		{
			this._debugModeEnabled = debugModeToggledEvent.Enabled;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002186 File Offset: 0x00000386
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			this._selectedAccessible = selectableObjectSelectedEvent.SelectableObject.GetEnabledComponent<Accessible>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002199 File Offset: 0x00000399
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this._selectedAccessible = null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A4 File Offset: 0x000003A4
		public void DrawSelectedAccessible()
		{
			Vector3 position = this._cursorDebugger.Position;
			foreach (Vector3 accessPosition in this._selectedAccessible.Accesses)
			{
				this.DrawAccessAndPath(accessPosition, position);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220C File Offset: 0x0000040C
		public void DrawAccessAndPath(Vector3 accessPosition, Vector3 end)
		{
			if (this._navigationService.DestinationIsReachable(accessPosition, end))
			{
				this.DrawPath(accessPosition, end);
				this.DrawAccess(accessPosition, Color.blue);
				return;
			}
			this.DrawAccess(accessPosition, Color.red);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000223E File Offset: 0x0000043E
		public void DrawAccess(Vector3 position, Color color)
		{
			this._meshDrawer.DrawAtPosition(position, Quaternion.identity, color);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002252 File Offset: 0x00000452
		public void DrawPath(Vector3 start, Vector3 end)
		{
			if (this._navigationService.FindPath(start, end, this._pathCorners))
			{
				this.DrawPath(Color.blue);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002274 File Offset: 0x00000474
		public void DrawPath(Color color)
		{
			for (int i = 0; i < this._pathCorners.Count - 1; i++)
			{
				PathCorner pathCorner = this._pathCorners[i];
				PathCorner pathCorner2 = this._pathCorners[i + 1];
				Debug.DrawLine(pathCorner.Position, pathCorner2.Position, color, 0f, false);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string MarkerMeshPath = "Markers/DebuggingSphere";

		// Token: 0x04000007 RID: 7
		public static readonly string MarkerMaterialPath = "Markers/AccessibleDebuggerMarker";

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly CursorDebugger _cursorDebugger;

		// Token: 0x0400000A RID: 10
		public readonly INavigationService _navigationService;

		// Token: 0x0400000B RID: 11
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x0400000C RID: 12
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x0400000D RID: 13
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000E RID: 14
		public Accessible _selectedAccessible;

		// Token: 0x0400000F RID: 15
		public readonly List<PathCorner> _pathCorners = new List<PathCorner>();

		// Token: 0x04000010 RID: 16
		public MeshDrawer _meshDrawer;

		// Token: 0x04000011 RID: 17
		public bool _debugModeEnabled;
	}
}
