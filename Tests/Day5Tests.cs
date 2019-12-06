using Puzzles;
using Xunit;

namespace Tests
{
    public class Day5Tests
    {
        [Theory]
        [InlineData("11011", 1, 1, 0, 11)]
        [InlineData("11111", 1, 1, 1, 11)]
        [InlineData("01111", 0, 1, 1, 11)]
        [InlineData("1111", 0, 1, 1, 11)]
        [InlineData("10111", 1, 0, 1, 11)]
        [InlineData("10101", 1, 0, 1, 1)]
        [InlineData("11199", 1, 1, 1, 99)]
        [InlineData("199", 0, 0, 1, 99)]
        [InlineData("102", 0, 0, 1, 2)]
        [InlineData("104", 0, 0, 1, 4)]
        public void Test1(string firstInput, int thirdParamMode, int secondParamMode, int firstParamMode, int opCode)
        {
            var result = new Day5().GetParameters(firstInput);
            Assert.Equal(opCode, result.opCode);
            Assert.Equal(firstParamMode, result.firstParameterMode);
            Assert.Equal(secondParamMode, result.secondParameterMode);
            Assert.Equal(thirdParamMode, result.thirdParameterMode);
        }
    }
}
