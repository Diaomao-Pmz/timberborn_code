using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.Navigation;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200000D RID: 13
	public class NavMeshObserver : TickableComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, INavMeshListener, IDeadNeededComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001B RID: 27 RVA: 0x000023A0 File Offset: 0x000005A0
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000023D8 File Offset: 0x000005D8
		public event EventHandler PlacedOnNavMesh;

		// Token: 0x0600001D RID: 29 RVA: 0x0000240D File Offset: 0x0000060D
		public NavMeshObserver(WalkerService walkerService, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._walkerService = walkerService;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000242E File Offset: 0x0000062E
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
			this._characterModel = base.GetComponent<CharacterModel>();
			base.GetComponents<INavMeshProximityValidator>(this._navMeshProximityValidators);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002454 File Offset: 0x00000654
		public override void StartTickable()
		{
			this.ReactToNewNavMesh();
			this._navMeshUpdateBounds = null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002468 File Offset: 0x00000668
		public override void Tick()
		{
			BoundingBox? navMeshUpdateBounds = this._navMeshUpdateBounds;
			if (navMeshUpdateBounds != null)
			{
				BoundingBox valueOrDefault = navMeshUpdateBounds.GetValueOrDefault();
				BoundingBox currentPathBounds = this._walker.CurrentPathBounds;
				if (valueOrDefault.Intersects(currentPathBounds) || valueOrDefault.Contains(NavigationCoordinateSystem.WorldToGridInt(base.Transform.position)))
				{
					this.ReactToNewNavMesh();
				}
				this._navMeshUpdateBounds = null;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024CF File Offset: 0x000006CF
		public void InitializeEntity()
		{
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024DD File Offset: 0x000006DD
		public void DeleteEntity()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024EB File Offset: 0x000006EB
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._navMeshUpdateBounds = new BoundingBox?(navMeshUpdate.Bounds);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FF File Offset: 0x000006FF
		public void Disable()
		{
			base.DisableComponent();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002507 File Offset: 0x00000707
		public void ReactToNewNavMesh()
		{
			if (base.Enabled)
			{
				this.PlaceOnNavMesh();
				this._walker.RefreshPath();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002524 File Offset: 0x00000724
		public void PlaceOnNavMesh()
		{
			if (!this.IsOnNavMesh())
			{
				Vector3 position = this._walkerService.ClosestPositionOnNavMesh(base.Transform.position);
				base.Transform.position = position;
				this._characterModel.Position = position;
				EventHandler placedOnNavMesh = this.PlacedOnNavMesh;
				if (placedOnNavMesh == null)
				{
					return;
				}
				placedOnNavMesh(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002580 File Offset: 0x00000780
		public bool IsOnNavMesh()
		{
			using (List<INavMeshProximityValidator>.Enumerator enumerator = this._navMeshProximityValidators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOnNavMesh())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000010 RID: 16
		public readonly WalkerService _walkerService;

		// Token: 0x04000011 RID: 17
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x04000012 RID: 18
		public Walker _walker;

		// Token: 0x04000013 RID: 19
		public CharacterModel _characterModel;

		// Token: 0x04000014 RID: 20
		public BoundingBox? _navMeshUpdateBounds;

		// Token: 0x04000015 RID: 21
		public readonly List<INavMeshProximityValidator> _navMeshProximityValidators = new List<INavMeshProximityValidator>();
	}
}
