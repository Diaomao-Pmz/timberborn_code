using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x0200000F RID: 15
	public class DecalSupplierBuildingIcon : BaseComponent, IAwakableComponent, IStartableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002ABF File Offset: 0x00000CBF
		public DecalSupplierBuildingIcon(IDecalService decalService)
		{
			this._decalService = decalService;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public void Awake()
		{
			this._decalSupplier = base.GetComponent<DecalSupplier>();
			string iconRendererName = base.GetComponent<DecalSupplierBuildingIconSpec>().IconRendererName;
			this._iconRenderer = base.GameObject.FindChild(iconRendererName).GetComponent<MeshRenderer>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B0C File Offset: 0x00000D0C
		public void Start()
		{
			if (base.GetComponent<BlockObject>().IsPreview)
			{
				this.UpdateIcon();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B21 File Offset: 0x00000D21
		public void InitializeEntity()
		{
			this._decalSupplier.ActiveDecalChanged += delegate(object _, EventArgs _)
			{
				this.UpdateIcon();
			};
			this.UpdateIcon();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B40 File Offset: 0x00000D40
		public void DeleteEntity()
		{
			Object.Destroy(this._iconRenderer.material);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B54 File Offset: 0x00000D54
		public void UpdateIcon()
		{
			Decal validatedDecal = this._decalService.GetValidatedDecal(this._decalSupplier.ActiveDecal);
			Texture2D decalTexture = this._decalService.GetDecalTexture(validatedDecal);
			this._iconRenderer.material.SetTexture(DecalSupplierBuildingIcon.IconPropertyId, decalTexture);
		}

		// Token: 0x04000023 RID: 35
		public static readonly int IconPropertyId = Shader.PropertyToID("_DetailAlbedoMap3");

		// Token: 0x04000024 RID: 36
		public readonly IDecalService _decalService;

		// Token: 0x04000025 RID: 37
		public DecalSupplier _decalSupplier;

		// Token: 0x04000026 RID: 38
		public MeshRenderer _iconRenderer;
	}
}
