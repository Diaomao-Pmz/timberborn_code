using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000007 RID: 7
	public class ConnectionCandidates : ILoadableSingleton, IUpdatableSingleton, ISingletonPreviewNavMeshListener, ISingletonInstantNavMeshListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ConnectionCandidates(ZiplineTowerRegistry ziplineTowerRegistry, ZiplineConnectionService ziplineConnectionService, IAssetLoader assetLoader, ISpecService specService, Highlighter highlighter)
		{
			this._ziplineTowerRegistry = ziplineTowerRegistry;
			this._ziplineConnectionService = ziplineConnectionService;
			this._assetLoader = assetLoader;
			this._specService = specService;
			this._highlighter = highlighter;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000214E File Offset: 0x0000034E
		public void Load()
		{
			this._ziplineSystemColorsSpec = this._specService.GetSingleSpec<ZiplineSystemColorsSpec>();
			this._markerPrefab = this._assetLoader.Load<GameObject>("Markers/ZiplineMarker");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002177 File Offset: 0x00000377
		public void EnableAndDrawMarkers(ZiplineTower origin)
		{
			this.EnableInternal(origin, true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002181 File Offset: 0x00000381
		public void Enable(ZiplineTower origin)
		{
			this.EnableInternal(origin, false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218B File Offset: 0x0000038B
		public void Disable()
		{
			this._highlighter.UnhighlightAllPrimary();
			this._origin = null;
			this.ClearCandidates();
			this._enabled = false;
			this._shouldUpdateCandidates = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B3 File Offset: 0x000003B3
		public bool Contains(ZiplineTower ziplineTower)
		{
			return this._candidates.Contains(ziplineTower);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C1 File Offset: 0x000003C1
		public void UpdateSingleton()
		{
			if (this._enabled && this._shouldUpdateCandidates)
			{
				this.UpdateCandidates();
				this._shouldUpdateCandidates = false;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._shouldUpdateCandidates = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E0 File Offset: 0x000003E0
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._shouldUpdateCandidates = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E9 File Offset: 0x000003E9
		public void UpdateCandidates()
		{
			this.ClearCandidates();
			this.AddCandidates();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F7 File Offset: 0x000003F7
		public void EnableInternal(ZiplineTower origin, bool drawMarkers)
		{
			this._origin = origin;
			this._shouldUpdateCandidates = true;
			this._enabled = true;
			this._highlighter.HighlightPrimary(this._origin, this._ziplineSystemColorsSpec.OriginColor);
			this._drawMarkers = drawMarkers;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002234 File Offset: 0x00000434
		public void ClearCandidates()
		{
			this._candidates.Clear();
			foreach (GameObject gameObject in this._markers)
			{
				Object.Destroy(gameObject.gameObject);
			}
			this._markers.Clear();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A0 File Offset: 0x000004A0
		public void AddCandidates()
		{
			foreach (ZiplineTower ziplineTower in this._ziplineTowerRegistry.ZiplineTowers)
			{
				if (this._ziplineConnectionService.CanBeConnected(this._origin, ziplineTower))
				{
					this._candidates.Add(ziplineTower);
					if (this._drawMarkers)
					{
						this.CreateMarker(ziplineTower);
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002324 File Offset: 0x00000524
		public void CreateMarker(ZiplineTower ziplineTower)
		{
			Vector3 vector = CoordinateSystem.GridToWorld(ziplineTower.CableAnchorPoint);
			Vector3 vector2 = vector - CoordinateSystem.GridToWorld(this._origin.CableAnchorPoint);
			vector2.y = 0f;
			Quaternion quaternion = Quaternion.LookRotation(vector2, Vector3.up);
			GameObject item = Object.Instantiate<GameObject>(this._markerPrefab, vector + new Vector3(0f, 0.12f, 0f), quaternion);
			this._markers.Add(item);
		}

		// Token: 0x04000008 RID: 8
		public readonly ZiplineTowerRegistry _ziplineTowerRegistry;

		// Token: 0x04000009 RID: 9
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x0400000A RID: 10
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000B RID: 11
		public readonly ISpecService _specService;

		// Token: 0x0400000C RID: 12
		public readonly Highlighter _highlighter;

		// Token: 0x0400000D RID: 13
		public ZiplineSystemColorsSpec _ziplineSystemColorsSpec;

		// Token: 0x0400000E RID: 14
		public GameObject _markerPrefab;

		// Token: 0x0400000F RID: 15
		public readonly List<ZiplineTower> _candidates = new List<ZiplineTower>();

		// Token: 0x04000010 RID: 16
		public readonly List<GameObject> _markers = new List<GameObject>();

		// Token: 0x04000011 RID: 17
		public ZiplineTower _origin;

		// Token: 0x04000012 RID: 18
		public bool _enabled;

		// Token: 0x04000013 RID: 19
		public bool _shouldUpdateCandidates;

		// Token: 0x04000014 RID: 20
		public bool _drawMarkers;
	}
}
