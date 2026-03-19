using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200001A RID: 26
	public class PinnedIndicatorsPanel : IPostLoadableSingleton
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004187 File Offset: 0x00002387
		public PinnedIndicatorsPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, EntityComponentRegistry entityComponentRegistry, EntitySelectionService entitySelectionService, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._entityComponentRegistry = entityComponentRegistry;
			this._entitySelectionService = entitySelectionService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000041C0 File Offset: 0x000023C0
		public void PostLoad()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/AutomationPins/PinnedIndicatorsPanel");
			this._indicatorsContainer = UQueryExtensions.Q<VisualElement>(this._root, "Indicators", null);
			this.Recreate();
			this._eventBus.Register(this);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000420C File Offset: 0x0000240C
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopLeft(this._root, 40);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004221 File Offset: 0x00002421
		[OnEvent]
		public void OnIndicatorPinnedModeChanged(IndicatorPinnedModeChangedEvent indicatorPinnedModeChangedEvent)
		{
			this.Recreate();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004229 File Offset: 0x00002429
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			this.RecreateIfIndicator(entityInitializedEvent.Entity);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004238 File Offset: 0x00002438
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			Indicator component = entityDeletedEvent.Entity.GetComponent<Indicator>();
			if (component != null)
			{
				this.UnsubscribeFromModification(component);
				this.Recreate();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004261 File Offset: 0x00002461
		[OnEvent]
		public void OnEntityNameChangedEvent(EntityNameChangedEvent entityNameChangedEvent)
		{
			this.RecreateIfIndicator(entityNameChangedEvent.Entity);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000426F File Offset: 0x0000246F
		public void RecreateIfIndicator(BaseComponent entity)
		{
			if (entity.HasComponent<Indicator>())
			{
				this.Recreate();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004280 File Offset: 0x00002480
		public void Recreate()
		{
			this._indicatorsContainer.Clear();
			this._indicatorItems.Clear();
			foreach (Indicator indicator2 in from indicator in this._entityComponentRegistry.GetAll<Indicator>()
			where indicator.PinnedMode > IndicatorPinnedMode.Never
			orderby indicator.GetComponent<NamedEntity>().SortingKey
			select indicator)
			{
				this.CreateIndicatorItem(indicator2);
			}
			this._root.ToggleDisplayStyle(this._indicatorsContainer.childCount > 0);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000434C File Offset: 0x0000254C
		public void CreateIndicatorItem(Indicator indicator)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/AutomationPins/PinnedIndicator");
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = indicator.IndicatorName;
			Image stateIcon = UQueryExtensions.Q<Image>(visualElement, "StateIcon", null);
			visualElement.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnIndicatorClicked(indicator);
			}, 0);
			PinnedIndicatorsPanel.IndicatorItem indicatorItem = new PinnedIndicatorsPanel.IndicatorItem(visualElement, stateIcon);
			PinnedIndicatorsPanel.UpdateIndicatorItem(indicator, indicatorItem);
			this._indicatorItems.Add(indicator, indicatorItem);
			this._indicatorsContainer.Add(visualElement);
			this.SubscribeToModification(indicator);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000043F8 File Offset: 0x000025F8
		public static void UpdateIndicatorItem(Indicator indicator, PinnedIndicatorsPanel.IndicatorItem indicatorItem)
		{
			indicatorItem.Root.ToggleDisplayStyle(indicator.State || indicator.PinnedMode == IndicatorPinnedMode.Always);
			indicatorItem.StateIcon.style.unityBackgroundImageTintColor = indicator.GetComponent<CustomizableIlluminator>().IconColor;
			indicatorItem.StateIcon.EnableInClassList(PinnedIndicatorsPanel.StateIconOnClass, indicator.State);
			indicatorItem.StateIcon.EnableInClassList(PinnedIndicatorsPanel.StateIconUnfinishedClass, !indicator.Enabled);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004473 File Offset: 0x00002673
		public void OnIndicatorClicked(Indicator indicator)
		{
			this._entitySelectionService.SelectAndFocusOn(indicator);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004481 File Offset: 0x00002681
		public void SubscribeToModification(Indicator indicator)
		{
			this.UnsubscribeFromModification(indicator);
			indicator.PinnedIndicatorModified += this.OnPinnedIndicatorModified;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000449C File Offset: 0x0000269C
		public void UnsubscribeFromModification(Indicator indicator)
		{
			indicator.PinnedIndicatorModified -= this.OnPinnedIndicatorModified;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000044B0 File Offset: 0x000026B0
		public void OnPinnedIndicatorModified(object sender, EventArgs e)
		{
			Indicator indicator = (Indicator)sender;
			PinnedIndicatorsPanel.IndicatorItem indicatorItem;
			if (this._indicatorItems.TryGetValue(indicator, out indicatorItem))
			{
				PinnedIndicatorsPanel.UpdateIndicatorItem(indicator, indicatorItem);
			}
		}

		// Token: 0x040000A7 RID: 167
		public static readonly string StateIconOnClass = "automation-state-icon--on";

		// Token: 0x040000A8 RID: 168
		public static readonly string StateIconUnfinishedClass = "automation-state-icon--unfinished";

		// Token: 0x040000A9 RID: 169
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000AA RID: 170
		public readonly UILayout _uiLayout;

		// Token: 0x040000AB RID: 171
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x040000AC RID: 172
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x040000AD RID: 173
		public readonly EventBus _eventBus;

		// Token: 0x040000AE RID: 174
		public VisualElement _root;

		// Token: 0x040000AF RID: 175
		public VisualElement _indicatorsContainer;

		// Token: 0x040000B0 RID: 176
		public readonly Dictionary<Indicator, PinnedIndicatorsPanel.IndicatorItem> _indicatorItems = new Dictionary<Indicator, PinnedIndicatorsPanel.IndicatorItem>();

		// Token: 0x0200001B RID: 27
		public class IndicatorItem : IEquatable<PinnedIndicatorsPanel.IndicatorItem>
		{
			// Token: 0x0600009F RID: 159 RVA: 0x000044F1 File Offset: 0x000026F1
			public IndicatorItem(VisualElement Root, Image StateIcon)
			{
				this.Root = Root;
				this.StateIcon = StateIcon;
				base..ctor();
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004507 File Offset: 0x00002707
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(PinnedIndicatorsPanel.IndicatorItem);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004513 File Offset: 0x00002713
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000451B File Offset: 0x0000271B
			public VisualElement Root { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004524 File Offset: 0x00002724
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000452C File Offset: 0x0000272C
			public Image StateIcon { get; set; }

			// Token: 0x060000A5 RID: 165 RVA: 0x00004538 File Offset: 0x00002738
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("IndicatorItem");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00004584 File Offset: 0x00002784
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("Root = ");
				builder.Append(this.Root);
				builder.Append(", StateIcon = ");
				builder.Append(this.StateIcon);
				return true;
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x000045BE File Offset: 0x000027BE
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(PinnedIndicatorsPanel.IndicatorItem left, PinnedIndicatorsPanel.IndicatorItem right)
			{
				return !(left == right);
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x000045CA File Offset: 0x000027CA
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(PinnedIndicatorsPanel.IndicatorItem left, PinnedIndicatorsPanel.IndicatorItem right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000045DE File Offset: 0x000027DE
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.<Root>k__BackingField)) * -1521134295 + EqualityComparer<Image>.Default.GetHashCode(this.<StateIcon>k__BackingField);
			}

			// Token: 0x060000AA RID: 170 RVA: 0x0000461E File Offset: 0x0000281E
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as PinnedIndicatorsPanel.IndicatorItem);
			}

			// Token: 0x060000AB RID: 171 RVA: 0x0000462C File Offset: 0x0000282C
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(PinnedIndicatorsPanel.IndicatorItem other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<VisualElement>.Default.Equals(this.<Root>k__BackingField, other.<Root>k__BackingField) && EqualityComparer<Image>.Default.Equals(this.<StateIcon>k__BackingField, other.<StateIcon>k__BackingField));
			}

			// Token: 0x060000AD RID: 173 RVA: 0x0000468D File Offset: 0x0000288D
			[CompilerGenerated]
			protected IndicatorItem([Nullable(1)] PinnedIndicatorsPanel.IndicatorItem original)
			{
				this.Root = original.<Root>k__BackingField;
				this.StateIcon = original.<StateIcon>k__BackingField;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000046AD File Offset: 0x000028AD
			[CompilerGenerated]
			public void Deconstruct(out VisualElement Root, out Image StateIcon)
			{
				Root = this.Root;
				StateIcon = this.StateIcon;
			}
		}
	}
}
