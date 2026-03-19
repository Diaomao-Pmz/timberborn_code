using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.SingletonSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.Achievements
{
	// Token: 0x02000055 RID: 85
	public class ZiplineNetworkLengthAchievement : Achievement
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000051CE File Offset: 0x000033CE
		public ZiplineNetworkLengthAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000051FE File Offset: 0x000033FE
		public override string Id
		{
			get
			{
				return "ZIPLINE_NETWORK_LENGTH";
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005205 File Offset: 0x00003405
		[OnEvent]
		public void OnZiplineConnectionActivated(ZiplineConnectionActivatedEvent ziplineConnectionActivatedEvent)
		{
			this.CheckLengthFrom(ziplineConnectionActivatedEvent.ZiplineTower);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005213 File Offset: 0x00003413
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005221 File Offset: 0x00003421
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005230 File Offset: 0x00003430
		public void CheckLengthFrom(ZiplineTower startTower)
		{
			this._towersToVisit.Enqueue(startTower);
			while (this._towersToVisit.Count > 0)
			{
				ZiplineTower ziplineTower = this._towersToVisit.Dequeue();
				if (this._visitedTowers.Add(ziplineTower))
				{
					foreach (ZiplineTower ziplineTower2 in ziplineTower.ConnectionTargets)
					{
						this._visitedEdges.Add(new ValueTuple<ZiplineTower, ZiplineTower>(ziplineTower2, ziplineTower));
						if (!this._visitedTowers.Contains(ziplineTower2))
						{
							this._towersToVisit.Enqueue(ziplineTower2);
						}
					}
				}
			}
			float num = 0f;
			foreach (ValueTuple<ZiplineTower, ZiplineTower> valueTuple in this._visitedEdges)
			{
				ZiplineTower item = valueTuple.Item1;
				ZiplineTower item2 = valueTuple.Item2;
				num += Vector3.Distance(item.CableAnchorPoint, item2.CableAnchorPoint);
			}
			this._visitedTowers.Clear();
			this._towersToVisit.Clear();
			this._visitedEdges.Clear();
			if (num >= ZiplineNetworkLengthAchievement.MinimumLength)
			{
				base.Unlock();
			}
		}

		// Token: 0x040000CC RID: 204
		public static readonly float MinimumLength = 1000f;

		// Token: 0x040000CD RID: 205
		public readonly EventBus _eventBus;

		// Token: 0x040000CE RID: 206
		public readonly Queue<ZiplineTower> _towersToVisit = new Queue<ZiplineTower>();

		// Token: 0x040000CF RID: 207
		public readonly HashSet<ZiplineTower> _visitedTowers = new HashSet<ZiplineTower>();

		// Token: 0x040000D0 RID: 208
		public readonly HashSet<ValueTuple<ZiplineTower, ZiplineTower>> _visitedEdges = new HashSet<ValueTuple<ZiplineTower, ZiplineTower>>();
	}
}
