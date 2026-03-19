using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateAttachmentSystem;
using UnityEngine;

namespace Timberborn.Particles
{
	// Token: 0x0200000E RID: 14
	public class ParticlesCache : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002918 File Offset: 0x00000B18
		public ParticlesCache(ParticlesFastForwarder particlesFastForwarder)
		{
			this._particlesFastForwarder = particlesFastForwarder;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002932 File Offset: 0x00000B32
		public void Awake()
		{
			this._particlesRunnerCreator = base.GetComponent<ParticlesRunnerCreator>();
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000294C File Offset: 0x00000B4C
		public void DeleteEntity()
		{
			foreach (ParticlesRunner particlesRunner in this._particlesRunnerCache.Values)
			{
				this._particlesFastForwarder.Unregister(particlesRunner);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029AC File Offset: 0x00000BAC
		public ParticlesRunner GetParticlesRunner(string attachmentId)
		{
			return this.GetParticlesRunner(ImmutableArray.Create<string>(attachmentId));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029C0 File Offset: 0x00000BC0
		public ParticlesRunner GetParticlesRunner(IList<string> attachmentIds)
		{
			string cacheKey = ParticlesCache.GetCacheKey(attachmentIds);
			ParticlesRunner result;
			if (this._particlesRunnerCache.TryGetValue(cacheKey, out result))
			{
				return result;
			}
			ParticlesRunner particlesRunner = this._particlesRunnerCreator.Create(this.CreateParticleAttachments(attachmentIds));
			this._particlesFastForwarder.Register(particlesRunner);
			this._particlesRunnerCache[cacheKey] = particlesRunner;
			return particlesRunner;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A13 File Offset: 0x00000C13
		public static string GetCacheKey(IEnumerable<string> attachmentIds)
		{
			return string.Join(ParticlesCache.IdSeparator, from attachmentId in attachmentIds
			orderby attachmentId
			select attachmentId);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A44 File Offset: 0x00000C44
		public List<ParticleSystem> CreateParticleAttachments(IEnumerable<string> attachmentIds)
		{
			List<ParticleSystem> list = new List<ParticleSystem>();
			foreach (string id in attachmentIds)
			{
				list.AddRange(this._templateAttachments.GetOrCreateAttachment(id).Transform.GetComponentsInChildren<ParticleSystem>(true));
			}
			return list;
		}

		// Token: 0x04000019 RID: 25
		public static readonly string IdSeparator = ",";

		// Token: 0x0400001A RID: 26
		public readonly ParticlesFastForwarder _particlesFastForwarder;

		// Token: 0x0400001B RID: 27
		public ParticlesRunnerCreator _particlesRunnerCreator;

		// Token: 0x0400001C RID: 28
		public TemplateAttachments _templateAttachments;

		// Token: 0x0400001D RID: 29
		public readonly Dictionary<string, ParticlesRunner> _particlesRunnerCache = new Dictionary<string, ParticlesRunner>();
	}
}
