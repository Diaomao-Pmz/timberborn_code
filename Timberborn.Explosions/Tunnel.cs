using System;
using System.Collections.Generic;
using Bindito.Unity;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.ConstructionSites;
using Timberborn.Coordinates;
using Timberborn.DeconstructionSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;
using Timberborn.TerrainLevelValidation;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000017 RID: 23
	public class Tunnel : BaseComponent, IAwakableComponent, IFinishedStateListener, ITerrainRemovingEntity, IBottomLevelProvider
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003A24 File Offset: 0x00001C24
		public Tunnel(EntityService entityService, IAssetLoader assetLoader, ITerrainService terrainService, IInstantiator instantiator, TemplateNameMapper templateNameMapper, ConstructionFactory constructionFactory, IBlockService blockService, ExplosionSoundPlayer explosionSoundPlayer, BlockValidator blockValidator)
		{
			this._entityService = entityService;
			this._assetLoader = assetLoader;
			this._terrainService = terrainService;
			this._instantiator = instantiator;
			this._templateNameMapper = templateNameMapper;
			this._constructionFactory = constructionFactory;
			this._blockService = blockService;
			this._explosionSoundPlayer = explosionSoundPlayer;
			this._blockValidator = blockValidator;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003A7C File Offset: 0x00001C7C
		public int BottomLevel
		{
			get
			{
				return this._blockObject.Coordinates.z + this._blockObject.Blocks.Size.z;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._deconstructible = base.GetComponent<Deconstructible>();
			TunnelSpec component = base.GetComponent<TunnelSpec>();
			this._explosionPrefab = this._assetLoader.Load<GameObject>(component.ExplosionPrefabPath);
			this._tunnelSupportTemplate = this._templateNameMapper.GetTemplate(component.TunnelSupportTemplateName).GetSpec<BlockObjectSpec>();
			base.GetComponent<DeleteOnFinishConstructionSite>().Deleted += this.OnDeleted;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B2E File Offset: 0x00001D2E
		public void OnEnterFinishedState()
		{
			this.Explode();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002443 File Offset: 0x00000643
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B36 File Offset: 0x00001D36
		public bool RemovesTerrainAt(Vector3Int coordinates)
		{
			return this._blockObject.Coordinates == coordinates;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B49 File Offset: 0x00001D49
		public void Explode()
		{
			this.SpawnParticles();
			this._terrainService.UnsetTerrain(this._blockObject.Coordinates, 1);
			this._deconstructible.DisableDeconstruction();
			this.DestroyGroundOnlyObjectsAbove();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B7C File Offset: 0x00001D7C
		public void OnDeleted(object sender, EventArgs e)
		{
			Placement placement = this._blockObject.Placement;
			if (this._blockValidator.BlocksValid(this._tunnelSupportTemplate, placement))
			{
				this._constructionFactory.CreateAsFinished(this._tunnelSupportTemplate.GetSpec<BuildingSpec>(), placement);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public void SpawnParticles()
		{
			GameObject gameObject = this._instantiator.Instantiate(this._explosionPrefab, null);
			gameObject.transform.position = this._blockObject.GetComponent<BlockObjectCenter>().WorldCenter;
			this._explosionSoundPlayer.Play(gameObject);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void DestroyGroundOnlyObjectsAbove()
		{
			List<BlockObject> list = new List<BlockObject>();
			Vector3Int coordinates = this._blockObject.Coordinates.Above();
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (blockObject.PositionedBlocks.GetBlock(coordinates).MatterBelow == MatterBelow.Ground)
				{
					list.Add(blockObject);
				}
			}
			foreach (BlockObject entity in list)
			{
				this._entityService.Delete(entity);
			}
		}

		// Token: 0x0400005D RID: 93
		public readonly EntityService _entityService;

		// Token: 0x0400005E RID: 94
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400005F RID: 95
		public readonly ITerrainService _terrainService;

		// Token: 0x04000060 RID: 96
		public readonly IInstantiator _instantiator;

		// Token: 0x04000061 RID: 97
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000062 RID: 98
		public readonly ConstructionFactory _constructionFactory;

		// Token: 0x04000063 RID: 99
		public readonly IBlockService _blockService;

		// Token: 0x04000064 RID: 100
		public readonly ExplosionSoundPlayer _explosionSoundPlayer;

		// Token: 0x04000065 RID: 101
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000066 RID: 102
		public BlockObject _blockObject;

		// Token: 0x04000067 RID: 103
		public Deconstructible _deconstructible;

		// Token: 0x04000068 RID: 104
		public GameObject _explosionPrefab;

		// Token: 0x04000069 RID: 105
		public BlockObjectSpec _tunnelSupportTemplate;
	}
}
