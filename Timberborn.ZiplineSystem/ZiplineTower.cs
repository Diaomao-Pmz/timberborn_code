using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001C RID: 28
	public class ZiplineTower : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, IFinishedStateListener, IPersistentEntity, IPostInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x000041B8 File Offset: 0x000023B8
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x000041F0 File Offset: 0x000023F0
		public event EventHandler ConnectionTargetsChanged;

		// Token: 0x060000D9 RID: 217 RVA: 0x00004225 File Offset: 0x00002425
		public ZiplineTower(ZiplineTowerRegistry ziplineTowerRegistry, ZiplineConnectionService ziplineConnectionService, ReferenceSerializer referenceSerializer)
		{
			this._ziplineTowerRegistry = ziplineTowerRegistry;
			this._ziplineConnectionService = ziplineConnectionService;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004242 File Offset: 0x00002442
		public bool HasFreeSlots
		{
			get
			{
				return this._connectionTargets.Count < this._ziplineTowerSpec.MaxConnections;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000425C File Offset: 0x0000245C
		public ReadOnlyList<ZiplineTower> ConnectionTargets
		{
			get
			{
				return this._connectionTargets.AsReadOnlyList<ZiplineTower>();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004269 File Offset: 0x00002469
		public bool IsActive
		{
			get
			{
				return this._blockObject.IsFinished;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004276 File Offset: 0x00002476
		public int MaxConnections
		{
			get
			{
				return this._ziplineTowerSpec.MaxConnections;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004283 File Offset: 0x00002483
		public int MaxDistance
		{
			get
			{
				return this._ziplineTowerSpec.MaxDistance;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004290 File Offset: 0x00002490
		public ImmutableArray<Vector3Int> UnobstructedCoordinates
		{
			get
			{
				return this._ziplineTowerSpec.UnobstructedCoordinates;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000429D File Offset: 0x0000249D
		public Vector3 CableAnchorPoint
		{
			get
			{
				return this._blockObject.TransformCoordinates(CoordinateSystem.WorldToGrid(this._ziplineTowerSpec.CableAnchorPoint));
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000042BA File Offset: 0x000024BA
		public Vector3Int CableAnchorPointInt
		{
			get
			{
				return this._blockObject.TransformCoordinates(CoordinateSystem.WorldToGridInt(this._ziplineTowerSpec.CableAnchorPoint));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000042D7 File Offset: 0x000024D7
		public void Awake()
		{
			this._ziplineTowerSpec = base.GetComponent<ZiplineTowerSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._connectionTargets = new List<ZiplineTower>(this._ziplineTowerSpec.MaxConnections);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004307 File Offset: 0x00002507
		public void InitializeEntity()
		{
			this._ziplineTowerRegistry.Add(this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004318 File Offset: 0x00002518
		public void PostInitializeEntity()
		{
			if (this._loadedConnectionTargets != null)
			{
				foreach (ZiplineTower ziplineTower in this._loadedConnectionTargets)
				{
					if (ziplineTower && !ziplineTower.GetComponent<EntityComponent>().Deleted && !this._connectionTargets.Contains(ziplineTower))
					{
						if (this._ziplineConnectionService.CanBeConnected(this, ziplineTower))
						{
							this._ziplineConnectionService.Connect(this, ziplineTower);
						}
						else
						{
							Vector3Int coordinates = ziplineTower.GetComponent<BlockObject>().Coordinates;
							Debug.LogWarning("Unable to recreate Loaded zipline connection between" + string.Format(" {0} ({1}) ", base.Name, this._blockObject.Coordinates) + string.Format("and {0} ({1})", ziplineTower.Name, coordinates));
						}
					}
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000440C File Offset: 0x0000260C
		public void DeleteEntity()
		{
			this._ziplineTowerRegistry.Remove(this);
			while (this._connectionTargets.Count > 0)
			{
				ZiplineTower otherZiplineTower = this._connectionTargets[0];
				this._ziplineConnectionService.Disconnect(this, otherZiplineTower);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000444F File Offset: 0x0000264F
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(ZiplineTower.ZiplineTowerKey).Set<ZiplineTower>(ZiplineTower.ConnectionTargetsKey, this._connectionTargets, this._referenceSerializer.Of<ZiplineTower>());
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004478 File Offset: 0x00002678
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ZiplineTower.ZiplineTowerKey);
			this._loadedConnectionTargets = component.Get<ZiplineTower>(ZiplineTower.ConnectionTargetsKey, this._referenceSerializer.Of<ZiplineTower>());
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000044B0 File Offset: 0x000026B0
		public void OnEnterFinishedState()
		{
			foreach (ZiplineTower ziplineTower in this._connectionTargets)
			{
				if (ziplineTower.IsActive)
				{
					this._ziplineConnectionService.ActivateConnection(this, ziplineTower);
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004514 File Offset: 0x00002714
		public void OnExitFinishedState()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004516 File Offset: 0x00002716
		public bool IsConnectedTo(ZiplineTower ziplineTower)
		{
			return this._connectionTargets.Contains(ziplineTower);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004524 File Offset: 0x00002724
		public void AddConnection(ZiplineTower connection)
		{
			this._connectionTargets.Add(connection);
			this.InvokeConnectionTargetsChanged();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004538 File Offset: 0x00002738
		public void RemoveConnection(ZiplineTower connection)
		{
			this._connectionTargets.Remove(connection);
			this.InvokeConnectionTargetsChanged();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000454D File Offset: 0x0000274D
		public void InvokeConnectionTargetsChanged()
		{
			EventHandler connectionTargetsChanged = this.ConnectionTargetsChanged;
			if (connectionTargetsChanged == null)
			{
				return;
			}
			connectionTargetsChanged(this, EventArgs.Empty);
		}

		// Token: 0x0400004F RID: 79
		public static readonly ComponentKey ZiplineTowerKey = new ComponentKey("ZiplineTower");

		// Token: 0x04000050 RID: 80
		public static readonly ListKey<ZiplineTower> ConnectionTargetsKey = new ListKey<ZiplineTower>("ConnectionTargets");

		// Token: 0x04000052 RID: 82
		public readonly ZiplineTowerRegistry _ziplineTowerRegistry;

		// Token: 0x04000053 RID: 83
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x04000054 RID: 84
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000055 RID: 85
		public ZiplineTowerSpec _ziplineTowerSpec;

		// Token: 0x04000056 RID: 86
		public BlockObject _blockObject;

		// Token: 0x04000057 RID: 87
		public List<ZiplineTower> _connectionTargets;

		// Token: 0x04000058 RID: 88
		public List<ZiplineTower> _loadedConnectionTargets;
	}
}
