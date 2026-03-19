using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000023 RID: 35
	public class UnfinishedStateSlotDisabler : BaseComponent, IAwakableComponent, IUnfinishedStateListener
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000042A7 File Offset: 0x000024A7
		public UnfinishedStateSlotDisabler(SlotRetriever slotRetriever)
		{
			this._slotRetriever = slotRetriever;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000042B8 File Offset: 0x000024B8
		public void Awake()
		{
			UnfinishedStateSlotDisablerSpec component = base.GetComponent<UnfinishedStateSlotDisablerSpec>();
			this._slots = this._slotRetriever.GetSlots(base.GameObject, component.SlotKeyword).ToImmutableArray<Transform>();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000042F0 File Offset: 0x000024F0
		public void OnEnterUnfinishedState()
		{
			foreach (Transform transform in this._slots)
			{
				transform.gameObject.SetActive(false);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004328 File Offset: 0x00002528
		public void OnExitUnfinishedState()
		{
			foreach (Transform transform in this._slots)
			{
				transform.gameObject.SetActive(true);
			}
		}

		// Token: 0x04000050 RID: 80
		public readonly SlotRetriever _slotRetriever;

		// Token: 0x04000051 RID: 81
		public ImmutableArray<Transform> _slots;
	}
}
