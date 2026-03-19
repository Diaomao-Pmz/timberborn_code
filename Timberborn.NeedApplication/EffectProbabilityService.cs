using System;
using System.Collections.Frozen;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.GameFactionSystem;
using Timberborn.GameSceneLoading;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200000D RID: 13
	public class EffectProbabilityService : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002A33 File Offset: 0x00000C33
		public EffectProbabilityService(ISceneLoader sceneLoader, ISingletonLoader singletonLoader, IRandomNumberGenerator randomNumberGenerator, ISpecService specService, FactionNeedService factionNeedService)
		{
			this._sceneLoader = sceneLoader;
			this._singletonLoader = singletonLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._specService = specService;
			this._factionNeedService = factionNeedService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A60 File Offset: 0x00000C60
		public bool CanApplyEffects
		{
			get
			{
				return this._injuryChanceModifier > 0f;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A70 File Offset: 0x00000C70
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(EffectProbabilityService.EffectProbabilityServiceKey, out objectLoader))
			{
				this._injuryChanceModifier = objectLoader.Get(EffectProbabilityService.InjuryChanceModifierKey);
			}
			else
			{
				GameModeSpec gameMode = this._sceneLoader.GetSceneParameters<GameSceneParameters>().NewGameConfiguration.GameMode;
				this._injuryChanceModifier = gameMode.InjuryChance;
			}
			this._probabilityGroups = this._specService.GetSingleSpec<ProbabilityGroupsSpec>().Groups.ToFrozenDictionary((ProbabilityGroupSpec group) => group.Id, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B06 File Offset: 0x00000D06
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(EffectProbabilityService.EffectProbabilityServiceKey).Set(EffectProbabilityService.InjuryChanceModifierKey, this._injuryChanceModifier);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B24 File Offset: 0x00000D24
		public bool CanApply(IProbabilityGroupProvider probabilityGroupProvider, NeedApplierEffectSpec spec)
		{
			if (this.CanApplyEffects)
			{
				float effectProbability = this.GetEffectProbability(spec, probabilityGroupProvider.ProbabilityGroupId);
				return this._randomNumberGenerator.CheckProbability(effectProbability);
			}
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B58 File Offset: 0x00000D58
		public unsafe float GetEffectProbability(NeedApplierEffectSpec spec, string probabilityGroupId)
		{
			ProbabilityGroupSpec probabilityGroupSpec = *this._probabilityGroups[probabilityGroupId];
			float num;
			switch (spec.Probability)
			{
			case EffectProbability.Low:
				num = probabilityGroupSpec.Low;
				break;
			case EffectProbability.Medium:
				num = probabilityGroupSpec.Medium;
				break;
			case EffectProbability.High:
				num = probabilityGroupSpec.High;
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unknown probability: {0}", spec.Probability));
			}
			float num2 = num;
			if (!this._factionNeedService.GetBeaverOrBotNeedById(spec.NeedId).HasSpec<InjurableNeedSpec>())
			{
				return num2;
			}
			return num2 * this._injuryChanceModifier;
		}

		// Token: 0x04000025 RID: 37
		public static readonly SingletonKey EffectProbabilityServiceKey = new SingletonKey("EffectProbabilityService");

		// Token: 0x04000026 RID: 38
		public static readonly PropertyKey<float> InjuryChanceModifierKey = new PropertyKey<float>("InjuryChanceModifier");

		// Token: 0x04000027 RID: 39
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000028 RID: 40
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000029 RID: 41
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002A RID: 42
		public readonly ISpecService _specService;

		// Token: 0x0400002B RID: 43
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x0400002C RID: 44
		public float _injuryChanceModifier;

		// Token: 0x0400002D RID: 45
		public FrozenDictionary<string, ProbabilityGroupSpec> _probabilityGroups;
	}
}
