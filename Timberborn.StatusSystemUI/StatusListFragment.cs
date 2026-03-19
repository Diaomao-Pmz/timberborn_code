using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.StatusSystem;
using UnityEngine.UIElements;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x0200000D RID: 13
	public class StatusListFragment : IEntityPanelFragment
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002983 File Offset: 0x00000B83
		public StatusListFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000299D File Offset: 0x00000B9D
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/StatusListFragment");
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void ShowFragment(BaseComponent entity)
		{
			StatusSubject component = entity.GetComponent<StatusSubject>();
			if (component != null)
			{
				this._selectedStatusSubject = component;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E6 File Offset: 0x00000BE6
		public void ClearFragment()
		{
			this._selectedStatusSubject = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029FC File Offset: 0x00000BFC
		public void UpdateFragment()
		{
			if (this._selectedStatusSubject)
			{
				int num = 0;
				foreach (StatusInstance statusInstance in this._selectedStatusSubject.ActiveStatuses)
				{
					if (statusInstance.IsVisible())
					{
						StatusListFragment.Show(this.GetStatusListElement(num++), statusInstance);
					}
				}
				this.HideStatusListElements(num);
				this._root.ToggleDisplayStyle(num > 0);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A90 File Offset: 0x00000C90
		public static void Show(VisualElement statusListElement, StatusInstance statusInstance)
		{
			UQueryExtensions.Q<VisualElement>(statusListElement, "Icon", null).style.backgroundImage = new StyleBackground(statusInstance.IconSmall);
			UQueryExtensions.Q<Label>(statusListElement, "Text", null).text = statusInstance.StatusDescription;
			statusListElement.ToggleDisplayStyle(true);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002ADC File Offset: 0x00000CDC
		public VisualElement GetStatusListElement(int index)
		{
			while (index >= this._statusListElements.Count)
			{
				string elementName = "Game/EntityPanel/StatusListElement";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				this._statusListElements.Add(visualElement);
				this._root.Add(visualElement);
			}
			return this._statusListElements[index];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B30 File Offset: 0x00000D30
		public void HideStatusListElements(int startingIndex)
		{
			for (int i = startingIndex; i < this._statusListElements.Count; i++)
			{
				this._statusListElements[i].ToggleDisplayStyle(false);
			}
		}

		// Token: 0x04000030 RID: 48
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000031 RID: 49
		public VisualElement _root;

		// Token: 0x04000032 RID: 50
		public StatusSubject _selectedStatusSubject;

		// Token: 0x04000033 RID: 51
		public readonly List<VisualElement> _statusListElements = new List<VisualElement>();
	}
}
