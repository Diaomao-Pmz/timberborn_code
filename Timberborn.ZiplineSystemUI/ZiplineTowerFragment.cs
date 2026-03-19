using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.ZiplineSystem;
using UnityEngine.UIElements;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000014 RID: 20
	public class ZiplineTowerFragment : IEntityPanelFragment
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000331E File Offset: 0x0000151E
		public ZiplineTowerFragment(VisualElementLoader visualElementLoader, ZiplineConnectionButtonFactory ziplineConnectionButtonFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._ziplineConnectionButtonFactory = ziplineConnectionButtonFactory;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003334 File Offset: 0x00001534
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ZiplineTowerFragment");
			this._buttons = UQueryExtensions.Q<VisualElement>(this._root, "Buttons", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003380 File Offset: 0x00001580
		public void ShowFragment(BaseComponent entity)
		{
			this._ziplineTower = entity.GetComponent<ZiplineTower>();
			if (this._ziplineTower)
			{
				this.CreateButtons();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000033AD File Offset: 0x000015AD
		public void UpdateFragment()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033AF File Offset: 0x000015AF
		public void ClearFragment()
		{
			this._ziplineTower = null;
			this._buttons.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033D0 File Offset: 0x000015D0
		public void CreateButtons()
		{
			int i;
			for (i = 0; i < this._ziplineTower.ConnectionTargets.Count; i++)
			{
				ZiplineTower otherZiplineTower = this._ziplineTower.ConnectionTargets[i];
				this._ziplineConnectionButtonFactory.CreateConnection(this._buttons, this._ziplineTower, otherZiplineTower);
			}
			if (this._ziplineTower.HasFreeSlots)
			{
				this.CreateFreeSlotButtons(i + 1);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003440 File Offset: 0x00001640
		public void CreateFreeSlotButtons(int index)
		{
			this._ziplineConnectionButtonFactory.CreateAddConnection(this._buttons, this._ziplineTower);
			while (index < this._ziplineTower.MaxConnections)
			{
				this._ziplineConnectionButtonFactory.CreateEmpty(this._buttons);
				index++;
			}
		}

		// Token: 0x04000060 RID: 96
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000061 RID: 97
		public readonly ZiplineConnectionButtonFactory _ziplineConnectionButtonFactory;

		// Token: 0x04000062 RID: 98
		public VisualElement _root;

		// Token: 0x04000063 RID: 99
		public VisualElement _buttons;

		// Token: 0x04000064 RID: 100
		public ZiplineTower _ziplineTower;
	}
}
