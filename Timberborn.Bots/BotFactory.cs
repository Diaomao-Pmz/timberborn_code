using System;
using Timberborn.BlueprintSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.TemplateSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Bots
{
	// Token: 0x0200000A RID: 10
	public class BotFactory : ILoadableSingleton
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022A1 File Offset: 0x000004A1
		public BotFactory(TemplateService templateService, EntityService entityService, IDayNightCycle dayNightCycle, TemplateInstantiator templateInstantiator)
		{
			this._templateService = templateService;
			this._entityService = entityService;
			this._dayNightCycle = dayNightCycle;
			this._templateInstantiator = templateInstantiator;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022C6 File Offset: 0x000004C6
		public void Load()
		{
			this._botTemplate = this._templateService.GetSingle<BotSpec>().Blueprint;
			this._templateInstantiator.CacheInstance(this._botTemplate);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022EF File Offset: 0x000004EF
		public void Create(Vector3 position)
		{
			this.Create(position, Quaternion.identity);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022FE File Offset: 0x000004FE
		public Bot Create(Vector3 position, Quaternion rotation)
		{
			EntityComponent entityComponent = this._entityService.Instantiate(this._botTemplate);
			entityComponent.GetComponent<Character>().DayOfBirth = this._dayNightCycle.DayNumber;
			entityComponent.Transform.SetPositionAndRotation(position, rotation);
			return entityComponent.GetComponent<Bot>();
		}

		// Token: 0x0400000C RID: 12
		public readonly TemplateService _templateService;

		// Token: 0x0400000D RID: 13
		public readonly EntityService _entityService;

		// Token: 0x0400000E RID: 14
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000F RID: 15
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x04000010 RID: 16
		public Blueprint _botTemplate;
	}
}
