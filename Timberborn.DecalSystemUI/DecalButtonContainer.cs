using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.DecalSystem;
using UnityEngine.UIElements;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x02000005 RID: 5
	public class DecalButtonContainer
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002297 File Offset: 0x00000497
		public DecalButtonContainer(IDecalService decalService, DecalButtonFactory decalButtonFactory)
		{
			this._decalService = decalService;
			this._decalButtonFactory = decalButtonFactory;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B8 File Offset: 0x000004B8
		public void Initialize(VisualElement root)
		{
			Asserts.FieldIsNull<DecalButtonContainer>(this, this._root, "_root");
			this._root = UQueryExtensions.Q<VisualElement>(root, "ButtonContainer", null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022E0 File Offset: 0x000004E0
		public void Show(DecalSupplier decalSupplier)
		{
			this.RemoveButtons();
			foreach (Decal decal in this._decalService.GetDecals(decalSupplier.Category))
			{
				DecalButton decalButton = this._decalButtonFactory.CreateButton(decal);
				decalButton.Show(decalSupplier);
				this._decalButtons.Add(decalButton);
				this._root.Add(decalButton.Root);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002368 File Offset: 0x00000568
		public void Clear()
		{
			foreach (DecalButton decalButton in this._decalButtons)
			{
				decalButton.Clear();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B8 File Offset: 0x000005B8
		public void RemoveButtons()
		{
			foreach (DecalButton decalButton in this._decalButtons)
			{
				this._root.Remove(decalButton.Root);
			}
			this._decalButtons.Clear();
		}

		// Token: 0x0400000E RID: 14
		public readonly IDecalService _decalService;

		// Token: 0x0400000F RID: 15
		public readonly DecalButtonFactory _decalButtonFactory;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;

		// Token: 0x04000011 RID: 17
		public readonly List<DecalButton> _decalButtons = new List<DecalButton>();
	}
}
