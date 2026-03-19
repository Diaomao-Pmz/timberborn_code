using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;
using Timberborn.WaterObjects;

namespace Timberborn.Achievements
{
	// Token: 0x02000027 RID: 39
	public class FloodBuildingAchievement : Achievement
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003826 File Offset: 0x00001A26
		public FloodBuildingAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003840 File Offset: 0x00001A40
		public override string Id
		{
			get
			{
				return "FLOOD_BUILDING";
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003848 File Offset: 0x00001A48
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			if (blockObject.HasComponent<FloodableBuilding>())
			{
				FloodableObject component = blockObject.GetComponent<FloodableObject>();
				if (component != null)
				{
					if (component.IsFlooded)
					{
						base.Unlock();
						return;
					}
					this._floodableBuildings.Add(component);
					component.Flooded += this.OnFlooded;
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000389C File Offset: 0x00001A9C
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			BlockObject blockObject = exitedFinishedStateEvent.BlockObject;
			if (blockObject.HasComponent<FloodableBuilding>())
			{
				FloodableObject component = blockObject.GetComponent<FloodableObject>();
				if (component != null)
				{
					this._floodableBuildings.Remove(component);
					component.Flooded -= this.OnFlooded;
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000038E1 File Offset: 0x00001AE1
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038F0 File Offset: 0x00001AF0
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
			foreach (FloodableObject floodableObject in this._floodableBuildings)
			{
				floodableObject.Flooded -= this.OnFlooded;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000261C File Offset: 0x0000081C
		public void OnFlooded(object sender, EventArgs e)
		{
			base.Unlock();
		}

		// Token: 0x04000059 RID: 89
		public readonly EventBus _eventBus;

		// Token: 0x0400005A RID: 90
		public readonly HashSet<FloodableObject> _floodableBuildings = new HashSet<FloodableObject>();
	}
}
