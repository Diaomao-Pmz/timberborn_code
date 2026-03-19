using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.Localization;
using Timberborn.Persistence;
using Timberborn.StatusSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Buildings
{
	// Token: 0x02000022 RID: 34
	public class PausableBuilding : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener, IUnfinishedStateListener, IPersistentEntity, IDuplicable<PausableBuilding>, IDuplicable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600010C RID: 268 RVA: 0x000041D0 File Offset: 0x000023D0
		// (remove) Token: 0x0600010D RID: 269 RVA: 0x00004208 File Offset: 0x00002408
		public event EventHandler PausedChanged;

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000423D File Offset: 0x0000243D
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004245 File Offset: 0x00002445
		public bool Paused { get; private set; }

		// Token: 0x06000110 RID: 272 RVA: 0x0000424E File Offset: 0x0000244E
		public PausableBuilding(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004273 File Offset: 0x00002473
		public bool IsDuplicable
		{
			get
			{
				return this.IsPausable();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000427C File Offset: 0x0000247C
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._blockObject = base.GetComponent<BlockObject>();
			base.GetComponents<IUnfinishedPausable>(this._unfinishedPausables);
			base.GetComponents<IFinishedPausable>(this._finishedPausables);
			this._pauseStatusToggle = StatusToggle.CreatePriorityStatusWithFloatingIcon("Pause", this._loc.T(PausableBuilding.PausedLocKey), 0f);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000042DE File Offset: 0x000024DE
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._pauseStatusToggle);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnEnterFinishedState()
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000042F1 File Offset: 0x000024F1
		public void OnExitFinishedState()
		{
			this.Resume();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnEnterUnfinishedState()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000042F1 File Offset: 0x000024F1
		public void OnExitUnfinishedState()
		{
			this.Resume();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000042F9 File Offset: 0x000024F9
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Paused)
			{
				entitySaver.GetComponent(PausableBuilding.PausableBuildingKey).Set(PausableBuilding.PausedKey, this.Paused);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004320 File Offset: 0x00002520
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(PausableBuilding.PausableBuildingKey, out objectLoader) && objectLoader.Get(PausableBuilding.PausedKey))
			{
				this.Pause();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000434F File Offset: 0x0000254F
		public void DuplicateFrom(PausableBuilding source)
		{
			if (this.IsPausable() && this._blockObject.IsFinished == source.GetComponent<BlockObject>().IsFinished)
			{
				if (source.Paused)
				{
					this.Pause();
					return;
				}
				this.Resume();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004386 File Offset: 0x00002586
		public bool IsPausable()
		{
			return (this._blockObject.IsUnfinished && !this._unfinishedPausables.IsEmpty<IUnfinishedPausable>()) || (this._blockObject.IsFinished && !this._finishedPausables.IsEmpty<IFinishedPausable>());
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000043C1 File Offset: 0x000025C1
		public void Pause()
		{
			if (!this.Paused)
			{
				this.Paused = true;
				this._blockableObject.Block(this);
				this._pauseStatusToggle.Activate();
				EventHandler pausedChanged = this.PausedChanged;
				if (pausedChanged == null)
				{
					return;
				}
				pausedChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000043FF File Offset: 0x000025FF
		public void Resume()
		{
			if (this.Paused)
			{
				this.Paused = false;
				this._blockableObject.Unblock(this);
				this._pauseStatusToggle.Deactivate();
				EventHandler pausedChanged = this.PausedChanged;
				if (pausedChanged == null)
				{
					return;
				}
				pausedChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000056 RID: 86
		public static readonly string PausedLocKey = "Status.Buildings.Paused";

		// Token: 0x04000057 RID: 87
		public static readonly ComponentKey PausableBuildingKey = new ComponentKey("PausableBuilding");

		// Token: 0x04000058 RID: 88
		public static readonly PropertyKey<bool> PausedKey = new PropertyKey<bool>("Paused");

		// Token: 0x0400005B RID: 91
		public readonly ILoc _loc;

		// Token: 0x0400005C RID: 92
		public BlockableObject _blockableObject;

		// Token: 0x0400005D RID: 93
		public BlockObject _blockObject;

		// Token: 0x0400005E RID: 94
		public readonly List<IUnfinishedPausable> _unfinishedPausables = new List<IUnfinishedPausable>();

		// Token: 0x0400005F RID: 95
		public readonly List<IFinishedPausable> _finishedPausables = new List<IFinishedPausable>();

		// Token: 0x04000060 RID: 96
		public StatusToggle _pauseStatusToggle;
	}
}
