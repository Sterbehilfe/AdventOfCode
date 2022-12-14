using System;

namespace AdventOfCode.Year2022.Day12;

public ref struct Queue
{
    private readonly Span<nuint> _queue;
    private readonly int _lastIndex;
    private int _enqueueIndex;
    private int _dequeueIndex;
    private int _count;

    public Queue(Span<nuint> queue)
    {
        _queue = queue;
        _lastIndex = queue.Length - 1;
    }

    public void Enqueue(nuint item)
    {
        if (_enqueueIndex == _lastIndex)
        {
            if (_dequeueIndex == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_enqueueIndex), "Queue is full");
            }

            Span<nuint> copy = _queue[_dequeueIndex.._enqueueIndex];
            copy.CopyTo(_queue);
            _dequeueIndex = 0;
            _enqueueIndex = copy.Length;
        }

        _queue[_enqueueIndex++] = item;
        _count++;
    }

    public nuint Dequeue()
    {
        _count--;
        return _queue[_dequeueIndex++];
    }

    public bool TryDequeue(out nuint item)
    {
        item = Dequeue();
        return _count > -1;
    }
}
