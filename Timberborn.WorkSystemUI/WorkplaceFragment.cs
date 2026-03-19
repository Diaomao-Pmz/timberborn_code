using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.Characters;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.PrioritySystemUI;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000016 RID: 22
	public class WorkplaceFragment : IEntityPanelFragment
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003150 File Offset: 0x00001350
		public WorkplaceFragment(VisualElementLoader visualElementLoader, WorkerViewFactory workerViewFactory, EntitySelectionService entitySelectionService, ILoc loc, ITooltipRegistrar tooltipRegistrar, WorkplacePriorityToggleGroupFactory workplacePriorityToggleGroupFactory, BotPopulation botPopulation, WorkerTypeToggleFactory workerTypeToggleFactory, BindableButtonFactory bindableButtonFactory, EntityBadgeService entityBadgeService)
		{
			this._visualElementLoader = visualElementLoader;
			this._workerViewFactory = workerViewFactory;
			this._entitySelectionService = entitySelectionService;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._workplacePriorityToggleGroupFactory = workplacePriorityToggleGroupFactory;
			this._botPopulation = botPopulation;
			this._workerTypeToggleFactory = workerTypeToggleFactory;
			this._bindableButtonFactory = bindableButtonFactory;
			this._entityBadgeService = entityBadgeService;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031BC File Offset: 0x000013BC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WorkplaceFragment");
			this._workplaceUsers = UQueryExtensions.Q<VisualElement>(this._root, "WorkplaceUsers", null);
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._tooltipRegistrar.Register(this._text, () => this._workplaceDescriber.GetWorkersTooltip());
			this._increase = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Increase", null), WorkplaceFragment.IncreaseWorkersKey, delegate
			{
				this._workplace.IncreaseDesiredWorkers();
			}, true);
			this._decrease = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Decrease", null), WorkplaceFragment.DecreaseWorkersKey, delegate
			{
				this._workplace.DecreaseDesiredWorkers();
			}, true);
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Increase", null), WorkplaceFragment.IncreaseWorkersKey);
			this._tooltipRegistrar.RegisterWithKeyBinding(UQueryExtensions.Q<Button>(this._root, "Decrease", null), WorkplaceFragment.DecreaseWorkersKey);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._root, "HeaderWrapper", null);
			this._priorityToggleGroup = this._workplacePriorityToggleGroupFactory.Create(visualElement, WorkplaceFragment.PriorityLabelLocKey);
			this._tooltipRegistrar.RegisterLocalizable(UQueryExtensions.Q<VisualElement>(visualElement, "TogglesWrapper", null), WorkplaceFragment.PriorityTooltipLocKey);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "WorkerTypeToggleWrapper", null);
			this._workerTypeToggle = this._workerTypeToggleFactory.CreateBindable(parent, WorkplaceFragment.ToggleWorkerTypeKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003354 File Offset: 0x00001554
		public void ShowFragment(BaseComponent entity)
		{
			this._workplace = entity.GetComponent<Workplace>();
			if (this._workplace)
			{
				this._workplaceDescriber = entity.GetComponent<WorkplaceDescriber>();
				WorkplaceWorkerType component = entity.GetComponent<WorkplaceWorkerType>();
				this._workerTypeToggle.Show(component);
				WorkplacePriority component2 = entity.GetComponent<WorkplacePriority>();
				this._priorityToggleGroup.Enable(component2);
				this.AddEmptyViews(component);
				this._increase.Bind();
				this._decrease.Bind();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033CC File Offset: 0x000015CC
		public void ClearFragment()
		{
			this._workplace = null;
			this._workplaceDescriber = null;
			this._workerTypeToggle.Clear();
			this._priorityToggleGroup.Disable();
			this._root.ToggleDisplayStyle(false);
			this._increase.Unbind();
			this._decrease.Unbind();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003420 File Offset: 0x00001620
		public void UpdateFragment()
		{
			if (this._workplace)
			{
				this._priorityToggleGroup.UpdateGroup();
				this.UpdateButtons();
				if (this._workplace.Enabled)
				{
					this.UpdateViews();
				}
				this._workplaceUsers.ToggleDisplayStyle(this._workplace.Enabled);
				if (this._botPopulation.BotCreated)
				{
					this._workerTypeToggle.Update();
					this._workerTypeToggle.Root.ToggleDisplayStyle(true);
				}
				else
				{
					this._workerTypeToggle.Root.ToggleDisplayStyle(false);
				}
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034CC File Offset: 0x000016CC
		public void AddEmptyViews(WorkplaceWorkerType workplaceWorkerType)
		{
			this.RemoveViews();
			for (int i = 0; i < this._workplace.MaxWorkers; i++)
			{
				this.AddEmptyView(workplaceWorkerType);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000034FC File Offset: 0x000016FC
		public void RemoveViews()
		{
			this._workplaceUsers.Clear();
			this._views.Clear();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003514 File Offset: 0x00001714
		public void AddEmptyView(WorkplaceWorkerType workplaceWorkerType)
		{
			WorkerView workerView = this._workerViewFactory.Create(workplaceWorkerType);
			workerView.ShowEmpty();
			this._views.Add(workerView);
			this._workplaceUsers.Add(workerView.Root);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003554 File Offset: 0x00001754
		public void UpdateViews()
		{
			IEnumerable<Character> enumerable = from worker in this._workplace.AssignedWorkers
			select worker.GetComponent<Character>();
			int i = 0;
			using (IEnumerator<Character> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Character character = enumerator.Current;
					WorkerView workerView = this._views[i];
					string firstName = character.FirstName;
					workerView.Fill(character, delegate
					{
						this._entitySelectionService.SelectAndFollow(character);
					}, firstName);
					i++;
				}
			}
			DistrictBuilding component = this._workplace.GetComponent<DistrictBuilding>();
			if (this._workplace.Understaffed && component.InstantDistrict)
			{
				while (i < this._workplace.DesiredWorkers)
				{
					this._views[i].ShowVacant();
					i++;
				}
			}
			while (i < this._views.Count)
			{
				this._views[i].ShowEmpty();
				i++;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003684 File Offset: 0x00001884
		public void UpdateButtons()
		{
			int numberOfAssignedWorkers = this._workplace.NumberOfAssignedWorkers;
			int desiredWorkers = this._workplace.DesiredWorkers;
			int maxWorkers = this._workplace.MaxWorkers;
			this._text.text = this._loc.T<int, int>(WorkplaceFragment.CurrentWorkersLocKey, numberOfAssignedWorkers, desiredWorkers);
			if (desiredWorkers > 1)
			{
				this._decrease.Enable();
			}
			else
			{
				this._decrease.Disable();
			}
			if (desiredWorkers < maxWorkers)
			{
				this._increase.Enable();
				return;
			}
			this._increase.Disable();
		}

		// Token: 0x04000060 RID: 96
		public static readonly string PriorityLabelLocKey = "Work.Workplace.DisplayName";

		// Token: 0x04000061 RID: 97
		public static readonly string PriorityTooltipLocKey = "Work.PriorityTitle";

		// Token: 0x04000062 RID: 98
		public static readonly string CurrentWorkersLocKey = "Work.CurrentWorkers";

		// Token: 0x04000063 RID: 99
		public static readonly string IncreaseWorkersKey = "IncreaseWorkers";

		// Token: 0x04000064 RID: 100
		public static readonly string DecreaseWorkersKey = "DecreaseWorkers";

		// Token: 0x04000065 RID: 101
		public static readonly string ToggleWorkerTypeKey = "ToggleWorkerType";

		// Token: 0x04000066 RID: 102
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000067 RID: 103
		public readonly WorkerViewFactory _workerViewFactory;

		// Token: 0x04000068 RID: 104
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000069 RID: 105
		public readonly ILoc _loc;

		// Token: 0x0400006A RID: 106
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400006B RID: 107
		public readonly WorkplacePriorityToggleGroupFactory _workplacePriorityToggleGroupFactory;

		// Token: 0x0400006C RID: 108
		public readonly BotPopulation _botPopulation;

		// Token: 0x0400006D RID: 109
		public readonly WorkerTypeToggleFactory _workerTypeToggleFactory;

		// Token: 0x0400006E RID: 110
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400006F RID: 111
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x04000070 RID: 112
		public VisualElement _root;

		// Token: 0x04000071 RID: 113
		public VisualElement _workplaceUsers;

		// Token: 0x04000072 RID: 114
		public Label _text;

		// Token: 0x04000073 RID: 115
		public BindableButton _increase;

		// Token: 0x04000074 RID: 116
		public BindableButton _decrease;

		// Token: 0x04000075 RID: 117
		public WorkerTypeToggle _workerTypeToggle;

		// Token: 0x04000076 RID: 118
		public Workplace _workplace;

		// Token: 0x04000077 RID: 119
		public WorkplaceDescriber _workplaceDescriber;

		// Token: 0x04000078 RID: 120
		public readonly List<WorkerView> _views = new List<WorkerView>();

		// Token: 0x04000079 RID: 121
		public PriorityToggleGroup _priorityToggleGroup;
	}
}
