using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000007 RID: 7
	public class Accessible : BaseComponent, IAwakableComponent, INamedComponent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023A3 File Offset: 0x000005A3
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000023AB File Offset: 0x000005AB
		public string ComponentName { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x000023B4 File Offset: 0x000005B4
		public Accessible(INavigationService navigationService)
		{
			this._navigationService = navigationService;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023E4 File Offset: 0x000005E4
		public ReadOnlyList<Vector3> Accesses
		{
			get
			{
				return this._accesses.AsReadOnlyList<Vector3>();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023F1 File Offset: 0x000005F1
		public bool ValidAccessible
		{
			get
			{
				return base.Enabled && this.IsValid();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		public Vector3? UnblockedSingleAccess
		{
			get
			{
				if (!base.Enabled || this.IsBlocked)
				{
					return null;
				}
				return new Vector3?(this._accesses.Single<Vector3>());
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000243C File Offset: 0x0000063C
		public Vector3? UnblockedSingleAccessInstant
		{
			get
			{
				if (!base.Enabled || this.IsBlockedInstant)
				{
					return null;
				}
				return new Vector3?(this._accesses.Single<Vector3>());
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002473 File Offset: 0x00000673
		public bool HasSingleAccess
		{
			get
			{
				return this._accesses.Count == 1;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002483 File Offset: 0x00000683
		public void Awake()
		{
			base.DisableComponent();
			this._pathModifier = base.GetComponent<IPathToAccessibleModifier>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002497 File Offset: 0x00000697
		public void Initialize(string componentName)
		{
			this.ComponentName = componentName;
			this._blockedAccessible = base.GetComponent<IBlockedAccessible>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024AC File Offset: 0x000006AC
		public void SetAccesses(IEnumerable<Vector3> accesses, Vector3? finalAccess = null)
		{
			base.GetComponents<IAccessibleValidator>(this._accessibleValidators);
			this._accesses.Clear();
			this._accesses.AddRange(accesses);
			this._finalAccess = finalAccess;
			base.EnableComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024DE File Offset: 0x000006DE
		public void ClearAccesses()
		{
			this._accesses.Clear();
			base.DisableComponent();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024F4 File Offset: 0x000006F4
		public bool IsReachableByRoad(Accessible end)
		{
			float num;
			return this.FindRoadPath(end, out num);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000250C File Offset: 0x0000070C
		public bool FindRoadPath(Vector3 end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				return this._navigationService.FindRoadPath(valueOrDefault, end, out distance);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002548 File Offset: 0x00000748
		public bool FindRoadPath(Accessible end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				float num = float.PositiveInfinity;
				bool result = false;
				if (!end.IsBlocked)
				{
					List<Vector3> accesses = end._accesses;
					for (int i = 0; i < accesses.Count; i++)
					{
						float val;
						if (this._navigationService.FindRoadPath(valueOrDefault, accesses[i], out val))
						{
							num = Math.Min(num, val);
							result = true;
						}
					}
				}
				distance = num;
				return result;
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025CC File Offset: 0x000007CC
		public bool FindInstantRoadPath(Vector3 end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				return this._navigationService.FindInstantRoadPath(valueOrDefault, end, out distance);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002608 File Offset: 0x00000808
		public bool FindInstantRoadPath(Accessible end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				float num = float.PositiveInfinity;
				bool result = false;
				if (!end.IsBlocked)
				{
					List<Vector3> accesses = end._accesses;
					for (int i = 0; i < accesses.Count; i++)
					{
						float val;
						if (this._navigationService.FindInstantRoadPath(valueOrDefault, accesses[i], out val))
						{
							num = Math.Min(num, val);
							result = true;
						}
					}
				}
				distance = num;
				return result;
			}
			distance = 0f;
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000268C File Offset: 0x0000088C
		public bool IsReachableByTerrain(Vector3 end)
		{
			float num;
			return this.FindTerrainPath(end, out num);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026A4 File Offset: 0x000008A4
		public bool IsReachableByRoadToTerrain(Accessible end)
		{
			Vector3 vector;
			float num;
			return this.FindRoadToTerrainPath(end, out vector, out num);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026BC File Offset: 0x000008BC
		public bool FindTerrainPath(Vector3 end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				return this._navigationService.FindTerrainPath(valueOrDefault, end, out distance);
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026F8 File Offset: 0x000008F8
		public bool FindTerrainPath(Accessible end, out float distance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				float num = float.PositiveInfinity;
				bool result = false;
				if (!end.IsBlocked)
				{
					List<Vector3> accesses = end._accesses;
					for (int i = 0; i < accesses.Count; i++)
					{
						float val;
						if (this._navigationService.FindTerrainPath(valueOrDefault, accesses[i], out val))
						{
							num = Math.Min(num, val);
							result = true;
						}
					}
				}
				distance = num;
				return result;
			}
			distance = 0f;
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000277C File Offset: 0x0000097C
		public bool FindRoadToTerrainPath(Accessible end, out Vector3 endOfRoad, out float totalDistance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				float num = float.PositiveInfinity;
				endOfRoad = default(Vector3);
				totalDistance = 0f;
				bool result = false;
				if (!end.IsBlocked)
				{
					List<Vector3> accesses = end._accesses;
					for (int i = 0; i < accesses.Count; i++)
					{
						Vector3 vector;
						float num2;
						float num3;
						if (this._navigationService.FindRoadToTerrainPath(valueOrDefault, accesses[i], out vector, out num2, out num3) && num > num2)
						{
							num = num2;
							endOfRoad = vector;
							totalDistance = num3;
							result = true;
						}
					}
				}
				return result;
			}
			endOfRoad = default(Vector3);
			totalDistance = 0f;
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002824 File Offset: 0x00000A24
		public bool FindRoadToTerrainPath(Vector3 end, out float totalDistance)
		{
			Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				Vector3 vector;
				float num;
				return this._navigationService.FindRoadToTerrainPath(valueOrDefault, end, out vector, out num, out totalDistance);
			}
			totalDistance = 0f;
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002864 File Offset: 0x00000A64
		public bool FindPathUnlimitedRange(Vector3 start, List<PathCorner> pathCorners, out float distance)
		{
			if (!this.IsBlocked && this._navigationService.FindPathUnlimitedRange(start, this._accesses, pathCorners, out distance))
			{
				Vector3? finalAccess = this._finalAccess;
				if (finalAccess != null)
				{
					Vector3 valueOrDefault = finalAccess.GetValueOrDefault();
					float num;
					if (!this._navigationService.FindPathUnlimitedRange(pathCorners.Last<PathCorner>().Position, valueOrDefault, this._tempPathCorners, out num))
					{
						pathCorners.Clear();
						distance = 0f;
						return false;
					}
					if (this._tempPathCorners.Count >= pathCorners.Count)
					{
						return this._navigationService.FindPathUnlimitedRange(start, valueOrDefault, pathCorners, out distance);
					}
					pathCorners.RemoveLast();
					pathCorners.AddRange(this._tempPathCorners);
				}
				IPathToAccessibleModifier pathModifier = this._pathModifier;
				if (pathModifier != null)
				{
					pathModifier.ModifyPath(start, pathCorners, ref distance);
				}
				return true;
			}
			pathCorners.Clear();
			distance = 0f;
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000293C File Offset: 0x00000B3C
		public Vector3 FindExitPath(Vector3 start, List<PathCorner> pathCorners)
		{
			if (this._finalAccess != null)
			{
				Vector3? unblockedSingleAccess = this.UnblockedSingleAccess;
				if (unblockedSingleAccess != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
					float num;
					if (this._navigationService.FindPathUnlimitedRange(start, valueOrDefault, pathCorners, out num))
					{
						return valueOrDefault;
					}
				}
			}
			return start;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002984 File Offset: 0x00000B84
		public bool IsReachableUnlimitedRange(Vector3 start)
		{
			if (!this.IsBlocked)
			{
				for (int i = 0; i < this._accesses.Count; i++)
				{
					if (this._navigationService.DestinationIsReachableUnlimitedRange(start, this._accesses[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsBlocked
		{
			get
			{
				IBlockedAccessible blockedAccessible = this._blockedAccessible;
				return blockedAccessible != null && blockedAccessible.IsBlocked();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000029DF File Offset: 0x00000BDF
		public bool IsBlockedInstant
		{
			get
			{
				IBlockedAccessible blockedAccessible = this._blockedAccessible;
				return blockedAccessible != null && blockedAccessible.IsBlockedInstant();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029F4 File Offset: 0x00000BF4
		public bool IsValid()
		{
			using (List<IAccessibleValidator>.Enumerator enumerator = this._accessibleValidators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.ValidAccessible)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000010 RID: 16
		public readonly INavigationService _navigationService;

		// Token: 0x04000011 RID: 17
		public IPathToAccessibleModifier _pathModifier;

		// Token: 0x04000012 RID: 18
		public readonly List<IAccessibleValidator> _accessibleValidators = new List<IAccessibleValidator>();

		// Token: 0x04000013 RID: 19
		public IBlockedAccessible _blockedAccessible;

		// Token: 0x04000014 RID: 20
		public readonly List<Vector3> _accesses = new List<Vector3>();

		// Token: 0x04000015 RID: 21
		public readonly List<PathCorner> _tempPathCorners = new List<PathCorner>();

		// Token: 0x04000016 RID: 22
		public Vector3? _finalAccess;
	}
}
