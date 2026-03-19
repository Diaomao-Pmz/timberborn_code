using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000012 RID: 18
	public class DistrictConnectionLineRenderer : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public DistrictConnectionLineRenderer(DistrictConnectionLineRotator districtConnectionLineRotator, ISpecService specService, RootObjectProvider rootObjectProvider)
		{
			this._districtConnectionLineRotator = districtConnectionLineRotator;
			this._specService = specService;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void Load()
		{
			DistrictConnectionLineRendererSpec singleSpec = this._specService.GetSingleSpec<DistrictConnectionLineRendererSpec>();
			GameObject gameObject = this._rootObjectProvider.CreateRootObject("DistrictConnectionLineRenderer");
			this._lineRenderer = Object.Instantiate<LineRenderer>(singleSpec.LineRendererPrefab.Asset, gameObject.transform);
			this._lineRenderer.enabled = false;
			this._arcAngleRad = (double)singleSpec.ArcAngle * 0.017453292519943295;
			this._curvePoints = singleSpec.CurvePoints;
			this._lineCutoff = singleSpec.LineCutoff;
			this._enabled = false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C80 File Offset: 0x00000E80
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				Shader.SetGlobalFloat(DistrictConnectionLineRenderer.UnscaledTimeProperty, Time.unscaledTime);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C99 File Offset: 0x00000E99
		public void Clear()
		{
			this._districtConnectionLineRotator.StopRotating();
			this._lineRenderer.enabled = false;
			this._enabled = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CBC File Offset: 0x00000EBC
		public void BuildMesh(Vector3 start, Vector3 end)
		{
			if (DistrictConnectionLineRenderer.ArePointsAboveEachOther(start, end))
			{
				this.BuildStraightLine(start, end);
				this._districtConnectionLineRotator.StartRotatingSimple(start, end, this._lineRenderer.transform);
				return;
			}
			this.BuildCurvedLine(start, end);
			this._districtConnectionLineRotator.StartRotating(start, end, this._lineRenderer.transform);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D13 File Offset: 0x00000F13
		public static bool ArePointsAboveEachOther(Vector3 start, Vector3 end)
		{
			return Mathf.Abs(start.x - end.x) <= 0.01f && Mathf.Abs(start.z - end.z) <= 0.01f;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D4C File Offset: 0x00000F4C
		public void BuildStraightLine(Vector3 start, Vector3 end)
		{
			Vector3[] renderer = new Vector3[]
			{
				start,
				end
			};
			this.SetRenderer(renderer);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D78 File Offset: 0x00000F78
		public void BuildCurvedLine(Vector3 start, Vector3 end)
		{
			Vector3 arcCenterPoint = this.GetArcCenterPoint(start, end);
			Vector3 vector = start - arcCenterPoint;
			Vector3 vector2 = end - arcCenterPoint;
			float num = (float)(Math.Tan((double)(this._lineCutoff / vector.magnitude)) / this._arcAngleRad);
			Vector3 vector3 = Vector3.Slerp(vector, vector2, num);
			Vector3 vector4 = Vector3.Slerp(vector, vector2, 1f - num);
			List<Vector3> list = new List<Vector3>();
			for (float num2 = 0f; num2 <= (float)this._curvePoints; num2 += 1f)
			{
				Vector3 item = arcCenterPoint + Vector3.Slerp(vector3, vector4, num2 / (float)this._curvePoints);
				list.Add(item);
			}
			this.SetRenderer(list.ToArray());
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E2B File Offset: 0x0000102B
		public void SetRenderer(Vector3[] points)
		{
			this._lineRenderer.positionCount = points.Length;
			this._lineRenderer.SetPositions(points);
			this._lineRenderer.enabled = true;
			this._enabled = true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E5C File Offset: 0x0000105C
		public Vector3 GetArcCenterPoint(Vector3 start, Vector3 end)
		{
			if (start.y > end.y)
			{
				Vector3 vector = end;
				Vector3 vector2 = start;
				start = vector;
				end = vector2;
			}
			Vector3 direction = end - start;
			if (DistrictConnectionLineRenderer.GetAngleToXZPlane(direction) <= 1.5707963267948966 - this._arcAngleRad)
			{
				return this.GetArcCenterFromTwoPoints(start, end);
			}
			return DistrictConnectionLineRenderer.GetArcCenterFromTangentCircle(start, direction);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static double GetAngleToXZPlane(Vector3 direction)
		{
			float magnitude = direction.magnitude;
			return Math.Asin((double)(direction.y / magnitude));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002ED4 File Offset: 0x000010D4
		public static Vector3 GetArcCenterFromTangentCircle(Vector3 start, Vector3 direction)
		{
			double num = Math.Pow((double)direction.x, 2.0);
			double num2 = Math.Pow((double)direction.y, 2.0);
			double num3 = Math.Pow((double)direction.z, 2.0);
			double num4 = (num + num2 + num3) / (2.0 * Math.Sqrt(num + num3));
			Vector3 vector;
			vector..ctor(direction.x, 0f, direction.z);
			Vector3 vector2 = vector.normalized * (float)num4;
			return start + vector2;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F6C File Offset: 0x0000116C
		public Vector3 GetArcCenterFromTwoPoints(Vector3 start, Vector3 end)
		{
			Vector3 vector = end - start;
			Vector3 vector2 = Vector3.Cross(vector, Vector3.down);
			Vector3 vector3 = Quaternion.AngleAxis(90f, vector2) * vector;
			float num = vector.magnitude * 0.5f / (float)Math.Tan(this._arcAngleRad);
			return (start + end) * 0.5f + vector3.normalized * num;
		}

		// Token: 0x04000031 RID: 49
		public static readonly int UnscaledTimeProperty = Shader.PropertyToID("_UnscaledTime");

		// Token: 0x04000032 RID: 50
		public readonly DistrictConnectionLineRotator _districtConnectionLineRotator;

		// Token: 0x04000033 RID: 51
		public readonly ISpecService _specService;

		// Token: 0x04000034 RID: 52
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000035 RID: 53
		public LineRenderer _lineRenderer;

		// Token: 0x04000036 RID: 54
		public double _arcAngleRad;

		// Token: 0x04000037 RID: 55
		public int _curvePoints;

		// Token: 0x04000038 RID: 56
		public float _lineCutoff;

		// Token: 0x04000039 RID: 57
		public bool _enabled;
	}
}
