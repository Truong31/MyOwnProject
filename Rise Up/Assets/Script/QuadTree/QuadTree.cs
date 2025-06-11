﻿using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public struct QuadElement<T> where T : unmanaged
{
    public float2 position;
    public T element;
}
public struct QuadNode
{
    public int firstChildIndex;
    public ushort count;
    public bool isLeaf;
}

[Serializable]
public unsafe partial struct QuadTree<T> : IDisposable where T : unmanaged
{
    [NativeDisableUnsafePtrRestriction]
    UnsafeList<QuadElement<T>>* elements;

    [NativeDisableUnsafePtrRestriction]
    UnsafeList<int>* lookup;

    [NativeDisableUnsafePtrRestriction]
    UnsafeList<QuadNode>* nodes;

    QuadBounds bounds;
    [SerializeField] int elementsCount;
    [SerializeField] byte maxDepth;
    [SerializeField] ushort maxLeafElements;

    public QuadTree(QuadBounds bounds, Allocator allocator = Allocator.Temp,
        byte maxDepth = 6, ushort maxLeafElements = 32, int initialElementsCapacity = 256)
    {
        this.bounds = bounds;
        this.maxDepth = maxDepth;
        this.maxLeafElements = maxLeafElements;
        elementsCount = 0;

        if (maxDepth > 8 || maxDepth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxDepth), "Max depth must be less than 9 and higher than 0");

        var totalSize = LookUpTable.DepthSizeLookup[maxDepth + 1];
        lookup = UnsafeList<int>.Create(
            totalSize,
            allocator,
            NativeArrayOptions.ClearMemory);

        nodes = UnsafeList<QuadNode>.Create(
           totalSize,
           allocator,
           NativeArrayOptions.ClearMemory);

        elements = UnsafeList<QuadElement<T>>.Create(
           initialElementsCapacity,
           allocator);
    }
    public void ClearAndBulkInsert(NativeArray<QuadElement<T>> incomingElements)
    {
        Clear();

        if (elements->Capacity < incomingElements.Length)
        {
            elements->Resize(math.max(incomingElements.Length, elements->Capacity * 2));
        }

        var mortonCodes = new NativeArray<int>(incomingElements.Length, Allocator.Temp);
        var depthExtentsScaling = LookUpTable.DepthLookup[maxDepth] / bounds.extents;
        for (int i = 0; i < incomingElements.Length; i++)
        {
            var positionElement = incomingElements[i].position;
            positionElement -= bounds.center;
            positionElement.y = -positionElement.y;
            var position = (positionElement + bounds.extents) * .5f;
            position *= depthExtentsScaling;
            mortonCodes[i] = LookUpTable.MortonLookup[(int)position.x] | (LookUpTable.MortonLookup[(int)position.y] << 1);

            int atIndex = 0;
            for (int depth = maxDepth; depth >= 0; depth--)
            {
                (*(int*)((IntPtr)lookup->Ptr + atIndex * sizeof(int)))++;
                atIndex += IncrementIndex(mortonCodes[i], depth);
            }
        }
        RecursivePrepareLeaves();

        for (int i = 0; i < incomingElements.Length; i++)
        {
            int atIndex = 0;
            for (int depth = maxDepth; depth >= 0; depth--)
            {
                var node = UnsafeUtility.ReadArrayElement<QuadNode>(nodes->Ptr, atIndex);
                if (node.isLeaf)
                {
                    UnsafeUtility.WriteArrayElement(elements->Ptr, node.firstChildIndex + node.count, incomingElements[i]);
                    node.count++;
                    UnsafeUtility.WriteArrayElement(nodes->Ptr, atIndex, node);
                    break;
                }
                atIndex += IncrementIndex(mortonCodes[i], depth);
            }
        }

        mortonCodes.Dispose();
    }
    private int IncrementIndex(int mortonCodes, int depth)
    {
        int shiftedMortonCode = (mortonCodes >> ((depth - 1) * 2)) & 0b11;
        return (LookUpTable.DepthSizeLookup[depth] * shiftedMortonCode) + 1;
    }
    private void RecursivePrepareLeaves(int previousOffset = 1, int depth = 1)
    {
        for (int i = 0; i < 4; i++)
        {
            var atIndex = previousOffset + i * LookUpTable.DepthSizeLookup[maxDepth - depth + 1];
            var elementCount = UnsafeUtility.ReadArrayElement<int>(lookup->Ptr, atIndex);
            if (elementCount > maxLeafElements && depth < maxDepth)
            {
                RecursivePrepareLeaves(atIndex + 1, depth + 1);
            }
            else if (elementCount != 0)
            {
                var node = new QuadNode { firstChildIndex = elementsCount, count = 0, isLeaf = true };
                UnsafeUtility.WriteArrayElement(nodes->Ptr, atIndex, node);
                elementsCount += elementCount;
            }
        }
    }
    public void RangeQuery(QuadBounds bounds, NativeList<QuadElement<T>> results)
    {
        new QuadTreeRangeQuery(this, bounds, results);
    }
    public void DrawGizmos(QuadBounds boundsParent = default, int prevousOffset = 1, int depth = 1)
    {
        Gizmos.DrawWireCube((Vector2)boundsParent.center, (Vector2)boundsParent.Size);
        if (lookup == null) return;
        else if (boundsParent.Equals(default(QuadBounds))) boundsParent = bounds;

        var depthSize = LookUpTable.DepthSizeLookup[maxDepth - depth + 1];
        for (int i = 0; i < 4; i++)
        {
            var boundsChild = boundsParent.GetBoundsChild(i);
            int atIndex = prevousOffset + i * depthSize;
            var elementCount = UnsafeUtility.ReadArrayElement<int>(lookup->Ptr, atIndex);
            if (elementCount > maxLeafElements && depth < maxDepth)
            {
                DrawGizmos(boundsChild, atIndex + 1, depth + 1);
            }
            else if (elementCount != 0)
            {
                Gizmos.DrawWireCube((Vector2)boundsChild.center, (Vector2)boundsChild.Size);
            }
        }
    }
    public void Clear()
    {
        UnsafeUtility.MemClear(lookup->Ptr, lookup->Capacity * UnsafeUtility.SizeOf<int>());
        UnsafeUtility.MemClear(nodes->Ptr, nodes->Capacity * UnsafeUtility.SizeOf<QuadNode>());
        UnsafeUtility.MemClear(elements->Ptr, elements->Capacity * UnsafeUtility.SizeOf<QuadElement<T>>());
        elementsCount = 0;
    }
    public void Dispose()
    {
        UnsafeList<QuadElement<T>>.Destroy(elements);
        UnsafeList<int>.Destroy(lookup);
        UnsafeList<QuadNode>.Destroy(nodes);
        elements = null;
        lookup = null;
        nodes = null;
    }

}

public unsafe partial struct QuadTree<T> where T : unmanaged
{
}
