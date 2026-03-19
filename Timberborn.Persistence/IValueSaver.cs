using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Persistence
{
	// Token: 0x02000009 RID: 9
	public interface IValueSaver
	{
		// Token: 0x06000061 RID: 97
		void AsInt(int value);

		// Token: 0x06000062 RID: 98
		void AsFloat(float value);

		// Token: 0x06000063 RID: 99
		void AsBool(bool value);

		// Token: 0x06000064 RID: 100
		void AsString(string value);

		// Token: 0x06000065 RID: 101
		void AsChar(char value);

		// Token: 0x06000066 RID: 102
		void AsQuaternion(Quaternion value);

		// Token: 0x06000067 RID: 103
		void AsVector3(Vector3 value);

		// Token: 0x06000068 RID: 104
		void AsVector3Int(Vector3Int value);

		// Token: 0x06000069 RID: 105
		void AsVector2(Vector2 value);

		// Token: 0x0600006A RID: 106
		void AsVector2Int(Vector2Int value);

		// Token: 0x0600006B RID: 107
		void AsColor(Color value);

		// Token: 0x0600006C RID: 108
		void AsGuid(Guid value);

		// Token: 0x0600006D RID: 109
		void As<T>(T value, IValueSerializer<T> serializer);

		// Token: 0x0600006E RID: 110
		void AsIntList(IReadOnlyCollection<int> values);

		// Token: 0x0600006F RID: 111
		void AsFloatList(IReadOnlyCollection<float> values);

		// Token: 0x06000070 RID: 112
		void AsBoolList(IReadOnlyCollection<bool> values);

		// Token: 0x06000071 RID: 113
		void AsStringList(IReadOnlyCollection<string> values);

		// Token: 0x06000072 RID: 114
		void AsCharList(IReadOnlyCollection<char> values);

		// Token: 0x06000073 RID: 115
		void AsQuaternionList(IReadOnlyCollection<Quaternion> values);

		// Token: 0x06000074 RID: 116
		void AsVector3List(IReadOnlyCollection<Vector3> values);

		// Token: 0x06000075 RID: 117
		void AsVector3IntList(IReadOnlyCollection<Vector3Int> values);

		// Token: 0x06000076 RID: 118
		void AsVector2List(IReadOnlyCollection<Vector2> values);

		// Token: 0x06000077 RID: 119
		void AsVector2IntList(IReadOnlyCollection<Vector2Int> values);

		// Token: 0x06000078 RID: 120
		void AsColorList(IReadOnlyCollection<Color> values);

		// Token: 0x06000079 RID: 121
		void AsGuidList(IReadOnlyCollection<Guid> values);

		// Token: 0x0600007A RID: 122
		void AsList<T>(IReadOnlyCollection<T> values, IValueSerializer<T> serializer);

		// Token: 0x0600007B RID: 123
		IObjectSaver AsObject();
	}
}
