using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200001B RID: 27
	public class BlockObjectState : BaseComponent, IAwakableComponent, IPersistentEntity, IPostInitializableEntity, IDeletableEntity
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000400B File Offset: 0x0000220B
		public BlockObjectState(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000401A File Offset: 0x0000221A
		public bool IsUnfinished
		{
			get
			{
				return this._state == BlockObjectState.State.Unfinished;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004025 File Offset: 0x00002225
		public bool IsFinished
		{
			get
			{
				return this._state == BlockObjectState.State.Finished;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004030 File Offset: 0x00002230
		public bool IsPreview
		{
			get
			{
				return this._state == BlockObjectState.State.Preview;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000403B File Offset: 0x0000223B
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004049 File Offset: 0x00002249
		public void PostInitializeEntity()
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000405C File Offset: 0x0000225C
		public void Initialize()
		{
			try
			{
				Asserts.IsFalse<BlockObjectState>(this, this._initialized, "_initialized");
				this.NotifyOnStateEntered();
				this._initialized = true;
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception while initializing: " + base.Name, innerException);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000040B4 File Offset: 0x000022B4
		public void MarkAsFinished()
		{
			if (this.IsFinished)
			{
				this.ThrowCannotTransitionToSameState();
			}
			this.EnterState(BlockObjectState.State.Finished);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000040CB File Offset: 0x000022CB
		public void MarkAsPreview()
		{
			if (this.IsPreview)
			{
				this.ThrowCannotTransitionToSameState();
			}
			this.EnterState(BlockObjectState.State.Preview);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000040E2 File Offset: 0x000022E2
		public void Save(IEntitySaver entitySaver)
		{
			if (!this.IsFinished)
			{
				entitySaver.GetComponent(BlockObjectState.BlockObjectStateKey).Set(BlockObjectState.FinishedKey, this.IsFinished);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004108 File Offset: 0x00002308
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BlockObjectState.BlockObjectStateKey, out objectLoader))
			{
				this._state = ((!objectLoader.Has<bool>(BlockObjectState.FinishedKey) || objectLoader.Get(BlockObjectState.FinishedKey)) ? BlockObjectState.State.Finished : BlockObjectState.State.Unfinished);
				return;
			}
			this._state = BlockObjectState.State.Finished;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004155 File Offset: 0x00002355
		public void DeleteEntity()
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			this.NotifyOnStateExited();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000416B File Offset: 0x0000236B
		public void EnterState(BlockObjectState.State state)
		{
			this.NotifyOnStateExited();
			this._state = state;
			if (this._initialized)
			{
				this.NotifyOnStateEntered();
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004188 File Offset: 0x00002388
		public void NotifyOnStateEntered()
		{
			if (this.IsFinished)
			{
				foreach (IFinishedStateListener finishedStateListener in base.GetComponentsAllocating<IFinishedStateListener>())
				{
					finishedStateListener.OnEnterFinishedState();
				}
				this._eventBus.Post(new EnteredFinishedStateEvent(this._blockObject));
				return;
			}
			if (this.IsUnfinished)
			{
				foreach (IUnfinishedStateListener unfinishedStateListener in base.GetComponentsAllocating<IUnfinishedStateListener>())
				{
					unfinishedStateListener.OnEnterUnfinishedState();
				}
				this._eventBus.Post(new EnteredUnfinishedStateEvent(this._blockObject));
				return;
			}
			if (this.IsPreview)
			{
				using (List<IPreviewStateListener>.Enumerator enumerator3 = base.GetComponentsAllocating<IPreviewStateListener>().GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						IPreviewStateListener previewStateListener = enumerator3.Current;
						previewStateListener.OnEnterPreviewState();
					}
					return;
				}
			}
			throw new ArgumentOutOfRangeException("_state", this._state, string.Format("Unexpected {0} value: {1}", this._state, this._state));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000042D4 File Offset: 0x000024D4
		public void NotifyOnStateExited()
		{
			if (this._initialized)
			{
				if (this.IsFinished)
				{
					foreach (IFinishedStateListener finishedStateListener in base.GetComponentsAllocating<IFinishedStateListener>())
					{
						finishedStateListener.OnExitFinishedState();
					}
					this._eventBus.Post(new ExitedFinishedStateEvent(this._blockObject));
					return;
				}
				if (this.IsUnfinished)
				{
					foreach (IUnfinishedStateListener unfinishedStateListener in base.GetComponentsAllocating<IUnfinishedStateListener>())
					{
						unfinishedStateListener.OnExitUnfinishedState();
					}
					this._eventBus.Post(new ExitedUnfinishedStateEvent(this._blockObject));
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000043AC File Offset: 0x000025AC
		public void ThrowCannotTransitionToSameState()
		{
			throw new InvalidOperationException(string.Format("{0} cannot transition to {1} state. It is already in this state.", base.Name, this._state));
		}

		// Token: 0x0400007D RID: 125
		public static readonly ComponentKey BlockObjectStateKey = new ComponentKey("BlockObjectState");

		// Token: 0x0400007E RID: 126
		public static readonly PropertyKey<bool> FinishedKey = new PropertyKey<bool>("Finished");

		// Token: 0x0400007F RID: 127
		public readonly EventBus _eventBus;

		// Token: 0x04000080 RID: 128
		public BlockObject _blockObject;

		// Token: 0x04000081 RID: 129
		public BlockObjectState.State _state;

		// Token: 0x04000082 RID: 130
		public bool _initialized;

		// Token: 0x0200001C RID: 28
		public enum State
		{
			// Token: 0x04000084 RID: 132
			Unfinished,
			// Token: 0x04000085 RID: 133
			Finished,
			// Token: 0x04000086 RID: 134
			Preview
		}
	}
}
