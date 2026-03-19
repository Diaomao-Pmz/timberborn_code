using System;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.TickSystem
{
	// Token: 0x02000019 RID: 25
	public class TickerUnityAdapter : MonoBehaviour
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002B25 File Offset: 0x00000D25
		[Inject]
		public void InjectDependencies(Ticker ticker)
		{
			this._ticker = ticker;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B2E File Offset: 0x00000D2E
		public void Update()
		{
			this._ticker.Update(Time.deltaTime);
		}

		// Token: 0x04000038 RID: 56
		[HideInInspector]
		public Ticker _ticker;
	}
}
