using System;
using Timberborn.BaseComponentSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.Wonders
{
	// Token: 0x02000022 RID: 34
	public class WonderUnselector : BaseComponent, IAwakableComponent, IUpdatableComponent
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000038DF File Offset: 0x00001ADF
		public WonderUnselector(EntitySelectionService entitySelectionService)
		{
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000038EE File Offset: 0x00001AEE
		public void Awake()
		{
			base.GetComponent<Wonder>().WonderActivated += this.OnWonderActivated;
			this._selectableObject = base.GetComponent<SelectableObject>();
			base.DisableComponent();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003919 File Offset: 0x00001B19
		public void Update()
		{
			if (Time.time >= this._unselectionTime)
			{
				if (this._selectableObject && this._entitySelectionService.SelectedObject == this._selectableObject)
				{
					this._entitySelectionService.Unselect();
				}
				base.DisableComponent();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003959 File Offset: 0x00001B59
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this._unselectionTime = Time.time + WonderUnselector.UnselectionDelay;
			base.EnableComponent();
		}

		// Token: 0x04000051 RID: 81
		public static readonly float UnselectionDelay = 0.5f;

		// Token: 0x04000052 RID: 82
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000053 RID: 83
		public SelectableObject _selectableObject;

		// Token: 0x04000054 RID: 84
		public float _unselectionTime;
	}
}
