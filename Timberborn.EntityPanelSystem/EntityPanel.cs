using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.EntityNamingUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200000E RID: 14
	public class EntityPanel : ILoadableSingleton, IUpdatableSingleton, IEntityPanel
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002EEC File Offset: 0x000010EC
		public EntityPanel(IEnumerable<EntityPanelModule> entityPanelModules, UILayout uiLayout, VisualElementLoader visualElementLoader, EventBus eventBus, EntityNameDialog entityNameDialog, EntityBadgeService entityBadgeService, ILoc loc, EntityDescriptionService entityDescriptionService, ITooltipRegistrar tooltipRegistrar, DiagnosticFragmentController diagnosticFragmentController)
		{
			this._entityPanelModules = entityPanelModules.ToImmutableArray<EntityPanelModule>();
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._entityNameDialog = entityNameDialog;
			this._entityBadgeService = entityBadgeService;
			this._loc = loc;
			this._entityDescriptionService = entityDescriptionService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._diagnosticFragmentController = diagnosticFragmentController;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F5C File Offset: 0x0000115C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/EntityPanel");
			this.InitializeFields();
			this.InitializeModules();
			this._eventBus.Register(this);
			this._uiLayout.AddAbsoluteItem(this._root);
			this._root.ToggleDisplayStyle(false);
			this.InitializeButtons();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FBA File Offset: 0x000011BA
		public void UpdateSingleton()
		{
			if (this._shownEntity)
			{
				this.UpdateEntityBadge();
				this.UpdateFragments();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FD8 File Offset: 0x000011D8
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			EntityComponent entity;
			if (selectableObjectSelectedEvent.SelectableObject.TryGetComponent<EntityComponent>(out entity))
			{
				this.Show(entity);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FFB File Offset: 0x000011FB
		[OnEvent]
		public void OnSelectableObjectUnselectedEvent(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.Hide();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003003 File Offset: 0x00001203
		public void ReloadDescription(EntityComponent entity)
		{
			if (entity == this._shownEntity)
			{
				this._entityDescription.Clear();
				this.UpdateEntityDescription();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000301F File Offset: 0x0000121F
		public bool DescriptionVisible
		{
			get
			{
				return this._entityDescription.IsDisplayed();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000302C File Offset: 0x0000122C
		public string DescriptionHiderTooltipText
		{
			get
			{
				if (!this.DescriptionVisible)
				{
					return this._loc.T(EntityPanel.ShowLocKey);
				}
				return this._loc.T(EntityPanel.HideLocKey);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003058 File Offset: 0x00001258
		public void InitializeFields()
		{
			this._entityAvatar = UQueryExtensions.Q<VisualElement>(this._root, "EntityAvatar", null);
			this._entityDescription = UQueryExtensions.Q<VisualElement>(this._root, "EntityDescription", null);
			this._entityDescriptionHider = UQueryExtensions.Q<Button>(this._root, "EntityDescriptionHider", null);
			this._entityNameButton = UQueryExtensions.Q<Button>(this._root, "EntityName", null);
			this._entityNameText = UQueryExtensions.Q<Label>(this._root, "EntityNameText", null);
			this._entityNameHint = UQueryExtensions.Q<VisualElement>(this._root, "EntityNameHint", null);
			this._entitySubtitle = UQueryExtensions.Q<Label>(this._root, "EntitySubtitle", null);
			this._entityClickableSubtitleButton = UQueryExtensions.Q<Button>(this._root, "EntityClickableSubtitle", null);
			this._entityClickableSubtitleWarningIcon = UQueryExtensions.Q<Image>(this._root, "SubtitleWarning", null);
			this._leftHeaderButtons = UQueryExtensions.Q<VisualElement>(this._root, "LeftButtons", null);
			this._rightHeaderButtons = UQueryExtensions.Q<VisualElement>(this._root, "RightButtons", null);
			this._middleHeaderButtons = UQueryExtensions.Q<VisualElement>(this._root, "MiddleButtons", null);
			this._sideFragments = UQueryExtensions.Q<VisualElement>(this._root, "SideFragments", null);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003190 File Offset: 0x00001390
		public void InitializeButtons()
		{
			this._entityNameButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnEntityNameClicked), 0);
			this._entityDescriptionHider.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleEntityDescription), 0);
			this._entityClickableSubtitleButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.PerformSubtitleButtonClickAction), 0);
			this._tooltipRegistrar.Register(this._entityDescriptionHider, () => this.DescriptionHiderTooltipText);
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(this._root, "ClickableSubtitleWrapper", null), () => this._entityClickableSubtitle.TooltipText);
			this._tooltipRegistrar.Register(this._entityNameButton, new Func<string>(this.GetEntityNameTooltip));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003248 File Offset: 0x00001448
		public void InitializeModules()
		{
			this.AddFragments(EntityPanel.Order(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.LeftHeaderFragments)), this._leftHeaderButtons);
			this.AddFragments(EntityPanel.Order(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.RightHeaderFragments)), this._rightHeaderButtons);
			this.AddFragments(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.MiddleHeaderFragments), this._middleHeaderButtons);
			this.AddFragments(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.SideFragments), this._sideFragments);
			this.AddFragments(EntityPanel.Order(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.ContentFragments)), UQueryExtensions.Q<VisualElement>(this._root, "Fragments", null));
			this._diagnosticFragmentController.Initialize(this._entityPanelModules.SelectMany((EntityPanelModule module) => module.DiagnosticFragments), this._root);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033D8 File Offset: 0x000015D8
		public void AddFragments(IEnumerable<IEntityPanelFragment> fragments, VisualElement parent)
		{
			foreach (IEntityPanelFragment entityPanelFragment in fragments)
			{
				parent.Add(entityPanelFragment.InitializeFragment());
				this._entityPanelFragments.Add(entityPanelFragment);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003434 File Offset: 0x00001634
		public void Show(EntityComponent entity)
		{
			this._shownEntity = entity;
			this.UpdateEntityBadge();
			this.UpdateEntityDescription();
			this.ShowFragments(entity);
			this.UpdateFragments();
			this._root.ToggleDisplayStyle(true);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003462 File Offset: 0x00001662
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
			this.ClearFragments();
			this._shownEntity = null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003480 File Offset: 0x00001680
		public void UpdateEntityBadge()
		{
			NamedEntity component = this._shownEntity.GetComponent<NamedEntity>();
			string entityName = component.EntityName;
			string entitySubtitle = this._entityBadgeService.GetEntitySubtitle(this._shownEntity);
			this._entityClickableSubtitle = this._entityBadgeService.GetEntityClickableSubtitle(this._shownEntity);
			Sprite entityAvatar = this._entityBadgeService.GetEntityAvatar(this._shownEntity);
			this._entityNameText.text = entityName;
			this._entitySubtitle.text = entitySubtitle;
			this._entitySubtitle.ToggleDisplayStyle(!string.IsNullOrEmpty(entitySubtitle));
			this._entityClickableSubtitleButton.text = this._entityClickableSubtitle.Subtitle;
			this._entityClickableSubtitleButton.ToggleDisplayStyle(this._entityClickableSubtitle.HasAction);
			this._entityClickableSubtitleWarningIcon.ToggleDisplayStyle(this._entityClickableSubtitle.HasWarning);
			this._entityAvatar.style.backgroundImage = new StyleBackground(entityAvatar);
			this._entityAvatar.ToggleDisplayStyle(entityAvatar != null);
			this._entityNameButton.SetEnabled(component.IsEditable);
			this._entityNameHint.ToggleDisplayStyle(component.IsEditable);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003594 File Offset: 0x00001794
		public void UpdateEntityDescription()
		{
			this._entityDescriptionService.DescribeAsSingleSection(this._shownEntity, this._entityDescription);
			bool flag = this._entityDescription.childCount == 0;
			this._entityDescription.EnableInClassList(EntityPanel.DescriptionNoneClass, flag);
			this._entityDescriptionHider.EnableInClassList(EntityPanel.HiderNoneClass, flag);
			this._entityDescriptionHider.SetEnabled(!flag);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035F8 File Offset: 0x000017F8
		public void ShowFragments(EntityComponent entity)
		{
			foreach (IEntityPanelFragment entityPanelFragment in this._entityPanelFragments)
			{
				entityPanelFragment.ShowFragment(entity);
			}
			this._diagnosticFragmentController.ShowFragments(entity);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003658 File Offset: 0x00001858
		public void ClearFragments()
		{
			foreach (IEntityPanelFragment entityPanelFragment in this._entityPanelFragments)
			{
				entityPanelFragment.ClearFragment();
			}
			this._entityDescription.Clear();
			this._diagnosticFragmentController.ClearFragments();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036C0 File Offset: 0x000018C0
		public void UpdateFragments()
		{
			foreach (IEntityPanelFragment entityPanelFragment in this._entityPanelFragments)
			{
				entityPanelFragment.UpdateFragment();
			}
			this._diagnosticFragmentController.UpdateFragments();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000371C File Offset: 0x0000191C
		public void ToggleEntityDescription(ClickEvent evt)
		{
			this._entityDescription.EnableInClassList(EntityPanel.DescriptionHiddenClass, this.DescriptionVisible);
			this._entityDescriptionHider.EnableInClassList(EntityPanel.HiderShowClass, this.DescriptionVisible);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000374C File Offset: 0x0000194C
		public string GetEntityNameTooltip()
		{
			EntityComponent shownEntity = this._shownEntity;
			bool? flag;
			if (shownEntity == null)
			{
				flag = null;
			}
			else
			{
				NamedEntity component = shownEntity.GetComponent<NamedEntity>();
				flag = ((component != null) ? new bool?(component.IsEditable) : null);
			}
			bool? flag2 = flag;
			if (!flag2.GetValueOrDefault())
			{
				return null;
			}
			return this._loc.T(EntityPanel.RenameLocKey);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000037A8 File Offset: 0x000019A8
		public void OnEntityNameClicked(ClickEvent evt)
		{
			this._entityNameDialog.Show(this._shownEntity.GetComponent<NamedEntity>());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000037C0 File Offset: 0x000019C0
		public void PerformSubtitleButtonClickAction(ClickEvent evt)
		{
			if (this._entityClickableSubtitle.HasAction)
			{
				this._entityClickableSubtitle.ClickAction();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000037E0 File Offset: 0x000019E0
		public static IEnumerable<IEntityPanelFragment> Order(IEnumerable<OrderedEntityPanelFragment> input)
		{
			return from fragment in input
			orderby fragment.Order
			select fragment.Fragment;
		}

		// Token: 0x04000033 RID: 51
		public static readonly string ShowLocKey = "EntityPanel.Show";

		// Token: 0x04000034 RID: 52
		public static readonly string HideLocKey = "EntityPanel.Hide";

		// Token: 0x04000035 RID: 53
		public static readonly string RenameLocKey = "EntityPanel.Rename";

		// Token: 0x04000036 RID: 54
		public static readonly string DescriptionHiddenClass = "entity-panel__description--hidden";

		// Token: 0x04000037 RID: 55
		public static readonly string HiderShowClass = "entity-panel__description-hider--show-icon";

		// Token: 0x04000038 RID: 56
		public static readonly string DescriptionNoneClass = "entity-panel__description--none";

		// Token: 0x04000039 RID: 57
		public static readonly string HiderNoneClass = "entity-panel__description-hider--none";

		// Token: 0x0400003A RID: 58
		public readonly UILayout _uiLayout;

		// Token: 0x0400003B RID: 59
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003C RID: 60
		public readonly EventBus _eventBus;

		// Token: 0x0400003D RID: 61
		public readonly EntityNameDialog _entityNameDialog;

		// Token: 0x0400003E RID: 62
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x0400003F RID: 63
		public readonly ILoc _loc;

		// Token: 0x04000040 RID: 64
		public readonly EntityDescriptionService _entityDescriptionService;

		// Token: 0x04000041 RID: 65
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000042 RID: 66
		public readonly DiagnosticFragmentController _diagnosticFragmentController;

		// Token: 0x04000043 RID: 67
		public readonly ImmutableArray<EntityPanelModule> _entityPanelModules;

		// Token: 0x04000044 RID: 68
		public readonly List<IEntityPanelFragment> _entityPanelFragments = new List<IEntityPanelFragment>();

		// Token: 0x04000045 RID: 69
		public VisualElement _root;

		// Token: 0x04000046 RID: 70
		public VisualElement _entityAvatar;

		// Token: 0x04000047 RID: 71
		public VisualElement _entityDescription;

		// Token: 0x04000048 RID: 72
		public EntityComponent _shownEntity;

		// Token: 0x04000049 RID: 73
		public Button _entityDescriptionHider;

		// Token: 0x0400004A RID: 74
		public Button _entityNameButton;

		// Token: 0x0400004B RID: 75
		public Label _entityNameText;

		// Token: 0x0400004C RID: 76
		public VisualElement _entityNameHint;

		// Token: 0x0400004D RID: 77
		public Label _entitySubtitle;

		// Token: 0x0400004E RID: 78
		public Button _entityClickableSubtitleButton;

		// Token: 0x0400004F RID: 79
		public Image _entityClickableSubtitleWarningIcon;

		// Token: 0x04000050 RID: 80
		public ClickableSubtitle _entityClickableSubtitle;

		// Token: 0x04000051 RID: 81
		public VisualElement _leftHeaderButtons;

		// Token: 0x04000052 RID: 82
		public VisualElement _rightHeaderButtons;

		// Token: 0x04000053 RID: 83
		public VisualElement _middleHeaderButtons;

		// Token: 0x04000054 RID: 84
		public VisualElement _sideFragments;
	}
}
