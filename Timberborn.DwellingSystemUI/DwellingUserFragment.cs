using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.EntityNaming;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x0200000F RID: 15
	public class DwellingUserFragment : IEntityPanelFragment
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002858 File Offset: 0x00000A58
		public DwellingUserFragment(ILoc loc, VisualElementLoader visualElementLoader, EntitySelectionService entitySelectionService, DwellerViewFactory dwellerViewFactory, EntityBadgeService entityBadgeService)
		{
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
			this._entitySelectionService = entitySelectionService;
			this._dwellerViewFactory = dwellerViewFactory;
			this._entityBadgeService = entityBadgeService;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002890 File Offset: 0x00000A90
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DwellingUserFragment");
			this._buttons = UQueryExtensions.Q<VisualElement>(this._root, "Buttons", null);
			this._header = UQueryExtensions.Q<Label>(this._root, "Header", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028F3 File Offset: 0x00000AF3
		public void ShowFragment(BaseComponent entity)
		{
			this._dwelling = entity.GetComponent<Dwelling>();
			if (this._dwelling)
			{
				this.InitializeUserViews();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002914 File Offset: 0x00000B14
		public void ClearFragment()
		{
			this._dwelling = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000292C File Offset: 0x00000B2C
		public void UpdateFragment()
		{
			if (this._dwelling && this._dwelling.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this.UpdateHeader();
				this.UpdateViews(this._views, this._dwelling.AdultDwellers, this._dwelling.ChildDwellers);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002994 File Offset: 0x00000B94
		public void InitializeUserViews()
		{
			this.RemoveAllUserViews();
			this.AddEmptyViewsForAllSlots();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void RemoveAllUserViews()
		{
			foreach (DwellerView dwellerView in this._views)
			{
				this._buttons.Remove(dwellerView.Root);
			}
			this._views.Clear();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void AddEmptyViewsForAllSlots()
		{
			for (int i = 0; i < this._dwelling.AdultSlots; i++)
			{
				this.CreateView().SetAsAdult();
			}
			for (int j = 0; j < this._dwelling.ChildSlots; j++)
			{
				this.CreateView().SetAsChild();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A5C File Offset: 0x00000C5C
		public DwellerView CreateView()
		{
			DwellerView dwellerView = this._dwellerViewFactory.Create();
			this._views.Add(dwellerView);
			this._buttons.Add(dwellerView.Root);
			return dwellerView;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A94 File Offset: 0x00000C94
		public void UpdateHeader()
		{
			string str = string.Format("{0} / {1}", this._dwelling.NumberOfDwellers, this._dwelling.MaxBeavers);
			this._header.text = this._loc.T(DwellingUserFragment.DwellersLocKey) + ": " + str;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void UpdateViews(IReadOnlyList<DwellerView> views, IEnumerable<Dweller> adults, IEnumerable<Dweller> children)
		{
			int num = 0;
			using (IEnumerator<Dweller> enumerator = adults.Concat(children).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Dweller beaver = enumerator.Current;
					DwellerView dwellerView = views[num];
					int wellbeing = beaver.GetComponent<WellbeingTracker>().Wellbeing;
					dwellerView.Fill(beaver, delegate
					{
						this._entitySelectionService.SelectAndFollow(beaver);
					}, beaver.GetComponent<NamedEntity>().EntityName, this._entityBadgeService.GetEntitySubtitle(beaver), wellbeing);
					num++;
				}
				goto IL_A3;
			}
			IL_93:
			views[num].Reset();
			num++;
			IL_A3:
			if (num >= views.Count)
			{
				return;
			}
			goto IL_93;
		}

		// Token: 0x0400002C RID: 44
		public static readonly string DwellersLocKey = "Dwelling.Dwellers";

		// Token: 0x0400002D RID: 45
		public readonly ILoc _loc;

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000030 RID: 48
		public readonly DwellerViewFactory _dwellerViewFactory;

		// Token: 0x04000031 RID: 49
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x04000032 RID: 50
		public VisualElement _root;

		// Token: 0x04000033 RID: 51
		public VisualElement _buttons;

		// Token: 0x04000034 RID: 52
		public Label _header;

		// Token: 0x04000035 RID: 53
		public Dwelling _dwelling;

		// Token: 0x04000036 RID: 54
		public readonly List<DwellerView> _views = new List<DwellerView>();
	}
}
