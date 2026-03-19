using System;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.Achievements
{
	// Token: 0x02000024 RID: 36
	public class DemolishAndRebuildAchievement : Achievement
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000035C2 File Offset: 0x000017C2
		public DemolishAndRebuildAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000035D1 File Offset: 0x000017D1
		public override string Id
		{
			get
			{
				return "DEMOLISH_AND_REBUILD";
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000035D8 File Offset: 0x000017D8
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			BlockObject blockObject = blockObjectSetEvent.BlockObject;
			if (!blockObject.IsPreview && blockObject.HasComponent<Building>())
			{
				if (Time.unscaledTime - this._lastDeletionTime < DemolishAndRebuildAchievement.TimeLimit && this.IsSameBuilding(blockObject))
				{
					base.Unlock();
					return;
				}
				this.Reset();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003628 File Offset: 0x00001828
		[OnEvent]
		public void OnExitedFinishedStateEvent(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			BlockObject blockObject = exitedFinishedStateEvent.BlockObject;
			if (blockObject.HasComponent<Building>())
			{
				this._lastDeletionPlacement = blockObject.Placement;
				this._lastDeletionId = blockObject.GetComponent<TemplateSpec>().TemplateName;
				this._lastDeletionTime = Time.unscaledTime;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000366C File Offset: 0x0000186C
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000367A File Offset: 0x0000187A
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003688 File Offset: 0x00001888
		public bool IsSameBuilding(BlockObject blockObject)
		{
			return !string.IsNullOrWhiteSpace(this._lastDeletionId) && blockObject.GetComponent<TemplateSpec>().TemplateName == this._lastDeletionId && blockObject.Placement == this._lastDeletionPlacement;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000036C2 File Offset: 0x000018C2
		public void Reset()
		{
			this._lastDeletionId = null;
			this._lastDeletionTime = 0f;
		}

		// Token: 0x0400004D RID: 77
		public static readonly float TimeLimit = 60f;

		// Token: 0x0400004E RID: 78
		public readonly EventBus _eventBus;

		// Token: 0x0400004F RID: 79
		public string _lastDeletionId;

		// Token: 0x04000050 RID: 80
		public Placement _lastDeletionPlacement;

		// Token: 0x04000051 RID: 81
		public float _lastDeletionTime;
	}
}
