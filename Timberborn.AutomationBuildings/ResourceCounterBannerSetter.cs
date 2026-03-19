using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000037 RID: 55
	public class ResourceCounterBannerSetter : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000743A File Offset: 0x0000563A
		public ResourceCounterBannerSetter(GoodIconVisualizer goodIconVisualizer, IGoodService goodService)
		{
			this._goodIconVisualizer = goodIconVisualizer;
			this._goodService = goodService;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007450 File Offset: 0x00005650
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._resourceCounter = base.GetComponent<ResourceCounter>();
			BuildingModel component = base.GetComponent<BuildingModel>();
			this._meshRenderer = component.FinishedModel.GetComponentInChildren<MeshRenderer>();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000748D File Offset: 0x0000568D
		public void Start()
		{
			this._resourceCounter.GoodChanged += this.OnGoodChanged;
			this.UpdateProperties();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000074AC File Offset: 0x000056AC
		public void OnGoodChanged(object sender, string e)
		{
			this.UpdateProperties();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000074B4 File Offset: 0x000056B4
		public void UpdateProperties()
		{
			string goodId = this._resourceCounter.GoodId;
			if (string.IsNullOrWhiteSpace(goodId))
			{
				this._goodIconVisualizer.HideColoredIcon(this._meshRenderer.material);
				return;
			}
			GoodSpec good = this._goodService.GetGood(goodId);
			this._goodIconVisualizer.ShowColoredIcon(this._meshRenderer.material, good, this._blockObject.FlipMode.IsFlipped, ResourceCounterBannerSetter.BannerIconColor);
		}

		// Token: 0x0400012C RID: 300
		public static readonly Color BannerIconColor = new Color(0.33f, 0.33f, 0.33f);

		// Token: 0x0400012D RID: 301
		public readonly GoodIconVisualizer _goodIconVisualizer;

		// Token: 0x0400012E RID: 302
		public readonly IGoodService _goodService;

		// Token: 0x0400012F RID: 303
		public BlockObject _blockObject;

		// Token: 0x04000130 RID: 304
		public ResourceCounter _resourceCounter;

		// Token: 0x04000131 RID: 305
		public MeshRenderer _meshRenderer;
	}
}
