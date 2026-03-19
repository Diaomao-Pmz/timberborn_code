using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000008 RID: 8
	public interface IValueLoader
	{
		// Token: 0x06000043 RID: 67
		int AsInt();

		// Token: 0x06000044 RID: 68
		float AsFloat();

		// Token: 0x06000045 RID: 69
		bool AsBool();

		// Token: 0x06000046 RID: 70
		string AsString();

		// Token: 0x06000047 RID: 71
		char AsChar();

		// Token: 0x06000048 RID: 72
		Quaternion AsQuaternion();

		// Token: 0x06000049 RID: 73
		Vector3 AsVector3();

		// Token: 0x0600004A RID: 74
		Vector3Int AsVector3Int();

		// Token: 0x0600004B RID: 75
		Vector2 AsVector2();

		// Token: 0x0600004C RID: 76
		Vector2Int AsVector2Int();

		// Token: 0x0600004D RID: 77
		Color AsColor();

		// Token: 0x0600004E RID: 78
		Guid AsGuid();

		// Token: 0x0600004F RID: 79
		T As<T>(IValueSerializer<T> serializer);

		// Token: 0x06000050 RID: 80
		bool AsObsoletable<T>(IValueSerializer<T> serializer, out T value);

		// Token: 0x06000051 RID: 81
		List<int> AsIntList();

		// Token: 0x06000052 RID: 82
		List<float> AsFloatList();

		// Token: 0x06000053 RID: 83
		List<bool> AsBoolList();

		// Token: 0x06000054 RID: 84
		List<string> AsStringList();

		// Token: 0x06000055 RID: 85
		List<char> AsCharList();

		// Token: 0x06000056 RID: 86
		List<Quaternion> AsQuaternionList();

		// Token: 0x06000057 RID: 87
		List<Vector3> AsVector3List();

		// Token: 0x06000058 RID: 88
		List<Vector3Int> AsVector3IntList();

		// Token: 0x06000059 RID: 89
		List<Vector2> AsVector2List();

		// Token: 0x0600005A RID: 90
		List<Vector2Int> AsVector2IntList();

		// Token: 0x0600005B RID: 91
		List<Color> AsColorList();

		// Token: 0x0600005C RID: 92
		List<Guid> AsGuidList();

		// Token: 0x0600005D RID: 93
		List<T> AsList<T>(IValueSerializer<T> serializer);

		// Token: 0x0600005E RID: 94
		IObjectLoader AsObject();

		// Token: 0x0600005F RID: 95
		bool IsObject();

		// Token: 0x06000060 RID: 96
		bool IsList();
	}
}
