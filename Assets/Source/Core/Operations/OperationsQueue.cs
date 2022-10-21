using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Operations
{
    public class OperationsQueue<TOperation, TData> where TOperation : class, IOperation<TData> where TData : class, IOperationData
    {
        public delegate TData InstantiateData();
        public delegate void HandleData(TData data);
        public delegate void ResetData(TData data);

        public bool UseGarbage { get; set; } = true;

        Dictionary<TOperation, List<TData>> operations = new Dictionary<TOperation, List<TData>>();
        Dictionary<int, Stack<TData>> garbage = new Dictionary<int, Stack<TData>>();

        TOperation currentOperation;
        List<TData> currentList;
        int currentDataIndex;

        public void RefreshOperations()
        {
            var operationsToDelete = operations.Where(o => o.Key.MarkToDelete).ToArray();

            foreach (var operation in operationsToDelete)
            {
                if (operation.Key.MarkToDelete)
                {
                    foreach (var data in operation.Value) data.Release();
                    operations.Remove(operation.Key);
                }
            }

            foreach (var operation in operations)
            {
                operation.Key.MarkToDelete = true;
            }
        }

        public void BeginOperation(TOperation operation)
        {
            if (currentOperation != null) throw new InvalidOperationException("Field currentOperation must be null. Looks like last operation had invalid ending.");

            if (!operations.ContainsKey(operation))
            {
                operations.Add(operation, new List<TData>());
            }

            operation.MarkToDelete = false;

            currentOperation = operation;
            currentList = operations[operation];
            currentDataIndex = 0;
        }

        void AddToGarbage(int typeCode, TData data)
        {
            if (UseGarbage)
            {
                if (garbage.ContainsKey(typeCode))
                {
                    garbage[typeCode].Push(data);
                }
                else
                {
                    var stack = new Stack<TData>();
                    stack.Push(data);
                    garbage.Add(typeCode, stack);
                }
            }
            else
            {
                data.Release();
            }
        }
        bool GetFromGarbage(int typeCode, out TData data)
        {
            Stack<TData> stack;
            if (UseGarbage && garbage.ContainsKey(typeCode) && (stack = garbage[typeCode]).Count != 0)
            {
                data = stack.Pop();
                return true;
            }
            else
            {
                data = null;
                return false;
            }
        }
        TData MakeInstance(int typeCode, HandleData handleData, InstantiateData instantiateData)
        {
            TData data;
            if (GetFromGarbage(typeCode, out data))
            {
                handleData(data);
                return data;
            }
            else
            {
                return instantiateData();
            }
        }

        public void HandleOperationData(int dataTypeCode, HandleData handleData, InstantiateData instantiateData)
        {
            if (currentList == null) throw new System.InvalidCastException("Call begin method first!");

            if (currentDataIndex < currentList.Count)
            {
                TData currentData = currentList[currentDataIndex];
                if (currentData.GetDataTypeCode() == dataTypeCode)
                {
                    handleData(currentData);
                }
                else
                {
                    AddToGarbage(currentData.GetDataTypeCode(), currentData);
                    currentList[currentDataIndex] = MakeInstance(dataTypeCode, handleData, instantiateData);
                }
            }
            else
            {
                currentList.Add(MakeInstance(dataTypeCode, handleData, instantiateData));
            }
            currentDataIndex++;
        }

        public void EndOperation()
        {
            if (currentDataIndex < currentList.Count)
            {
                int start = currentDataIndex;
                int end = currentList.Count - 1;
                for (int i = start; i <= end; i++)
                {
                    currentList[i].Release();
                }
                currentList.RemoveRange(start, currentList.Count - start);
            }
            foreach (var stack in garbage)
            {
                foreach (var data in stack.Value) data.Release();
            }
            garbage.Clear();

            currentOperation = null;
        }
    }
}
