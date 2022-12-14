using System;
using System.Runtime.CompilerServices;
using HLE;

namespace AdventOfCode.Year2021.Day02;

public sealed class Puzzle2 : Puzzle
{
    private const char _cmdForward = 'f';
    private const char _cmdUp = 'u';
    private const char _cmdDown = 'd';
    private const char _zero = '0';

    public Puzzle2() : base("Year2021.Day02.Input.txt")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public int SolvePart1()
    {
        ReadOnlySpan<char> input = _input;
        Span<Range> lineRanges = stackalloc Range[1000];
        input.GetRangesOfSplit(Environment.NewLine, lineRanges);

        int x = 0;
        int y = 0;
        for (int i = 0; i < 1000; i++)
        {
            ReadOnlySpan<char> line = input[lineRanges[i]];
            byte value = (byte)(line[^1] - _zero);
            char cmd = line[0];
            switch (cmd)
            {
                case _cmdForward:
                    x += value;
                    break;
                case _cmdDown:
                    y += value;
                    break;
                case _cmdUp:
                    y -= value;
                    break;
            }
        }

        return x * y;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public int SolvePart2()
    {
        ReadOnlySpan<char> input = _input;
        Span<Range> lineRanges = stackalloc Range[1000];
        input.GetRangesOfSplit(Environment.NewLine, lineRanges);

        int x = 0;
        int y = 0;
        int aim = 0;
        for (int i = 0; i < 1000; i++)
        {
            ReadOnlySpan<char> line = input[lineRanges[i]];
            byte value = (byte)(line[^1] - _zero);
            char cmd = line[0];
            switch (cmd)
            {
                case _cmdForward:
                    x += value;
                    y += aim * value;
                    break;
                case _cmdDown:
                    aim += value;
                    break;
                case _cmdUp:
                    aim -= value;
                    break;
            }
        }

        return x * y;
    }
}
