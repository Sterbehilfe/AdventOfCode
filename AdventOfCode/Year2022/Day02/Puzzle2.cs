using System;
using System.Runtime.CompilerServices;
using HLE;

namespace AdventOfCode.Year2022.Day02;

public sealed class Puzzle2 : Puzzle
{
    private const byte _rockOpponent = 65;
    private const byte _paperOpponent = 66;
    private const byte _scissorsOpponent = 67;
    private const byte _rockMe = 88;
    private const byte _paperMe = 89;
    private const byte _scissorsMe = 90;
    private const byte _lossMe = 88;
    private const byte _drawMe = 89;
    private const byte _winMe = 90;

    private const byte _rockBonus = 1;
    private const byte _paperBonus = 2;
    private const byte _scissorsBonus = 3;

    private const byte _win = 6;
    private const byte _draw = 3;
    private const byte _loss = 0;

    public Puzzle2() : base("Year2022.Day02.Input.txt")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public (ushort ScorePart1, ushort ScorePart2) Solve()
    {
        ReadOnlySpan<char> input = _input;
        Span<Range> lineRanges = stackalloc Range[2500];
        input.GetRangesOfSplit(Environment.NewLine, lineRanges);

        ushort scorePart1 = 0;
        ushort scorePart2 = 0;
        for (int i = 0; i < 2500; i++)
        {
            ReadOnlySpan<char> line = input[lineRanges[i]];
            byte opponent = (byte)line[0];
            byte me = (byte)line[2];

            scorePart1 += CalculateScorePart1(me, opponent);
            scorePart2 += CalculateScorePart2(me, opponent);
        }

        return (scorePart1, scorePart2);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static ushort CalculateScorePart1(byte me, byte opponent)
    {
        ushort score = 0;
        switch (me)
        {
            case _rockMe:
            {
                score += _rockBonus;
                score += opponent switch
                {
                    _rockOpponent => _draw,
                    _paperOpponent => _loss,
                    _scissorsOpponent => _win,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
            case _paperMe:
            {
                score += _paperBonus;
                score += opponent switch
                {
                    _rockOpponent => _win,
                    _paperOpponent => _draw,
                    _scissorsOpponent => _loss,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
            case _scissorsMe:
            {
                score += _scissorsBonus;
                score += opponent switch
                {
                    _rockOpponent => _loss,
                    _paperOpponent => _win,
                    _scissorsOpponent => _draw,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
        }

        return score;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static ushort CalculateScorePart2(byte me, byte opponent)
    {
        ushort score = 0;
        switch (me)
        {
            case _lossMe:
            {
                score += _loss;
                score += opponent switch
                {
                    _rockOpponent => _scissorsBonus,
                    _paperOpponent => _rockBonus,
                    _scissorsOpponent => _paperBonus,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
            case _drawMe:
            {
                score += _draw;
                score += opponent switch
                {
                    _rockOpponent => _rockBonus,
                    _paperOpponent => _paperBonus,
                    _scissorsOpponent => _scissorsBonus,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
            case _winMe:
            {
                score += _win;
                score += opponent switch
                {
                    _rockOpponent => _paperBonus,
                    _paperOpponent => _scissorsBonus,
                    _scissorsOpponent => _rockBonus,
                    _ => throw new ArgumentOutOfRangeException()
                };
                break;
            }
        }

        return score;
    }
}
