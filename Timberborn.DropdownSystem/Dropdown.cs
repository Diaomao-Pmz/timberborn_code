using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000004 RID: 4
	[UxmlElement]
	public class Dropdown : VisualElement, ILocalizableElement
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler ValueChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler Showed;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021A5 File Offset: 0x000003A5
		public string HoveredItem { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public Dropdown()
		{
			Resources.Load<VisualTreeAsset>("UI/Views/Core/Dropdown").CloneTree(this);
			UQueryExtensions.Q<Label>(this, "Label", null).text = this._labelLocKey;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002200 File Offset: 0x00000400
		public bool IsSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002204 File Offset: 0x00000404
		public void Initialize(DropdownListDrawer dropdownListDrawer)
		{
			this._dropdownListDrawer = dropdownListDrawer;
			this._selectedItem = UQueryExtensions.Q<VisualElement>(this, "SelectedItemContent", null);
			this._selection = UQueryExtensions.Q<Button>(this, "Selection", null);
			this._selection.EnableInClassList(Dropdown.SelectableClass, !this._buttonsOnlySelection);
			this._selection.RegisterCallback<DetachFromPanelEvent>(delegate(DetachFromPanelEvent _)
			{
				this._dropdownListDrawer.HideDropdown();
			}, 0);
			if (this._buttonsOnlySelection)
			{
				UQueryExtensions.Q<Button>(this, "ArrowDown", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleSelectionListDisplayStyle), 0);
				UQueryExtensions.Q<Button>(this, "ArrowLeft", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectPrevious), 0);
				UQueryExtensions.Q<Button>(this, "ArrowRight", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectNext), 0);
				return;
			}
			UQueryExtensions.Q<Button>(this, "ArrowLeft", null).ToggleDisplayStyle(false);
			UQueryExtensions.Q<Button>(this, "ArrowRight", null).ToggleDisplayStyle(false);
			this._selection.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleSelectionListDisplayStyle), 0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000230C File Offset: 0x0000050C
		public void OverrideLabelLocKey(string newLocKey)
		{
			this._labelLocKey = newLocKey;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
		public void Localize(ILoc loc)
		{
			Label label = UQueryExtensions.Q<Label>(this, "Label", null);
			if (!string.IsNullOrEmpty(this._labelLocKey))
			{
				label.text = loc.T(this._labelLocKey);
				return;
			}
			label.ToggleDisplayStyle(this._forceLabel);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000235E File Offset: 0x0000055E
		public void SetItems(IDropdownProvider dropdownProvider, ITooltipRegistrar tooltipRegistrar, Func<string, bool, DropdownElement> elementGetter)
		{
			this._tooltipRegistrar = tooltipRegistrar;
			this._dropdownProvider = dropdownProvider;
			this._elementGetter = elementGetter;
			this.UpdateSelectedValue();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000237C File Offset: 0x0000057C
		public void ClearItems()
		{
			this._dropdownProvider = null;
			this._elementGetter = null;
			this.HoveredItem = null;
			this._selectedItem.Clear();
			this._items.Clear();
			this._elements.Clear();
			this._dropdownListDrawer.HideDropdown();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023CA File Offset: 0x000005CA
		public void UpdateSelectedValue()
		{
			this.UpdateSelectedValue(this._dropdownProvider.GetValue());
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023DD File Offset: 0x000005DD
		public void Hide()
		{
			this._dropdownListDrawer.HideDropdown();
			this.HoveredItem = null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F4 File Offset: 0x000005F4
		public void ToggleSelectionListDisplayStyle(ClickEvent evt)
		{
			if (this._dropdownListDrawer.DropdownVisible)
			{
				this.Hide();
				return;
			}
			this.AddItemsIfNeeded();
			this._dropdownListDrawer.ShowDropdown(this._selection, this._elements);
			EventHandler showed = this.Showed;
			if (showed == null)
			{
				return;
			}
			showed(this, EventArgs.Empty);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002448 File Offset: 0x00000648
		public void SelectPrevious(ClickEvent evt)
		{
			this.AddItemsIfNeeded();
			if (this._items.Count == 0)
			{
				return;
			}
			int num = this._items.IndexOf(this._dropdownProvider.GetValue()) - 1;
			if (num < 0)
			{
				num = this._items.Count - 1;
			}
			this.SetAndUpdate(this._items[num]);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024A8 File Offset: 0x000006A8
		public void SelectNext(ClickEvent evt)
		{
			this.AddItemsIfNeeded();
			if (this._items.Count == 0)
			{
				return;
			}
			int num = this._items.IndexOf(this._dropdownProvider.GetValue()) + 1;
			if (num >= this._items.Count)
			{
				num = 0;
			}
			this.SetAndUpdate(this._items[num]);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002504 File Offset: 0x00000704
		public void AddItemsIfNeeded()
		{
			if (this._elements.Count == 0)
			{
				for (int i = 0; i < this._dropdownProvider.Items.Count; i++)
				{
					string item = this._dropdownProvider.Items[i];
					DropdownElement dropdownElement = this._elementGetter(item, false);
					dropdownElement.Content.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
					{
						this.SetAndUpdate(item);
					}, 0);
					if (!string.IsNullOrWhiteSpace(dropdownElement.Tooltip))
					{
						this._tooltipRegistrar.Register(dropdownElement.Content, dropdownElement.Tooltip);
					}
					this._items.Add(item);
					this._elements.Add(dropdownElement.Content);
					dropdownElement.Content.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
					{
						this.HoveredItem = item;
					}, 0);
					dropdownElement.Content.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
					{
						this.HoveredItem = null;
					}, 0);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002610 File Offset: 0x00000810
		public void SetAndUpdate(string newValue)
		{
			if (this._dropdownProvider.GetValue() != newValue)
			{
				this._dropdownProvider.SetValue(newValue);
				this.UpdateSelectedValue(newValue);
				EventHandler valueChanged = this.ValueChanged;
				if (valueChanged != null)
				{
					valueChanged(this, EventArgs.Empty);
				}
			}
			this._dropdownListDrawer.HideDropdown();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002668 File Offset: 0x00000868
		public void UpdateSelectedValue(string newValue)
		{
			this._selectedItem.Clear();
			VisualElement content = this._elementGetter(newValue, true).Content;
			content.SetEnabled(false);
			content.AddToClassList("dropdown-item--selected");
			this._selectedItem.Add(content);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string SelectableClass = "dropdown__selectable";

		// Token: 0x0400000A RID: 10
		[UxmlAttribute("label-loc-key")]
		public string _labelLocKey;

		// Token: 0x0400000B RID: 11
		[UxmlAttribute("force-label")]
		public bool _forceLabel;

		// Token: 0x0400000C RID: 12
		[UxmlAttribute("buttons-only-selection")]
		public bool _buttonsOnlySelection;

		// Token: 0x0400000D RID: 13
		public DropdownListDrawer _dropdownListDrawer;

		// Token: 0x0400000E RID: 14
		public ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000F RID: 15
		public Button _selection;

		// Token: 0x04000010 RID: 16
		public VisualElement _selectedItem;

		// Token: 0x04000011 RID: 17
		public readonly List<string> _items = new List<string>();

		// Token: 0x04000012 RID: 18
		public readonly List<VisualElement> _elements = new List<VisualElement>();

		// Token: 0x04000013 RID: 19
		public IDropdownProvider _dropdownProvider;

		// Token: 0x04000014 RID: 20
		public Func<string, bool, DropdownElement> _elementGetter;

		// Token: 0x02000005 RID: 5
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600001B RID: 27 RVA: 0x000026D8 File Offset: 0x000008D8
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(Dropdown.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_labelLocKey", "label-loc-key", null, Array.Empty<string>()),
					new UxmlAttributeNames("_forceLabel", "force-label", null, Array.Empty<string>()),
					new UxmlAttributeNames("_buttonsOnlySelection", "buttons-only-selection", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x0600001C RID: 28 RVA: 0x0000274F File Offset: 0x0000094F
			public override object CreateInstance()
			{
				return new Dropdown();
			}

			// Token: 0x0600001D RID: 29 RVA: 0x00002758 File Offset: 0x00000958
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				Dropdown dropdown = (Dropdown)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._labelLocKey_UxmlAttributeFlags))
				{
					dropdown._labelLocKey = this._labelLocKey;
				}
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._forceLabel_UxmlAttributeFlags))
				{
					dropdown._forceLabel = this._forceLabel;
				}
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._buttonsOnlySelection_UxmlAttributeFlags))
				{
					dropdown._buttonsOnlySelection = this._buttonsOnlySelection;
				}
			}

			// Token: 0x04000015 RID: 21
			[UxmlAttribute("label-loc-key")]
			[SerializeField]
			private string _labelLocKey;

			// Token: 0x04000016 RID: 22
			[UxmlAttribute("force-label")]
			[SerializeField]
			private bool _forceLabel;

			// Token: 0x04000017 RID: 23
			[UxmlAttribute("buttons-only-selection")]
			[SerializeField]
			private bool _buttonsOnlySelection;

			// Token: 0x04000018 RID: 24
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _labelLocKey_UxmlAttributeFlags;

			// Token: 0x04000019 RID: 25
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _forceLabel_UxmlAttributeFlags;

			// Token: 0x0400001A RID: 26
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _buttonsOnlySelection_UxmlAttributeFlags;
		}
	}
}
