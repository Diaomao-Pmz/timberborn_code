using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000007 RID: 7
	public class Enterable : BaseComponent, IAwakableComponent, IInitializableEntity, IUnfinishedStateListener, IFinishedStateListener, IRegisteredComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler<EntererAddedEventArgs> EntererAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler<EntererRemovedEventArgs> EntererRemoved;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021E5 File Offset: 0x000003E5
		public EnterableSpec EnterableSpec { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021EE File Offset: 0x000003EE
		public int Capacity
		{
			get
			{
				if (!this._blockObject.IsFinished)
				{
					return this.EnterableSpec.CapacityUnfinished;
				}
				return this.EnterableSpec.CapacityFinished;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002214 File Offset: 0x00000414
		public int NumberOfEnterersInside
		{
			get
			{
				return this._enterersInside.Count;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002221 File Offset: 0x00000421
		public bool CanReserveSlot
		{
			get
			{
				return base.Enabled && (!this.LimitedCapacity || this.NumberOfReservedSlots < this.Capacity);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002245 File Offset: 0x00000445
		public bool CanEnter
		{
			get
			{
				return base.Enabled && (!this.LimitedCapacity || this._enterersInside.Count < this.Capacity);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000226E File Offset: 0x0000046E
		public Quaternion ExitWorldSpaceRotation
		{
			get
			{
				return this._blockObject.PositionedEntrance.Direction2D.Across().ToWorldSpaceRotation();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000228A File Offset: 0x0000048A
		public IEnumerable<Enterer> EnterersInside
		{
			get
			{
				return this._enterersInside.AsReadOnlyEnumerable<Enterer>();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002297 File Offset: 0x00000497
		public void Awake()
		{
			this.EnterableSpec = base.GetComponent<EnterableSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			base.DisableComponent();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B7 File Offset: 0x000004B7
		public void InitializeEntity()
		{
			if (this.ShouldOperate)
			{
				base.EnableComponent();
				return;
			}
			base.DisableComponent();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022CE File Offset: 0x000004CE
		public void OnEnterUnfinishedState()
		{
			if (this.ShouldOperate)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022DE File Offset: 0x000004DE
		public void OnExitUnfinishedState()
		{
			if (this.ShouldOperate)
			{
				this.ForceRemoveEveryone();
				base.DisableComponent();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CE File Offset: 0x000004CE
		public void OnEnterFinishedState()
		{
			if (this.ShouldOperate)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022DE File Offset: 0x000004DE
		public void OnExitFinishedState()
		{
			if (this.ShouldOperate)
			{
				this.ForceRemoveEveryone();
				base.DisableComponent();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F4 File Offset: 0x000004F4
		public void Add(Enterer enterer)
		{
			if (!this.CanEnter)
			{
				throw new InvalidOperationException(string.Format("Can't add enterer {0} to {1}.", enterer, base.Name));
			}
			this._enterersInside.Add(enterer);
			EventHandler<EntererAddedEventArgs> entererAdded = this.EntererAdded;
			if (entererAdded == null)
			{
				return;
			}
			entererAdded(this, new EntererAddedEventArgs(enterer));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002344 File Offset: 0x00000544
		public void Remove(Enterer enterer)
		{
			if (!this._enterersInside.Contains(enterer))
			{
				throw new ArgumentException(string.Format("Can't remove enterer {0} from {1} ", enterer, base.Name) + "because it's not inside.");
			}
			this._enterersInside.Remove(enterer);
			EventHandler<EntererRemovedEventArgs> entererRemoved = this.EntererRemoved;
			if (entererRemoved == null)
			{
				return;
			}
			entererRemoved(this, new EntererRemovedEventArgs(enterer));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A4 File Offset: 0x000005A4
		public void ReserveSlot()
		{
			this._numberOfIncomingVisitors++;
			this.ValidateReservedSlots();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023BA File Offset: 0x000005BA
		public void UnreserveSlot()
		{
			this._numberOfIncomingVisitors--;
			this.ValidateReservedSlots();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023D0 File Offset: 0x000005D0
		public bool LimitedCapacity
		{
			get
			{
				if (!this._blockObject.IsFinished)
				{
					return this.EnterableSpec.LimitedCapacityUnfinished;
				}
				return this.EnterableSpec.LimitedCapacityFinished;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023F8 File Offset: 0x000005F8
		public bool ShouldOperate
		{
			get
			{
				return this.EnterableSpec.OperatingState == OperatingState.FinishedAndUnfinished || (this._blockObject.IsFinished && this.EnterableSpec.OperatingState == OperatingState.Finished) || (!this._blockObject.IsFinished && this.EnterableSpec.OperatingState == OperatingState.Unfinished);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000244C File Offset: 0x0000064C
		public int NumberOfReservedSlots
		{
			get
			{
				return this._numberOfIncomingVisitors + this._enterersInside.Count;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002460 File Offset: 0x00000660
		public void ForceRemoveEveryone()
		{
			foreach (Enterer enterer in this._enterersInside.ToArray<Enterer>())
			{
				enterer.Abandon();
				this.Remove(enterer);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002498 File Offset: 0x00000698
		public void ValidateReservedSlots()
		{
			if (this.LimitedCapacity && (this.NumberOfReservedSlots < 0 || this.NumberOfReservedSlots > this.Capacity))
			{
				Debug.LogError(string.Format("Reserved slots ({0}) of {1} ", this.NumberOfReservedSlots, base.Name) + string.Format("are out of bounds (0 to {0})!", this.Capacity));
			}
		}

		// Token: 0x0400000B RID: 11
		public BlockObject _blockObject;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<Enterer> _enterersInside = new HashSet<Enterer>();

		// Token: 0x0400000D RID: 13
		public int _numberOfIncomingVisitors;
	}
}
