using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Hauling;
using Timberborn.InputSystemUI;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.HaulingUI
{
	// Token: 0x02000007 RID: 7
	public class HaulCandidateFragment : IEntityPanelFragment
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
		public HaulCandidateFragment(VisualElementLoader visualElementLoader, BindableToggleFactory bindableToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._bindableToggleFactory = bindableToggleFactory;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002328 File Offset: 0x00000528
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/HaulCandidateFragment");
			this._priorityToggle = this._bindableToggleFactory.Create(UQueryExtensions.Q<Toggle>(this._root, "Toggle", null), HaulCandidateFragment.ToggleHaulingPriorityKey, delegate(bool value)
			{
				this._haulPrioritizable.Prioritized = value;
			}, () => this._haulPrioritizable.Prioritized);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000239C File Offset: 0x0000059C
		public void ShowFragment(BaseComponent entity)
		{
			HaulCandidate component = entity.GetComponent<HaulCandidate>();
			if (component && !component.GetComponent<HaulingBlocker>())
			{
				this._haulCandidate = component;
				this._haulPrioritizable = entity.GetComponent<HaulPrioritizable>();
				Manufactory component2 = entity.GetComponent<Manufactory>();
				if (component2)
				{
					if (component2.ProductionRecipes.FastAll((RecipeSpec recipe) => !recipe.ConsumesIngredients && !recipe.ProducesProducts))
					{
						this._root.ToggleDisplayStyle(false);
						return;
					}
				}
				this._priorityToggle.Bind();
				this._priorityToggle.Enable();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000244D File Offset: 0x0000064D
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._priorityToggle.Disable();
			this._priorityToggle.Unbind();
			this._haulPrioritizable = null;
			this._haulCandidate = null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247F File Offset: 0x0000067F
		public void UpdateFragment()
		{
			if (this._haulCandidate)
			{
				this._priorityToggle.Update();
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly string ToggleHaulingPriorityKey = "ToggleHaulingPriority";

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000013 RID: 19
		public BindableToggle _priorityToggle;

		// Token: 0x04000014 RID: 20
		public HaulCandidate _haulCandidate;

		// Token: 0x04000015 RID: 21
		public HaulPrioritizable _haulPrioritizable;

		// Token: 0x04000016 RID: 22
		public VisualElement _root;
	}
}
