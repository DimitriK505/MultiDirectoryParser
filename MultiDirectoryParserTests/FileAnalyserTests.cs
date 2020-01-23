using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiDirectoryParser;
using Moq;
using System.Collections.Generic;

namespace MultiDirectoryParserTests
{
    [TestClass]
    public class FileAnalyserTests
    {
        Mock<ILogger> loggerMock = new Mock<ILogger>();
        [TestMethod]
        public void EmtpyList()
        {            
            Mock<ILineReader> lineReaderMock = new Mock<ILineReader>();
            lineReaderMock.Setup(x => x.GetLines(It.IsAny<string>())).Returns(new List<string>());

            FileAnalyser f = new FileAnalyser(loggerMock.Object, lineReaderMock.Object);
            f.AnalyzeLines(new FileDetails("",0));
            Assert.AreEqual(f.TotalLines, 0);
            Assert.AreEqual(f.TotalEmptyLines, 0);
            Assert.AreEqual(f.TotalDashLines, 0);
        }

        [TestMethod]
        public void SingleLine()
        {
            Mock<ILineReader> lineReaderMock = new Mock<ILineReader>();
            lineReaderMock.Setup(x => x.GetLines(It.IsAny<string>())).Returns(new List<string>() { "Test Line" });

            FileAnalyser f = new FileAnalyser(loggerMock.Object, lineReaderMock.Object);
            f.AnalyzeLines(new FileDetails("", 0));
            Assert.AreEqual(f.TotalLines, 1);
            Assert.AreEqual(f.TotalEmptyLines, 0);
            Assert.AreEqual(f.TotalDashLines, 0);
        }

        [TestMethod]
        public void SingleEmptyLine()
        {
            Mock<ILineReader> lineReaderMock = new Mock<ILineReader>();
            lineReaderMock.Setup(x => x.GetLines(It.IsAny<string>())).Returns(new List<string>() { "--Test Line" });

            FileAnalyser f = new FileAnalyser(loggerMock.Object, lineReaderMock.Object);
            f.AnalyzeLines(new FileDetails("", 0));
            Assert.AreEqual(f.TotalLines, 1);
            Assert.AreEqual(f.TotalEmptyLines, 0);
            Assert.AreEqual(f.TotalDashLines, 1);
        }

        [TestMethod]
        public void SingleDashLine()
        {
            Mock<ILineReader> lineReaderMock = new Mock<ILineReader>();
            lineReaderMock.Setup(x => x.GetLines(It.IsAny<string>())).Returns(new List<string>() { "--Test Line" });

            FileAnalyser f = new FileAnalyser(loggerMock.Object, lineReaderMock.Object);
            f.AnalyzeLines(new FileDetails("", 0));
            Assert.AreEqual(f.TotalLines, 1);
            Assert.AreEqual(f.TotalEmptyLines, 0);
            Assert.AreEqual(f.TotalDashLines, 1);
        }

        [TestMethod]
        public void ThreeDifferentLines()
        {
            Mock<ILineReader> lineReaderMock = new Mock<ILineReader>();
            lineReaderMock.Setup(x => x.GetLines(It.IsAny<string>())).Returns(new List<string>() { "", "--Test Line", "Normal Line" });

            FileAnalyser f = new FileAnalyser(loggerMock.Object, lineReaderMock.Object);
            f.AnalyzeLines(new FileDetails("", 0));
            Assert.AreEqual(f.TotalLines, 3);
            Assert.AreEqual(f.TotalEmptyLines, 1);
            Assert.AreEqual(f.TotalDashLines, 1);
        }
    }

}
