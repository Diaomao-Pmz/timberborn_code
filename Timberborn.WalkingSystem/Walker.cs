using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000018 RID: 24
	public class Walker : TickableComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000087 RID: 135 RVA: 0x000034E0 File Offset: 0x000016E0
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x00003518 File Offset: 0x00001718
		public event EventHandler<StartedNewPathEventArgs> StartedNewPath;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000354D File Offset: 0x0000174D
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003555 File Offset: 0x00001755
		public bool CurrentDestinationReachable { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000355E File Offset: 0x0000175E
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003566 File Offset: 0x00001766
		public BoundingBox CurrentPathBounds { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000356F File Offset: 0x0000176F
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003577 File Offset: 0x00001777
		public PathFollower PathFollower { get; private set; }

		// Token: 0x0600008F RID: 143 RVA: 0x00003580 File Offset: 0x00001780
		public Walker(PathFollowerFactory pathFollowerFactory, DestinationValueSerializer destinationValueSerializer, INavigationService navigationService, IDayNightCycle dayNightCycle)
		{
			this._pathFollowerFactory = pathFollowerFactory;
			this._destinationValueSerializer = destinationValueSerializer;
			this._navigationService = navigationService;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000035D7 File Offset: 0x000017D7
		public ReadOnlyList<PathCorner> PathCorners
		{
			get
			{
				return this._pathCorners.AsReadOnlyList<PathCorner>();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000035E4 File Offset: 0x000017E4
		public void Awake()
		{
			this._enterer = base.GetComponent<Enterer>();
			this._citizen = base.GetComponent<Citizen>();
			this._walkerSpeedManager = base.GetComponent<WalkerSpeedManager>();
			this._walkerPathStart = base.GetComponent<WalkerPathStart>();
			this.PathFollower = this._pathFollowerFactory.Create(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003633 File Offset: 0x00001833
		public override void Tick()
		{
			if (!this.Stopped() && (this.IsOutsideAndReachedDestination() || this._stopNextTick))
			{
				this.StopMoving();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003653 File Offset: 0x00001853
		public ExecutorStatus GoTo(IDestination destination)
		{
			return this.FindPath(destination);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000365C File Offset: 0x0000185C
		public void StopNextTick()
		{
			this._stopNextTick = true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003665 File Offset: 0x00001865
		public bool Stopped()
		{
			return this._currentDestination == null;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003670 File Offset: 0x00001870
		public void RefreshPath()
		{
			if (this._currentDestination != null)
			{
				this.FindPath(this._currentDestination);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003687 File Offset: 0x00001887
		public void Save(IEntitySaver entitySaver)
		{
			if (this._currentDestination != null)
			{
				entitySaver.GetComponent(Walker.WalkerKey).Set<IDestination>(Walker.CurrentDestinationKey, this._currentDestination, this._destinationValueSerializer);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000036B4 File Offset: 0x000018B4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Walker.WalkerKey, out objectLoader))
			{
				this._currentDestination = objectLoader.Get<IDestination>(Walker.CurrentDestinationKey, this._destinationValueSerializer);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000036E8 File Offset: 0x000018E8
		public float CalculateTravelTimeInHours(Vector3 start, Vector3 destination)
		{
			float num;
			if (!this._navigationService.FindPathUnlimitedRange(start, destination, null, out num))
			{
				num = this._navigationService.HeuristicDistance(start, destination);
			}
			float seconds = num / this._walkerSpeedManager.GetWalkerBaseSpeed();
			return this._dayNightCycle.SecondsToHours(seconds);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000372F File Offset: 0x0000192F
		public void StopMoving()
		{
			this._currentDestination = null;
			this._stopNextTick = false;
			this.PathFollower.StopMoving();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000374C File Offset: 0x0000194C
		public ExecutorStatus FindPath(IDestination destination)
		{
			if (destination == null)
			{
				throw new NullReferenceException("Walker destination is null.");
			}
			this._stopNextTick = false;
			this._pathCorners.Clear();
			this._tempPathCorners.Clear();
			Vector3 start;
			this._walkerPathStart.GetPathStart(destination, this._pathCorners, out start);
			float num;
			this.CurrentDestinationReachable = (destination.FindPath(start, this._tempPathCorners, out num) && !this.PathIsTooFarFromDistrict(start, num));
			if (!this.CurrentDestinationReachable)
			{
				this._citizen.UnassignDistrictIfCutOff();
				this.StopMoving();
				return ExecutorStatus.Failure;
			}
			this._pathCorners.AddRange(this._tempPathCorners);
			this.PathFollower.StartMovingAlongPath(this._pathCorners);
			EventHandler<StartedNewPathEventArgs> startedNewPath = this.StartedNewPath;
			if (startedNewPath != null)
			{
				startedNewPath(this, new StartedNewPathEventArgs(num));
			}
			this._currentDestination = destination;
			this.RecalculatePathBounds();
			if (this.IsOutsideAndReachedDestination())
			{
				this.StopMoving();
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003830 File Offset: 0x00001A30
		public void RecalculatePathBounds()
		{
			BoundingBox.Builder builder = default(BoundingBox.Builder);
			for (int i = 0; i < this._pathCorners.Count; i++)
			{
				builder.Expand(NavigationCoordinateSystem.WorldToGridInt(this._pathCorners[i].Position));
			}
			this.CurrentPathBounds = builder.Build();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003888 File Offset: 0x00001A88
		public bool IsOutsideAndReachedDestination()
		{
			return !this._enterer.IsInside && this.PathFollower.ReachedLastPathCorner();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000038A4 File Offset: 0x00001AA4
		public bool PathIsTooFarFromDistrict(Vector3 start, float pathLength)
		{
			if (pathLength > (float)WalkerLimits.BlockingEdgeCost)
			{
				DistrictCenter assignedDistrict = this._citizen.AssignedDistrict;
				if (assignedDistrict != null)
				{
					this._districtPathCorners.Clear();
					float num;
					return assignedDistrict.GetEnabledComponent<Accessible>().FindPathUnlimitedRange(start, this._districtPathCorners, out num) && num < (float)WalkerLimits.BlockingEdgeCost;
				}
			}
			return false;
		}

		// Token: 0x0400003F RID: 63
		public static readonly ComponentKey WalkerKey = new ComponentKey("Walker");

		// Token: 0x04000040 RID: 64
		public static readonly PropertyKey<IDestination> CurrentDestinationKey = new PropertyKey<IDestination>("CurrentDestination");

		// Token: 0x04000045 RID: 69
		public readonly PathFollowerFactory _pathFollowerFactory;

		// Token: 0x04000046 RID: 70
		public readonly DestinationValueSerializer _destinationValueSerializer;

		// Token: 0x04000047 RID: 71
		public readonly INavigationService _navigationService;

		// Token: 0x04000048 RID: 72
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000049 RID: 73
		public Enterer _enterer;

		// Token: 0x0400004A RID: 74
		public Citizen _citizen;

		// Token: 0x0400004B RID: 75
		public WalkerSpeedManager _walkerSpeedManager;

		// Token: 0x0400004C RID: 76
		public WalkerPathStart _walkerPathStart;

		// Token: 0x0400004D RID: 77
		public readonly List<PathCorner> _pathCorners = new List<PathCorner>(100);

		// Token: 0x0400004E RID: 78
		public readonly List<PathCorner> _tempPathCorners = new List<PathCorner>(100);

		// Token: 0x0400004F RID: 79
		public readonly List<PathCorner> _districtPathCorners = new List<PathCorner>(100);

		// Token: 0x04000050 RID: 80
		public IDestination _currentDestination;

		// Token: 0x04000051 RID: 81
		public bool _stopNextTick;
	}
}
