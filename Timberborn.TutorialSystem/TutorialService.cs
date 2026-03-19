using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.FactionSystem;
using Timberborn.GameFactionSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000010 RID: 16
	public class TutorialService : ITutorialService, ISaveableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000229C File Offset: 0x0000049C
		public TutorialService(ISingletonLoader singletonLoader, EventBus eventBus, ISpecService specService, FactionService factionService, TutorialStageService tutorialStageService)
		{
			this._singletonLoader = singletonLoader;
			this._eventBus = eventBus;
			this._specService = specService;
			this._factionService = factionService;
			this._tutorialStageService = tutorialStageService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002300 File Offset: 0x00000500
		[BackwardCompatible(2025, 10, 21, Compatibility.Save)]
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(TutorialService.TutorialServiceKey);
			if (this._oldTutorialWasFinished)
			{
				singleton.Set(new PropertyKey<bool>("FinishedTutorial"), true);
			}
			singleton.Set(TutorialService.FinishedTutorialsKey, this._finishedTutorials);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002344 File Offset: 0x00000544
		[BackwardCompatible(2025, 10, 21, Compatibility.Save)]
		public void Load()
		{
			IObjectLoader objectLoader;
			if (!this._singletonLoader.TryGetSingleton(TutorialService.TutorialServiceKey, out objectLoader))
			{
				this.StartNewTutorial();
				return;
			}
			PropertyKey<bool> key = new PropertyKey<bool>("FinishedTutorial");
			if (objectLoader.Has<bool>(key) && objectLoader.Get(key))
			{
				this._oldTutorialWasFinished = true;
				return;
			}
			if (objectLoader.Has<string>(TutorialService.FinishedTutorialsKey))
			{
				this._finishedTutorials.AddRange(objectLoader.Get(TutorialService.FinishedTutorialsKey));
			}
			this.StartNewTutorial();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023BB File Offset: 0x000005BB
		public void PostLoad()
		{
			this.FastForwardTutorial();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023C3 File Offset: 0x000005C3
		public void AddTutorialTrigger(string triggerId)
		{
			this._finishedTutorials.Add(triggerId);
			this.UpdateStages(triggerId);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023D8 File Offset: 0x000005D8
		public bool TutorialWasFinished(string tutorialId)
		{
			return this._finishedTutorials.Contains(tutorialId);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023E8 File Offset: 0x000005E8
		public void StartNextStage(string tutorialId)
		{
			Queue<TutorialStage> queue = this._waitingTutorialStages[tutorialId];
			if (!queue.IsEmpty<TutorialStage>())
			{
				TutorialStage tutorialStage = queue.Dequeue();
				this._activeTutorialStages[tutorialId] = tutorialStage;
				this._eventBus.Post(new TutorialStageStartedEvent(tutorialId, tutorialStage));
				return;
			}
			this._waitingTutorialStages.Remove(tutorialId);
			this._activeTutorialStages.Remove(tutorialId);
			this._finishedTutorials.Add(tutorialId);
			this._eventBus.Post(new TutorialFinishedEvent(tutorialId));
			this.UpdateStages(tutorialId);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002470 File Offset: 0x00000670
		public void StartNewTutorial()
		{
			this.GetConfigurations();
			foreach (TutorialConfiguration tutorialConfiguration in this._tutorialConfigurations)
			{
				if (!this._finishedTutorials.Contains(tutorialConfiguration.TutorialId) && this.RequirementsMet(tutorialConfiguration.RequiredTutorialIds))
				{
					this.StartNewTutorial(tutorialConfiguration);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024EC File Offset: 0x000006EC
		public void GetConfigurations()
		{
			if (this._factionService.Current.HasSpec<StartingFactionSpec>())
			{
				foreach (TutorialSpec tutorialSpec in from spec in this._specService.GetSpecs<TutorialSpec>()
				orderby spec.SortOrder
				select spec)
				{
					this._tutorialConfigurations.Add(new TutorialConfiguration(tutorialSpec, this.GetStages(tutorialSpec.Stages)));
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000258C File Offset: 0x0000078C
		public IEnumerable<TutorialStage> GetStages(ImmutableArray<string> stages)
		{
			foreach (string stageId in stages)
			{
				yield return this._tutorialStageService.GetStage(stageId);
			}
			ImmutableArray<string>.Enumerator enumerator = default(ImmutableArray<string>.Enumerator);
			yield break;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025A4 File Offset: 0x000007A4
		public void FastForwardTutorial()
		{
			foreach (string text in this._activeTutorialStages.Keys.ToList<string>())
			{
				TutorialStage tutorialStage = this._activeTutorialStages[text];
				while ((tutorialStage != null && tutorialStage.HasSteps && tutorialStage.AllStepsAchieved) || (tutorialStage != null && !tutorialStage.HasSteps && this.NextStageIsAchieved(text)))
				{
					this.StartNextStage(text);
					this._activeTutorialStages.TryGetValue(text, out tutorialStage);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002648 File Offset: 0x00000848
		public bool NextStageIsAchieved(string tutorialId)
		{
			foreach (TutorialStage tutorialStage in this._waitingTutorialStages[tutorialId])
			{
				if (tutorialStage.HasSteps && tutorialStage.AllStepsAchieved)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026B4 File Offset: 0x000008B4
		public void UpdateStages(string finishedTutorialId)
		{
			Predicate<string> <>9__0;
			foreach (TutorialConfiguration tutorialConfiguration in this._tutorialConfigurations)
			{
				IReadOnlyList<string> source = tutorialConfiguration.RequiredTutorialIds;
				Predicate<string> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((string id) => id == finishedTutorialId));
				}
				if (source.FastAny(predicate) && this.RequirementsMet(tutorialConfiguration.RequiredTutorialIds) && !this._finishedTutorials.Contains(tutorialConfiguration.TutorialId))
				{
					this.StartNewTutorial(tutorialConfiguration);
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002768 File Offset: 0x00000968
		public void StartNewTutorial(TutorialConfiguration configuration)
		{
			if (!string.IsNullOrEmpty(configuration.SkipIfTutorialFinished) && this._finishedTutorials.Contains(configuration.SkipIfTutorialFinished))
			{
				this._finishedTutorials.Add(configuration.TutorialId);
				return;
			}
			string tutorialId = configuration.TutorialId;
			this._waitingTutorialStages[tutorialId] = new Queue<TutorialStage>(configuration.TutorialStages);
			this._eventBus.Post(new TutorialCreatedEvent(configuration));
			this.StartNextStage(tutorialId);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool RequirementsMet(ImmutableArray<string> requiredTutorialIds)
		{
			if (requiredTutorialIds.IsDefaultOrEmpty)
			{
				return true;
			}
			foreach (string item in requiredTutorialIds)
			{
				if (!this._finishedTutorials.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000011 RID: 17
		public static readonly SingletonKey TutorialServiceKey = new SingletonKey("TutorialService");

		// Token: 0x04000012 RID: 18
		public static readonly ListKey<string> FinishedTutorialsKey = new ListKey<string>("FinishedTutorials");

		// Token: 0x04000013 RID: 19
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000014 RID: 20
		public readonly EventBus _eventBus;

		// Token: 0x04000015 RID: 21
		public readonly ISpecService _specService;

		// Token: 0x04000016 RID: 22
		public readonly FactionService _factionService;

		// Token: 0x04000017 RID: 23
		public readonly TutorialStageService _tutorialStageService;

		// Token: 0x04000018 RID: 24
		public readonly List<TutorialConfiguration> _tutorialConfigurations = new List<TutorialConfiguration>();

		// Token: 0x04000019 RID: 25
		public readonly Dictionary<string, Queue<TutorialStage>> _waitingTutorialStages = new Dictionary<string, Queue<TutorialStage>>();

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<string, TutorialStage> _activeTutorialStages = new Dictionary<string, TutorialStage>();

		// Token: 0x0400001B RID: 27
		public readonly List<string> _finishedTutorials = new List<string>();

		// Token: 0x0400001C RID: 28
		public bool _oldTutorialWasFinished;
	}
}
