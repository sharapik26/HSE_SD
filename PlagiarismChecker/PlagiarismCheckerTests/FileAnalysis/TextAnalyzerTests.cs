using FileAnalysisService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismCheckerTests.FileAnalysis
{
    public class TextAnalyzerTests
    {
        [Fact]
        public void CountParagraphs_ReturnsCorrectCount()
        {
            var text = "Line 1\n\nLine 2\nLine 3\n\nLine 4";
            var result = TextAnalyzer.CountParagraphs(text);
            Assert.Equal(5, result); // 5 непустых строк
        }

        [Fact]
        public void CountWords_ReturnsCorrectCount()
        {
            var text = "This   is a\n test   with  multiple   spaces.";
            var result = TextAnalyzer.CountWords(text);
            Assert.Equal(8, result);
        }

        [Fact]
        public void CountCharacters_ReturnsCorrectCount()
        {
            var text = "123456789";
            var result = TextAnalyzer.CountCharacters(text);
            Assert.Equal(9, result);
        }

        [Fact]
        public void CountWords_WithOnlyWhitespace_ReturnsZero()
        {
            var result = TextAnalyzer.CountWords("    \n\t  ");
            Assert.Equal(0, result);
        }

        [Fact]
        public void CountParagraphs_WithEmptyString_ReturnsZero()
        {
            var result = TextAnalyzer.CountParagraphs("");
            Assert.Equal(0, result);
        }

        [Fact]
        public void CountCharacters_WithEmptyString_ReturnsZero()
        {
            var result = TextAnalyzer.CountCharacters("");
            Assert.Equal(0, result);
        }
    }
}
