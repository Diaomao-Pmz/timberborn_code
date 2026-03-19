using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Debugging;
using Timberborn.Navigation;
using Timberborn.RootProviders;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.WalkingSystemUI
{
	// Token: 0x02000007 RID: 7
	public class WalkerDebugger : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public WalkerDebugger(EventBus eventBus, DebugModeManager debugModeManager, IAssetLoader assetLoader, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._debugModeManager = debugModeManager;
			this._assetLoader = assetLoader;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public void Load()
		{
			this._eventBus.Register(this);
			this._root = this._rootObjectProvider.CreateRootObject("WalkerDebugger");
			WalkerDebuggerSpec singleSpec = this._specService.GetSingleSpec<WalkerDebuggerSpec>();
			this._walkerGameObjectMarker = this.CreateMarker(singleSpec.WalkerGameObjectMarkerPath);
			this._walkerModelMarker = this.CreateMarker(singleSpec.WalkerModelMarkerPath);
			this._destinationMarker = this.CreateMarker(singleSpec.DestinationMarkerPath);
			this._cornerMarkerPrefab = this._assetLoader.Load<GameObject>(singleSpec.CornerMarkerPath);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public void LateUpdateSingleton()
		{
			if (this._debugModeManager.Enabled && this._walkerSelected)
			{
				this.UpdateMarkers();
				return;
			}
			this.HideMarkers();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E0 File Offset: 0x000003E0
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			Walker component = selectableObjectSelectedEvent.SelectableObject.GetComponent<Walker>();
			if (component)
			{
				this.UpdateSelectedWalker(component);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002208 File Offset: 0x00000408
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.HideMarkers();
			this._walkerSelected = false;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002218 File Offset: 0x00000418
		public Vector3 Destination
		{
			get
			{
				if (this._walker.PathCorners.IsEmpty())
				{
					return this._root.transform.position;
				}
				return this._walker.PathCorners.Last<PathCorner>().Position;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002268 File Offset: 0x00000468
		public GameObject CreateMarker(string name)
		{
			return Object.Instantiate<GameObject>(this._assetLoader.Load<GameObject>(name), this._root.transform);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002286 File Offset: 0x00000486
		public void UpdateSelectedWalker(Walker walker)
		{
			this._walker = walker;
			this._characterModel = this._walker.GetComponent<CharacterModel>();
			this._walkerSelected = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022A7 File Offset: 0x000004A7
		public void UpdateMarkers()
		{
			this.UpdateMarker();
			if (this.PathMarkersStale())
			{
				this.ResetPathMarkers();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C0 File Offset: 0x000004C0
		public void UpdateMarker()
		{
			Transform transform = this._walker.Transform;
			this._walkerGameObjectMarker.transform.SetPositionAndRotation(transform.position, transform.rotation);
			this._walkerGameObjectMarker.SetActive(true);
			this._walkerModelMarker.transform.SetPositionAndRotation(this._characterModel.Position, this._characterModel.Rotation);
			this._walkerModelMarker.SetActive(true);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002334 File Offset: 0x00000534
		public bool PathMarkersStale()
		{
			Vector3? destination = this._destination;
			Vector3 destination2 = this.Destination;
			return destination == null || (destination != null && destination.GetValueOrDefault() != destination2) || this._walker.PathCorners.Count != this._cornerMarkers.Count;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000239C File Offset: 0x0000059C
		public void HideMarkers()
		{
			this._walkerGameObjectMarker.SetActive(false);
			this._walkerModelMarker.SetActive(false);
			this._destinationMarker.SetActive(false);
			foreach (GameObject gameObject in this._cornerMarkers)
			{
				Object.Destroy(gameObject);
			}
			this._cornerMarkers.Clear();
			this._destination = null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002428 File Offset: 0x00000628
		public void ResetPathMarkers()
		{
			this.HideMarkers();
			this._destinationMarker.transform.position = this.Destination;
			this._destinationMarker.SetActive(true);
			foreach (PathCorner pathCorner in this._walker.PathCorners)
			{
				Vector3 vector = pathCorner.Position + Random.insideUnitSphere * 0.1f;
				this._cornerMarkers.Add(Object.Instantiate<GameObject>(this._cornerMarkerPrefab, vector, Quaternion.identity, this._root.transform));
			}
			this._destination = new Vector3?(this.Destination);
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x0400000A RID: 10
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000B RID: 11
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000C RID: 12
		public readonly ISpecService _specService;

		// Token: 0x0400000D RID: 13
		public bool _walkerSelected;

		// Token: 0x0400000E RID: 14
		public Walker _walker;

		// Token: 0x0400000F RID: 15
		public CharacterModel _characterModel;

		// Token: 0x04000010 RID: 16
		public readonly List<GameObject> _cornerMarkers = new List<GameObject>();

		// Token: 0x04000011 RID: 17
		public Vector3? _destination;

		// Token: 0x04000012 RID: 18
		public GameObject _walkerGameObjectMarker;

		// Token: 0x04000013 RID: 19
		public GameObject _walkerModelMarker;

		// Token: 0x04000014 RID: 20
		public GameObject _destinationMarker;

		// Token: 0x04000015 RID: 21
		public GameObject _cornerMarkerPrefab;

		// Token: 0x04000016 RID: 22
		public GameObject _root;
	}
}
