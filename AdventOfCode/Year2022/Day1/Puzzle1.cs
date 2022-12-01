﻿using System;
using System.Reflection;
using HLE;
using HLE.Resources;

namespace AdventOfCode.Year2022.Day1;

public sealed class Puzzle1
{
    private readonly string _input;

    public Puzzle1()
    {
        ResourceReader reader = new(Assembly.GetExecutingAssembly(), false);
        string? input = reader.ReadResource("Year2022.Day1.Input.txt");
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        _input = input;
    }

    public (uint Max, uint TopThree) Solve()
    {
        ReadOnlySpan<char> input = _input;
        input = input[..^2];
        Span<Range> lineRanges = stackalloc Range[input.Length];
        int lineRangesLength = input.GetRangesOfSplit(Environment.NewLine, lineRanges);
        lineRanges = lineRanges[..lineRangesLength];

        uint max = 0;
        uint elfTotal = 0;
        Span<uint> topThree = stackalloc uint[3];
        for (int i = 0; i < lineRangesLength; i++)
        {
            ReadOnlySpan<char> line = input[lineRanges[i]];
            if (line.Length == 0)
            {
                CheckForHigherTopThree(topThree, elfTotal);
                max = elfTotal > max ? elfTotal : max;
                elfTotal = 0;
                continue;
            }

            elfTotal += uint.Parse(line);
        }

        uint topThreeSum = topThree[0] + topThree[1] + topThree[2];
        return (max, topThreeSum);
    }

    private static void CheckForHigherTopThree(Span<uint> topThree, uint newValue)
    {
        int minIdx = -1;
        uint min = uint.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            uint top = topThree[i];
            if (top >= min)
            {
                continue;
            }

            min = top;
            minIdx = i;
        }

        if (min < newValue && minIdx != -1)
        {
            topThree[minIdx] = newValue;
        }
    }
}
