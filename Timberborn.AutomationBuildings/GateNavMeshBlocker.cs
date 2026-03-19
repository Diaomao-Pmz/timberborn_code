using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000016 RID: 22
	public class GateNavMeshBlocker : BaseComponent, IAwakableComponent, IPreInitializableEntity, IPersistentEntity
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003C19 File Offset: 0x00001E19
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003C21 File Offset: 0x00001E21
		public bool NavMeshBlocked { get; private set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x00003C2A File Offset: 0x00001E2A
		public GateNavMeshBlocker(INavMeshService navMeshService, NavMeshGroupService navMeshGroupService)
		{
			this._navMeshService = navMeshService;
			this._navMeshGroupService = navMeshGroupService;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003C40 File Offset: 0x00001E40
		public void Awake()
		{
			this._gatePlacement = base.GetComponent<GatePlacement>();
			this._buildingNavMesh = base.GetComponent<BuildingNavMesh>();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003C5A File Offset: 0x00001E5A
		public void PreInitializeEntity()
		{
			if (this.NavMeshBlocked)
			{
				this.Block();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003C6A File Offset: 0x00001E6A
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(GateNavMeshBlocker.ComponentKey).Set(GateNavMeshBlocker.NavMeshBlockedKey, this.NavMeshBlocked);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003C88 File Offset: 0x00001E88
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(GateNavMeshBlocker.ComponentKey);
			this.NavMeshBlocked = component.Get(GateNavMeshBlocker.NavMeshBlockedKey);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public void Block()
		{
			this.SetPathBlockage(true);
			this.SetTraverseCost(true);
			this.NavMeshBlocked = true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public void Unblock()
		{
			this.SetPathBlockage(false);
			this.SetTraverseCost(false);
			this.NavMeshBlocked = false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public void SetPathBlockage(bool isBlocked)
		{
			if (isBlocked)
			{
				this._buildingNavMesh.BlockAndRemoveFromNavMesh();
				return;
			}
			this._buildingNavMesh.UnblockAndAddToNavMesh();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003CFC File Offset: 0x00001EFC
		public void SetTraverseCost(bool isExpensive)
		{
			if (isExpensive != this._expensiveTraverseCostSet)
			{
				Vector3Int start = this._gatePlacement.Start;
				Vector3Int end = this._gatePlacement.End;
				Vector3Int center = this._gatePlacement.Center;
				if (isExpensive)
				{
					this._navMeshService.RemoveEdge(this.GetNormalEdge(center, start));
					this._navMeshService.RemoveEdge(this.GetNormalEdge(center, end));
					this._navMeshService.AddEdge(this.GetExpensiveEdge(center, start));
					this._navMeshService.AddEdge(this.GetExpensiveEdge(center, end));
				}
				else
				{
					this._navMeshService.AddEdge(this.GetNormalEdge(center, start));
					this._navMeshService.AddEdge(this.GetNormalEdge(center, end));
					this._navMeshService.RemoveEdge(this.GetExpensiveEdge(center, start));
					this._navMeshService.RemoveEdge(this.GetExpensiveEdge(center, end));
				}
				this._expensiveTraverseCostSet = isExpensive;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003DDD File Offset: 0x00001FDD
		public NavMeshEdge GetNormalEdge(Vector3Int start, Vector3Int end)
		{
			return this.GetEdge(start, end, 1f);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003DEC File Offset: 0x00001FEC
		public NavMeshEdge GetExpensiveEdge(Vector3Int start, Vector3Int end)
		{
			return this.GetEdge(start, end, (float)WalkerLimits.BlockingEdgeCost);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003DFC File Offset: 0x00001FFC
		public NavMeshEdge GetEdge(Vector3Int start, Vector3Int end, float cost)
		{
			return NavMeshEdge.CreateGrouped(start, end, this._navMeshGroupService.GetDefaultGroupId(), false, cost);
		}

		// Token: 0x0400005A RID: 90
		public static readonly ComponentKey ComponentKey = new ComponentKey("GateNavMeshBlocker");

		// Token: 0x0400005B RID: 91
		public static readonly PropertyKey<bool> NavMeshBlockedKey = new PropertyKey<bool>("NavMeshBlocked");

		// Token: 0x0400005D RID: 93
		public readonly INavMeshService _navMeshService;

		// Token: 0x0400005E RID: 94
		public readonly NavMeshGroupService _navMeshGroupService;

		// Token: 0x0400005F RID: 95
		public GatePlacement _gatePlacement;

		// Token: 0x04000060 RID: 96
		public BuildingNavMesh _buildingNavMesh;

		// Token: 0x04000061 RID: 97
		public bool _expensiveTraverseCostSet;
	}
}
