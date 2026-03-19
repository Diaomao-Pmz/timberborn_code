using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.LifeSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.Beavers
{
	// Token: 0x0200000C RID: 12
	public class BeaverFactory : ILoadableSingleton
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000022ED File Offset: 0x000004ED
		public BeaverFactory(TemplateService templateService, EntityService entityService, LifeService lifeService, TemplateInstantiator templateInstantiator)
		{
			this._templateService = templateService;
			this._entityService = entityService;
			this._lifeService = lifeService;
			this._templateInstantiator = templateInstantiator;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002320 File Offset: 0x00000520
		public void Load()
		{
			this._adultTemplate = this._templateService.GetSingle<AdultSpec>().Blueprint;
			this._childTemplate = this._templateService.GetSingle<ChildSpec>().Blueprint;
			this._templateInstantiator.CacheInstance(this._adultTemplate);
			this._templateInstantiator.CacheInstance(this._childTemplate);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000237C File Offset: 0x0000057C
		public void CreateAdult(Vector3 position, float adulthoodProgress)
		{
			float lifeProgress = this._lifeService.AdulthoodProgressToLifeProgress(adulthoodProgress);
			this.CreateAndInitialize(this._adultTemplate, position, lifeProgress);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023A8 File Offset: 0x000005A8
		public Beaver CreateChild(Vector3 position, float childhoodProgress)
		{
			float lifeProgress = this._lifeService.ChildhoodProgressToLifeProgress(childhoodProgress);
			Beaver beaver = this.CreateAndInitialize(this._childTemplate, position, lifeProgress);
			beaver.GetComponent<Child>().FastForwardGrowthProgress(childhoodProgress);
			return beaver;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023DC File Offset: 0x000005DC
		public Beaver CreateNewbornAdult(Vector3 position)
		{
			return this.CreateAndInitialize(this._adultTemplate, position, 0f);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F0 File Offset: 0x000005F0
		public Beaver CreateNewbornChild(Vector3 position)
		{
			return this.CreateChild(position, 0f);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002400 File Offset: 0x00000600
		public Beaver CreateAdultFromChild(Child child)
		{
			Character component = child.GetComponent<Character>();
			Beaver beaver = this.CreateNewBeaver(this._adultTemplate, child.Transform.position);
			this.InfluenceAdultWithChildhood(beaver, component);
			return beaver;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002435 File Offset: 0x00000635
		public Beaver CreateAndInitialize(Blueprint template, Vector3 position, float lifeProgress)
		{
			Beaver beaver = this.CreateNewBeaver(template, position);
			beaver.GetComponent<Character>().DayOfBirth = this._lifeService.CalculateDayOfBirth(lifeProgress);
			beaver.GetComponent<LifeProgressor>().LifeProgress = lifeProgress;
			return beaver;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002462 File Offset: 0x00000662
		public Beaver CreateNewBeaver(Blueprint template, Vector3 position)
		{
			EntityComponent entityComponent = this._entityService.Instantiate(template);
			entityComponent.Transform.position = position;
			return entityComponent.GetComponent<Beaver>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002484 File Offset: 0x00000684
		public void InfluenceAdultWithChildhood(Beaver adult, Character child)
		{
			adult.GetComponents<IChildhoodInfluenced>(this._componentsToInfluence);
			foreach (IChildhoodInfluenced childhoodInfluenced in this._componentsToInfluence)
			{
				childhoodInfluenced.InfluenceByChildhood(child);
			}
			this._componentsToInfluence.Clear();
		}

		// Token: 0x0400000C RID: 12
		public readonly TemplateService _templateService;

		// Token: 0x0400000D RID: 13
		public readonly EntityService _entityService;

		// Token: 0x0400000E RID: 14
		public readonly LifeService _lifeService;

		// Token: 0x0400000F RID: 15
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x04000010 RID: 16
		public readonly List<IChildhoodInfluenced> _componentsToInfluence = new List<IChildhoodInfluenced>();

		// Token: 0x04000011 RID: 17
		public Blueprint _adultTemplate;

		// Token: 0x04000012 RID: 18
		public Blueprint _childTemplate;
	}
}
