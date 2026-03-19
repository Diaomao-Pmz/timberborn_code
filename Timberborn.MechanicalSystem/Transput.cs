using System;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000020 RID: 32
	public class Transput
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000435E File Offset: 0x0000255E
		public MechanicalNode ParentNode { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004366 File Offset: 0x00002566
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000436E File Offset: 0x0000256E
		public bool ReversedRotation { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004377 File Offset: 0x00002577
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000437F File Offset: 0x0000257F
		public Transput ConnectedTransput { get; private set; }

		// Token: 0x06000109 RID: 265 RVA: 0x00004388 File Offset: 0x00002588
		public Transput(MechanicalNode parentNode, TransputSpec transputSpec, Direction3D direction, BlockObject blockObject)
		{
			this.ParentNode = parentNode;
			this.ReversedRotation = (this._initialRotation = (blockObject.FlipMode.IsFlipped ? (!transputSpec.ReverseRotation) : transputSpec.ReverseRotation));
			this._coordinates = transputSpec.Coordinates;
			this._direction = direction;
			this._blockObject = blockObject;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000043EE File Offset: 0x000025EE
		public Vector3Int Coordinates
		{
			get
			{
				return this._blockObject.TransformCoordinates(this._coordinates);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004401 File Offset: 0x00002601
		public Direction3D Direction
		{
			get
			{
				return this._blockObject.TransformDirection(this._direction);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004414 File Offset: 0x00002614
		public Direction3D BaseDirection
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000441C File Offset: 0x0000261C
		public Vector3Int Target
		{
			get
			{
				return this.Coordinates + this.Direction.ToOffset();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004434 File Offset: 0x00002634
		public Vector3Int Offset
		{
			get
			{
				return this._coordinates - new Vector3Int(0, 0, this._blockObject.BaseZ);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004453 File Offset: 0x00002653
		public MechanicalNode ConnectedNode
		{
			get
			{
				if (!this.Connected)
				{
					return null;
				}
				return this.ConnectedTransput.ParentNode;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000446A File Offset: 0x0000266A
		public bool IsFinished
		{
			get
			{
				return this._blockObject.IsFinished;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004477 File Offset: 0x00002677
		public bool Connected
		{
			get
			{
				return this.ConnectedTransput != null;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004482 File Offset: 0x00002682
		public void Connect(Transput other)
		{
			this.ConnectedTransput = other;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000448B File Offset: 0x0000268B
		public void Disconnect()
		{
			this.ConnectedTransput = null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004494 File Offset: 0x00002694
		public void ReverseRotation()
		{
			this.ReversedRotation = !this.ReversedRotation;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000044A5 File Offset: 0x000026A5
		public void ResetRotation()
		{
			this.ReversedRotation = this._initialRotation;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000044B3 File Offset: 0x000026B3
		public bool RotationMatches(Transput other)
		{
			return this.ReversedRotation == other.ReversedRotation;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000044C3 File Offset: 0x000026C3
		public bool Faces(Transput other)
		{
			return other.Target == this.Coordinates && this.Target == other.Coordinates && other.Direction == this.Direction.Across();
		}

		// Token: 0x04000064 RID: 100
		public readonly BlockObject _blockObject;

		// Token: 0x04000065 RID: 101
		public readonly Vector3Int _coordinates;

		// Token: 0x04000066 RID: 102
		public readonly Direction3D _direction;

		// Token: 0x04000067 RID: 103
		public readonly bool _initialRotation;
	}
}
