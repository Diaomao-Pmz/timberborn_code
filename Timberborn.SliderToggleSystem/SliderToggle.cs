using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x02000004 RID: 4
	public class SliderToggle : IInputProcessor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public bool IsBound { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public SliderToggle(InputService inputService, VisualElement root, string toggleBindingKey, IEnumerable<SliderToggleButton> sliderToggleButtons)
		{
			this.Root = root;
			this._inputService = inputService;
			this._toggleBindingKey = toggleBindingKey;
			this._sliderToggleButtons = sliderToggleButtons.ToImmutableList<SliderToggleButton>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(this._toggleBindingKey))
			{
				this.SelectNext();
				return true;
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public void Bind()
		{
			if (!string.IsNullOrWhiteSpace(this._toggleBindingKey))
			{
				Asserts.IsFalse<SliderToggle>(this, this.IsBound, "IsBound");
				this.IsBound = true;
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002152 File Offset: 0x00000352
		public void Unbind()
		{
			if (this.IsBound)
			{
				this.IsBound = false;
				this._inputService.RemoveInputProcessor(this);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002170 File Offset: 0x00000370
		public void Update()
		{
			for (int i = 0; i < this._sliderToggleButtons.Count; i++)
			{
				this._sliderToggleButtons[i].Update();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
		public void Clear()
		{
			for (int i = 0; i < this._sliderToggleButtons.Count; i++)
			{
				this._sliderToggleButtons[i].Clear();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public void SelectNext()
		{
			this._sliderToggleButtons[this.GetNextIndex()].Select();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F0 File Offset: 0x000003F0
		public int GetNextIndex()
		{
			for (int i = 0; i < this._sliderToggleButtons.Count; i++)
			{
				if (this._sliderToggleButtons[i].CurrentState == SliderToggleState.Active)
				{
					return (i + 1) % this._sliderToggleButtons.Count;
				}
			}
			return 0;
		}

		// Token: 0x04000008 RID: 8
		public readonly InputService _inputService;

		// Token: 0x04000009 RID: 9
		public readonly string _toggleBindingKey;

		// Token: 0x0400000A RID: 10
		public readonly ImmutableList<SliderToggleButton> _sliderToggleButtons;
	}
}
