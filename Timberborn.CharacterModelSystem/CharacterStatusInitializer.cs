using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MortalComponents;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000E RID: 14
	public class CharacterStatusInitializer : BaseComponent, IAwakableComponent, IDeadNeededComponent
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000029AC File Offset: 0x00000BAC
		public void Awake()
		{
			Transform model = base.GetComponent<CharacterModel>().Model;
			base.GetComponent<StatusIconCycler>().InitializeIcon(model, 0.4f);
		}
	}
}
