using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WindSystem
{
	// Token: 0x02000011 RID: 17
	public class WindService : ITickableSingleton, IUpdatableSingleton, ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002B8D File Offset: 0x00000D8D
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002B95 File Offset: 0x00000D95
		public float WindStrength { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002B9E File Offset: 0x00000D9E
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002BA6 File Offset: 0x00000DA6
		public Vector2 WindDirection { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002BAF File Offset: 0x00000DAF
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public bool IsForcedWind { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public WindService(ISingletonLoader singletonLoader, IRandomNumberGenerator randomNumberGenerator, IDayNightCycle dayNightCycle, EventBus eventBus, ISpecService specService)
		{
			this._singletonLoader = singletonLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._dayNightCycle = dayNightCycle;
			this._eventBus = eventBus;
			this._specService = specService;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void Load()
		{
			this._windServiceSpec = this._specService.GetSingleSpec<WindServiceSpec>();
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WindService.WindServiceKey, out objectLoader))
			{
				this.WindStrength = objectLoader.Get(WindService.WindStrengthKey);
				this.WindDirection = objectLoader.Get(WindService.WindDirectionKey);
				this._nextWindChangeTime = objectLoader.Get(WindService.NextWindChangeTimeKey);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002C55 File Offset: 0x00000E55
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(WindService.WindServiceKey);
			singleton.Set(WindService.WindStrengthKey, this.WindStrength);
			singleton.Set(WindService.WindDirectionKey, this.WindDirection);
			singleton.Set(WindService.NextWindChangeTimeKey, this._nextWindChangeTime);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C94 File Offset: 0x00000E94
		public void UpdateSingleton()
		{
			this.UpdateShaderProperties();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void Tick()
		{
			if (!this.IsForcedWind && this._dayNightCycle.PartialDayNumber >= this._nextWindChangeTime)
			{
				this.ChangeWind();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002CBF File Offset: 0x00000EBF
		public void ToggleForcedWind()
		{
			this.IsForcedWind = !this.IsForcedWind;
			this.ChangeWind();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public void ChangeWind()
		{
			this._shaderWindStrength = this.WindStrength;
			float hours = this._randomNumberGenerator.Range(this._windServiceSpec.MinWindTimeInHours, this._windServiceSpec.MaxWindTimeInHours);
			this._nextWindChangeTime = this._dayNightCycle.DayNumberHoursFromNow(hours);
			this.WindDirection = this._randomNumberGenerator.InsideUnitCircle();
			this.WindStrength = this._randomNumberGenerator.Range(this._windServiceSpec.MinWindStrength, this._windServiceSpec.MaxWindStrength);
			this.WindDirection.Normalize();
			this.UpdateShaderProperties();
			this._eventBus.Post(new WindChangedEvent());
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D84 File Offset: 0x00000F84
		public void UpdateShaderProperties()
		{
			if (!Mathf.Approximately(this._shaderWindStrength, this.WindStrength))
			{
				this._shaderWindStrength = Mathf.MoveTowards(this._shaderWindStrength, this.WindStrength, Time.deltaTime);
				Shader.SetGlobalFloat(WindService.WindStrengthProperty, this._shaderWindStrength);
			}
		}

		// Token: 0x0400001B RID: 27
		public static readonly int WindStrengthProperty = Shader.PropertyToID("_WindStrength");

		// Token: 0x0400001C RID: 28
		public static readonly SingletonKey WindServiceKey = new SingletonKey("WindService");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<float> WindStrengthKey = new PropertyKey<float>("WindStrength");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<Vector2> WindDirectionKey = new PropertyKey<Vector2>("WindDirection");

		// Token: 0x0400001F RID: 31
		public static readonly PropertyKey<float> NextWindChangeTimeKey = new PropertyKey<float>("NextWindChangeTime");

		// Token: 0x04000023 RID: 35
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000024 RID: 36
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000025 RID: 37
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000026 RID: 38
		public readonly EventBus _eventBus;

		// Token: 0x04000027 RID: 39
		public readonly ISpecService _specService;

		// Token: 0x04000028 RID: 40
		public WindServiceSpec _windServiceSpec;

		// Token: 0x04000029 RID: 41
		public float _nextWindChangeTime;

		// Token: 0x0400002A RID: 42
		public float _shaderWindStrength;
	}
}
