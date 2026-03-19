using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WaterObjects;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000010 RID: 16
	public class Floodgate : BaseComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener, IPreviewStateListener, IPersistentEntity, IDuplicable<Floodgate>, IDuplicable, ITerminal
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003182 File Offset: 0x00001382
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000318A File Offset: 0x0000138A
		public bool IsSynchronized { get; private set; } = true;

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003193 File Offset: 0x00001393
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000319B File Offset: 0x0000139B
		public float Height { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000031A4 File Offset: 0x000013A4
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000031AC File Offset: 0x000013AC
		public float AutomationHeight { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x000031B5 File Offset: 0x000013B5
		public Floodgate(FloodgateSynchronizer floodgateSynchronizer)
		{
			this._floodgateSynchronizer = floodgateSynchronizer;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000031CB File Offset: 0x000013CB
		public int MaxHeight
		{
			get
			{
				return this._floodgateSpec.MaxHeight;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000031D8 File Offset: 0x000013D8
		public float PositionedHeight
		{
			get
			{
				return (float)this._blockObject.Coordinates.z + this.Height;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003200 File Offset: 0x00001400
		public float PositionedAutomationHeight
		{
			get
			{
				return (float)this._blockObject.Coordinates.z + this.AutomationHeight;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003228 File Offset: 0x00001428
		public bool IsAutomated
		{
			get
			{
				return this._automatable.IsAutomated;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003235 File Offset: 0x00001435
		public bool IsInputOn
		{
			get
			{
				return this._automatable.State == ConnectionState.On;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003248 File Offset: 0x00001448
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterObstacle = base.GetComponent<WaterObstacle>();
			this._automatable = base.GetComponent<Automatable>();
			this._animationController = base.GetComponent<FloodgateAnimationController>();
			this._floodgateSpec = base.GetComponent<FloodgateSpec>();
			this.Height = (float)this.MaxHeight - Floodgate.DefaultHeightOffset;
			this.AutomationHeight = (float)this.MaxHeight;
			base.DisableComponent();
			this._automatable.InputReconnected += this.OnAutomatableInputReconnected;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032CE File Offset: 0x000014CE
		public void SetHeightAndSynchronize(float newHeight)
		{
			this.SetHeight(newHeight);
			this.SynchronizeAllNeighbors();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032DD File Offset: 0x000014DD
		public void SetAutomationHeightAndSynchronize(float newAutomationHeight)
		{
			this.SetAutomationHeight(newAutomationHeight);
			this.SynchronizeAllNeighbors();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032EC File Offset: 0x000014EC
		public void SetHeight(float newHeight)
		{
			this.Height = this.ClampHeight(newHeight);
			this.UpdateEffectiveHeight(false);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003302 File Offset: 0x00001502
		public void SetAutomationHeight(float newAutomationHeight)
		{
			this.AutomationHeight = this.ClampHeight(newAutomationHeight);
			this.UpdateEffectiveHeight(false);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003318 File Offset: 0x00001518
		public void ToggleSynchronization(bool newValue)
		{
			this.IsSynchronized = newValue;
			this._floodgateSynchronizer.SynchronizeWithAllNeighbors(this);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000332D File Offset: 0x0000152D
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Floodgate.FloodgateKey);
			component.Set(Floodgate.HeightKey, this.Height);
			component.Set(Floodgate.AutomationHeightKey, this.AutomationHeight);
			component.Set(Floodgate.IsSynchronizedKey, this.IsSynchronized);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000336C File Offset: 0x0000156C
		[BackwardCompatible(2025, 12, 15, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Floodgate.FloodgateKey);
			this.Height = component.Get(Floodgate.HeightKey);
			if (component.Has<float>(Floodgate.AutomationHeightKey))
			{
				this.AutomationHeight = component.Get(Floodgate.AutomationHeightKey);
			}
			this.IsSynchronized = component.Get(Floodgate.IsSynchronizedKey);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033C5 File Offset: 0x000015C5
		public void DuplicateFrom(Floodgate source)
		{
			this.IsSynchronized = source.IsSynchronized;
			this.Height = this.ClampHeight(source.Height);
			this.AutomationHeight = this.ClampHeight(source.AutomationHeight);
			this.UpdateEffectiveHeight(false);
			this.SynchronizeAllNeighbors();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003404 File Offset: 0x00001604
		public void OnEnterUnfinishedState()
		{
			this._floodgateSynchronizer.SynchronizeWithUnfinishedNeighbors(this);
			this.UpdateEffectiveHeight(true);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000256E File Offset: 0x0000076E
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003419 File Offset: 0x00001619
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateEffectiveHeight(true);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003428 File Offset: 0x00001628
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._waterObstacle.RemoveFromWaterService();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000343B File Offset: 0x0000163B
		public void OnEnterPreviewState()
		{
			this.UpdateEffectiveHeight(true);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003444 File Offset: 0x00001644
		public void Evaluate()
		{
			this.UpdateEffectiveHeight(false);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000344D File Offset: 0x0000164D
		public void OnAutomatableInputReconnected(object sender, EventArgs e)
		{
			this.SynchronizeAllNeighbors();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003458 File Offset: 0x00001658
		public void UpdateEffectiveHeight(bool forceInstant)
		{
			float num = (this._automatable.State == ConnectionState.On) ? this.AutomationHeight : this.Height;
			if (!this._lastEffectiveHeight.Equals(num))
			{
				this.SetVisualHeight(num, forceInstant);
				if (this._blockObject.IsFinished)
				{
					this.SetObstacleHeight(num);
					this._lastEffectiveHeight = new float?(num);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034C3 File Offset: 0x000016C3
		public void SetVisualHeight(float effectiveHeight, bool forceInstant)
		{
			if (forceInstant || !this._blockObject.IsFinished)
			{
				this._animationController.MoveGateInstantly(effectiveHeight);
				return;
			}
			this._animationController.MoveGateSmoothly(effectiveHeight);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034EE File Offset: 0x000016EE
		public void SetObstacleHeight(float effectiveHeight)
		{
			this._waterObstacle.RemoveFromWaterService();
			if (effectiveHeight > 0f)
			{
				this._waterObstacle.AddToWaterService(effectiveHeight);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000350F File Offset: 0x0000170F
		public void SynchronizeAllNeighbors()
		{
			this._floodgateSynchronizer.SynchronizeAllNeighbors(this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000351D File Offset: 0x0000171D
		public float ClampHeight(float newHeight)
		{
			return Mathf.Clamp(newHeight, 0f, (float)this.MaxHeight);
		}

		// Token: 0x04000031 RID: 49
		public static readonly ComponentKey FloodgateKey = new ComponentKey("Floodgate");

		// Token: 0x04000032 RID: 50
		public static readonly PropertyKey<bool> IsSynchronizedKey = new PropertyKey<bool>("IsSynchronized");

		// Token: 0x04000033 RID: 51
		public static readonly PropertyKey<float> HeightKey = new PropertyKey<float>("Height");

		// Token: 0x04000034 RID: 52
		public static readonly PropertyKey<float> AutomationHeightKey = new PropertyKey<float>("AutomationHeight");

		// Token: 0x04000035 RID: 53
		public static readonly float DefaultHeightOffset = 0.35f;

		// Token: 0x04000039 RID: 57
		public readonly FloodgateSynchronizer _floodgateSynchronizer;

		// Token: 0x0400003A RID: 58
		public BlockObject _blockObject;

		// Token: 0x0400003B RID: 59
		public WaterObstacle _waterObstacle;

		// Token: 0x0400003C RID: 60
		public Automatable _automatable;

		// Token: 0x0400003D RID: 61
		public FloodgateAnimationController _animationController;

		// Token: 0x0400003E RID: 62
		public FloodgateSpec _floodgateSpec;

		// Token: 0x0400003F RID: 63
		public float? _lastEffectiveHeight;
	}
}
