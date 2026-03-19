using System;
using Timberborn.InputSystem;
using UnityEngine;

namespace Timberborn.LevelVisibilitySystemUI
{
	// Token: 0x02000009 RID: 9
	public class LevelVisibilitySelector : IInputProcessor
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000279A File Offset: 0x0000099A
		public LevelVisibilitySelector(InputService inputService)
		{
			this._inputService = inputService;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027A9 File Offset: 0x000009A9
		public void StartLevelSelection(Action<int> changeCallback, Action endCallback)
		{
			if (!this._midSelection)
			{
				this._changeCallback = changeCallback;
				this._endCallback = endCallback;
				this._accumulatedChange = 0f;
				this._midSelection = true;
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027E0 File Offset: 0x000009E0
		public bool ProcessInput()
		{
			if (this._inputService.MainMouseButtonUp)
			{
				this.EndLevelSelection();
			}
			else
			{
				int num = this.ProcessMouseMovement();
				if (num != 0)
				{
					this._changeCallback(num);
				}
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002819 File Offset: 0x00000A19
		public void EndLevelSelection()
		{
			this._inputService.RemoveInputProcessor(this);
			this._midSelection = false;
			this._endCallback();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000283C File Offset: 0x00000A3C
		public int ProcessMouseMovement()
		{
			Vector2 vector = this._inputService.MouseXYAxes * LevelVisibilitySelector.SelectionSpeed;
			this._accumulatedChange += vector.y;
			int num = Mathf.RoundToInt(this._accumulatedChange);
			if (num != 0)
			{
				this._accumulatedChange -= (float)num;
				return num;
			}
			return 0;
		}

		// Token: 0x04000030 RID: 48
		public static readonly float SelectionSpeed = 0.1f;

		// Token: 0x04000031 RID: 49
		public readonly InputService _inputService;

		// Token: 0x04000032 RID: 50
		public Action<int> _changeCallback;

		// Token: 0x04000033 RID: 51
		public Action _endCallback;

		// Token: 0x04000034 RID: 52
		public float _accumulatedChange;

		// Token: 0x04000035 RID: 53
		public bool _midSelection;
	}
}
