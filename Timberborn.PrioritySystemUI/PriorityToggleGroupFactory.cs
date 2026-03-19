using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PrioritySystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x0200000E RID: 14
	public class PriorityToggleGroupFactory
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public PriorityToggleGroupFactory(InputService inputService, ILoc loc, PriorityToggleFactory priorityToggleFactory, VisualElementLoader visualElementLoader)
		{
			this._inputService = inputService;
			this._loc = loc;
			this._priorityToggleFactory = priorityToggleFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B0C File Offset: 0x00000D0C
		public PriorityToggleGroup Create(VisualElement parent, string labelLocKey, IPrioritySpriteLoader prioritySpriteLoader, string decreasePriorityKey, string increasePriorityKey)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/PriorityToggleGroup");
			parent.Add(visualElement);
			UQueryExtensions.Q<Label>(visualElement, "Label", null).text = this._loc.T(labelLocKey);
			IEnumerable<PriorityToggle> toggles = this.CreateToggles(UQueryExtensions.Q<VisualElement>(visualElement, "TogglesWrapper", null), prioritySpriteLoader);
			return new PriorityToggleGroup(this._inputService, toggles, decreasePriorityKey, increasePriorityKey);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B72 File Offset: 0x00000D72
		public IEnumerable<PriorityToggle> CreateToggles(VisualElement prioritiesWrapper, IPrioritySpriteLoader prioritySpriteLoader)
		{
			foreach (Priority priority in Priorities.Ascending)
			{
				Sprite sprite = prioritySpriteLoader.LoadSprite(priority);
				yield return this._priorityToggleFactory.Create(priority, prioritiesWrapper, sprite);
			}
			ImmutableArray<Priority>.Enumerator enumerator = default(ImmutableArray<Priority>.Enumerator);
			yield break;
		}

		// Token: 0x0400001F RID: 31
		public readonly InputService _inputService;

		// Token: 0x04000020 RID: 32
		public readonly ILoc _loc;

		// Token: 0x04000021 RID: 33
		public readonly PriorityToggleFactory _priorityToggleFactory;

		// Token: 0x04000022 RID: 34
		public readonly VisualElementLoader _visualElementLoader;
	}
}
