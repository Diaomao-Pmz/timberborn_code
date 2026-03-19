using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000017 RID: 23
	public class TutorialStageService : ILoadableSingleton
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002E0B File Offset: 0x0000100B
		public TutorialStageService(ISpecService specService, IEnumerable<IStepDeserializer> stepDeserializers)
		{
			this._specService = specService;
			this._stepDeserializers = stepDeserializers.ToImmutableArray<IStepDeserializer>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E34 File Offset: 0x00001034
		public void Load()
		{
			foreach (TutorialStageSpec tutorialStageSpec in this._specService.GetSpecs<TutorialStageSpec>())
			{
				this._stages.Add(tutorialStageSpec.Id, tutorialStageSpec);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E94 File Offset: 0x00001094
		public TutorialStage GetStage(string stageId)
		{
			TutorialStageSpec tutorialStageSpec = this._stages[stageId];
			return new TutorialStage(tutorialStageSpec.Id, tutorialStageSpec.Intro.Value, this.GetSteps(tutorialStageSpec.Blueprint.Children));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002ED5 File Offset: 0x000010D5
		public IEnumerable<TutorialStep> GetSteps(ImmutableArray<Blueprint> steps)
		{
			foreach (Blueprint step in steps)
			{
				ImmutableArray<IStepDeserializer>.Enumerator enumerator2 = this._stepDeserializers.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					TutorialStep tutorialStep;
					if (enumerator2.Current.TryDeserialize(step, out tutorialStep))
					{
						yield return tutorialStep;
						break;
					}
				}
			}
			ImmutableArray<Blueprint>.Enumerator enumerator = default(ImmutableArray<Blueprint>.Enumerator);
			yield break;
		}

		// Token: 0x04000034 RID: 52
		public readonly ISpecService _specService;

		// Token: 0x04000035 RID: 53
		public readonly ImmutableArray<IStepDeserializer> _stepDeserializers;

		// Token: 0x04000036 RID: 54
		public readonly Dictionary<string, TutorialStageSpec> _stages = new Dictionary<string, TutorialStageSpec>();
	}
}
