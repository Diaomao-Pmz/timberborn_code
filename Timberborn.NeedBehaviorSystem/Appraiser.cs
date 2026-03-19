using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.NeedSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200000A RID: 10
	public class Appraiser : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024C2 File Offset: 0x000006C2
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024D0 File Offset: 0x000006D0
		public float AppraiseEffects(IEnumerable<InstantEffect> instantEffects)
		{
			float num = 0f;
			foreach (InstantEffect instantEffect in instantEffects)
			{
				NeedManager needManager = this._needManager;
				string needId = instantEffect.NeedId;
				Effect effect = Effect.From(instantEffect);
				float num2;
				if (!needManager.TryAppraise(needId, effect, out num2))
				{
					return 0f;
				}
				num += num2;
			}
			return num;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000254C File Offset: 0x0000074C
		public float AppraiseEffect(in EssentialAction essentialAction)
		{
			string needId = essentialAction.Effect.NeedId;
			InstantEffect instantEffect = InstantEffect.DiscretizeContinuousEffect(needId);
			NeedManager needManager = this._needManager;
			string needId2 = needId;
			Effect effect = Effect.From(instantEffect);
			float result;
			if (!needManager.TryAppraise(needId2, effect, out result))
			{
				return 0f;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002590 File Offset: 0x00000790
		public float AppraiseEffects(ImmutableArray<InstantEffect> effects, NeedFilter needFilter)
		{
			float num = 0f;
			bool flag = false;
			for (int i = 0; i < effects.Length; i++)
			{
				InstantEffect instantEffect = effects[i];
				string needId = instantEffect.NeedId;
				NeedManager needManager = this._needManager;
				string needId2 = needId;
				Effect effect = Effect.From(instantEffect);
				float num2;
				if (!needManager.TryAppraise(needId2, effect, out num2))
				{
					return 0f;
				}
				if (needFilter.Filter(needId) && num2 > 0f)
				{
					flag = true;
				}
				num += num2;
			}
			if (!flag)
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x0400001E RID: 30
		public NeedManager _needManager;
	}
}
