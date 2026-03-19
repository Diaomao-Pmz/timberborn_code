using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.ReservableSystem;
using Timberborn.SingletonSystem;
using Timberborn.Yielding;

namespace Timberborn.Forestry
{
	// Token: 0x02000014 RID: 20
	public class TreeCutter : BaseComponent, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002960 File Offset: 0x00000B60
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002998 File Offset: 0x00000B98
		public event EventHandler CuttingStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000063 RID: 99 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x00002A08 File Offset: 0x00000C08
		public event EventHandler CuttingStopped;

		// Token: 0x06000065 RID: 101 RVA: 0x00002A3D File Offset: 0x00000C3D
		public TreeCutter(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A4C File Offset: 0x00000C4C
		public void InitializeEntity()
		{
			this._yielderRemover = base.GetComponent<YielderRemover>();
			this._removeYieldExecutor = base.GetComponent<RemoveYieldExecutor>();
			this._removeYieldExecutor.WorkStarted += this.OnWorkStarted;
			this._removeYieldExecutor.WorkFinished += this.OnWorkFinished;
			base.GetComponent<Character>().Died += delegate(object _, EventArgs _)
			{
				this.FinishIfCutting();
			};
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public void OnWorkStarted(object sender, EventArgs e)
		{
			if (this._yielderRemover.ReservedYielder)
			{
				TreeRemoveYieldStrategy treeRemoveYieldStrategy = this._yielderRemover.ReservedYielder.RemoveYieldStrategy as TreeRemoveYieldStrategy;
				if (treeRemoveYieldStrategy != null)
				{
					if (this._removeYieldExecutor.IsLoadedLegacyExecutor())
					{
						treeRemoveYieldStrategy.Reacher.NotifyReservableReached(this);
					}
					EventHandler cuttingStarted = this.CuttingStarted;
					if (cuttingStarted != null)
					{
						cuttingStarted(this, EventArgs.Empty);
					}
					this._treeRemoveYieldStrategy = treeRemoveYieldStrategy;
					treeRemoveYieldStrategy.StartCutting(this);
					this._cuttingStarted = true;
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002B35 File Offset: 0x00000D35
		public void OnWorkFinished(object sender, WorkFinishedEventArgs e)
		{
			if (e.WasCompleted)
			{
				this._eventBus.Post(new TreeCutEvent());
			}
			this.FinishIfCutting();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002B58 File Offset: 0x00000D58
		public void FinishIfCutting()
		{
			if (this._cuttingStarted)
			{
				if (this._treeRemoveYieldStrategy)
				{
					this._treeRemoveYieldStrategy.StopCutting();
					this._treeRemoveYieldStrategy = null;
				}
				EventHandler cuttingStopped = this.CuttingStopped;
				if (cuttingStopped != null)
				{
					cuttingStopped(this, EventArgs.Empty);
				}
				this._cuttingStarted = false;
			}
		}

		// Token: 0x04000020 RID: 32
		public readonly EventBus _eventBus;

		// Token: 0x04000021 RID: 33
		public YielderRemover _yielderRemover;

		// Token: 0x04000022 RID: 34
		public RemoveYieldExecutor _removeYieldExecutor;

		// Token: 0x04000023 RID: 35
		public TreeRemoveYieldStrategy _treeRemoveYieldStrategy;

		// Token: 0x04000024 RID: 36
		public bool _cuttingStarted;
	}
}
