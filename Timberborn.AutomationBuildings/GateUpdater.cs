using System;
using System.Collections.Generic;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200001B RID: 27
	public class GateUpdater : ILateUpdatableSingleton, ISingletonNavMeshListener
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000421C File Offset: 0x0000241C
		public GateUpdater(GateConflictDetector gateConflictDetector)
		{
			this._gateConflictDetector = gateConflictDetector;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004270 File Offset: 0x00002470
		public void LateUpdateSingleton()
		{
			if (this._hasScheduledGates)
			{
				this.CloseScheduledGates();
				this.OpenScheduledGates();
				this._hasScheduledUnblocking = true;
				this._hasScheduledGates = false;
			}
			if (this._hasScheduledUnblocking)
			{
				this.TryOpenConflictedGates();
				this._hasScheduledUnblocking = false;
			}
			this._openGateCrossings.Clear();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000042BF File Offset: 0x000024BF
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._hasScheduledUnblocking = true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000042C8 File Offset: 0x000024C8
		public void ScheduleToOpen(Gate gate)
		{
			this._gatesScheduledToOpen.Add(gate);
			this._gatesScheduledToClose.Remove(gate);
			this._hasScheduledGates = true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000042EB File Offset: 0x000024EB
		public void ScheduleToClose(Gate gate)
		{
			this._gatesScheduledToClose.Add(gate);
			this._gatesScheduledToOpen.Remove(gate);
			this._hasScheduledGates = true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000430E File Offset: 0x0000250E
		public void Remove(Gate gate)
		{
			this._gatesScheduledToClose.Remove(gate);
			this._gatesScheduledToOpen.Remove(gate);
			this.RemoveGateFromConflicted(gate);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004334 File Offset: 0x00002534
		public void CloseScheduledGates()
		{
			foreach (Gate gate in this._gatesScheduledToClose)
			{
				this.TryCloseGate(gate);
			}
			this._gatesScheduledToClose.Clear();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004394 File Offset: 0x00002594
		public void OpenScheduledGates()
		{
			foreach (Gate gate in this._gatesScheduledToOpen)
			{
				this.TryOpenGate(gate);
			}
			this._gatesScheduledToOpen.Clear();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000043F4 File Offset: 0x000025F4
		public void TryCloseGate(Gate gate)
		{
			if (!gate.GetComponent<GateNavMeshBlocker>().NavMeshBlocked)
			{
				gate.BlockNavMesh();
			}
			this.RemoveGateFromConflicted(gate);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004410 File Offset: 0x00002610
		public void TryOpenGate(Gate gate)
		{
			GateNavMeshBlocker component = gate.GetComponent<GateNavMeshBlocker>();
			if (component.NavMeshBlocked)
			{
				GatePlacement component2 = gate.GetComponent<GatePlacement>();
				if (this._gateConflictDetector.CanOpenGateWithoutConflict(component2.Start, component2.End, component2.Center, this._openGateCrossings))
				{
					gate.UnblockNavMesh();
					this.RemoveGateFromConflicted(gate);
					this.AddToOpenGateCrossings(component2);
					return;
				}
				if (!component.NavMeshBlocked)
				{
					gate.BlockNavMesh();
				}
				this.AddGateToConflicted(gate);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004482 File Offset: 0x00002682
		public void AddGateToConflicted(Gate gate)
		{
			gate.EnableConflict();
			this._gatesWithConflict.Add(gate);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004497 File Offset: 0x00002697
		public void RemoveGateFromConflicted(Gate gate)
		{
			gate.DisableConflict();
			this._gatesWithConflict.Remove(gate);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000044AC File Offset: 0x000026AC
		public void TryOpenConflictedGates()
		{
			if (this._gatesWithConflict.Count > 0)
			{
				this._gatesWithConflictCache.AddRange(this._gatesWithConflict);
				this._gatesWithConflict.Clear();
				foreach (Gate gate in this._gatesWithConflictCache)
				{
					this.TryOpenGate(gate);
				}
				this._gatesWithConflictCache.Clear();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004534 File Offset: 0x00002734
		public void AddToOpenGateCrossings(GatePlacement gatePlacement)
		{
			this._openGateCrossings[gatePlacement.Start] = gatePlacement.End;
			this._openGateCrossings[gatePlacement.End] = gatePlacement.Start;
		}

		// Token: 0x04000071 RID: 113
		public readonly GateConflictDetector _gateConflictDetector;

		// Token: 0x04000072 RID: 114
		public readonly HashSet<Gate> _gatesScheduledToOpen = new HashSet<Gate>();

		// Token: 0x04000073 RID: 115
		public readonly HashSet<Gate> _gatesScheduledToClose = new HashSet<Gate>();

		// Token: 0x04000074 RID: 116
		public readonly HashSet<Gate> _gatesWithConflict = new HashSet<Gate>();

		// Token: 0x04000075 RID: 117
		public readonly List<Gate> _gatesWithConflictCache = new List<Gate>();

		// Token: 0x04000076 RID: 118
		public readonly Dictionary<Vector3Int, Vector3Int> _openGateCrossings = new Dictionary<Vector3Int, Vector3Int>();

		// Token: 0x04000077 RID: 119
		public bool _hasScheduledGates;

		// Token: 0x04000078 RID: 120
		public bool _hasScheduledUnblocking;
	}
}
