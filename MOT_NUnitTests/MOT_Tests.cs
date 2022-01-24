using MOT_History.Data;
using MOT_History.Models;
using NUnit.Framework;

namespace MOT_NUnitTests
{
    public class Tests
    {
        [Test]
        public void BuildAPIUrl_Returns_Valid_URL_String()
        {
            //Arrange
            DataService dataService = new DataService();
            //Act
            var result = dataService.BuildAPIUrl("GH89TTY");
            //Assert
            Assert.AreEqual("https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=GH89TTY", result);
        }
        [Test]
        public void GetMOTHistory_Returns_Valid_Result()
        {
            DataService dataService = new DataService();
            var result = dataService.GetMOTHistory("SH63UDY");
            Assert.True(result.Result[0].make == "PEUGEOT");
            Assert.AreEqual(result.Result[0].primaryColour, "Red");
        }
        [Test]
        public void GetMOTHistory_Returns_Invalid_Result()
        {
            DataService dataService = new DataService();
            var result = dataService.GetMOTHistory("bat");
            Assert.IsNull(result.Result);
        }
        [Test]
        public void GetMOTHistory_Converts_Json_To_Car_Obj()
        {
            DataService dataService = new DataService();
            var result = dataService.GetMOTHistory("SH63UDY");
            Assert.IsInstanceOf(typeof(Car), result.Result[0]);
        }
        [Test]
        public void GetMOTHistory_Converts_Json_To_MotTest_Obj()
        {
            DataService dataService = new DataService();
            var result = dataService.GetMOTHistory("SH63UDY");
            Assert.IsInstanceOf(typeof(MotTest), result.Result[0].motTests[0]);
        }
        [Test]
        public void GetMOTHistory_Converts_Json_To_RfrAndComment_Obj()
        {
            DataService dataService = new DataService();
            var result = dataService.GetMOTHistory("SH63UDY");
            Assert.IsInstanceOf(typeof(RfrAndComment), result.Result[0].motTests[1].rfrAndComments[0]);
        }
    }
}