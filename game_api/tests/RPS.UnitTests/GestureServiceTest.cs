using AutoMapper;
using FizzWare.NBuilder;
using NSubstitute;
using RPS.Application.Models.Game;
using RPS.Application.Services;
using FluentAssertions;
using RPS.Core.Enums;
using RPS.Core.Services;

public class GestureServiceServiceTests
{
    public GestureServiceServiceTests()
    {
    }

    [Theory]
    [InlineData(Gesture.Paper, Gesture.Rock, true)]
    [InlineData(Gesture.Paper, Gesture.Scissors, false)]
    [InlineData(Gesture.Paper, Gesture.Paper, false)]
    [InlineData(Gesture.Scissors, Gesture.Paper, true)]
    [InlineData(Gesture.Scissors, Gesture.Rock, false)]
    [InlineData(Gesture.Scissors, Gesture.Scissors, false)]
    [InlineData(Gesture.Rock, Gesture.Scissors, true)]
    [InlineData(Gesture.Rock, Gesture.Paper, false)]
    [InlineData(Gesture.Rock, Gesture.Rock, false)]

    public void Beat_Should_Return_Bool(Gesture myGesture, Gesture opponentGesture, bool result)
    {
        GestureService.Beat(myGesture, opponentGesture).Should().Be(result);
    }
}
