namespace FileAnalysisService.Services
{
    public static class TextAnalyzer
    {
        public static int CountParagraphs(string text)
        {
            return text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int CountWords(string text)
        {
            return text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int CountCharacters(string text)
        {
            return text.Length;
        }
    }
}
