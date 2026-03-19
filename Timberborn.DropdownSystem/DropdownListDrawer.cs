using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DropdownSystem
{
	// Token: 0x0200000E RID: 14
	public class DropdownListDrawer : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002A87 File Offset: 0x00000C87
		public DropdownListDrawer(InputService inputService, ScrollBarInitializationService scrollBarInitializationService, RootVisualElementProvider rootVisualElementProvider, EventBus eventBus)
		{
			this._inputService = inputService;
			this._scrollBarInitializationService = scrollBarInitializationService;
			this._rootVisualElementProvider = rootVisualElementProvider;
			this._eventBus = eventBus;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002AAC File Offset: 0x00000CAC
		public bool DropdownVisible
		{
			get
			{
				return this._root.IsDisplayed();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void Load()
		{
			VisualElement visualElement = this._rootVisualElementProvider.Create("DropdownListDrawer", "Core/DropdownItems", 2);
			this._root = UQueryExtensions.Q<VisualElement>(visualElement, "DropdownItemsWrapper", null);
			this._items = UQueryExtensions.Q<ScrollView>(this._root, "DropdownItems", null);
			this._scrollBarInitializationService.InitializeVisualElement(this._items);
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B28 File Offset: 0x00000D28
		public bool ProcessInput()
		{
			if ((!this._ignoreWorldInput || this._inputService.MouseOverUI) && (this._inputService.Cancel || ((this._inputService.MainMouseButtonDown || this._inputService.ScrollWheelActive) && this._isMouseOver == 0)))
			{
				this.HideDropdown();
				return true;
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B84 File Offset: 0x00000D84
		public void ShowDropdown(VisualElement parent, IEnumerable<VisualElement> items)
		{
			this.HideDropdown();
			this._parent = parent;
			this._inputService.AddInputProcessor(this);
			this._parent.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.MouseEntered();
			}, 0);
			this._parent.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.MouseLeft();
			}, 0);
			this._items.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.MouseEntered();
			}, 0);
			this._items.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.MouseLeft();
			}, 0);
			this._isMouseOver = 1;
			this._root.ToggleDisplayStyle(true);
			foreach (VisualElement visualElement in items)
			{
				this._items.Add(visualElement);
			}
			this.CalculateDimensions();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C64 File Offset: 0x00000E64
		public void HideDropdown()
		{
			if (this.DropdownVisible)
			{
				this._inputService.RemoveInputProcessor(this);
				this._items.Clear();
				this._root.ToggleDisplayStyle(false);
				this._parent.UnregisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
				{
					this.MouseEntered();
				}, 0);
				this._parent.UnregisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
				{
					this.MouseLeft();
				}, 0);
				this._items.UnregisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
				{
					this.MouseEntered();
				}, 0);
				this._items.UnregisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
				{
					this.MouseLeft();
				}, 0);
				this._isMouseOver = 0;
				this._parent = null;
				this._eventBus.Post(new DropdownHiddenEvent());
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D1D File Offset: 0x00000F1D
		public void IgnoreWorldInput(bool ignoreWorldInput)
		{
			this._ignoreWorldInput = ignoreWorldInput;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D26 File Offset: 0x00000F26
		public void MouseEntered()
		{
			this._isMouseOver++;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D36 File Offset: 0x00000F36
		public void MouseLeft()
		{
			this._isMouseOver--;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D48 File Offset: 0x00000F48
		public void CalculateDimensions()
		{
			Vector2 vector = VisualElementExtensions.LocalToWorld(this._parent, this._parent.resolvedStyle.translate);
			this._root.style.left = vector.x;
			this._root.style.width = this._parent.resolvedStyle.width;
			float num = vector.y + this._parent.resolvedStyle.height;
			float height = this._root.parent.resolvedStyle.height;
			bool flag = height - num > 150f;
			float val = flag ? (height - num - 20f) : (vector.y + 20f);
			this._root.style.maxHeight = Math.Min((float)DropdownListDrawer.MaxHeight, val);
			if (flag)
			{
				this._root.style.top = num;
				this._root.style.bottom = new StyleLength(2);
				return;
			}
			this._root.style.top = new StyleLength(2);
			this._root.style.bottom = height - vector.y;
		}

		// Token: 0x04000029 RID: 41
		public static readonly int MaxHeight = 510;

		// Token: 0x0400002A RID: 42
		public readonly InputService _inputService;

		// Token: 0x0400002B RID: 43
		public readonly ScrollBarInitializationService _scrollBarInitializationService;

		// Token: 0x0400002C RID: 44
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x0400002D RID: 45
		public readonly EventBus _eventBus;

		// Token: 0x0400002E RID: 46
		public VisualElement _root;

		// Token: 0x0400002F RID: 47
		public ScrollView _items;

		// Token: 0x04000030 RID: 48
		public VisualElement _parent;

		// Token: 0x04000031 RID: 49
		public int _isMouseOver;

		// Token: 0x04000032 RID: 50
		public bool _ignoreWorldInput;
	}
}
