namespace BackEnd {
    internal interface IExporter {
        /// <summary>
        /// Daný seznam zaměstnavatelů se zaměstnanci uloží do souboru.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="outputPath"></param>
        public void SaveTo(List<Employer> inputList, string outputPath);
    }
}
