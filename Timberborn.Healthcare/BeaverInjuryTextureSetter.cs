using System;
using System.Collections.Immutable;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Healthcare
{
	// Token: 0x02000008 RID: 8
	public class BeaverInjuryTextureSetter : BaseComponent, IAwakableComponent, IDeadNeededComponent, IPersistentEntity, IInitializableEntity
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002321 File Offset: 0x00000521
		public BeaverInjuryTextureSetter(IRandomNumberGenerator randomNumberGenerator, IAssetLoader assetLoader)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002338 File Offset: 0x00000538
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
			this._beaverInjuryTextureSetterSpec = base.GetComponent<BeaverInjuryTextureSetterSpec>();
			this._injuryDiffusePropertyName = Shader.PropertyToID(BeaverInjuryTextureSetter.InjuryDiffusePropertyName);
			this._injuryNormalMapPropertyName = Shader.PropertyToID(BeaverInjuryTextureSetter.InjuryNormalMapPropertyName);
			this._injuryDisplacementPropertyName = Shader.PropertyToID(BeaverInjuryTextureSetter.InjuryDisplacementPropertyName);
			base.GetComponent<NeedManager>().NeedChangedActiveState += this.OnNeedChangedActiveState;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023A4 File Offset: 0x000005A4
		public void InitializeEntity()
		{
			bool isInjured = base.GetComponent<NeedManager>().NeedIsActive(BeaverInjuryTextureSetter.InjuryNeedId);
			this.UpdateInjuryTextures(isInjured);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C9 File Offset: 0x000005C9
		public void Save(IEntitySaver entitySaver)
		{
			if (this._injurySetId != 0)
			{
				entitySaver.GetComponent(BeaverInjuryTextureSetter.BeaverInjuryTextureSetterKey).Set(BeaverInjuryTextureSetter.InjurySetIdKey, this._injurySetId);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F0 File Offset: 0x000005F0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BeaverInjuryTextureSetter.BeaverInjuryTextureSetterKey, out objectLoader) && objectLoader.Has<int>(BeaverInjuryTextureSetter.InjurySetIdKey))
			{
				this._injurySetId = objectLoader.Get(BeaverInjuryTextureSetter.InjurySetIdKey);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000242A File Offset: 0x0000062A
		public ImmutableArray<BeaverInjuryTextureSet> InjuryTextureSets
		{
			get
			{
				return this._beaverInjuryTextureSetterSpec.InjuryTextureSets;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002437 File Offset: 0x00000637
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			if (e.NeedSpec.Id == BeaverInjuryTextureSetter.InjuryNeedId)
			{
				if (e.IsActive)
				{
					this.RandomizeTextureSetId();
				}
				this.UpdateInjuryTextures(e.IsActive);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002470 File Offset: 0x00000670
		public void RandomizeTextureSetId()
		{
			this._injurySetId = this._randomNumberGenerator.Range(0, this.InjuryTextureSets.Length);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000249D File Offset: 0x0000069D
		public void UpdateInjuryTextures(bool isInjured)
		{
			if (isInjured)
			{
				this.SetInjuryTexturesInMaterial();
				return;
			}
			this.ClearInjuryTexturesInMaterial();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024B0 File Offset: 0x000006B0
		public void SetInjuryTexturesInMaterial()
		{
			this.SetTexturesInMaterial(this.LoadTexture(this.InjuryTextureSets[this._injurySetId].DiffusePath), this.LoadTexture(this.InjuryTextureSets[this._injurySetId].NormalMapPath), this.LoadTexture(this.InjuryTextureSets[this._injurySetId].DisplacementPath));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002520 File Offset: 0x00000720
		public Texture2D LoadTexture(string path)
		{
			return this._assetLoader.Load<Texture2D>(path);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000252E File Offset: 0x0000072E
		public void ClearInjuryTexturesInMaterial()
		{
			this.SetTexturesInMaterial(Texture2D.blackTexture, Texture2D.blackTexture, Texture2D.blackTexture);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002545 File Offset: 0x00000745
		public void SetTexturesInMaterial(Texture diffuse, Texture normalMap, Texture displacement)
		{
			this._characterMaterialModifier.SetTexture(this._injuryDiffusePropertyName, diffuse);
			this._characterMaterialModifier.SetTexture(this._injuryNormalMapPropertyName, normalMap);
			this._characterMaterialModifier.SetTexture(this._injuryDisplacementPropertyName, displacement);
		}

		// Token: 0x0400000B RID: 11
		public static readonly string InjuryNeedId = "Injury";

		// Token: 0x0400000C RID: 12
		public static readonly string InjuryDiffusePropertyName = "_InjuryDiffuse";

		// Token: 0x0400000D RID: 13
		public static readonly string InjuryNormalMapPropertyName = "_InjuryNormalMap";

		// Token: 0x0400000E RID: 14
		public static readonly string InjuryDisplacementPropertyName = "_InjuryDisplacement";

		// Token: 0x0400000F RID: 15
		public static readonly ComponentKey BeaverInjuryTextureSetterKey = new ComponentKey("BeaverInjuryTextureSetter");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<int> InjurySetIdKey = new PropertyKey<int>("InjurySetId");

		// Token: 0x04000011 RID: 17
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000012 RID: 18
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000013 RID: 19
		public CharacterMaterialModifier _characterMaterialModifier;

		// Token: 0x04000014 RID: 20
		public BeaverInjuryTextureSetterSpec _beaverInjuryTextureSetterSpec;

		// Token: 0x04000015 RID: 21
		public int _injuryDiffusePropertyName;

		// Token: 0x04000016 RID: 22
		public int _injuryNormalMapPropertyName;

		// Token: 0x04000017 RID: 23
		public int _injuryDisplacementPropertyName;

		// Token: 0x04000018 RID: 24
		public int _injurySetId;
	}
}
