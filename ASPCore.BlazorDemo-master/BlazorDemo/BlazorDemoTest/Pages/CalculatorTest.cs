#nullable enable
using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using BlazorDemo.Pages;
using Bunit;
using Xunit;

namespace BlazorDemoTest.Pages
{
    public sealed class CalculatorTest: TestContext
    {
        private readonly IRenderedComponent<Calculator> _calculatorComponent;
        private readonly IElement _firstInput;
        private readonly IElement _secondInput;
        private readonly IElement _resultInput;
        
        public CalculatorTest()
        {
            _calculatorComponent = RenderComponent<Calculator>();
            _firstInput = 
                _calculatorComponent.Find("input[placeholder=\"Enter First Number\"]");
            _secondInput =
                _calculatorComponent.Find("input[placeholder=\"Enter Second Number\"]");
            _resultInput = 
                _calculatorComponent.Find("input[readonly]");
        }
        
        [Fact]
        public void Should_displayResult_when_addingNumbers()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            _firstInput.Change("15");
            _secondInput.Change("10");
            var buttons = _calculatorComponent.FindAll("button");
            var addButton = buttons[0];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("25", result);
        }
        
        private static void VerifyInitialState(
            IElement firstInput,
            IElement secondInput,
            IElement resultInput)
        {
            firstInput.MarkupMatches("<input placeholder=\"Enter First Number\" >");
            secondInput.MarkupMatches("<input placeholder=\"Enter Second Number\" >");
            resultInput.MarkupMatches("<input readonly=\"\" >");
        }
        
        [Fact]
        public void Should_displayResult_when_subtractingNumbers()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("15");
            _secondInput.Change("10");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[1];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("5", result);
        }
        
        [Fact]
        public void Should_displayResult_when_multiplyingNumbers()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("15");
            _secondInput.Change("10");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[2];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("150", result);
        }
        
        [Fact]
        public void Should_displayResult_when_dividingNumbers()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);

            //Act:
            _firstInput.Change("50");
            _secondInput.Change("10");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[3];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("5", result);
        }

        [Fact]
        public void Should_displayError_when_dividingByZero()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("50");
            _secondInput.Change("0");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[3];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("Cannot Divide by Zero", result);
        }
        
        [Fact]
        public void Should_displayResult_when_calculatingSquareRootOfFirstInput()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("16");
            _secondInput.Change("0");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[4];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("4", result);
        }

        [Theory]
        [MemberData(nameof(SquareRootData))]
        public void Should_ignoreSecondInput_when_calculatingSquareRootOfFirstInput(
            string firstInput,
            string secondInput,
            string expectedResult)
        {
            _firstInput.Change(firstInput);
            _secondInput.Change(secondInput);
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[4];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal(expectedResult, result);
        }

        private static IEnumerable<object[]> SquareRootData()
        {
            return new List<object[]>{
                new object[] {16, 3, 4},
                new object[] {25, "int.MaxValue", 5},
                new object[] {0, String.Empty, 0}

            };
        }
        
        [Fact]
        public void Should_displayNaN_when_calculatingSquareRootOfNegativeInput()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("-16");
            _secondInput.Change("0");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[4];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("NaN", result);
        }
        
        [Fact]
        public void Should_displayResult_when_calculatingFirstElementUpliftedToThePowerOfSecondElement()
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("16");
            _secondInput.Change("0.5");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[5];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("4", result);
        }

        [Fact]
        public void Should_resetResult_when_resetButtonIsClicked()
        {
            //Arrange:
            _firstInput.Change("5");
            _secondInput.Change("5");
            _resultInput.Change("10");
            
            //Act:
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[6];
            addButton.Click();
            
            //Assert:
            string? firstValue = _firstInput.GetAttribute("value");
            Assert.Equal("Enter First Number", firstValue);
            string? secondValue = _secondInput.GetAttribute("value");
            Assert.Equal("Enter Second Number", secondValue);
            string? resultValue = _resultInput.GetAttribute("value");
            Assert.Equal("", resultValue);
        }

        [Theory]
        [MemberData(nameof(ButtonData))]
        public void Should_reset_when_invalidInputIsReceived(int buttonNumber)
        {
            VerifyInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("Garbage input");
            _secondInput.Change("super duper ultra garbage input");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[buttonNumber];
            addButton.Click();
            
            //Assert:
            string? firstValue = _firstInput.GetAttribute("value");
            Assert.Equal("Enter First Number", firstValue);
            string? secondValue = _secondInput.GetAttribute("value");
            Assert.Equal("Enter Second Number", secondValue);
            string? resultValue = _resultInput.GetAttribute("value");
            Assert.Equal("", resultValue);
        }

        private static IEnumerable<object[]> ButtonData()
        {
            return new List<object[]>
            {
                new object[] {0},
                new object[] {1},
                new object[] {2},
                new object[] {3},
                new object[] {4},
                new object[] {5}
            };
        }
    }
}