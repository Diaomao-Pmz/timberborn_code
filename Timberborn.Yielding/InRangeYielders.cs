using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.Yielding
{
	// Token: 0x02000008 RID: 8
	public class InRangeYielders : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000019 RID: 25 RVA: 0x000024D8 File Offset: 0x000006D8
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x00002510 File Offset: 0x00000710
		public event EventHandler YieldersChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002548 File Offset: 0x00000748
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x00002580 File Offset: 0x00000780
		public event EventHandler<Yielder> YielderAdded;

		// Token: 0x0600001D RID: 29 RVA: 0x000025B5 File Offset: 0x000007B5
		public InRangeYielders(IBlockService blockService, EventBus eventBus)
		{
			this._blockService = blockService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025D6 File Offset: 0x000007D6
		public void Awake()
		{
			this._yieldRemovingBuilding = base.GetComponent<YieldRemovingBuilding>();
			this._yielderRetriever = base.GetComponent<IYielderRetriever>();
			this._buildingTerrainRange = base.GetComponent<BuildingTerrainRange>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025FC File Offset: 0x000007FC
		public void OnEnterFinishedState()
		{
			this._buildingTerrainRange.RangeChanged += this.OnRangeChanged;
			this._eventBus.Register(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002621 File Offset: 0x00000821
		public void OnExitFinishedState()
		{
			this._buildingTerrainRange.RangeChanged -= this.OnRangeChanged;
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002648 File Offset: 0x00000848
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			Yielder yielder;
			if (this._yielderRetriever.TryGetYielder(entityInitializedEvent.Entity, out yielder) && this._buildingTerrainRange.GetRange().Contains(yielder.Coordinates) && this.IsAllowed(yielder))
			{
				this._yieldersInRange.Add(yielder);
				EventHandler<Yielder> yielderAdded = this.YielderAdded;
				if (yielderAdded == null)
				{
					return;
				}
				yielderAdded(this, yielder);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026AC File Offset: 0x000008AC
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			Yielder item;
			if (this._yielderRetriever.TryGetYielder(entityDeletedEvent.Entity, out item) && this._yieldersInRange.Remove(item))
			{
				this.InvokeYieldersChanged();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026E4 File Offset: 0x000008E4
		public void GetYields(HashSet<string> yields)
		{
			this.UpdateYielders(false);
			for (int i = 0; i < this._yieldersInRange.Count; i++)
			{
				Yielder yielder = this._yieldersInRange[i];
				if (yielder)
				{
					yields.Add(yielder.YielderSpec.Yield.Id);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000273C File Offset: 0x0000093C
		public bool GetYielders(IList<Yielder> yielders, string templateName = null)
		{
			bool result = false;
			this.UpdateYielders(true);
			for (int i = 0; i < this._yieldersInRange.Count; i++)
			{
				Yielder yielder = this._yieldersInRange[i];
				if (InRangeYielders.IsValidYielder(templateName, yielder))
				{
					result = true;
					if (!yielder.Reservable.Reserved)
					{
						yielders.Add(yielder);
					}
				}
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002795 File Offset: 0x00000995
		public void OnRangeChanged(object sender, RangeChangedEventArgs rangeChangedEventArgs)
		{
			this._dirty = true;
			if (rangeChangedEventArgs.IsInitialChange)
			{
				this.UpdateYielders(true);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027B0 File Offset: 0x000009B0
		public void UpdateYielders(bool postEvent = true)
		{
			if (this._dirty)
			{
				this._yieldersInRange.Clear();
				foreach (Vector3Int coordinates in this._buildingTerrainRange.GetRange())
				{
					BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(coordinates);
					if (bottomObjectAt)
					{
						EntityComponent component = bottomObjectAt.GetComponent<EntityComponent>();
						Yielder yielder;
						if (component != null && component.Initialized && this._yielderRetriever.TryGetYielder(component, out yielder) && this.IsAllowed(yielder))
						{
							this._yieldersInRange.Add(yielder);
						}
					}
				}
				this._dirty = false;
				if (postEvent)
				{
					this.InvokeYieldersChanged();
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000287C File Offset: 0x00000A7C
		public static bool IsValidYielder(string templateName, Yielder yielder)
		{
			return yielder && (templateName == null || yielder.GetComponent<TemplateSpec>().IsNamed(templateName));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002899 File Offset: 0x00000A99
		public bool IsAllowed(Yielder yielder)
		{
			return this._yieldRemovingBuilding.IsAllowed(yielder.YielderSpec);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028AC File Offset: 0x00000AAC
		public void InvokeYieldersChanged()
		{
			EventHandler yieldersChanged = this.YieldersChanged;
			if (yieldersChanged == null)
			{
				return;
			}
			yieldersChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000011 RID: 17
		public readonly IBlockService _blockService;

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;

		// Token: 0x04000013 RID: 19
		public YieldRemovingBuilding _yieldRemovingBuilding;

		// Token: 0x04000014 RID: 20
		public IYielderRetriever _yielderRetriever;

		// Token: 0x04000015 RID: 21
		public BuildingTerrainRange _buildingTerrainRange;

		// Token: 0x04000016 RID: 22
		public readonly List<Yielder> _yieldersInRange = new List<Yielder>();

		// Token: 0x04000017 RID: 23
		public bool _dirty;
	}
}
