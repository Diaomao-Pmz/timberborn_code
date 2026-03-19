using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Modding;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ModdingUI
{
	// Token: 0x02000009 RID: 9
	public class ModListView
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001D RID: 29 RVA: 0x0000241C File Offset: 0x0000061C
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x00002454 File Offset: 0x00000654
		public event EventHandler ListChanged;

		// Token: 0x0600001F RID: 31 RVA: 0x00002489 File Offset: 0x00000689
		public ModListView(IModItemFactory modItemFactory, ModSorter modSorter)
		{
			this._modItemFactory = modItemFactory;
			this._modSorter = modSorter;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024B5 File Offset: 0x000006B5
		public void Initialize(VisualElement root, IEnumerable<Mod> mods)
		{
			this._scrollView = UQueryExtensions.Q<ScrollView>(root, null, null);
			UQueryExtensions.Q<Button>(root, "ResetOrderButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ResetPriorities();
			}, 0);
			this.BuildList(mods);
			this.SortList();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024F0 File Offset: 0x000006F0
		public void ResetScroll()
		{
			this._scrollView.scrollOffset = Vector2.zero;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002504 File Offset: 0x00000704
		public void Update()
		{
			foreach (ModItem modItem in this._modItems.Values)
			{
				modItem.Update();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000255C File Offset: 0x0000075C
		public void ResetPriorities()
		{
			foreach (Mod mod in this._modItems.Keys)
			{
				ModPlayerPrefsHelper.ResetModPriority(mod);
			}
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this.SortList();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D0 File Offset: 0x000007D0
		public void SortList()
		{
			foreach (Mod key in this._modSorter.Sort(this._modItems.Keys))
			{
				this._modItems[key].Root.BringToFront();
			}
			this._modWarningUpdater.Update(this._modItems);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002650 File Offset: 0x00000850
		public void BuildList(IEnumerable<Mod> mods)
		{
			foreach (Mod mod in mods)
			{
				ModItem modItem = this._modItemFactory.CreateModItem(mod, new Action<Mod, bool>(this.OnPriorityIncreased), new Action<Mod, bool>(this.OnPriorityDecreased));
				modItem.ModToggled += this.OnModToggled;
				this._modItems.Add(mod, modItem);
				this._scrollView.Add(modItem.Root);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026E8 File Offset: 0x000008E8
		public void OnPriorityIncreased(Mod mod, bool moveToTop)
		{
			ModItem modItem = this._modItems[mod];
			int num = modItem.Root.parent.IndexOf(modItem.Root);
			if (num > 0)
			{
				if (moveToTop)
				{
					this.MoveToTop(mod, num, modItem.Root.parent);
					return;
				}
				this.IncreasePriority(mod, num, modItem.Root.parent);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002748 File Offset: 0x00000948
		public void MoveToTop(Mod mod, int index, VisualElement parent)
		{
			ModPlayerPrefsHelper.SetModPriority(mod, ModPlayerPrefsHelper.GetModPriority(mod) + index);
			while (index > 0)
			{
				ModPlayerPrefsHelper.DecreaseModPriority(this.GetModFromElement(parent.ElementAt(index - 1)));
				index--;
			}
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this.SortList();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000279F File Offset: 0x0000099F
		public void IncreasePriority(Mod mod, int index, VisualElement parent)
		{
			Mod modFromElement = this.GetModFromElement(parent.ElementAt(index - 1));
			ModPlayerPrefsHelper.IncreaseModPriority(mod);
			ModPlayerPrefsHelper.DecreaseModPriority(modFromElement);
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this.SortList();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027D8 File Offset: 0x000009D8
		public void OnPriorityDecreased(Mod mod, bool moveToBottom)
		{
			ModItem modItem = this._modItems[mod];
			int num = modItem.Root.parent.IndexOf(modItem.Root);
			if (num < modItem.Root.parent.childCount - 1)
			{
				if (moveToBottom)
				{
					this.MoveToBottom(mod, num, modItem.Root.parent);
					return;
				}
				this.DecreasePriority(mod, num, modItem.Root.parent);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002848 File Offset: 0x00000A48
		public void MoveToBottom(Mod mod, int index, VisualElement parent)
		{
			ModPlayerPrefsHelper.SetModPriority(mod, ModPlayerPrefsHelper.GetModPriority(mod) - (parent.childCount - index - 1));
			while (index < parent.childCount - 1)
			{
				ModPlayerPrefsHelper.IncreaseModPriority(this.GetModFromElement(parent.ElementAt(index + 1)));
				index++;
			}
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this.SortList();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028AF File Offset: 0x00000AAF
		public void DecreasePriority(Mod mod, int index, VisualElement parent)
		{
			Mod modFromElement = this.GetModFromElement(parent.ElementAt(index + 1));
			ModPlayerPrefsHelper.DecreaseModPriority(mod);
			ModPlayerPrefsHelper.IncreaseModPriority(modFromElement);
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this.SortList();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void OnModToggled(object sender, EventArgs e)
		{
			EventHandler listChanged = this.ListChanged;
			if (listChanged != null)
			{
				listChanged(this, EventArgs.Empty);
			}
			this._modWarningUpdater.Update(this._modItems);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002914 File Offset: 0x00000B14
		public Mod GetModFromElement(VisualElement element)
		{
			return this._modItems.Single((KeyValuePair<Mod, ModItem> x) => x.Value.Root == element).Key;
		}

		// Token: 0x04000015 RID: 21
		public readonly IModItemFactory _modItemFactory;

		// Token: 0x04000016 RID: 22
		public readonly ModSorter _modSorter;

		// Token: 0x04000017 RID: 23
		public readonly ModWarningUpdater _modWarningUpdater = new ModWarningUpdater();

		// Token: 0x04000018 RID: 24
		public ScrollView _scrollView;

		// Token: 0x04000019 RID: 25
		public readonly Dictionary<Mod, ModItem> _modItems = new Dictionary<Mod, ModItem>();
	}
}
