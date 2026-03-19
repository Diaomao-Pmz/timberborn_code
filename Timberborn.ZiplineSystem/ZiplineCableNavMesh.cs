using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200000E RID: 14
	public class ZiplineCableNavMesh : ILoadableSingleton
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002911 File Offset: 0x00000B11
		public ZiplineCableNavMesh(ISpecService specService, INavMeshService navMeshService, ZiplineGroupService ziplineGroupService)
		{
			this._specService = specService;
			this._navMeshService = navMeshService;
			this._ziplineGroupService = ziplineGroupService;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002944 File Offset: 0x00000B44
		public void Load()
		{
			this._cableUnitCost = this._specService.GetSingleSpec<ZiplineCableNavMeshSpec>().CableUnitCost;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000295C File Offset: 0x00000B5C
		public void AddInactiveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this.AddPreviewConnectionToNavMesh(ziplineTower, otherZiplineTower);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002966 File Offset: 0x00000B66
		public void AddActiveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this.AddPreviewConnectionToNavMesh(ziplineTower, otherZiplineTower);
			this.AddRegularConnectionToNavMesh(ziplineTower, otherZiplineTower);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002978 File Offset: 0x00000B78
		public void ActivateConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this.AddRegularConnectionToNavMesh(ziplineTower, otherZiplineTower);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002982 File Offset: 0x00000B82
		public void RemoveConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this.RemoveConnectionFromNavMesh(ziplineTower, otherZiplineTower);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000298C File Offset: 0x00000B8C
		public void AddPreviewConnectionToNavMesh(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableNavMesh.EdgePair edgePair;
			if (!this.TryGetPreviewEdges(ziplineTower, otherZiplineTower, out edgePair))
			{
				ZiplineCableNavMesh.EdgePair edgePair2 = this.CreateEdges(ziplineTower, otherZiplineTower);
				this._navMeshService.AddPreviewEdge(edgePair2.EdgeFrom);
				this._navMeshService.AddPreviewEdge(edgePair2.EdgeTo);
				this._previewNavMeshEdges.Add(CableKey.Create(ziplineTower, otherZiplineTower), edgePair2);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029E4 File Offset: 0x00000BE4
		public void AddRegularConnectionToNavMesh(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableNavMesh.EdgePair edgePair;
			if (!this.TryGetRegularEdges(ziplineTower, otherZiplineTower, out edgePair))
			{
				ZiplineCableNavMesh.EdgePair edgePair2 = this.CreateEdges(ziplineTower, otherZiplineTower);
				this._navMeshService.AddEdge(edgePair2.EdgeFrom);
				this._navMeshService.AddEdge(edgePair2.EdgeTo);
				this._regularNavMeshEdges.Add(CableKey.Create(ziplineTower, otherZiplineTower), edgePair2);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A3C File Offset: 0x00000C3C
		public void RemoveConnectionFromNavMesh(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ZiplineCableNavMesh.EdgePair edgePair;
			if (this.TryGetPreviewEdges(ziplineTower, otherZiplineTower, out edgePair))
			{
				this._navMeshService.RemovePreviewEdge(edgePair.EdgeFrom);
				this._navMeshService.RemovePreviewEdge(edgePair.EdgeTo);
				this._previewNavMeshEdges.Remove(CableKey.Create(ziplineTower, otherZiplineTower));
			}
			ZiplineCableNavMesh.EdgePair edgePair2;
			if (this.TryGetRegularEdges(ziplineTower, otherZiplineTower, out edgePair2))
			{
				this._navMeshService.RemoveEdge(edgePair2.EdgeFrom);
				this._navMeshService.RemoveEdge(edgePair2.EdgeTo);
				this._regularNavMeshEdges.Remove(CableKey.Create(ziplineTower, otherZiplineTower));
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002ACC File Offset: 0x00000CCC
		public ZiplineCableNavMesh.EdgePair CreateEdges(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			Vector3Int vector3Int = ziplineTower.CableAnchorPoint.FloorToInt();
			Vector3Int vector3Int2 = otherZiplineTower.CableAnchorPoint.FloorToInt();
			float cost = this._cableUnitCost * (vector3Int - vector3Int2).magnitude;
			int regularGroupId = this._ziplineGroupService.RegularGroupId;
			NavMeshEdge edgeFrom = NavMeshEdge.CreateGrouped(vector3Int, vector3Int2, regularGroupId, true, cost);
			NavMeshEdge edgeTo = NavMeshEdge.CreateGrouped(vector3Int2, vector3Int, regularGroupId, true, cost);
			return new ZiplineCableNavMesh.EdgePair(edgeFrom, edgeTo);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B32 File Offset: 0x00000D32
		public bool TryGetRegularEdges(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, out ZiplineCableNavMesh.EdgePair edges)
		{
			return this._regularNavMeshEdges.TryGetValue(CableKey.Create(ziplineTower, otherZiplineTower), out edges);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B47 File Offset: 0x00000D47
		public bool TryGetPreviewEdges(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, out ZiplineCableNavMesh.EdgePair edges)
		{
			return this._previewNavMeshEdges.TryGetValue(CableKey.Create(ziplineTower, otherZiplineTower), out edges);
		}

		// Token: 0x04000013 RID: 19
		public readonly ISpecService _specService;

		// Token: 0x04000014 RID: 20
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000015 RID: 21
		public readonly ZiplineGroupService _ziplineGroupService;

		// Token: 0x04000016 RID: 22
		public readonly Dictionary<CableKey, ZiplineCableNavMesh.EdgePair> _regularNavMeshEdges = new Dictionary<CableKey, ZiplineCableNavMesh.EdgePair>();

		// Token: 0x04000017 RID: 23
		public readonly Dictionary<CableKey, ZiplineCableNavMesh.EdgePair> _previewNavMeshEdges = new Dictionary<CableKey, ZiplineCableNavMesh.EdgePair>();

		// Token: 0x04000018 RID: 24
		public float _cableUnitCost;

		// Token: 0x0200000F RID: 15
		[NullableContext(1)]
		[Nullable(0)]
		public class EdgePair : IEquatable<ZiplineCableNavMesh.EdgePair>
		{
			// Token: 0x0600004C RID: 76 RVA: 0x00002B5C File Offset: 0x00000D5C
			public EdgePair(NavMeshEdge EdgeFrom, NavMeshEdge EdgeTo)
			{
				this.EdgeFrom = EdgeFrom;
				this.EdgeTo = EdgeTo;
				base..ctor();
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B72 File Offset: 0x00000D72
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(ZiplineCableNavMesh.EdgePair);
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B7E File Offset: 0x00000D7E
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00002B86 File Offset: 0x00000D86
			public NavMeshEdge EdgeFrom { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B8F File Offset: 0x00000D8F
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B97 File Offset: 0x00000D97
			public NavMeshEdge EdgeTo { get; set; }

			// Token: 0x06000052 RID: 82 RVA: 0x00002BA0 File Offset: 0x00000DA0
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("EdgePair");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00002BEC File Offset: 0x00000DEC
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("EdgeFrom = ");
				builder.Append(this.EdgeFrom.ToString());
				builder.Append(", EdgeTo = ");
				builder.Append(this.EdgeTo.ToString());
				return true;
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00002C4D File Offset: 0x00000E4D
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(ZiplineCableNavMesh.EdgePair left, ZiplineCableNavMesh.EdgePair right)
			{
				return !(left == right);
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00002C59 File Offset: 0x00000E59
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(ZiplineCableNavMesh.EdgePair left, ZiplineCableNavMesh.EdgePair right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00002C6D File Offset: 0x00000E6D
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<NavMeshEdge>.Default.GetHashCode(this.<EdgeFrom>k__BackingField)) * -1521134295 + EqualityComparer<NavMeshEdge>.Default.GetHashCode(this.<EdgeTo>k__BackingField);
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002CAD File Offset: 0x00000EAD
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as ZiplineCableNavMesh.EdgePair);
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002CBC File Offset: 0x00000EBC
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(ZiplineCableNavMesh.EdgePair other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<NavMeshEdge>.Default.Equals(this.<EdgeFrom>k__BackingField, other.<EdgeFrom>k__BackingField) && EqualityComparer<NavMeshEdge>.Default.Equals(this.<EdgeTo>k__BackingField, other.<EdgeTo>k__BackingField));
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002D1D File Offset: 0x00000F1D
			[CompilerGenerated]
			protected EdgePair(ZiplineCableNavMesh.EdgePair original)
			{
				this.EdgeFrom = original.<EdgeFrom>k__BackingField;
				this.EdgeTo = original.<EdgeTo>k__BackingField;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002D3D File Offset: 0x00000F3D
			[CompilerGenerated]
			public void Deconstruct(out NavMeshEdge EdgeFrom, out NavMeshEdge EdgeTo)
			{
				EdgeFrom = this.EdgeFrom;
				EdgeTo = this.EdgeTo;
			}
		}
	}
}
