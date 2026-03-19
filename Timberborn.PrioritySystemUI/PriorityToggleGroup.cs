using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.PrioritySystem;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x0200000D RID: 13
	public class PriorityToggleGroup : IInputProcessor
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000293E File Offset: 0x00000B3E
		public PriorityToggleGroup(InputService inputService, IEnumerable<PriorityToggle> toggles, string decreasePriorityKey, string increasePriorityKey)
		{
			this._inputService = inputService;
			this._toggles = toggles.ToImmutableArray<PriorityToggle>();
			this._decreasePriorityKey = decreasePriorityKey;
			this._increasePriorityKey = increasePriorityKey;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002968 File Offset: 0x00000B68
		public void UpdateGroup()
		{
			for (int i = 0; i < this._toggles.Length; i++)
			{
				this._toggles[i].UpdateState();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000299C File Offset: 0x00000B9C
		public void Enable(IPrioritizable prioritizable)
		{
			this._prioritizable = prioritizable;
			for (int i = 0; i < this._toggles.Length; i++)
			{
				this._toggles[i].Enable(prioritizable);
			}
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029E4 File Offset: 0x00000BE4
		public void Disable()
		{
			this._prioritizable = null;
			for (int i = 0; i < this._toggles.Length; i++)
			{
				this._toggles[i].Disable();
			}
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A2B File Offset: 0x00000C2B
		public bool ProcessInput()
		{
			if (this.IsDefinedAndPressed(this._decreasePriorityKey))
			{
				this.DecreasePriorityIfPossible();
				return true;
			}
			if (this.IsDefinedAndPressed(this._increasePriorityKey))
			{
				this.IncreasePriorityIfPossible();
				return true;
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A5A File Offset: 0x00000C5A
		public bool IsDefinedAndPressed(string key)
		{
			return !string.IsNullOrEmpty(key) && this._inputService.IsKeyDown(key);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A74 File Offset: 0x00000C74
		public void DecreasePriorityIfPossible()
		{
			Priority priority = this._prioritizable.Priority.Previous<Priority>();
			if (priority != this._prioritizable.Priority)
			{
				this._prioritizable.SetPriority(priority);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void IncreasePriorityIfPossible()
		{
			Priority priority = this._prioritizable.Priority.Next<Priority>();
			if (priority != this._prioritizable.Priority)
			{
				this._prioritizable.SetPriority(priority);
			}
		}

		// Token: 0x0400001A RID: 26
		public readonly InputService _inputService;

		// Token: 0x0400001B RID: 27
		public readonly ImmutableArray<PriorityToggle> _toggles;

		// Token: 0x0400001C RID: 28
		public readonly string _decreasePriorityKey;

		// Token: 0x0400001D RID: 29
		public readonly string _increasePriorityKey;

		// Token: 0x0400001E RID: 30
		public IPrioritizable _prioritizable;
	}
}
