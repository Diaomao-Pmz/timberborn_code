using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Automation;
using Timberborn.AutomationBuildings;
using Timberborn.AutomationUI;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using Timberborn.UISound;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200001E RID: 30
	public class PinnedLeversPanel : IPostLoadableSingleton, IInputProcessor
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000046F8 File Offset: 0x000028F8
		public PinnedLeversPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, EntityComponentRegistry entityComponentRegistry, EventBus eventBus, AutomationStateIconBuilder automationStateIconBuilder, UISoundController uiSoundController, InputService inputService)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._entityComponentRegistry = entityComponentRegistry;
			this._eventBus = eventBus;
			this._automationStateIconBuilder = automationStateIconBuilder;
			this._uiSoundController = uiSoundController;
			this._inputService = inputService;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004758 File Offset: 0x00002958
		public void PostLoad()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/AutomationPins/PinnedLeversPanel");
			this._leversContainer = UQueryExtensions.Q<VisualElement>(this._root, "Levers", null);
			this.Recreate();
			this._eventBus.Register(this);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000047B0 File Offset: 0x000029B0
		public bool ProcessInput()
		{
			int num = Math.Min(this._levers.Count, PinnedLeversPanel.PinnedLeverKeys.Length);
			for (int i = 0; i < num; i++)
			{
				string keyId = PinnedLeversPanel.PinnedLeverKeys[i];
				Lever lever = this._levers[i];
				if (lever)
				{
					if (this._inputService.IsKeyDown(keyId))
					{
						lever.Press();
					}
					else if (this._inputService.IsKeyUp(keyId))
					{
						lever.Release();
					}
				}
			}
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004830 File Offset: 0x00002A30
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopLeft(this._root, 40);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004848 File Offset: 0x00002A48
		[OnEvent]
		public void OnPinnedLeverModified(PinnedLeverModified pinnedLeverModified)
		{
			AutomationStateIcon automationStateIcon;
			if (this._leverStateIcons.TryGetValue(pinnedLeverModified.Lever, out automationStateIcon))
			{
				automationStateIcon.Update();
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004870 File Offset: 0x00002A70
		[OnEvent]
		public void OnLeverPinnedChanged(LeverPinnedChangedEvent leverPinnedChangedEvent)
		{
			this.Recreate();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004878 File Offset: 0x00002A78
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			this.ReacreateIfLever(entityInitializedEvent.Entity);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004886 File Offset: 0x00002A86
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			this.ReacreateIfLever(entityDeletedEvent.Entity);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004894 File Offset: 0x00002A94
		[OnEvent]
		public void OnEntityNameChangedEvent(EntityNameChangedEvent entityNameChangedEvent)
		{
			this.ReacreateIfLever(entityNameChangedEvent.Entity);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000048A2 File Offset: 0x00002AA2
		public void ReacreateIfLever(BaseComponent entity)
		{
			if (entity.HasComponent<Lever>())
			{
				this.Recreate();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000048B4 File Offset: 0x00002AB4
		public void Recreate()
		{
			this._leverStateIcons.Clear();
			this._leversContainer.Clear();
			this._levers.Clear();
			foreach (Lever lever2 in from lever in this._entityComponentRegistry.GetAll<Lever>()
			where lever.IsPinned
			orderby lever.GetComponent<NamedEntity>().SortingKey
			select lever)
			{
				this.CreateLeverItem(lever2);
				this._levers.Add(lever2);
			}
			this._root.ToggleDisplayStyle(this._leversContainer.childCount > 0);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004994 File Offset: 0x00002B94
		public void CreateLeverItem(Lever lever)
		{
			VisualElement leverItem = this._visualElementLoader.LoadVisualElement("Game/AutomationPins/PinnedLever");
			Label label = UQueryExtensions.Q<Label>(leverItem, "Name", null);
			label.text = lever.LeverName;
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(leverItem, "State", null);
			visualElement.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				leverItem.RemoveFromClassList(PinnedLeversPanel.EnableHoverClass);
			}, 0);
			visualElement.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				leverItem.AddToClassList(PinnedLeversPanel.EnableHoverClass);
			}, 0);
			Image icon = UQueryExtensions.Q<Image>(leverItem, "StateIcon", null);
			AutomationStateIcon automationStateIcon = this._automationStateIconBuilder.Create(icon, new Func<Automator>(lever.GetComponent<Automator>)).SetClickableIcon().Build();
			automationStateIcon.Update();
			this._leverStateIcons.Add(lever, automationStateIcon);
			label.RegisterCallback<PointerDownEvent>(delegate(PointerDownEvent evt)
			{
				this.OnLeverDown(evt, lever);
			}, 1);
			label.RegisterCallback<PointerUpEvent>(delegate(PointerUpEvent evt)
			{
				PinnedLeversPanel.OnLeverUp(evt, lever);
			}, 1);
			label.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				PinnedLeversPanel.OnMouseLeave(lever);
			}, 0);
			this._leversContainer.Add(leverItem);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004ABE File Offset: 0x00002CBE
		public void OnLeverDown(PointerDownEvent evt, Lever lever)
		{
			if (evt.button == 0)
			{
				this._uiSoundController.PlayClickSound();
				lever.Press();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004AD9 File Offset: 0x00002CD9
		public static void OnLeverUp(PointerUpEvent evt, Lever lever)
		{
			if (evt.button == 0)
			{
				lever.Release();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004AE9 File Offset: 0x00002CE9
		public static void OnMouseLeave(Lever lever)
		{
			if (lever.IsSpringReturn)
			{
				lever.Release();
			}
		}

		// Token: 0x040000B8 RID: 184
		public static readonly string EnableHoverClass = "hover-enabled";

		// Token: 0x040000B9 RID: 185
		public static readonly ImmutableArray<string> PinnedLeverKeys = ImmutableArray.Create<string>(new string[]
		{
			"PinnedLever1",
			"PinnedLever2",
			"PinnedLever3",
			"PinnedLever4",
			"PinnedLever5",
			"PinnedLever6",
			"PinnedLever7",
			"PinnedLever8",
			"PinnedLever9",
			"PinnedLever10"
		});

		// Token: 0x040000BA RID: 186
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000BB RID: 187
		public readonly UILayout _uiLayout;

		// Token: 0x040000BC RID: 188
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x040000BD RID: 189
		public readonly EventBus _eventBus;

		// Token: 0x040000BE RID: 190
		public readonly AutomationStateIconBuilder _automationStateIconBuilder;

		// Token: 0x040000BF RID: 191
		public readonly UISoundController _uiSoundController;

		// Token: 0x040000C0 RID: 192
		public readonly InputService _inputService;

		// Token: 0x040000C1 RID: 193
		public VisualElement _root;

		// Token: 0x040000C2 RID: 194
		public VisualElement _leversContainer;

		// Token: 0x040000C3 RID: 195
		public readonly Dictionary<Lever, AutomationStateIcon> _leverStateIcons = new Dictionary<Lever, AutomationStateIcon>();

		// Token: 0x040000C4 RID: 196
		public readonly List<Lever> _levers = new List<Lever>();
	}
}
