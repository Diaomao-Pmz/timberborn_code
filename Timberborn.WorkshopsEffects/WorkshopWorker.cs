using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000018 RID: 24
	public class WorkshopWorker : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000035E3 File Offset: 0x000017E3
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			this._behaviorManager = base.GetComponent<BehaviorManager>();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000035FD File Offset: 0x000017FD
		public void UpdateVisibility()
		{
			if (this._behaviorManager.IsRunningBehavior<WaitInsideIdlyWorkplaceBehavior>())
			{
				this._characterModel.Show();
				return;
			}
			this._characterModel.Hide();
		}

		// Token: 0x0400003A RID: 58
		public CharacterModel _characterModel;

		// Token: 0x0400003B RID: 59
		public BehaviorManager _behaviorManager;
	}
}
