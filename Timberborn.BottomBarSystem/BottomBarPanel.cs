using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.BottomBarSystem
{
	// Token: 0x02000007 RID: 7
	public class BottomBarPanel : ILoadableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021A2 File Offset: 0x000003A2
		public BottomBarPanel(IEnumerable<BottomBarModule> bottomBarModules, VisualElementLoader visualElementLoader, UILayout uiLayout, EventBus eventBus)
		{
			this._bottomBarModules = bottomBarModules.ToImmutableArray<BottomBarModule>();
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._eventBus = eventBus;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021CC File Offset: 0x000003CC
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/BottomBar/BottomBarPanel");
			this._mainElements = UQueryExtensions.Q<VisualElement>(this._root, "MainSection", null);
			this._subElements = UQueryExtensions.Q<VisualElement>(this._root, "SubSection", null);
			this._uiLayout.AddBottomBar(this._root, 100);
			this._root.ToggleDisplayStyle(false);
			this.InitializeSections();
			this._eventBus.Register(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000224E File Offset: 0x0000044E
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._root.ToggleDisplayStyle(true);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225C File Offset: 0x0000045C
		public void InitializeSections()
		{
			this.InitializeLeftSection();
			this.InitializeMiddleSection();
			this.InitializeRightSection();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002270 File Offset: 0x00000470
		public void InitializeLeftSection()
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._mainElements, "LeftSection", null);
			Dictionary<int, IBottomBarElementsProvider> dictionary = new Dictionary<int, IBottomBarElementsProvider>();
			foreach (KeyValuePair<int, IBottomBarElementsProvider> keyValuePair in this._bottomBarModules.SelectMany((BottomBarModule module) => module.LeftElements))
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (int key2 in from key in dictionary.Keys
			orderby key
			select key)
			{
				foreach (BottomBarElement bottomBarElement in dictionary[key2].GetElements())
				{
					this.AddElement(bottomBarElement, visualElement);
				}
			}
			visualElement.ToggleDisplayStyle(dictionary.Count > 0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023C4 File Offset: 0x000005C4
		public void InitializeMiddleSection()
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._mainElements, "MiddleSection", null);
			foreach (IBottomBarElementsProvider bottomBarElementsProvider in this._bottomBarModules.SelectMany((BottomBarModule module) => module.MiddleElements))
			{
				foreach (BottomBarElement bottomBarElement in bottomBarElementsProvider.GetElements())
				{
					this.AddElement(bottomBarElement, visualElement);
				}
			}
			visualElement.ToggleDisplayStyle(visualElement.childCount > 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002490 File Offset: 0x00000690
		public void InitializeRightSection()
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._mainElements, "RightSection", null);
			foreach (IBottomBarElementsProvider bottomBarElementsProvider in this._bottomBarModules.SelectMany((BottomBarModule module) => module.RightElements))
			{
				foreach (BottomBarElement bottomBarElement in bottomBarElementsProvider.GetElements())
				{
					this.AddElement(bottomBarElement, visualElement);
				}
			}
			visualElement.ToggleDisplayStyle(visualElement.childCount > 0);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000255C File Offset: 0x0000075C
		public void AddElement(BottomBarElement bottomBarElement, VisualElement elementRoot)
		{
			elementRoot.Add(bottomBarElement.MainElement);
			if (bottomBarElement.SubElement != null)
			{
				this._subElements.Add(bottomBarElement.SubElement);
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly ImmutableArray<BottomBarModule> _bottomBarModules;

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly UILayout _uiLayout;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public VisualElement _mainElements;

		// Token: 0x04000013 RID: 19
		public VisualElement _subElements;

		// Token: 0x04000014 RID: 20
		public VisualElement _root;
	}
}
