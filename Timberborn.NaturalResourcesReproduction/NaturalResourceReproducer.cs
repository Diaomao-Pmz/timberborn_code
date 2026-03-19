using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Demolishing;
using Timberborn.NaturalResources;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.NaturalResourcesReproduction
{
	// Token: 0x0200000B RID: 11
	public class NaturalResourceReproducer : ITickableSingleton, ILoadableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021F8 File Offset: 0x000003F8
		public NaturalResourceReproducer(IBlockService blockService, IDayNightCycle dayNightCycle, IRandomNumberGenerator randomNumberGenerator, EventBus eventBus, NaturalResourceFactory naturalResourceFactory)
		{
			this._blockService = blockService;
			this._dayNightCycle = dayNightCycle;
			this._randomNumberGenerator = randomNumberGenerator;
			this._eventBus = eventBus;
			this._naturalResourceFactory = naturalResourceFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002246 File Offset: 0x00000446
		public IEnumerable<Vector3Int> PotentialSpots
		{
			get
			{
				return this._potentialSpots.SelectMany((KeyValuePair<NaturalResourceReproducer.ReproducibleKey, HashSet<Vector3Int>> pair) => pair.Value);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002272 File Offset: 0x00000472
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002280 File Offset: 0x00000480
		public void Tick()
		{
			this.TryReproduceResources();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002288 File Offset: 0x00000488
		public void MarkSpots(Reproducible reproducible)
		{
			Vector3Int coordinates = reproducible.GetComponent<BlockObject>().Coordinates;
			NaturalResourceReproducer.ReproducibleKey key = NaturalResourceReproducer.ReproducibleKey.Create(reproducible);
			HashSet<Vector3Int> orAdd = this._potentialSpots.GetOrAdd(key, () => new HashSet<Vector3Int>());
			foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
			{
				orAdd.Add(coordinates + vector3Int);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002308 File Offset: 0x00000508
		public void UnmarkSpots(Reproducible reproducible)
		{
			NaturalResourceReproducer.ReproducibleKey key = NaturalResourceReproducer.ReproducibleKey.Create(reproducible);
			HashSet<Vector3Int> hashSet;
			if (this._potentialSpots.TryGetValue(key, out hashSet))
			{
				Vector3Int coordinates = reproducible.GetComponent<BlockObject>().Coordinates;
				foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
				{
					Vector3Int vector3Int2 = coordinates + vector3Int;
					if (hashSet.Contains(vector3Int2) && !this.CanReproduceAtCoordinates(reproducible.Id, vector3Int2))
					{
						hashSet.Remove(vector3Int2);
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002388 File Offset: 0x00000588
		public void TryReproduceResources()
		{
			float num = this._dayNightCycle.FixedDeltaTimeInHours / 24f;
			foreach (KeyValuePair<NaturalResourceReproducer.ReproducibleKey, HashSet<Vector3Int>> keyValuePair in this._potentialSpots)
			{
				float num2 = num * keyValuePair.Key.ReproductionChance;
				float num3 = this._randomNumberGenerator.Range(0f, 1f);
				HashSet<Vector3Int> value = keyValuePair.Value;
				if (num3 < num2 * (float)value.Count)
				{
					int index = this._randomNumberGenerator.Range(0, value.Count);
					this._newResources.Add(new ValueTuple<NaturalResourceReproducer.ReproducibleKey, Vector3Int>(keyValuePair.Key, keyValuePair.Value.ElementAt(index)));
				}
			}
			this.SpawnNewResources();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002468 File Offset: 0x00000668
		public void SpawnNewResources()
		{
			foreach (ValueTuple<NaturalResourceReproducer.ReproducibleKey, Vector3Int> valueTuple in this._newResources)
			{
				NaturalResourceReproducer.ReproducibleKey item = valueTuple.Item1;
				Vector3Int item2 = valueTuple.Item2;
				bool spawnMarkedForDemolish = this.AnyNeighborMarkedForDemolish(item, item2);
				this._naturalResourceFactory.SpawnNew(item.Id, item2, spawnMarkedForDemolish);
			}
			this._newResources.Clear();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024E8 File Offset: 0x000006E8
		public bool CanReproduceAtCoordinates(string id, Vector3Int coordinates)
		{
			foreach (Vector2Int value in Deltas.Neighbors4Vector2Int)
			{
				Vector3Int coordinates2 = coordinates + value.XYZ();
				Reproducible bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Reproducible>(coordinates2);
				if (bottomObjectComponentAt != null && bottomObjectComponentAt.Id == id && !bottomObjectComponentAt.ReproductionDisabled && bottomObjectComponentAt.GetComponent<BlockObject>().Coordinates.z == coordinates.z)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000256C File Offset: 0x0000076C
		public bool AnyNeighborMarkedForDemolish(NaturalResourceReproducer.ReproducibleKey reproducibleKey, Vector3Int coordinates)
		{
			foreach (Vector2Int value in Deltas.Neighbors4Vector2Int)
			{
				Vector3Int coordinates2 = coordinates + value.XYZ();
				NaturalResource bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<NaturalResource>(coordinates2);
				if (bottomObjectComponentAt && bottomObjectComponentAt.GetComponent<TemplateSpec>().TemplateName == reproducibleKey.Id)
				{
					Demolishable component = bottomObjectComponentAt.GetComponent<Demolishable>();
					if (component != null && component.IsMarked)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400000C RID: 12
		public readonly IBlockService _blockService;

		// Token: 0x0400000D RID: 13
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000E RID: 14
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public readonly NaturalResourceFactory _naturalResourceFactory;

		// Token: 0x04000011 RID: 17
		public readonly Dictionary<NaturalResourceReproducer.ReproducibleKey, HashSet<Vector3Int>> _potentialSpots = new Dictionary<NaturalResourceReproducer.ReproducibleKey, HashSet<Vector3Int>>();

		// Token: 0x04000012 RID: 18
		public readonly List<ValueTuple<NaturalResourceReproducer.ReproducibleKey, Vector3Int>> _newResources = new List<ValueTuple<NaturalResourceReproducer.ReproducibleKey, Vector3Int>>();

		// Token: 0x0200000C RID: 12
		public readonly struct ReproducibleKey : IEquatable<NaturalResourceReproducer.ReproducibleKey>
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000025EC File Offset: 0x000007EC
			public string Id { get; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000025F4 File Offset: 0x000007F4
			public float ReproductionChance { get; }

			// Token: 0x0600001D RID: 29 RVA: 0x000025FC File Offset: 0x000007FC
			public ReproducibleKey(string id, float reproductionChance)
			{
				this.Id = id;
				this.ReproductionChance = reproductionChance;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x0000260C File Offset: 0x0000080C
			public static NaturalResourceReproducer.ReproducibleKey Create(Reproducible reproducible)
			{
				return new NaturalResourceReproducer.ReproducibleKey(reproducible.Id, reproducible.ReproductionChance);
			}

			// Token: 0x0600001F RID: 31 RVA: 0x0000261F File Offset: 0x0000081F
			public bool Equals(NaturalResourceReproducer.ReproducibleKey other)
			{
				return this.Id == other.Id;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002634 File Offset: 0x00000834
			public override bool Equals(object obj)
			{
				if (obj is NaturalResourceReproducer.ReproducibleKey)
				{
					NaturalResourceReproducer.ReproducibleKey other = (NaturalResourceReproducer.ReproducibleKey)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002659 File Offset: 0x00000859
			public override int GetHashCode()
			{
				if (this.Id == null)
				{
					return 0;
				}
				return this.Id.GetHashCode();
			}
		}
	}
}
