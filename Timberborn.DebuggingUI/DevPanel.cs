using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000009 RID: 9
	public class DevPanel : ILoadableSingleton
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002A04 File Offset: 0x00000C04
		public DevPanel(VisualElementLoader visualElementLoader, EventBus eventBus, DevModeManager devModeManager, UILayout uiLayout, InputBindingDescriber inputBindingDescriber, IEnumerable<IDevModule> devModules)
		{
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._devModeManager = devModeManager;
			this._uiLayout = uiLayout;
			this._inputBindingDescriber = inputBindingDescriber;
			this._devModules = devModules.ToImmutableArray<IDevModule>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A60 File Offset: 0x00000C60
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/DevPanel/DevPanel");
			this._eventBus.Register(this);
			this._root.ToggleDisplayStyle(this._devModeManager.Enabled);
			UQueryExtensions.Q<Button>(this._root, "DevPanelTitle", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.DevPanelTitleClicked), 0);
			this._filter = UQueryExtensions.Q<TextField>(this._root, "DevPanelFilter", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._filter, delegate(ChangeEvent<string> evt)
			{
				this.FilterButtons(evt.newValue);
			});
			this._filter.textEdition.placeholder = "Type to filter...";
			this._devPanelContent = UQueryExtensions.Q(this._root, "DevPanelContent", null);
			UQueryExtensions.Q<ScrollView>(this._root, "DevPanelScroll", null).verticalScrollerVisibility = 1;
			this._favouriteMethodsContainer = UQueryExtensions.Q(this._root, "FavouriteMethods", null);
			this._otherMethodsContainer = UQueryExtensions.Q(this._root, "OtherMethods", null);
			this.LoadDevMethods();
			this.LoadFavoriteMethods();
			this.UpdateContentVisibility();
			this.CreateDevMethodButtons();
			this._uiLayout.AddBottomLeft(this._root, 2);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B92 File Offset: 0x00000D92
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			this.UpdateRootVisibility();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void LoadDevMethods()
		{
			this._devMethods = (from devMethod in (from devModule in this._devModules
			select devModule.GetDefinition()).SelectMany((DevModuleDefinition devModuleDefinition) => devModuleDefinition.Methods)
			orderby devMethod.Name
			select devMethod).ToList<DevMethod>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C28 File Offset: 0x00000E28
		public void LoadFavoriteMethods()
		{
			if (PlayerPrefs.HasKey(DevPanel.FavouritesPrefValue))
			{
				string[] array = PlayerPrefs.GetString(DevPanel.FavouritesPrefValue).Split(DevPanel.FavouritesSeparator, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string favourite = array[i];
					if (this._devMethods.Any((DevMethod devMethod) => devMethod.Name == favourite))
					{
						this._favouriteMethods.Add(favourite);
					}
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public void CreateDevMethodButtons()
		{
			this._buttons.Clear();
			foreach (DevMethod devMethod in this._devMethods)
			{
				this._buttons.Add(devMethod.Name.ToLower(), this.CreateDevMethodButton(devMethod));
			}
			bool visible = this._favouriteMethods.Any<string>();
			UQueryExtensions.Q<VisualElement>(this._root, "FavouritesLabel", null).ToggleDisplayStyle(visible);
			UQueryExtensions.Q<VisualElement>(this._root, "OtherLabel", null).ToggleDisplayStyle(visible);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D50 File Offset: 0x00000F50
		public VisualElement CreateDevMethodButton(DevMethod devMethod)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/DevPanel/DevPanelButton");
			bool flag = this._favouriteMethods.Contains(devMethod.Name);
			Button button = UQueryExtensions.Q<Button>(visualElement, "Invoke", null);
			string str = string.IsNullOrEmpty(devMethod.KeyBindingId) ? "" : (" [" + this._inputBindingDescriber.GetInputBindingText(devMethod.KeyBindingId) + "]");
			button.text = devMethod.Name + str;
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.InvokeAction(devMethod);
			}, 0);
			Button button2 = UQueryExtensions.Q<Button>(visualElement, "Add", null);
			button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.AddToFavourites(devMethod);
			}, 0);
			button2.ToggleDisplayStyle(!flag);
			Button button3 = UQueryExtensions.Q<Button>(visualElement, "Remove", null);
			button3.ToggleDisplayStyle(flag);
			button3.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.RemoveFromFavourites(devMethod);
			}, 0);
			if (flag)
			{
				this._favouriteMethodsContainer.Add(visualElement);
			}
			else
			{
				this._otherMethodsContainer.Add(visualElement);
			}
			return visualElement;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E78 File Offset: 0x00001078
		public void DevPanelTitleClicked(ClickEvent evt)
		{
			this._expanded = !this._expanded;
			this.UpdateContentVisibility();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E8F File Offset: 0x0000108F
		public void InvokeAction(DevMethod devMethod)
		{
			this.ShowAllButtons();
			devMethod.Invoke();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E9D File Offset: 0x0000109D
		public void AddToFavourites(DevMethod devMethod)
		{
			this._favouriteMethods.Add(devMethod.Name);
			this.SaveFavouritesAndRebuildPanel();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002EB7 File Offset: 0x000010B7
		public void RemoveFromFavourites(DevMethod devMethod)
		{
			this._favouriteMethods.Remove(devMethod.Name);
			this.SaveFavouritesAndRebuildPanel();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002ED1 File Offset: 0x000010D1
		public void SaveFavouritesAndRebuildPanel()
		{
			this.ResetFilter();
			this.SaveFavouriteMethods();
			this.ClearDevMethodButtons();
			this.CreateDevMethodButtons();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002EEC File Offset: 0x000010EC
		public void SaveFavouriteMethods()
		{
			string text = string.Join(DevPanel.FavouritesSeparator, this._favouriteMethods);
			PlayerPrefs.SetString(DevPanel.FavouritesPrefValue, text);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F15 File Offset: 0x00001115
		public void ClearDevMethodButtons()
		{
			this._favouriteMethodsContainer.Clear();
			this._otherMethodsContainer.Clear();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F2D File Offset: 0x0000112D
		public void UpdateRootVisibility()
		{
			this.ShowAllButtons();
			this._root.ToggleDisplayStyle(this._devModeManager.Enabled);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F4B File Offset: 0x0000114B
		public void UpdateContentVisibility()
		{
			this.ShowAllButtons();
			this._devPanelContent.ToggleDisplayStyle(this._expanded);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F64 File Offset: 0x00001164
		public void FilterButtons(string textFilter)
		{
			if (string.IsNullOrEmpty(textFilter))
			{
				this.ShowAllButtons();
				return;
			}
			string value = textFilter.ToLower();
			foreach (KeyValuePair<string, VisualElement> keyValuePair in this._buttons)
			{
				string text;
				VisualElement visualElement;
				keyValuePair.Deconstruct(ref text, ref visualElement);
				string text2 = text;
				visualElement.ToggleDisplayStyle(text2.Contains(value));
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FE4 File Offset: 0x000011E4
		public void ShowAllButtons()
		{
			this.ResetFilter();
			foreach (VisualElement visualElement in this._buttons.Values)
			{
				visualElement.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003040 File Offset: 0x00001240
		public void ResetFilter()
		{
			this._filter.SetValueWithoutNotify("");
		}

		// Token: 0x04000024 RID: 36
		public static readonly string FavouritesPrefValue = "DevPanel.Favourites";

		// Token: 0x04000025 RID: 37
		public static readonly string FavouritesSeparator = "|";

		// Token: 0x04000026 RID: 38
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000027 RID: 39
		public readonly EventBus _eventBus;

		// Token: 0x04000028 RID: 40
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000029 RID: 41
		public readonly UILayout _uiLayout;

		// Token: 0x0400002A RID: 42
		public readonly InputBindingDescriber _inputBindingDescriber;

		// Token: 0x0400002B RID: 43
		public readonly ImmutableArray<IDevModule> _devModules;

		// Token: 0x0400002C RID: 44
		public readonly HashSet<string> _favouriteMethods = new HashSet<string>();

		// Token: 0x0400002D RID: 45
		public readonly Dictionary<string, VisualElement> _buttons = new Dictionary<string, VisualElement>();

		// Token: 0x0400002E RID: 46
		public List<DevMethod> _devMethods;

		// Token: 0x0400002F RID: 47
		public VisualElement _root;

		// Token: 0x04000030 RID: 48
		public VisualElement _devPanelContent;

		// Token: 0x04000031 RID: 49
		public VisualElement _favouriteMethodsContainer;

		// Token: 0x04000032 RID: 50
		public VisualElement _otherMethodsContainer;

		// Token: 0x04000033 RID: 51
		public TextField _filter;

		// Token: 0x04000034 RID: 52
		public bool _expanded;
	}
}
