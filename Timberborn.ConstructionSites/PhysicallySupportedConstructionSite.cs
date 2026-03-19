using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.TerrainPhysics;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000027 RID: 39
	public class PhysicallySupportedConstructionSite : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IConstructionSiteValidator
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000104 RID: 260 RVA: 0x00004638 File Offset: 0x00002838
		// (remove) Token: 0x06000105 RID: 261 RVA: 0x00004670 File Offset: 0x00002870
		public event EventHandler ValidationStateChanged;

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000046A5 File Offset: 0x000028A5
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000046AD File Offset: 0x000028AD
		public bool IsValid { get; private set; } = true;

		// Token: 0x06000108 RID: 264 RVA: 0x000046B6 File Offset: 0x000028B6
		public PhysicallySupportedConstructionSite(ITerrainPhysicsService terrainPhysicsService)
		{
			this._terrainPhysicsService = terrainPhysicsService;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000046CC File Offset: 0x000028CC
		public bool IsModelValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000046D4 File Offset: 0x000028D4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000046E2 File Offset: 0x000028E2
		public void OnEnterUnfinishedState()
		{
			this.Validate();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003A19 File Offset: 0x00001C19
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000046EC File Offset: 0x000028EC
		public void Validate()
		{
			bool isValid = this.IsValid;
			this.IsValid = this._terrainPhysicsService.CanTerrainBeAdded(this._blockObject.Coordinates);
			if (isValid != this.IsValid)
			{
				EventHandler validationStateChanged = this.ValidationStateChanged;
				if (validationStateChanged == null)
				{
					return;
				}
				validationStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000073 RID: 115
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000074 RID: 116
		public BlockObject _blockObject;
	}
}
