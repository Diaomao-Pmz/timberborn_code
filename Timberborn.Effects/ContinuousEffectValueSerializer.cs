using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;
using Timberborn.Persistence;
using UnityEngine;

namespace Timberborn.Effects
{
	// Token: 0x02000005 RID: 5
	public class ContinuousEffectValueSerializer : IValueSerializer<ContinuousEffect>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000217B File Offset: 0x0000037B
		public ContinuousEffectValueSerializer(FactionNeedService factionNeedService)
		{
			this._factionNeedService = factionNeedService;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000218A File Offset: 0x0000038A
		public void Serialize(ContinuousEffect value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(ContinuousEffectValueSerializer.NeedIdKey, value.NeedId);
			objectSaver.Set(ContinuousEffectValueSerializer.PointsPerHourKey, value.PointsPerHour);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B8 File Offset: 0x000003B8
		public Obsoletable<ContinuousEffect> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			string text = objectLoader.Get(ContinuousEffectValueSerializer.NeedIdKey);
			float pointsPerHour = objectLoader.Get(ContinuousEffectValueSerializer.PointsPerHourKey);
			if (!this.NeedSpecs.Contains(text))
			{
				Debug.Log("Need " + text + " found in save doesn't exist, ignoring it.");
				return default(Obsoletable<ContinuousEffect>);
			}
			return new ContinuousEffect(text, pointsPerHour);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000221B File Offset: 0x0000041B
		public IEnumerable<string> NeedSpecs
		{
			get
			{
				return from need in this._factionNeedService.Needs
				select need.Id;
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<string> NeedIdKey = new PropertyKey<string>("NeedId");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<float> PointsPerHourKey = new PropertyKey<float>("PointsPerHour");

		// Token: 0x0400000A RID: 10
		public readonly FactionNeedService _factionNeedService;
	}
}
