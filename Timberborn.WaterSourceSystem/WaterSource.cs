using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Debugging;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000017 RID: 23
	public class WaterSource : TickableComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WaterSource>, IDuplicable, IRegisteredComponent, IInitializableEntity, IPostInitializableEntity, IDeletableEntity, IWaterSource
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A2 RID: 162 RVA: 0x000030F0 File Offset: 0x000012F0
		// (remove) Token: 0x060000A3 RID: 163 RVA: 0x00003128 File Offset: 0x00001328
		public event EventHandler WaterStrengthModifierAdded;

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000315D File Offset: 0x0000135D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003165 File Offset: 0x00001365
		public ImmutableArray<Vector3Int> Coordinates { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000316E File Offset: 0x0000136E
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003176 File Offset: 0x00001376
		public float SpecifiedStrength { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000317F File Offset: 0x0000137F
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003187 File Offset: 0x00001387
		public float CurrentStrength { get; private set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00003190 File Offset: 0x00001390
		public WaterSource(IWaterService waterService, WaterStrengthService waterStrengthService, MapEditorMode mapEditorMode, DevModeManager devModeManager)
		{
			this._waterService = waterService;
			this._waterStrengthService = waterStrengthService;
			this._mapEditorMode = mapEditorMode;
			this._devModeManager = devModeManager;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000031C0 File Offset: 0x000013C0
		public ReadOnlyList<IWaterStrengthModifier> WaterStrengthModifiers
		{
			get
			{
				return this._waterStrengthModifiers.AsReadOnlyList<IWaterStrengthModifier>();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000031CD File Offset: 0x000013CD
		public float Contamination
		{
			get
			{
				return this._waterSourceContamination.Contamination;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000031DA File Offset: 0x000013DA
		public bool IsDuplicable
		{
			get
			{
				return this._mapEditorMode.IsMapEditor || this._devModeManager.Enabled;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000031F8 File Offset: 0x000013F8
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterSourceContamination = base.GetComponent<WaterSourceContamination>();
			this._waterSourceSpec = base.GetComponent<WaterSourceSpec>();
			this.SpecifiedStrength = (this.CurrentStrength = this._waterSourceSpec.DefaultStrength);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003243 File Offset: 0x00001443
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WaterSource.WaterSourceKey);
			component.Set(WaterSource.SpecifiedStrengthKey, this.SpecifiedStrength);
			component.Set(WaterSource.CurrentStrengthKey, this.CurrentStrength);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003274 File Offset: 0x00001474
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaterSource.WaterSourceKey);
			this.SetCurrentStrength(this.LimitStrength(component.Get(WaterSource.CurrentStrengthKey)));
			this.SpecifiedStrength = this.LimitStrength(component.Get(WaterSource.SpecifiedStrengthKey));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000032BB File Offset: 0x000014BB
		public void DuplicateFrom(WaterSource source)
		{
			this.SetSpecifiedStrength(source.SpecifiedStrength);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000032CC File Offset: 0x000014CC
		public void InitializeEntity()
		{
			this.Coordinates = (from coords in this._waterSourceSpec.Coordinates.Select(new Func<Vector2Int, Vector2Int>(this._blockObject.TransformTile))
			select new Vector3Int(coords.x, coords.y, this._blockObject.Coordinates.z + this._blockObject.BaseZ)).ToImmutableArray<Vector3Int>();
			this._waterService.RegisterWaterSource(this);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003322 File Offset: 0x00001522
		public void PostInitializeEntity()
		{
			this.UpdateCurrentStrength();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000332A File Offset: 0x0000152A
		public void DeleteEntity()
		{
			this._waterService.UnregisterWaterSource(this);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003322 File Offset: 0x00001522
		public override void Tick()
		{
			this.UpdateCurrentStrength();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003338 File Offset: 0x00001538
		public void SetSpecifiedStrength(float strength)
		{
			float num = this.LimitStrength(strength);
			if (!Mathf.Approximately(num, this.SpecifiedStrength))
			{
				this.SpecifiedStrength = num;
				this.UpdateCurrentStrength();
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003368 File Offset: 0x00001568
		public void AddWaterStrengthModifier(IWaterStrengthModifier waterStrengthModifier)
		{
			this._waterStrengthModifiers.Add(waterStrengthModifier);
			this.UpdateCurrentStrength();
			EventHandler waterStrengthModifierAdded = this.WaterStrengthModifierAdded;
			if (waterStrengthModifierAdded == null)
			{
				return;
			}
			waterStrengthModifierAdded(this, EventArgs.Empty);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003392 File Offset: 0x00001592
		public void RemoveWaterStrengthModifier(IWaterStrengthModifier waterStrengthModifier)
		{
			this._waterStrengthModifiers.Remove(waterStrengthModifier);
			this.UpdateCurrentStrength();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000033A8 File Offset: 0x000015A8
		public void UpdateCurrentStrength()
		{
			float num = this.SpecifiedStrength;
			foreach (IWaterStrengthModifier waterStrengthModifier in this._waterStrengthModifiers)
			{
				num *= waterStrengthModifier.GetStrengthModifier();
			}
			this.SetCurrentStrength(num);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000340C File Offset: 0x0000160C
		public float LimitStrength(float strength)
		{
			float num = this._waterStrengthService.MaxWaterSourceStrength * (float)this._waterSourceSpec.Coordinates.Length;
			return Mathf.Min(strength, num);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003441 File Offset: 0x00001641
		public void SetCurrentStrength(float strength)
		{
			this.CurrentStrength = this.LimitStrength(strength);
		}

		// Token: 0x04000036 RID: 54
		public static readonly ComponentKey WaterSourceKey = new ComponentKey("WaterSource");

		// Token: 0x04000037 RID: 55
		public static readonly PropertyKey<float> SpecifiedStrengthKey = new PropertyKey<float>("SpecifiedStrength");

		// Token: 0x04000038 RID: 56
		public static readonly PropertyKey<float> CurrentStrengthKey = new PropertyKey<float>("CurrentStrength");

		// Token: 0x0400003D RID: 61
		public readonly IWaterService _waterService;

		// Token: 0x0400003E RID: 62
		public readonly WaterStrengthService _waterStrengthService;

		// Token: 0x0400003F RID: 63
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000040 RID: 64
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000041 RID: 65
		public BlockObject _blockObject;

		// Token: 0x04000042 RID: 66
		public WaterSourceContamination _waterSourceContamination;

		// Token: 0x04000043 RID: 67
		public WaterSourceSpec _waterSourceSpec;

		// Token: 0x04000044 RID: 68
		public readonly List<IWaterStrengthModifier> _waterStrengthModifiers = new List<IWaterStrengthModifier>();
	}
}
