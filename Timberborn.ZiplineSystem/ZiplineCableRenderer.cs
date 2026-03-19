using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlueprintSystem;
using Timberborn.ConstructionMode;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Rendering;
using Timberborn.RootProviders;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000011 RID: 17
	public class ZiplineCableRenderer : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002EA0 File Offset: 0x000010A0
		public ZiplineCableRenderer(RootObjectProvider rootObjectProvider, ConstructionModeService constructionModeService, EventBus eventBus, Highlighter highlighter, MaterialColorer materialColorer, TemplateInstantiator templateInstantiator, ISpecService specService)
		{
			this._rootObjectProvider = rootObjectProvider;
			this._constructionModeService = constructionModeService;
			this._eventBus = eventBus;
			this._highlighter = highlighter;
			this._materialColorer = materialColorer;
			this._templateInstantiator = templateInstantiator;
			this._specService = specService;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EF3 File Offset: 0x000010F3
		public void Load()
		{
			this._root = this._rootObjectProvider.CreateRootObject("ZiplineCableRenderer").transform;
			this._cableTemplate = this._specService.GetBlueprint(ZiplineCableRenderer.CableTemplatePath);
			this._eventBus.Register(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F32 File Offset: 0x00001132
		public void UpdateSingleton()
		{
			if (this._layerVisibilityChanged)
			{
				this._layerVisibilityChanged = false;
				this.UpdateLayerVisibility();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F4C File Offset: 0x0000114C
		[OnEvent]
		public void OnConstructionModeChanged(ConstructionModeChangedEvent constructionModeChangedEvent)
		{
			foreach (KeyValuePair<CableKey, ZiplineCableModel> keyValuePair in this._cableModels)
			{
				CableKey cableKey;
				ZiplineCableModel ziplineCableModel;
				keyValuePair.Deconstruct(ref cableKey, ref ziplineCableModel);
				CableKey cableKey2 = cableKey;
				if (!ziplineCableModel.IsActive)
				{
					this.UpdateVisibility(cableKey2, this._constructionModeService.InConstructionMode);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FC0 File Offset: 0x000011C0
		[OnEvent]
		public void OnMaxVisibleTerrainLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this._layerVisibilityChanged = true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FCC File Offset: 0x000011CC
		public void HighlightConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, Color highlightColor)
		{
			ZiplineCableModel ziplineCableModel;
			CableKey cableKey;
			if (this.TryGetCableModel(ziplineTower, otherZiplineTower, out ziplineCableModel, out cableKey))
			{
				ziplineCableModel.Highlight(highlightColor);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FF0 File Offset: 0x000011F0
		public void UnhighlightConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableModel ziplineCableModel;
			CableKey cableKey;
			if (this.TryGetCableModel(ziplineTower, otherZiplineTower, out ziplineCableModel, out cableKey))
			{
				ziplineCableModel.Unhighlight();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003014 File Offset: 0x00001214
		public void AddActiveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			CableKey cableKey = CableKey.Create(ziplineTower, otherZiplineTower);
			this.CreateCableModel(cableKey);
			this.UpdateVisibility(cableKey, true);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000303C File Offset: 0x0000123C
		public void AddInactiveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			CableKey cableKey = CableKey.Create(ziplineTower, otherZiplineTower);
			ZiplineCableModel ziplineCableModel = this.CreateCableModel(cableKey);
			ziplineCableModel.IsActive = false;
			ziplineCableModel.SetGreyscale(true);
			this.UpdateVisibility(cableKey, this._constructionModeService.InConstructionMode);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003078 File Offset: 0x00001278
		public void ActivateConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableModel ziplineCableModel;
			CableKey cableKey;
			if (this.TryGetCableModel(ziplineTower, otherZiplineTower, out ziplineCableModel, out cableKey))
			{
				ziplineCableModel.IsActive = true;
				ziplineCableModel.SetGreyscale(false);
				this.UpdateVisibility(cableKey, true);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030AC File Offset: 0x000012AC
		public ZiplineCableModel CreateCableModel()
		{
			return new ZiplineCableModel(this._materialColorer, this._highlighter, this._templateInstantiator.Instantiate(this._cableTemplate, this._root, null), this._templateInstantiator.Instantiate(this._cableTemplate, this._root, null));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030FC File Offset: 0x000012FC
		public void RemoveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableModel ziplineCableModel;
			CableKey key;
			if (this.TryGetCableModel(ziplineTower, otherZiplineTower, out ziplineCableModel, out key))
			{
				ziplineCableModel.Destroy();
				this._cableModels.Remove(key);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000312C File Offset: 0x0000132C
		public void UpdateConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableModel ziplineCableModel;
			CableKey cableKey;
			if (this.TryGetCableModel(ziplineTower, otherZiplineTower, out ziplineCableModel, out cableKey))
			{
				ziplineCableModel.UpdateModel(ziplineTower, otherZiplineTower);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003150 File Offset: 0x00001350
		public void UpdateVisibility(CableKey cableKey, bool isVisible)
		{
			ZiplineCableModel ziplineCableModel = this._cableModels[cableKey];
			ziplineCableModel.SetVisibility(isVisible);
			ZiplineCableRenderer.UpdateLayerVisibility(cableKey, ziplineCableModel);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003178 File Offset: 0x00001378
		public void UpdateLayerVisibility()
		{
			foreach (KeyValuePair<CableKey, ZiplineCableModel> keyValuePair in this._cableModels)
			{
				CableKey cableKey;
				ZiplineCableModel ziplineCableModel;
				keyValuePair.Deconstruct(ref cableKey, ref ziplineCableModel);
				CableKey cableKey2 = cableKey;
				ZiplineCableModel cableModel = ziplineCableModel;
				ZiplineCableRenderer.UpdateLayerVisibility(cableKey2, cableModel);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000031DC File Offset: 0x000013DC
		public static void UpdateLayerVisibility(CableKey cableKey, ZiplineCableModel cableModel)
		{
			BlockObjectModelController component = cableKey.ZiplineTower.GetComponent<BlockObjectModelController>();
			BlockObjectModelController component2 = cableKey.OtherZiplineTower.GetComponent<BlockObjectModelController>();
			cableModel.SetShadowOnly(!component.IsAnyModelShown || !component2.IsAnyModelShown);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000321C File Offset: 0x0000141C
		public ZiplineCableModel CreateCableModel(CableKey cableKey)
		{
			ZiplineCableModel ziplineCableModel = this.CreateCableModel();
			ziplineCableModel.UpdateModel(cableKey.ZiplineTower, cableKey.OtherZiplineTower);
			this._cableModels.Add(cableKey, ziplineCableModel);
			return ziplineCableModel;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003250 File Offset: 0x00001450
		public bool TryGetCableModel(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, out ZiplineCableModel model, out CableKey cableKey)
		{
			cableKey = CableKey.Create(ziplineTower, otherZiplineTower);
			return this._cableModels.TryGetValue(cableKey, out model);
		}

		// Token: 0x0400001C RID: 28
		public static readonly string CableTemplatePath = "Models/ZiplineCable/ZiplineCable.blueprint";

		// Token: 0x0400001D RID: 29
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400001E RID: 30
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x0400001F RID: 31
		public readonly EventBus _eventBus;

		// Token: 0x04000020 RID: 32
		public readonly Highlighter _highlighter;

		// Token: 0x04000021 RID: 33
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000022 RID: 34
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x04000023 RID: 35
		public readonly ISpecService _specService;

		// Token: 0x04000024 RID: 36
		public Blueprint _cableTemplate;

		// Token: 0x04000025 RID: 37
		public Transform _root;

		// Token: 0x04000026 RID: 38
		public readonly Dictionary<CableKey, ZiplineCableModel> _cableModels = new Dictionary<CableKey, ZiplineCableModel>();

		// Token: 0x04000027 RID: 39
		public bool _layerVisibilityChanged;
	}
}
