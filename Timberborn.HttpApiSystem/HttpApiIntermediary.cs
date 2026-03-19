using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using Timberborn.EntityNaming;
using Timberborn.SingletonSystem;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200001D RID: 29
	public class HttpApiIntermediary : IUpdatableSingleton
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004378 File Offset: 0x00002578
		public HttpApiIntermediary(UniquelyNamedEntityService uniquelyNamedEntityService)
		{
			this._uniquelyNamedEntityService = uniquelyNamedEntityService;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000043A8 File Offset: 0x000025A8
		public void AddAdapterSnapshot(HttpAdapterSnapshot httpAdapterSnapshot)
		{
			this._adapters[httpAdapterSnapshot.Name] = httpAdapterSnapshot;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000043C0 File Offset: 0x000025C0
		public void RemoveAdapterSnapshot(string name)
		{
			HttpAdapterSnapshot httpAdapterSnapshot;
			this._adapters.TryRemove(name, out httpAdapterSnapshot);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000043DC File Offset: 0x000025DC
		public void AddLeverSnapshot(HttpLeverSnapshot httpLeverSnapshot)
		{
			this._levers[httpLeverSnapshot.Name] = httpLeverSnapshot;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000043F4 File Offset: 0x000025F4
		public void RemoveLeverSnapshot(string name)
		{
			HttpLeverSnapshot httpLeverSnapshot;
			this._levers.TryRemove(name, out httpLeverSnapshot);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004410 File Offset: 0x00002610
		public void AddLeverCommand(HttpLeverCommand httpLeverCommand)
		{
			this._leverCommands.Enqueue(httpLeverCommand);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000441E File Offset: 0x0000261E
		public ImmutableArray<HttpAdapterSnapshot> GetAdapters()
		{
			return this._adapters.Values.ToImmutableArray<HttpAdapterSnapshot>();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004430 File Offset: 0x00002630
		public ImmutableArray<HttpLeverSnapshot> GetLevers()
		{
			return this._levers.Values.ToImmutableArray<HttpLeverSnapshot>();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004442 File Offset: 0x00002642
		public bool TryGetAdapter(string name, out HttpAdapterSnapshot httpAdapterSnapshot)
		{
			return this._adapters.TryGetValue(name, out httpAdapterSnapshot);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004451 File Offset: 0x00002651
		public bool TryGetLever(string name, out HttpLeverSnapshot httpLeverSnapshot)
		{
			return this._levers.TryGetValue(name, out httpLeverSnapshot);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004460 File Offset: 0x00002660
		public void UpdateSingleton()
		{
			HttpLeverCommand httpLeverCommand;
			while (this._leverCommands.TryDequeue(out httpLeverCommand))
			{
				UniquelyNamedEntity uniquelyNamedEntity;
				if (this._uniquelyNamedEntityService.TryGet(httpLeverCommand.Name, out uniquelyNamedEntity))
				{
					HttpLever component = uniquelyNamedEntity.GetComponent<HttpLever>();
					if (component != null)
					{
						if (httpLeverCommand.State != null)
						{
							component.SetState(httpLeverCommand.State.Value);
						}
						if (httpLeverCommand.Color != null)
						{
							component.SetColor(httpLeverCommand.Color.Value);
						}
					}
				}
			}
		}

		// Token: 0x0400007A RID: 122
		public readonly UniquelyNamedEntityService _uniquelyNamedEntityService;

		// Token: 0x0400007B RID: 123
		public readonly ConcurrentDictionary<string, HttpAdapterSnapshot> _adapters = new ConcurrentDictionary<string, HttpAdapterSnapshot>();

		// Token: 0x0400007C RID: 124
		public readonly ConcurrentDictionary<string, HttpLeverSnapshot> _levers = new ConcurrentDictionary<string, HttpLeverSnapshot>();

		// Token: 0x0400007D RID: 125
		public readonly ConcurrentQueue<HttpLeverCommand> _leverCommands = new ConcurrentQueue<HttpLeverCommand>();
	}
}
