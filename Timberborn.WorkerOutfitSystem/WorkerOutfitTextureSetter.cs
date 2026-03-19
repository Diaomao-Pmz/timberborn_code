using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Characters;
using UnityEngine;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000012 RID: 18
	public class WorkerOutfitTextureSetter : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000301E File Offset: 0x0000121E
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
			base.GetComponent<WorkerOutfitChangeNotifier>().OutfitChanged += this.OnOutfitChanged;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003044 File Offset: 0x00001244
		public void OnOutfitChanged(object sender, WorkerOutfitChangedEventArgs e)
		{
			WorkerOutfitSpec workerOutfitSpec = e.WorkerOutfitSpec;
			Texture texture;
			if (workerOutfitSpec == null)
			{
				texture = null;
			}
			else
			{
				AssetRef<Texture2D> diffuseTexture = workerOutfitSpec.DiffuseTexture;
				texture = ((diffuseTexture != null) ? diffuseTexture.Asset : null);
			}
			this.SetTexture(texture, WorkerOutfitTextureSetter.DiffuseTextureId, ref this._diffuseTextureSet);
			WorkerOutfitSpec workerOutfitSpec2 = e.WorkerOutfitSpec;
			Texture texture2;
			if (workerOutfitSpec2 == null)
			{
				texture2 = null;
			}
			else
			{
				AssetRef<Texture2D> normalTexture = workerOutfitSpec2.NormalTexture;
				texture2 = ((normalTexture != null) ? normalTexture.Asset : null);
			}
			this.SetTexture(texture2, WorkerOutfitTextureSetter.NormalTextureId, ref this._normalTextureSet);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000030AF File Offset: 0x000012AF
		public void SetTexture(Texture texture, int textureId, ref bool setFlag)
		{
			if (texture)
			{
				this._characterMaterialModifier.SetTexture(textureId, texture);
				setFlag = true;
				return;
			}
			if (setFlag)
			{
				this._characterMaterialModifier.SetTexture(textureId, null);
				setFlag = false;
			}
		}

		// Token: 0x04000029 RID: 41
		public static readonly int DiffuseTextureId = Shader.PropertyToID("_WorkerOutfitDiffuse");

		// Token: 0x0400002A RID: 42
		public static readonly int NormalTextureId = Shader.PropertyToID("_WorkerOutfitNormal");

		// Token: 0x0400002B RID: 43
		public CharacterMaterialModifier _characterMaterialModifier;

		// Token: 0x0400002C RID: 44
		public bool _diffuseTextureSet;

		// Token: 0x0400002D RID: 45
		public bool _normalTextureSet;
	}
}
