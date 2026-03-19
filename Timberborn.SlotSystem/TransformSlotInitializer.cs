using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200001F RID: 31
	public class TransformSlotInitializer : SlotInitializer
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public TransformSlotInitializer(SlotRetriever slotRetriever, TransformSlotFactory transformSlotFactory)
		{
			this._slotRetriever = slotRetriever;
			this._transformSlotFactory = transformSlotFactory;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003BDC File Offset: 0x00001DDC
		public override IEnumerable<ISlot> InitializeSlots()
		{
			if (this._initialized)
			{
				throw new InvalidOperationException("TransformSlotInitializer at " + base.Name + " already initialized its slots");
			}
			this._initialized = true;
			return base.GetComponent<TransformSlotInitializerSpec>().Slots.SelectMany(new Func<TransformSlotSpec, IEnumerable<ISlot>>(this.InitializeSlotsOfSpec));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C34 File Offset: 0x00001E34
		public IEnumerable<ISlot> InitializeSlotsOfSpec(TransformSlotSpec spec)
		{
			IEnumerable<Transform> slots = this._slotRetriever.GetSlots(base.GameObject, spec.SlotKeyword);
			int i = 0;
			foreach (Transform followedTransform in slots)
			{
				int num = i + 1;
				i = num;
				yield return this._transformSlotFactory.Create(followedTransform, spec);
			}
			IEnumerator<Transform> enumerator = null;
			if (i == 0)
			{
				throw new InvalidOperationException("There are no \"" + spec.SlotKeyword + "\" slots in " + base.Name);
			}
			yield break;
			yield break;
		}

		// Token: 0x0400003F RID: 63
		public readonly SlotRetriever _slotRetriever;

		// Token: 0x04000040 RID: 64
		public readonly TransformSlotFactory _transformSlotFactory;

		// Token: 0x04000041 RID: 65
		public bool _initialized;
	}
}
