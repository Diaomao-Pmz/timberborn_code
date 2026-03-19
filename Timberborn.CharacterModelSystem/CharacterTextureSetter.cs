using System;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000010 RID: 16
	public class CharacterTextureSetter : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002C5D File Offset: 0x00000E5D
		public CharacterTextureSetter(IRandomNumberGenerator randomNumberGenerator, IAssetLoader assetLoader)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C73 File Offset: 0x00000E73
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C84 File Offset: 0x00000E84
		public void PostInitializeEntity()
		{
			CharacterTextureSetterSpec component = base.GetComponent<CharacterTextureSetterSpec>();
			CharacterTexturePack enumerableElement = this._randomNumberGenerator.GetEnumerableElement<CharacterTexturePack>(component.TexturePacks);
			this.SetTexture(enumerableElement.DiffuseTexture, CharacterTextureSetter.DiffuseMapId);
			this.SetTexture(enumerableElement.EmissionTexture, CharacterTextureSetter.EmissionMapId);
			this.SetTexture(enumerableElement.NormalTexture, CharacterTextureSetter.NormalMapId);
			this.SetTexture(enumerableElement.DisplacementTexture, CharacterTextureSetter.DisplacementMapId);
			this._characterMaterialModifier.SetFloat(CharacterTextureSetter.DisplacementScaleId, 3f);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D08 File Offset: 0x00000F08
		public void SetTexture(string textureName, int textureId)
		{
			if (!string.IsNullOrEmpty(textureName))
			{
				Texture2D texture = this._assetLoader.Load<Texture2D>(textureName);
				this._characterMaterialModifier.SetTexture(textureId, texture);
			}
		}

		// Token: 0x04000029 RID: 41
		public static readonly int DiffuseMapId = Shader.PropertyToID("_BaseMap");

		// Token: 0x0400002A RID: 42
		public static readonly int EmissionMapId = Shader.PropertyToID("_EmissionMap");

		// Token: 0x0400002B RID: 43
		public static readonly int NormalMapId = Shader.PropertyToID("_BumpMap");

		// Token: 0x0400002C RID: 44
		public static readonly int DisplacementMapId = Shader.PropertyToID("_BeeStingMap");

		// Token: 0x0400002D RID: 45
		public static readonly int DisplacementScaleId = Shader.PropertyToID("_BeeSting");

		// Token: 0x0400002E RID: 46
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002F RID: 47
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000030 RID: 48
		public CharacterMaterialModifier _characterMaterialModifier;
	}
}
