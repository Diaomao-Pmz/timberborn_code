using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200000E RID: 14
	public class PatrollingSlotInitializer : SlotInitializer
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000027C5 File Offset: 0x000009C5
		public PatrollingSlotInitializer(SlotRetriever slotRetriever, PatrollingSlotFactory patrollingSlotFactory)
		{
			this._slotRetriever = slotRetriever;
			this._patrollingSlotFactory = patrollingSlotFactory;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027DC File Offset: 0x000009DC
		public override IEnumerable<ISlot> InitializeSlots()
		{
			if (this._initialized)
			{
				throw new InvalidOperationException("PatrollingSlotInitializer at " + base.Name + " already initialized its slots");
			}
			this._initialized = true;
			return base.GetComponent<PatrollingSlotInitializerSpec>().PatrollingSlots.SelectMany(new Func<PatrollingSlotSpec, IEnumerable<ISlot>>(this.InitializeSlotsOfSpec));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002834 File Offset: 0x00000A34
		public IEnumerable<ISlot> InitializeSlotsOfSpec(PatrollingSlotSpec spec)
		{
			IEnumerable<Transform> slots = this._slotRetriever.GetSlots(base.GameObject, spec.SlotKeyword);
			int i = 0;
			foreach (Transform transform in slots)
			{
				GameObject gameObject = transform.gameObject;
				string format = "{0}{1}";
				object arg = "PatrollingSlot";
				int num = i;
				i = num + 1;
				yield return this.CreateSlot(gameObject, spec, string.Format(format, arg, num));
			}
			IEnumerator<Transform> enumerator = null;
			if (i == 0)
			{
				throw new InvalidOperationException("There are no \"" + spec.SlotKeyword + "\" slots in " + base.Name);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000284C File Offset: 0x00000A4C
		public PatrollingSlot CreateSlot(GameObject emptyInModel, PatrollingSlotSpec spec, string slotObjectName)
		{
			ValueTuple<Transform, Transform> startAndEnd = this._slotRetriever.GetStartAndEnd(emptyInModel);
			Transform item = startAndEnd.Item1;
			Transform item2 = startAndEnd.Item2;
			Transform transform = new GameObject(slotObjectName).transform;
			transform.parent = base.Transform;
			return this._patrollingSlotFactory.Create(transform, item, item2, spec);
		}

		// Token: 0x04000015 RID: 21
		public readonly SlotRetriever _slotRetriever;

		// Token: 0x04000016 RID: 22
		public readonly PatrollingSlotFactory _patrollingSlotFactory;

		// Token: 0x04000017 RID: 23
		public bool _initialized;
	}
}
