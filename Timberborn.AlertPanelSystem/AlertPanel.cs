using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.AlertPanelSystem
{
	// Token: 0x02000004 RID: 4
	public class AlertPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public AlertPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, EventBus eventBus, IEnumerable<AlertPanelModule> alertPanelModules)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._alertPanelModules = alertPanelModules.ToImmutableArray<AlertPanelModule>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F3 File Offset: 0x000002F3
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/AlertPanel/AlertPanel");
			this.AddAlertFragments(this._root);
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002123 File Offset: 0x00000323
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				this.UpdateAlertFragments();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddBottomLeft(this._root, 1);
			this._enabled = true;
			this.UpdateAlertFragments();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public void AddAlertFragments(VisualElement root)
		{
			Dictionary<int, IAlertFragment> dictionary = new Dictionary<int, IAlertFragment>();
			foreach (AlertPanelModule alertPanelModule in this._alertPanelModules)
			{
				foreach (KeyValuePair<int, IAlertFragment> keyValuePair in alertPanelModule.AlertFragments)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (int key2 in from key in dictionary.Keys
			orderby key
			select key)
			{
				IAlertFragment alertFragment = dictionary[key2];
				this._alertFragments.Add(alertFragment);
				alertFragment.InitializeAlertFragment(root);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002258 File Offset: 0x00000458
		public void UpdateAlertFragments()
		{
			foreach (IAlertFragment alertFragment in this._alertFragments)
			{
				alertFragment.UpdateAlertFragment();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly UILayout _uiLayout;

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly ImmutableArray<AlertPanelModule> _alertPanelModules;

		// Token: 0x0400000A RID: 10
		public readonly List<IAlertFragment> _alertFragments = new List<IAlertFragment>();

		// Token: 0x0400000B RID: 11
		public VisualElement _root;

		// Token: 0x0400000C RID: 12
		public bool _enabled;
	}
}
