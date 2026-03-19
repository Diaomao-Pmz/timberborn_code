using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x0200000F RID: 15
	public class ObjectSelector
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003280 File Offset: 0x00001480
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x000032B8 File Offset: 0x000014B8
		public event EventHandler<object> SelectedObjectChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600004D RID: 77 RVA: 0x000032F0 File Offset: 0x000014F0
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x00003328 File Offset: 0x00001528
		public event EventHandler ContextChanged;

		// Token: 0x0600004F RID: 79 RVA: 0x0000335D File Offset: 0x0000155D
		public ObjectSelector(VisualElementLoader visualElementLoader, ISingletonRepository singletonRepository, EventBus eventBus, EntitySelectionService entitySelectionService, EntityBadgeService entityBadgeService)
		{
			this._visualElementLoader = visualElementLoader;
			this._singletonRepository = singletonRepository;
			this._eventBus = eventBus;
			this._entitySelectionService = entitySelectionService;
			this._entityBadgeService = entityBadgeService;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003398 File Offset: 0x00001598
		public void Initialize(VisualElement root)
		{
			Asserts.FieldIsNull<ObjectSelector>(this, this._typesListView, "_typesListView");
			this._contextLabel = UQueryExtensions.Q<Label>(root, "Context", null);
			this._searchField = UQueryExtensions.Q<TextField>(root, "SearchField", null);
			this._searchField.RegisterCallback<ChangeEvent<string>>(delegate(ChangeEvent<string> _)
			{
				this.Refresh();
			}, 0);
			this._typesListView = UQueryExtensions.Q<ListView>(root, "ObjectListView", null);
			this._typesListView.makeItem = (() => this._visualElementLoader.LoadVisualElement("Common/DebuggingPanel/ObjectSelectorItem"));
			this._typesListView.bindItem = delegate(VisualElement element, int index)
			{
				((Label)element).text = this._objects[index].GetType().Name;
			};
			this._typesListView.selectionChanged += this.OnSelectionChanged;
			this._typesListView.virtualizationMethod = 1;
			this._typesListView.itemsSource = this._objects;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003466 File Offset: 0x00001666
		public void Enable()
		{
			this._eventBus.Register(this);
			this.Refresh();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000347A File Offset: 0x0000167A
		public void Disable()
		{
			this._eventBus.Unregister(this);
			this._objects.Clear();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003493 File Offset: 0x00001693
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			this.ShowEntity(selectableObjectSelectedEvent.SelectableObject);
			EventHandler contextChanged = this.ContextChanged;
			if (contextChanged == null)
			{
				return;
			}
			contextChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000034B7 File Offset: 0x000016B7
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.ShowSingletons();
			EventHandler contextChanged = this.ContextChanged;
			if (contextChanged == null)
			{
				return;
			}
			contextChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034D5 File Offset: 0x000016D5
		public void Refresh()
		{
			if (this._entitySelectionService.IsAnythingSelected)
			{
				this.ShowEntity(this._entitySelectionService.SelectedObject);
				return;
			}
			this.ShowSingletons();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034FC File Offset: 0x000016FC
		public void OnSelectionChanged(IEnumerable<object> obj)
		{
			object obj2 = obj.FirstOrDefault<object>();
			if (obj2 != null)
			{
				EventHandler<object> selectedObjectChanged = this.SelectedObjectChanged;
				if (selectedObjectChanged == null)
				{
					return;
				}
				selectedObjectChanged(this, obj2);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003528 File Offset: 0x00001728
		public void ShowEntity(SelectableObject selectableObject)
		{
			this.Show(selectableObject.AllComponents);
			string entityName = selectableObject.GetComponent<NamedEntity>().EntityName;
			Guid entityId = selectableObject.GetComponent<EntityComponent>().EntityId;
			this.UpdateContextLabel(string.Format("{0} ({1})", entityName, entityId));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003575 File Offset: 0x00001775
		public void ShowSingletons()
		{
			this.Show(this._singletonRepository.GetSingletons<object>());
			this.UpdateContextLabel("Singletons");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003594 File Offset: 0x00001794
		public void Show(IEnumerable<object> candidateObjects)
		{
			this._objects.Clear();
			string value = this._searchField.value;
			foreach (object obj in candidateObjects)
			{
				if (obj.GetType().Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
				{
					this._objects.Add(obj);
				}
			}
			this._typesListView.Rebuild();
			this._typesListView.ClearSelection();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003624 File Offset: 0x00001824
		public void UpdateContextLabel(string context)
		{
			this._contextLabel.text = string.Format(ObjectSelector.ContextFormat, context);
		}

		// Token: 0x04000044 RID: 68
		public static readonly string ContextFormat = "Context: {0}";

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000048 RID: 72
		public readonly ISingletonRepository _singletonRepository;

		// Token: 0x04000049 RID: 73
		public readonly EventBus _eventBus;

		// Token: 0x0400004A RID: 74
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400004B RID: 75
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x0400004C RID: 76
		public readonly List<object> _objects = new List<object>();

		// Token: 0x0400004D RID: 77
		public Label _contextLabel;

		// Token: 0x0400004E RID: 78
		public TextField _searchField;

		// Token: 0x0400004F RID: 79
		public ListView _typesListView;
	}
}
