using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x0200000A RID: 10
	public class UndergroundWaterSourceDrill : BaseComponent, IAwakableComponent, IFinishedStateListener, IWaterStrengthModifier
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000229C File Offset: 0x0000049C
		public UndergroundWaterSourceDrill(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AB File Offset: 0x000004AB
		public void Awake()
		{
			this._underlyingWaterSource = base.GetComponent<UnderlyingWaterSource>();
			this._hazardousWeatherObserver = base.GetComponent<HazardousWeatherObserver>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E0 File Offset: 0x000004E0
		public void OnEnterFinishedState()
		{
			this._underlyingWaterSource.AddWaterStrengthModifier(this);
			WaterSource waterSource = this._underlyingWaterSource.WaterSource;
			if (waterSource != null)
			{
				waterSource.GetComponent<UndergroundWaterSource>().SetOccupied();
			}
			if (this._hazardousWeatherObserver.IsBadtideWeather || this._hazardousWeatherObserver.IsDroughtWeather)
			{
				this.Block();
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002340 File Offset: 0x00000540
		public void OnExitFinishedState()
		{
			this._underlyingWaterSource.RemoveWaterStrengthModifier(this);
			WaterSource waterSource = this._underlyingWaterSource.WaterSource;
			if (waterSource != null)
			{
				waterSource.GetComponent<UndergroundWaterSource>().SetUnoccupied();
			}
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002375 File Offset: 0x00000575
		public float GetStrengthModifier()
		{
			if (this._hazardousWeatherObserver.IsBadtideWeather)
			{
				return 0f;
			}
			if (!this._mechanicalNode.ActiveAndPowered)
			{
				return 0f;
			}
			return this._mechanicalNode.PowerEfficiency;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023A8 File Offset: 0x000005A8
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			if (!this._isBlocking)
			{
				this.Block();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B8 File Offset: 0x000005B8
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (this._isBlocking)
			{
				this._blockableObject.Unblock(this);
				this._isBlocking = false;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023D5 File Offset: 0x000005D5
		public void Block()
		{
			this._isBlocking = true;
			this._blockableObject.Block(this);
		}

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public UnderlyingWaterSource _underlyingWaterSource;

		// Token: 0x04000011 RID: 17
		public HazardousWeatherObserver _hazardousWeatherObserver;

		// Token: 0x04000012 RID: 18
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000013 RID: 19
		public BlockableObject _blockableObject;

		// Token: 0x04000014 RID: 20
		public bool _isBlocking;
	}
}
