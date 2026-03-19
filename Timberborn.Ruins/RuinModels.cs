using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Ruins
{
	// Token: 0x0200000D RID: 13
	public class RuinModels : BaseComponent, IPersistentEntity
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002739 File Offset: 0x00000939
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002741 File Offset: 0x00000941
		public string VariantId { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000274A File Offset: 0x0000094A
		public bool IsInitialized
		{
			get
			{
				return this._wetModel && this._dryModel;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002766 File Offset: 0x00000966
		public void Initialize(string variantId, GameObject wetModel, GameObject dryModel)
		{
			this.VariantId = variantId;
			this._wetModel = wetModel;
			this._dryModel = dryModel;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000277D File Offset: 0x0000097D
		public void ShowWetModel()
		{
			this._wetModel.SetActive(true);
			this._dryModel.SetActive(false);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002797 File Offset: 0x00000997
		public void ShowDryModel()
		{
			this._wetModel.SetActive(false);
			this._dryModel.SetActive(true);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027B1 File Offset: 0x000009B1
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(RuinModels.RuinModelsKey).Set(RuinModels.VariantIdKey, this.VariantId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027D0 File Offset: 0x000009D0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(RuinModels.RuinModelsKey);
			this.VariantId = component.Get(RuinModels.VariantIdKey);
		}

		// Token: 0x04000017 RID: 23
		public static readonly ComponentKey RuinModelsKey = new ComponentKey("RuinModels");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<string> VariantIdKey = new PropertyKey<string>("VariantId");

		// Token: 0x0400001A RID: 26
		public GameObject _wetModel;

		// Token: 0x0400001B RID: 27
		public GameObject _dryModel;
	}
}
