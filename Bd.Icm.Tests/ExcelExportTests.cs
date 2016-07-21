using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bd.Icm.Tests
{
    [TestClass]
    public class ExcelExportTests : AuthenticatedTest
    {
        private static Instrument AddInstrument()
        {
            return InstrumentTests.ExistingObject();
        }

        private static Part AddPart()
        {
            return PartTests.NewChildObject();
        }

        private static PartAction AddPartAction()
        {
            return PartActionTests.NewObject();
        }

        private static PartMetadata AddPartSetting()
        {
            return PartMetadataTests.NewObject();
        }

        [TestMethod]
        public void ExportInstrumentWithNullThrowsArgumentNullException()
        {
            try
            {
                var stream = ExcelExporter.CreateInstrumentExport(null);
            }
            catch (ArgumentNullException ex)
            {
                return;
            }
            Assert.Fail("Expected ArgumentNullException exception.");
        }

        [TestMethod]
        public void ExportInstrumentFound() {
            var newInstrument = AddInstrument();
            var newPart1 = AddPart();
            var newPart2 = AddPart();
            var newPart3 = AddPart();
            var newPart4 = AddPart();
            var newPart5 = AddPart();
            var newPart6 = AddPart();
            var newPart7 = AddPart();
            var newPart8 = AddPart();
            newPart1.Actions.Add(AddPartAction());
            var partSetting1 = AddPartSetting();
            partSetting1.MetaKey = "Belt Tension";
            partSetting1.MetaValue = "55";
            newPart1.Name = "Part 1 with setting";
            newPart1.Metadata.Add(partSetting1);

            var partSetting2 = AddPartSetting();
            partSetting2.MetaKey = "Belt Tension";
            partSetting2.MetaValue = "59";
            newPart2.Name = "Part 2 with setting";
            newPart2.Metadata.Add(partSetting2);

            newPart1.Name = "Part 1";
            newPart2.Name = "Part 2";
            newPart3.Name = "Part 3";
            newPart4.Name = "Part 4";
            newPart5.Name = "Part 5";
            newPart6.Name = "Part 6";
            newPart7.Name = "Part 7";
            newPart8.Name = "Part 8";
            newPart1.Parts.Add(newPart2);
            newPart1.Parts.Add(newPart3);
            newPart1.Parts.Add(newPart4);
            newPart4.Parts.Add(newPart5);
            newPart4.Parts.Add(newPart6);
            newPart6.Parts.Add(newPart7);
            newPart6.Parts.Add(newPart8);

            newInstrument.Parts.Add(newPart1);
            newInstrument = newInstrument.Save();

            var instruments = new List<Instrument> {newInstrument};
            var stream = ExcelExporter.CreateInstrumentExport(instruments);

            using (var fileStream = new FileStream("C:\\Temp\\BD\\Export.xlsx", FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
            Assert.IsTrue(stream != null);
        }
    }
}
