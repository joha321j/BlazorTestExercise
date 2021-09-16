using AngleSharp.Dom;
using BlazorDemo.Pages;
using Bunit;
using Xunit;

namespace BlazorDemoTest.Pages
{
    public class CalculatorTest: TestContext
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
            _firstInput.Change("15");
            _secondInput.Change("10");
            var buttons = _calculatorComponent.FindAll("button");
            var addButton = buttons[0];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("25", result);
        }
        
        private static void AssertInitialState(
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);

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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
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
        
        [Fact]
        public void Should_displayNaN_when_calculatingSquareRootOfNegativeInput()
        {
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
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
            AssertInitialState(_firstInput, _secondInput, _resultInput);
            
            //Act:
            _firstInput.Change("16");
            _secondInput.Change("0");
            var buttons = 
                _calculatorComponent.FindAll("button");
            var addButton = buttons[5];
            addButton.Click();
            
            //Assert:
            var result = _resultInput.GetAttribute("value");
            Assert.Equal("1", result);
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
    }
}