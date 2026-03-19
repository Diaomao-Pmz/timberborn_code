using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.UndoSystem
{
	// Token: 0x02000008 RID: 8
	public class UndoRegistry : IUndoRegistry, IUpdatableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002140 File Offset: 0x00000340
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002148 File Offset: 0x00000348
		public bool IsProcessingStack { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002151 File Offset: 0x00000351
		public UndoRegistry(EventBus eventBus, IEnumerable<IUndoPostprocessor> undoPostprocessors)
		{
			this._eventBus = eventBus;
			this._undoPostprocessors = undoPostprocessors.ToImmutableArray<IUndoPostprocessor>();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000218D File Offset: 0x0000038D
		public bool UndoAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002190 File Offset: 0x00000390
		public bool CanUndo
		{
			get
			{
				return this._undoStack.Count > 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021A0 File Offset: 0x000003A0
		public bool CanRedo
		{
			get
			{
				return this._redoStack.Count > 0;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021B0 File Offset: 0x000003B0
		public void UpdateSingleton()
		{
			if (!this._activated)
			{
				this._activated = true;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021C1 File Offset: 0x000003C1
		public void RegisterSingleUndoable(IUndoable undoable)
		{
			if (this._activated)
			{
				Asserts.IsFalse<UndoRegistry>(this, this.IsProcessingStack, "IsProcessingStack");
				this.AddUndoableToStack(undoable);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021E3 File Offset: 0x000003E3
		public void RegisterStackedUndoable(IUndoable undoable)
		{
			if (this._activated)
			{
				Asserts.IsFalse<UndoRegistry>(this, this.IsProcessingStack, "IsProcessingStack");
				this._stackToRegister.Add(undoable);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000220C File Offset: 0x0000040C
		public void CommitStack()
		{
			if (this._stackToRegister.Count > 0)
			{
				UndoableStack undoable = new UndoableStack(this._stackToRegister);
				this.AddUndoableToStack(undoable);
				this._stackToRegister.Clear();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002248 File Offset: 0x00000448
		public void Undo()
		{
			this.CommitStack();
			this.IsProcessingStack = true;
			if (this._undoStack.Count > 0)
			{
				IUndoable undoable = this._undoStack.Pop();
				undoable.Undo();
				this._redoStack.Push(undoable);
			}
			this.PostprocessUndoables();
			this.IsProcessingStack = false;
			this._eventBus.Post(new UndoStateChangedEvent());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022AC File Offset: 0x000004AC
		public void Redo()
		{
			this.CommitStack();
			this.IsProcessingStack = true;
			if (this._redoStack.Count > 0)
			{
				IUndoable undoable = this._redoStack.Pop();
				undoable.Redo();
				this._undoStack.Push(undoable);
			}
			this.PostprocessUndoables();
			this.IsProcessingStack = false;
			this._eventBus.Post(new UndoStateChangedEvent());
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000230F File Offset: 0x0000050F
		public void AddUndoableToStack(IUndoable undoable)
		{
			this._undoStack.Push(undoable);
			this._redoStack.Clear();
			this._eventBus.Post(new UndoStateChangedEvent());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002338 File Offset: 0x00000538
		public void PostprocessUndoables()
		{
			foreach (IUndoPostprocessor undoPostprocessor in this._undoPostprocessors)
			{
				undoPostprocessor.PostprocessUndoables();
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly ImmutableArray<IUndoPostprocessor> _undoPostprocessors;

		// Token: 0x0400000A RID: 10
		public readonly Stack<IUndoable> _undoStack = new Stack<IUndoable>();

		// Token: 0x0400000B RID: 11
		public readonly Stack<IUndoable> _redoStack = new Stack<IUndoable>();

		// Token: 0x0400000C RID: 12
		public readonly List<IUndoable> _stackToRegister = new List<IUndoable>();

		// Token: 0x0400000D RID: 13
		public bool _activated;
	}
}
