using System;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x0200000B RID: 11
	public class BeaverBuildingView
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000029FC File Offset: 0x00000BFC
		public Button Root { get; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002A04 File Offset: 0x00000C04
		public BeaverBuildingView(EntitySelectionService entitySelectionService, Button root, Image buildingImage, Label description)
		{
			this._entitySelectionService = entitySelectionService;
			this.Root = root;
			this.Root.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClick), 0);
			this._buildingImage = buildingImage;
			this._description = description;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A44 File Offset: 0x00000C44
		public void SetBuilding(Building building, string description)
		{
			this.Root.SetEnabled(true);
			this._building = building;
			Sprite image = this._building.GetComponent<LabeledEntity>().Image;
			this._buildingImage.sprite = image;
			this._buildingImage.AddToClassList(BeaverBuildingView.HideDefaultClass);
			this._description.text = description;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A9D File Offset: 0x00000C9D
		public void SetDescriptionOnly(string description)
		{
			this._buildingImage.sprite = null;
			this._buildingImage.RemoveFromClassList(BeaverBuildingView.HideDefaultClass);
			this.Root.SetEnabled(false);
			this._description.text = description;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public void OnClick(ClickEvent evt)
		{
			this._entitySelectionService.SelectAndFocusOn(this._building);
		}

		// Token: 0x04000033 RID: 51
		public static readonly string HideDefaultClass = "beaver-buildings-fragment__icon--empty";

		// Token: 0x04000035 RID: 53
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000036 RID: 54
		public readonly Image _buildingImage;

		// Token: 0x04000037 RID: 55
		public readonly Label _description;

		// Token: 0x04000038 RID: 56
		public Building _building;
	}
}
